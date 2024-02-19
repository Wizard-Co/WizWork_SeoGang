using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WizWork.Tools
{
    public class INI_GS//INI GET,WRITE 클래스
    {
        string sFileName = ConnectionInfo.filePath;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        //섹션명[]. 키값,
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        public String GetValue(String section, String key, String default_String)
        {
            StringBuilder temp = new StringBuilder();
            temp.Capacity = 1024;
            if (GetPrivateProfileString(section, key, "(NONE)", temp, 1024, sFileName) == 0)
            {
                if (default_String == null) //디폴트값없을때
                { return ""; }
                else
                { return default_String; }
            }
            else { return temp.ToString(); }
        }
        public void SetValue(string section, string key, string sValue, string sFileName)
        {
            WritePrivateProfileString(section, key, sValue, sFileName);//섹션,키,값,INI파일경로
        }
    }
}