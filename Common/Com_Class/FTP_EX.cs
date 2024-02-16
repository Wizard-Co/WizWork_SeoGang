using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Collections.Generic;
using System.Windows.Forms;
//*******************************************************************************
//프로그램명    FTP_EX.cs
//메뉴ID        
//설명          공통 메서드
//작성일        2016.12.06
//개발자        김은미
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************
//
//
//*******************************************************************************

namespace Common
{
	public class FTP_EX
	{
		private string host = null;
		private string user = null;
		private string pass = null;
		private FtpWebRequest ftpRequest = null;
		private FtpWebResponse ftpResponse = null;
		private Stream ftpStream = null;
		private int bufferSize = 2048;

		/* Construct Object */
		public FTP_EX(string hostIP, string userName, string password) { host = hostIP; user = userName; pass = password; }

		/* Download File */
		public bool download(string remoteFile, string localFile)
		{
			try
			{
				/* Create an FTP Request */
				//string url = HttpUtility.UrlEncode("#");
				//url = remoteFile.Replace("#", url);

				//ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + url);
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(user, pass);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;


                ftpRequest.UsePassive = true;
                //ftpRequest.UsePassive = false;


                ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.DownloadFile;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Get the FTP Server's Response Stream */
				ftpStream = ftpResponse.GetResponseStream();
				/* Open a File Stream to Write the Downloaded File */
				FileStream localFileStream = new FileStream(localFile, FileMode.Create);
				/* Buffer for the Downloaded Data */
				byte[] byteBuffer = new byte[bufferSize];
				int bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
				/* Download the File by Writing the Buffered Data Until the Transfer is Complete */
				try
				{
					while (bytesRead > 0)
					{
						localFileStream.Write(byteBuffer, 0, bytesRead);
						bytesRead = ftpStream.Read(byteBuffer, 0, bufferSize);
					}
				}
				catch (Exception ex) {
                    System.Windows.Forms.MessageBox.Show("1" + ex.Message + " / " + ex.Source);
                    throw ex; 
                }
				/* Resource Cleanup */
				localFileStream.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
                return true;
			}
			catch (Exception ex) 
            {
                System.Windows.Forms.MessageBox.Show("2" + ex.Message + " / " + ex.Source);
                throw ex;
            }
		}

