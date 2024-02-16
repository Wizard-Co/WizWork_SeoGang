using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//*******************************************************************************
//프로그램명    Ins_InspectAuto.cs
//메뉴ID        
//설명          Ins_InspectAuto Table Field Names
//작성일        2016.11.16
//개발자        최경열
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************
//
//
//*******************************************************************************

namespace WizWork
{
    class Work_sWorkDefect
    {
        /*
         * PARAMETER SETTING
         */
        public const string LOTNO = "LotNo";
        public const string PROCESSID = "ProcessID";


        /*
         * 
         */


        public const string INSTID = "InstID";                  //지시번호
        public const string ARTICLE = "Article";                //품명
        public const string DEFECTID = "DefectID";              //불량명
        public const string DEFECTQTY = "DefectQty";            //불량수량
        public const string INSPECTDATE = "InspectDate";        //검사날짜
        public const string PERSONID = "PersonID";              //검사자
        //public const string PROCESSID = "ProcessID";            //공정
        public const string MACHINEID = "MachineID";            //설비 
       
    }
}
