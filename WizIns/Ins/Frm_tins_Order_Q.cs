using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizWork.Properties;
using WizCommon;
using System.ComponentModel;
using WizWork.GLS.PopUp;

//*******************************************************************************
//프로그램명    Frm_tins_Order_Q.cs
//메뉴ID        
//설명          Frm_tins_Order_Q 메인소스입니다.
//작성일        2019.08.01
//개발자        허윤구
//*******************************************************************************
// 변경일자     변경자      요청자      요구사항ID          요청 및 작업내용
//*******************************************************************************

//*******************************************************************************

namespace WizIns
{
    public partial class Frm_tins_Order_Q : Form
    {
        string[] Message = new string[2];
        INI_GS gs = new INI_GS();
        WizWorkLib Lib = new WizWorkLib();

        // 메인 데이터 그리드 뷰 관리 객체
        List<Frm_tins_Order_Q_CodeView> lstMain = new List<Frm_tins_Order_Q_CodeView>();

        // 검사할 리스트
        List<Frm_tins_Order_Q_CodeView> lstIns = new List<Frm_tins_Order_Q_CodeView>();

        public Frm_tins_Order_Q()
        {
            InitializeComponent();

            InitPanel();
        }

        #region InitPanel

        private void InitPanel()
        {
            pnlForm.Dock = DockStyle.Fill;
            tlpForm.Dock = DockStyle.Fill;
            foreach (Control control in tlpForm.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(1, 1, 1, 1);
                foreach (Control ctl in control.Controls)//tlp 상위에서 3번째
                {
                    ctl.Dock = DockStyle.Fill;
                    ctl.Margin = new Padding(1, 1, 1, 1);
                    foreach (Control con in ctl.Controls)
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
                                foreach (Control contro in c.Controls)
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
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        private void Frm_tins_Order_Q_Load(object sender, EventArgs e)
        {
            // 바코드 초기화 및 포커스
            txtLabelID.Text = "";           
        }

        private void Frm_tins_Order_Q_Activated(object sender, EventArgs e)
        {
            txtLabelID.Focus();
        }

        #region Right 버튼 클릭 이벤트

        // 닫기 버튼 클릭 이벤트
        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

            #region 포장 라벨이 새롭게 나오는 경우
            //Frm_PopUp_Packing pack = new Frm_PopUp_Packing(lstIns);
            //pack.ShowDialog();

            //if (pack.DialogResult == DialogResult.OK)
            //{
            //    dgdMain.Rows.Clear();

            //    txtLabelID.Text = "";
            //    txtLabelID.Focus();
            //}
            #endregion

            #region 생산라벨 그대로 포장 하는 경우 (서강정밀)
            Frm_PopUp_PackingAndOutBox pack = new Frm_PopUp_PackingAndOutBox(lstIns);
            pack.ShowDialog();

            if (pack.DialogResult == DialogResult.OK)
            {
                dgdMain.Rows.Clear();

                txtLabelID.Text = "";
                txtLabelID.Focus();
            }
            #endregion
        }

        #endregion

        // (실제는 체크, 모양은 버튼) 체크형 버튼들 > 체크하더라도 본 색깔 유형 그대로 유지하도록.
        private void checkBox_CheckedPrevent(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked == true)
            {
                ((CheckBox)sender).Checked = false;
            }
            else
            {
                ((CheckBox)sender).Checked = false;
            }
        }

        #region Top 라벨(바코드) 입력 팝업창 모음.

        // 작업수량 기입 팝업창 생성
        private void chkLabelID_Click(object sender, EventArgs e)
        {
            loadKeyboard(txtLabelID, "라벨");

            if (txtLabelID.Text.Trim().Length > 0)
            {
                BarcodeScan(txtLabelID.Text.Trim().ToUpper());
                txtLabelID.Text = "";
                txtLabelID.Focus();
            }
        }

        // 작업수량 기입 팝업창 생성
        private void txtLabelID_Click(object sender, EventArgs e)
        {
            loadKeyboard(txtLabelID, "라벨");

            if (txtLabelID.Text.Trim().Length > 0)
            {
                BarcodeScan(txtLabelID.Text.Trim().ToUpper());
                txtLabelID.Text = "";
                txtLabelID.Focus();
            }
        }

        // 입력창 팝업 띄우기
        private void loadKeyboard(TextBox txtSender, string title)
        {
            txtSender.Text = "";
            WizWork.POPUP.Frm_CMKeypad keypad = new WizWork.POPUP.Frm_CMKeypad(title + "입력", title);

            keypad.Owner = this;
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                txtSender.Text = keypad.tbInputText.Text;
            }
        }