		/* Upload File */
		public bool upload(string remoteFile, string localFile)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + remoteFile);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(user, pass);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
				/* Establish Return Communication with the FTP Server */
				ftpStream = ftpRequest.GetRequestStream();
				/* Open a File Stream to Read the File for Upload */
				FileStream localFileStream = new FileStream(localFile, FileMode.Open);
				/* Buffer for the Upload Data */
				byte[] byteBuffer = new byte[bufferSize];
				int bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
				/* Upload the File by Sending the Buffered Data Until the Transfer is Complete */
				try
				{
					while (bytesSent != 0)
					{
						ftpStream.Write(byteBuffer, 0, bytesSent);
						bytesSent = localFileStream.Read(byteBuffer, 0, bufferSize);
					}
				}
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
				/* Resource Cleanup */
				localFileStream.Close();
				ftpStream.Close();
				ftpRequest = null;
                return true;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			return false;
		}

		/* Delete File */
		public bool delete(string deleteFile)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + deleteFile);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(user, pass);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Resource Cleanup */
				ftpResponse.Close();
				ftpRequest = null;
                return true;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			return false;
		}

        //2016.12.05.kem
        /* Remove Directory */
        public bool removeDir(string deleteDirectory)
        {
            try
            {
                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + deleteDirectory);
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* When in doubt, use these options */
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;

                List<string> lines = new List<string>();

                using (FtpWebResponse listResponse = (FtpWebResponse)ftpRequest.GetResponse())
                using (Stream listStream = listResponse.GetResponseStream())
                using (StreamReader listReader = new StreamReader(listStream))
                {
                    while (!listReader.EndOfStream)
                    {
                        lines.Add(listReader.ReadLine());
                    }
                }

                foreach (string line in lines)
                {
                    string[] tokens = line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                    string name = tokens[8].ToString();
                    string permissions = tokens[0].ToString();

                    string fileUrl = host + "/" + deleteDirectory + "/" + name;

                    FtpWebRequest deleteRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                    deleteRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                    deleteRequest.Credentials = new NetworkCredential(user, pass);

                    deleteRequest.GetResponse();
                }

                /* Create an FTP Request */
                ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + deleteDirectory);
                /* Log in to the FTP Server with the User Name and Password Provided */
                ftpRequest.Credentials = new NetworkCredential(user, pass);
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;
                /* Establish Return Communication with the FTP Server */
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                /* Resource Cleanup */
                ftpResponse.Close();
                ftpRequest = null;
                return true;
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }
            return false;
        }

		/* Rename File */
		public bool rename(string currentFileNameAndPath, string newFileName)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + currentFileNameAndPath);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(user, pass);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.Rename;
				/* Rename the File */
				ftpRequest.RenameTo = newFileName;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Resource Cleanup */
				ftpResponse.Close();
				ftpRequest = null;
                return true;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			return false;
		}

		/* Create a New Directory on the FTP Server */
		public bool createDirectory(string newDirectory)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)WebRequest.Create(host + "/" + newDirectory);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(user, pass);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Resource Cleanup */
				ftpResponse.Close();
				ftpRequest = null;
                return true;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			return false;
		}

		/* Get the Date/Time a File was Created */
		public string getFileCreatedDateTime(string fileName)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + fileName);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(user, pass);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.GetDateTimestamp;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Establish Return Communication with the FTP Server */
				ftpStream = ftpResponse.GetResponseStream();
				/* Get the FTP Server's Response Stream */
				StreamReader ftpReader = new StreamReader(ftpStream);
				/* Store the Raw Response */
				string fileInfo = null;
				/* Read the Full Response Stream */
				try { fileInfo = ftpReader.ReadToEnd(); }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
				/* Resource Cleanup */
				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
				/* Return File Created Date Time */
				return fileInfo;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			/* Return an Empty string Array if an Exception Occurs */
			return "";
		}

		/* Get the Size of a File */
		public string getFileSize(string fileName)
		
        {
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + fileName);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(user, pass);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;


                ftpRequest.UsePassive = true;
                //ftpRequest.UsePassive = false; //227 Entering PassiveMode로 인한 false로 변경 


				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Establish Return Communication with the FTP Server */
				ftpStream = ftpResponse.GetResponseStream();
				/* Get the FTP Server's Response Stream */
				StreamReader ftpReader = new StreamReader(ftpStream);
				/* Store the Raw Response */
				string fileInfo = null;
				/* Read the Full Response Stream */
				try { while (ftpReader.Peek() != -1) { fileInfo = ftpReader.ReadToEnd(); } }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
				/* Resource Cleanup */
				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
				/* Return File Size */
				return fileInfo;
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			/* Return an Empty string Array if an Exception Occurs */
			return "";
		}
        /* 파일사이즈 검색 float형으로 반환*/
        public float GetFileSize(string filename)
        {
            float results = 0; //리턴할 파일 사이즈 값
            try
            {
                FtpWebRequest request = FtpWebRequest.Create(host + "/" + filename) as FtpWebRequest;//파일불러올 위치, 서버
                request.Credentials = new NetworkCredential(user, pass);
                request.UsePassive = true;
                //request.UsePassive = false;
                request.Method = WebRequestMethods.Ftp.GetFileSize;//Method를 GetFileSize로 설정합니다.
                results = 1;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                results = response.ContentLength;//이부분이 결과값으로 파일크기를 반환합니다.
                response.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return results;
        }







		/* List Directory Contents File/Folder Name Only */
		public string[] directoryListSimple(string directory)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(user, pass);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Establish Return Communication with the FTP Server */
				ftpStream = ftpResponse.GetResponseStream();
				/* Get the FTP Server's Response Stream */
				StreamReader ftpReader = new StreamReader(ftpStream);
				/* Store the Raw Response */
				string directoryRaw = null;
				/* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
				try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
				/* Resource Cleanup */
				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
				/* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
				try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			/* Return an Empty string Array if an Exception Occurs */
			return new string[] { "" };
		}

		/* List Directory Contents in Detail (Name, Size, Created, etc.) */
		public string[] directoryListDetailed(string directory)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(user, pass);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Establish Return Communication with the FTP Server */
				ftpStream = ftpResponse.GetResponseStream();
				/* Get the FTP Server's Response Stream */
				StreamReader ftpReader = new StreamReader(ftpStream);
				/* Store the Raw Response */
				string directoryRaw = null;
				/* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
				try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
				/* Resource Cleanup */
				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
				/* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
				try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			/* Return an Empty string Array if an Exception Occurs */
			return new string[] { "" };
		}

		/* List Directory Contents File/Folder Name Only */
		public string[] directoryListSimple(string directory, Encoding encoding)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(user, pass);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                
                /* Specify the Type of FTP Request */
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Establish Return Communication with the FTP Server */
				ftpStream = ftpResponse.GetResponseStream();
				/* Get the FTP Server's Response Stream */
				StreamReader ftpReader = new StreamReader(ftpStream, encoding);
                
				/* Store the Raw Response */
				string directoryRaw = null;
                /* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
                //try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
                //try { while (ftpReader.Peek() > -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }

                List<string> Listdirectory = null;
                
                try
                {
                    Listdirectory = GetFTPList(host + "/" + directory, user, pass);
                    foreach (string str in Listdirectory)
                    {
                        directoryRaw += "|" + str;
                    }
                }
                //try { while (ftpReader.Peek() > 0 || ftpReader.Peek() == 0) { directoryRaw += ftpReader.ReadLine() + "|"; } }
                catch (Exception ex)
                {
                    MessageBox.Show("@@" + ex.ToString() + "@@");
                    Console.WriteLine(ex.ToString());
                }
   
                /* Resource Cleanup */
                ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
				/* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
				try { string[] directoryList = directoryRaw.Split("|".ToCharArray());
                    return directoryList; }
				catch (Exception ex)
                {
                    MessageBox.Show("@@" + ex.ToString() + "@@");
                    Console.WriteLine(ex.ToString());
                }
			}
			catch (Exception ex)
            {
                MessageBox.Show("@@" + ex.ToString() + "@@");
                Console.WriteLine(ex.ToString());
            }
			/* Return an Empty string Array if an Exception Occurs */
			return new string[] { "" };
		}

        public List<string> GetFTPList(string targetURI, string userID, string password)

        {

            try

            {

                FtpWebRequest ftpWebRequest = WebRequest.Create(targetURI) as FtpWebRequest;



                ftpWebRequest.Credentials = new NetworkCredential(userID, password);

                ftpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;



                StreamReader streamReader = new StreamReader(ftpWebRequest.GetResponse().GetResponseStream());



                List<string> list = new List<string>();



                while (true)

                {

                    string fileName = streamReader.ReadLine();



                    if (string.IsNullOrEmpty(fileName))

                    {

                        break;

                    }



                    list.Add(fileName);

                }



                streamReader.Close();



                return list;

            }

            catch

            {

                return null;

            }

        }


        

		/* List Directory Contents in Detail (Name, Size, Created, etc.) */
		public string[] directoryListDetailed(string directory, Encoding encoding)
		{
			try
			{
				/* Create an FTP Request */
				ftpRequest = (FtpWebRequest)FtpWebRequest.Create(host + "/" + directory);
				/* Log in to the FTP Server with the User Name and Password Provided */
				ftpRequest.Credentials = new NetworkCredential(user, pass);
				/* When in doubt, use these options */
				ftpRequest.UseBinary = true;
				ftpRequest.UsePassive = true;
				ftpRequest.KeepAlive = true;
				/* Specify the Type of FTP Request */
				ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
				/* Establish Return Communication with the FTP Server */
				ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
				/* Establish Return Communication with the FTP Server */
				ftpStream = ftpResponse.GetResponseStream();
				/* Get the FTP Server's Response Stream */
				StreamReader ftpReader = new StreamReader(ftpStream, encoding);
				/* Store the Raw Response */
				string directoryRaw = null;
				/* Read Each Line of the Response and Append a Pipe to Each Line for Easy Parsing */
				try { while (ftpReader.Peek() != -1) { directoryRaw += ftpReader.ReadLine() + "|"; } }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
				/* Resource Cleanup */
				ftpReader.Close();
				ftpStream.Close();
				ftpResponse.Close();
				ftpRequest = null;
				/* Return the Directory Listing as a string Array by Parsing 'directoryRaw' with the Delimiter you Append (I use | in This Example) */
				try { string[] directoryList = directoryRaw.Split("|".ToCharArray()); return directoryList; }
				catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
			/* Return an Empty string Array if an Exception Occurs */
			return new string[] { "" };
		}
	}
}
