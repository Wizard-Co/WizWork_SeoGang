using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Net;
using Common;
using System.Text.RegularExpressions;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.Data;
using System.Drawing;




namespace Common
{

    #region TBaseSpec 클래스
    public class TBaseSpec
    {
        //private string _OrderID = "";            // ' 관리번호
        //private string _OrderNO = "";            // ' Order NO
        //private string _Custom = "";            // ' 거래처 명
        //private int _OrderQty = 0;             // ' 수주수량
        //private string _OrderUnit = "";            // ' 수주 단위
        //private int _OrderSeq = 0;             // ' 오더 SEQ
        //private string _Article = "";             // ' Article
        ////'-------------------------------------------
        //private string _ProcessID = "";            // ' 공정ID
        //private string _Process = "";              // ' 공정명
        //private string _ProcessCode = "";         // ' 공정 코드
        //private int _PlanProcSeq = 0;            // ' 가공순위
        //private string _ProcessChildCheckYN = ""; // ' 하위 Check 여부
        //private string _AutoGatheringYN = "";      // ' 자동 통신데이타 수집건 인지 확인
        ////'-------------------------------------------
        //private string _BoxID = "";                // ' Box 번호
        //private string _CardID = "";              // ' Card 번호
        //private string _SplitID = "";              // ' 카드분할번호
        //private int _WorkSeq = 0;              // ' 가공순위
        //private string _MachineID = "";            // ' 기계호기
        ////'-------------------------------------------
        //private string _ColorID = "";              // ' 색상 코드
        //private string _Color = "";                // ' 색상
        //private int _ColorQty = 0;              // ' 색상 수량
        ////'-------------------------------------------
        //private string _TeamID = "";             // ' 작업조 코드
        //private string _Team = "";             // ' 작업조
        //private string _PersonID = "";             // ' 작업자 코드
        //private string _Person = "";             // ' 작업자
        //private string _WorkNO = "";             // ' 공정 호기
        //private string _TagID = "";             // ' 선택태그ID (001, 002, ...)

        ////'-------------------------------------------
        ////' 생산시점 불량 check
        ////'-------------------------------------------
        //private int _DefectCnt = 0;            // ' 생산중 불량 수량
        //private int _DLoss = 0;            // ' 불량 길이
        //private int _WorkQty = 0;            //  수량
        ////private string _Basis = "";           // ' 검사기준 명
        ////private int _BasisID = 0;            // ' 검사기준 ID
        //private string _QcLot = "";           //

        ////'------------------------------------------
        ////'금형등록
        ////'------------------------------------------
        //private string _sMoldID = "";            // '금형ID,  2016.03.25, 성광 작업시 추가 함
        //private string _sMold = "";            // '금형명,  2016.03.25, 성광 작업시 추가 함
        //private string _sInstID = "";            // '작업지시ID, 2016.05.02, 성광 작업시 추가 함
        //private string _sLotID = "";            // 'LotID , 2016.05.4, 성광 작업시 추가함


        //WizIns용 전역변수 추가 / 180423 
        private string _OrderID = "";            // ' 관리번호
        private string _OrderNo = "";
        private string _Custom = "";
        private string _Article = "";
        private string _OrderQty = "";
        private string _UnitClss = "";
        private string _InspectQty = "";
        private string _InspectRoll = "";
        private string _PassQty = "";
        private string _PassRoll = "";
        private string _DefectQty = "";
        private string _DefectRoll = "";
        private string _OrderSeq = "";
        private string _DesignNo = "";
        private string _Color = "";
        private string _ColorQty = "";
        private string _InspectSubQty = "";
        private string _InspectSubRoll = "";

        private string _PassSubQty = "";
        private string _PassSubRoll = "";
        private string _DefectSubQty = "";
        private string _DefectSubRoll = "";
        private string _DepartID = "";
        private string _TeamID = "";
        private string _Team = "";
        private string _PersonID = "";
        private string _Person = "";
        private string _RNK = "";
        private string _ExamNO = "";

        private string _RollClss = "";
        private string _SRollNo = "";

        private string _BasisID = "";
        private string _Basis = "";
        private string _BasisUnit = "";

        private string _ExamNoInsepectRoll = "";
        private string _ExamNoInsepectQty = "";
        private string _ExamNoPassRoll = "";
        private string _ExamNoPassQty = "";
        private string _ExamNoDefectRoll = "";
        private string _ExamNoDefectQty = "";

        private string _ProcessID = "";
        private string _Process = "";
        private string _ProcessCode = "";
        private string _PlanProcSeq = "";
        private string _ProcessChildCheckYN = "";

        private string _BoxID = "";
        private string _MachineID = "";

        private string _LotInsepectRoll = "";
        private string _LotInsepectQty = "";


        private string _LotPassRoll = "";
        private string _LotPassQty = "";
        private string _LotDefectRoll = "";
        private string _LotDefectQty = "";

        private string _QcLot = "";
        private string _Width = "";
        private string _BCutQty = "";
        private string _FCutQty = "";
        private string _CutDefectID = "";
        private string _CutDefectClss = "";
        private string _BSample = "";
        private string _FSample = "";
        private string _Loss = "";
        private string _DLoss = "";
        private string _Density = "";
        private string _DefectCnt = "";
        private string _TagID = "";
        private string _TagName = "";
        private string _TagNo = "";
        private string _InspectType = "";
        private string _InspectTypeName = "";
        private string _ExamDatePrint = "";
        private string _uOrderID = "";
        private string _uRollSeq = "";
        private string _uRollClss = "";

        private string _CardID = "";
        private string _SplitID = "";
        private string _WorkSFlag = "";
        private string _WorkSeq = "";
        private string _WorkInspect = "";

        private string _PassWord = "";
        private string _LabelID = "";
        private string _LabelGubun = "";
        private string _WorkQty = "";
        private string _ExpectDate = "";
        private string _ModelID = "";
        private string _Model = "";
        private string _ItemSpec = "";

        private string _InstID = "";
        private string _ProductDate = "";

        //public string OrderID
        //{
        //    get { return _OrderID; }
        //    set { _OrderID = value; }
        //}
        //public string OrderNO { get; set; }

