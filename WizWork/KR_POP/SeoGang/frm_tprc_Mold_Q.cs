using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizWork.Properties;
using WizWork.Tools;
using WizCommon;

namespace WizWork
{

    public partial class Frm_tprc_Mold_Q : Form
    {
        DataTable dt = null;
        WizWorkLib Lib = Frm_tprc_Main.Lib;
        string[] Message = new string[2];
        private static string m_InstID = "";
        private static string m_sProcess = "";
        private static string m_LotID = "";
        private static string m_Machine = "";

        public string m_sFormName = "";
        bool CheckOk = false;

        public Frm_tprc_Mold_Q()
        {
            InitializeComponent();

        }


        public Frm_tprc_Mold_Q(string strLotID, string strProcess, string strMachine)
        {
            InitializeComponent();
            m_LotID = strLotID;
            m_sProcess = strProcess;
            m_Machine = strMachine;
        }


        public Frm_tprc_Mold_Q(string strInstID, string strsProcess, string strMachine, string strLotID, string strsFormName)
        {
            InitializeComponent();
            m_LotID = strLotID;
            m_InstID = strInstID;
            m_sProcess = strsProcess;
            m_Machine = strMachine;

            m_sFormName = strsFormName;
        }
        private void Frm_tprc_Mold_Q_Load(object sender, EventArgs e)
        {
            try
            {
                CheckOk = true;
                SetScreen();
                InitGrid();
                //InitGrid(); // Grid 초기화
                SetFormClearData();
                btnClose.Text = "취소";
                btnUseMold.Enabled = true;
                FillGrid(m_LotID);
            }
            catch (Exception ex)
            {
                btnClose.Text = "닫기";
            }
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
        }
        #endregion

        private void SetFormClearData()
        {

        }

        private void InitGrid()
        {
            dgvMoldList.Columns.Clear();
            dgvMoldList.ColumnCount = 4;

            int i = 0;

            DataGridViewCheckBoxColumn chkCol = new DataGridViewCheckBoxColumn();
            {
                chkCol.HeaderText = "선택";
                chkCol.Name = "Click";
                chkCol.Width = 110;
                //chkCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                chkCol.FlatStyle = FlatStyle.Standard;
                chkCol.ThreeState = true;
                chkCol.CellTemplate = new DataGridViewCheckBoxCell();
                chkCol.CellTemplate.Style.BackColor = Color.Beige;
                chkCol.Visible = CheckOk;
            }
            dgvMoldList.Columns.Insert(0, chkCol);

            dgvMoldList.Columns[++i].Name = "MoldID";
            dgvMoldList.Columns[i].HeaderText = "금형ID";
            dgvMoldList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMoldList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvMoldList.Columns[i].ReadOnly = true;
            dgvMoldList.Columns[i].Visible = false;

            dgvMoldList.Columns[++i].Name = "LotNo";
            dgvMoldList.Columns[i].HeaderText = "금형LotNO";
            dgvMoldList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMoldList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMoldList.Columns[i].ReadOnly = true;
            dgvMoldList.Columns[i].Visible = true;

            dgvMoldList.Columns[++i].Name = "Article";
            dgvMoldList.Columns[i].HeaderText = "품명";
            dgvMoldList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMoldList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMoldList.Columns[i].ReadOnly = true;
            dgvMoldList.Columns[i].Visible = true;

            dgvMoldList.Columns[++i].Name = "BuyerArticleNo";
            dgvMoldList.Columns[i].HeaderText = "품번";
            dgvMoldList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dgvMoldList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgvMoldList.Columns[i].ReadOnly = true;
            dgvMoldList.Columns[i].Visible = true;

            dgvMoldList.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            dgvMoldList.RowTemplate.Height = 30;
            dgvMoldList.ColumnHeadersHeight = 35;
            dgvMoldList.ScrollBars = ScrollBars.Both;
            dgvMoldList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMoldList.MultiSelect = false;
            dgvMoldList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMoldList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);

            foreach (DataGridViewColumn col in dgvMoldList.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            return;
        }

