using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using WizCommon;
using WizWork;
using WizWork.Properties;

namespace WizIns
{
    public partial class Frm_tins_Result_Q : Form
    {
        INI_GS gs = Frm_tprc_Main.gs;
        WizWorkLib Lib = new WizWorkLib();

        // 메인 데이터 그리드 뷰 관리 객체
        List<Frm_tins_Result_Q_CodeView> lstMain = new List<Frm_tins_Result_Q_CodeView>();

        // 서브 데이터 그리드 뷰 관리 객체
        List<Frm_tins_InsDefectList> lstSub = new List<Frm_tins_InsDefectList>();

        // 삭제 및 라벨 재발행건들을 위한 객체
        List<Frm_tins_Result_Q_CodeView> lstIns = new List<Frm_tins_Result_Q_CodeView>();

        // 삭제는 PackID로 일괄 삭제 -> 합격수량이 0 일때, 포장 데이터가 생성되지 않아서, 해당 건은 삭제가 불가능하게 됨. 2020-11-10 GDU
        List<string> lstPackID = new List<string>();

        // lstPackID 위의 문제로, InspectID 만 삭제하는 용도 리스트 생성. 2020-11-10 GDU
        List<string> lstInspectID = new List<string>();

        List<WizWork.Sub_TWkLabelPrint> list_TWkLabelPrint = null;
        string[] Message = new string[2];

        /// <summary>
        /// 생성
        /// </summary>
        public Frm_tins_Result_Q()
        {
            InitializeComponent();
        }