        //public string Custom
        //{
        //    get { return _Custom; }
        //    set { _Custom = value; }
        //}
        //public int OrderQty
        //{
        //    get { return _OrderQty; }
        //    set { _OrderQty = value; }
        //}
        public string OrderUnit { get; set; }
        //public int OrderSeq
        //{
        //    get { return _OrderSeq; }
        //    set { _OrderSeq = value; }
        //}
        //public string Article
        //{
        //    get { return _Article; }
        //    set { _Article = value; }
        //}
        //public string ProcessID
        //{
        //    get { return _ProcessID; }
        //    set { _ProcessID = value; }
        //}
        //public string Process
        //{
        //    get { return _Process; }
        //    set { _Process = value; }
        //}
        //public string ProcessCode
        //{
        //    get { return _ProcessCode; }
        //    set { _ProcessCode = value; }
        //}   
        //public int PlanProcSeq
        //{
        //    get { return _PlanProcSeq; }
        //    set { _PlanProcSeq = value; }
        //}
        //public string ProcessChildCheckYN
        //{
        //    get { return _ProcessChildCheckYN; }
        //    set { _ProcessChildCheckYN = value; }
        //}
        //public string AutoGatheringYN
        //{
        //    get { return _AutoGatheringYN; }
        //    set { _AutoGatheringYN = value; }
        //}
        //public string BoxID
        //{
        //    get { return _BoxID; }
        //    set { _BoxID = value; }
        //}
        //public string CardID
        //{
        //    get { return _CardID; }
        //    set { _CardID = value; }
        //}
        //public string SplitID
        //{
        //    get { return _SplitID; }
        //    set { _SplitID = value; }
        //}
        //public int WorkSeq
        //{
        //    get { return _WorkSeq; }
        //    set { _WorkSeq = value; }
        //}
        //public string MachineID
        //{
        //    get { return _MachineID; }
        //    set { _MachineID = value; }
        //}
        public string ColorID { get; set; }
        //public string Color
        //{
        //    get { return _Color; }
        //    set { _Color = value; }
        //}
        //public int ColorQty
        //{
        //    get { return _ColorQty; }
        //    set { _ColorQty = value; }
        //}
        //public string TeamID
        //{
        //    get { return _TeamID; }
        //    set { _TeamID = value; }
        //}
        //public string Team
        //{
        //    get { return _Team; }
        //    set { _Team = value; }
        //}
        //public string PersonID
        //{
        //    get { return _PersonID; }
        //    set { _PersonID = value; }
        //}
        //public string Person
        //{
        //    get { return _Person; }
        //    set { _Person = value; }
        //}
        //public string WorkNO
        //{
        //    get { return _WorkNO; }
        //    set { _WorkNO = value; }
        //}
        //public string TagID
        //{
        //    get { return _TagID; }
        //    set { _TagID = value; }
        //}
        //public int DefectCnt
        //{
        //    get { return _DefectCnt; }
        //    set { _DefectCnt = value; }
        //}
        //public int DLoss
        //{
        //    get { return _DLoss; }
        //    set { _DLoss = value; }
        //}
        //public int WorkQty
        //{
        //    get { return _WorkQty; }
        //    set { _WorkQty = value; }
        //}
        //public string Basis
        //{
        //    get { return _Basis; }
        //    set { _Basis = value; }
        //}
        //public int BasisID
        //{
        //    get { return _BasisID; }
        //    set { _BasisID = value; }
        //}
        //public string QcLot
        //{
        //    get { return _QcLot; }
        //    set { _QcLot = value; }
        //}
        //public string sMoldID
        //{
        //    get { return _sMoldID; }
        //    set { _sMoldID = value; }
        //}
        //public string sMold
        //{
        //    get { return _sMold; }
        //    set { _sMold = value; }
        //}
        public string sInstID { get; set; }

        public string sLotID { get; set; }
        public string sInstDetSeq { get; set; }

        public string OrderID { get; set; }//   ' 관리번호
        public string OrderNo { get; set; }  //   ' Order NO
        public string Custom { get; set; }  //  ' 거래처 명
        public string Article { get; set; }  //   ' 품명
        public string OrderQty { get; set; }  //      ' 수주수량
        public string UnitClss { get; set; }  //     ' 수주 단위
        public string InspectQty { get; set; }  //        ' 수주별 검사수량
        public string InspectRoll { get; set; }  //      ' 수주별 검사절수
                                                 // 'S_201203_조일_04 에 의한 추가------------------
        public string PassQty { get; set; }        //           ' 수주별 합격수량
        public string PassRoll { get; set; }    //         ' 수주별 합격절수
        public string DefectQty { get; set; }//        ' 수주별 불합격수량
        public string DefectRoll { get; set; }//      ' 수주별 불합격절수
                                              //'-------------------------------------------
        public string OrderSeq { get; set; }//       ' 색상 코드
        public string DesignNo { get; set; }//      ' Design No
        public string Color { get; set; }//   ' 색상명
        public string ColorQty { get; set; }    //       ' 색상 수량
        public string InspectSubQty { get; set; } //    ' 색상별 검사수량
        public string InspectSubRoll { get; set; }//   ' 색상별 검사절수
                                                  //'S_201203_조일_04 에 의한 추가------------------
        public string PassSubQty { get; set; }  //       ' 색상별 합격수량
        public string PassSubRoll { get; set; }  //      ' 색상별 합격절수
        public string DefectSubQty { get; set; } //     ' 색상별 불합격수량
        public string DefectSubRoll { get; set; } //   ' 색상별 불합격절수
                                                  // '-------------------------------------------
        public string DepartID { get; set; }  //    ' 부서 코드
        public string TeamID { get; set; } //   ' 검사조 코드
        public string Team { get; set; } // ' 검사조
        public string PersonID { get; set; }//     ' 검사자 코드
        public string Person { get; set; }//   ' 검사자
        public string RNK { get; set; }//' 라벨 발행시 사용
        public string ExamNO { get; set; }//   ' 검사 호기
                                          // '-------------------------------------------
        public string RollClss { get; set; } //       ' 절번호 부여방법 : 0 - 오더별 1 - 오더 색상 2 - 오더 색상 로트
        public string SRollNo { get; set; }//      ' 시작절번호
                                           // '-------------------------------------------  ' 색상별 절번호일경우 시작 절번호를 받는다
        public string BasisID { get; set; }//   As EBasisID     ' 검사기준
        public string Basis { get; set; }//    ' 기준 명칭
        public string BasisUnit { get; set; } //As EUnit        ' 검사기준단위
                                              //'-------------------------------------------
        public string ExamNoInsepectRoll { get; set; }//      ' 금일 호기별 검사절수         'S_201203_조일_04 에 의한 수정(OLD:ExamNoRoll)
        public string ExamNoInsepectQty { get; set; } //    ' 금일 호기별 검사수량(Y변환)   'S_201203_조일_04 에 의한 수정(OLD:ExamNoQty)