        private void txtLabelID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                BarcodeScan(txtLabelID.Text.Trim().ToUpper());
                txtLabelID.Text = "";
                txtLabelID.Focus();
            }
        }

        #endregion

        #region Top 바코드 스캔 전 중복 검사 : CheckScanData()

        private bool CheckScanData(string LabelID)
        {
            // 바코드 길이
            if (LabelID.Length != 11)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("바코드 문자 길이를 확인해주세요.\r\n[ex) C2009160001 : 11자리]", "[바코드 오류]", 0, 1);
                return false;
            }

            // 데이터 그리드 뷰에서 중복 체크
            for(int i = 0; i < dgdMain.Rows.Count; i++)
            {
                string arrLabelID = Lib.CheckNull(dgdMain.Rows[i].Cells["LabelID"].Value.ToString().Trim());
                if (LabelID.Equals(arrLabelID))
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("해당 라벨은 이미 스캔하셨습니다.", "[중복 오류]", 0, 1);
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Top 바코드 스캔 → 조회 : BarcodeScan()

        private void BarcodeScan(string LabelID)
        {
            try
            {
                if (CheckScanData(LabelID) == false) { return; }

                // 해당 라벨이 이미 등록 되었다면!

                //앞공정의 실적을 체크한다. 없을 시 close한다. ex)2차가류에 데이터가 없는데, 
                Dictionary<string, object> sqlParameters = new Dictionary<string, object>();
                sqlParameters.Add("LabelID", LabelID);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_prdIns_swkResult]", sqlParameters, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    var Scan = new Frm_tins_Order_Q_CodeView()
                    {
                        Num = dgdMain.Rows.Count + 1,
                        OrderID = dr["OrderID"].ToString(),
                        LabelID = dr["LabelID"].ToString().Trim().ToUpper(),
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

                        Check = true
                    };

                    lstMain.Add(Scan);

                    BindingSource bs = new BindingSource();
                    bs.DataSource = lstMain;
                    dgdMain.DataSource = bs;
                }
                else
                {
                    //앞공정의 실적을 체크한다. 없을 시 close한다. ex)2차가류에 데이터가 없는데, 
                    sqlParameters = new Dictionary<string, object>();
                    sqlParameters.Add("LabelID", LabelID);
                    dt = DataStore.Instance.ProcedureToDataTable("[xp_prdIns_sCheckLabelID]", sqlParameters, false);

                    if (dt != null
                        && dt.Rows.Count > 0
                        && dt.Columns.Count == 2)
                    {
                        DataRow dr = dt.Rows[0];

                        string Title = dr["Title"].ToString();
                        string Msg = dr["Msg"].ToString().Replace("|", "\r\n");

                        WizCommon.Popup.MyMessageBox.ShowBox(Msg, Title, 0, 1);
                    }
                    else
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("해당 라벨 정보를 찾을 수 없습니다.", "[오류]", 0, 1);
                    }
                }

            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        #endregion

        #region Content 메인 데이터 그리드 클릭 이벤트
        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
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
            }
        }

        #endregion

        #region 목록 전체 지우기 : btnDeleteAll_Click(), 선택 지우기 : btnDelete_Click() 이벤트 

        // 목록 전체 지우기
        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (dgdMain.Rows.Count > 0
                && WizCommon.Popup.MyMessageBox.ShowBox("스캔한 라벨 목록을 모두 초기화 하시겠습니까?", "[초기화 전]", 0, 0) == DialogResult.OK)
            {
                lstMain.Clear();

                BindingSource bs = new BindingSource();
                bs.DataSource = lstMain;
                dgdMain.DataSource = bs;
            }
        }

        // 선택 지우기
        private void btnDelete_Click(object sender, EventArgs e)
        {
            lstIns = new List<Frm_tins_Order_Q_CodeView>();

            if (CheckBeforeDelete() == false) { return; }

            if (WizCommon.Popup.MyMessageBox.ShowBox("선택한 라벨 " + lstIns.Count + " 건을 모두 삭제하시겠습니까?", "[삭제 전]", 0, 0) == DialogResult.OK)
            {
                for (int i = 0; i < lstIns.Count; i++)
                {
                    if (lstMain.Contains(lstIns[i])) { lstMain.Remove(lstIns[i]); }

                    BindingSource bs = new BindingSource();
                    bs.DataSource = lstMain;
                    dgdMain.DataSource = bs;
                }
            }
        }

        #region 삭제 전 체크 : CheckBeforeDelete()
        private bool CheckBeforeDelete()
        {
            if (dgdMain.Rows.Count <= 0)
            {
                return false;
            }

            lstIns = new List<Frm_tins_Order_Q_CodeView>();

            for (int i = 0; i < dgdMain.Rows.Count; i++)
            {
                var Main = dgdMain.Rows[i].DataBoundItem as Frm_tins_Order_Q_CodeView;
                if (Main != null
                    && Main.Check == true)
                {
                    lstIns.Add(Main);
                }
            }

            if (lstIns.Count <= 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("삭제할 라벨을 선택(체크)해주세요.", "[삭제 전]", 0, 1);
                return false;
            }

            return true;
        }
        #endregion

        #endregion

        #region 위 아래 버튼 클릭 이벤트 모음

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

    #region 전수검사 코드뷰 : Frm_tins_Order_Q_CodeView

    public class Frm_tins_Order_Q_CodeView
    {
        public int Num { get; set; }

        public string OrderID { get; set; }
        public string LabelID { get; set; }
        public string LabelGubun { get; set; }
        public string ProcessID { get; set; }
        public string ArticleID { get; set; }
        public string KCustom { get; set; }
        public string Article { get; set; }
        public string BuyerArticleNo { get; set; }
        public string Model { get; set; }
        public string OrderQty { get; set; }
        public string WorkQty { get; set; }
        public string NoInspectQty { get; set; }
        public string WorkDate { get; set; }

        public bool Check { get; set; }
    }

    #endregion
}
