using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using WizCommon;

namespace WizWork
{
    public partial class frm_tprc_CardRePrint_U : Form
    {
        INI_GS gs = new INI_GS();
        WizWorkLib Lib = new WizWorkLib();
        List<Sub_TWkLabelPrint> list_TWkLabelPrint = null;
        string[] Message = new string[2];


        public frm_tprc_CardRePrint_U()
        {
            InitializeComponent();
        }

        #region Default Grid Setting

        private void InitGrid()
        {
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdData.ColumnCount = 17;

            int i = 0;

            grdData.Columns[i].Name = "RowSeq";
            grdData.Columns[i].HeaderText = "No";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;


            grdData.Columns[++i].Name = "LabelID";
            grdData.Columns[i].HeaderText = "라벨ID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "PrintDate";
            grdData.Columns[i].HeaderText = "발행일자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "PrintTime";
            grdData.Columns[i].HeaderText = "발행시각";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "BuyerArticleNo";
            grdData.Columns[i].HeaderText = "품번";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Article";
            grdData.Columns[i].HeaderText = "품명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Worker";
            grdData.Columns[i].HeaderText = "작업자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ReprintDate";
            grdData.Columns[i].HeaderText = "재발행일자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ReprintQty";
            grdData.Columns[i].HeaderText = "재발행수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "InstID";
            grdData.Columns[i].HeaderText = "지시번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "OrderID";
            grdData.Columns[i].HeaderText = "오더번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "QtyPerBox";
            grdData.Columns[i].HeaderText = "수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "OrderArticleID";
            grdData.Columns[i].HeaderText = "품목코드";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "KCustom";
            grdData.Columns[i].HeaderText = "거래처";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;           

            grdData.Columns[++i].Name = "Model";
            grdData.Columns[i].HeaderText = "차종";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "LastProdArticleID";
            grdData.Columns[i].HeaderText = "최종품품명코드ID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;
           

            grdData.Columns[++i].Name = "WorkDate";
            grdData.Columns[i].HeaderText = "작업일자";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            DataGridViewCheckBoxColumn curCol = new DataGridViewCheckBoxColumn();
            curCol.HeaderText = "선택";
            curCol.Name = "Check";
            curCol.Width = 50;
            curCol.DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns.Insert(1, curCol);

            grdData.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;


            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        #endregion

        private void btnNew_Click(object sender, EventArgs e)
        {
            FillGridData();
        }

        private void FillGridData()
        {
            try
            {
                grdData.Rows.Clear();
                string sLabelGubun = "";
                string sInstID = "";
                int nChkDate = chkInstDate.Checked == true ? 1 : 0;
                string sSDate = mtb_From.Text.Replace("-", "");
                string sEDate = mtb_To.Text.Replace("-", "");

                
                foreach (Control rbn in tlpSearch_LL.Controls)
                {
                    if (rbn is RadioButton)
                    {
                        RadioButton chkrbn = rbn as RadioButton;
                        if (chkrbn.Checked)
                        {
                            sLabelGubun = rbn.Tag.ToString();
                            break;
                        }
                    }
                }

                // 2020.04.13 둘리 품번으로 검색, 재 발행인데 라벨 번호로 검색을 하는건 거의 불가능 하지 않슴니까
                int nChkMtrLotNo = chkMtrLotNo.Checked == true ? 1 : 0;
                if (txtMtrLotNo.Text.ToUpper().Contains("PL") && (txtMtrLotNo.Text.Trim().Length == 15 || txtMtrLotNo.Text.Trim().Length == 16))
                {
                    sInstID = txtMtrLotNo.Text.Trim().Substring(2, 12);
                }

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("nChkDate", nChkDate);//상위품ID
                sqlParameter.Add("FromDate", sSDate);
                sqlParameter.Add("ToDate", sEDate);
                sqlParameter.Add("LabelGubun", sLabelGubun);
                sqlParameter.Add("nChkInstID", 0);
                sqlParameter.Add("InstID", "");//상위품ID
                sqlParameter.Add("nChkMtrLotNo", nChkMtrLotNo);//상위품ID
                sqlParameter.Add("MtrLotNo", txtMtrLotNo.Text);//상위품ID

                
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sCardLabelPrint", sqlParameter, false);
                DataRow dr = null;
                int QtyPerBox = 0;
                int ReprintQty = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        int.TryParse(Lib.CheckNum(dr["ReprintQty"].ToString()), out ReprintQty);
                        int.TryParse(Lib.CheckNum(dr["QtyPerBox"].ToString()), out QtyPerBox);
                        grdData.Rows.Add((i + 1).ToString(),//Index
                                         false,                     //선택
                                         dr["LabelID"].ToString(),  //라벨ID
                                         Lib.MakeDateTime("yyyymmdd", dr["PrintDate"].ToString().Trim()),
                                         Lib.MakeDateTime("HHmmSS", dr["WorkEndTime"].ToString().Trim()),
                                         dr["BuyerArticleNo"].ToString(),    //'품번  
                                         dr["Article"].ToString(),         //'품명   
                                         dr["Worker"].ToString(),          // '작업자  
                                         Lib.MakeDateTime("yyyymmdd", dr["ReprintDate"].ToString().Trim()),   //'재발행일자
                                         string.Format("{0:n0}", ReprintQty),       // 재발행'수량

                   
                                         dr["InstID"].ToString(),       // 'InstID       
                                         dr["OrderID"].ToString(),           // 'OrderID  
                                         string.Format("{0:n0}", QtyPerBox),   //'수량
                                         dr["OrderArticleID"].ToString(),         //'품명

                                         dr["KCUSTOM"].ToString(),             //  '거래처                                         
                                         dr["Model"].ToString(),            //'차종   
                                         dr["LastProdArticleID"].ToString(),   //'마지막제품 품명코드ID                                           
                                         dr["WorkDate"].ToString()          //'작업일자             
                        );
                    }
                    grdData.ClearSelection();
                    if (grdData.Rows.Count > 0) { grdData[0, 0].Selected = true; } //0번째 행 선택 
                    Frm_tprc_Main.gv.queryCount = string.Format("{0:n0}", grdData.RowCount);
                    Frm_tprc_Main.gv.SetStbInfo();
                }
                else
                {
                    ((WizWork.Frm_tprc_Main)(this.MdiParent)).stsInfo_Msg.Text = "0개의 자료가 검색되었습니다.";
                    grdData.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        private void cmdRowUp_Click(object sender, EventArgs e)
        {
            Lib.btnRowUp(grdData);
        }

        private void cmdRowDown_Click(object sender, EventArgs e)
        {
            Lib.btnRowDown(grdData);
        }

        private void cmdBarcode_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                if (SaveData() == true)
                {
                    FillGridData();
                }

            }
        }
        private bool CheckData()
        {
            bool IsOk = true;
            if (grdData.Rows.Count > 0)
            {
                bool hasCheck = false;
                foreach (DataGridViewRow dr in grdData.Rows)
                {
                    string strReprintDate = dr.Cells["ReprintDate"].Value.ToString().Trim();
                    bool isChecked = (bool)dr.Cells["Check"].EditedFormattedValue;
                    if (isChecked)//체크된
                    {
                        if (strReprintDate.Equals(""))
                        {
                            Message[0] = "[재발행일 확인]";
                            Message[1] = "재발행일자가 없을 경우 재발행되지 않습니다";
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                            IsOk = false;
                        }
                        else
                        {
                            hasCheck = true;
                        }
                    }
                }
                
                if (!hasCheck)
                {
                    Message[0] = "[재발행라벨 선택]";
                    Message[1] = "재발행할 라벨을 선택해야 합니다";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    IsOk = false;
                }
            }
            else
            {
                Message[0] = "[조회]";
                Message[1] = "조회된 내용이 없습니다. 조회 후 클릭하여 주십시오.";
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                IsOk = false;
            }
            return IsOk;
        }