        //'S_201203_조일_04 에 의한 추가--//----------------
        public string ExamNoPassRoll { get; set; }       //      ' 금일 호기별 합격절수
        public string ExamNoPassQty { get; set; }       //    ' 금일 호기별 합격수량(Y변환)
        public string ExamNoDefectRoll { get; set; }          // //      ' 금일 호기별 불합격절수
        public string ExamNoDefectQty { get; set; }       //    ' 금일 호기별 불합격수량(Y변환)


        // '-------------------------------------------
        public string ProcessID { get; set; } //* 4   ' 공정ID
        public string Process { get; set; }                  //   ' 공정명
        public string ProcessCode { get; set; }                  //As EPROCESSCODE ' 공정 코드
        public string PlanProcSeq { get; set; }                 //      ' 가공순위
        public string ProcessChildCheckYN { get; set; }              //   ' 하위 Check 여부
                                                                     // '-------------------------------------------
        public string BoxID { get; set; } // ' Box 번호
        public string MachineID { get; set; }  //    ' 기계호기
                                               // '-------------------------------------------

        // '--------------------------------------------------------------
        public string LotInsepectRoll { get; set; }//      ' Lot별 검사절수         'S_201203_조일_04 에 의한 수정(OLD:LotRoll)
        public string LotInsepectQty { get; set; } //    ' Lot별 검사수량   'S_201203_조일_04 에 의한 수정(OLD:LotQty)

        //'S_201203_조일_04 에 의한 추가------------------
        public string LotPassRoll { get; set; }          //      ' Lot별 합격절수
        public string LotPassQty { get; set; }          //  ' Lot별  합격수량
        public string LotDefectRoll { get; set; }          //      ' Lot별  불합격절수
        public string LotDefectQty { get; set; }          //  ' Lot별  불합격수량
                                                          // '--------------------------------------------------------------

        //'---------------------------------------------
        public string QcLot { get; set; }      //      ' Lot No
        public string Width { get; set; }      //       ' 원단 폭
        public string BCutQty { get; set; }      //      ' 앞 난단수량
        public string FCutQty { get; set; }      //       ' 뒷 난단수량
        public string CutDefectID { get; set; }  //' 난단 대표불량
        public string CutDefectClss { get; set; }  //  ' 난단 대표불량 불량 구분
        public string BSample { get; set; }          //      ' 앞 견본
        public string FSample { get; set; }          //       ' 뒷 견본
        public string Loss { get; set; }          //       ' 보상
        public string DLoss { get; set; }          //       ' 불량 보상     'S_201201_조일_07 에 의한 추가
        public string StuffQty { get; set; }          //' 투입길이
        public string Density { get; set; }          //      ' 원단 밀도
        public string DefectCnt { get; set; }           //      ' 불량 갯수
                                                        // '---------------------------------------------
        public string TagID { get; set; } //     ' 선택태그ID (001, 002, ...)
        public string TagName { get; set; }         //       ' 선택태그명칭 (Form1, Form2, ...)      ''2014.03.11 추가
        public string TagNo { get; set; }      //      ' Tag 발행수
        public string InspectType { get; set; }     // As EInspectType ' 검사 방법 (0:단순검사, 1:불량검사)
        public string InspectTypeName { get; set; }         //' 검사 방법명칭(단순검사, 불량검사)       '2014.03.11 추가
        public string ExamDatePrint { get; set; }         //
                                                          // '----------------------------------------------
        public string uOrderID { get; set; }   //      ' 검사 수정시 오더
        public string uRollSeq { get; set; }   //      ' 검사 수정시 절 번호
        public string uRollClss { get; set; }   //      ' 검사 수정시 절번호 부여 방법
                                                // '-----------------------------------------------
        public string CardID { get; set; }      //      ' 공정카드 번호
        public string SplitID { get; set; }  //             ' 공정카드 분할 번호
        public string WorkSFlag { get; set; }      //    //      ' 공정작업 시작 구분 0:밀차없이 작업 1:공정시작 2:공정종료
        public string WorkSeq { get; set; }      //     //      ' 공정순서
        public string WorkInspect { get; set; }      //      ' 기존검사작업구분
                                                     //'---------------------------------------------
        public string PassWord { get; set; }//   
        public string LabelID { get; set; }//      'BoxID  , AFT 적용시 추가
        public string LabelGubun { get; set; } //         'LabelGubun , AFT 적용시 추가
        public string WorkQty { get; set; }//      '박스당수량 , AFT 적용시 추가
        public string ExpectDate { get; set; }//    '작업시작일 , AFT 적용시 추가
        public string ModelID { get; set; } //             '차종 , AFT 적용시 추가
        public string Model { get; set; }//   '차종명 , AFT 적용시 추가
        public string ItemSpec { get; set; } //     'Article Spec , AFT 적용시 추가

        public string InstID { get; set; } //     'InstID
        public string ProductDate { get; set; }//     '제조일자, YS 적용시 추가

