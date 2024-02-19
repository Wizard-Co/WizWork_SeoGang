using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizWork.Properties;
using WizCommon;

namespace WizWork
{
    
    public partial class Frm_tprc_MoldDetail_Q : Form
    {
        string[] Message = new string[2];
        private string m_BuyerModel = "";
        private string m_MoldID = "";
        private string m_Article = "";
        private string m_MoldLotNo = "";
        private string m_BuyerArticleNo = "";
        private string m_MoldName = "";
        private string m_MoldKindID = "";

        WizWorkLib Lib = new WizWorkLib();

        public Frm_tprc_MoldDetail_Q()
        {
            InitializeComponent();
           
        }
        public Frm_tprc_MoldDetail_Q(string strMoldLotNo, string strMoldID, string strArticle, string strBuyerModel, string strBuyerArticleNo, string strMoldName, string strMoldKindID)
        {
            InitializeComponent();
            m_MoldLotNo = strMoldLotNo;
            m_MoldID = strMoldID;
            m_Article = strArticle;
            m_BuyerModel = strBuyerModel;
            m_BuyerArticleNo = strBuyerArticleNo;
            m_MoldName = strMoldName;
            m_MoldKindID = strMoldKindID;
        }

        private void Frm_tprc_MoldDetail_Q_Load(object sender, EventArgs e)
        {
            SetScreen();
            InitGrid();

            if (m_MoldKindID != "" && m_MoldID != "")
            {
              txtMoldNo.Text = m_MoldLotNo;
              txtBuyerArticleNo.Text = m_BuyerArticleNo;
              txtArticle.Text =m_Article;
              txtBuyerModel.Text = m_BuyerModel;
              procQuery();
            }
            else
            {
                //this.txtMoldKind.Text = "품명";
                //this.txtLotNo.Text = "금영LotNo";
            }
        }

        private void InitGrid()
        {
            grdData.Columns.Clear();
            grdData.ColumnCount = 9;

            int i = 0;

            // Set the Colums Hearder Names


            grdData.Columns[i].Name = "RowSeq";
            grdData.Columns[i].HeaderText = "No.";
            grdData.Columns[i].Width = 30;
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;

            grdData.Columns[i].Name = Work_sMoldbyLotID.IODATE;
            grdData.Columns[i].HeaderText = "일자";
            grdData.Columns[i].Width = 100;
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;

            grdData.Columns[i].Name = Work_sMoldbyLotID.GBNNAME;
            grdData.Columns[i].HeaderText = "구분";
            grdData.Columns[i].Width = 90;
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;

            grdData.Columns[i].Name = Work_sMoldbyLotID.INQTY;
            grdData.Columns[i].HeaderText = "입고량";
            grdData.Columns[i].Width = 70;
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;

            grdData.Columns[i].Name = Work_sMoldbyLotID.OUTQTY;
            grdData.Columns[i].HeaderText = "출고량";
            grdData.Columns[i].Width = 70;
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;

            grdData.Columns[i].Name = Work_sMoldbyLotID.STOCKQTY;
            grdData.Columns[i].HeaderText = "재고량";
            grdData.Columns[i].Width = 70;
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;


            grdData.Columns[i].Name = Work_sMoldbyLotID.PRODQTY;
            grdData.Columns[i].HeaderText = "제품생산량";
            grdData.Columns[i].Width = 90;
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;

            grdData.Columns[i].Name = Work_sMoldbyLotID.PROCESS;
            grdData.Columns[i].HeaderText = "공정";
            grdData.Columns[i].Width = 100;
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;

            grdData.Columns[i].Name = Work_sMoldbyLotID.MACHINENAME;
            grdData.Columns[i].HeaderText = "설비명";
            grdData.Columns[i].Width = 100;
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i++].Visible = true;

            grdData.Font = new Font("맑은 고딕", 12);
            grdData.RowsDefaultCellStyle.Font = new Font("맑은 고딕", 12);
            grdData.RowTemplate.Height = 30;
            grdData.ColumnHeadersHeight = 35;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;


        }

        public void procQuery()
        {

            DataRow dr = null;
            DataGridViewRow row = null;
            grdData.Rows.Clear();
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add(Work_sMoldbyLotID.CHKDATE, 0);
                sqlParameter.Add(Work_sMoldbyLotID.FROMDATE, "");
                sqlParameter.Add(Work_sMoldbyLotID.TODATE, "");
                sqlParameter.Add(Work_sMoldbyLotID.NCHKMOLDKIND, 1 );
                sqlParameter.Add(Work_sMoldbyLotID.MOLDKIND, m_MoldKindID);

                sqlParameter.Add(Work_sMoldbyLotID.NCHKMOLD, 1);
                sqlParameter.Add(Work_sMoldbyLotID.MOLDNO, m_MoldID);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_dvlMold_sMoldTrackLotNoDetailList", sqlParameter, false);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        
                        grdData.Rows.Add(i+1,
                                                 dr[Work_sMoldbyLotID.IODATE].ToString().Substring(0, 4) + "-" + dr[Work_sMoldbyLotID.IODATE].ToString().Substring(4, 2) + "-" + dr[Work_sMoldbyLotID.IODATE].ToString().Substring(6, 2),               //일자
                                                 dr[Work_sMoldbyLotID.GBNNAME],              //구분
                                                 string.Format("{0:n0}", (int)(Lib.GetDouble(dr[Work_sMoldbyLotID.INQTY].ToString()))), 
                                                 string.Format("{0:n0}", (int)(Lib.GetDouble(dr[Work_sMoldbyLotID.OUTQTY].ToString()))),
                                                 string.Format("{0:n0}", (int)(Lib.GetDouble(dr[Work_sMoldbyLotID.STOCKQTY].ToString()))),
                                                 string.Format("{0:n0}", (int)(Lib.GetDouble(dr[Work_sMoldbyLotID.PRODQTY].ToString()))), 
                                                 dr[Work_sMoldbyLotID.PROCESS].ToString().Trim(),              //공정
                                                 dr[Work_sMoldbyLotID.MACHINENAME].ToString().Trim()           //MachineNo
                                                  
                                          );


                        row = grdData.Rows[i];
                        row.Height = 28;
                    }
                    grdData.ClearSelection();
                }
            }
            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #region TableLayoutPanel 하위 컨트롤들의 DockStyle.Fill 세팅
        private void SetScreen()
        {
            pnlForm.Dock = DockStyle.Fill;
            pnlForm.Margin = new Padding(0, 0, 0, 0);
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
        }
        #endregion

        private void btnUp_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowUp(grdData);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowDown(grdData);
        }

        private void Frm_tprc_MoldDetail_Q_Activated(object sender, EventArgs e)
        {
            //2022-01-22
            //Main 폼에서 클릭한 거와 작업지시, 작업자 선택 후 나오는 화면 금형선택화면 2개라 조건 생성
            if (Owner is null)
            {

            }
            else
            {
                ((Frm_tprc_Main)(Owner)).LoadRegistry();
            }
        }
    }
}
