using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using WizCommon;

namespace WizWork
{
    public partial class Frm_PopUpSel_sArticle_ByScan : Form
    {
        WizWorkLib Lib = new WizWorkLib();
        public delegate void TextEventHandler(string sArticleID, string sArticle, string sOrderArticleID);                // string을 반환값으로 갖는 대리자를 선언합니다.
        //public event TextEventHandler WriteTextEvent;           // 대리자 타입의 이벤트 처리기를 설정합니다.
        DataTable dt = null; //화면 로드 시 전체 dt 가져옴. 전역변수로 검색 시 마다 dt에서 조건 줘서 검색함.
        //DataTable tempdt = null; //조건 검색 row 담을 dt
        DataRow[] dr = null; //조건 검색용 datarow, 매검색시 마다 담아서 사용

        string LabelID = "";

        public string AddMessage = "";

        public string returnArticleID = "";
        public string returnArticle = "";
        public string returnBuyerArticleNo = "";

        public Frm_PopUpSel_sArticle_ByScan()
        {
            InitializeComponent();
        }

        public Frm_PopUpSel_sArticle_ByScan(string LotID)
        {
            InitializeComponent();
            this.LabelID = LotID;

            lblTitle.Text = LotID + " 라벨 품명 리스트";
        }

        private void Frm_PopUpSel_sArticle_Load(object sender, EventArgs e)
        {
            lblTitle.Text += AddMessage;

            Text = "품명/품번";
            Size = new Size(600, 499);
            InitGrid();
            SetScreen();
            FillGrid();
        }
        //tableLayoutPanel 세팅
        private void SetScreen()
        {
            pnlForm.Dock = DockStyle.Fill;
            pnlForm.Margin = new Padding(0, 0, 0, 0);
            foreach (Control control in pnlForm.Controls)
            {
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0, 0, 0, 0);
                foreach (Control contro in control.Controls)
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
                            }
                        }
                    }
                }
            }
        }

        //그리드 컬럼 셋팅
        private void InitGrid()
        {
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdData.ColumnCount = 6;

            int i = 0;

            grdData.Columns[i].Name = "IDX";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].Width = 80;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "ArticleID";
            grdData.Columns[i].HeaderText = "ArticleID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "BuyerArticleNo";
            grdData.Columns[i].HeaderText = "품번";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Article";
            grdData.Columns[i].HeaderText = "품명";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "RemainQty";
            grdData.Columns[i].HeaderText = "잔량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "UnitClssName";
            grdData.Columns[i].HeaderText = "단위";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        //그리드에 콤보박스 인덱스(부서)에 따른 사용자 세팅
        private void FillGrid()
        {
            try
            { 

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("LabelID", LabelID);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sArticleStockQty_ByLabelID", sqlParameter, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    int index = 0;
                    foreach (DataRow dr in dt.Rows)
                    {

                        index++;
                        grdData.Rows.Add(index,
                                    dr["ArticleID"].ToString(),
                                    dr["BuyerArticleNo"].ToString(),
                                    dr["Article"].ToString(),
                                    stringFormatN2(dr["RemainQty"]),
                                    dr["UnitClssName"].ToString()
                                    );
                    }
                }
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
        }
        //닫기
        private void btnClose_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }
        private void btnUp_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowUp(grdData);
            txtArticle.Select();
            txtArticle.Focus();
        }
        private void btnDown_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowDown(grdData);
            txtArticle.Select();
            txtArticle.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (grdData.Rows.Count == 0 || grdData.SelectedRows.Count == 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("선택된 품명 또는 사번이 없습니다. 품명 또는 사번을 선택해주세요.", "[오류]", 0, 1);
                return;
            }
            this.returnArticleID = grdData.SelectedRows[0].Cells["ArticleID"].Value.ToString();
            this.returnArticle = grdData.SelectedRows[0].Cells["Article"].Value.ToString();
            this.returnBuyerArticleNo = grdData.SelectedRows[0].Cells["BuyerArticleNo"].Value.ToString();

            this.DialogResult = DialogResult.OK;
            Dispose();
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Dispose();
            Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            FindFillGrid();
            txtArticle.Select();
            txtArticle.Focus();
        }

        private void txtArticle_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                FindFillGrid();
            }
        }

        private void FindFillGrid()
        {
            try
            {         
                dr = dt.Select("BuyerArticleNo like '%" + txtArticle.Text + "%' or Article like '%" + txtArticle.Text + "%'" , "BuyerArticleNo DESC, ArticleID DESC");
                if (dr.Length > 0)
                {
                    grdData.Rows.Clear();
                    int i = 0;
                    foreach (DataRow drw in dr)
                    {
                        grdData.Rows.Add(++i,
                                        drw["ArticleID"].ToString(),
                                        
                                        drw["BuyerArticleNo"].ToString(),
                                        drw["Article"].ToString()
                                        );
                    }
                }
                
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
        }

        private void chkArticle_Click(object sender, EventArgs e)
        {
            //실행중인 프로세스가 없을때 
            if (!Frm_tprc_Main.Lib.ReturnProcessRunStop("osk"))
            {
                System.Diagnostics.Process ps = new System.Diagnostics.Process();
                ps.StartInfo.FileName = "osk.exe";
                ps.Start();
            }
            txtArticle.Select();
            txtArticle.Focus();
        }


        #region 기타 메서드 모음

        // 천마리 콤마, 소수점 버리기
        private string stringFormatN0(object obj)
        {
            return string.Format("{0:N0}", obj);
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

        #endregion
    }
}