        public TBaseSpec()
        {
            //OrderID = "";            // ' 관리번호
            //OrderNO = "";            // ' Order NO
            //Custom = "";            // ' 거래처 명
            //OrderQty = 0;             // ' 수주수량
            //OrderUnit = "";            // ' 수주 단위
            //OrderSeq = 0;             // ' 오더 SEQ
            //Article = "";             // ' Article
            //--------------------------
            //ProcessID = "";            // ' 공정ID
            //Process = "";              // ' 공정명
            //ProcessCode = "";         // ' 공정 코드
            //PlanProcSeq = 0;            // ' 가공순위
            //ProcessChildCheckYN = ""; // ' 하위 Check 여부
            //AutoGatheringYN = "";      // ' 자동 통신데이타 수집건 인지 확인
            //--------------------------
            //BoxID = "";                // ' Box 번호
            //CardID = "";              // ' Card 번호
            //SplitID = "";              // ' 카드분할번호
            //WorkSeq = 0;              // ' 가공순위
            //MachineID = "";            // ' 기계호기
            //--------------------------
            //ColorID = "";              // ' 색상 코드
            //Color = "";                // ' 색상
            //ColorQty = 0;              // ' 색상 수량
            //--------------------------
            //TeamID = "";             // ' 작업조 코드
            //Team = "";             // ' 작업조
            //PersonID = "";             // ' 작업자 코드
            //Person = "";             // ' 작업자
            //WorkNO = "";             // ' 공정 호기
            //TagID = "";             // ' 선택태그ID (001, 002, ...)

            //--------------------------
            //DefectCnt = 0;            // ' 생산중 불량 수량
            //DLoss = 0;            // ' 불량 길이
            //WorkQty = 0;            //  수량
            //Basis = "";           // ' 검사기준 명
            //BasisID = 0;            // ' 검사기준 ID
            //QcLot = "";           //
            //-------------------------
            //sMoldID = "";            // '금형ID,  2016.03.25, 성광 작업시 추가 함
            //sMold = "";            // '금형명,  2016.03.25, 성광 작업시 추가 함
            //sInstID = "";            // '작업지시ID, 2016.05.02, 성광 작업시 추가 함
            //sInstDetSeq = "";
            //sLotID = "";            // 'LotID , 2016.05.4, 성광 작업시 추가함
            OrderNo = "";
            Custom = "";
            Article = "";
            OrderQty = "";
            UnitClss = "";
            InspectQty = "";
            InspectRoll = "";
            PassQty = "";
            PassRoll = "";
            DefectQty = "";
            DefectRoll = "";
            OrderSeq = "";
            DesignNo = "";
            Color = "";
            ColorQty = "";
            InspectSubQty = "";
            InspectSubRoll = "";

            PassSubQty = "";
            PassSubRoll = "";
            DefectSubQty = "";
            DefectSubRoll = "";
            DepartID = "";
            TeamID = "";
            Team = "";
            PersonID = "";
            Person = "";
            RNK = "";
            ExamNO = "";

            RollClss = "";
            SRollNo = "";

            BasisID = "";
            Basis = "";
            BasisUnit = "";

            ExamNoInsepectRoll = "";
            ExamNoInsepectQty = "";
            ExamNoPassRoll = "";
            ExamNoPassQty = "";
            ExamNoDefectRoll = "";
            ExamNoDefectQty = "";

            ProcessID = "";
            Process = "";
            ProcessCode = "";
            PlanProcSeq = "";
            ProcessChildCheckYN = "";

            BoxID = "";
            MachineID = "";


            LotInsepectRoll = "";
            LotInsepectQty = "";


            LotPassRoll = "";
            LotPassQty = "";
            LotDefectRoll = "";
            LotDefectQty = "";

            QcLot = "";
            Width = "";
            BCutQty = "";
            FCutQty = "";
            CutDefectID = "";
            CutDefectClss = "";
            BSample = "";
            FSample = "";
            Loss = "";
            DLoss = "";
            Density = "";
            DefectCnt = "";
            TagID = "";
            TagName = "";
            TagNo = "";
            InspectType = "";
            InspectTypeName = "";
            ExamDatePrint = "";
            uOrderID = "";
            uRollSeq = "";
            uRollClss = "";

            CardID = "";
            SplitID = "";
            WorkSFlag = "";
            WorkSeq = "";
            WorkInspect = "";

            PassWord = "";
            LabelID = "";
            LabelGubun = "";
            WorkQty = "";
            ExpectDate = "";
            ModelID = "";
            Model = "";
            ItemSpec = "";

            InstID = "";
            ProductDate = "";
        }
        //public void DataClear()
        //{
        //    OrderID = "";            // ' 관리번호
        //    OrderNO = "";            // ' Order NO
        //    Custom = "";            // ' 거래처 명
        //    OrderQty = 0;             // ' 수주수량
        //    OrderUnit = "";            // ' 수주 단위
        //    OrderSeq = 0;             // ' 오더 SEQ
        //    Article = "";             // ' Article
        //    //--------------------------
        //    ProcessID = "";            // ' 공정ID
        //    Process = "";              // ' 공정명
        //    ProcessCode = "";         // ' 공정 코드
        //    PlanProcSeq = 0;            // ' 가공순위
        //    ProcessChildCheckYN = ""; // ' 하위 Check 여부
        //    AutoGatheringYN = "";      // ' 자동 통신데이타 수집건 인지 확인
        //    //--------------------------
        //    BoxID = "";                // ' Box 번호
        //    CardID = "";              // ' Card 번호
        //    SplitID = "";              // ' 카드분할번호
        //    WorkSeq = 0;              // ' 가공순위
        //    MachineID = "";            // ' 기계호기
        //    //--------------------------
        //    ColorID = "";              // ' 색상 코드
        //    Color = "";                // ' 색상
        //    ColorQty = 0;              // ' 색상 수량
        //    //--------------------------
        //    TeamID = "";             // ' 작업조 코드
        //    Team = "";             // ' 작업조
        //    PersonID = "";             // ' 작업자 코드
        //    Person = "";             // ' 작업자
        //    WorkNO = "";             // ' 공정 호기
        //    TagID = "";             // ' 선택태그ID (001, 002, ...)

        //    //--------------------------
        //    DefectCnt = 0;            // ' 생산중 불량 수량
        //    DLoss = 0;            // ' 불량 길이
        //    WorkQty = 0;            //  수량
        //    Basis = "";           // ' 검사기준 명
        //    BasisID = 0;            // ' 검사기준 ID
        //    QcLot = "";           //
        //    //-------------------------
        //    sMoldID = "";            // '금형ID,  2016.03.25, 성광 작업시 추가 함
        //    sMold = "";            // '금형명,  2016.03.25, 성광 작업시 추가 함
        //    sInstID = "";            // '작업지시ID, 2016.05.02, 성광 작업시 추가 함
        //    sInstDetSeq = "";
        //    sLotID = "";            // 'LotID , 2016.05.4, 성광 작업시 추가함
        //    return;
        //}
    }
    #endregion

    #region TWkResultDefect 클래스

