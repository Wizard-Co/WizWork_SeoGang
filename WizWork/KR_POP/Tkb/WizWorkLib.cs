using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;
using WizWork.Tools;
using System.Printing;
using System.Drawing.Printing;
using System.Text.RegularExpressions;

namespace WizWork
{

    #region TBaseSpec 클래스
    public class TBaseSpec
    {
        private string _OrderID = "";            // ' 관리번호
        private string _OrderNO = "";            // ' Order NO
        private string _Custom = "";            // ' 거래처 명
        private int _OrderQty = 0;             // ' 수주수량
        private string _OrderUnit = "";            // ' 수주 단위
        private int _OrderSeq = 0;             // ' 오더 SEQ
        private string _Article = "";             // ' Article
        //'-------------------------------------------
        private string _ProcessID = "";            // ' 공정ID
        private string _Process = "";              // ' 공정명
        private string _ProcessCode = "";         // ' 공정 코드
        private int _PlanProcSeq = 0;            // ' 가공순위
        private string _ProcessChildCheckYN = ""; // ' 하위 Check 여부
        private string _AutoGatheringYN = "";      // ' 자동 통신데이타 수집건 인지 확인
        //'-------------------------------------------
        private string _BoxID = "";                // ' Box 번호
        private string _CardID = "";              // ' Card 번호
        private string _SplitID = "";              // ' 카드분할번호
        private int _WorkSeq = 0;              // ' 가공순위
        private string _MachineID = "";            // ' 기계호기
        private string _Machine = "";            // ' 기계
        //'-------------------------------------------
        private string _ColorID = "";              // ' 색상 코드
        private string _Color = "";                // ' 색상
        private int _ColorQty = 0;              // ' 색상 수량
        //'-------------------------------------------
        private string _TeamID = "";             // ' 작업조 코드
        private string _Team = "";             // ' 작업조
        private string _PersonID = "";             // ' 작업자 코드
        private string _Person = "";             // ' 작업자
        private string _WorkNO = "";             // ' 공정 호기
        private string _TagID = "";             // ' 선택태그ID (001, 002, ...)
        private string _DayOrNightID = "";        //  ' 작업자 (주/야 근무자 설정) (작업조가 그 역할을 못할때) 
        private string _DayOrNight = "";        //  ' 작업자 (주/야 근무자 설정) (작업조가 그 역할을 못할때) 

        //'-------------------------------------------
        //' 생산시점 불량 check
        //'-------------------------------------------
        private int _DefectCnt = 0;            // ' 생산중 불량 수량
        private int _DLoss = 0;            // ' 불량 길이
        private int _WorkQty = 0;            //  수량
        private string _Basis = "";           // ' 검사기준 명
        private int _BasisID = 0;            // ' 검사기준 ID
        private string _QcLot = "";           //

        //'------------------------------------------
        //'금형등록
        //'------------------------------------------
        private string _sMoldID = "";            // '금형ID,  2016.03.25, 성광 작업시 추가 함
        private string _sMold = "";            // '금형명,  2016.03.25, 성광 작업시 추가 함
        private string _sInstID = "";            // '작업지시ID, 2016.05.02, 성광 작업시 추가 함
        private string _sLotID = "";            // 'LotID , 2016.05.4, 성광 작업시 추가함
        private string _sInstDetSeq = "";
        private string _sPLotID = "";


        private string _tempDate = ""; // 날짜 넘겨주려고 만든 임시 변수

        public string tempDate
        {
            get { return _tempDate; }
            set { _tempDate = value; }
        }

        public string OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        public string OrderNO
        {
            get { return _OrderNO; }
            set { _OrderNO = value; }
        }

        public string Custom
        {
            get { return _Custom; }
            set { _Custom = value; }
        }
        public int OrderQty
        {
            get { return _OrderQty; }
            set { _OrderQty = value; }
        }
        public string OrderUnit
        {
            get { return _OrderUnit; }
            set { _OrderUnit = value; }
        }
        public int OrderSeq
        {
            get { return _OrderSeq; }
            set { _OrderSeq = value; }
        }

        public string Article
        {
            get { return _Article; }
            set { _Article = value; }
        }

        public string ProcessID
        {
            get { return _ProcessID; }
            set { _ProcessID = value; }
        }
        public string Process
        {
            get { return _Process; }
            set { _Process = value; }
        }

        public string ProcessCode
        {
            get { return _ProcessCode; }
            set { _ProcessCode = value; }
        }

        public int PlanProcSeq
        {
            get { return _PlanProcSeq; }
            set { _PlanProcSeq = value; }
        }

        public string ProcessChildCheckYN
        {
            get { return _ProcessChildCheckYN; }
            set { _ProcessChildCheckYN = value; }
        }

        public string AutoGatheringYN
        {
            get { return _AutoGatheringYN; }
            set { _AutoGatheringYN = value; }
        }

        public string BoxID
        {
            get { return _BoxID; }
            set { _BoxID = value; }
        }
        public string CardID
        {
            get { return _CardID; }
            set { _CardID = value; }
        }
        public string SplitID
        {
            get { return _SplitID; }
            set { _SplitID = value; }
        }

        public int WorkSeq
        {
            get { return _WorkSeq; }
            set { _WorkSeq = value; }
        }

        public string MachineID
        {
            get { return _MachineID; }
            set { _MachineID = value; }
        }
        public string ColorID
        {
            get { return _ColorID; }
            set { _ColorID = value; }
        }
        public string Color
        {
            get { return _Color; }
            set { _Color = value; }
        }

        public string sArticleID { get; set; }
        public string sArticle { get; set; }

        public int ColorQty
        {
            get { return _ColorQty; }
            set { _ColorQty = value; }
        }

        public string TeamID
        {
            get { return _TeamID; }
            set { _TeamID = value; }
        }

        public string Team
        {
            get { return _Team; }
            set { _Team = value; }
        }
        public string PersonID
        {
            get { return _PersonID; }
            set { _PersonID = value; }
        }

        public string Person
        {
            get { return _Person; }
            set { _Person = value; }
        }
        public string WorkNO
        {
            get { return _WorkNO; }
            set { _WorkNO = value; }
        }
        public string TagID
        {
            get { return _TagID; }
            set { _TagID = value; }
        }
        public string DayOrNightID
        {
            get { return _DayOrNightID; }
            set { _DayOrNightID = value; }
        }
        public string DayOrNight
        {
            get { return _DayOrNight; }
            set { _DayOrNight = value; }
        }


        public int DefectCnt
        {
            get { return _DefectCnt; }
            set { _DefectCnt = value; }
        }

        public int DLoss
        {
            get { return _DLoss; }
            set { _DLoss = value; }
        }


        public int WorkQty
        {
            get { return _WorkQty; }
            set { _WorkQty = value; }
        }

        public string Basis
        {
            get { return _Basis; }
            set { _Basis = value; }
        }
        public int BasisID
        {
            get { return _BasisID; }
            set { _BasisID = value; }
        }
        public string QcLot
        {
            get { return _QcLot; }
            set { _QcLot = value; }
        }

        public string sMoldID
        {
            get { return _sMoldID; }
            set { _sMoldID = value; }
        }
        public string sMold
        {
            get { return _sMold; }
            set { _sMold = value; }
        }
        public string sInstID
        {
            get { return _sInstID; }
            set { _sInstID = value; }
        }
        public string sLotID
        {
            get { return _sLotID; }
            set { _sLotID = value; }
        }
        public string sPLotID
        {
            get { return _sPLotID; }
            set { _sPLotID = value; }
        }

        public string sInstDetSeq
        {
            get { return _sInstDetSeq; }
            set { _sInstDetSeq = value; }
        }
        public string Machine
        {
            get { return _Machine; }
            set { _Machine = value; }
        }


        public TBaseSpec()
        {
            OrderID = "";            // ' 관리번호
            OrderNO = "";            // ' Order NO
            Custom = "";            // ' 거래처 명
            OrderQty = 0;             // ' 수주수량
            OrderUnit = "";            // ' 수주 단위
            OrderSeq = 0;             // ' 오더 SEQ
            Article = "";             // ' Article
            //--------------------------
            ProcessID = "";            // ' 공정ID
            Process = "";              // ' 공정명
            ProcessCode = "";         // ' 공정 코드
            PlanProcSeq = 0;            // ' 가공순위
            ProcessChildCheckYN = ""; // ' 하위 Check 여부
            AutoGatheringYN = "";      // ' 자동 통신데이타 수집건 인지 확인
            //--------------------------
            BoxID = "";                // ' Box 번호
            CardID = "";              // ' Card 번호
            SplitID = "";              // ' 카드분할번호
            WorkSeq = 0;              // ' 가공순위
            MachineID = "";            // ' 기계호기
            Machine = "";            // ' 기계호기
            //--------------------------
            ColorID = "";              // ' 색상 코드
            Color = "";                // ' 색상
            ColorQty = 0;              // ' 색상 수량
            //--------------------------
            TeamID = "";             // ' 작업조 코드
            Team = "";             // ' 작업조
            PersonID = "";             // ' 작업자 코드
            Person = "";             // ' 작업자
            WorkNO = "";             // ' 공정 호기
            TagID = "";             // ' 선택태그ID (001, 002, ...)
            DayOrNightID = "";        //  (주 / 야 ) 근무 구분자
            DayOrNight = "";        //  (주 / 야 ) 근무 구분자

            //--------------------------
            DefectCnt = 0;            // ' 생산중 불량 수량
            DLoss = 0;            // ' 불량 길이
            WorkQty = 0;            //  수량
            Basis = "";           // ' 검사기준 명
            BasisID = 0;            // ' 검사기준 ID
            QcLot = "";           //
            //-------------------------
            sMoldID = "";            // '금형ID,  2016.03.25, 성광 작업시 추가 함
            sMold = "";            // '금형명,  2016.03.25, 성광 작업시 추가 함
            sInstID = "";            // '작업지시ID, 2016.05.02, 성광 작업시 추가 함
            sLotID = "";            // 'LotID , 2016.05.4, 성광 작업시 추가함
            sInstDetSeq = "";
            sPLotID = "";
        }
        public void DataClear()
        {
            sPLotID = "";
            //-----------
            sArticleID = "";
            sArticle = "";
            //-----------
            OrderID = "";            // ' 관리번호
            OrderNO = "";            // ' Order NO
            Custom = "";            // ' 거래처 명
            OrderQty = 0;             // ' 수주수량
            OrderUnit = "";            // ' 수주 단위
            OrderSeq = 0;             // ' 오더 SEQ
            Article = "";             // ' Article
            //--------------------------
            ProcessID = "";            // ' 공정ID
            Process = "";              // ' 공정명
            ProcessCode = "";         // ' 공정 코드
            PlanProcSeq = 0;            // ' 가공순위
            ProcessChildCheckYN = ""; // ' 하위 Check 여부
            AutoGatheringYN = "";      // ' 자동 통신데이타 수집건 인지 확인
            //--------------------------
            BoxID = "";                // ' Box 번호
            CardID = "";              // ' Card 번호
            SplitID = "";              // ' 카드분할번호
            WorkSeq = 0;              // ' 가공순위
            MachineID = "";            // ' 기계호기
            Machine = "";            // ' 기계호기
            //--------------------------
            ColorID = "";              // ' 색상 코드
            Color = "";                // ' 색상
            ColorQty = 0;              // ' 색상 수량
            //--------------------------
            TeamID = "";             // ' 작업조 코드
            Team = "";             // ' 작업조
            PersonID = "";             // ' 작업자 코드
            Person = "";             // ' 작업자
            WorkNO = "";             // ' 공정 호기
            TagID = "";             // ' 선택태그ID (001, 002, ...)
            DayOrNightID = "";        //  (주 / 야 ) 근무 구분자
            DayOrNight = "";        //  (주 / 야 ) 근무 구분자

            //--------------------------
            DefectCnt = 0;            // ' 생산중 불량 수량
            DLoss = 0;            // ' 불량 길이
            WorkQty = 0;            //  수량
            Basis = "";           // ' 검사기준 명
            BasisID = 0;            // ' 검사기준 ID
            QcLot = "";           //
            //-------------------------
            sMoldID = "";            // '금형ID,  2016.03.25, 성광 작업시 추가 함
            sMold = "";            // '금형명,  2016.03.25, 성광 작업시 추가 함
            sInstID = "";            // '작업지시ID, 2016.05.02, 성광 작업시 추가 함
            sLotID = "";            // 'LotID , 2016.05.4, 성광 작업시 추가함
            sInstDetSeq = "";
            return;
        }
    }
    #endregion

