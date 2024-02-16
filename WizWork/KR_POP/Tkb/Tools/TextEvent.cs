using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWork.Tools
{
    //이벤트 함수의 형식 설정
    //public delegate void TextSenderHandler(object sender, TextEvent e);

    public delegate void TextEventHandler(object sender, TextEvent e);

    public class CurrentData
    {
        public string sProcessID ;
        public string sInstID ;
        public string sInstQty;
        public string sLotID ;
        public string sOrderID;
        //public string s

        public CurrentData(string ProcessID, string InstID, string InstQty, string LotID)
        {
            sProcessID = ProcessID;
            sInstID = InstID;
            sInstQty = InstQty;
            sLotID = LotID;
        }
    }
}