    public class TWkResultDefect
    {
        private int JobID = 0;
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
            JobID = 0;
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
            JobID = 0;
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
        string _WorkStartDate = "";
        string _WorkStartTime = "";
        string _YLabelID = "";


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
            this.UpBurnPlateTemper1 = 0;
            this.UpBurnPlateTemper2 = 0;
            this.WDID = "";
            this.WDNO = "";
            this.WDQty = 0;
            this.WorkEndDate = "";
            this.WorkEndTime = "";
            this.WorkQty = 0;
            this.WorkStartDate = "";
            this.WorkStartTime = "";
            this.YLabelID = "";
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
            this.WorkStartDate = "";
            this.WorkStartTime = "";
            this.YLabelID = "";

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
                         string strWorkStartTime, string strYLabelID)
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
            this.WorkStartDate = strWorkStartDate;
            this.WorkStartTime = strWorkStartTime;
            this.YLabelID = strYLabelID;
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
    }

    public class GlobalVar
    {
        //public delegate void TextEventHandler();    // string을 반환값으로 갖는 대리자를 선언합니다.
        //public event TextEventHandler SendEvent;          // 대리자 타입의 이벤트 처리기를 설정합니다. 
        //System.Windows.Forms.ToolStripStatusLabel toolstr = openForm.
        //StatusStrip stastr = getActiceStatusStrop();

        //public StatusStrip getActiceStatusStrop(){
        //    foreach (Form openForm in Application.OpenForms)
        //    {
        //        if (openForm.Name == "Frm_tprc_Main")
        //        {
        //        }
        //    }
        //    return new StatusStrip();
        //}
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






public class WizWorkLib
{
    public const int ID_ADDNEW = 0;
    public const int ID_UPDATE = 1;
    public const int ID_DELETE = 2;
    public const int ID_SAVE = 3;
    public const int ID_CANCEL = 4;
    public enum CodeTypeClss
    {
        CD_DEPART = 1,       // 부서관리
        CD_DUTY = 2,    // 직책 관리
        CD_TRADE = 3,    // 거래기준 관리
        CD_BASIS = 4,    // 검사기준 관리
        CD_TEAM = 5,    // 작업조 관리
        CD_WIDTH = 6,    // 원단폭 관리
        CD_WORK = 7,    // 가공구분 관리
        CD_LABEL = 8,    // 레벨구분 관리
        CD_BAND = 9,  // 밴드구분 관리
        CD_TAG = 10,    // 태그구분 관리
        CD_FORM = 11,    // 주문형태 관리
        CD_CLASS = 12,    // 주문구분 관리
        CD_KIND = 13,    // 불량구분 관리
        CD_OUTCLSS = 14,    // 출고구분 관리
        CD_BACKCLSS = 15,    // 반품구분 관리
        CD_GRADE = 16,    // 등급 관리
        CD_DEFECT = 17,    //불량 관리
        CD_PROCESS = 18,    //공정관리
        CD_LENGTH = 19    // 필장
    }

    public DataTable GetCode(CodeTypeClss nType)
    {
        string sCode = "";
        switch (Convert.ToInt32(nType))
        {
            case (int)WizWorkLib.CodeTypeClss.CD_DEPART:
                sCode = "Depart";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_DUTY:
                sCode = "Duty";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_TRADE:
                sCode = "Trade";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_BASIS:
                sCode = "Basis";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_TEAM:
                sCode = "Team";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_WIDTH:
                sCode = "StuffWidth";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_WORK:
                sCode = "Work";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_LABEL:
                sCode = "Label";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_BAND:
                sCode = "Band";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_TAG:
                sCode = "Tag";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_FORM:
                sCode = "Form";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_CLASS:
                sCode = "Class";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_KIND:
                sCode = "Kind";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_OUTCLSS:
                sCode = "OutClss";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_BACKCLSS:
                sCode = "BackClss";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_GRADE:
                sCode = "Grade";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_DEFECT:
                sCode = "Defect";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_PROCESS:
                sCode = "Process";
                break;
            case (int)WizWorkLib.CodeTypeClss.CD_LENGTH:
                sCode = "Length";
                break;
        }
        sCode = "xp_Code_s" + sCode;
        DataTable dt = DataStore.Instance.ProcedureToDataTable(sCode, null, false);
        return dt;
    }

    //날짜 타입 변환함수에 사용
    public enum DateTimeClss
    {
        DF_FULL = 0,        //yyyy년 mm월 dd일
        DF_LONG = 1,        //yyyy-mm-dd
        DF_SHORT = 2,       //mm/dd
        DF_MID = 3,         //yy-mm-dd
        DF_MD = 4,          //yyyymmdd
        DF_FD = 5,          //yyyy-mm-dd
    }


    [DllImport("kernel32", CharSet = CharSet.Auto)]
    public static extern Int32 GetComputerName(String Buffer, ref Int32 BufferLength);

    /// <summary>
    /// 테이블레이아웃패널, 열(컬럼)의 수, 행(로우)의 수를 매개변수로 받아서, 
    /// 테이블레이아웃패널의 %비율로 열의 수와 행의수를 변경해줌
    /// </summary>
    /// <param name="tlp"></param>
    /// <param name="Horizontal"></param>
    /// <param name="Vertical"></param>
    public void SetLayout(TableLayoutPanel tlp, int Horizontal, int Vertical)
    {
        while (tlp.ColumnCount != Horizontal)
        {
            if (tlp.ColumnCount > Horizontal)
            {
                tlp.ColumnCount--;
                tlp.ColumnStyles.RemoveAt(tlp.ColumnCount);
            }
            else if (tlp.ColumnCount < Horizontal)
            {
                tlp.ColumnCount++;
                tlp.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            }
        }
        foreach (ColumnStyle style in tlp.ColumnStyles)
        {
            style.SizeType = SizeType.Percent;
            style.Width = 100.0f / tlp.ColumnCount;
        }

        while (tlp.RowCount != Vertical)
        {
            if (tlp.RowCount > Vertical)
            {
                tlp.RowCount--;
                tlp.RowStyles.RemoveAt(tlp.RowCount);
            }
            else if (tlp.RowCount < Vertical)
            {
                tlp.RowCount++;
                tlp.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
            }
        }
        foreach (RowStyle style in tlp.RowStyles)
        {
            style.SizeType = SizeType.Percent;
            style.Height = 100.0f / tlp.RowCount;
        }
    }

    public void AdjustWidthComboBox_DropDown(object sender, EventArgs e)
    {
        var senderComboBox = (ComboBox)sender;
        int width = senderComboBox.DropDownWidth;
        Graphics g = senderComboBox.CreateGraphics();
        Font font = senderComboBox.Font;

        int vertScrollBarWidth = (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

        var itemsList = senderComboBox.Items.Cast<object>().Select(item => item.ToString());

        foreach (string s in itemsList)
        {
            int newWidth = (int)g.MeasureString(s, font).Width + vertScrollBarWidth;

            if (width < newWidth)
            {
                width = newWidth;
            }
        }

        senderComboBox.DropDownWidth = width;
    }


    /// <summary>
    /// 콤보박스를 패널에 맞게 콤보박스사이즈,폰트를 자동조절 해주는 함수
    /// ComboBox의 부모컨트롤이 Panel이어야한다.
    /// DockStyle 사용으로 상위 컨트롤로 패널사용 필수   
    /// 콤보박스 가로 * 세로 최대 길이가 있으므로 미리 테스트해보고 DockStyle 사용여부 결정할것.
    /// </summary>
    /// <param name="cbx"></param>
    /// <param name="ValueMember = ID"></param>
    /// <param name="DisplayMember = Name"></param>
    public void SetComboBox(ComboBox cbx, DataTable dt, List<CB_IDNAME> list_cbx)
    {
        //사이즈 조절
        //cbx.DrawMode = DrawMode.OwnerDrawVariable;
        cbx.DrawMode = DrawMode.OwnerDrawFixed;
        cbx.DropDownStyle = ComboBoxStyle.DropDownList;
        cbx.Dock = DockStyle.Fill;

        if (cbx.Parent is Panel)
        {
            Panel pnl = cbx.Parent as Panel;
            cbx.ItemHeight = pnl.Size.Height;
            while (cbx.Height > pnl.Size.Height)
            {
                cbx.ItemHeight = cbx.ItemHeight - 1;
            }
            cbx.ItemHeight = cbx.ItemHeight - 4;
        }
        //콤보박스 정보를 리스트로 넘긴다.
        if (dt != null)
        {
            int c = 0;
            foreach (DataRow dr in dt.Rows)
            {
                CB_IDNAME ci = new CB_IDNAME();
                ci.cbName = cbx.Name;
                ci.Idx = c;
                ci.ID = dr["ID"].ToString();
                ci.NAME = dr["Name"].ToString();
                list_cbx.Add(ci);
                c++;
            }
        }

        //콤보박스와 CB_IDNAME클래스의 콤보박스이름이 같으면 아이템 추가해라
        if (list_cbx != null)
        {
            foreach (CB_IDNAME ci in list_cbx)
            {
                if (cbx.Name == ci.cbName)
                {
                    cbx.Items.Add(ci.NAME);
                }
            }
        }

        //이벤트 추가
        cbx.DrawItem += new DrawItemEventHandler(ComboBox_DrawItem);

        if (cbx.Items.Count > 0)
        {
            //Item이 하나일때 자동 선택
            if (cbx.Items.Count == 1)
            {
                cbx.SelectedIndex = 0;
            }
            //폰트사이즈 조절
            int i = 0; // while문 3번 돌리기위해서
            while (i < 2)
            {
                AutoFontSize(cbx, cbx.Items[0].ToString());
                i++;
            }
        }


        //DropDown하면서 Item항목을 그릴때 발생
        void ComboBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics; //콤보박스 항목부분의 Graphics객체
            Rectangle rect = e.Bounds;//콤보박스 DropDown부분의 사각형크기

            if (e.Index >= 0) //현재 Item 항목의 Index
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                float size = 0;
                size = ((ComboBox)sender).Font.Size;
                Font f = new Font("맑은 고딕", size, FontStyle.Regular);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top); //원래항목을 DrawString으로 그림.
            }
        }
        void AutoFontSize(ComboBox cb, String text)
        {
            Font ft;
            Graphics gp;
            SizeF sz;
            Single Faktor, FaktorX, FaktorY;
            gp = cb.CreateGraphics();
            sz = gp.MeasureString(text, cbx.Font);
            gp.Dispose();

            FaktorX = (cb.Width) / sz.Width;
            FaktorY = (cb.Height) / sz.Height;
            if (FaktorX > FaktorY)
                Faktor = FaktorY;
            else
                Faktor = FaktorX;
            ft = cb.Font;

            cb.Font = new Font("맑은 고딕", ft.SizeInPoints * (Faktor) - 1);
        }
    }

    public void SetTextBox(TextBox tbx)
    {
        //사이즈 조절
        tbx.Dock = DockStyle.Fill;

        if (tbx.Parent is Panel)
        {
            Panel pnl = tbx.Parent as Panel;
            //cbx.ItemHeight = pnl.Size.Height;
            while (tbx.Height > pnl.Size.Height)
            {
                AutoFontSize(tbx, "가나다");
            }
        }
        void AutoFontSize(TextBox tb, String text)
        {
            Font ft;
            Graphics gp;
            SizeF sz;
            Single Faktor, FaktorX, FaktorY;
            gp = tb.CreateGraphics();
            sz = gp.MeasureString(text, tb.Font);
            gp.Dispose();

            FaktorX = (tb.Width) / sz.Width;
            FaktorY = (tb.Height) / sz.Height;
            if (FaktorX > FaktorY)
                Faktor = FaktorY;
            else
                Faktor = FaktorX;
            ft = tb.Font;

            tb.Font = new Font("맑은 고딕", ft.SizeInPoints * (Faktor) - 1);
        }
    }

    public class FontWizard
    {
        public static Font FlexFont(Graphics g, float minFontSize, float maxFontSize, Size layoutSize, string s, Font f, out SizeF extent)
        {
            if (maxFontSize == minFontSize)
                f = new Font(f.FontFamily, minFontSize, f.Style);

            extent = g.MeasureString(s, f);

            if (maxFontSize <= minFontSize)
                return f;

            float hRatio = layoutSize.Height / extent.Height;
            float wRatio = layoutSize.Width / extent.Width;
            float ratio = (hRatio < wRatio) ? hRatio : wRatio;

            float newSize = f.Size * ratio;

            if (newSize < minFontSize)
                newSize = minFontSize;
            else if (newSize > maxFontSize)
                newSize = maxFontSize;

            f = new Font(f.FontFamily, newSize, f.Style);
            extent = g.MeasureString(s, f);

            return f;
        }

        public static void OnPaint(object sender, PaintEventArgs e, string text)
        {
            var control = sender as Control;
            if (control == null)
                return;

            control.Text = string.Empty;    //delete old stuff
            var rectangle = control.ClientRectangle;

            using (Font f = new System.Drawing.Font("Microsoft Sans Serif", 20.25f, FontStyle.Bold))
            {
                SizeF size;
                using (Font f2 = FontWizard.FlexFont(e.Graphics, 5, 50, rectangle.Size, text, f, out size))
                {
                    PointF p = new PointF((rectangle.Width - size.Width) / 2, (rectangle.Height - size.Height) / 2);
                    e.Graphics.DrawString(text, f2, Brushes.Black, p);
                }
            }
        }
    }

    public void AutoFontSize(Button label, String text)
    {
        Font ft;
        Graphics gp;
        SizeF sz;
        Single Faktor, FaktorX, FaktorY;
        gp = label.CreateGraphics();
        sz = gp.MeasureString(text, label.Font);
        gp.Dispose();

        FaktorX = (label.Width) / sz.Width;
        FaktorY = (label.Height) / sz.Height;
        if (FaktorX > FaktorY)
            Faktor = FaktorY;
        else
            Faktor = FaktorX;
        ft = label.Font;

        label.Font = new Font("맑은 고딕", ft.SizeInPoints * (Faktor) - 1);
    }

    public void AutoFontSize(RadioButton label, String text)
    {
        Font ft;
        Graphics gp;
        SizeF sz;
        Single Faktor, FaktorX, FaktorY;
        gp = label.CreateGraphics();
        sz = gp.MeasureString(text, label.Font);
        gp.Dispose();

        FaktorX = (label.Width) / sz.Width;
        FaktorY = (label.Height) / sz.Height;
        if (FaktorX > FaktorY)
            Faktor = FaktorY;
        else
            Faktor = FaktorX;
        ft = label.Font;

        label.Font = new Font("맑은 고딕", ft.SizeInPoints * (Faktor) - 1);
    }
    /// <summary>
    /// 콤보박스의 ID값을 가져오는 함수 SetCombobox로 콤보박스를 세팅할 경우에 값을 가져올때 사용하는 함수
    /// </summary>
    /// <param name="cbx"></param>
    /// <param name="list_cbx"></param>
    /// <returns></returns>
    public string GetComboValue(ComboBox cbx, List<CB_IDNAME> list_cbx)
    {
        if (cbx.Items.Count == 0)
        { return "Item이 존재하지 않습니다"; }
        int i = cbx.SelectedIndex;
        foreach (CB_IDNAME ci in list_cbx)
        {
            if (ci.cbName == cbx.Name && ci.Idx == i)
            {
                return ci.ID;
            }
        }
        return "ID가 존재하지 않습니다";
    }

    //public int GetComboIndex(ComboBox cbx, List<CB_IDNAME> list_cbx)
    //{
    //    if (cbx.Items.Count == 0)
    //    { return 0; }
    //    int i = cbx.SelectedIndex;
    //    foreach (CB_IDNAME ci in list_cbx)
    //    {
    //        if (ci.cbName == cbx.Name && ci.Idx == i)
    //        {
    //            return ci.Idx;
    //        }
    //    }
    //    return 0;
    //}

    public string FindComboBoxIdx(ComboBox cbx, List<CB_IDNAME> list_cbx, string DepartID)
    {
        if (cbx.Items.Count == 0)
        { return ""; }
        foreach (CB_IDNAME ci in list_cbx)
        {
            if (ci.cbName == cbx.Name && ci.ID.ToLower() == DepartID.ToLower())
            {
                return ci.Idx.ToString();
            }
        }
        return "";
    }

    public string FindComboBoxID(ComboBox cbx, List<CB_IDNAME> list_cbx)
    {
        if (cbx.Items.Count == 0)
        { return ""; }
        int i = cbx.SelectedIndex;
        foreach (CB_IDNAME ci in list_cbx)
        {
            if (ci.cbName == cbx.Name && cbx.SelectedIndex == ci.Idx)
            {
                return ci.ID;
            }
        }
        return "";
    }

    //글자 세로로 쓰기
    public void DrawVerticalText(string txt, Form form)
    {
        Graphics formGraphics = form.CreateGraphics();
        //txt = "Sample 한글..Text";
        Font drawFont = new Font("맑은 고딕", 22);
        SolidBrush drawBrush = new SolidBrush(Color.Black);
        float x = 150.0f; float y = 50.0f;
        // 출력방향 설정 
        StringFormat drawFormat = new StringFormat(StringFormatFlags.DirectionVertical);//세로
        formGraphics.DrawString(txt, drawFont, drawBrush, x, y, drawFormat);
        drawFont.Dispose();
        drawBrush.Dispose();
        formGraphics.Dispose();
    }



    public string UserIPAddress
    {
        get
        {
            IPHostEntry IPHost = Dns.GetHostByName(Dns.GetHostName());

            string _UserIPAddress = IPHost.AddressList[0].ToString();

            return _UserIPAddress;
        }

    }

    //위 버튼, 그리드뷰 선택된 셀에서 위로 이동
    public void btnRowUp(DataGridView dgv)
    {
        int iSelRow = 0;
        for (int i = 0; i < dgv.SelectedCells.Count; i++)
        {
            iSelRow = dgv.SelectedCells[i].RowIndex;
            if (iSelRow == 0) return;
            dgv[0, iSelRow - 1].Selected = true;
            break;
        }

    }
    //아래 버튼, 그리드뷰 선택된 셀에서 아래로 이동
    public void btnRowDown(DataGridView dgv)
    {
        int iSelRow = 0;
        for (int i = 0; i < dgv.SelectedCells.Count; i++)
        {
            iSelRow = dgv.SelectedCells[i].RowIndex;
            if (iSelRow == dgv.Rows.Count - 1) return;
            dgv[0, iSelRow + 1].Selected = true;
            break;
        }
    }

    // int형 숫자 문자열에서 숫자만 추출하기 위한 함수 ex)콤마제거
    public int OnlyNumber(string strNumber)
    {
        string strTarget = CheckNum(strNumber);
        string strTmp = Regex.Replace(strTarget, @"\D", "");
        int nTmp = int.Parse(strTmp);
        return nTmp;
    }

    public int Number(string strNumber)
    {
        string strTarget = CheckNum(strNumber);
        double douTmp = double.Parse(strTarget);
        int intTmp = (int)douTmp;
        return intTmp;
    }

    public double GetDouble(string strNumber)
    {
        string douNumber = "";
        string jisu = "";
        string sosu = "";
        string sign = "";
        double douNum = 0;
        if (strNumber == "")
        {
            strNumber = "0";
        }
        if (strNumber.Contains("-"))
        {
            sign = "-";
        }
        else
        {
            sign = "";
        }
        if (strNumber.Contains("."))
        {
            string[] jisusosu = null;
            jisusosu = strNumber.Split('.');
            if (jisusosu.Length == 2)
            {
                jisu = jisusosu[0];
                sosu = jisusosu[1];
            }
        }
        else
        {
            jisu = strNumber;
        }
        jisu = Regex.Replace(jisu, @"\D", "");
        douNumber = sign + jisu + "." + sosu;
        double.TryParse(douNumber, out douNum);
        return douNum;
    }

    //Null값 체크
    public string CheckNull(string Value)
    {
        if (String.IsNullOrEmpty(Value) == false)
        {
            return Value.Trim();
        }
        else
        {
            return "";
        }
    }

    public string CheckNum(string Value)
    {
        if (WizWorkLib.IsNumeric(Value) && Value != "")
        {
            return string.Format("{0:#,###}", Value);
        }
        else
        {
            return "0";
        }
    }

    //날짜타입함수
    public string MakeDate(DateTimeClss iFormat, string sDate)
    {
        string functionReturnValue = "";
        IFormatProvider KR_Format = new System.Globalization.CultureInfo("ko-KR", true);
        DateTime dt;
        if (sDate == "")
        { return ""; }


        if (sDate.Length == 6)
        {
            sDate = sDate + "01";
        }

        dt = DateTime.ParseExact(sDate, "yyyyMMdd", KR_Format);

        if (sDate.Length == 8)
        {
            switch (Convert.ToInt32(iFormat))
            {
                case (int)WizWorkLib.DateTimeClss.DF_FULL:
                    functionReturnValue = string.Format("{0:yyyy년 MM월 dd일}", dt);
                    break;
                case (int)WizWorkLib.DateTimeClss.DF_LONG:
                    functionReturnValue = string.Format("{0:d}", dt);
                    break;
                case (int)WizWorkLib.DateTimeClss.DF_SHORT:
                    functionReturnValue = string.Format("{0:MM/dd}", dt);
                    break;
                case (int)WizWorkLib.DateTimeClss.DF_MID:
                    functionReturnValue = string.Format("{0:yy-MM-dd}", dt);
                    break;
                case (int)WizWorkLib.DateTimeClss.DF_MD:
                    functionReturnValue = string.Format("{0:yyyyMMdd}", dt);
                    break;
                case (int)WizWorkLib.DateTimeClss.DF_FD:
                    functionReturnValue = string.Format("{0:yyyy-MM-dd}", dt);
                    break;
            }
        }
        else
        {
            functionReturnValue = "";
        }
        return functionReturnValue;
    }

    public string MakeDateTime(string strType, string Date)
    {
        string value = "";
        if (Date.Length <= 0)
        {
            return "";
        }
        if (strType.ToUpper() == "YYYYMMDD")
        {
            value = Date.Substring(0, 4) + "-" + Date.Substring(4, 2) + "-" + Date.Substring(6, 2);               //일자

        }
        else if (strType.ToUpper() == "YYMMDD")
        {
            value = Date.Substring(0, 2) + "-" + Date.Substring(2, 2) + "-" + Date.Substring(4, 2);
        }
        else if (strType.ToUpper() == "HHMM")
        {
            value = Date.Substring(0, 2) + ":" + Date.Substring(2, 2);
        }
        else if (strType.ToUpper() == "HHMMSS")
        {
            value = Date.Substring(0, 2) + ":" + Date.Substring(2, 2) + ":" + Date.Substring(4, 2);
        }
        return value;
    }

    public string GetDefaultPrinter()
    {
        PrinterSettings settings = new PrinterSettings();
        foreach (string printer in PrinterSettings.InstalledPrinters)
        {
            settings.PrinterName = printer;
            //if (settings.IsDefaultPrinter) 기본 프린트일때
            if (printer.Contains("TSC"))
            {
                if (printer.Contains("Pro"))
                {
                    return printer;
                }
            }

        }
        return string.Empty;
    }

    #region DataCheck

    public static bool IsNumeric(string value)
    {
        value = value.Replace("-", "");
        value = value.Replace(",", "");
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
    #endregion

    /// <summary>
    /// 문자열 왼쪽편처음부터 지정된 문자열값 리턴(VBScript Left기능)
    /// </summary>
    /// <param name="target">얻을 문자열</param>
    /// <param name="length">얻을 문자열길이</param>
    /// <returns>얻은 문자열 값</returns>
    public static string Left(string target, int length)
    {
        if (length <= target.Length)
        {
            return target.Substring(0, length);
        }
        return target;
    }
    /// <summary>
    /// 지정된 위치이후 모든 문자열 리턴 (VBScript Mid기능)
    /// </summary>
    /// <param name="target">얻을 문자열</param>
    /// <param name="start">얻을 시작위치</param>
    /// <returns>지정된 위치 이후 모든 문자열리턴</returns>
    public static string Mid(string target, int start)
    {
        if (start <= target.Length)
        {
            return target.Substring(start - 1);
        }
        return string.Empty;
    }
    /// <summary>
    /// 문자열이 지정된 위치에서 지정된 길이만큼까지의 문자열 리턴 (VBScript Mid기능)
    /// </summary>
    /// <param name="target">얻을 문자열</param>
    /// <param name="start">얻을 시작위치</param>
    /// <param name="length">얻을 문자열길이</param>
    /// <returns>지정된 길이만큼의 문자열 리턴</returns>
    public static string Mid(string target, int start, int length)
    {
        if (start <= target.Length)
        {
            if (start + length - 1 <= target.Length)
            {
                return target.Substring(start - 1, length);
            }
            return target.Substring(start - 1);
        }
        return string.Empty;
    }
    /// <summary>
    /// 문자열 오른쪽편처음부터 지정된 문자열값 리턴(VBScript Right기능) 
    /// </summary>
    /// <param name="target">얻을 문자열</param>
    /// <param name="length">얻을 문자열길이</param>
    /// <returns>얻은 문자열 값</returns>
    public static string Right(string target, int length)
    {
        if (length <= target.Length)
        {
            return target.Substring(target.Length - length);
        }
        return target;
    }


    #region Query 성공여부 bool값 false면 성공 true면 실패

    public bool IsOKQuery(string[] strMessage)
    {
        bool IsOK = true;
        foreach (var str in strMessage)
        {
            if (str == "success")
            {
                IsOK = false;
            }
        }
        return IsOK;
    }

    #endregion

    //17.12.22 TextBox 숫자만 입력(소수점)
    public static void TypingOnlyNumber(object sender, KeyPressEventArgs e, bool includePoint, bool includeMinus)
    {
        bool isValidInput = false;
        if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != 5)
        {
            if (includeMinus == true) { if (e.KeyChar == '-') isValidInput = true; }
            if (includePoint == true) { if (e.KeyChar == '.') isValidInput = true; }

            if (isValidInput == false) e.Handled = true;
        }

        if (includePoint == true)
        {
            if (e.KeyChar == '.' && (string.IsNullOrEmpty((sender as TextBox).Text.Trim()) || (sender as TextBox).Text.IndexOf('.') > -1)) e.Handled = true;
        }

        if (includeMinus == true)
        {
            if (e.KeyChar == '-' && (string.IsNullOrEmpty((sender as TextBox).Text.Trim()) || (sender as TextBox).Text.IndexOf('-') > -1)) e.Handled = true;
        }

        //(sender as TextBox).MaxLength = 8;
    }

}



#endregion