    #region TSTUFFIN 클래스
    public class TSTUFFIN
    {
        public string StuffinID         { get; set; }
        public string StuffDate         { get; set; }
        public string StuffClss         { get; set; }
        public string CustomID          { get; set; }
        public string Custom            { get; set; }
        public string UnitClss          { get; set; }
        public string TotRoll           { get; set; }
        public string TotQty            { get; set; }
        public string TotQtyY           { get; set; }
        public string UnitPrice         { get; set; }
        public string PriceClss         { get; set; }
        public string ExchRate          { get; set; }
        public string VatIndYN          { get; set; }
        public string Remark            { get; set; }
        public string OrderId           { get; set; }
        public string ArticleID         { get; set; }
        public string WorkID            { get; set; }
        public string OrderNO           { get; set; }
        public string InsStuffINYN      { get; set; }
        public string OutwareID         { get; set; }
        public string CompanyID         { get; set; }
        public string BrandClss         { get; set; }
        public string OrderForm         { get; set; }
        public string FromLocID         { get; set; }
        public string TOLocID           { get; set; }
        public string CreateUserID      { get; set; }
        public string CreateDate        { get; set; }
        public string UpdateUserID      { get; set; }
        public string UpdateDate        { get; set; }

        public TSTUFFIN()
        {
            StuffinID       = "";
            StuffDate       = "";
            StuffClss       = "";
            CustomID        = "";
            Custom          = "";
            UnitClss        = "";
            TotRoll         = "";
            TotQty          = "";
            TotQtyY         = "";
            UnitPrice       = "";
            PriceClss       = "";
            ExchRate        = "";
            VatIndYN        = "";
            Remark          = "";
            OrderId         = "";
            ArticleID       = "";
            WorkID          = "";
            OrderNO         = "";
            InsStuffINYN    = "";
            OutwareID       = "";
            CompanyID       = "";
            BrandClss       = "";
            OrderForm       = "";
            FromLocID       = "";
            TOLocID         = "";
            CreateUserID    = "";
            CreateDate      = "";
            UpdateUserID    = "";
            UpdateDate      = "";
        }
    }
    #endregion

    #region TSTUFFINSUB 클래스
    public class TSTUFFINSUB
    {
        public string StuffInID         { get; set; }
        public string StuffInSubSeq     { get; set; }
        public string RollNo            { get; set; }
        public string StuffClss         { get; set; }
        public string Qty               { get; set; }
        public string LotID             { get; set; }
        public string SetDate           { get; set; }
        public string InspectApprovalYN { get; set; }
        public string CreateUserID      { get; set; }
        public string CreateDate        { get; set; }
        public string UpdateUserID      { get; set; }
        public string UpdateDate        { get; set; }

        public TSTUFFINSUB()
        {
            StuffInID           = "";
            StuffInSubSeq       = "";
            RollNo              = "";
            StuffClss           = "";
            Qty                 = "";
            LotID               = "";
            SetDate             = "";
            InspectApprovalYN   = "";
            CreateUserID        = "";
            CreateDate          = "";
            UpdateUserID        = "";
            UpdateDate          = "";
        }
    }
    #endregion

    #region TOUTWARE 클래스
    public class TOUTWARE
    {
        public string OrderID { get; set; }
        public string CompanyID { get; set; }
        public string sOutwareID { get; set; }
        public string CustomID { get; set; }
        public string OutSeq { get; set; }
        public string OutClss { get; set; }
        public string WorkID { get; set; }
        public string ExchRate { get; set; }
        public string Unitprice { get; set; }
        public string LossRate { get; set; }
        public string LossQty { get; set; }
        //'    OutRoll         As Integer
        public string OutRoll { get; set; }
        public string OutQty { get; set; }
        public string OutRealQty { get; set; }
        public string OutWeight { get; set; }
        public string OutRealWeight { get; set; }
        public string OutDate { get; set; }
        public string ResultDate { get; set; }
        public string OutTime { get; set; }
        public string BoOutClss { get; set; }
        public string BoConfirmClss { get; set; }
        public string BoConfirmDate { get; set; }
        public string LoadTime { get; set; }
        public string TranNo { get; set; }
        public string TranSeq { get; set; }
        public string OutType { get; set; }
        public string Remark { get; set; }
        public string snAmount { get; set; }
        public string snVatAmount { get; set; }
        public string sFromLocID { get; set; }
        public string sToLocID { get; set; }
        public string OutCustomID { get; set; }
        public string OutCustom { get; set; }
        public string BuyerDirectYN { get; set; }
        public string InsStuffINYN { get; set; }
        public string sCreateuserID { get; set; }
        public string sUpdateUserID { get; set; }
        public string VatINDYN { get; set; }
        public string sArticleID { get; set; }
        public string sUnitClss { get; set; }

        public TOUTWARE()
        {
            OrderID = "";
            CompanyID = "";
            sOutwareID = "";
            CustomID = "";
            OutSeq = "";
            OutClss = "";
            WorkID = "";
            ExchRate = "";
            Unitprice = "";
            LossRate = "";
            LossQty = "";

            OutRoll = "";
            OutQty = "";
            OutRealQty = "";
            OutWeight = "";
            OutRealWeight = "";
            OutDate = "";
            ResultDate = "";
            OutTime = "";
            BoOutClss = "";
            BoConfirmClss = "";
            BoConfirmDate = "";
            LoadTime = "";
            TranNo = "";
            TranSeq = "";
            OutType = "";
            Remark = "";
            snAmount = "";
            snVatAmount = "";
            sFromLocID = "";
            sToLocID = "";
            OutCustomID = "";
            OutCustom = "";
            BuyerDirectYN = "";
            InsStuffINYN = "";
            sCreateuserID = "";
            sUpdateUserID = "";
            VatINDYN = "";
            sArticleID = "";
            sUnitClss = "";
        }
    }
    #endregion

    #region TOUTWARESUB 클래스
    public class TOUTWARESUB
    {
        public string sOutwareID { get; set; }
        public string OrderID { get; set; }
        public string OutSeq { get; set; }
        public string ArticleID { get; set; }
        public string OutSubSeq { get; set; }
        public string OrderSeq { get; set; }
        public string RollSeq { get; set; }
        public string LabelID { get; set; }
        public string LabelGubun { get; set; }
        public string LotNo { get; set; }
        public string OutQty { get; set; }
        public string Weight { get; set; }
        public string Unitprice { get; set; }
        public string OutRoll { get; set; }
        public string CustomBoxID { get; set; }
        public string PackingID { get; set; }

        public string stuffinID { get; set; }
        public string StuffInSubSeq { get; set; }

        public string sCreateuserID { get; set; }
        public string sUpdateUserID { get; set; }


        public TOUTWARESUB()
        {
            sOutwareID = "";
            OrderID = "";
            OutSeq = "";
            ArticleID = "";
            OutSubSeq = "";
            OrderSeq = "";
            RollSeq = "";
            LabelID = "";
            LabelGubun = "";
            LotNo = "";
            OutQty = "";
            Weight = "";
            Unitprice = "";
            OutRoll = "";
            CustomBoxID = "";
            PackingID = "";

            stuffinID = "";
            StuffInSubSeq = "";

            sCreateuserID = "";
            sUpdateUserID = "";
        }
    }
    #endregion

    #region TWorkData 클래스
    public class TWorkData
    {
        public string CardID { get; set; }              //    ' 카드번호
        public string SplitID { get; set; }             //    ' 분할번호
        public string WorkSeq { get; set; }             //    ' 작업순위
        public string ProcessID { get; set; }           //    ' 공정번호
        public string MachineID { get; set; }           //    ' 호기
        public string WorkDate { get; set; }            //    ' 작업일자
        public string TeamID { get; set; }              //    ' 작업조
        public string PersonID { get; set; }            //    ' 작업자
        public string WorkRoll { get; set; }            //    ' 절수 --
        public string WorkQty { get; set; }             //    ' 수량 --
        public string StuffWidth { get; set; }          //    ' 전폭 --
        public string WorkWidth { get; set; }           //    ' 후폭 --
        public string OverWidth { get; set; }           //    ' Over --

        public string StartDate { get; set; }           //    ' 시작일자
        public string StartTime { get; set; }           //    ' 시작시간
        public string EndDate { get; set; }             //    ' 종료일자
        public string EndTime { get; set; }             //    ' 종료시간
        public string OrderID { get; set; }             //    ' 관리번호
        public string OrderSeq { get; set; }            //    ' 색상번호
        public string CustomID { get; set; }            //    ' 거래처
        public string ArticleID { get; set; }           //    ' 품명
        public string WorkUnitID { get; set; }          //    ' 작업단위번호
        public string WorkUnitSeq { get; set; }         //    ' 작업단위 내 차수
        public string LotNO { get; set; }               //    ' 로트번호
        public string Remark { get; set; }              //    ' 비고사항
        public string ResultDensity { get; set; }       //    ' 밀도      --
        public string BatJaID { get; set; }             //    ' 실물 밧자기 번호
        public string bRework { get; set; }             //    ' 재작업여부
        // ' 공정별 실적                   	
        public string Temper { get; set; }              //    '온도 --
        public string ChunkRate { get; set; }           //    '축률 --
        public string Velocity { get; set; }            //    '속도 --
        public string OverFeed { get; set; }            //
        public string Density { get; set; }             //    '밀도 --

        public string StuffQty { get; set; }            //    '투입길이
        public string RPM { get; set; }                 //    'RPM
        public string Pressure1 { get; set; }           //    '압력
        public string Pressure2 { get; set; }           //
        public string Pressure3 { get; set; }           //
        public string NAOH { get; set; }                //    'NAOH
        public string MachineRunTime { get; set; }      //    '기기가동시간
        public string WorkCon { get; set; }             //    '가동조건
        public string HoldReason { get; set; }          //
        public string CodeID { get; set; }              //

        public string DryID { get; set; }               //
        public string SettingClss { get; set; }         //
        public string SideClss { get; set; }            //
        public string Wind { get; set; }                //
        public string Gas { get; set; }                 //
        public string DyeAuxID { get; set; }            //
        public string RefineClss { get; set; }          //
        public string PepaBon1 { get; set; }            //
        public string PepaBon2 { get; set; }            //
        public string PepaBon3 { get; set; }            //
        public string PepaBon4 { get; set; }            //
        public string Tension { get; set; }             //
        public string RealLoss { get; set; }            //
        public string BaseTemp { get; set; }            //
        public string AgingTemp { get; set; }           //
        public string WRQty { get; set; }               //
        public string WRPrice { get; set; }             //
        public string Change { get; set; }              //
        public string LastUpdateUserID { get; set; }	//'변경작업자
        public TWorkData()
        {
            CardID = "";
            SplitID = "";
            WorkSeq = "";
            ProcessID = "";
            MachineID = "";
            WorkDate = "";
            TeamID = "";
            PersonID = "";
            WorkRoll = "";
            WorkQty = "";
            StuffWidth = "";
            WorkWidth = "";
            OverWidth = "";

            StartDate = "";
            StartTime = "";
            EndDate = "";
            EndTime = "";
            OrderID = "";
            OrderSeq = "";
            CustomID = "";
            ArticleID = "";
            WorkUnitID = "";
            WorkUnitSeq = "";
            LotNO = "";
            Remark = "";
            ResultDensity = "";
            BatJaID = "";
            bRework = "";

            Temper = "";
            ChunkRate = "";
            Velocity = "";
            OverFeed = "";
            Density = "";

            StuffQty = "";
            RPM = "";
            Pressure1 = "";
            Pressure2 = "";
            Pressure3 = "";
            NAOH = "";
            MachineRunTime = "";
            WorkCon = "";
            HoldReason = "";
            CodeID = "";

            DryID = "";
            SettingClss = "";
            SideClss = "";
            Wind = "";
            Gas = "";
            DyeAuxID = "";
            RefineClss = "";
            PepaBon1 = "";
            PepaBon2 = "";
            PepaBon3 = "";
            PepaBon4 = "";
            Tension = "";
            RealLoss = "";
            BaseTemp = "";
            AgingTemp = "";
            WRQty = "";
            WRPrice = "";
            Change = "";
            LastUpdateUserID = "";
        }
        public void TWorkData_DataClear()
        {
            CardID = "";
            SplitID = "";
            WorkSeq = "";
            ProcessID = "";
            MachineID = "";
            WorkDate = "";
            TeamID = "";
            PersonID = "";
            WorkRoll = "";
            WorkQty = "";
            StuffWidth = "";
            WorkWidth = "";
            OverWidth = "";

            StartDate = "";
            StartTime = "";
            EndDate = "";
            EndTime = "";
            OrderID = "";
            OrderSeq = "";
            CustomID = "";
            ArticleID = "";
            WorkUnitID = "";
            WorkUnitSeq = "";
            LotNO = "";
            Remark = "";
            ResultDensity = "";
            BatJaID = "";
            bRework = "";

            Temper = "";
            ChunkRate = "";
            Velocity = "";
            OverFeed = "";
            Density = "";

            StuffQty = "";
            RPM = "";
            Pressure1 = "";
            Pressure2 = "";
            Pressure3 = "";
            NAOH = "";
            MachineRunTime = "";
            WorkCon = "";
            HoldReason = "";
            CodeID = "";

            DryID = "";
            SettingClss = "";
            SideClss = "";
            Wind = "";
            Gas = "";
            DyeAuxID = "";
            RefineClss = "";
            PepaBon1 = "";
            PepaBon2 = "";
            PepaBon3 = "";
            PepaBon4 = "";
            Tension = "";
            RealLoss = "";
            BaseTemp = "";
            AgingTemp = "";
            WRQty = "";
            WRPrice = "";
            Change = "";
            LastUpdateUserID = "";
        }
    }
    #endregion

