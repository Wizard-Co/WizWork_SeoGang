using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WizWork.Lib
{
    public class MES_WTS_Lib
    {


        public MES_WTS_Lib()
        {
             
            //생성자
        }


        /// <summary>
        ///  숫자체크 함수
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsNumeric(string value)
        {

            value = value.Replace("-", "");

            if (value == "")
            {
                return true;
            }

            foreach (char _char in value)
            {
                if (_char != '.')
                {
                    if (!Char.IsNumber(_char))
                        return false;
                }
            }
            return true;
        }

        public bool GetCheckLoID(string strLotID)
        {
            bool blResult = false;

            if (strLotID.Length == 15 || strLotID.Length == 16)
            {
                blResult = true;
            }
            else
            {
                blResult = false;
            }

            return blResult;
        }

        public bool GetCheckstrOrderID(string strOrderID)
        {
            bool blResult = false;

            if (strOrderID.Length == 10)
            {
                blResult = true;
            }
            else
            {
                blResult = false;
            }

            return blResult;
        }

    }
}
