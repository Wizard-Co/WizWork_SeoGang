using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Windows.Forms;



namespace WizWork.Tools
{
    public class FTPAccess
{
    protected string _ftpURL;
    protected string _ftpUser;
    protected string _ftpPassword;
    protected string _localPath;
    protected long _fileSize;
    protected Action<string> actionMsg;
    protected Action<int> actionProgress;
 
    protected string _targetPath;
    protected string _fileName;
 
    /// <summary>
    ///
    /// </summary>
    /// <param name="ftpUrl">199.199.199.199:8033
    /// <param name="ftpUser">ftpuser
    /// <param name="ftpPassword">ftpuser
    /// <param name="localPath">D:\02.Document\04.work\FtpDownload
    /// <param name="actionMsg">메세지 출력한 actoin
    public FTPAccess(string ftpUrl, string ftpUser, string ftpPassword, string localPath, Action<string> actionMsg, Action<int> actionProgress)
    {
        this._ftpURL = ftpUrl;
        this._ftpUser = ftpUser;
        this._ftpPassword = ftpPassword;
        this._localPath = localPath;
        this.actionMsg = actionMsg;
        this.actionProgress = actionProgress;
    }
 
    public void SingleDownload(string sourcePath, string targetPath, string fileName)
    {
        this._targetPath = targetPath;
        this._fileName = fileName;
 
        string strDownloadPath = string.Format(@"ftp://{0}/{1}/{2}", _ftpURL, sourcePath, fileName);
 
        Uri ftpUri = new Uri(strDownloadPath);
 
        using (WebClient wc = new WebClient())
        {
            actionMsg(string.Format("{0} Access : ftp://{1}", DateTime.Now.ToString(), _ftpURL));
             
            // 파일 사이즈
            FtpWebRequest reqFtp = (FtpWebRequest)WebRequest.Create(ftpUri);
            reqFtp.Method = WebRequestMethods.Ftp.GetFileSize;
            reqFtp.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
            FtpWebResponse resFtp = (FtpWebResponse)reqFtp.GetResponse();
            _fileSize = resFtp.ContentLength;
            resFtp.Close();
 
            actionMsg(string.Format("{0} Login : {1}", DateTime.Now.ToString(), _ftpUser));
            actionMsg(string.Format("{0} Download FIle : {1}", DateTime.Now.ToString(), fileName));
            actionMsg(string.Format("{0} FileSize : {1}", DateTime.Now.ToString(), _fileSize.ToString("#,##0")));
            actionMsg(string.Format("{0} Download Start.", DateTime.Now.ToString()));
            wc.BaseAddress = strDownloadPath;
            wc.Credentials = new NetworkCredential(_ftpUser, _ftpPassword);
 
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler(wc_DownloadProgressChanged);
            wc.DownloadFileCompleted += new AsyncCompletedEventHandler(wc_DownloadFileCompleted);
 
            //폴더 없으면 생성
            if(!Directory.Exists(_localPath + "\\" + targetPath))
                Directory.CreateDirectory(_localPath + "\\" + targetPath);
 
            //파일 존재하면 삭제(안해도됨.)
            string downloadPath = string.Format(@"{0}\{1}\{2}", _localPath, targetPath, fileName);
            if (File.Exists(downloadPath))
                File.Delete(downloadPath);
 
            wc.DownloadFileAsync(ftpUri, downloadPath);
        }
    }
 
    //다운로드가 완료되었을 때
    void wc_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
        try
        {
            actionMsg(string.Format("{0} Complete.", DateTime.Now.ToString()));              
        }
        catch(Exception ex)
        {
            MessageBox.Show(ex.Message);
            actionMsg(string.Format("{0} Error : {1}", DateTime.Now.ToString(), ex.Message));
        }
    }
 
    //다운로드 중간중간에
    void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
        int value = Convert.ToInt32((Convert.ToDouble(e.BytesReceived) / Convert.ToDouble(_fileSize)) * 100);
        actionProgress(value);       
    }
}
}