    #region TWkResultDefect 클래스

    public class TWkResultDefect
    {
        //private int JobID = 0;
        private string _WkDefectID = "";  // '불량번호
        private string _DefectID = "";
        private string _OrderID = "";   // '오더번호
        private int _OrderSeq = 0;   // '오더순
        private string _ProcessID = "";   // '공정
        private string _MachineID = "";   // '호기
        private string _BoxID = "";   // '박스
        private int _XPos = 0;    // '불량위치
        private int _YPos = 0;    // '불량위치
        private string _InspectDate = "";
        private string _InspectTime = "";
        private string _PersonID = "";
        private int _ButtonSeq = 0;
        private string _KDefect = "";     // ' 불량명 한글
        private string _EDefect = "";     // ' 불량명 영문
        private string _TagName = "";     // ' TagName
        private int _Demerit = 0;      // ' 결점  '2011.12.08 추가
        private string _QcLot = "";
        private int _nDefectQty = 0;


        public string WkDefectID
        {
            get { return _WkDefectID; }
            set { _WkDefectID = value; }
        }

        public string DefectID
        {
            get { return _DefectID; }
            set { _DefectID = value; }
        }


        public string OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        public int OrderSeq
        {
            get { return _OrderSeq; }
            set { _OrderSeq = value; }
        }
        public string ProcessID
        {
            get { return _ProcessID; }
            set { _ProcessID = value; }
        }
        public string MachineID
        {
            get { return _MachineID; }
            set { _MachineID = value; }
        }
        public string BoxID
        {
            get { return _BoxID; }
            set { _BoxID = value; }
        }
        public int XPos
        {
            get { return _XPos; }
            set { _XPos = value; }
        }
        public int YPos
        {
            get { return _YPos; }
            set { _YPos = value; }
        }

        public string InspectDate
        {
            get { return _InspectDate; }
            set { _InspectDate = value; }
        }

        public string InspectTime
        {
            get { return _InspectTime; }
            set { _InspectTime = value; }
        }

        public string PersonID
        {
            get { return _PersonID; }
            set { _PersonID = value; }
        }
        public int ButtonSeq
        {
            get { return _ButtonSeq; }
            set { _ButtonSeq = value; }
        }

        public string KDefect
        {
            get { return _KDefect; }
            set { _KDefect = value; }
        }

        public string EDefect
        {
            get { return _EDefect; }
            set { _EDefect = value; }
        }
        public string TagName
        {
            get { return _TagName; }
            set { _TagName = value; }
        }
        public int Demerit
        {
            get { return _Demerit; }
            set { _Demerit = value; }
        }

        public string QcLot
        {
            get { return _QcLot; }
            set { _QcLot = value; }
        }

        public int nDefectQty
        {
            get { return _nDefectQty; }
            set { _nDefectQty = value; }
        }
        public TWkResultDefect()
        {
            //JobID = 0;
            WkDefectID = "";  // '불량번호
            DefectID = "";
            OrderID = "";   // '오더번호
            OrderSeq = 0;   // '오더순
            ProcessID = "";   // '공정
            MachineID = "";   // '호기
            BoxID = "";   // '박스
            XPos = 0;    // '불량위치
            YPos = 0;    // '불량위치
            InspectDate = "";
            InspectTime = "";
            PersonID = "";
            ButtonSeq = 0;
            KDefect = "";     // ' 불량명 한글
            EDefect = "";     // ' 불량명 영문
            TagName = "";     // ' TagName
            Demerit = 0;      // ' 결점  '2011.12.08 추가
            QcLot = "";
            nDefectQty = 0;
        }
        public void DataClear()
        {
            //JobID = 0;
            WkDefectID = "";  // '불량번호
            DefectID = "";
            OrderID = "";   // '오더번호
            OrderSeq = 0;   // '오더순
            ProcessID = "";   // '공정
            MachineID = "";   // '호기
            BoxID = "";   // '박스
            XPos = 0;    // '불량위치
            YPos = 0;    // '불량위치
            InspectDate = "";
            InspectTime = "";
            PersonID = "";
            ButtonSeq = 0;
            KDefect = "";     // ' 불량명 한글
            EDefect = "";     // ' 불량명 영문
            TagName = "";     // ' TagName
            Demerit = 0;      // ' 결점  '2011.12.08 추가
            QcLot = "";
            nDefectQty = 0;

            return;
        }

    }

    #endregion

    #region TDefect 클래스

    public class TDefect
    {
        private string _DefectID = "";  // ' 불량 코드
        private string _Display = "";  // ' 단말기 불량명
        private string _KDefect = "";  // ' 불량명 한글
        private string _EDefect = "";  // ' 불량명 영문
        private string _TagName = "";  // ' TagName
        private string _DefectClss = "";  // ' 불량 구분
        private int _DefectCnt = 0;   // ' 불량 갯수
        private int _ButtonSeq = 0;   // ' 버튼 위치
        private int _Demerit = 0;   // ' 결점  '2011.12.08 추가
        private int _Loss = 0;   //  ' Loss  'S_201201_조일_07 에 의한 추가

        public string DefectID
        {
            get { return _DefectID; }
            set { _DefectID = value; }
        }
        public string Display
        {
            get { return _Display; }
            set { _Display = value; }
        }
        public string KDefect
        {
            get { return _KDefect; }
            set { _KDefect = value; }
        }
        public string EDefect
        {
            get { return _EDefect; }
            set { _EDefect = value; }
        }
        public string TagName
        {
            get { return _TagName; }
            set { _TagName = value; }
        }

        public string DefectClss
        {
            get { return _DefectClss; }
            set { _DefectClss = value; }
        }
        public int DefectCnt
        {
            get { return _DefectCnt; }
            set { _DefectCnt = value; }
        }

        public int ButtonSeq
        {
            get { return _ButtonSeq; }
            set { _ButtonSeq = value; }
        }

        public int Demerit
        {
            get { return _Demerit; }
            set { _Demerit = value; }
        }
        public int Loss
        {
            get { return _Loss; }
            set { _Loss = value; }
        }
        public TDefect()
        {
            DefectID = "";  // ' 불량 코드
            Display = "";   // ' 단말기 불량명
            KDefect = "";  // ' 불량명 한글
            EDefect = "";  // ' 불량명 영문
            TagName = "";  // ' TagName
            DefectClss = "";  // ' 불량 구분
            DefectCnt = 0;   // ' 불량 갯수
            ButtonSeq = 0;   // ' 버튼 위치
            Demerit = 0;   // ' 결점  '2011.12.08 추가
            Loss = 0;   //  ' Loss  'S_201201_조일_07 에 의한 추가
        }

        public void DataClear()
        {
            DefectID = "";  // ' 불량 코드
            Display = "";   // ' 단말기 불량명
            KDefect = "";  // ' 불량명 한글
            EDefect = "";  // ' 불량명 영문
            TagName = "";  // ' TagName
            DefectClss = "";  // ' 불량 구분
            DefectCnt = 0;   // ' 불량 갯수
            ButtonSeq = 0;   // ' 버튼 위치
            Demerit = 0;   // ' 결점  '2011.12.08 추가
            Loss = 0;   //  ' Loss  'S_201201_조일_07 에 의한 추가

        }
    }
    #endregion

    #region TSplit 클래스
    public class TSplit
    {
        private float _JobID = 0;
        private string _LabelID = "";  
        private string _ArticleID = "";
        private string _PersonID = "";   
        private double _Qty = 0;

        private string _InstID = "";



        public float JobID
        {
            get { return _JobID; }
            set { _JobID = value; }
        }

        public string LabelID
        {
            get { return _LabelID; }
            set { _LabelID = value; }
        }

        public string ArticleID
        {
            get { return _ArticleID; }
            set { _ArticleID = value; }
        }

        public string PersonID
        {
            get { return _PersonID; }
            set { _PersonID = value; }
        }
        public double Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }
        public string InstID
        {
            get { return _InstID; }
            set { _InstID = value; }
        }


