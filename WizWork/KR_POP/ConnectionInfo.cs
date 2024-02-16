using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWork
{
	class ConnectionInfo
	{
        //public const string POPServerIPAddress = "192.168.0.7,20140";
        public const string POPServerIPAddress = "wizis.iptime.org,20140";
        //public const string POPServerIPAddress = "HKSERVER,20140";
        
        //public const string POPServerIPAddress = "wizis.iptime.org,20080";
        public const string POPServerLoginID = "dbuser";
        public const string POPServerPassword = "Wizardis";

        public const string DBCatalog_MES = ";Initial Catalog=MES_HIT;UID=";
        //public const string DBCatalog_MES = ";Initial Catalog=MES_DHS;UID=";
        public const string DBCatalog_LOG = ";Initial Catalog=WizLog;UID=";
        public const string filePath = "C:\\Windows\\wizard.ini";
	}
}



