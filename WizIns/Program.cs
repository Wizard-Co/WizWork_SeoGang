using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.Threading;
using System.Reflection;
using WizCommon;
using WizWork;

namespace WizIns
{
    class Program
    {
        private static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            Assembly thisAssembly = Assembly.GetExecutingAssembly();

            var name = args.Name.Substring(0, args.Name.IndexOf(',')) + ".ocx";

            var resources = thisAssembly.GetManifestResourceNames().Where(s => s.EndsWith(name));

            if (resources.Count() > 0)

            {

                string resourceName = resources.First();

                using (Stream stream = thisAssembly.GetManifestResourceStream(resourceName))

                {

                    if (stream != null)

                    {

                        byte[] assembly = new byte[stream.Length];

                        stream.Read(assembly, 0, assembly.Length);

                        Console.WriteLine("Dll file load : " + resourceName);

                        return Assembly.Load(assembly);

                    }

                }

            }

            return null;
        }
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 컴파일 배포시 단이 exe 로 배포하고 , 참조하는 dll은 리소스 파일에 추가하여 (참조리소스)
            // 단일 실행파일로 만들어 배포함.
            // 프로그램은 실행시 참조 dll 파일을 생성하고 리소스 파일에 있는 원 파일을 읽어서 대체함.

            //string strOutlookPath = System.Windows.Forms.Application.ExecutablePath.Replace("/", "\\");
            //int intPos = strOutlookPath.LastIndexOf("\\");
            //if (intPos >= 1) strOutlookPath = strOutlookPath.Substring(0, intPos).Trim('\\');
            //strOutlookPath += "\\AxInterop_MSACAL.DLL";// *Microsoft.Office.Interop.Outlook.dll*";

            //FileInfo fileinfo = new FileInfo(strOutlookPath);
            //if (fileinfo.Exists == false)
            //{
            //    byte[] aryData = WizWork.Properties.Resources.AxInterop_MSACAL; //리소드 디자인에 저장한 이름
            //    FileStream fileStream = new FileStream(fileinfo.FullName, FileMode.CreateNew);
            //    fileStream.Write(aryData, 0, aryData.Length);
            //    fileStream.Close();
            //}
            //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(ResolveAssembly);
            //뮤텍스명 지정
            string mtxName = "WizardInformationSystem-WizIns-AFT";
            Mutex mtx = new Mutex(true, mtxName);
            //1초 동안 뮤텍스를 획득하려 대기
            TimeSpan tsWait = new TimeSpan(0, 0, 1);
            bool success = mtx.WaitOne(tsWait);
            //실패하면 프로그램 종료
            if (!success)
            {
                return;
            }
            //AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(ResolveAssembly);
            ApplicationStart();
        }

        private static void ApplicationStart()
        {
            try
            {
                WizWork.Tools.INI_GS gs = new WizWork.Tools.INI_GS();
                string POPServerIPAddress = Frm_tprc_Main.gs.GetValue("SQLServer", "server", "wizis.iptime.org,20140");
                string DBCatalog_MES = ";Initial Catalog= " + Frm_tprc_Main.gs.GetValue("SQLServer", "Database", "WizMRP") + ";UID=";

                DataStore.Instance.SetConnectionString(POPServerIPAddress, ConnectionInfo.POPServerLoginID, ConnectionInfo.POPServerPassword, DBCatalog_MES);//, ConnectionInfo.DBCatalog_LOG);
                DataStore.Log_Instance.L_SetConnectionString(POPServerIPAddress, ConnectionInfo.POPServerLoginID, ConnectionInfo.POPServerPassword, ConnectionInfo.DBCatalog_LOG);

                Application.EnableVisualStyles();

                Application.SetCompatibleTextRenderingDefault(false);

                Application.Run(new WizIns.Frm_tins_Main()); // Frm_tprc_Main // Frm_tins_Main // Frm_mon_Main
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }
    }
}