        public TSplit()
        {
            JobID = 0;
            LabelID = "";  
            ArticleID = "";
            PersonID = "";   
            Qty = 0;

            InstID = "";


        }
        public void DataClear()
        {
            JobID = 0;
            LabelID = "";
            ArticleID = "";
            PersonID = "";
            Qty = 0;

            InstID = "";

            return;
        }
    }

    #endregion

    #region TTerminalSet 클래스
    public class TTerminalSet
    {
        private int _EnCoderPort = 0;
        private int _PrinterPort = 0;
        private int _IndicatorPort = 0;
        private string _EncoderClss = "";    //' Encoder의 단위 (Yard, Meter)
        private int _TagShift = 0;        // '프린터 보정위치 값
        //'-------------------------------------------
        private string _GradeClss = "";    //' 등급결정 방법 (0: 사용않함, 1: 검사자지정, 2:예외)
        private string _DemeritClss = "";    //' 벌점적용 방법 (0: 사용않함, 1: 불량보상-지정감점사용, 2: 검사기준적용, 3: 수동입력적용)
        private string _LossClss = "";    //' 보상적용 방법 (0: 사용않함, 1: 보상-지정보상사용)
        private string _DefectClss = "";    //' 대표불량 선택 등급기준 (어느 등급(Index)부터 대표불량을 적용할것인지 ? > 0)
        private string _CutDefect = "";    //' 난단불량 사용여부 (0 : 사용않함, ~ )
        //'-------------------------------------------
        private int _ButtonX = 0;     //' 버튼 X 갯수
        private int _ButtonY = 0;     //' 버튼 Y 갯수
        private int _ColorCnt = 0;     //' 색상수
        private int _RepeatCnt = 0;     //' 색상 반복수
        private int _FontSize = 0;     //'= 폰트 크기
        //'-------------------------------------------
        private string _RoundClss = "";   //' 소숫점 관리
        private int _RollClss = 0;      //' 0: Order별, 1: Color별, 2:Order, Color, Lot별, 3: Order, Lot별, 4: Order, Color, 호기별 
        private int _WeightPort = 0;         //'20160316 성광콜드포징 게측기 (중량) 기기 추가


        public int EnCoderPort
        {
            get { return _EnCoderPort; }
            set { _EnCoderPort = value; }
        }
        public int PrinterPort
        {
            get { return _PrinterPort; }
            set { _PrinterPort = value; }
        }
        public int IndicatorPort
        {
            get { return _IndicatorPort; }
            set { _IndicatorPort = value; }
        }
        public string EncoderClss
        {
            get { return _EncoderClss; }
            set { _EncoderClss = value; }
        }
        public int TagShift
        {
            get { return _TagShift; }
            set { _TagShift = value; }
        }
        public string GradeClss
        {
            get { return _GradeClss; }
            set { _GradeClss = value; }
        }
        public string DemeritClss
        {
            get { return _DemeritClss; }
            set { _DemeritClss = value; }
        }
        public string LossClss
        {
            get { return _LossClss; }
            set { _LossClss = value; }
        }
        public string DefectClss
        {
            get { return _DefectClss; }
            set { _DefectClss = value; }
        }
        public string CutDefect
        {
            get { return _CutDefect; }
            set { _CutDefect = value; }
        }
        public int ButtonX
        {
            get { return _ButtonX; }
            set { _ButtonX = value; }
        }
        public int ButtonY
        {
            get { return _ButtonY; }
            set { _ButtonY = value; }
        }
        public int ColorCnt
        {
            get { return _ColorCnt; }
            set { _ColorCnt = value; }
        }
        public int RepeatCnt
        {
            get { return _RepeatCnt; }
            set { _RepeatCnt = value; }
        }
        public int FontSize
        {
            get { return _FontSize; }
            set { _FontSize = value; }
        }
        public string RoundClss
        {
            get { return _RoundClss; }
            set { _RoundClss = value; }
        }
        public int RollClss
        {
            get { return _RollClss; }
            set { _RollClss = value; }
        }
        public int WeightPort
        {
            get { return _WeightPort; }
            set { _WeightPort = value; }
        }

        public TTerminalSet()
        {
            EnCoderPort = 0;
            PrinterPort = 0;
            IndicatorPort = 0;
            EncoderClss = "";    //' Encoder의 단위 (Yard, Meter)
            TagShift = 0;        // '프린터 보정위치 값
            // -----------------------------
            GradeClss = "";    //' 등급결정 방법 (0: 사용않함, 1: 검사자지정, 2:예외)
            DemeritClss = "";    //' 벌점적용 방법 (0: 사용않함, 1: 불량보상-지정감점사용, 2: 검사기준적용, 3: 수동입력적용)
            LossClss = "";    //' 보상적용 방법 (0: 사용않함, 1: 보상-지정보상사용)
            DefectClss = "";    //' 대표불량 선택 등급기준 (어느 등급(Index)부터 대표불량을 적용할것인지 ? > 0)
            CutDefect = "";    //' 난단불량 사용여부 (0 : 사용않함, ~ )
            //-----------------------------
            ButtonX = 0;     //' 버튼 X 갯수
            ButtonY = 0;     //' 버튼 Y 갯수
            ColorCnt = 0;     //' 색상수
            RepeatCnt = 0;     //' 색상 반복수
            FontSize = 0;     //'= 폰트 크기
            //-----------------------------
            RoundClss = "";   //' 소숫점 관리
            RollClss = 0;      //' 0: Order별, 1: Color별, 2:Order, Color, Lot별, 3: Order, Lot별, 4: Order, Color, 호기별 
            WeightPort = 0;         //'20160316 성광콜드포징 게측기 (중량) 기기 추가

        }
        public void DataClear()
        {
            EnCoderPort = 0;
            PrinterPort = 0;
            IndicatorPort = 0;
            EncoderClss = "";    //' Encoder의 단위 (Yard, Meter)
            TagShift = 0;        // '프린터 보정위치 값
            //-----------------------------
            GradeClss = "";    //' 등급결정 방법 (0: 사용않함, 1: 검사자지정, 2:예외)
            DemeritClss = "";    //' 벌점적용 방법 (0: 사용않함, 1: 불량보상-지정감점사용, 2: 검사기준적용, 3: 수동입력적용)
            LossClss = "";    //' 보상적용 방법 (0: 사용않함, 1: 보상-지정보상사용)
            DefectClss = "";    //' 대표불량 선택 등급기준 (어느 등급(Index)부터 대표불량을 적용할것인지 ? > 0)
            CutDefect = "";    //' 난단불량 사용여부 (0 : 사용않함, ~ )
            //-----------------------------
            ButtonX = 0;     //' 버튼 X 갯수
            ButtonY = 0;     //' 버튼 Y 갯수
            ColorCnt = 0;     //' 색상수
            RepeatCnt = 0;     //' 색상 반복수
            FontSize = 0;     //'= 폰트 크기
            //-----------------------------
            RoundClss = "";   //' 소숫점 관리
            RollClss = 0;      //' 0: Order별, 1: Color별, 2:Order, Color, Lot별, 3: Order, Lot별, 4: Order, Color, 호기별 
            WeightPort = 0;         //'20160316 성광콜드포징 게측기 (중량) 기기 추가
            return;
        }
    }

    #endregion

    #region TMold 클래스
    public class TMold
    {
        private string _sMoldID = "";
        private string _sLotNo = "";
        private int _sCavity = 0;
        private int _sRealCavity = 0;
        private int _sHitCount = 0;
        private int _sSafeHitCount = 0;


        public string sMoldID
        {
            get { return _sMoldID; }
            set { _sMoldID = value; }
        }
        public string sLotNo
        {
            get { return _sLotNo; }
            set { _sLotNo = value; }
        }

        public int sCavity
        {
            get { return _sCavity; }
            set { _sCavity = value; }
        }

        public int sRealCavity
        {
            get { return _sRealCavity; }
            set { _sRealCavity = value; }
        }

        public int sHitCount
        {
            get { return _sHitCount; }
            set { _sHitCount = value; }
        }

        public int sSafeHitCount
        {
            get { return _sSafeHitCount; }
            set { _sSafeHitCount = value; }
        }

        public TMold()
        {
            this.sMoldID = "";
            this.sRealCavity = 0;
            this.sHitCount = 0;
            this.sLotNo = "";
            this.sCavity = 0;
            this.sSafeHitCount = 0;


        }
        public void DataClear()
        {
            this.sMoldID = "";
            this.sRealCavity = 0;
            this.sHitCount = 0;
            this.sLotNo = "";
            this.sCavity = 0;
            this.sSafeHitCount = 0;

            return;
        }
        public TMold(string strsMoldID, string strsLotNo, int intsRealCavity, int intsHitCount, int intsCavity, int intsSafeHitCount)
        {
            this.sMoldID = strsMoldID;
            this.sRealCavity = intsRealCavity;
            this.sHitCount = intsHitCount;
            this.sLotNo = strsLotNo;
            this.sCavity = intsCavity;
            this.sSafeHitCount = intsSafeHitCount;

        }



    }

    #endregion

    #region TMold 클래스 여러개일때 사용
    public class Mold
    {
        private string _sMoldID = "";
        private string _sLotNo = "";
        private int _sCavity = 0;
        private int _sRealCavity = 0;
        private int _sHitCount = 0;
        private int _sSafeHitCount = 0;


        public string sMoldID
        {
            get { return _sMoldID; }
            set { _sMoldID = value; }
        }
        public string sLotNo
        {
            get { return _sLotNo; }
            set { _sLotNo = value; }
        }

        public int sCavity
        {
            get { return _sCavity; }
            set { _sCavity = value; }
        }

        public int sRealCavity
        {
            get { return _sRealCavity; }
            set { _sRealCavity = value; }
        }

        public int sHitCount
        {
            get { return _sHitCount; }
            set { _sHitCount = value; }
        }

        public int sSafeHitCount
        {
            get { return _sSafeHitCount; }
            set { _sSafeHitCount = value; }
        }

        public Mold()
        {
            this.sMoldID = "";
            this.sRealCavity = 0;
            this.sHitCount = 0;
            this.sLotNo = "";
            this.sCavity = 0;
            this.sSafeHitCount = 0;


        }
        public void DataClear()
        {
            this.sMoldID = "";
            this.sRealCavity = 0;
            this.sHitCount = 0;
            this.sLotNo = "";
            this.sCavity = 0;
            this.sSafeHitCount = 0;

            return;
        }
        public Mold(string strsMoldID, string strsLotNo, int intsRealCavity, int intsHitCount, int intsCavity, int intsSafeHitCount)
        {
            this.sMoldID = strsMoldID;
            this.sRealCavity = intsRealCavity;
            this.sHitCount = intsHitCount;
            this.sLotNo = strsLotNo;
            this.sCavity = intsCavity;
            this.sSafeHitCount = intsSafeHitCount;

        }



    }

    #endregion

    #region Sub_TMold 클래스 Local에서 사용
    public class Sub_TMold
    {
        private string _sMoldID = "";
        private int _sRealCavity = 0;
        private int _sHitCount = 0;

        public string sMoldID
        {
            get { return _sMoldID; }
            set { _sMoldID = value; }
        }

        public int sRealCavity
        {
            get { return _sRealCavity; }
            set { _sRealCavity = value; }
        }

        public int sHitCount
        {
            get { return _sHitCount; }
            set { _sHitCount = value; }
        }

        public Sub_TMold()
        {
            this.sMoldID = "";
            this.sRealCavity = 0;
            this.sHitCount = 0;

        }
        public void DataClear()
        {
            this.sMoldID = "";
            this.sRealCavity = 0;
            this.sHitCount = 0;

            return;
        }
        public Sub_TMold(string strsMoldID, int intsRealCavity, int intsHitCount)
        {
            this.sMoldID = strsMoldID;
            this.sRealCavity = intsRealCavity;
            this.sHitCount = intsHitCount;
        }
    }

    #endregion

    #region Sub_TtdChange 클래스
    public class Sub_TtdChange
    {
        private string _CreateUserID = "";
        private string _MachineID = "";
        private string _PersonID = "";
        private string _ProcessID = "";
        private string _TdChangeYN = "";
        private string _TdDate = "";
        private string _Tdgbn = "";
        private string _tdtime = "";


        public string CreateUserID
        {
            get { return _CreateUserID; }
            set { _CreateUserID = value; }
        }
        public string MachineID
        {
            get { return _MachineID; }
            set { _MachineID = value; }
        }

        public string PersonID
        {
            get { return _PersonID; }
            set { _PersonID = value; }
        }

        public string ProcessID
        {
            get { return _ProcessID; }
            set { _ProcessID = value; }
        }

        public string TdChangeYN
        {
            get { return _TdChangeYN; }
            set { _TdChangeYN = value; }
        }

        public string TdDate
        {
            get { return _TdDate; }
            set { _TdDate = value; }
        }

        public string Tdgbn
        {
            get { return _Tdgbn; }
            set { _Tdgbn = value; }
        }

        public string tdtime
        {
            get { return _tdtime; }
            set { _tdtime = value; }
        }

        public Sub_TtdChange()
        {
            CreateUserID = "";
            MachineID = "";
            PersonID = "";
            ProcessID = "";
            TdChangeYN = "";
            TdDate = "";
            Tdgbn = "";
            tdtime = "";
        }
        public void DataClear()
        {
            CreateUserID = "";
            MachineID = "";
            PersonID = "";
            ProcessID = "";
            TdChangeYN = "";
            TdDate = "";
            Tdgbn = "";
            tdtime = "";

            return;
        }
        public Sub_TtdChange(string strCreateUserID, string strMachineID, string strPersonID, string strProcessID, string strTdChangeYN, string strTdDate, string strTdgbn, string strtdtime)
        {
            this.CreateUserID = strCreateUserID;
            this.MachineID = strMachineID;
            this.PersonID = strPersonID;
            this.ProcessID = strProcessID;
            this.TdChangeYN = strTdChangeYN;
            this.TdDate = strTdDate;
            this.Tdgbn = strTdgbn;
            this.tdtime = strtdtime;
        }



    }

    #endregion

    #region Sub_TWkResult 클래스
    public class Sub_TWkResult
    {
        string _ArticleID = "";
        string _Comments = "";
        string _CreateUserID = "";
        float _DownBurnPlateTemper1 = 0;
        float _DownBurnPlateTemper2 = 0;
        float _FormaOpenTime = 0;
        float _FormaTime = 0;
        int _InstDetSeq = 0;
        string _InstID = "";
        string _JobGbn = "";
        float _JobID = 0;
        string _LabelGubun = "";
        string _LabelID = "";
        string _MachineID = "";
        int _nOrderSeq = 0;
        string _ProcessID = "";
        string _ProdAutoInspectYN = "";
        string _ReworkLinkProdID = "";
        string _ReworkOldYN = "";
        string _s4MID = "";
        string _ScanDate = "";
        string _ScanTime = "";
        float _SetDownBurnPlateTemper = 0;
        float _SetFormaOpenTime = 0;
        float _SetFormaTime = 0;
        float _SetUpBurnPlateTemper = 0;
        string _sLastArticleYN = "";
        float _sLogID = 0;
        string _sNowReworkCode = "";
        string _sOrderID = "";
        string _sUnitClss = "";
        float _UpBurnPlateTemper1 = 0;
        float _UpBurnPlateTemper2 = 0;
        string _WDID = "";
        string _WDNO = "";
        float _WDQty = 0;
        string _WorkEndDate = "";
        string _WorkEndTime = "";
        float _WorkQty = 0;
        float _CycleTime = 0;
        string _WorkStartDate = "";
        string _WorkStartTime = "";
        string _YLabelID = "";
        string _EmitTemp = "";
        string _ChamberTemp = "";
        string _OutWaterTemp = "";
        string _EnterWaterTemp = "";
        string _NidaTime = "";
        string _RoraTime = "";
        string _RPM = "";
        string _BallSize = "";
        string _Temp = "";
        string _JaturiQty = "";
        string _BoxWeight = "";
        string _MiddleWeight = "";
        string _EarlyWeight = "";
        string _Weight = "";
        string _Length = "";
        string _Thickness = "";
        string _Width = "";
        string _DumpingTemp = "";
        string _DayOrNightID = "";
        string _SplitYNGBN = "";

        public string DumpingTemp
        {
            get { return _DumpingTemp; }
            set { _DumpingTemp = value; }
        }
        public string Width
        {
            get { return _Width; }
            set { _Width = value; }
        }
        public string Thickness
        {
            get { return _Thickness; }
            set { _Thickness = value; }
        }
        public string Length
        {
            get { return _Length; }
            set { _Length = value; }
        }
        public string Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }
        public string EarlyWeight
        {
            get { return _EarlyWeight; }
            set { _EarlyWeight = value; }
        }
        public string MiddleWeight
        {
            get { return _MiddleWeight; }
            set { _MiddleWeight = value; }
        }
        public string BoxWeight
        {
            get { return _BoxWeight; }
            set { _BoxWeight = value; }
        }
        public string JaturiQty
        {
            get { return _JaturiQty; }
            set { _JaturiQty = value; }
        }
        public string Temp
        {
            get { return _Temp; }
            set { _Temp = value; }
        }
        public string BallSize
        {
            get { return _BallSize; }
            set { _BallSize = value; }

        }
        public string RPM
        {
            get { return _RPM; }
            set { _RPM = value; }

        }





        public string RoraTime
        {
            get { return _RoraTime; }
            set { _RoraTime = value; }
        }
        public string NidaTime
        {
            get { return _NidaTime; }
            set { _NidaTime = value; }
        }
        public string EnterWaterTemp
        {
            get { return _EnterWaterTemp; }
            set { _EnterWaterTemp = value; }
        }
        public string OutWaterTemp
        {
            get { return _OutWaterTemp; }
            set { _OutWaterTemp = value; }
        }
        public string ChamberTemp
        {
            get { return _ChamberTemp; }
            set { _ChamberTemp = value; }
        }
        public string EmitTemp
        {
            get { return _EmitTemp; }
            set { _EmitTemp = value; }

        }



        public string ArticleID
        {
            get { return _ArticleID; }
            set { _ArticleID = value; }
        }
        public string Comments
        {
            get { return _Comments; }
            set { _Comments = value; }
        }

        public string CreateUserID
        {
            get { return _CreateUserID; }
            set { _CreateUserID = value; }
        }

        public float DownBurnPlateTemper1
        {
            get { return _DownBurnPlateTemper1; }
            set { _DownBurnPlateTemper1 = value; }
        }

        public float DownBurnPlateTemper2
        {
            get { return _DownBurnPlateTemper2; }
            set { _DownBurnPlateTemper2 = value; }
        }

        public float FormaOpenTime
        {
            get { return _FormaOpenTime; }
            set { _FormaOpenTime = value; }
        }

        public float FormaTime
        {
            get { return _FormaTime; }
            set { _FormaTime = value; }
        }

        public int InstDetSeq
        {
            get { return _InstDetSeq; }
            set { _InstDetSeq = value; }
        }

        public string InstID
        {
            get { return _InstID; }
            set { _InstID = value; }
        }

        public string JobGbn
        {
            get { return _JobGbn; }
            set { _JobGbn = value; }
        }

        public float JobID
        {
            get { return _JobID; }
            set { _JobID = value; }
        }

        public string LabelGubun
        {
            get { return _LabelGubun; }
            set { _LabelGubun = value; }
        }

        public string LabelID
        {
            get { return _LabelID; }
            set { _LabelID = value; }
        }

        public string MachineID
        {
            get { return _MachineID; }
            set { _MachineID = value; }
        }

        public int nOrderSeq
        {
            get { return _nOrderSeq; }
            set { _nOrderSeq = value; }
        }

        public string ProcessID
        {
            get { return _ProcessID; }
            set { _ProcessID = value; }
        }

        public string ProdAutoInspectYN
        {
            get { return _ProdAutoInspectYN; }
            set { _ProdAutoInspectYN = value; }
        }

        public string ReworkLinkProdID
        {
            get { return _ReworkLinkProdID; }
            set { _ReworkLinkProdID = value; }
        }
        public string ReworkOldYN
        {
            get { return _ReworkOldYN; }
            set { _ReworkOldYN = value; }
        }
        public string s4MID
        {
            get { return _s4MID; }
            set { _s4MID = value; }
        }
        public string ScanDate
        {
            get { return _ScanDate; }
            set { _ScanDate = value; }
        }
        public string ScanTime
        {
            get { return _ScanTime; }
            set { _ScanTime = value; }
        }
        public float SetDownBurnPlateTemper
        {
            get { return _SetDownBurnPlateTemper; }
            set { _SetDownBurnPlateTemper = value; }
        }
        public float SetFormaOpenTime
        {
            get { return _SetFormaOpenTime; }
            set { _SetFormaOpenTime = value; }
        }
        public float SetFormaTime
        {
            get { return _SetFormaTime; }
            set { _SetFormaTime = value; }
        }
        public float SetUpBurnPlateTemper
        {
            get { return _SetUpBurnPlateTemper; }
            set { _SetUpBurnPlateTemper = value; }
        }
        public string sLastArticleYN
        {
            get { return _sLastArticleYN; }
            set { _sLastArticleYN = value; }
        }
        public float sLogID
        {
            get { return _sLogID; }
            set { _sLogID = value; }
        }
        public string sNowReworkCode
        {
            get { return _sNowReworkCode; }
            set { _sNowReworkCode = value; }
        }
        public string sOrderID
        {
            get { return _sOrderID; }
            set { _sOrderID = value; }
        }
        public string sUnitClss
        {
            get { return _sUnitClss; }
            set { _sUnitClss = value; }
        }
        public float UpBurnPlateTemper1
        {
            get { return _UpBurnPlateTemper1; }
            set { _UpBurnPlateTemper1 = value; }
        }
        public float UpBurnPlateTemper2
        {
            get { return _UpBurnPlateTemper2; }
            set { _UpBurnPlateTemper2 = value; }
        }
        public string WDID
        {
            get { return _WDID; }
            set { _WDID = value; }

        }

        public string WDNO
        {
            get { return _WDNO; }
            set { _WDNO = value; }
        }


        public float WDQty
        {
            get { return _WDQty; }
            set { _WDQty = value; }
        }

        public string WorkEndDate
        {
            get { return _WorkEndDate; }
            set { _WorkEndDate = value; }
        }

        public string WorkEndTime
        {
            get { return _WorkEndTime; }
            set { _WorkEndTime = value; }
        }

        public float WorkQty
        {
            get { return _WorkQty; }
            set { _WorkQty = value; }
        }

        public float CycleTime
        {
            get { return _CycleTime; }
            set { _CycleTime = value; }
        }

        public string WorkStartDate
        {
            get { return _WorkStartDate; }
            set { _WorkStartDate = value; }
        }

        public string WorkStartTime
        {
            get { return _WorkStartTime; }
            set { _WorkStartTime = value; }
        }

        public string YLabelID
        {
            get { return _YLabelID; }
            set { _YLabelID = value; }
        }

        public string DayOrNightID
        {
            get { return _DayOrNightID; }
            set { _DayOrNightID = value; }
        }
        public string SplitYNGBN
        {
            get { return _SplitYNGBN; }
            set { _SplitYNGBN = value; }
        }




        public Sub_TWkResult()
        {
            this.ArticleID = "";
            this.Comments = "";
            this.CreateUserID = "";
            this.DownBurnPlateTemper1 = 0;
            this.DownBurnPlateTemper2 = 0;
            this.FormaOpenTime = 0;
            this.FormaTime = 0;
            this.InstDetSeq = 0;
            this.InstID = "";
            this.JobGbn = "";
            this.JobID = 0;
            this.LabelGubun = "";
            this.LabelID = "";
            this.MachineID = "";
            this.nOrderSeq = 0;
            this.ProcessID = "";
            this.ProdAutoInspectYN = "";
            this.ReworkLinkProdID = "";
            this.ReworkOldYN = "";
            this.s4MID = "";
            this.ScanDate = "";
            this.ScanTime = "";
            this.SetDownBurnPlateTemper = 0;
            this.SetFormaOpenTime = 0;
            this.SetFormaTime = 0;
            this.SetUpBurnPlateTemper = 0;
            this.sLastArticleYN = "";
            this.sLogID = 0;
            this.sNowReworkCode = "";
            this.sOrderID = "";
            this.sUnitClss = "";
            this.UpBurnPlateTemper1 = 0; this.UpBurnPlateTemper2 = 0;
            this.WDID = "";
            this.WDNO = "";
            this.WDQty = 0;
            this.WorkEndDate = "";
            this.WorkEndTime = "";
            this.WorkQty = 0;
            this.CycleTime = 0;
            this.WorkStartDate = "";
            this.WorkStartTime = "";
            this.YLabelID = "";
            EmitTemp = "";
            ChamberTemp = "";
            OutWaterTemp = "";
            EnterWaterTemp = "";
            NidaTime = "";
            RoraTime = "";
            RPM = "";
            BallSize = "";
            Temp = "";
            JaturiQty = "";
            BoxWeight = "";
            MiddleWeight = "";
            EarlyWeight = "";
            Weight = "";
            Length = "";
            Thickness = "";
            Width = "";
            DumpingTemp = "";
            DayOrNightID = "";
            SplitYNGBN = "";
        }
        public void DataClear()
        {
            this.ArticleID = "";
            this.Comments = "";
            this.CreateUserID = "";
            this.DownBurnPlateTemper1 = 0;
            this.DownBurnPlateTemper2 = 0;
            this.FormaOpenTime = 0;
            this.FormaTime = 0;
            this.InstDetSeq = 0;
            this.InstID = "";
            this.JobGbn = "";
            this.JobID = 0;
            this.LabelGubun = "";
            this.LabelID = "";
            this.MachineID = "";
            this.nOrderSeq = 0;
            this.ProcessID = "";
            this.ProdAutoInspectYN = "";
            this.ReworkLinkProdID = "";
            this.ReworkOldYN = "";
            this.s4MID = "";
            this.ScanDate = "";
            this.ScanTime = "";
            this.SetDownBurnPlateTemper = 0;
            this.SetFormaOpenTime = 0;
            this.SetFormaTime = 0;
            this.SetUpBurnPlateTemper = 0;
            this.sLastArticleYN = "";
            this.sLogID = 0;
            this.sNowReworkCode = "";
            this.sOrderID = "";
            this.sUnitClss = "";
            this.UpBurnPlateTemper1 = 0;
            this.UpBurnPlateTemper2 = 0;
            this.WDID = "";
            this.WDNO = "";
            this.WDQty = 0;
            this.WorkEndDate = "";
            this.WorkEndTime = "";
            this.WorkQty = 0;
            this.CycleTime = 0;
            this.WorkStartDate = "";
            this.WorkStartTime = "";
            this.YLabelID = "";
            EmitTemp = "";
            ChamberTemp = "";
            OutWaterTemp = "";
            EnterWaterTemp = "";
            NidaTime = "";
            RoraTime = "";
            RPM = "";
            BallSize = "";
            Temp = "";
            JaturiQty = "";
            BoxWeight = "";
            MiddleWeight = "";
            EarlyWeight = "";
            Weight = "";
            Length = "";
            Thickness = "";
            Width = "";
            DumpingTemp = "";
            DayOrNightID = "";
            SplitYNGBN = "";

            return;
        }
        public Sub_TWkResult(string strArticleID, string strComments, string strCreateUserID, float sinDownBurnPlateTemper1, float sinDownBurnPlateTemper2,
                         float sinFormaOpenTime, float sinFormaTime, int intInstDetSeq, string strInstID, string strJobGbn,
                         float sinJobID, string strLabelGubun, string strLabelID, string strMachineID, int intnOrderSeq,
                         string strProcessID, string strProdAutoInspectYN, string strReworkLinkProdID, string strReworkOldYN, string strs4MID,
                         string strScanDate, string strScanTime, float sinSetDownBurnPlateTemper, float sinSetFormaOpenTime, float sinSetFormaTime,
                         float sinSetUpBurnPlateTemper, string strsLastArticleYN, float sinsLogID, string strsNowReworkCode, string strsOrderID,
                         string strsUnitClss, float sinUpBurnPlateTemper1, float sinUpBurnPlateTemper2, string strWDID, string strWDNO,
                         float sinWDQty, string strWorkEndDate, string strWorkEndTime, float sinWorkQty, string strWorkStartDate,
                         string strWorkStartTime, string strYLabelID, string DayOrNightID, string SplitYNGBN)
        {
            this.ArticleID = strArticleID;
            this.Comments = strComments;
            this.CreateUserID = strCreateUserID;
            this.DownBurnPlateTemper1 = sinDownBurnPlateTemper1;
            this.DownBurnPlateTemper2 = sinDownBurnPlateTemper2;
            this.FormaOpenTime = sinFormaOpenTime;
            this.FormaTime = sinFormaTime;
            this.InstDetSeq = intInstDetSeq;
            this.InstID = strInstID;
            this.JobGbn = strJobGbn;
            this.JobID = sinJobID;
            this.LabelGubun = strLabelGubun;
            this.LabelID = strLabelID;
            this.MachineID = strMachineID;
            this.nOrderSeq = intnOrderSeq;
            this.ProcessID = strProcessID;
            this.ProdAutoInspectYN = strProdAutoInspectYN;
            this.ReworkLinkProdID = strReworkLinkProdID;
            this.ReworkOldYN = strReworkOldYN;
            this.s4MID = strs4MID;
            this.ScanDate = strScanDate;
            this.ScanTime = strScanTime;
            this.SetDownBurnPlateTemper = sinSetDownBurnPlateTemper;
            this.SetFormaOpenTime = sinSetFormaOpenTime;
            this.SetFormaTime = sinSetFormaTime;
            this.SetUpBurnPlateTemper = sinSetUpBurnPlateTemper;
            this.sLastArticleYN = strsLastArticleYN;
            this.sLogID = sinsLogID;
            this.sNowReworkCode = strsNowReworkCode;
            this.sOrderID = strsOrderID;
            this.sUnitClss = strsUnitClss;
            this.UpBurnPlateTemper1 = sinUpBurnPlateTemper1;
            this.UpBurnPlateTemper2 = sinUpBurnPlateTemper2;
            this.WDID = strWDID;
            this.WDNO = strWDNO;
            this.WDQty = sinWDQty;
            this.WorkEndDate = strWorkEndDate;
            this.WorkEndTime = strWorkEndTime;
            this.WorkQty = sinWorkQty;
            this.CycleTime = 0;
            this.WorkStartDate = strWorkStartDate;
            this.WorkStartTime = strWorkStartTime;
            this.YLabelID = strYLabelID;
            this.DayOrNightID = DayOrNightID;
            this.SplitYNGBN = SplitYNGBN;
        }



    }

    #endregion

    #region Sub_TWkResultArticleChild 클래스
    public class Sub_TWkResultArticleChild
    {
        private string _ChildArticleID = "";
        private string _ChildLabelGubun = "";
        private string _ChildLabelID = "";
        private string _CreateUserID = "";
        private string _Flag = "";
        private float _JobID = 0;
        private int _JobSeq = 0;
        private string _OutDate = "";
        private string _OutTime = "";
        private string _ReworkOldYN = "";
        private string _ReworkLinkChildProdID = "";
        private double _WorkQty = 0;
        public string ChildArticleID
        {
            get { return _ChildArticleID; }
            set { _ChildArticleID = value; }
        }

        public string ChildLabelGubun
        {
            get { return _ChildLabelGubun; }
            set { _ChildLabelGubun = value; }
        }

        public string ChildLabelID
        {
            get { return _ChildLabelID; }
            set { _ChildLabelID = value; }
        }

        public string CreateUserID
        {
            get { return _CreateUserID; }
            set { _CreateUserID = value; }
        }

        public string Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }

        public float JobID
        {
            get { return _JobID; }
            set { _JobID = value; }
        }

        public int JobSeq
        {
            get { return _JobSeq; }
            set { _JobSeq = value; }
        }
        public string OutDate
        {
            get { return _OutDate; }
            set { _OutDate = value; }
        }

        public string OutTime
        {
            get { return _OutTime; }
            set { _OutTime = value; }
        }
        public string ReworkLinkChildProdID
        {
            get { return _ReworkLinkChildProdID; }
            set { _ReworkLinkChildProdID = value; }
        }

        public string ReworkOldYN
        {
            get { return _ReworkOldYN; }
            set { _ReworkOldYN = value; }
        }
        public double WorkQty
        {
            get { return _WorkQty; }
            set { _WorkQty = value; }
        }

        public Sub_TWkResultArticleChild()
        {
            this.ChildArticleID = "";
            this.ChildLabelGubun = "";
            this.ChildLabelID = "";
            this.CreateUserID = "";
            this.Flag = "";
            this.JobID = 0;
            this.JobSeq = 0;
            this.OutDate = "";
            this.OutTime = "";
            this.ReworkLinkChildProdID = "";
            this.ReworkOldYN = "";
            WorkQty = 0;
        }
        public void DataClear()
        {
            this.ChildArticleID = "";
            this.ChildLabelGubun = "";
            this.ChildLabelID = "";
            this.CreateUserID = "";
            this.Flag = "";
            this.JobID = 0;
            this.JobSeq = 0;
            this.OutDate = "";
            this.OutTime = "";
            this.ReworkLinkChildProdID = "";
            this.ReworkOldYN = "";
            WorkQty = 0;
            return;
        }
        public Sub_TWkResultArticleChild(string strChildArticleID, string strChildLabelGubun, string strChildLabelID, string strCreateUserID, string strFlag,
                                     float floJobID, int intJobSeq, string strOutDate, string OutTime, string ReworkLinkChildProdID, string ReworkOldYN)
        {
            this.ChildArticleID = strChildArticleID;
            this.ChildLabelGubun = strChildLabelGubun;
            this.ChildLabelID = strChildLabelID;
            this.CreateUserID = strCreateUserID;
            this.Flag = strFlag;
            this.JobID = floJobID;
            this.JobSeq = intJobSeq;
            this.OutDate = strOutDate;
            this.OutTime = OutTime;
            this.ReworkLinkChildProdID = ReworkLinkChildProdID;
            this.ReworkOldYN = ReworkOldYN;
        }
    }

    #endregion

    #region Sub_TWkLabelPrint 클래스
    public class Sub_TWkLabelPrint
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

        public Sub_TWkLabelPrint()
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
        public Sub_TWkLabelPrint(int intnInstDetSeq, long longnPrintProductQty, int intnPrintQty, long longnQtyPerBox, int intnReprintQty,
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

    #region Sub_TWkResult_SplitAdd 클래스
    public class Sub_TWkResult_SplitAdd
    {
        private float _JobID = 0;
        private int _SplitSeq = 0;
        private string _WorkPersonID = "";
        private double _WorkQty = 0;
        private string _ScanDate = "";

        private string _ScanTime = "";
        private string _WorkStartDate = "";
        private string _WorkEndDate = "";
        private string _WorkStartTime = "";
        private string _WorkEndTime = "";

        private string _CreateUserID = "";
        

        public float JobID
        {
            get { return _JobID; }
            set { _JobID = value; }
        }

        public int SplitSeq
        {
            get { return _SplitSeq; }
            set { _SplitSeq = value; }
        }

        public string WorkPersonID
        {
            get { return _WorkPersonID; }
            set { _WorkPersonID = value; }
        }

        public double WorkQty
        {
            get { return _WorkQty; }
            set { _WorkQty = value; }
        }

        public string ScanDate
        {
            get { return _ScanDate; }
            set { _ScanDate = value; }
        }

        public string ScanTime
        {
            get { return _ScanTime; }
            set { _ScanTime = value; }
        }

        public string WorkStartDate
        {
            get { return _WorkStartDate; }
            set { _WorkStartDate = value; }
        }

        public string WorkEndDate
        {
            get { return _WorkEndDate; }
            set { _WorkEndDate = value; }
        }

        public string WorkStartTime
        {
            get { return _WorkStartTime; }
            set { _WorkStartTime = value; }
        }

        public string WorkEndTime
        {
            get { return _WorkEndTime; }
            set { _WorkEndTime = value; }
        }

        public string CreateUserID
        {
            get { return _CreateUserID; }
            set { _CreateUserID = value; }
        }

        public Sub_TWkResult_SplitAdd()
        {
            this.JobID = 0;
            this.SplitSeq = 0;
            this.WorkPersonID = "";
            this.WorkQty = 0;
            this.ScanDate = "";

            this.ScanTime = "";
            this.WorkStartDate = "";
            this.WorkEndDate = "";
            this.WorkStartTime = "";
            this.WorkEndTime = "";

            this.CreateUserID = "";
           
        }
        public void DataClear()
        {
            this.JobID = 0;
            this.SplitSeq = 0;
            this.WorkPersonID = "";
            this.WorkQty = 0;
            this.ScanDate = "";

            this.ScanTime = "";
            this.WorkStartDate = "";
            this.WorkEndDate = "";
            this.WorkStartTime = "";
            this.WorkEndTime = "";

            this.CreateUserID = "";

            return;
        }
        public Sub_TWkResult_SplitAdd(float JobID, int SplitSeq, string WorkPersonID, double WorkQty, string ScanDate,
                             string ScanTime, string WorkStartDate, string WorkEndDate, string WorkStartTime, string WorkEndTime,
                             string CreateUserID)
        {
            this.JobID = JobID;
            this.SplitSeq = SplitSeq;
            this.WorkPersonID = WorkPersonID;
            this.WorkQty = WorkQty;
            this.ScanDate = ScanDate;

            this.ScanTime = ScanTime;
            this.WorkStartDate = WorkStartDate;
            this.WorkEndDate = WorkEndDate;
            this.WorkStartTime = WorkStartTime;
            this.WorkEndTime = WorkEndTime;

            this.CreateUserID = CreateUserID;
        }
    }

    #endregion

    #region 태그프린트 클래스
    public class TSCLIB_DLL
    {
        [DllImport("TSCLIB.dll", EntryPoint = "about")]
        public static extern int about();

        [DllImport("TSCLIB.dll", EntryPoint = "openport")]
        public static extern int openport(string printername);

        [DllImport("TSCLIB.dll", EntryPoint = "barcode")]
        public static extern int barcode(string x, string y, string type,
                    string height, string readable, string rotation,
                    string narrow, string wide, string code);

        [DllImport("TSCLIB.dll", EntryPoint = "clearbuffer")]
        public static extern int clearbuffer();

        [DllImport("TSCLIB.dll", EntryPoint = "closeport")]
        public static extern int closeport();

        [DllImport("TSCLIB.dll", EntryPoint = "downloadpcx")]
        public static extern int downloadpcx(string filename, string image_name);

        [DllImport("TSCLIB.dll", EntryPoint = "formfeed")]
        public static extern int formfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "nobackfeed")]
        public static extern int nobackfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "printerfont")]
        public static extern int printerfont(string x, string y, string fonttype,
                        string rotation, string xmul, string ymul,
                        string text);

        [DllImport("TSCLIB.dll", EntryPoint = "printlabel")]
        public static extern int printlabel(string set, string copy);

        [DllImport("TSCLIB.dll", EntryPoint = "sendcommand")]
        public static extern int sendcommand(string printercommand);

        [DllImport("TSCLIB.dll", EntryPoint = "setup")]
        public static extern int setup(string width, string height,
                  string speed, string density,
                  string sensor, string vertical,
                  string offset);

        [DllImport("TSCLIB.dll", EntryPoint = "windowsfont")]
        public static extern int windowsfont(int x, int y, int fontheight,
                        int rotation, int fontstyle, int fontunderline,
                        string szFaceName, string content);

    }
    #endregion

    #region TTag 클래스
    public class TTag
    {
        private string _sTagID = "";
        private string _sTag = "";
        private float _nWidth = 0;
        private float _nHeight = 0;
        private int _nClss = 0;
        private int _nDefectClss = 0;
        private int _nDefHeight = 0;
        private int _nDefBaseY = 0;
        private int _nDefBaseX1 = 0;
        private int _nDefBaseX2 = 0;
        private int _nDefBaseX3 = 0;
        private int _nDefGapY = 0;
        private int _nDefGapX1 = 0;
        private int _nDefGapX2 = 0;
        private int _nDefLength = 0;
        private int _nDefHCount = 0;
        private int _nDefBarClss = 0;
        private int _nGap = 0;
        private string _sDirect = "";


        public string sTagID
        {
            get { return _sTagID; }
            set { _sTagID = value; }
        }
        public string sTag
        {
            get { return _sTag; }
            set { _sTag = value; }
        }

        public float nWidth
        {
            get { return _nWidth; }
            set { _nWidth = value; }
        }

        public float nHeight
        {
            get { return _nHeight; }
            set { _nHeight = value; }
        }

        public int nClss
        {
            get { return _nClss; }
            set { _nClss = value; }
        }

        public int nDefectClss
        {
            get { return _nDefectClss; }
            set { _nDefectClss = value; }
        }

        public int nDefHeight
        {
            get { return _nDefHeight; }
            set { _nDefHeight = value; }
        }

        public int nDefBaseY
        {
            get { return _nDefBaseY; }
            set { _nDefBaseY = value; }
        }

        public int nDefBaseX1
        {
            get { return _nDefBaseX1; }
            set { _nDefBaseX1 = value; }
        }

        public int nDefBaseX2
        {
            get { return _nDefBaseX2; }
            set { _nDefBaseX2 = value; }
        }

        public int nDefBaseX3
        {
            get { return _nDefBaseX3; }
            set { _nDefBaseX3 = value; }
        }

        public int nDefGapY
        {
            get { return _nDefGapY; }
            set { _nDefGapY = value; }
        }

        public int nDefGapX1
        {
            get { return _nDefGapX1; }
            set { _nDefGapX1 = value; }
        }

        public int nDefGapX2
        {
            get { return _nDefGapX2; }
            set { _nDefGapX2 = value; }
        }

        public int nDefLength
        {
            get { return _nDefLength; }
            set { _nDefLength = value; }
        }

        public int nDefHCount
        {
            get { return _nDefHCount; }
            set { _nDefHCount = value; }
        }

        public int nDefBarClss
        {
            get { return _nDefBarClss; }
            set { _nDefBarClss = value; }
        }

        public int nGap
        {
            get { return _nGap; }
            set { _nGap = value; }
        }

        public string sDirect
        {
            get { return _sDirect; }
            set { _sDirect = value; }
        }

        public TTag()
        {
            this.sTagID = "";
            this.sTag = "";
            this.nWidth = 0;
            this.nHeight = 0;
            this.nClss = 0;
            this.nDefectClss = 0;
            this.nDefHeight = 0;
            this.nDefBaseY = 0;
            this.nDefBaseX1 = 0;
            this.nDefBaseX2 = 0;
            this.nDefBaseX3 = 0;
            this.nDefGapY = 0;
            this.nDefGapX1 = 0;
            this.nDefGapX2 = 0;
            this.nDefLength = 0;
            this.nDefHCount = 0;
            this.nDefBarClss = 0;
            this.nGap = 0;
            this.sDirect = "";


        }
        public void DataClear()
        {
            this.sTagID = "";
            this.sTag = "";
            this.nWidth = 0;
            this.nHeight = 0;
            this.nClss = 0;
            this.nDefectClss = 0;
            this.nDefHeight = 0;
            this.nDefBaseY = 0;
            this.nDefBaseX1 = 0;
            this.nDefBaseX2 = 0;
            this.nDefBaseX3 = 0;
            this.nDefGapY = 0;
            this.nDefGapX1 = 0;
            this.nDefGapX2 = 0;
            this.nDefLength = 0;
            this.nDefHCount = 0;
            this.nDefBarClss = 0;
            this.nGap = 0;
            this.sDirect = "";

            return;
        }
    }
    #endregion

    #region TTagSub 클래스
    public class TTagSub
    {
        private string _sName = "";
        private int _nType = 0;
        private int _nTypeSub = 0;
        private int _nAlign = 0;
        private int _x = 0;
        private int _y = 0;
        private int _nFont = 0;
        private int _nLength = 0;
        private int _nHMulti = 0;
        private int _nVMulti = 0;
        private int _nRotation = 0;
        private string _sText = "";
        private int _nSpace = 0;
        private int _nRelation = 0;
        private int _nPrevItem = 0;
        private int _nBarType = 0;
        private int _nBarHeight = 0;
        private int _nFigureWidth = 0;
        private int _nFigureHeight = 0;
        private int _nThickness = 0;
        private string _sImageFile = "";
        private int _nWidth = 0;
        private int _nHeight = 0;
        private int _nVisible = 0;
        private string _sFontName = "";
        private string _sFontStyle = "";
        private string _sFontUnderLine = "";


        public string sName
        {
            get { return _sName; }
            set { _sName = value; }
        }

        public int nType
        {
            get { return _nType; }
            set { _nType = value; }
        }

        public int nTypeSub
        {
            get { return _nTypeSub; }
            set { _nTypeSub = value; }
        }


        public int nAlign
        {
            get { return _nAlign; }
            set { _nAlign = value; }
        }

        public int x
        {
            get { return _x; }
            set { _x = value; }
        }

        public int y
        {
            get { return _y; }
            set { _y = value; }
        }

        public int nFont
        {
            get { return _nFont; }
            set { _nFont = value; }
        }

        public int nLength
        {
            get { return _nLength; }
            set { _nLength = value; }
        }

        public int nHMulti
        {
            get { return _nHMulti; }
            set { _nHMulti = value; }
        }

        public int nVMulti
        {
            get { return _nVMulti; }
            set { _nVMulti = value; }
        }

        public int nRotation
        {
            get { return _nRotation; }
            set { _nRotation = value; }
        }

        public string sText
        {
            get { return _sText; }
            set { _sText = value; }
        }

        public int nSpace
        {
            get { return _nSpace; }
            set { _nSpace = value; }
        }

        public int nRelation
        {
            get { return _nRelation; }
            set { _nRelation = value; }
        }

        public int nPrevItem
        {
            get { return _nPrevItem; }
            set { _nPrevItem = value; }
        }

        public int nBarType
        {
            get { return _nBarType; }
            set { _nBarType = value; }
        }

        public int nBarHeight
        {
            get { return _nBarHeight; }
            set { _nBarHeight = value; }
        }

        public int nFigureWidth
        {
            get { return _nFigureWidth; }
            set { _nFigureWidth = value; }
        }

        public int nFigureHeight
        {
            get { return _nFigureHeight; }
            set { _nFigureHeight = value; }
        }

        public int nThickness
        {
            get { return _nThickness; }
            set { _nThickness = value; }
        }

        public string sImageFile
        {
            get { return _sImageFile; }
            set { _sImageFile = value; }
        }

        public int nWidth
        {
            get { return _nWidth; }
            set { _nWidth = value; }
        }

        public int nHeight
        {
            get { return _nHeight; }
            set { _nHeight = value; }
        }

        public int nVisible
        {
            get { return _nVisible; }
            set { _nVisible = value; }
        }

        public string sFontName
        {
            get { return _sFontName; }
            set { _sFontName = value; }
        }
        public string sFontStyle
        {
            get { return _sFontStyle; }
            set { _sFontStyle = value; }
        }
        public string sFontUnderLine
        {
            get { return _sFontUnderLine; }
            set { _sFontUnderLine = value; }
        }

        public TTagSub()
        {
            this.sName = "";
            this.nType = 0;
            this.nAlign = 0;
            this.x = 0;
            this.y = 0;
            this.nFont = 0;
            this.nLength = 0;
            this.nHMulti = 0;
            this.nVMulti = 0;
            this.nRotation = 0;
            this.sText = "";
            this.nSpace = 0;
            this.nRelation = 0;
            this.nPrevItem = 0;
            this.nBarType = 0;
            this.nBarHeight = 0;
            this.nFigureWidth = 0;
            this.nFigureHeight = 0;
            this.nThickness = 0;
            this.sImageFile = "";
            this.nWidth = 0;
            this.nHeight = 0;
            this.nVisible = 0;
            this.nTypeSub = 0;
            this.sFontName = "";
            this.sFontStyle = "";
            this.sFontUnderLine = "";
        }
        public void DataClear()
        {
            this.sName = "";
            this.nType = 0;
            this.nAlign = 0;
            this.x = 0;
            this.y = 0;
            this.nFont = 0;
            this.nLength = 0;
            this.nHMulti = 0;
            this.nVMulti = 0;
            this.nRotation = 0;
            this.sText = "";
            this.nSpace = 0;
            this.nRelation = 0;
            this.nPrevItem = 0;
            this.nBarType = 0;
            this.nBarHeight = 0;
            this.nFigureWidth = 0;
            this.nFigureHeight = 0;
            this.nThickness = 0;
            this.sImageFile = "";
            this.nWidth = 0;
            this.nHeight = 0;
            this.nVisible = 0;
            this.sFontName = "";
            this.sFontStyle = "";
            this.sFontUnderLine = "";


            return;
        }
    }
    #endregion

    #region 태그프린트 클래스
    public class Zebra_Lib
    {
        [DllImport("TSCLIB.dll", EntryPoint = "about")]
        public static extern int about();

        [DllImport("TSCLIB.dll", EntryPoint = "openport")]
        public static extern int openport(string printername);

        [DllImport("TSCLIB.dll", EntryPoint = "barcode")]
        public static extern int barcode(string x, string y, string type,
                    string height, string readable, string rotation,
                    string narrow, string wide, string code);

        [DllImport("TSCLIB.dll", EntryPoint = "clearbuffer")]
        public static extern int clearbuffer();

        [DllImport("TSCLIB.dll", EntryPoint = "closeport")]
        public static extern int closeport();

        [DllImport("TSCLIB.dll", EntryPoint = "downloadpcx")]
        public static extern int downloadpcx(string filename, string image_name);

        [DllImport("TSCLIB.dll", EntryPoint = "formfeed")]
        public static extern int formfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "nobackfeed")]
        public static extern int nobackfeed();

        [DllImport("TSCLIB.dll", EntryPoint = "printerfont")]
        public static extern int printerfont(string x, string y, string fonttype,
                        string rotation, string xmul, string ymul,
                        string text);

        [DllImport("TSCLIB.dll", EntryPoint = "printlabel")]
        public static extern int printlabel(string set, string copy);

        [DllImport("TSCLIB.dll", EntryPoint = "sendcommand")]
        public static extern int sendcommand(string printercommand);

        [DllImport("TSCLIB.dll", EntryPoint = "setup")]
        public static extern int setup(string width, string height,
                  string speed, string density,
                  string sensor, string vertical,
                  string offset);

        [DllImport("TSCLIB.dll", EntryPoint = "windowsfont")]
        public static extern int windowsfont(int x, int y, int fontheight,
                        int rotation, int fontstyle, int fontunderline,
                        string szFaceName, string content);

    }
    #endregion

    public class EnumItem
    {
        public static int IO_DATA = 0;
        public static int IO_BARCODE = 1;
        public static int IO_TEXT = 2;
        public static int IO_LINE = 3;
        public static int IO_RECT = 4;
        public static int IO_DIAMOND = 5;
        public static int IO_CIRCLE = 6;
        public static int IO_IMAGE = 7;
        public static int IO_QRcode = 8;
        public static int IO_BOX = 9;
    }

    public class GlobalVar
    {
        private string _queryCount;
        private string _ProcessName;
        private string _ProcessID;
        private string _TeamID;
        private string _TeamName;
        private string _PersonID;
        private string _PersonName;
        private string _InstID;
        private string _MachineID;
        private string _MachineName;
        private string _MoldName;
        private string _MoldID;

        public void SetStbInfo()
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm.Name == "Frm_tprc_Main")
                {
                    Frm_tprc_Main main = (Frm_tprc_Main)openForm;
                    main.stsInfo_Team.Text = _TeamName;
                    main.stsInfo_Team.Tag = _TeamID;
                    main.stsInfo_ProMac.Text = _MachineName;
                    main.stsInfo_ProMac.Tag = _MachineID;
                    main.stsInfo_Mold.Text = _MoldName;
                    main.stsInfo_Mold.Tag = _MoldID;
                    main.stsInfo_Person.Tag = _PersonID;
                    main.stsInfo_Person.Text = _PersonName;

                    if (_queryCount == "")
                    {
                        main.stsInfo_Msg.Text = "";
                    }
                    else
                    {
                        main.stsInfo_Msg.Text = _queryCount + "건의 자료가 검색되었습니다.";
                    }
                    break;
                }
            }
        }

        public string queryCount
        {
            get { return _queryCount; }
            set { _queryCount = value; }
        }

        public string ProcessID
        {
            get { return _ProcessID; }
            set { _ProcessID = value; }
        }
        public string ProcessName
        {
            get { return _ProcessName; }
            set { _ProcessName = value; }
        }
        public string TeamID
        {
            get { return _TeamID; }
            set { _TeamID = value; }
        }
        public string TeamName
        {
            get { return _TeamName; }
            set { _TeamName = value; }
        }
        public string PersonID
        {
            get { return _PersonID; }
            set { _PersonID = value; }
        }
        public string PersonName
        {
            get { return _PersonName; }
            set { _PersonName = value; }
        }
        public string InstID
        {
            get { return _InstID; }
            set { _InstID = value; }
        }
        public string MachineID
        {
            get { return _MachineID; }
            set { _MachineID = value; }
        }

        public string MachineName
        {
            get { return _MachineName; }
            set { _MachineName = value; }
        }
        public string MoldName
        {
            get { return _MoldName; }
            set { _MoldName = value; }
        }
        public string MoldID
        {
            get { return _MoldID; }
            set { _MoldID = value; }
        }
    }
}

