using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Net;
using Microsoft.Win32;

namespace KR_POP.Common
{
	public class FontInstall
	{
		const int WM_FONTCHANGE = 0x001D; 
		const int HWND_BROADCAST = 0xffff;


		public static void InstallFont(string fontPath, string fontName, string fontFileName)
		{
			int Ret; 
			int Res;

			string fontFilePathFull = fontPath + fontFileName;

			Ret = FontInstall.AddFontResource(fontFilePathFull); 
			Res = FontInstall.SendMessage(HWND_BROADCAST, WM_FONTCHANGE, 0, 0);
			Ret = FontInstall.WriteProfileString("fonts", fontName + " (TrueType)", fontFileName); 
		}




		[DllImport("kernel32.dll", SetLastError=true)]
		static extern int WriteProfileString( string lpszSection, string lpszKeyName, string lpszString) ; 

		[DllImport("user32.dll")] 
		public static extern int SendMessage(int hWnd, // handle to destination window 
		uint Msg, // message 
		int wParam, // first message parameter 
		int lParam // second message parameter 
		); 

		[DllImport("gdi32")] 
		public static extern int AddFontResource(string lpFileName); 
	}
}
