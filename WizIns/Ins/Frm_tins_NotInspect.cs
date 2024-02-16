using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;
using WizWork;
using WizWork.Properties;

namespace WizIns
{
    public partial class Frm_tins_NotInspect : Form
    {
        INI_GS gs = Frm_tprc_Main.gs;
        WizWorkLib Lib = new WizWorkLib();

        // 메인 데이터 그리드 뷰 관리 객체
        List<Frm_tins_Order_Q_CodeView> lstMain = new List<Frm_tins_Order_Q_CodeView>();

        // 검사할 리스트
        List<Frm_tins_Order_Q_CodeView> lstIns = new List<Frm_tins_Order_Q_CodeView>();

        /// <summary>
        /// 생성
        /// </summary>
        public Frm_tins_NotInspect()
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

        private void Frm_tins_NotInspect_Load(object sender, EventArgs e)
        {
            SetScreen();

            // 오늘 날짜 세팅
            mtb_From.Text = DateTime.Today.ToString("yyyyMMdd");
            mtb_To.Text = DateTime.Today.ToString("yyyyMMdd");

            FillGrid();
        }

        private void Frm_tins_NotInspect_Activated(object sender, EventArgs e)
        {

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

                dgdMain.Refresh();
            }
        }

        #region 검사 조회 : FillGrid()

        private void FillGrid()
        {
            try
            {
                // 총 작업수량
                int SumWorkQty = 0;
                int SumNoInsQty = 0;

                lstMain.Clear();

                Dictionary<string, object> sqlParameters = new Dictionary<string, object>();
                sqlParameters.Add("ChkWorkDate", chkDate.Checked == true ? 1 : 0);
                sqlParameters.Add("SDate", mtb_From.Text.Replace("-", ""));
                sqlParameters.Add("EDate", mtb_To.Text.Replace("-", ""));
                sqlParameters.Add("ChkArticle", chkBuyerArticleNo.Checked == true ? 1 : 0);
                sqlParameters.Add("Article", txtBuyerArticleNo.Text.Trim());
                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_prdIns_sNoInspect]", sqlParameters, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    int i = 0;
                    foreach(DataRow dr in dt.Rows)
                    {
                        i++;
                        SumWorkQty += (int)Lib.ConvertDouble(dr["WorkQty"].ToString());
                        SumNoInsQty += (int)Lib.ConvertDouble(dr["NoInspectQty"].ToString());

                        var Scan = new Frm_tins_Order_Q_CodeView()
                        {
                            Num = i,
                            OrderID = dr["OrderID"].ToString(),
                            LabelID = dr["LabelID"].ToString().Trim(),
                            LabelGubun = dr["LabelGubun"].ToString(),
                            ProcessID = dr["ProcessID"].ToString(),
                            ArticleID = dr["ArticleID"].ToString(),
                            KCustom = dr["KCustom"].ToString(),
                            Article = dr["Article"].ToString(),
                            BuyerArticleNo = dr["BuyerArticleNo"].ToString(),
                            Model = dr["Model"].ToString(),
                            OrderQty = Lib.stringFormatN0(dr["OrderQty"]),
                            WorkQty = Lib.stringFormatN0(dr["WorkQty"]),
                            NoInspectQty = Lib.stringFormatN0(dr["NoInspectQty"]),
                            WorkDate = Lib.DatePickerFormat(dr["WorkEndDate"].ToString())
                        };

                        lstMain.Add(Scan);
                    }

                    setSumDgv(i, SumWorkQty, SumNoInsQty);
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

        private void setSumDgv(int count, int SumWorkQty, int SumNoInsQty)
        {
            dgdSum.Rows.Clear();

            dgdSum.Rows.Add("작업량 / 미검사량 합계"
                                        , Lib.stringFormatN0(count) + " 건"
                                        , Lib.stringFormatN0(SumWorkQty)
                                        , Lib.stringFormatN0(SumNoInsQty)
                                        );

            dgdSum.CurrentCell.Selected = false;
        }

        #endregion

        #region 검사 등록 전 체크 + 검사할 리스트 세팅 To lstIns : CheckBeforeStart()
        private bool CheckBeforeStart()
        {
            lstIns = new List<Frm_tins_Order_Q_CodeView>();

            // 품명이 같은 것만 검사 및 포장 가능
            string ArticleID = "";
            for (int i = 0; i < dgdMain.Rows.Count; i++)
            {
                var Main = dgdMain.Rows[i].DataBoundItem as Frm_tins_Order_Q_CodeView;
                if (Main != null
                    && Main.Check == true)
                {
                    if (ArticleID.Equals(""))
                    {
                        ArticleID = Main.ArticleID;
                    }
                    else
                    {
                        if (!ArticleID.Equals(Main.ArticleID))
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox("품명이 같은것만 선택해주세요.", "[검사 등록 전]", 0, 1);
                            return false;
                        }
                    }
                    lstIns.Add(Main);
                }
            }

            if (lstIns.Count <= 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("검사 등록할 라벨을 선택(체크)해주세요.", "[검사 등록 전]", 0, 1);
                return false;
            }

            return true;
        }
        #endregion

        // 검사 등록 버튼 클릭 이벤트
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (CheckBeforeStart() == false) { return; }

            #region 포장 라벨이 새롭개 나오는 경우
            //Frm_PopUp_Packing pack = new Frm_PopUp_Packing(lstIns);
            //pack.ShowDialog();

            //if (pack.DialogResult == DialogResult.OK)
            //{
            //    FillGrid();
            //}
            #endregion

            #region 생산라벨 그대로 포장 하는 경우 (서강정밀)
            Frm_PopUp_PackingAndOutBox pack = new Frm_PopUp_PackingAndOutBox(lstIns);
            pack.ShowDialog();

            if (pack.DialogResult == DialogResult.OK)
            {
                FillGrid();
            }
            #endregion
        }

        // 전체 선택 체크 이벤트
        private void btnAllCheck_Click(object sender, EventArgs e)
        {
            Button btnSender = sender as Button;
            if (btnSender.Text.Equals("전체해제"))
            {
                for (int i = 0; i < lstMain.Count; i++)
                {
                    lstMain[i].Check = false;
                }
                btnSender.Text = "전체선택";
            }
            else
            {
                for (int i = 0; i < lstMain.Count; i++)
                {
                    lstMain[i].Check = true;
                }
                btnSender.Text = "전체해제";
            }

            BindingSource bs = new BindingSource();
            bs.DataSource = lstMain;
            dgdMain.DataSource = bs;
        }
    }
}