public class TagPrint
{
    public const float DPI_RATIO = 0.8F;
    public const int GAP_X = 4;
    public const int GAP_Y = 2;

    public string g_sLabelFontName//              라벨 출력용 폰트 이름
    {
        get
        {
            INI_GS ini = new INI_GS();
            string _g_sLabelFontName = ini.GetValue("LABEL", "FontName", "굴림체");
            return _g_sLabelFontName;
        }
    }

    public int g_sLabelFontStyle//             라벨 출력폰트 스타일(0:기본,1:기울임,2:굵게)
    {
        get
        {
            INI_GS ini = new INI_GS();
            string _g_sLabelFontStyle = ini.GetValue("LABEL", "FontStyle", "2");
            return int.Parse(_g_sLabelFontStyle);
        }
    }

    public int g_sLabelFontUnderLine//          라벨 출력폰트 밑줄 여부(0:없음,1:밑줄 표시)
    {
        get
        {
            INI_GS ini = new INI_GS();
            string _g_sLabelFontStyle = ini.GetValue("LABEL", "FontUnderLine", "0");
            return int.Parse(_g_sLabelFontStyle);
        }
    }




    public int GetCleverFontGapY(int nSize)
    {
        int GapY = 0;
        switch (nSize)
        {
            case 1:
                GapY = 0;
                break;
            case 2:
                GapY = 7;
                break;
            case 3:
                GapY = 9;
                break;
            case 4:
                GapY = 13;
                break;
            case 5:
                GapY = 18;
                break;
        }
        return GapY;
    }

