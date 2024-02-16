using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public class InsView
    {
        public const int MOVE_UP = 0;
        public const int MOVE_DN = 1;
        public const int ID_ADDNEW = 0;
        public const int ID_UPDATE = 1;
        public const int ID_DELETE = 2;
        public const int ID_SAVE = 3;
        public const int ID_CANCEL = 4;
        
        public enum EMakeType { MT_EXPAND = 0, MT_reduce = 1 }

        public enum EInspectType { MT_NORMAL = 0, MT_DEFECT = 1 }

        public enum EBasisID { MT_10POINT = 1, MT_4POINT = 2, MT_COUNT = 3, MT_10P_COUNT = 4, MT_4P_COUNT = 5 }

        public enum EUnit { MT_YARD = 0, MT_METER = 1, MT_SQY = 2, MT_SQM = 3 }

        public enum EEdit { MT_ADDNEW = 0, MT_UPDATE = 1 }

        public enum ESearch { MT_WORKING = 0, MT_SEARCH = 1 }

        public enum EGridLoad { MT_LOADING = 0, MT_COMPLETE = 1 }

        public enum EWorkState { MT_FIRST = 0, MT_CONTINUE = 1 }

        public enum EDefectSelect { MT_NOTSELECT = 0, MT_SELECT = 1 }//MT_NOTSELECT = False, MT_SELECT = True

        #region TInsepct 클래스
        public class TInspect
        {
            public string OrderID { get; set; } //' 관리 번호
            public string RollSeq { get; set; } //' 일련 순위
            public string OrderSeq { get; set; }//' 색상 순위
            public string RollNo { get; set; }//'절번호
            public string ExamNo { get; set; }//' 검사 호기
            public string ExamDate { get; set; }//' 검사 일자
            public string ExamTime { get; set; }//' 검사 시간
            public string TeamID { get; set; }//' 검사 조
            public string PersonID { get; set; }//' 검사자 코드
            public string StuffQty { get; set; }//' 투입원단 수량
            public string RealQty { get; set; } //' 실제검사 수량
            public string CtrlQty { get; set; } //' 조정검사 수량
            public string SampleQty { get; set; }//' 견본 수량
            public string LossQty { get; set; } //' 보상 수량
            public string CutQty { get; set; }//' 난단 수량
            public string UnitClss { get; set; }//' 검사 단위
            public string StuffWeight { get; set; }//' 원단 중량
            public string StuffWeightUnit { get; set; }//'단위당 중량
            public string Density { get; set; } //' 원단 밀도
            public string StuffWidth { get; set; }//' 원단검사 폭
            public string GradeID { get; set; } //' 등급
            public string LotNo { get; set; }//' QC Lot
            public string DefectID { get; set; }//' 대표불량 코드
            public string Defect { get; set; }//' 대표불량 명
            public string DefectClss { get; set; }//' 불량 구분
            public string CutDefectID { get; set; }//' 난단 대표불량 코드
            public string CutDefectClss { get; set; }//'난단 불량 구분
            public string DefectQty { get; set; }//' 불량 갯수
            public string DefectPoint { get; set; }//' 불량 점수
            public string CardID { get; set; }//' 카드번호
            public string SplitID { get; set; }//' 카드 분할 번호
            public string ReworkClss { get; set; }//'재작업 여부   
            public string BoxID { get; set; }//' 박스ID
            public string InstID { get; set; }//' InstID
            public string CreateUserID { get; set; }//
            public string DivideQty { get; set; }//'분할수량
            public string DefectSeq { get; set; }//'YS (InspectSub의 DefectSeq)
            public string CardIDList { get; set; }//' CardIDList, 2016.08.22 추가

            public void TInsepct()
            {
                OrderID = "";
                RollSeq = "";
                OrderSeq = "";
                RollNo = "";
                ExamNo = "";
                ExamDate = "";
                ExamTime = "";
                TeamID = "";
                PersonID = "";
                StuffQty = "";
                RealQty = "";
                CtrlQty = "";
                SampleQty = "";
                LossQty = "";
                CutQty = "";
                UnitClss = "";
                StuffWeight = "";
                StuffWeightUnit = "";
                Density = "";
                StuffWidth = "";
                GradeID = "";
                LotNo = "";
                DefectID = "";
                Defect = "";
                DefectClss = "";
                CutDefectID = "";
                CutDefectClss = "";
                DefectQty = "";
                DefectPoint = "";
                CardID = "";
                SplitID = "";
                ReworkClss = "";
                BoxID = "";
                InstID = "";
                CreateUserID = "";
                DivideQty = "";
                DefectSeq = "";
                CardIDList = "";

            }
        }
        #endregion

        #region TInsepctSub 클래스
        public class TInspectSub
        {
            public string OrderID { get; set; }            //' 관리 번호
            public string RollSeq { get; set; }            //' 일련 순위
            public string DefectSeq { get; set; }          //' 불량 순위
            public string DefectID { get; set; }           //' 불량 코드
            public string KDefect { get; set; }            //' 불량명 한글
            public string EDefect { get; set; }            //' 불량명 영문
            public string TagName { get; set; }            //' Tag Name
            public string YPos { get; set; }               //' 수직위치
            public string Demerit { get; set; }            //' 감점
            public string ButtonSeq { get; set; }          //' 버튼 위치
            public string BonusQty { get; set; }           //' 보상수량(신원의 경우(Loss사용함))
            public string nDefectQty { get; set; }         //'defect 수량

            public void TInsepctSub()
            {
                OrderID = "";
                RollSeq = "";
                DefectSeq = "";
                DefectID = "";
                KDefect = "";
                EDefect = "";
                TagName = "";
                YPos = "";
                Demerit = "";
                ButtonSeq = "";
                BonusQty = "";
                nDefectQty = "";
            }
        }
        #endregion

        #region TDefect 클래스
        public class TDefect
        {
            public string DefectID { get; set; }         //' 불량 코드
            public string Display { get; set; }         //' 단말기 불량명
            public string KDefect { get; set; }         //' 불량명 한글
            public string EDefect { get; set; }         //' 불량명 영문
            public string TagName { get; set; }         //' TagName
            public string DefectClss { get; set; }         //' 불량 구분
            public string DefectCnt { get; set; }         //' 불량 갯수
            public string ButtonSeq { get; set; }         //' 버튼 위치
            public string Demerit { get; set; }         //' 결점  '2011.12.08 추가
            public string Loss { get; set; }         //' Loss  'S_201201_조일_07 에 의한 추가
            public string nPos { get; set; }                  //' 버튼에 매칭되는 defect 클래스를 찾기위한 값 18.05.08 추가

            public TDefect()
            {
                DefectID = "";
                Display = "";
                KDefect = "";
                EDefect = "";
                TagName = "";
                DefectClss = "";
                DefectCnt = "";
                ButtonSeq = "";
                Demerit = "";
                Loss = "";
            }
        }
        #endregion

        #region TLogData 클래스
        public class TLogData
        {
            public string bLog { get; set; }        //' 로그기록여부(true인 경우만 로그 기록)
            public string sWorkModule { get; set; }        //' 모듈구분(MrpPlus,WizINS)
            public string sWorkFormName { get; set; }        //' 작업폼이름
            public string sWorkFlag { get; set; }        //' 작업구분(U:수정,D:삭제)
            public string sWorkDate { get; set; }        //' 작업일자
            public string sWorkTime { get; set; }        //' 작업시간
            public string sWorkPersonid { get; set; }        //' 작업자
            public string sWorkComputer { get; set; }        //' 작업컴퓨터명
            public string sWorkComputerIP { get; set; }        //' 작업IP
            public string sWorkExNo { get; set; }                 //' 작업호기
            public string sWorkLog { get; set; }                  //' 작업 로그
            public TLogData()
            {
                bLog = "";
                sWorkModule = "";
                sWorkFormName = "";
                sWorkFlag = "";
                sWorkDate = "";
                sWorkTime = "";
                sWorkPersonid = "";
                sWorkComputer = "";
                sWorkComputerIP = "";
                sWorkExNo = "";
                sWorkLog = "";
            }
        }
        #endregion

        #region TBoxTransfer 클래스
        public class TBoxTransfer
        {
            public string BoxID { get; set; }        //' BoxID
            public string GetBoxID { get; set; }        //' 가져올 BoxID
            public string DivideBoxID { get; set; }        //' 나눌 BoxID
            public string nBeforeGetBoxQty { get; set; }        //' 처리전 이전Box의 장입량
            public string nBeforeDivideBoxQty { get; set; }        //' 처리전 이후Box의 장입량
            public string nTransferQty { get; set; }        //' 이전수량

            public TBoxTransfer()
            {
                BoxID = "";
                GetBoxID = "";
                DivideBoxID = "";
                nBeforeGetBoxQty = "";
                nBeforeDivideBoxQty = "";
                nTransferQty = "";
            }
        }
        #endregion

        #region TinsInspectAuto 클래스
        public class TinsInspectAuto
        {
            public string InspectID { get; set; }//
            public string ArticleID { get; set; }    //
            public string InspectGubun { get; set; }    //        '--검사 구분  1:전수검사, 2:샘플검사, 3:일반검사
            public string InspectDate { get; set; }    //        '입고일자
            public string LotID { get; set; }    //        'lot ID
            public string InspectQty { get; set; }    //        '검사수량
            public string ECONo { get; set; }    //        'ECO 번호
            public string Comments { get; set; }    //
            public string InspectLevel { get; set; }    //        '검사수준
            public string SketchPath { get; set; }    //        '약도파일 경로
            public string SketchFile { get; set; }    //        '약도파일 명
            public string AttachedPath { get; set; }    //        '첨부화일 경로
            public string AttachedFile { get; set; }    //        '첨부화일 명
            public string InspectUserID { get; set; }    //        '담당자
            public string CreateUserID { get; set; }    //
            public string InspectBasisID { get; set; }    //
            public string InspectBasisIDSeq { get; set; }    //

            public string DefectYN { get; set; }    //        ' 판정결과
            public string ProcessID { get; set; }    //        ' 공정

            public string InspectPoint { get; set; }    //        ' 1: 수입, 3:자주,5:출하
            public string ImportSecYN { get; set; }    //        ' 보안 부품중요도
            public string ImportlawYN { get; set; }    //        ' 법규
            public string ImportImpYN { get; set; }    //        ' 중요
            public string ImportNorYN { get; set; }    //        ' 일반
            public string IRELevel { get; set; }    //        '부품품질 위험도 -- 특별관리 고, 중요관리 중, 일반관
            public string InpCustomID { get; set; }    //        ' 입고거래처
            public string InpDate { get; set; }    //        ' 입고일
            public string OutCustomID { get; set; }    //        ' 출고거래처
            public string OutDate { get; set; }    //        ' 출고일
            public string MachineID { get; set; }    //        ' 설비 호기
            public string BuyerModelID { get; set; }    //        '  고객 모델
            public string FMLGubun { get; set; }    //        '  초.중.종품 구분     , S_201508_AFT_06   자주 검사
            public string BoxID { get; set; }    //
            public string RefKey { get; set; }    //        'YS 기준 InspectSub key
            public string RefLotID { get; set; }    //
            public TinsInspectAuto()
            {
                InspectID = "";
                ArticleID = "";
                InspectGubun = "";
                InspectDate = "";
                LotID = "";

                InspectQty = "";
                ECONo = "";
                Comments = "";
                InspectLevel = "";
                SketchPath = "";

                SketchFile = "";
                AttachedPath = "";
                AttachedFile = "";
                InspectUserID = "";
                CreateUserID = "";

                InspectBasisID = "";
                InspectBasisIDSeq = "";

                DefectYN = "";
                ProcessID = "";

                InspectPoint = "";
                ImportSecYN = "";
                ImportlawYN = "";
                ImportImpYN = "";
                ImportNorYN = "";
                IRELevel = "";
                InpCustomID = "";
                InpDate = "";
                OutCustomID = "";
                OutDate = "";
                MachineID = "";
                BuyerModelID = "";
                FMLGubun = "";
                BoxID = "";
                RefKey = "";
                RefLotID = "";

            }
        }
        #endregion

        #region TinsInspectAutoSub 클래스
        public class TinsInspectAutoSub
        {
            public string InspectID { get; set; } 
            public string Seq                 {get;set;}
            public string InspectBasisID      {get;set;}
            public string InspectBasisSeq     {get;set;}
            public string InspectBasisSubSeq  {get;set;}
            public string InspectValue        {get;set;}
            public string InspectText         {get;set;}
            public string RefKey              {get;set;}
            public string CreateUserID        { get; set; }
            
            public TinsInspectAutoSub()
            {
                InspectID          = "";
                Seq                = "";
                InspectBasisID     = "";
                InspectBasisSeq    = "";
                InspectBasisSubSeq = "";
                InspectValue       = "";
                InspectText        = "";
                RefKey             = "";
                CreateUserID       = "";
            }
        }
        #endregion

        #region TinsInspectAutoSubTemp 클래스
        public class TinsInspectAutoSubTemp
        {
            public string InspectTempID { get; set; }
            public string InspectValue  {get;set;} 
            public string InspectID     {get;set;} 
            public string Seq           {get;set;} 
            public string CreateUserID  { get;set;}

            public TinsInspectAutoSubTemp()
            {
                InspectTempID = "";
                InspectValue = "";
                InspectID = "";
                Seq = "";
                CreateUserID = "";
            }
        }
        #endregion

        #region Sub_TWkLabelPrint 클래스
        public class TWkLabelPrint
        {
            private int _nInstDetSeq = 0;
            private long _nPrintProductQty = 0;
            private int _nPrintQty = 0;
            private long _nQtyPerBox = 0;
            private int _nReprintQty = 0;

            private string _nRestYN = "";
            private string _sArticleID = "";
            private string _sCardID = "";
            private string _sCreateDate = "";
            private string _sCreateuserID = "";

            private string _sCustomID = "";
            private string _sInstID = "";
            private string _sLabelGubun = "";
            private string _sLabelID = "";
            private string _sLastProdArticleID = "";

            private string _sLastUpdateDate = "";
            private string _sLastUpdateUserID = "";
            private string _sLotID = "";
            private string _sOrderID = "";
            private string _sPrintDate = "";

            private string _sProcessID = "";
            private string _sReprintDate = "";
            private string _sWoker = "";


            public int nInstDetSeq
            {
                get { return _nInstDetSeq; }
                set { _nInstDetSeq = value; }
            }

            public long nPrintProductQty
            {
                get { return _nPrintProductQty; }
                set { _nPrintProductQty = value; }
            }

            public int nPrintQty
            {
                get { return _nPrintQty; }
                set { _nPrintQty = value; }
            }

            public long nQtyPerBox
            {
                get { return _nQtyPerBox; }
                set { _nQtyPerBox = value; }
            }

            public int nReprintQty
            {
                get { return _nReprintQty; }
                set { _nReprintQty = value; }
            }

            public string nRestYN
            {
                get { return _nRestYN; }
                set { _nRestYN = value; }
            }

            public string sArticleID
            {
                get { return _sArticleID; }
                set { _sArticleID = value; }
            }

            public string sCardID
            {
                get { return _sCardID; }
                set { _sCardID = value; }
            }

            public string sCreateDate
            {
                get { return _sCreateDate; }
                set { _sCreateDate = value; }
            }

            public string sCreateuserID
            {
                get { return _sCreateuserID; }
                set { _sCreateuserID = value; }
            }

            public string sCustomID
            {
                get { return _sCustomID; }
                set { _sCustomID = value; }
            }

            public string sInstID
            {
                get { return _sInstID; }
                set { _sInstID = value; }
            }

            public string sLabelGubun
            {
                get { return _sLabelGubun; }
                set { _sLabelGubun = value; }
            }

            public string sLabelID
            {
                get { return _sLabelID; }
                set { _sLabelID = value; }
            }

            public string sLastProdArticleID
            {
                get { return _sLastProdArticleID; }
                set { _sLastProdArticleID = value; }
            }

            public string sLastUpdateDate
            {
                get { return _sLastUpdateDate; }
                set { _sLastUpdateDate = value; }
            }

            public string sLastUpdateUserID
            {
                get { return _sLastUpdateUserID; }
                set { _sLastUpdateUserID = value; }
            }

            public string sLotID
            {
                get { return _sLotID; }
                set { _sLotID = value; }
            }

            public string sOrderID
            {
                get { return _sOrderID; }
                set { _sOrderID = value; }
            }

            public string sPrintDate
            {
                get { return _sPrintDate; }
                set { _sPrintDate = value; }
            }

            public string sProcessID
            {
                get { return _sProcessID; }
                set { _sProcessID = value; }
            }

            public string sReprintDate
            {
                get { return _sReprintDate; }
                set { _sReprintDate = value; }
            }

            public string sWoker
            {
                get { return _sWoker; }
                set { _sWoker = value; }
            }

            public TWkLabelPrint()
            {
                this.nInstDetSeq = 0;
                this.nPrintProductQty = 0;
                this.nPrintQty = 0;
                this.nQtyPerBox = 0;
                this.nReprintQty = 0;

                this.nRestYN = "";
                this.sArticleID = "";
                this.sCardID = "";
                this.sCreateDate = "";
                this.sCreateuserID = "";

                this.sCustomID = "";
                this.sInstID = "";
                this.sLabelGubun = "";
                this.sLabelID = "";
                this.sLastProdArticleID = "";

                this.sLastUpdateDate = "";
                this.sLastUpdateUserID = "";
                this.sLotID = "";
                this.sOrderID = "";
                this.sPrintDate = "";

                this.sProcessID = "";
                this.sReprintDate = "";
                this.sWoker = "";
            }
            public void DataClear()
            {
                this.nInstDetSeq = 0;
                this.nPrintProductQty = 0;
                this.nPrintQty = 0;
                this.nQtyPerBox = 0;
                this.nReprintQty = 0;

                this.nRestYN = "";
                this.sArticleID = "";
                this.sCardID = "";
                this.sCreateDate = "";
                this.sCreateuserID = "";

                this.sCustomID = "";
                this.sInstID = "";
                this.sLabelGubun = "";
                this.sLabelID = "";
                this.sLastProdArticleID = "";

                this.sLastUpdateDate = "";
                this.sLastUpdateUserID = "";
                this.sLotID = "";
                this.sOrderID = "";
                this.sPrintDate = "";

                this.sProcessID = "";
                this.sReprintDate = "";
                this.sWoker = "";

                return;
            }
            public TWkLabelPrint(int intnInstDetSeq, long longnPrintProductQty, int intnPrintQty, long longnQtyPerBox, int intnReprintQty,
                                 string strnRestYN, string strsArticleID, string strsCardID, string strsCreateDate, string strsCreateuserID,
                                 string strsCustomID, string strsInstID, string strsLabelGubun, string strsLabelID, string strsLastProdArticleID,
                                 string strsLastUpdateDate, string strsLastUpdateUserID, string strsLotID, string strsOrderID, string strsPrintDate,
                                 string strssProcessID, string strsReprintDate, string strsWoker)
            {
                this.nInstDetSeq = intnInstDetSeq;
                this.nPrintProductQty = longnPrintProductQty;
                this.nPrintQty = intnPrintQty;
                this.nQtyPerBox = longnQtyPerBox;
                this.nReprintQty = intnReprintQty;

                this.nRestYN = strnRestYN;
                this.sArticleID = strsArticleID;
                this.sCardID = strsCardID;
                this.sCreateDate = strsCreateDate;
                this.sCreateuserID = strsCreateuserID;

                this.sCustomID = strsCustomID;
                this.sInstID = strsInstID;
                this.sLabelGubun = strsLabelGubun;
                this.sLabelID = strsLabelID;
                this.sLastProdArticleID = strsLastProdArticleID;

                this.sLastUpdateDate = strsLastUpdateDate;
                this.sLastUpdateUserID = strsLastUpdateUserID;
                this.sLotID = strsLotID;
                this.sOrderID = strsOrderID;
                this.sPrintDate = strsPrintDate;

                this.sProcessID = strssProcessID;
                this.sReprintDate = strsReprintDate;
                this.sWoker = strsWoker;
            }
        }
        #endregion
    }
}