        private void FillGrid(string LotID)
        {
            dgvMoldList.Rows.Clear();
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("LotNo", LotID);

                dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sMoldbyLotID", sqlParameter, false);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        dgvMoldList.Rows.Add(false,
                                              Lib.CheckNull(dr["MoldID"].ToString()),           //금형명
                                              Lib.CheckNull(dr["MoldNo"].ToString()),           //금형LotNO
                                              Lib.CheckNull(dr["Article"].ToString()),           //품명
                                              Lib.CheckNull(dr["BuyerArticleNo"].ToString())     //품번
                                          );
                    }
                    if (dgvMoldList.Rows.Count > 0)
                    {
                        dgvMoldList.Rows[0].Selected = true;
                    }
                    else
                    {
                        Message[0] = "[검색결과 없음]";
                        Message[1] = "해당 품목에 등록된 금형이 없습니다.";
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 1);
                        //btnUseMold.Enabled = false;
                    }
                }
                else
                {
                    Message[0] = "[검색결과 없음]";
                    Message[1] = "해당 품목에 등록된 금형이 없습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 1);
                    //btnUseMold.Enabled = false;
                }
            }
            catch (Exception excpt)
            {
                Message[0] = "[오류]";
                Message[1] = string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message);
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 2, 1);
                btnUseMold.Enabled = false;
            }
            finally
            {
                DataStore.Instance.CloseConnection();
            }
        }

        private void btnUseMold_Click(object sender, EventArgs e)
        {
            if (GetdgvMoldListCheckValueCount() < 1)
            {
                Message[0] = "[확인]";
                Message[1] = "선택된 금형이 없습니다.금형을 선택하지 않으시겠습니까 ?";
                if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.OK)//NO
                {
                    Frm_tprc_Main.g_tBase.sMold = ""; //2022-01-22
                    Frm_tprc_Main.g_tBase.sMoldID = ""; //2022-01-22
                    Frm_tprc_Main.list_tMold.Clear(); //2024-02-19
                    DialogResult = DialogResult.OK;
                    Dispose();
                    Close();
                }
            }
            else
            {
                /// 금형정보 선택시
                /// 
                SetdgvMoldListCheckValue();
                this.Close();
                DialogResult = DialogResult.OK;
                return;
            }


        }
      
        private void btnClose_Click(object sender, EventArgs e)
        {

            DialogResult = DialogResult.No;
            this.Dispose();
            this.Close();

        }

        private void dgvMoldList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dr = dgvMoldList.SelectedRows[0];
            if (dr.Cells[0].Value.ToString().ToUpper() == "false".ToUpper())
            {
                //SetdgvMoldListAllCheckValue(false);
                dr.Cells[0].Value = true;
            }
            else
            {
                //SetdgvMoldListAllCheckValue(false);
                dr.Cells[0].Value = false;
            }
        }

        private void SetdgvMoldListAllCheckValue(bool blChekValue)
        {
            if (dgvMoldList.RowCount < 1)
            {
                return;
            }
            if (blChekValue.ToString() == "")
            {
                blChekValue = false;
            }
            for (int i = 0; i < dgvMoldList.RowCount; i++)
            {
                dgvMoldList.Rows[i].Cells[0].Value = blChekValue;
            }
            return;
        }

        private int GetdgvMoldListCheckValueCount()
        {
            int intResult = 0;
            int dgvRowCount = 0;
            foreach (DataGridViewRow dgvr in dgvMoldList.Rows)
            {
                if (dgvr.Cells["MoldID"].Value.ToString() != "")
                {
                    dgvRowCount++;
                }
            }

            if (dgvRowCount < 1)
            {
                return intResult;
            }

            for (int i = 0; i < dgvMoldList.RowCount; i++)
            {
                if (dgvMoldList.Rows[i].Cells[1].Value.ToString().Equals(""))
                    intResult = intResult + 1;
                if (dgvMoldList.Rows[i].Cells[0].Value.ToString().ToUpper() == "true".ToUpper())
                {
                    intResult = intResult + 1;
                }
            }
            return intResult;
        }
        /// <summary>
        ///  선택된 금형정보 담기
        /// </summary>
        /// <returns></returns>
        private void SetdgvMoldListCheckValue()
        {
            int nCount = dgvMoldList.RowCount;
            int sCount = 0;
            Frm_tprc_Main.list_tMold = new List<TMold>();

            if (nCount < 1)
            {
                return;
            }

            for (int i = 0; i < nCount; i++)
            {
                if (dgvMoldList.Rows[i].Cells["Click"].Value.ToString().ToUpper() == "TRUE".ToUpper())
                {
                    sCount++;

                    #region 금형 무조건 하나면 주석 풀고 이걸로 처리 2022-10-18
                    //Frm_tprc_Main.list_tMold.Add(Frm_tprc_Main.g_tMold);

                    //Frm_tprc_Main.g_tBase.sMold = dgvMoldList.Rows[i].Cells["LotNo"].Value.ToString();
                    //Frm_tprc_Main.g_tBase.sMoldID = dgvMoldList.Rows[i].Cells["MoldID"].Value.ToString();

                    //Frm_tprc_Main.list_tMold[i].sMoldID = dgvMoldList.Rows[i].Cells["MoldID"].Value.ToString();
                    //Frm_tprc_Main.list_tMold[i].sLotNo = dgvMoldList.Rows[i].Cells["LotNo"].Value.ToString();
                    #endregion

                    //금형 여러개 일 경우를 대비 2022-10-18
                    Frm_tprc_Main.list_tMold.Add(new TMold() { sMoldID = dgvMoldList.Rows[i].Cells["MoldID"].Value.ToString(), sLotNo = dgvMoldList.Rows[i].Cells["LotNo"].Value.ToString()});

                }
            }

            return;
        }

    }
}