    public int GetCleverFontDot(int nSize)
    {
        int FontDot = 0;
        switch (nSize)
        {
            case 1:
                FontDot = 28;
                break;
            case 2:
                FontDot = 40;
                break;
            case 3:
                FontDot = 48;
                break;
            case 4:
                FontDot = 64;
                break;
            case 5:
                FontDot = 94;
                break;
        }
        return FontDot;
    }

    public int GetCleverHangulFontSize(int nSize)
    {
        int HangulFontSize = 0;
        switch (nSize)
        {
            case 1:
                HangulFontSize = 15;
                break;
            case 2:
                HangulFontSize = 24;
                break;
            case 3:
                HangulFontSize = 30;
                break;
            case 4:
                HangulFontSize = 40;
                break;
            case 5:
                HangulFontSize = 60;
                break;
        }
        return HangulFontSize;
    }

    
}

#region 대구 산업 : 라벨 리스트 : 둘리

public class ChildLabel
{
    private string _LabelID = "";
    private string _ArticleID = "";
    private string _Article = "";
    private string _BuyerArticleNo = "";
    private float _ReaQty = 0;
    private float _LocRemainQty = 0;
    private float _NeedQty = 0; // 필요 재고량
    private float _DeficiencyQty = 0; // 부족량
    private float _WorkQty = 0; // 생산하려는 양
    private string _UnitClss = ""; // 생산하려는 양

