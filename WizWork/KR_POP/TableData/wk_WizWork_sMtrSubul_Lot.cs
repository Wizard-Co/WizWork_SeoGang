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
    class WizWork_sMtrSubul_Lot
    {
        public const string NCHKDATE = "nChkDate";
        public const string SSDATE = "sSDate";

        public const string SEDATE = "SEDate";                                                  //지시번호
        public const string NCHKARTICLEID = "nChkArticleID";                                          //지시번호SEQ
        public const string SARTICLEID = "sArticleID";                                            //품명코드 = 재종코드
        public const string NCHKPARENTARTICLEID = "nChkParentArticleID";                                            //품명코드 = 재종코드
        public const string SPARENTARTICLEID = "sParentArticleID";                                                //품명 = 재종          //GP기호 조회때도 사용
        public const string NCHKCUSTOM = "nChkCustom";                                            //품명규격=재종규격
        public const string SCUSTOMID = "sCustomID";                                            //품명규격=재종규격
        public const string INCNOTAPPROVALYN = "incNotApprovalYN";                                                //수주번호
        public const string INCAUTOINOUTYN = "incAutoInOutYN";                                                //수주번호
        public const string NMAINITEM = "nMainItem";                                              //발주업체코드
        public const string NCUSTOMITEM = "nCustomItem";                                                //발주업체



        public const string CLS = "cls";                                                  //용도
        public const string ARTICLEID = "ArticleID";                                                  //구분
        public const string ARTICLE = "Article";                                                  //TOTAL C
        public const string BUYERARTICLENO = "BuyerArticleNo";                                                  //설비ID
        public const string LOTID = "lotID";                                                  //설비NO
        public const string IODATE = "IODate";                                                  //지시커멘트
        /// //////////////////성형정보
        public const string STUFFROLL = "StuffRoll";                                                  //성형금형
        public const string STUFFQTY = "StuffQty";                                              //금형규격
        public const string UNITCLSS = "UnitClss";                                            //수축률
        public const string UNITCLSSNAME = "UnitClssName";                                          //성형높이

        public const string OUTROLL = "OutRoll";                                          //성형중량
        public const string OUTQTY = "OutQty";                                                //성형수량
        /// ///////////제품규격기본(원형의경우)
        public const string STUFFREALQTY = "StuffRealQty";                  //T/OD 

        public const string UNITCLSSREAL = "UnitClssReal";                  //W/ID
        public const string UNITCLSSNAMEREAL = "UnitClssNameReal";                                            //L
        public const string STOCKQTY = "StockQty";
        public const string SPRODLOTID = "sProdLotID";
        public const string PROCESSID = "ProcessID";  
        //L   
    }
}