        private bool SaveData()
        {

            bool IsOK = true;
            int Count = 0;
            //if(grdData.Rows.Count == grdData.CurrentRow.Index)
            //{
            //    //MessageBox.Show(LoadResString(231));
            //}

            list_TWkLabelPrint = new List<Sub_TWkLabelPrint>(); // 초기화

            foreach (DataGridViewRow dr in grdData.Rows)
            {
                DataGridViewCell Cell = dr.Cells["Check"];
                bool isChecked = (bool)Cell.EditedFormattedValue;
                if (isChecked)
                {
                    list_TWkLabelPrint.Add(new Sub_TWkLabelPrint());
                    list_TWkLabelPrint[Count].sLabelID = dr.Cells["LabelID"].Value.ToString();
                    list_TWkLabelPrint[Count].sReprintDate = dr.Cells["ReprintDate"].Value.ToString().Replace("-", "");
                    list_TWkLabelPrint[Count].nQtyPerBox = Int32.Parse(Lib.CheckNum(dr.Cells["QtyPerBox"].Value.ToString()).Replace(",", ""));
                    list_TWkLabelPrint[Count].sCreateuserID = Frm_tprc_Main.g_tBase.PersonID;
                    list_TWkLabelPrint[Count].sLastProdArticleID = Lib.CheckNull(dr.Cells["LastProdArticleID"].Value.ToString());
                    list_TWkLabelPrint[Count].sInstID = dr.Cells["InstID"].Value.ToString();
                    Count++;
                }
            }

            UpdateWorkCardPrint(Count);

            return IsOK;
        }
        private void UpdateWorkCardPrint(int nCount)
        {
            if (list_TWkLabelPrint.Count > 0)
            {
                List<string> list_Data = null;
                for (int i = 0; i < nCount; i++)
                {
                    try
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("LabelID", list_TWkLabelPrint[i].sLabelID);//상위품ID
                        sqlParameter.Add("ReprintDate", list_TWkLabelPrint[i].sReprintDate);//상위품ID
                        sqlParameter.Add("UpdateUserID", list_TWkLabelPrint[i].sCreateuserID);//상위품ID

                        DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_uCardLabelPrint", sqlParameter, false);


                        string g_sPrinterName = Lib.GetDefaultPrinter();

                        string TagID = "";
                        list_Data = new List<string>();
                        //라벨선택
                        if (rbnProcessLabel.Checked)
                        {
                            TagID = "013";

                            list_TWkLabelPrint[i].sLabelGubun = "7";
                        }
                        //데이터셋팅

                        double douworkqty = 0;
                        double doudefectqty = 0;

                        string DayNightName = string.Empty;
                        string ArticleSub = string.Empty;


                        int index = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            index++;
                            if (index == 1)//  (dr["ProcSeq"].ToString().Equals("1"))
                            {
                                if (dr["DayOrNightID"].ToString().Trim().Equals("01"))
                                {
                                    DayNightName = "주간";
                                }
                                else if (dr["DayOrNightID"].ToString().Trim().Equals("02"))
                                {
                                    DayNightName = "야간";
                                }
                                double.TryParse(dr["WorkQty"].ToString(), out douworkqty);
                                double.TryParse(dr["wk_defectQty"].ToString(), out doudefectqty);

                                list_Data.Add(Lib.CheckNull(dr["wk_CardID"].ToString())); //라벨번호(공정전표)

                                list_Data.Add(Lib.CheckNull(dr["Model"].ToString()));// 차종
                                list_Data.Add(Lib.CheckNull(dr["Process"].ToString()));// 공정명
                                list_Data.Add(Lib.CheckNull(dr["BuyerArticleNo"].ToString()));// 품번
                                list_Data.Add(Lib.CheckNull(dr["Article"].ToString())); // 품명
                                //list_Data.Add(Lib.CheckNull(dr["DayOrNightID"].ToString())); // 주/야간
                                list_Data.Add(Lib.CheckNull(DayNightName));
                                list_Data.Add(Lib.CheckNull(dr["wk_Name"].ToString()));// 작업자
                                list_Data.Add((string.Format("{0:n0}", (int)douworkqty)));// _수량
                                list_Data.Add(Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FD, DateTime.Now.ToString("yyyyMMdd")));//재발행일자로 수정 2024-02-19
                                //list_Data.Add(Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FD, Lib.CheckNull(dr["wk_ResultDate"].ToString())));//D_생산일자
                                //list_Data.Add((string.Format("{0:n0}", (int)doudefectqty)));// _불량수량

                                #region 서강정밀 Zebra Printer 사용

                                //#region 라벨에 주/야간 출력(서강정밀)


                                //if (dr["Article"].ToString().Length > 12)  // 품명 12글자 이상일 경우 12글자까지만 출력
                                //{
                                //    ArticleSub = dr["Article"].ToString().Substring(0, 12);
                                //}
                                //#endregion


                                //#region 제브라 ZT230 프린터 라벨 소스(2020.11.03.서강정밀)
                                ////Zebra Printer 인쇄 라벨 디자인 2020.10.23.KGH
                                //string zZPL = " ^XA\n";
                                //zZPL += "^SEE:UHANGUL.DAT^FS\n";               // 요 두줄을 날려줘야 한글이 출력
                                //zZPL += "^CW1,E:KFONT3.FNT^CI26^FS\n";

                                //zZPL += "^LH3,3\n";
                                //zZPL += "^FO10,20^GB712,390,3^FS\n";           // 가장 바깥 테두리
                                //zZPL += "^FO10,20^^FS\n";
                                //zZPL += "^FO20,30^A1N35,35^FD생 산 전 표^FS\n";// T 좌측상단 텍스트
                                //zZPL += "^FO600,30^A1N35,35^FD출하동^FS\n";    // T 우측상단 텍스트
                                ////zZPL += "^FO125,335^BY2,3,60^B3 ,,,, ^FD"+Lib.CheckNull(dr["wk_CardID"].ToString())+"^FS\n";
                                //zZPL += "^FO125,315^BY3^BCN,60,Y,N,N^FD" + Lib.CheckNull(dr["wk_CardID"].ToString()) + "^FS\n";    // D 바코드
                                //zZPL += "^PRD\n";                              // 프린트 속도 152.4mm/Sec
                                //zZPL += "^FO20,65^GB693,60,3^FS\n";            // 세로 1번째 칸
                                //zZPL += "^FO20,65^^FS\n";
                                //zZPL += "^FO20,65^GB693,120,3^FS\n";           // 세로 2번째 칸
                                //zZPL += "^FO20,65^^FS\n";
                                //zZPL += "^FO20,65^GB693,180,3^FS\n";           // 세로 3번째 칸
                                //zZPL += "^FO20,65^^FS\n";
                                //zZPL += "^FO20,65^GB693,240,3^FS\n";           // 세로 4번째 칸
                                //zZPL += "^FO20,65^^FS\n";
                                //zZPL += "^FO25,80^A1N35,35^FD차 종^FS\n";      // T 세로 1번째 텍스트
                                //zZPL += "^FO25,140^A1N35,35^FD품 번^FS\n";     // T 세로 2번째 텍스트
                                //zZPL += "^FO25,200^A1N35,35^FD품 명^FS\n";     // T 세로 3번째 텍스트
                                //zZPL += "^FO25,260^A1N35,35^FD수 량^FS\n";     // T 세로 4번째 텍스트
                                //zZPL += "^FO130,80^A1N35,35^FD" + Lib.CheckNull(dr["Model"].ToString()) + "^FS\n"; // D 텍스트 1 내용 텍스트 (차종)
                                //zZPL += "^FO130,140^A1N35,35^FD" + Lib.CheckNull(dr["BuyerArticleNo"].ToString()) + "^FS\n"; // D 텍스트 2 내용 텍스트 (품번)
                                //zZPL += "^FO130,200^A1N35,35^FD" + ArticleSub + "..." + "^FS\n"; // D 텍스트 3 내용 텍스트(품명)
                                //zZPL += "^FO130,260^A1N35,35^FD" + string.Format("{0:n0}", (int)douworkqty) + "^FS\n"; // D 텍스트 4 내용 텍스트 (수량)
                                //zZPL += "^FO20,65^GB100,240,3^FS\n";           // 차종, 품번, 품명, 수량 칸
                                //zZPL += "^FO20,65^^FS\n";
                                //zZPL += "^FO310,65^GB149,60,3^FS\n";           // 작업장(공정) 칸
                                //zZPL += "^FO310,65^^FS\n";
                                //zZPL += "^FO335,80^A1N35,35^FD공 정^FS\n";    // T 작업장(공정) 칸 텍스트
                                //zZPL += "^FO470,80^A1N35,35^FD" + Lib.CheckNull(dr["Process"].ToString()) + "^FS\n";    // D 작업장 내용 텍스트(공정명)
                                //zZPL += "^FO548,123^GB165,122,2^FS\n";         // 주/야간 칸, 합격자 성명 칸
                                //zZPL += "^FO585,123^^FS\n";
                                //zZPL += "^FO560,140^A1N35,35^FD" + DayNightName + "^FS\n";   // D 주/야간 텍스트  
                                //zZPL += "^FO455,183^GB95,62,2^FS\n";          // 합격 칸
                                //zZPL += "^FO455,183^^FS\n";
                                //zZPL += "^FO465,200^A1N35,35^FD합격^FS\n";    // T 합격 칸 텍스트 
                                //zZPL += "^FO560,200^A1N35,35^FD" + Lib.CheckNull(dr["wk_Name"].ToString()) + "^FS\n";  // D 합격 칸 내용 텍스트
                                //zZPL += "^FO310,242^GB157,63,2^FS\n";          // 생산일자 칸
                                //zZPL += "^FO310,242^^FS\n";
                                //zZPL += "^FO320,260^A1N35,35^FD생산일자^FS\n"; // T 생산일자 텍스트
                                ////zZPL += "^FO500,277^AE30,30^FD"+Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FD, Lib.CheckNull(dr["wk_ResultDate"].ToString().Trim()))+"^FS\n"; // D 날짜 텍스트
                                //zZPL += "^FO480,260^AE35,35^FD" + ConvertDateTime(Lib.CheckNull(dr["wk_ResultDate"].ToString().Trim())) + "^FS\n"; // D 날짜 텍스트
                                //zZPL += "^FO20,65^GB693,240,3^FS\n";           // 안쪽 바깥 테두리
                                //zZPL += "^XZ";

                                //// Zebra Printer 연결
                                //RawPrinterHelper.SendStringToPrinter("Zebra ZM400 (203 dpi) - ZPL", zZPL); // 프린터 이름으로 연결(ZDesigner ZT230-200dpi ZPL), Zebra ZM400 (203 dpi) - ZPL, Zebra ZT230 (203 dpi)
                                //#endregion
                                #endregion
                            }
                            else
                            {
                                list_Data.Add(Lib.CheckNull(dr["Process"].ToString())); // 다음(순차) 공정의  품명
                            }
                        }
                        //인쇄DLL 

                        frm_tprc_Work_U ftWU = new frm_tprc_Work_U();

                        TSCLIB_DLL.openport(g_sPrinterName);
                        if (ftWU.SendWindowDllCommand(list_Data, TagID, 1, 0))
                        {
                            Message[0] = "[라벨발행 중]";
                            Message[1] = "라벨 발행중입니다. 잠시만 기다려주세요.";
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
                        }

                        else
                        {
                            Message[0] = "[라벨발행 실패]";
                            Message[1] = "라벨 발행에 실패했습니다. 관리자에게 문의하여주세요.\r\n<SendWindowDllCommand>";
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 2);
                        }
                        TSCLIB_DLL.closeport();
                    }
                    catch (Exception ex)
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                    }
                }
            }
        }

        private DateTime ConvertDateTime(string str)
        {
            DateTime result = new DateTime();
            str = str.Replace("/", "").Replace(".", "").Replace("-", "").Trim();

            if (str.Length == 8)
            {
                str = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
            }

            if (DateTime.TryParse(str, out result) == false)
            {
                result = DateTime.Today;
                //result = Lib.MakeDate(WizWorkLib.DateTimeClss.DF_FD, Lib.CheckNull(dr["wk_ResultDate"].ToString().Trim()));
            }
            return result;
        }


        private void frm_tprc_CardRePrint_U_Load(object sender, EventArgs e)
        {
            SetScreen();
            InitGrid();
            SetDateTime();

            rbnProcessLabel.Checked = true;
            FillGridData();
        }

        private void SetDateTime()
        {
            ////ini 날짜 불러와서 기간 설정하기
            chkInstDate.Checked = true;
            int Days = 0;
            string[] sInstDate =Frm_tprc_Main.gs.GetValue("Work", "Screen", "Screen").Split('|');
            foreach (string str in sInstDate)
            {
                string[] Value = str.Split('/');
                if (this.Name.ToUpper().Contains(Value[0].ToUpper()))
                {
                    int.TryParse(Value[1], out Days);
                    break;
                }
            }
            mtb_From.Text = DateTime.Today.AddDays(-Days).ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");
            //
        }

        #region TableLayoutPanel 하위 컨트롤들의 DockStyle.Fill 세팅
        private void SetScreen()
        {
            tlpForm.Dock = DockStyle.Fill;
            tlpForm.Margin = new Padding(0, 0, 0, 0);
            foreach (Control control in tlpForm.Controls)//con = tlp 상위에서 2번째
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 0, 0, 0);
                foreach (Control contro in control.Controls)//tlp 상위에서 3번째
                {
                    contro.Dock = DockStyle.Fill;
                    contro.Margin = new Padding(0, 0, 0, 0);
                    foreach (Control contr in contro.Controls)
                    {
                        contr.Dock = DockStyle.Fill;
                        contr.Margin = new Padding(0, 0, 0, 0);
                        foreach (Control cont in contr.Controls)
                        {
                            cont.Dock = DockStyle.Fill;
                            cont.Margin = new Padding(0, 0, 0, 0);
                            foreach (Control con in cont.Controls)
                            {
                                con.Dock = DockStyle.Fill;
                                con.Margin = new Padding(0, 0, 0, 0);
                                foreach (Control co in con.Controls)
                                {
                                    co.Dock = DockStyle.Fill;
                                    co.Margin = new Padding(0, 0, 0, 0);
                                }
                            }
                        }
                    }
                }
            }

            //Main tlp 세팅
            tlp_Search_Date.SetRowSpan(chkInstDate, 2);
            tlpSearch_LL.SetColumnSpan(txtMtrLotNo, 2);

        }
        #endregion
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                bool flag = (bool)grdData.Rows[e.RowIndex].Cells["Check"].Value;
                grdData.Rows[e.RowIndex].Cells["Check"].Value = !flag;
                if (!flag)
                {
                    if(grdData.Rows[e.RowIndex].Cells["ReprintDate"].Value.ToString().Equals(""))
                    {
                        grdData.Rows[e.RowIndex].Cells["ReprintDate"].Value = Lib.MakeDateTime("yyyymmdd", DateTime.Now.ToString("yyyyMMdd"));
                    }
                }
                else
                {
                    grdData.Rows[e.RowIndex].Cells["ReprintDate"].Value = "";
                }
            }
        }       

        #region 달력 From값 입력 // 달력 창 띄우기
        private void mtb_From_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_From.Text.Replace("-", ""), mtb_From.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }
        #endregion
        #region Calendar.Value -> mtbBox.Text 달력창으로부터 텍스트로 값을 옮겨주는 메소드
        private void GetDate(string strDate, string btnName)
        {
            DateTime dateTime = new DateTime();
            dateTime = DateTime.ParseExact(strDate, "yyyyMMdd", null);
            if (btnName == mtb_From.Name)
            {
                mtb_From.Text = dateTime.ToString("yyyy-MM-dd");
            }
            else if (btnName == mtb_To.Name)
            {
                mtb_To.Text = dateTime.ToString("yyyy-MM-dd");
            }

        }
        #endregion
        #region 달력 To값 입력 // 달력 창 띄우기
        private void mtb_To_Click(object sender, EventArgs e)
        {
            WizCommon.Popup.Frm_TLP_Calendar calendar = new WizCommon.Popup.Frm_TLP_Calendar(mtb_To.Text.Replace("-", ""), mtb_To.Name);
            calendar.WriteDateTextEvent += new WizCommon.Popup.Frm_TLP_Calendar.TextEventHandler(GetDate);
            calendar.Owner = this;
            calendar.ShowDialog();
        }
        #endregion

        private void rbn_Click(object sender, EventArgs e)
        {
            FillGridData();
        }

        #region 둘리 - 원자재 로트 검색

        private void txtMtrLotNo_Click(object sender, EventArgs e)
        {
            if (this.chkMtrLotNo.Checked)
            {
                txtMtrLotNo.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("원자재 로트 입력", "원자재 로트");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtMtrLotNo.Text = keypad.tbInputText.Text;
                }
            }
            else
            {
                chkMtrLotNo.Checked = true;
                txtMtrLotNo.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("원자재 로트 입력", "원자재 로트");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtMtrLotNo.Text = keypad.tbInputText.Text;
                }
            }
        }

        private void chkMtrLotNo_Click(object sender, EventArgs e)
        {
            if (this.chkMtrLotNo.Checked)
            {
                txtMtrLotNo.Text = "";
                POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad("원자재 로트 입력", "원자재 로트");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtMtrLotNo.Text = keypad.tbInputText.Text;
                }
            }
            else
            {
                this.txtMtrLotNo.Text = "";
            }
        }

        #endregion


        #region 기타 메서드 모음

        // 천마리 콤마, 소수점 버리기
        private string stringFormatN0(object obj)
        {
            return string.Format("{0:N0}", obj);
        }

        // 천마리 콤마, 소수점 버리기
        private string stringFormatN1(object obj)
        {
            return string.Format("{0:N1}", obj);
        }

        // 천마리 콤마, 소수점 두자리
        private string stringFormatN2(object obj)
        {
            return string.Format("{0:N2}", obj);
        }

        // 데이터피커 포맷으로 변경
        private string DatePickerFormat(string str)
        {
            string result = "";

            if (str.Length == 8)
            {
                if (!str.Trim().Equals(""))
                {
                    result = str.Substring(0, 4) + "-" + str.Substring(4, 2) + "-" + str.Substring(6, 2);
                }
            }

            return result;
        }

        // 시간 형식 6글자라면! 11:11:11
        private string DateTimeFormat(string str)
        {
            str = str.Replace(":", "").Trim();

            if (str.Length == 6)
            {
                string Hour = str.Substring(0, 2);
                string Min = str.Substring(2, 2);
                string Sec = str.Substring(4, 2);

                str = Hour + ":" + Min + ":" + Sec;
            }

            return str;
        }

        // Int로 변환
        private int ConvertInt(string str)
        {
            int result = 0;
            int chkInt = 0;

            if (!str.Trim().Equals(""))
            {
                str = str.Replace(",", "");

                if (Int32.TryParse(str, out chkInt) == true)
                {
                    result = Int32.Parse(str);
                }
            }

            return result;
        }

        // 소수로 변환 가능한지 체크 이벤트
        private bool CheckConvertDouble(string str)
        {
            bool flag = false;
            double chkDouble = 0;

            if (!str.Trim().Equals(""))
            {
                if (Double.TryParse(str, out chkDouble) == true)
                {
                    flag = true;
                }
            }

            return flag;
        }

        // 숫자로 변환 가능한지 체크 이벤트
        private bool CheckConvertInt(string str)
        {
            bool flag = false;
            int chkInt = 0;

            if (!str.Trim().Equals(""))
            {
                str = str.Trim().Replace(",", "");

                if (Int32.TryParse(str, out chkInt) == true)
                {
                    flag = true;
                }
            }

            return flag;
        }

        // 소수로 변환
        private double ConvertDouble(string str)
        {
            double result = 0;
            double chkDouble = 0;

            if (!str.Trim().Equals(""))
            {
                str = str.Replace(",", "");

                if (Double.TryParse(str, out chkDouble) == true)
                {
                    result = Double.Parse(str);
                }
            }

            return result;
        }

        // 나누기 함수
        private double devideNum(double a, double b)
        {
            double result = 0;

            if (b == 0)
            {
                return result;
            }
            else
            {
                result = a / b;
            }

            return result;
        }

        #endregion

    }
}