    public ChildLabel() { }

    public ChildLabel(string LabelID, string ArticleID, string Article, string BuyerArticleNo, float ReaQty, float LocRemainQty, float WorkQty)
    {
        this._LabelID = LabelID;
        this._ArticleID = ArticleID;
        this._Article = Article;
        this._BuyerArticleNo = BuyerArticleNo;

        this._WorkQty = WorkQty;
        this._ReaQty = ReaQty;
        this._LocRemainQty = LocRemainQty;

        this._NeedQty = WorkQty * ReaQty;
        this._DeficiencyQty = NeedQty - LocRemainQty;
        if (this._DeficiencyQty < 0)
        {
            this._DeficiencyQty = 0;
        }
    }

    public string UnitClss
    {
        get { return _UnitClss; }
        set { _UnitClss = value; }
    }

    public string LabelID
    {
        get { return _LabelID; }
        set { _LabelID = value; }
    }

    public string ArticleID
    {
        get { return _ArticleID; }
        set { _ArticleID = value; }
    }

    public string Article
    {
        get { return _Article; }
        set { _Article = value; }
    }

    public string BuyerArticleNo
    {
        get { return _BuyerArticleNo; }
        set { _BuyerArticleNo = value; }
    }

    public float WorkQty
    {
        get { return _WorkQty; }
        set { _WorkQty = value; }
    }