        #region Dock = Fill : SetScreen()
        private void SetScreen()
        {
            tlpForm.Dock = DockStyle.Fill;
            tlpForm.Margin = new Padding(1, 1, 1, 1);
            foreach (Control control in tlpForm.Controls)//con = tlp 상위에서 2번째
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(1, 1, 1, 1);
                foreach (Control contro in control.Controls)//tlp 상위에서 3번째
                {
                    contro.Dock = DockStyle.Fill;
                    contro.Margin = new Padding(1, 1, 1, 1);
                    foreach (Control contr in contro.Controls)
                    {
                        contr.Dock = DockStyle.Fill;
                        contr.Margin = new Padding(1, 1, 1, 1);
                        foreach (Control cont in contr.Controls)
                        {
                            cont.Dock = DockStyle.Fill;
                            cont.Margin = new Padding(1, 1, 1, 1);
                            foreach (Control con in cont.Controls)
                            {
                                con.Dock = DockStyle.Fill;
                                con.Margin = new Padding(1, 1, 1, 1);
                                foreach (Control co in con.Controls)
                                {
                                    co.Dock = DockStyle.Fill;
                                    co.Margin = new Padding(1, 1, 1, 1);
                                    foreach (Control c in co.Controls)
                                    {
                                        c.Dock = DockStyle.Fill;
                                        c.Margin = new Padding(1, 1, 1, 1);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion

        private void Frm_tins_Result_Q_Load(object sender, EventArgs e)
        {
            SetScreen();

            // 데이터 그리드 초기 설정
            initDgv();

            // 오늘 날짜 세팅
            mtb_From.Text = DateTime.Today.ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");

            FillGrid();
        }

        private void Frm_tins_Result_Q_Activated(object sender, EventArgs e)
        {

        }

        #region 데이터 그리드 초기 설정 : wrap 설정
        private void initDgv()
        {
            // Header wrap 속성 false
            dgdMain.Columns["ExamDate"].HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            dgdMain.Columns["ExamTime"].HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            dgdMain.Columns["BuyerArticleNo"].HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            dgdMain.Columns["Article"].HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            dgdMain.Columns["Worker"].HeaderCell.Style.WrapMode = DataGridViewTriState.False;
            dgdMain.Columns["BoxID"].HeaderCell.Style.WrapMode = DataGridViewTriState.False;
        }

        #endregion

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

        #region Header - 검색조건 : 품번 검색

        private void chkBuyerArticleNo_Click(object sender, EventArgs e)
        {
            if (chkBuyerArticleNo.Checked)
            {
                txtBuyerArticleNo.Text = "";
                WizWork.POPUP.Frm_CMKeypad keypad = new WizWork.POPUP.Frm_CMKeypad("품번입력", "품번");

                keypad.Owner = this;
                if (keypad.ShowDialog() == DialogResult.OK)
                {
                    txtBuyerArticleNo.Text = keypad.tbInputText.Text;
                    FillGrid();
                }
            }
            else
            {
                txtBuyerArticleNo.Text = "";
            }
        }

        private void txtBuyerArticleNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                chkBuyerArticleNo.Checked = true;
                FillGrid();
            }
        }

        #endregion

        // 조회 버튼 클릭 이벤트
        private void btnSearch_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        // 닫기 버튼 클릭 이벤트
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        #region Right - 위 아래 버튼 클릭 이벤트 모음
        private void btnUp_Click(object sender, EventArgs e)
        {
            DataGridSelRow_UpDown(-1);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            DataGridSelRow_UpDown(1);
        }
        private void DataGridSelRow_UpDown(int upDown)
        {
            if (dgdMain.Rows.Count > 0
                 && dgdMain.SelectedRows[0] != null)
            {
                int moveIndex = dgdMain.SelectedRows[0].Index + upDown;
                int maxIndex = dgdMain.Rows.Count;

                if (moveIndex >= 0
                    && moveIndex < maxIndex)
                {
                    dgdMain[0, moveIndex].Selected = true;
                }
            }
        }
        #endregion

        #region Right - 삭제 버튼, 구문 모음

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CheckBeforeDelete() == false) { return; }

            if (DeleteData(lstPackID))
            {
                FillGrid();
            }
        }

        #region 삭제 구문 : DeleteData() ← 수정 필요
        private bool DeleteData(List<string> lstPackID)
        {
            bool flag = false;

            try
            {
                int deleteCount = 0;

                for (int i = 0; i < lstInspectID.Count; i++)
                {
                    string orderID = lstInspectID[i].Split('|')[0];
                    int rollSeq = Lib.ConvertInt(lstInspectID[i].Split('|')[1]);

                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("OrderID", orderID);
                    sqlParameter.Add("RollSeq", rollSeq);

                    string[] sConfirm = new string[2];
                    sConfirm = DataStore.Instance.ExecuteProcedure("[xp_prdIns_dInspect]", sqlParameter, true); //삭제
                    if (sConfirm[0].ToUpper() == "SUCCESS")
                    { deleteCount++; }
                }

                for (int i = 0; i < lstPackID.Count; i++)
                {
                    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    sqlParameter.Add("PackID", lstPackID[i]);

                    string[] sConfirm = new string[2];
                    sConfirm = DataStore.Instance.ExecuteProcedure("[xp_prdIns_dInspectAndPacking]", sqlParameter, true); //삭제
                    if (sConfirm[0].ToUpper() == "SUCCESS")
                    { deleteCount++; }
                }

                if (deleteCount > 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("총 " + deleteCount + "건이 삭제 완료 되었습니다.", "[삭제 완료]", 0, 1);
                    return true;
                }
                
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                return false;
            }

            return flag;
        }
        #endregion

        // 삭제할 리스트 확인 및 세팅
        private bool CheckBeforeDelete()
        {
            bool flag = false;

            if (dgdMain.Rows.Count <= 0)
            {
                return false;
            }

            lstPackID.Clear();
            lstInspectID.Clear();

            int OutCnt = 0;
            for (int i = 0; i < dgdMain.Rows.Count; i++)
            {
                var Main = dgdMain.Rows[i].DataBoundItem as Frm_tins_Result_Q_CodeView;
                if (Main != null && Main.Check == true)
                {
                    if (Main.OutDate.Trim().Equals("") == false)
                    {
                        OutCnt++;
                    }
                    else if (Main.PackID.Trim().Equals("") == false)
                    {
                        if (lstPackID.Contains(Main.PackID) == false) { lstPackID.Add(Main.PackID); }
                    }
                    else if (string.IsNullOrEmpty(Main.PackID.Trim()) // 2020.11.10 GDU, 합격 수량이 0 일 경우, 검사 테이블만 삭제할 수 있도록
                        && string.IsNullOrEmpty(Main.OrderID) == false)
                    {
                        lstInspectID.Add(Main.OrderID + "|" + Main.RollSeq);
                    }
                }
            }

            if (lstPackID.Count + lstInspectID.Count <= 0)
            {
                // 출고되었을 경우 팝업창 출력 2020.11.10. KGH
                if (dgdMain.SelectedRows[0].Cells["OutDate"].Value.ToString() != null)
                {
                    for (int i = 0; i < dgdMain.Rows.Count; i++)
                    {
                        if (dgdMain.Rows[i].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                        {
                            //MessageBox.Show("text");
                            WizCommon.Popup.MyMessageBox.ShowBox("선택한 데이터는 모두 출고된 건으로\n 삭제가 불가능합니다.", "[삭제 전]", 0, 1);
                            return true;
                        }
                    }
                }
                // 선택된 라벨이 없을 경우 해당 팝업창 출력
                WizCommon.Popup.MyMessageBox.ShowBox("삭제할 라벨을 선택(체크)해주세요.", "[삭제 전]", 0, 1);
                return false;
            }

            if (OutCnt > 0)
            {
                //if (WizCommon.Popup.MyMessageBox.ShowBox("포장되어 출고된 건 " + OutCnt + "건 을 제외한 " + lstIns.Count + " 건을 모두 삭제하시겠습니까?", "[삭제 전]", 0, 1) == DialogResult.OK)
                if (WizCommon.Popup.MyMessageBox.ShowBox("총 " + lstIns.Count + "건이 삭제 완료 되었습니다.", "[삭제 완료]", 0, 1) == DialogResult.OK)
                {
                    flag = true;
                }
            }
            else
            {
                flag = true;
            }

            return flag;
        }

        #endregion



        private List<DataGridViewRow> lstReprint = new List<DataGridViewRow>(); // 재발행 라벨 리스트

        private bool CheckData()
        {
            bool IsOk = true;
            if (dgdMain.Rows.Count > 0)
            {
                //bool hasCheck = false;
                //foreach (DataGridViewRow dr in dgdMain.Rows)
                //{
                //    string strReprintDate = dr.Cells["BoxID"].Value.ToString().Trim();
                //    bool isChecked = (bool)dr.Cells["Check"].EditedFormattedValue;
                //    if (isChecked)//체크된
                //    {
                //        if (strReprintDate.Equals(""))
                //        {
                //            Message[0] = "[이동전표 확인]";
                //            Message[1] = "이동전표가 없을 경우 재발행되지 않습니다";
                //            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                //            IsOk = false;
                //        }
                //        else
                //        {
                //            hasCheck = true;
                //        }
                //    }
                //}

                //if (!hasCheck)
                //{
                //    Message[0] = "[재발행라벨 선택]";
                //    Message[1] = "재발행할 라벨을 선택해야 합니다";
                //    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                //    IsOk = false;
                //}

                lstReprint.Clear();

                foreach (DataGridViewRow dr in dgdMain.Rows)
                {
                    if ((bool)dr.Cells["Check"].EditedFormattedValue == true)
                    {
                        lstReprint.Add(dr);
                    }
                }

                if (lstReprint.Count == 0)
                {
                    Message[0] = "[재발행라벨 선택]";
                    Message[1] = "재발행할 라벨을 선택해야 합니다";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
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
            bool flag = true;

            List<string> list_Data = null;
            for (int i = 0; i < lstReprint.Count; i++)
            {
                try
                {
                    string[] bLabel = lstReprint[i].Cells["PackBoxID"].Value.ToString().Split(',');

                    for(int k = 0; k < bLabel.Length; k++)
                    {
                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("LabelID", bLabel[k].Trim());//상위품ID

                        DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdIns_sBLabelInfo", sqlParameter, false);

                        DataRow dr = dt.Rows[0];

                        string g_sPrinterName = Lib.GetDefaultPrinter();

                        list_Data = new List<string>();

                        string TagID = "014";

                        list_Data.Add(Lib.CheckNull(dr["InBoxID"].ToString()));// 품번
                        list_Data.Add(Lib.CheckNull(dr["KCustom"].ToString()));// 업체명
                        list_Data.Add(Lib.CheckNull(dr["BuyerArticleNo"].ToString()));// 품번
                        list_Data.Add(Lib.CheckNull(dr["Article"].ToString()));// 품명
                        list_Data.Add(Lib.stringFormatN0(Lib.ConvertDouble(Lib.CheckNull(dr["PackQty"].ToString()))));// 검사수량
                        list_Data.Add(Lib.DatePickerFormat(Lib.CheckNull(dr["PackDate"].ToString())));// 포장일자
                        list_Data.Add(Lib.CheckNull(dr["Name"].ToString()));// 작업자
                        list_Data.Add(Lib.stringFormatN0(Lib.ConvertDouble(Lib.CheckNull(dr["DefectQty"].ToString()))));// 불량수량
                        
                        frm_tprc_Work_U ftWU = new frm_tprc_Work_U();

                        WizWork.TSCLIB_DLL.openport(g_sPrinterName);
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
                        WizWork.TSCLIB_DLL.closeport();
                    }
                }
                catch (Exception ex)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                }
            }

            return flag;

            #region 버림
            //bool IsOK = true;
            //int Count = 0;
            ////if(grdData.Rows.Count == grdData.CurrentRow.Index)
            ////{
            ////    //MessageBox.Show(LoadResString(231));
            ////}

            //list_TWkLabelPrint = new List<WizWork.Sub_TWkLabelPrint>(); // 초기화

            //foreach (DataGridViewRow dr in dgdMain.Rows)
            //{
            //    DataGridViewCell Cell = dr.Cells["Check"];
            //    bool isChecked = (bool)Cell.EditedFormattedValue;
            //    if (isChecked)
            //    {
            //        list_TWkLabelPrint.Add(new WizWork.Sub_TWkLabelPrint());
            //        list_TWkLabelPrint[Count].sLabelID = dr.Cells["LabelID"].Value.ToString();
            //        list_TWkLabelPrint[Count].sReprintDate = dr.Cells["ReprintDate"].Value.ToString().Replace("-", "");
            //        list_TWkLabelPrint[Count].nQtyPerBox = Int32.Parse(Lib.CheckNum(dr.Cells["QtyPerBox"].Value.ToString()).Replace(",", ""));
            //        list_TWkLabelPrint[Count].sCreateuserID = Frm_tprc_Main.g_tBase.PersonID;
            //        list_TWkLabelPrint[Count].sLastProdArticleID = Lib.CheckNull(dr.Cells["LastProdArticleID"].Value.ToString());
            //        list_TWkLabelPrint[Count].sInstID = dr.Cells["InstID"].Value.ToString();
            //        Count++;
            //    }
            //}

            //UpdateWorkCardPrint(Count);

            //return IsOK;
            #endregion
        }



        private void UpdateWorkCardPrint(int nCount)
        {
            
        }
        #region Right - 라벨 재발행
        // Winform btnRePrint button 속성 Visible : false (서강정밀) 추후 타업체 재발행 활성화 필요. 2020.10.30.KGH
        private void btnRePrint_Click(object sender, EventArgs e)
        {
            if (CheckData() == true)
            {
                if (SaveData() == true)
                {
                    FillGrid();
                }

            }
        }
        #endregion

        private void dgdMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (dgdMain.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "FALSE")
                {
                    dgdMain.Rows[e.RowIndex].Cells["Check"].Value = true;
                }
                else if (dgdMain.Rows[e.RowIndex].Cells["Check"].Value.ToString().ToUpper() == "TRUE")
                {
                    dgdMain.Rows[e.RowIndex].Cells["Check"].Value = false;
                }

                setDgdSub(e.RowIndex);

            }
        }

        #region 불량 내역 조회 : FillGridSub()

        private void FillGridSub(string OrderID, int RollSeq)
        {
            try
            {
                lstSub.Clear();

                Dictionary<string, object> sqlParameters = new Dictionary<string, object>();
                sqlParameters.Add("OrderID", OrderID);
                sqlParameters.Add("RollSeq", RollSeq);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_prdIns_sInspectSub]", sqlParameters, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        i++;

                        var Ins = new Frm_tins_InsDefectList()
                        {
                            Num = i,
                            KDefect = dr["KDefect"].ToString().Trim(),
                            DefectQty = Lib.stringFormatN0(dr["DefectQty"])
                        };

                        lstSub.Add(Ins);
                    }
                }

                BindingSource bs = new BindingSource();
                bs.DataSource = lstSub;
                dgdSub.DataSource = bs;
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        #endregion
        #region 서브 그리드  : 불량 내역이 있으면 활성화

        private void setDgdSub(int index)
        {
            // 불량이 있으면 불량 데이터 그리드 활성화
            double DefectQty = Lib.ConvertDouble(dgdMain.Rows[index].Cells["DefectQty"].Value.ToString());
            if (DefectQty > 0)
            {
                string OrderID = dgdMain.Rows[index].Cells["OrderID"].Value.ToString();
                int RollSeq = Lib.ConvertInt(dgdMain.Rows[index].Cells["RollSeq"].Value.ToString());

                /* 불량 내역 조회 */
                FillGridSub(OrderID, RollSeq);

                /* 서브 그리드 세팅 높이 세팅 */
                int rowH = dgdSub.Rows.GetRowsHeight(DataGridViewElementStates.Visible) + 1;
                int headerH = dgdSub.ColumnHeadersHeight;

                dgdSub.Height = rowH + headerH;

                /* dgdSub 좌표 구하기 */

                #region 셀 바로 밑에 보이게 하는 버전
                //// 최대 Y 값.
                //int maxY = dgdMain.Location.Y + dgdMain.Height;

                //// 선택 셀 Bottom 좌표
                //int selCellY = dgdMain.GetCellDisplayRectangle(1, index, false).Bottom + 45; // 왜 45라는 오차가 발생하는지 모르겠음

                //// dgdMain 을 벗어나지 않는다면
                //if (selCellY + dgdSub.Height < maxY)
                //{
                //    dgdSub.Location = new Point(dgdSub.Location.X, selCellY);
                //    //dgdSub.Location = new Point(dgdSub.Location.X, 285);
                //}
                //else // 벗어나면 위로
                //{
                //    selCellY = dgdMain.GetCellDisplayRectangle(1, index, false).Top - dgdSub.Height + 45;

                //    dgdSub.Location = new Point(dgdSub.Location.X, selCellY);
                //}
                #endregion
                #region 선택 셀이 dgdMain 의 절반을 넘어서면 위로 보이도록

                int halfY = (dgdMain.Location.Y + dgdMain.Height) / 2;

                int selCellY = dgdMain.GetCellDisplayRectangle(1, index, false).Bottom + 25; 

                if (selCellY <= halfY)
                {
                    dgdSub.Location = new Point(dgdSub.Location.X, halfY + dgdSub.RowTemplate.Height + 13);
                }
                else
                {
                    dgdSub.Location = new Point(dgdSub.Location.X, halfY - dgdSub.MaximumSize.Height);
                }

                #endregion

                dgdSub.Visible = true;
            }
            else
            {
                dgdSub.Visible = false;
            }
        }

        #endregion

        #region 검사 조회 : FillGrid()

        private void FillGrid()
        {
            try
            {
                // 불량 그리드 숨기기
                dgdSub.Visible = false;

                // 총 검사수량
                int SumInsQty = 0;

                lstMain.Clear();

                Dictionary<string, object> sqlParameters = new Dictionary<string, object>();
                sqlParameters.Add("ChkExamDate", chkDate.Checked == true ? 1 : 0);
                sqlParameters.Add("SDate", mtb_From.Text.Replace("-", ""));
                sqlParameters.Add("EDate", mtb_To.Text.Replace("-", ""));
                sqlParameters.Add("ChkArticle", chkBuyerArticleNo.Checked == true ? 1 : 0);
                sqlParameters.Add("Article", txtBuyerArticleNo.Text.Trim());
                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_prdIns_sInspect]", sqlParameters, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach(DataRow dr in dt.Rows)
                    {
                        i++;
                        SumInsQty += (int)Lib.ConvertDouble(dr["RealQty"].ToString());

                        var Ins = new Frm_tins_Result_Q_CodeView()
                        {
                            ExamDate = Lib.DatePickerFormat(dr["ExamDate"].ToString()),
                            ExamTime = Lib.DateTimeFormat(dr["ExamTime"].ToString()),
                            ArticleID = dr["ArticleID"].ToString().Trim(),
                            BuyerArticleNo = dr["BuyerArticleNo"].ToString(),
                            Article = dr["Article"].ToString(),

                            RealQty = Lib.stringFormatN0(dr["RealQty"]),
                            CtrlQty = Lib.stringFormatN0(dr["CtrlQty"]),
                            DefectQty = Lib.stringFormatN0(dr["DefectQty"]),
                            PersonID = dr["PersonID"].ToString(),
                            Name = dr["Name"].ToString(),

                            BoxID = dr["BoxID"].ToString().Trim(),
                            PackBoxID = dr["PackBoxID"].ToString().Trim(),
                            PackID = dr["PackID"].ToString().Trim(),
                            OutDate = Lib.DatePickerFormat(dr["OutDate"].ToString()),
                            OrderID = dr["OrderID"].ToString().Trim(),

                            RollSeq = dr["RollSeq"].ToString().Trim()
                        };

                        lstMain.Add(Ins);
                    }

                    setSumDgv(i, SumInsQty);
                }

                BindingSource bs = new BindingSource();
                bs.DataSource = lstMain;
                dgdMain.DataSource = bs;
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        private void setSumDgv(int count, int SumQty)
        {
            dgdSum.Rows.Clear();

            dgdSum.Rows.Add("검사 합계"
                                        , Lib.stringFormatN0(count) + " 건"
                                        , Lib.stringFormatN0(SumQty)
                                        );

            dgdSum.CurrentCell.Selected = false;
        }

        #endregion

        
    }

    #region 검사실적 코드뷰 : Frm_tins_Result_Q_CodeView
    public class Frm_tins_Result_Q_CodeView
    {
        //public int Num { get; set; }
        public string ExamDate { get; set; }
        public string ExamTime { get; set; }
        public string ArticleID { get; set; }
        public string BuyerArticleNo { get; set; }
        public string Article { get; set; }

        public string RealQty { get; set; }
        public string CtrlQty { get; set; }
        public string DefectQty { get; set; }
        public string PersonID { get; set; }
        public string Name { get; set; }

        public string BoxID { get; set; }
        public string PackBoxID { get; set; }
        public string OutDate { get; set; }
        public string PackID { get; set; }
        public string OrderID { get; set; }
        public string RollSeq { get; set; }

        public bool Check { get; set; }
    }
    #endregion

    #region 불량내역 코드뷰 : Frm_tins_InsDefectList

    class Frm_tins_InsDefectList
    {
        public int Num { get; set; }
        public string KDefect { get; set; }
        public string DefectQty { get; set; }
    }

    #endregion
}