    public float ReaQty
    {
        get { return _ReaQty; }
        set { _ReaQty = value; }
    }

    public float LocRemainQty
    {
        get { return _LocRemainQty; }
        set { _LocRemainQty = value; }
    }

    public float NeedQty
    {
        get { return _NeedQty; }
        set { _NeedQty = value; }
    }

    public float DeficiencyQty
    {
        get { return _DeficiencyQty; }
        set { _DeficiencyQty = value; }
    }
}

#endregion


//public class WizWorkLib
//{
//    // int형 숫자 문자열에서 숫자만 추출하기 위한 함수 ex)콤마제거
//    public int OnlyNumber(string strNumber)
//    {
//        string strTarget = CheckNum(strNumber);
//        string strTmp = Regex.Replace(strTarget, @"\D", "");
//        int nTmp = int.Parse(strTmp);
//        return nTmp;
//    }
//    //17.12.22 TextBox 숫자만 입력(소수점)
//    public static void TypingOnlyNumber(object sender, KeyPressEventArgs e, bool includePoint, bool includeMinus)
//    {
//        bool isValidInput = false;
//        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != 5)
//        {
//            if (includeMinus == true) { if (e.KeyChar == '-') isValidInput = true; }
//            if (includePoint == true) { if (e.KeyChar == '.') isValidInput = true; }

//            if (isValidInput == false) e.Handled = true;
//        }

//        if (includePoint == true)
//        {
//            if (e.KeyChar == '.' && (string.IsNullOrEmpty((sender as TextBox).Text.Trim()) || (sender as TextBox).Text.IndexOf('.') > -1)) e.Handled = true;
//        }

//        if (includeMinus == true)
//        {
//            if (e.KeyChar == '-' && (string.IsNullOrEmpty((sender as TextBox).Text.Trim()) || (sender as TextBox).Text.IndexOf('-') > -1)) e.Handled = true;
//        }

//        //(sender as TextBox).MaxLength = 8;
//    }

//    public string GetDefaultPrinter()
//    {
//        PrinterSettings settings = new PrinterSettings();
//        foreach (string printer in PrinterSettings.InstalledPrinters)
//        {
//            settings.PrinterName = printer;
//            //if (settings.IsDefaultPrinter) 기본 프린트일때
//            if (printer.Contains("TSC"))
//            {
//                return printer;
//            }

//        }
//        return string.Empty;
//    }
//    //날짜 타입 변환함수에 사용
//    private const int DF_FULL = 0;        //yyyy년 mm월 dd일
//    private const int DF_LONG = 1;        //yyyy-mm-dd
//    private const int DF_SHORT = 2;       //mm/dd
//    private const int DF_MID = 3;         //yy-mm-dd
//    private const int DF_MD = 4;          //yyyymmdd

//    [DllImport("kernel32", CharSet = CharSet.Auto)]
//    public static extern Int32 GetComputerName(String Buffer, ref Int32 BufferLength);

//    public string UserIPAddress
//    {
//        get
//        {
//            IPHostEntry IPHost = Dns.GetHostByName(Dns.GetHostName());

//            string _UserIPAddress = IPHost.AddressList[0].ToString();

//            return _UserIPAddress;
//        }

//    }


//    //Null값 체크
//    public string CheckNull(string Value)
//    {
//        if (String.IsNullOrEmpty(Value) == false)
//        {
//            return Value.Trim();
//        }
//        else
//        {
//            return "";
//        }
//    }

//    public string CheckNum(string Value)
//    {
//        if (WizWorkLib.IsNumeric(Value) && Value != "" && WizWorkLib.IsNumeric(Value) && Value != "0")
//        {
//            return string.Format("{0:#,###}", Value);
//        }
//        else
//        {
//            return "0";
//        }
//    }



//    public string MakeDateTime(string strType, string Date)
//    {
//        string value = "";
//        if (Date.Length <= 0)
//        {
//            return "";
//        }
//        if (strType.ToUpper() == "YYYYMMDD")
//        {
//            value = Date.Substring(0, 4) + "-" + Date.Substring(4, 2) + "-" + Date.Substring(6, 2);               //일자

//        }
//        else if (strType.ToUpper() == "YYMMDD")
//        {
//            value = Date.Substring(0, 2) + "-" + Date.Substring(2, 2) + "-" +Date.Substring(4, 2);
//        }
//        else if (strType.ToUpper() == "HHMM")
//        {
//            value = Date.Substring(0, 2) + ":" + Date.Substring(2, 2);
//        }
//        else if (strType.ToUpper() == "HHMMSS")
//        {
//            value = Date.Substring(0, 2) + ":" + Date.Substring(2, 2) + ":" + Date.Substring(4, 2);
//        }
//        return value;
//    }

//    //날짜타입함수
//    public string MakeDate(int iFormat, string sDate)
//    {
//        string functionReturnValue = "";
//        IFormatProvider KR_Format = new System.Globalization.CultureInfo("ko-KR", true);
//        DateTime dt;
//        if (sDate.Length == 6)
//        {
//            sDate = sDate + "01";
//        }

//        dt = DateTime.ParseExact(sDate, "yyyyMMdd", KR_Format);

//        if (sDate.Length == 8)
//        {
//            switch (iFormat)
//            {
//                case DF_FULL:
//                    functionReturnValue = string.Format("{0:yyyy년 MM월 dd일}", dt);
//                    break;
//                case DF_LONG:
//                    functionReturnValue = string.Format("{0:d}", dt);
//                    break;
//                case DF_SHORT:
//                    functionReturnValue = string.Format("{0:MM/dd}", dt);
//                    break;
//                case DF_MID:
//                    functionReturnValue = string.Format("{0:yy-MM-dd}", dt);
//                    break;
//                case DF_MD:
//                    functionReturnValue = string.Format("{0:yyyy-MM-dd}", dt);
//                    break;
//            }
//        }
//        else
//        {
//            functionReturnValue = "";
//        }
//        return functionReturnValue;
//    }

//    #region DataCheck

//    public static bool IsNumeric(string value)
//    {
//        value = value.Replace("-", "");
//        value = value.Replace(",", "");
//        if (value == "")
//        {
//            return true;
//        }

//        foreach (char _char in value)
//        {
//            if (_char != '.')
//            {
//                if (!Char.IsNumber(_char))
//                    return false;
//            }
//        }
//        return true;
//    }
//    #endregion

//           /// <summary>
//        /// 문자열 왼쪽편처음부터 지정된 문자열값 리턴(VBScript Left기능)
//        /// </summary>
//        /// <param name="target">얻을 문자열</param>
//        /// <param name="length">얻을 문자열길이</param>
//        /// <returns>얻은 문자열 값</returns>
//        public static string Left(string target, int length)
//        {
//            if (length <= target.Length)
//            {
//                return target.Substring(0, length);
//            }
//            return target;
//        }
//        /// <summary>
//        /// 지정된 위치이후 모든 문자열 리턴 (VBScript Mid기능)
//        /// </summary>
//        /// <param name="target">얻을 문자열</param>
//        /// <param name="start">얻을 시작위치</param>
//        /// <returns>지정된 위치 이후 모든 문자열리턴</returns>
//        public static string Mid(string target, int start)
//        {
//            if (start <= target.Length)
//            {
//                return target.Substring(start - 1);
//            }
//            return string.Empty;
//        }
//        /// <summary>
//        /// 문자열이 지정된 위치에서 지정된 길이만큼까지의 문자열 리턴 (VBScript Mid기능)
//        /// </summary>
//        /// <param name="target">얻을 문자열</param>
//        /// <param name="start">얻을 시작위치</param>
//        /// <param name="length">얻을 문자열길이</param>
//        /// <returns>지정된 길이만큼의 문자열 리턴</returns>
//        public static string Mid(string target, int start, int length)
//        {
//            if (start <= target.Length)
//            {
//                if (start + length - 1 <= target.Length)
//                {
//                    return target.Substring(start - 1, length);
//                }
//                return target.Substring(start - 1);
//            }
//            return string.Empty;
//        }
//        /// <summary>
//        /// 문자열 오른쪽편처음부터 지정된 문자열값 리턴(VBScript Right기능) 
//        /// </summary>
//        /// <param name="target">얻을 문자열</param>
//        /// <param name="length">얻을 문자열길이</param>
//        /// <returns>얻은 문자열 값</returns>
//        public static string Right(string target, int length)
//        {
//            if (length <= target.Length)
//            {
//                return target.Substring(target.Length - length);
//            }
//            return target;
//        }


//    #region Query 성공여부 bool값 false면 성공 true면 실패

//    public bool IsOKQuery(string[] strMessage)
//    {
//        bool IsOK = true;
//        foreach (var str in strMessage)
//        {
//            if (str == "success")
//            {
//                IsOK = false;
//            }
//        }
//        return IsOK;
//    }

//    #endregion

//}

