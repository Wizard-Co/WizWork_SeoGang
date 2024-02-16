using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WizWork
{
    public partial class frm_mtr_RemainQtyMoveByLotID2_U : Form
    {
        string[] Message = new string[2];
        List<TSTUFFIN> list_TStuffin = null;
        List<TSTUFFINSUB> list_TStuffinSub = null;
        List<TOUTWARE> list_TOutware = null;
        List<TOUTWARESUB> list_TOutwareSub = null;
        List<WizCommon.Procedure> Prolist = null;
        List<WizCommon.Procedure> Prolist2 = null;
        List<Dictionary<string, object>> ListParameter = null;
        List<Dictionary<string, object>> ListParameter2 = null;

        // B_Lot에 들어있는 자식들의 총 잔량 기록장치.
        // 잔량값이 변하더라도 이 값이 유지되어 초과량을 비교할 수 있는 잣대가 된다.
        List<decimal> list_B_Lot_TotalRemainQty = new List<decimal>();


        string m_BarCode = "";
        string StuffDate = "";
        DataStore dataStore = new DataStore();
        WizWorkLib Lib = new WizWorkLib();
        public frm_mtr_RemainQtyMoveByLotID2_U()
        {
            InitializeComponent();
        }
        
        private void frm_mtr_RemainQtyMoveByLotID2_U_Load(object sender, EventArgs e)
        {
            SetScreen();
            ClearData();
            rbn_ALot.Checked = true;
            rbn_GrdALot.Checked = true;
            txtBarCode.Select();
            txtBarCode.Focus();
        }

        private void ClearData()
        {
            lblArticle.Text = string.Empty;
            lblArticle.Tag = string.Empty;
            txtBarCode.Text = string.Empty;
            lblBuyerArticleNo.Text = string.Empty;
            StuffDate = "";
            lblSetupQuantity.Text = "0.00";

            //rbn_ALot.Checked = true;
            //rbn_GrdALot.Checked = true;

            grd_BLotSum.Rows.Clear();
            grd_FIFOList.Rows.Clear();
            grd_ALot.Rows.Clear();
            grd_BLot.Rows.Clear();
            //lbl_Explain.Text = "Lot간 잔량이동처리란? 두개의 Lot를 하나의 Lot로 합치는 작업입니다.\r\n" +
            //    "A Lot와 B Lot를 합쳐서 하나의 A 또는 B Lot로 만들어줍니다.\r\n스캔버튼을 누르거나 스캔옆의 " +
            //    "흰색배경의 칸에 Lot를 입력하거나, 왼쪽의 선입선출 대상에서 A Lot와 B Lot를 선택하여 합칠 수 있습니다.";

            //rbn_ALot.Checked = true;

            InitGrid();
            FillGrdBLotSum();
        }

        private void EnabledFalse()
        {
            chkLotID.Enabled = false;
            txtBarCode.Enabled = false;
            btnSave.Enabled = false;
            btnInit.Enabled = false;
        }

        private void EnabledTrue()
        {
            chkLotID.Enabled = true;
            txtBarCode.Enabled = true;
            btnSave.Enabled = true;
            btnInit.Enabled = true;
        }

        private void SetScreen()
        {
            //패널 배치 및 조정          
            pnlForm.Dock = DockStyle.Fill;
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

        #region 그리드뷰 컬럼 셋팅
        private void InitGrid()
        {
            ///선입선출 순서별 롯트 목록 grdData///
            grd_FIFOList.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grd_FIFOList.ColumnCount = 10;

            int i = 0;

            grd_FIFOList.Columns[i].Name = "No";
            grd_FIFOList.Columns[i].HeaderText = "";
            grd_FIFOList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            //grd_FIFOList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grd_FIFOList.Columns[i].Width = 40;
            grd_FIFOList.Columns[i].ReadOnly = true;
            grd_FIFOList.Columns[i].Visible = true;

            grd_FIFOList.Columns[++i].Name = "LotID";
            grd_FIFOList.Columns[i].HeaderText = "LotID";
            grd_FIFOList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //grd_FIFOList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grd_FIFOList.Columns[i].Width = 150;
            grd_FIFOList.Columns[i].ReadOnly = true;
            grd_FIFOList.Columns[i].Visible = true;

            grd_FIFOList.Columns[++i].Name = "LabelGubun";
            grd_FIFOList.Columns[i].HeaderText = "LabelGubun";
            grd_FIFOList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_FIFOList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grd_FIFOList.Columns[i].ReadOnly = true;
            grd_FIFOList.Columns[i].Visible = false;

            grd_FIFOList.Columns[++i].Name = "OrderID";
            grd_FIFOList.Columns[i].HeaderText = "OrderID";
            grd_FIFOList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_FIFOList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grd_FIFOList.Columns[i].ReadOnly = true;
            grd_FIFOList.Columns[i].Visible = false;

            grd_FIFOList.Columns[++i].Name = "RemainQty";
            grd_FIFOList.Columns[i].HeaderText = "잔량";
            grd_FIFOList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grd_FIFOList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grd_FIFOList.Columns[i].ReadOnly = true;
            grd_FIFOList.Columns[i].Visible = true;

            grd_FIFOList.Columns[++i].Name = "StuffDate";
            grd_FIFOList.Columns[i].HeaderText = "입고일";
            grd_FIFOList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //grd_FIFOList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grd_FIFOList.Columns[i].Width = 110;
            grd_FIFOList.Columns[i].ReadOnly = true;
            grd_FIFOList.Columns[i].Visible = true;

            grd_FIFOList.Columns[++i].Name = "LocID";
            grd_FIFOList.Columns[i].HeaderText = "마지막창고ID";
            grd_FIFOList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_FIFOList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grd_FIFOList.Columns[i].ReadOnly = true;
            grd_FIFOList.Columns[i].Visible = false;

            grd_FIFOList.Columns[++i].Name = "UnitClssName";
            grd_FIFOList.Columns[i].HeaderText = "단위";
            grd_FIFOList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            //grd_FIFOList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grd_FIFOList.Columns[i].Width = 40;
            grd_FIFOList.Columns[i].ReadOnly = true;
            grd_FIFOList.Columns[i].Visible = true;

            grd_FIFOList.Columns[++i].Name = "UnitClss";
            grd_FIFOList.Columns[i].HeaderText = "UnitClss";
            grd_FIFOList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grd_FIFOList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd_FIFOList.Columns[i].ReadOnly = true;
            grd_FIFOList.Columns[i].Visible = false;

            grd_FIFOList.Columns[++i].Name = "FIFO_RANK";
            grd_FIFOList.Columns[i].HeaderText = "FIFO_RANK";
            grd_FIFOList.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grd_FIFOList.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd_FIFOList.Columns[i].ReadOnly = true;
            grd_FIFOList.Columns[i].Visible = false;

            DataGridViewButtonColumn btnCol0 = new DataGridViewButtonColumn();
            {
                btnCol0.HeaderText = "삭제";
                btnCol0.Name = "btnDelete";
                btnCol0.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                btnCol0.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                btnCol0.Visible = false;
            }
            grd_FIFOList.Columns.Insert(++i, btnCol0);

            grd_FIFOList.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grd_FIFOList.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 11, FontStyle.Bold);
            grd_FIFOList.RowTemplate.Height = 35;
            grd_FIFOList.ColumnHeadersHeight = 30;
            grd_FIFOList.ScrollBars = ScrollBars.Both;
            grd_FIFOList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grd_FIFOList.MultiSelect = false;
            grd_FIFOList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grd_FIFOList.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            //grd_FIFOList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            grd_FIFOList.ReadOnly = true;

            foreach (DataGridViewColumn col in grd_FIFOList.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            grd_ALot.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grd_ALot.ColumnCount = 10;

            int j = 0;

            grd_ALot.Columns[j].Name = "No";
            grd_ALot.Columns[j].HeaderText = "";
            grd_ALot.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_ALot.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd_ALot.Columns[j].ReadOnly = true;
            grd_ALot.Columns[j].Visible = false;

            grd_ALot.Columns[++j].Name = "LotID";
            grd_ALot.Columns[j].HeaderText = "LotID";
            grd_ALot.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grd_ALot.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grd_ALot.Columns[j].ReadOnly = true;
            grd_ALot.Columns[j].Visible = true;

            grd_ALot.Columns[++j].Name = "LabelGubun";
            grd_ALot.Columns[j].HeaderText = "LabelGubun";
            grd_ALot.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_ALot.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grd_ALot.Columns[j].ReadOnly = true;
            grd_ALot.Columns[j].Visible = false;

            grd_ALot.Columns[++j].Name = "OrderID";
            grd_ALot.Columns[j].HeaderText = "OrderID";
            grd_ALot.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_ALot.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grd_ALot.Columns[j].ReadOnly = true;
            grd_ALot.Columns[j].Visible = false;

            grd_ALot.Columns[++j].Name = "RemainQty";
            grd_ALot.Columns[j].HeaderText = "잔량";
            grd_ALot.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grd_ALot.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grd_ALot.Columns[j].ReadOnly = true;
            grd_ALot.Columns[j].Visible = true;

            grd_ALot.Columns[++j].Name = "StuffDate";
            grd_ALot.Columns[j].HeaderText = "입고일";
            grd_ALot.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_ALot.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grd_ALot.Columns[j].ReadOnly = true;
            grd_ALot.Columns[j].Visible = true;

            grd_ALot.Columns[++j].Name = "LocID";
            grd_ALot.Columns[j].HeaderText = "마지막창고ID";
            grd_ALot.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_ALot.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grd_ALot.Columns[j].ReadOnly = true;
            grd_ALot.Columns[j].Visible = false;

            grd_ALot.Columns[++j].Name = "UnitClssName";
            grd_ALot.Columns[j].HeaderText = "단위";
            grd_ALot.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grd_ALot.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd_ALot.Columns[j].ReadOnly = true;
            grd_ALot.Columns[j].Visible = true;

            grd_ALot.Columns[++j].Name = "UnitClss";
            grd_ALot.Columns[j].HeaderText = "UnitClss";
            grd_ALot.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grd_ALot.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd_ALot.Columns[j].ReadOnly = true;
            grd_ALot.Columns[j].Visible = false;
            
            grd_ALot.Columns[++j].Name = "FIFO_RANK";
            grd_ALot.Columns[j].HeaderText = "FIFO_RANK";
            grd_ALot.Columns[j].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grd_ALot.Columns[j].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd_ALot.Columns[j].ReadOnly = true;
            grd_ALot.Columns[j].Visible = false;

            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            {
                btnCol.HeaderText = "삭제";
                btnCol.Name = "btnDelete";
                btnCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                btnCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                btnCol.Visible = true;
            }
            grd_ALot.Columns.Insert(++j, btnCol);

            grd_ALot.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grd_ALot.RowTemplate.Height = 35;
            grd_ALot.ColumnHeadersHeight = 30;
            grd_ALot.ScrollBars = ScrollBars.Both;
            grd_ALot.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grd_ALot.MultiSelect = false;
            grd_ALot.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grd_ALot.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grd_ALot.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grd_ALot.ReadOnly = true;

            foreach (DataGridViewColumn col in grd_ALot.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            grd_BLot.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grd_BLot.ColumnCount = 10;

            int k = 0;

            grd_BLot.Columns[k].Name = "No";
            grd_BLot.Columns[k].HeaderText = "";
            grd_BLot.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_BLot.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd_BLot.Columns[k].ReadOnly = true;
            grd_BLot.Columns[k].Visible = true;

            grd_BLot.Columns[++k].Name = "LotID";
            grd_BLot.Columns[k].HeaderText = "LotID";
            grd_BLot.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grd_BLot.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grd_BLot.Columns[k].ReadOnly = true;
            grd_BLot.Columns[k].Visible = true; 

            grd_BLot.Columns[++k].Name = "LabelGubun";
            grd_BLot.Columns[k].HeaderText = "LabelGubun";
            grd_BLot.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_BLot.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grd_BLot.Columns[k].ReadOnly = true;
            grd_BLot.Columns[k].Visible = false;

            grd_BLot.Columns[++k].Name = "OrderID";
            grd_BLot.Columns[k].HeaderText = "OrderID";
            grd_BLot.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_BLot.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grd_BLot.Columns[k].ReadOnly = true;
            grd_BLot.Columns[k].Visible = false;

            grd_BLot.Columns[++k].Name = "RemainQty";
            grd_BLot.Columns[k].HeaderText = "잔량";
            grd_BLot.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grd_BLot.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grd_BLot.Columns[k].ReadOnly = true;
            grd_BLot.Columns[k].Visible = true;

            grd_BLot.Columns[++k].Name = "StuffDate";
            grd_BLot.Columns[k].HeaderText = "입고일";
            grd_BLot.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_BLot.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grd_BLot.Columns[k].ReadOnly = true;
            grd_BLot.Columns[k].Visible = true;

            grd_BLot.Columns[++k].Name = "LocID";
            grd_BLot.Columns[k].HeaderText = "마지막창고ID";
            grd_BLot.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_BLot.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grd_BLot.Columns[k].ReadOnly = true;
            grd_BLot.Columns[k].Visible = false;

            grd_BLot.Columns[++k].Name = "UnitClssName";
            grd_BLot.Columns[k].HeaderText = "단위";
            grd_BLot.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grd_BLot.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd_BLot.Columns[k].ReadOnly = true;
            grd_BLot.Columns[k].Visible = true;

            grd_BLot.Columns[++k].Name = "UnitClss";
            grd_BLot.Columns[k].HeaderText = "UnitClss";
            grd_BLot.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grd_BLot.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd_BLot.Columns[k].ReadOnly = true;
            grd_BLot.Columns[k].Visible = false;
            
            grd_BLot.Columns[++k].Name = "FIFO_RANK";
            grd_BLot.Columns[k].HeaderText = "FIFO_RANK";
            grd_BLot.Columns[k].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grd_BLot.Columns[k].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd_BLot.Columns[k].ReadOnly = true;
            grd_BLot.Columns[k].Visible = false;

            DataGridViewButtonColumn btnCol2 = new DataGridViewButtonColumn();
            {
                btnCol2.HeaderText = "삭제";
                btnCol2.Name = "btnDelete";
                btnCol2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                btnCol2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                btnCol2.Visible = true;
            }
            grd_BLot.Columns.Insert(++k, btnCol2);

            //grd_BLot.Columns.Add(new DataGridViewImageColumn()
            //{
            //    Image = Properties.Resources.delete_button,
            //    Name = "imgbtnDelete",
            //    HeaderText = "삭제"
            //});

            grd_BLot.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grd_BLot.RowTemplate.Height = 35;
            grd_BLot.ColumnHeadersHeight = 30;
            grd_BLot.ScrollBars = ScrollBars.Both;
            grd_BLot.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grd_BLot.MultiSelect = false;
            grd_BLot.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grd_BLot.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grd_BLot.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grd_BLot.ReadOnly = true;

            foreach (DataGridViewColumn col in grd_BLot.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            grd_BLotSum.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grd_BLotSum.ColumnCount = 5;

            int z = 0;

            grd_BLotSum.Columns[z].Name = "WorkCountText";
            grd_BLotSum.Columns[z].HeaderText = "WorkCountText";
            grd_BLotSum.Columns[z].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //grd_BLotSum.Columns[z].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd_BLotSum.Columns[z].Width = 120;
            grd_BLotSum.Columns[z].ReadOnly = true;
            grd_BLotSum.Columns[z].Visible = true;
            
            grd_BLotSum.Columns[++z].Name = "WorkCount";
            grd_BLotSum.Columns[z].HeaderText = "WorkCount";
            grd_BLotSum.Columns[z].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            //grd_BLotSum.Columns[z].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grd_BLotSum.Columns[z].Width = 80;
            grd_BLotSum.Columns[z].ReadOnly = true;
            grd_BLotSum.Columns[z].Visible = true;

            grd_BLotSum.Columns[++z].Name = "WorkSumQtyText";
            grd_BLotSum.Columns[z].HeaderText = "WorkSumQtyText";
            grd_BLotSum.Columns[z].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_BLotSum.Columns[z].Width = 120;
            //grd_BLotSum.Columns[z].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd_BLotSum.Columns[z].ReadOnly = true;
            grd_BLotSum.Columns[z].Visible = true;

            grd_BLotSum.Columns[++z].Name = "WorkSumQty";
            grd_BLotSum.Columns[z].HeaderText = "WorkSumQty";
            grd_BLotSum.Columns[z].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grd_BLotSum.Columns[z].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grd_BLotSum.Columns[z].ReadOnly = true;
            grd_BLotSum.Columns[z].Visible = true;

            grd_BLotSum.Columns[++z].Name = "UnitClssName";
            grd_BLotSum.Columns[z].HeaderText = "UnitClssName";
            grd_BLotSum.Columns[z].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_BLotSum.Columns[z].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            grd_BLotSum.Columns[z].ReadOnly = true;
            grd_BLotSum.Columns[z].Visible = true;

            grd_BLotSum.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grd_BLotSum.RowTemplate.Height = 35;
            grd_BLotSum.ColumnHeadersHeight = 50;
            grd_BLotSum.ScrollBars = ScrollBars.Both;
            grd_BLotSum.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grd_BLotSum.MultiSelect = false;
            grd_BLotSum.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grd_BLotSum.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            //grd_BLotSum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grd_BLotSum.ReadOnly = true;
            grd_BLotSum.ColumnHeadersVisible = false;
            foreach (DataGridViewColumn col in grd_BLotSum.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            grd_BLotSum.Rows.Add("B Lot 갯수",
                                    "0 건",
                                    "잔량 합계",
                                    "0",
                                    ""
                                    );
            foreach (DataGridViewCell dgvc in grd_BLotSum.SelectedCells)
            {
                dgvc.Selected = false;
            }

            grd_AllLotSum.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grd_AllLotSum.ColumnCount = 3;

            int y = 0;

            grd_AllLotSum.Columns[y].Name = "WorkCountText";
            grd_AllLotSum.Columns[y].HeaderText = "WorkCountText";
            grd_AllLotSum.Columns[y].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            //grd_AllLotSum.Columns[y].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd_AllLotSum.Columns[y].Width = 320;
            grd_AllLotSum.Columns[y].ReadOnly = true;
            grd_AllLotSum.Columns[y].Visible = true;

            grd_AllLotSum.Columns[++y].Name = "WorkSumQty";
            grd_AllLotSum.Columns[y].HeaderText = "WorkSumQty";
            grd_AllLotSum.Columns[y].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grd_AllLotSum.Columns[y].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grd_AllLotSum.Columns[y].ReadOnly = true;
            grd_AllLotSum.Columns[y].Visible = true;

            grd_AllLotSum.Columns[++y].Name = "UnitClssName";
            grd_AllLotSum.Columns[y].HeaderText = "UnitClssName";
            grd_AllLotSum.Columns[y].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grd_AllLotSum.Columns[y].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grd_AllLotSum.Columns[y].ReadOnly = true;
            grd_AllLotSum.Columns[y].Visible = true;

            grd_AllLotSum.Font = new Font("맑은 고딕", 15, FontStyle.Bold);
            grd_AllLotSum.RowTemplate.Height = 35;
            grd_AllLotSum.ColumnHeadersHeight = 50;
            grd_AllLotSum.ScrollBars = ScrollBars.Both;
            grd_AllLotSum.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grd_AllLotSum.MultiSelect = false;
            grd_AllLotSum.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grd_AllLotSum.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            //grd_AllLotSum.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grd_AllLotSum.ReadOnly = true;
            grd_AllLotSum.ColumnHeadersVisible = false;
            foreach (DataGridViewColumn col in grd_AllLotSum.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            grd_AllLotSum.Rows.Add("전체 합계 ( A + B )",
                                    "0",
                                    ""
                                    );
            foreach (DataGridViewCell dgvc in grd_AllLotSum.SelectedCells)
            {
                dgvc.Selected = false;
            }
        }
        #endregion

        private void CalcRollSum()
        {
            int nTRoll = 0;
            double douTQty = 0;
            double douTRealQty = 0;
            int nNo = 0;

            foreach (DataGridViewRow dgvr in grd_FIFOList.Rows)
            {
                nTRoll = nTRoll + 1;
                douTQty = douTQty + Frm_tprc_Main.Lib.GetDouble(dgvr.Cells["MoveableQty"].Value.ToString());
                douTRealQty = douTRealQty + Frm_tprc_Main.Lib.GetDouble(dgvr.Cells["MoveableQty"].Value.ToString());
                dgvr.Cells["Seq"].Value = (++nNo).ToString();
            }
        }
        private void FillGrdBLotSum()
        {
            try
            {
                double QtySum = 0;
                double dQty = 0;
                foreach (DataGridViewRow dgvr in grd_BLot.Rows)
                {
                    double.TryParse(dgvr.Cells["RemainQty"].Value.ToString().Replace(",",""), out dQty);
                    QtySum = QtySum + dQty;
                }
                if (grd_BLotSum.Rows.Count == 1)
                {
                    grd_BLotSum.Rows[0].Cells["WorkCount"].Value = grd_BLot.Rows.Count + " 건";
                    grd_BLotSum.Rows[0].Cells["WorkSumQty"].Value = string.Format("{0:n3}", QtySum);
                }
                dQty = 0;
                if (grd_ALot.RowCount == 1)
                {
                    double.TryParse(grd_ALot.Rows[0].Cells["RemainQty"].Value.ToString().Replace(",", ""), out dQty);
                }
                QtySum = QtySum + dQty;
                grd_AllLotSum.Rows[0].Cells["WorkSumQty"].Value = string.Format("{0:n3}", QtySum);
                if (grd_ALot.RowCount == 0 && grd_BLot.RowCount == 0)
                {
                    grd_BLotSum.Rows[0].Cells["UnitClssName"].Value = "";
                    grd_AllLotSum.Rows[0].Cells["UnitClssName"].Value = "";
                }
            }
            catch (Exception e)
            {
                Message[0] = "[오류]";
                Message[1] = e.Message;
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return;
            }
            
        }
    

        private bool CheckData()
        {
            try
            {
                //'품명이 없을 경우
                if (lblArticle.Text == "")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("품명을 입력해야 합니다.", "[오류]", 0, 1);
                    return false;
                }
                //서로 다른 단위의 Lot끼리는 합칠 수 없다. Lot의 단위가 KG, G이어도 합칠 수 없다.
                if (grd_ALot.RowCount == 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("A Lot가 입력되지 않았습니다.", "[오류-A Lot 미입력]", 0, 1);
                    return false;
                }
                else if (grd_ALot.RowCount > 1)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("A Lot가 여러개의 Lot가 입력되었습니다. 하나만 남겨주세요.", "[오류-A Lot 다수입력]", 0, 1);
                    return false;
                }
                else if (grd_BLot.RowCount == 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("B Lot가 입력되지 않았습니다.", "[오류-B Lot 미입력]", 0, 1);
                    return false;
                }
                if (grd_ALot.RowCount == 1)
                {
                    foreach (DataGridViewRow dgvr in grd_BLot.Rows)
                    {
                        if (grd_ALot.Rows[0].Cells["UnitClss"].Value.ToString() != dgvr.Cells["UnitClss"].Value.ToString())
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox("서로 다른 단위의 Lot를 합칠 수 없습니다.", "[오류 - 단위 다름]", 0, 1);
                            dgvr.Selected = true;
                            return false;
                        }
                    }
                }
                txtBarCode.Select();
                txtBarCode.Focus();
                return true;
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
                return false;
            }
        }

        private void FillGridFifoList()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@ArticleID", lblArticle.Tag.ToString());
                sqlParameter.Add("@StuffDate", StuffDate);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sLotFIFOByArticleID", sqlParameter, false);

                double doutqty = 0;
                //No(Rnk),LotID,RemainQty,StuffDate,UnitClssName,UnitClss,OrderID,LocID,CHKEFFECT, EFFECTYN, DEFECTYN, AGINGYN
                grd_FIFOList.Rows.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    doutqty = 0;
                    double.TryParse(dr["RemainQty"].ToString(), out doutqty);
                    grd_FIFOList.Rows.Add(dr["FIFO_Rank"].ToString(),       //선입선출 순번
                                    dr["LotID"].ToString().Trim(),                 //'LotID
                                    dr["LabelGubun"].ToString(),            //'LabelGubun
                                    dr["OrderID"].ToString(),               //'OrderID
                                    string.Format("{0:n2}", doutqty),       //잔량
                                    Frm_tprc_Main.Lib.MakeDateTime("yyyymmdd", dr["StuffDate"].ToString()), //입고일자
                                    dr["LocID"].ToString(),                 //현재Lot의 창고위치ID
                                    dr["UnitClssName"].ToString(),          //단위 한글명
                                    dr["UnitClss"].ToString(),              //단위코드
                                    dr["FIFO_RANK"].ToString(),             //선입선출 순번
                                    "삭제"                                  //삭제버튼
                                    );
                }
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
        }

        private bool SaveData()
        {
            try
            {
                if (!(grd_ALot.RowCount == 1 && grd_BLot.RowCount > 0))
                {
                    return false;
                }

                else
                {
                    list_TStuffin = new List<TSTUFFIN>();
                    list_TStuffinSub = new List<TSTUFFINSUB>();
                    list_TOutware = new List<TOUTWARE>();
                    list_TOutwareSub = new List<TOUTWARESUB>();
                    Prolist = new List<WizCommon.Procedure>();
                    ListParameter = new List<Dictionary<string, object>>();
                    Prolist2 = new List<WizCommon.Procedure>();
                    ListParameter2 = new List<Dictionary<string, object>>();

                    // foreach count 
                    int foreachcount = 0;

                    //DB에 넣을 데이터 셋팅
                    foreach (DataGridViewRow dgvr in grd_BLot.Rows)
                    {
                        TOUTWARE tOUTWARE = new TOUTWARE();
                        tOUTWARE.OrderID = dgvr.Cells["OrderID"].Value.ToString();
                        tOUTWARE.CompanyID = "0001";

                        tOUTWARE.OutClss = "06";                                    //'이동구분 06 : 잔량이동처리
                        tOUTWARE.CustomID = "0001";                                 //'이동의 경우에는 거래처가 없으므로 해당 시스템이 설치된 업체의 코드를 가져옴(해당시스템 업체의 거래처명, 매출)
                        tOUTWARE.BuyerDirectYN = "N";
                        tOUTWARE.WorkID = "0001";                                   //'가공구분
                        tOUTWARE.ExchRate = "0";

                        tOUTWARE.InsStuffINYN = "Y";                               //'동시입고 Y, 이동이므로 출고와 동시입고 처리함

                        tOUTWARE.OutCustomID = "0001";                            //'이동의 경우에는 거래처가 없으므로 해당 시스템이 설치된 업체의 코드를 가져옴(해당시스템 업체의 거래처명, 매출)
                        tOUTWARE.OutCustom = "AFT 잔량이동";
                        tOUTWARE.LossRate = "0";
                        tOUTWARE.LossQty = "0";
                        tOUTWARE.OutRoll = "1";// Lib.GetDouble(lblOutRoll.Text).ToString();                   //'건수

                        tOUTWARE.OutQty = Frm_tprc_Main.Lib.GetDouble(dgvr.Cells["RemainQty"].Value.ToString()).ToString();//Lib.GetDouble(lblOutQty.Text).ToString();                     //'출고량-정산에서 사용
                        tOUTWARE.OutRealQty = Frm_tprc_Main.Lib.GetDouble(dgvr.Cells["RemainQty"].Value.ToString()).ToString();//Lib.GetDouble(lblOutQty.Text).ToString();                 //'소요량-수불에서 사용

                        tOUTWARE.OutDate = DateTime.Now.ToString("yyyyMMdd");   // '출고일자
                        tOUTWARE.ResultDate = tOUTWARE.OutDate;

                        tOUTWARE.BoOutClss = "";
                        tOUTWARE.OutTime = DateTime.Now.ToString("HHmm");
                        tOUTWARE.LoadTime = DateTime.Now.ToString("HHmm");

                        decimal totalremain = list_B_Lot_TotalRemainQty[foreachcount];
                        double TotalRemainQty = Convert.ToDouble(totalremain);
                        if (tOUTWARE.OutQty != TotalRemainQty.ToString())
                        {
                            if (tOUTWARE.OutQty == "0")
                            {
                                tOUTWARE.Remark = "설정수량 초과에 따른 출고량 0 잔량이동, 남은잔량 : " + TotalRemainQty.ToString();
                            }
                            else
                            {
                                // 변수에 담긴 잔량과 총잔량이 일치하지 않는다 >> 전량이동처리가 아니다.
                                tOUTWARE.Remark = "잔량이 남아있는 잔량이동처리, 남은잔량 : " + (TotalRemainQty - Frm_tprc_Main.Lib.GetDouble(dgvr.Cells["RemainQty"].Value.ToString())).ToString();
                            }                            
                        }
                        else
                        {
                            tOUTWARE.Remark = "정상 _ 전량 잔량이동";
                        }
                        
                        tOUTWARE.OutType = "3";                                    //'스켄출고
                        tOUTWARE.snVatAmount = "0";                                   //'이동의 경우 금액 0 -- lblVatINDYN.Caption
                        tOUTWARE.VatINDYN = "0";                                   //'이동의 경우 금액 0 -- lblVatINDYN.Caption
                        tOUTWARE.sUnitClss = dgvr.Cells["UnitClss"].Value.ToString();

                        tOUTWARE.sFromLocID = dgvr.Cells["LocID"].Value.ToString(); //'From 창고
                        tOUTWARE.sToLocID = dgvr.Cells["LocID"].Value.ToString();   //'TO 창고
                        tOUTWARE.sArticleID = lblArticle.Tag.ToString();            //          '품명코드

                        TOUTWARESUB tOUTWARESUB = new TOUTWARESUB();
                        tOUTWARESUB.sOutwareID = "";
                        tOUTWARESUB.OrderID = dgvr.Cells["OrderID"].Value.ToString();
                        tOUTWARESUB.OutSubSeq = "1";
                        tOUTWARESUB.OrderSeq = "0";
                        tOUTWARESUB.ArticleID = lblArticle.Tag.ToString();
                        tOUTWARESUB.RollSeq = "0";
                        tOUTWARESUB.LotNo = "0";
                        tOUTWARESUB.OutQty = Frm_tprc_Main.Lib.GetDouble(dgvr.Cells["RemainQty"].Value.ToString()).ToString();
                        tOUTWARESUB.OutRoll = "1";
                        tOUTWARESUB.LabelID = dgvr.Cells["LotID"].Value.ToString();
                        tOUTWARESUB.LabelGubun = dgvr.Cells["LabelGubun"].Value.ToString();
                        tOUTWARESUB.Unitprice = "0";

                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("@OrderID", tOUTWARE.OrderID);
                        sqlParameter.Add("@CompanyID", tOUTWARE.CompanyID);
                        sqlParameter.Add("@OutSeq", tOUTWARE.OutSeq);
                        sqlParameter.Add("@OutwareID", tOUTWARE.sOutwareID);         //'출고번호		
                        sqlParameter.Add("@OutClss", tOUTWARE.OutClss);
                        sqlParameter.Add("@CustomID", tOUTWARE.CustomID);
                        sqlParameter.Add("@BuyerDirectYN", tOUTWARE.BuyerDirectYN);
                        sqlParameter.Add("@WorkID", tOUTWARE.WorkID);
                        sqlParameter.Add("@ExchRate", tOUTWARE.ExchRate);
                        sqlParameter.Add("@UnitPriceclss", tOUTWARE.Unitprice);
                        sqlParameter.Add("@InsStuffInYN", tOUTWARE.InsStuffINYN);
                        sqlParameter.Add("@OutcustomID", tOUTWARE.OutCustomID);
                        sqlParameter.Add("@Outcustom", tOUTWARE.OutCustom);
                        sqlParameter.Add("@LossRate", tOUTWARE.LossRate);
                        sqlParameter.Add("@LossQty", tOUTWARE.LossQty);
                        sqlParameter.Add("@OutRoll", tOUTWARE.OutRoll);
                        sqlParameter.Add("@OutQty", tOUTWARE.OutQty);
                        sqlParameter.Add("@OutRealQty", tOUTWARE.OutRealQty);
                        sqlParameter.Add("@OutDate", tOUTWARE.OutDate);
                        sqlParameter.Add("@ResultDate", tOUTWARE.ResultDate);
                        sqlParameter.Add("@BoOutClss", tOUTWARE.BoOutClss);
                        sqlParameter.Add("@Remark", tOUTWARE.Remark);
                        sqlParameter.Add("@OutType", tOUTWARE.OutType);
                        sqlParameter.Add("@Amount", "0");                                    // '금액   		
                        sqlParameter.Add("@VatAmount", tOUTWARE.snVatAmount);            // '부가세 	
                        sqlParameter.Add("@VatINDYN", tOUTWARE.VatINDYN);         // '부가세별도여부 		
                        sqlParameter.Add("@FromLocID", tOUTWARE.sFromLocID);       // '이전창고		
                        sqlParameter.Add("@ToLocID", tOUTWARE.sToLocID);            // '이후창고			
                        sqlParameter.Add("@UnitClss", tOUTWARE.sUnitClss);               //  '단위	
                        sqlParameter.Add("@ArticleID", tOUTWARE.sArticleID);
                        sqlParameter.Add("@UserID", Frm_tprc_Main.g_tBase.PersonID);
                        //output 2개 ow.OutSeq  //ow.sOutwareID
                        WizCommon.Procedure pro1 = new WizCommon.Procedure();
                        pro1.list_OutputName = new List<string>();
                        pro1.list_OutputLength = new List<string>();

                        //pro1.Name = "xp_Outware_iOutware";
                        pro1.Name = "xp_WizWork_iOutware";
                        pro1.OutputUseYN = "Y";
                        pro1.list_OutputName.Add("@OutSeq");
                        pro1.list_OutputName.Add("@OutwareID");
                        pro1.list_OutputLength.Add("10");
                        pro1.list_OutputLength.Add("12");

                        Prolist.Add(pro1);
                        ListParameter.Add(sqlParameter);

                        Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                        sqlParameter2.Add("@OutwareID", tOUTWARESUB.sOutwareID);
                        sqlParameter2.Add("@OrderID", tOUTWARESUB.OrderID);
                        sqlParameter2.Add("@OutSeq", tOUTWARE.OutSeq);
                        sqlParameter2.Add("@OutSubSeq", tOUTWARESUB.OutSubSeq);
                        sqlParameter2.Add("@OrderSeq", tOUTWARESUB.OrderSeq);
                        sqlParameter2.Add("@LineSeq", "0");
                        sqlParameter2.Add("@LineSubSeq", "0");
                        sqlParameter2.Add("@RollSeq", tOUTWARESUB.RollSeq);
                        sqlParameter2.Add("@LabelID", tOUTWARESUB.LabelID);
                        sqlParameter2.Add("@LabelGubun", tOUTWARESUB.LabelGubun);
                        sqlParameter2.Add("@LotNo", tOUTWARESUB.LotNo);
                        sqlParameter2.Add("@Gubun", "2");
                        sqlParameter2.Add("@StuffQty", "0");
                        sqlParameter2.Add("@OutQty", tOUTWARESUB.OutQty);
                        sqlParameter2.Add("@OutRoll", tOUTWARESUB.OutRoll);
                        sqlParameter2.Add("@UnitPrice", tOUTWARESUB.Unitprice);  //'단가
                        sqlParameter2.Add("@UserID", Frm_tprc_Main.g_tBase.PersonID);
                        sqlParameter2.Add("@CustomBoxID", tOUTWARESUB.CustomBoxID); //'고객 BoxID
                        sqlParameter2.Add("@ArticleID", tOUTWARESUB.ArticleID); //'고객 BoxID

                        WizCommon.Procedure pro2 = new WizCommon.Procedure();
                        pro2.list_OutputName = new List<string>();
                        pro2.list_OutputLength = new List<string>();
                        
                        pro2.Name = "xp_WizWork_iOutwareSub";
                        //pro2.Name = "xp_Outware_iOutwareSub";
                        pro2.OutputUseYN = "N";
                        pro2.list_OutputName.Add("sRtnMsg");
                        pro2.list_OutputLength.Add("30");

                        Prolist.Add(pro2);
                        ListParameter.Add(sqlParameter2);

                        foreachcount++;
                        //list_TOutware.Add(tOUTWARE);
                        //list_TOutwareSub.Add(tOUTWARESUB);
                        //tOUTWARE = null;
                        //tOUTWARESUB = null;
                    }
                    if (grd_ALot.RowCount == 1)
                    {
                        double douAllQty = 0;
                        douAllQty = Frm_tprc_Main.Lib.GetDouble(grd_BLotSum.Rows[0].Cells["WorkSumQty"].Value.ToString()) +
                            Frm_tprc_Main.Lib.GetDouble(grd_ALot.Rows[0].Cells["RemainQty"].Value.ToString());
                        TSTUFFIN tSTUFFIN = new TSTUFFIN();
                        tSTUFFIN.StuffinID = "";
                        tSTUFFIN.StuffDate = DateTime.Now.ToString("yyyyMMdd");     // '입고일자
                        tSTUFFIN.StuffClss = "06";                                  //'이동구분 06 : 잔량이동처리
                        tSTUFFIN.CustomID = "0001";                            //'이동의 경우에는 거래처가 없으므로 해당 시스템이 설치된 업체의 코드를 가져옴(해당시스템 업체의 거래처명, 매출)
                        //tSTUFFIN.Custom = mc.KCustom
                        tSTUFFIN.UnitClss = grd_ALot.Rows[0].Cells["UnitClss"].Value.ToString();
                        tSTUFFIN.TotRoll = "1";
                        tSTUFFIN.TotQty = Frm_tprc_Main.Lib.GetDouble(grd_BLotSum.Rows[0].Cells["WorkSumQty"].Value.ToString()).ToString();
                        tSTUFFIN.TotQtyY = Frm_tprc_Main.Lib.GetDouble(grd_BLotSum.Rows[0].Cells["WorkSumQty"].Value.ToString()).ToString();
                        tSTUFFIN.UnitPrice = "0";
                        tSTUFFIN.PriceClss = "";
                        tSTUFFIN.ExchRate = "0";
                        tSTUFFIN.VatIndYN = "0";
                        tSTUFFIN.Remark = "잔량이동처리시점 LOTID " + grd_ALot.Rows[0].Cells["LotID"].Value.ToString() + "의 현재 총 잔량은 |" + douAllQty.ToString() + "| 이다.";
                        tSTUFFIN.OrderId = "";
                        tSTUFFIN.ArticleID = lblArticle.Tag.ToString();             //ArticleID
                        tSTUFFIN.WorkID = "0001";                                   //'가공구분
                        tSTUFFIN.OrderNO = "";
                        tSTUFFIN.InsStuffINYN = "Y";
                        tSTUFFIN.OutwareID = "";
                        tSTUFFIN.CompanyID = "0001";
                        tSTUFFIN.FromLocID = grd_ALot.Rows[0].Cells["LocID"].Value.ToString();
                        tSTUFFIN.TOLocID = grd_ALot.Rows[0].Cells["LocID"].Value.ToString();
                        tSTUFFIN.CreateUserID = Frm_tprc_Main.g_tBase.PersonID;

                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                        sqlParameter.Add("@StuffinID", "");
                        sqlParameter.Add("@StuffDate", tSTUFFIN.StuffDate);
                        sqlParameter.Add("@StuffClss", tSTUFFIN.StuffClss);
                        sqlParameter.Add("@CustomID", tSTUFFIN.CustomID);
                        sqlParameter.Add("@UnitClss", tSTUFFIN.UnitClss);
                        sqlParameter.Add("@TotRoll", tSTUFFIN.TotRoll);
                        sqlParameter.Add("@TotQty", tSTUFFIN.TotQty);
                        sqlParameter.Add("@TotQtyY", tSTUFFIN.TotQtyY);
                        sqlParameter.Add("@UnitPrice", tSTUFFIN.UnitPrice);
                        sqlParameter.Add("@PriceClss", tSTUFFIN.PriceClss);
                        sqlParameter.Add("@ExchRate", tSTUFFIN.ExchRate);
                        sqlParameter.Add("@VatIndYN", tSTUFFIN.VatIndYN);
                        sqlParameter.Add("@Remark", tSTUFFIN.Remark);
                        sqlParameter.Add("@OrderId", tSTUFFIN.OrderId);
                        sqlParameter.Add("@ArticleID", tSTUFFIN.ArticleID);
                        sqlParameter.Add("@WorkID", tSTUFFIN.WorkID);
                        sqlParameter.Add("@OrderNO", tSTUFFIN.OrderNO);
                        sqlParameter.Add("@InsStuffINYN", tSTUFFIN.InsStuffINYN);
                        sqlParameter.Add("@OutwareID", tSTUFFIN.OutwareID);
                        sqlParameter.Add("@CompanyID", tSTUFFIN.CompanyID);
                        sqlParameter.Add("@BrandClss", tSTUFFIN.BrandClss);
                        sqlParameter.Add("@OrderForm", tSTUFFIN.OrderForm);
                        sqlParameter.Add("@FromLocID", tSTUFFIN.FromLocID);
                        sqlParameter.Add("@TOLocID", tSTUFFIN.TOLocID);
                        sqlParameter.Add("@CreateUserID", tSTUFFIN.CreateUserID);

                        WizCommon.Procedure pro1 = new WizCommon.Procedure();
                        pro1.list_OutputName = new List<string>();
                        pro1.list_OutputLength = new List<string>();

                        pro1.Name = "xp_WizWork_iStuffIN";
                        pro1.OutputUseYN = "Y";
                        pro1.list_OutputName.Add("@StuffinID");
                        pro1.list_OutputLength.Add("20");

                        Prolist.Add(pro1);
                        ListParameter.Add(sqlParameter);

                        TSTUFFINSUB tSTUFFINSUB = new TSTUFFINSUB();
                        tSTUFFINSUB.StuffInID = "";
                        tSTUFFINSUB.StuffInSubSeq = "1";
                        tSTUFFINSUB.RollNo = "1";
                        tSTUFFINSUB.StuffClss = "06";                                  //'이동구분 06 : 잔량이동처리
                        tSTUFFINSUB.Qty = Frm_tprc_Main.Lib.GetDouble(grd_BLotSum.Rows[0].Cells["WorkSumQty"].Value.ToString()).ToString();
                        tSTUFFINSUB.LotID = grd_ALot.Rows[0].Cells["LotID"].Value.ToString();
                        tSTUFFINSUB.SetDate = "";
                        tSTUFFINSUB.InspectApprovalYN = "Y";
                        tSTUFFINSUB.CreateUserID = Frm_tprc_Main.g_tBase.PersonID;

                        Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                        sqlParameter2.Add("@StuffInID", tSTUFFINSUB.StuffInID);
                        sqlParameter2.Add("@StuffInSubSeq", tSTUFFINSUB.StuffInSubSeq);
                        sqlParameter2.Add("@RollNo", tSTUFFINSUB.RollNo);
                        sqlParameter2.Add("@StuffClss", tSTUFFINSUB.StuffClss);
                        sqlParameter2.Add("@Qty", tSTUFFINSUB.Qty);
                        sqlParameter2.Add("@LotID", tSTUFFINSUB.LotID);
                        sqlParameter2.Add("@SetDate", tSTUFFINSUB.SetDate);
                        sqlParameter2.Add("@InspectApprovalYN", tSTUFFINSUB.InspectApprovalYN);
                        sqlParameter2.Add("@CreateUserID", tSTUFFINSUB.CreateUserID);

                        WizCommon.Procedure pro2 = new WizCommon.Procedure();
                        pro2.list_OutputName = new List<string>();
                        pro2.list_OutputLength = new List<string>();

                        pro2.Name = "xp_WizWork_iStuffINSub";
                        pro2.OutputUseYN = "N";

                        Prolist.Add(pro2);
                        ListParameter.Add(sqlParameter2);

                        list_TStuffin.Add(tSTUFFIN);
                        list_TStuffinSub.Add(tSTUFFINSUB);
                        tSTUFFIN = null;
                        tSTUFFINSUB = null;
                    }

                    List<KeyValue> list_Result = new List<KeyValue>();
                    list_Result = DataStore.Instance.ExecuteProcedureNoCommitGetOutputList(Prolist, ListParameter);

                    if (list_Result[0].key.ToLower() == "success")
                    {
                        List<string> OUTWAREID = new List<string>();
                        string STUFFINID = "";
                        foreach (KeyValue kv in list_Result)
                        {
                            if (kv.key.ToUpper() == "@OUTWAREID")
                            {
                                OUTWAREID.Add(kv.value.Trim().ToUpper());
                            }
                            else if (kv.key.ToUpper() == "@STUFFINID")
                            {
                                STUFFINID = kv.value.ToUpper().Trim();
                            }
                        }
                        if (STUFFINID != "")
                        {
                            int i = 0;
                            foreach (string owid in OUTWAREID)
                            {
                                Dictionary<string, object> sqlParameter0 = new Dictionary<string, object>();
                                List<string> Pro = new List<string>();
                                i++;
                                sqlParameter0.Add("MoveID", "");
                                sqlParameter0.Add("STUFFINID", STUFFINID);
                                sqlParameter0.Add("OUTWAREID", owid);
                                sqlParameter0.Add("CreateUserID", Frm_tprc_Main.g_tBase.PersonID);
                                if (i == 1)
                                {
                                    WizCommon.Procedure pro1 = new WizCommon.Procedure();
                                    pro1.list_OutputName = new List<string>();
                                    pro1.list_OutputLength = new List<string>();

                                    pro1.Name = "xp_WizWork_iRemainMove";
                                    pro1.OutputUseYN = "Y";
                                    pro1.list_OutputName.Add("MoveID");
                                    pro1.list_OutputLength.Add("16");

                                    Prolist2.Add(pro1);
                                    ListParameter2.Add(sqlParameter0);
                                }
                                else
                                {
                                    WizCommon.Procedure pro2 = new WizCommon.Procedure();
                                    pro2.list_OutputName = new List<string>();
                                    pro2.list_OutputLength = new List<string>();

                                    pro2.Name = "xp_WizWork_iRemainMoveNoOut";
                                    pro2.OutputUseYN = "N";

                                    Prolist2.Add(pro2);
                                    ListParameter2.Add(sqlParameter0);
                                }
                            }
                            bool list_Remain = DataStore.Instance.ExecuteProcedureAllNoBeginOKCommit(Prolist2, ListParameter2);
                            if (list_Remain)
                            { WizCommon.Popup.MyMessageBox.ShowBox("잔량이동처리가 완료되었습니다.", "[잔량이동처리완료]", 3, 1); }
                            grd_ALot.Rows.Clear();
                            grd_BLot.Rows.Clear();
                            FillGrdBLotSum();
                        }
                    }
                    else
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("[잔량이동처리실패]\r\n" + list_Result[0].value.ToString(), "[오류]", 0, 1, 1);
                        return false;
                    }
                }
                txtBarCode.Focus();
                return true;
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
                return false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                //실행중인 프로세스가 없을때 
                if (!Frm_tprc_Main.Lib.ReturnProcessRunStop("osk"))
                {
                    System.Diagnostics.Process ps = new System.Diagnostics.Process();
                    ps.StartInfo.FileName = "osk.exe";
                    ps.Start();

                    //string progFiles = @"C:\Program Files\Common Files\Microsoft Shared\ink";
                    //string keyboardPath = Path.Combine(progFiles, "TabTip.exe");

                    //Process.Start(keyboardPath);
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                MessageBox.Show("실패");
                Process.Start(@"C:\Windows\winsxs\x86_microsoft-windows-osk_31bf3856ad364e35_6.1.7601.18512_none_acc225fbb832b17f\osk.exe");
            }
            txtBarCode.Select();
            txtBarCode.Focus();
        }

        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                txtBarCode_Enter();
                if (grd_FIFOList.Rows.Count == 0)
                {
                    if (lblArticle.Tag.ToString() != "")
                    {
                        FillGridFifoList();
                    }
                }
            }
        }

        private void CheckLabelID()
        {
            string NowBarCode = txtBarCode.Text.Trim().ToUpper();
            //그리드에 같은 LabelID가 있는지
            foreach (DataGridViewRow dgvr in grd_FIFOList.Rows)
            {
                if (dgvr.Cells["Barcode"].Value.ToString() == NowBarCode)
                {
                    Message[0] = "[중복 스캔]";
                    Message[1] = "이미 스캔한 롯트입니다. 중복으로 스캔할 수 없습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                }
            }
            //전창고에 현재 존재하고 있는 수량인지 Check
        }

        private bool CheckCondition(DataRow dr)
        {
            try
            {
                foreach (DataGridViewRow dgvr in grd_ALot.Rows)
                {

                    //2. 이미 스캔된 바코드를 스캔했을 때
                    if (dgvr.Cells["LotID"].Value.ToString() == dr["LotID"].ToString())
                    {
                        Message[0] = "[이미 스캔]";
                        Message[1] = "이미 스캔되있는 바코드를 스캔하셨습니다.";
                        dgvr.Selected = true;
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                        txtBarCode.Text = "";
                        txtBarCode.Select();
                        txtBarCode.Focus();
                        return false;
                    }

                    //3. 단위가 다른 바코드를 스캔하였을때 사용못하도록 막는다.
                    if (dgvr.Cells["UnitClssName"].Value.ToString().ToUpper() != dr["UnitClssName"].ToString().ToUpper())
                    {
                        Message[0] = "[단위 다름]";
                        Message[1] = "스캔한 Lot의 단위는 " + dr["UnitClssName"].ToString() + " 입니다." + dgvr.Cells["UnitClssName"].Value.ToString() + "단위를 가진 Lot를 스캔해주세요.";
                        dgvr.Selected = true;
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                        txtBarCode.Text = "";
                        txtBarCode.Select();
                        txtBarCode.Focus();
                        return false;
                    }
                }
                foreach (DataGridViewRow dgvr in grd_BLot.Rows)
                {

                    //2. 이미 스캔된 바코드를 스캔했을 때
                    if (dgvr.Cells["LotID"].Value.ToString() == dr["LotID"].ToString())
                    {
                        Message[0] = "[이미 스캔]";
                        Message[1] = "이미 스캔되있는 바코드를 스캔하셨습니다.";
                        dgvr.Selected = true;
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                        txtBarCode.Text = "";
                        txtBarCode.Select();
                        txtBarCode.Focus();
                        return false;
                    }
                    //3. 단위가 다른 바코드를 스캔하였을때 사용못하도록 막는다.
                    if (dgvr.Cells["UnitClssName"].Value.ToString().ToUpper() != dr["UnitClssName"].ToString().ToUpper())
                    {
                        Message[0] = "[단위 다름]";
                        Message[1] = "스캔한 Lot의 단위는 " + dr["UnitClssName"].ToString() + " 입니다." + dgvr.Cells["UnitClssName"].Value.ToString() + "단위를 가진 Lot를 스캔해주세요.";
                        dgvr.Selected = true;
                        WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                        txtBarCode.Text = "";
                        txtBarCode.Select();
                        txtBarCode.Focus();
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                int i = 0;
                if (Message[0].Length > 50)
                {
                    i = 1;
                }
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1, i);
                txtBarCode.Text = "";
                txtBarCode.Select();
                txtBarCode.Focus();
                return false;
            }
        }
        private bool CheckCondition(DataGridViewRow dr)
        {
            try
            {
                foreach (DataGridViewRow dgvr in grd_ALot.Rows)
                {
                    //2. 이미 스캔된 바코드를 스캔했을 때
                    if (dgvr.Cells["LotID"].Value.ToString() == dr.Cells["LotID"].Value.ToString())
                    {
                        Message[0] = "[이미 스캔]";
                        Message[1] = "이미 스캔되있는 바코드를 스캔하셨습니다.";
                        dgvr.Selected = true;
                        throw new Exception();
                    }
                    //3. 단위가 다른 바코드를 스캔하였을때 사용못하도록 막는다.
                    if (dgvr.Cells["UnitClssName"].Value.ToString().ToUpper() != dr.Cells["UnitClssName"].Value.ToString().ToUpper())
                    {
                        Message[0] = "[단위 다름]";
                        Message[1] = "스캔한 Lot의 단위는 " + dr.Cells["UnitClssName"].Value.ToString() + " 입니다.\r\n" + dgvr.Cells["UnitClssName"].Value.ToString() + "단위를 가진 Lot를 스캔해주세요.";
                        dgvr.Selected = true;
                        throw new Exception();
                    }
                }
                foreach (DataGridViewRow dgvr in grd_BLot.Rows)
                {
                    //2. 이미 스캔된 바코드를 스캔했을 때
                    if (dgvr.Cells["LotID"].Value.ToString() == dr.Cells["LotID"].Value.ToString())
                    {
                        Message[0] = "[이미 스캔]";
                        Message[1] = "이미 스캔되있는 바코드를 스캔하셨습니다.";
                        dgvr.Selected = true;
                        throw new Exception();
                    }
                    //3. 단위가 다른 바코드를 스캔하였을때 사용못하도록 막는다.
                    if (dgvr.Cells["UnitClssName"].Value.ToString().ToUpper() != dr.Cells["UnitClssName"].Value.ToString().ToUpper())
                    {
                        Message[0] = "[단위 다름]";
                        Message[1] = "스캔한 Lot의 단위는 " + dr.Cells["UnitClssName"].Value.ToString() + " 입니다.\r\n" + dgvr.Cells["UnitClssName"].Value.ToString() + "단위를 가진 Lot를 스캔해주세요.";
                        dgvr.Selected = true;
                        throw new Exception();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                int i = 0;
                if (Message[0].Length > 50)
                {
                    i = 1;
                }
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1, i);
                txtBarCode.Text = "";
                txtBarCode.Select();
                txtBarCode.Focus();
                return false;
            }
            
        }

        #region 해당 라벨의 품명이 여러개인지를 체크하는 메서드

        private bool CheckLabelArticleCount()
        {
            bool flag = false;

            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("LabelID", txtBarCode.Text.ToUpper());
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sArticleStockQty_ByLabelID", sqlParameter, false);

                if (dt != null
                    && dt.Rows.Count > 1)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                flag = false;
            }

            return flag;
        }

        #endregion

        private void txtBarCode_Enter()
        {
            try
            {
                string ArticleID = "";
                string LotID = txtBarCode.Text.ToUpper().Trim();

                // 이미 바코드가 등록되어 있다면..
                if (CheckBarcodeExists(LotID) == false)
                {
                    Message[0] = "[이미 스캔]";
                    Message[1] = "이미 스캔되있는 바코드를 스캔하셨습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                    txtBarCode.Text = "";
                    txtBarCode.Select();
                    txtBarCode.Focus();
                    return;
                }

                if (CheckLabelArticleCount())
                {
                    Frm_PopUpSel_sArticle_ByScan fps = new Frm_PopUpSel_sArticle_ByScan(LotID);
                    //fps.WriteTextEvent += new Frm_PopUpSel_sArticle_ByScan.TextEventHandler(GetData);
                    fps.Owner = this;
                    fps.BringToFront();
                    fps.ShowDialog();

                    if (fps.DialogResult == DialogResult.OK)
                    {
                        ArticleID = fps.returnArticleID;

                        FillGridBarcode(LotID, ArticleID);
                        return;
                    }
                    else
                    {
                        txtBarCode.Text = "";
                        txtBarCode.Select();
                        txtBarCode.Focus();
                        return;
                    }
                }

                FillGridBarcode(LotID, "");

            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        private bool CheckBarcodeExists(string LotID)
        {
            bool flag = true;

            for(int i = 0; i < grd_ALot.Rows.Count; i++)
            {
                if (LotID.Equals(grd_ALot.Rows[i].Cells["LotID"].Value.ToString()))
                {
                    flag = false;
                    break;
                }
            }

            for (int i = 0; i < grd_BLot.Rows.Count; i++)
            {
                if (LotID.Equals(grd_BLot.Rows[i].Cells["LotID"].Value.ToString()))
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }

        private void FillGridBarcode(string LabelID, string ArticleID)
        {
            try
            {
                string FIFO_RANK = "";
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("LotID", LabelID);
                sqlParameter.Add("sArticleID", ArticleID);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_WizWork_sLotInfoByLotID]", sqlParameter, false);

                if (dt.Rows.Count == 0)
                {
                    string str = "존재하지 않는 바코드번호 입니다. \r\n(" + txtBarCode.Text + ")";
                    throw new Exception(str);
                }

                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    //1. 다른 ArticleID 스캔 시
                    if (lblArticle.Tag.ToString() != "")//ArticleID를 가지고 있을때만 확인
                    {
                        if (dr["ArticleID"].ToString() != lblArticle.Tag.ToString())
                        {
                            Message[0] = "[다른 품명 스캔]";
                            Message[1] = "다른 품명을 스캔하셨습니다." + lblArticle.Text + "가 아닌 " +
                                dr["Article"].ToString() + "의 잔량이동처리를 진행하시겠습니까?";

                            if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) != DialogResult.OK)//NO
                            {
                                return;
                            }
                            else
                            {
                                //FIFO List 재조회
                                ClearData();
                                lblArticle.Text = dr["Article"].ToString();
                                lblArticle.Tag = dr["ArticleID"].ToString();
                                lblBuyerArticleNo.Text = dr["BuyerArticleNo"].ToString();
                                StuffDate = dr["StuffDate"].ToString();
                                FillGridFifoList();
                            }
                        }
                    }
                    else
                    {
                        ClearData();
                        lblArticle.Text = dr["Article"].ToString();
                        lblArticle.Tag = dr["ArticleID"].ToString();
                        lblBuyerArticleNo.Text = dr["BuyerArticleNo"].ToString();
                        StuffDate = dr["StuffDate"].ToString();
                        FillGridFifoList();
                    }
                    if (!CheckCondition(dr))
                    { return; }

                    // A Lot 그리드와 B Lot 그리드 둘다 Row가 없을때 단위값을 넣어준다.
                    if (grd_ALot.RowCount == 0 && grd_BLot.RowCount == 0 && grd_BLotSum.RowCount == 1)
                    {
                        grd_BLotSum.Rows[0].Cells["UnitClssName"].Value = dr["UnitClssName"].ToString();
                        grd_AllLotSum.Rows[0].Cells["UnitClssName"].Value = dr["UnitClssName"].ToString();
                    }

                    if (grd_FIFOList.RowCount > 0)
                    {
                        foreach (DataGridViewRow dgvr in grd_FIFOList.Rows)
                        {
                            if (dgvr.Cells["LotID"].Value.ToString() == dr["LotID"].ToString())
                            {
                                dgvr.Visible = false;
                                FIFO_RANK = dgvr.Cells["FIFO_RANK"].Value.ToString();
                            }
                        }
                    }

                    double db_RemainQty = 0;
                    double.TryParse(dr["RemainQty"].ToString(), out db_RemainQty);

                    if (rbn_ALot.Checked)
                    {
                        if (grd_ALot.RowCount > 0)
                        {
                            grd_ALot.Rows.Clear();
                        }

                        grd_ALot.Rows.Add(grd_ALot.RowCount + 1,
                                        dr["LotID"].ToString(),
                                        dr["LabelGubun"].ToString(),            //'LabelGubun
                                        dr["OrderID"].ToString(),               //'OrderID
                                        string.Format("{0:n2}", db_RemainQty),  //'잔량  
                                        Frm_tprc_Main.Lib.MakeDateTime("yyyymmdd", dr["StuffDate"].ToString()),
                                        dr["LocID"].ToString(),
                                        dr["UnitClssName"].ToString(),
                                        dr["UnitClss"].ToString(),
                                        FIFO_RANK,
                                        "삭제"
                                        );
                        FillGrdBLotSum();
                        rbn_BLot.Checked = true;
                        rbn_GrdBLot.Checked = true;
                        foreach (DataGridViewRow dgvr in grd_FIFOList.Rows)
                        {
                            if (dgvr.Visible)
                            {
                                dgvr.Selected = true;
                                break;
                            }
                        }
                    }
                    else if (rbn_BLot.Checked)
                    {
                        grd_BLot.Rows.Add(grd_BLot.RowCount + 1,
                                        dr["LotID"].ToString(),
                                        dr["LabelGubun"].ToString(),            //'LabelGubun
                                        dr["OrderID"].ToString(),               //'OrderID
                                        string.Format("{0:n2}", db_RemainQty),  //'잔량
                                        Frm_tprc_Main.Lib.MakeDateTime("yyyymmdd", dr["StuffDate"].ToString()),
                                        dr["LocID"].ToString(),
                                        dr["UnitClssName"].ToString(),
                                        dr["UnitClss"].ToString(),
                                        FIFO_RANK,
                                        "삭제"
                                        );
                        FillGrdBLotSum();

                        // 처음 B_LOT 에 들어올때, 총 잔량을 리스트[전역변수] 에 기입.                        
                        decimal TotalRemainQty = 0;
                        string totalremain = string.Format("{0:n2}", db_RemainQty);
                        decimal.TryParse(totalremain, out TotalRemainQty);
                        list_B_Lot_TotalRemainQty.Insert(grd_BLot.RowCount - 1, TotalRemainQty);

                    }
                    grd_BLotSum.Rows[0].Cells["UnitClssName"].Value = dr["UnitClssName"].ToString();
                    grd_AllLotSum.Rows[0].Cells["UnitClssName"].Value = dr["UnitClssName"].ToString();
                }
                else
                {
                    Message[0] = "[바코드 오류]";
                    Message[1] = "스캔한 바코드로 검색되는 LotID가 없습니다. \r\n잘못된 바코드를 스캔하였거나, 해당 Lot의 입고내역이 없습니다.";
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                }
                m_BarCode = txtBarCode.Text.ToUpper().Trim();
                txtBarCode.Text = "";
                StuffDate = "";
            }
            catch(Exception ex)
            {
                int i = 0;
                if (ex.Message.Length > 50)
                {
                    i = 1;
                }
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1, i);
                txtBarCode.Text = "";
                txtBarCode.Select();
                txtBarCode.Focus();
                StuffDate = "";
            }
        }

        private void btnInit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckData())
            {
                txtBarCode.Focus();
                return;
            }

            // 저장하기 전에 먼저 설정수량을 확인한다. 2019.05.17 허윤구           
            if (lblSetupQuantity.Text == "0.00")
            {
                // case 1. 설정수량이 비어있다면,
                if (WizCommon.Popup.MyMessageBox.ShowBox("설정수량을 선택하지 않으셨습니다. \r\n 그대로 진행하시겠습니까?", "[잔량이동]", 0, 0) == DialogResult.OK)
                {
                    if (SaveData())
                    {
                        DialogResult = DialogResult.OK;
                        ClearData();
                        txtBarCode.Select();
                        txtBarCode.Focus();
                        rbn_ALot.Checked = true;
                    }
                }
            }
            else
            {
                if (SetupQuantity_BLotReSetting())      // 설정수에 맞게 B 변화.
                {
                    FillGrdBLotSum();                   // 설정수 맞춰서 BLOTSUM 값 변화.
                    if (SaveData())
                    {
                        DialogResult = DialogResult.OK;
                        ClearData();
                        txtBarCode.Select();
                        txtBarCode.Focus();
                        rbn_ALot.Checked = true;
                    }
                }            
            }

            //if (SaveData())
            //{
            //    DialogResult = DialogResult.OK;
            //    ClearData();
            //    txtBarCode.Select();
            //    txtBarCode.Focus();
            //}
        }

        private void btn_sArticleID_Click(object sender, EventArgs e)
        {
            if (grd_ALot.Rows.Count <= 0)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("A Lot에 라벨을 먼저 등록해주세요.", "[품번 변경 불가]", 0, 1);
                return;
            }

            //if (rbn_ALot.Checked == false)
            //{
            //    WizCommon.Popup.MyMessageBox.ShowBox("A Lot 버튼을 클릭(활성화) 해주세요.\r\n(품번 변경은 ALot 기준으로만 변경 가능합니다.) ", "[품번 변경 불가]", 0, 1);
            //    return;
            //}

            string LotID = grd_ALot.Rows[0].Cells["LotID"].Value.ToString();

            foreach (Form openForm in Application.OpenForms)//중복실행방지
            {
                if (openForm.Name == "Frm_PopUpSel_sArticle_O")
                {
                    openForm.BringToFront();
                    openForm.Activate();
                    return;
                }
            }

            Frm_PopUpSel_sArticle_ByScan fps = new Frm_PopUpSel_sArticle_ByScan(LotID);
            fps.AddMessage = "\r\n[품번 변경은 A Lot 기준으로만 변경 가능합니다.]";
            fps.Owner = this;
            fps.BringToFront();
            fps.ShowDialog();

            if (fps.DialogResult == DialogResult.OK)
            {
                string sArticleID = fps.returnArticleID;
                string sArticle = fps.returnArticle;
                string sBuyerArticleNo = fps.returnBuyerArticleNo;

                if (lblArticle.Tag != null
                    && !lblArticle.Tag.ToString().Equals(sArticleID))
                {
                    if (WizCommon.Popup.MyMessageBox.ShowBox("A Lot 품명을 변경하시겠습니까?\r\nA Lot 품명 변경 후 B Lot은 초기화 됩니다.", "[확인]", 0, 0) == DialogResult.OK)
                    {
                        lblArticle.Text = sArticle;
                        lblArticle.Tag = sArticleID;
                        lblBuyerArticleNo.Text = sBuyerArticleNo;
                        grd_ALot.Rows.Clear();
                        grd_BLot.Rows.Clear();
                        rbn_ALot.Checked = true;

                        FillGridBarcode(LotID, sArticleID);
                    }
                }
            }

            //void GetData(string sArticleID, string sArticle, string sBuyerArticleNo)
            //{
            //    lblArticle.Text = sArticle;
            //    lblArticle.Tag = sArticleID;
            //    lblBuyerArticleNo.Text = sBuyerArticleNo;
            //    grd_ALot.Rows.Clear();
            //    grd_BLot.Rows.Clear();
            //    FillGrdBLotSum();
            //    FillGridFifoList();
            //}
        }

        
        // 2019.05.17 허윤구 추가.  설정수량버튼 신규.
        private void btn_sSetupQuantity_Click(object sender, EventArgs e)
        {
            double db_SetupQuantity = 0;
            string SendValue = string.Empty;
            string ReturnValue = string.Empty;

            if (lblSetupQuantity.Text != "0.00")
            {
                db_SetupQuantity = Lib.GetDouble(lblSetupQuantity.Text);
                SendValue = db_SetupQuantity.ToString();
            }

            POPUP.Frm_CMNumericKeypad numkeypad = new POPUP.Frm_CMNumericKeypad();
            POPUP.Frm_CMNumericKeypad.KeypadStr = SendValue;
            POPUP.Frm_CMNumericKeypad.g_Name = "설정수량";
            if (numkeypad.ShowDialog() == DialogResult.OK)
            {
                ReturnValue = numkeypad.tbInputText.Text;
                double.TryParse(ReturnValue, out db_SetupQuantity);
                lblSetupQuantity.Text = string.Format("{0:n2}", db_SetupQuantity);
            }
        }


        // 2019.05.17 허윤구 추가.  설정수량에 따른 B_Lot qty change 정렬.
        private bool SetupQuantity_BLotReSetting()
        {
            // case 2. 설정수량이 있다면, 이 펑션에 들어옵니다.

            double TotalLotSum = Lib.GetDouble(grd_AllLotSum.Rows[0].Cells["WorkSumQty"].Value.ToString());
            double db_SetupQuantity = Lib.GetDouble(lblSetupQuantity.Text);
            if (TotalLotSum >= db_SetupQuantity)
            {
                // case 2-1. A, B Lot 전체의 합계가 설정수 보다 크거나 같을경우,
                double A_LotSum = Lib.GetDouble(grd_ALot.Rows[0].Cells["RemainQty"].Value.ToString());
                double B_LotSum_ing = 0;
                // -----------------------------------------------------------------
                // 2-1-1. A 혼자만으로도 이미 설정수를 뛰어넘어 버렸어.
                if (A_LotSum >= db_SetupQuantity)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("A Lot의 잔량이 설정수량보다 큽니다.", "[이동처리 불필요]", 0, 1);
                    return false;
                }
                // 2-1-2. B Lot를 foreach 돌면서 설정수를 맞춰줍니다.
                int ForeachCount = 0;
                foreach (DataGridViewRow dgvr in grd_BLot.Rows)
                {
                    double db_dgvr_RemainQty = Lib.GetDouble(dgvr.Cells["RemainQty"].Value.ToString());
                    B_LotSum_ing = B_LotSum_ing + db_dgvr_RemainQty;
                    if ((A_LotSum + B_LotSum_ing) > db_SetupQuantity)
                    {
                        // dgvr의 잔량을 설정수량에 일치하게끔 조정.
                        double MinusValue = (A_LotSum + B_LotSum_ing) - db_SetupQuantity;
                        double New_dgvr_qty = db_dgvr_RemainQty - MinusValue;
                        dgvr.Cells["RemainQty"].Value = string.Format("{0:n2}", New_dgvr_qty);
                        break;
                    }
                    else if ((A_LotSum + B_LotSum_ing) == db_SetupQuantity)
                    {
                        break;
                    }
                    else
                    {
                        ForeachCount++;
                    }
                }

                int B_RowCount = grd_BLot.RowCount;
                if (B_RowCount > (ForeachCount + 1))
                {
                    for (int i = ForeachCount + 1; i < B_RowCount; i++)
                    {
                        // 하위 나머지들 값 다 0으로 바꺼버리기.
                        grd_BLot.Rows[i].Cells["RemainQty"].Value = string.Format("{0:n2}", 0);
                    }
                }
                return true;
            }
            else if (TotalLotSum < db_SetupQuantity)
            {
                WizCommon.Popup.MyMessageBox.ShowBox("잔량이동의 총합이 설정수량보다 작습니다.", "[이동처리 불가]", 0, 1);
                return false;
            }
            else
            {
                return false;
            }
        }


        private void btnUp_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowUp(grd_FIFOList);
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowDown(grd_FIFOList);
        }

        private void rbn_ABLot_Click(object sender, EventArgs e)
        {
            RadioButton rbn = sender as RadioButton;
            if (rbn.Name.ToUpper().Contains("ALOT"))
            {
                rbn_ALot.Checked = true;
                rbn_GrdALot.Checked = true;
            }
            else if (rbn.Name.ToUpper().Contains("BLOT"))
            {
                rbn_BLot.Checked = true;
                rbn_GrdBLot.Checked = true;
            }
            
            txtBarCode.Select();
            txtBarCode.Focus();
        }

        private void btnBDown_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowDown(grd_BLot);
        }

        private void btnBUp_Click(object sender, EventArgs e)
        {
            Frm_tprc_Main.Lib.btnRowUp(grd_BLot);
        }

        private void grd_BLot_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {

        }

        private void grd_BLot_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {

        }

        private void grd_BLot_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == grd_BLot.ColumnCount - 1)//마지막 컬럼(삭제버튼 클릭 시)
            {
                int You = grd_BLot.SelectedRows[0].Index;

                foreach (DataGridViewRow dgvr in grd_FIFOList.Rows)
                {
                    if (dgvr.Cells["FIFO_RANK"].Value.ToString() == grd_BLot.Rows[e.RowIndex].Cells["FIFO_RANK"].Value.ToString())
                    {
                        dgvr.Visible = true;
                    }
                }
                grd_BLot.Rows.RemoveAt(e.RowIndex);

                //총 잔량정보 삭제.
                list_B_Lot_TotalRemainQty.RemoveAt(You);

                if (grd_BLot.Rows.Count > 0)
                {
                    int i = 0;
                    foreach (DataGridViewRow dgvr in grd_BLot.Rows)
                    {
                        i++;
                        dgvr.Cells["No"].Value = i.ToString();
                    }
                }
                //계산
                double QtySum = 0;
                double dQty = 0;
                string UCN = "";
                foreach (DataGridViewRow dgvr in grd_BLot.Rows)
                {
                    double.TryParse(dgvr.Cells["RemainQty"].Value.ToString().Replace(",", ""), out dQty);
                    QtySum = QtySum + dQty;
                    UCN = dgvr.Cells["UnitClssName"].Value.ToString();
                }
                if (grd_BLotSum.Rows.Count == 1)
                {
                    grd_BLotSum.Rows[0].Cells["WorkCount"].Value = grd_BLot.Rows.Count + " 건";
                    grd_BLotSum.Rows[0].Cells["WorkSumQty"].Value = string.Format("{0:n3}", QtySum);
                    //A, B 모두의 GridRow가 0일때 단위 삭제
                    if (grd_ALot.RowCount == 0 && grd_BLot.RowCount == 0)
                    {
                        grd_BLotSum.Rows[0].Cells["UnitClssName"].Value = "";
                        grd_AllLotSum.Rows[0].Cells["UnitClssName"].Value = "";
                    }
                }
                FillGrdBLotSum();
                setCheckClearArticleID();

            }
            txtBarCode.Select();
            txtBarCode.Focus();
        }

        private void btnFifoChoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (grd_FIFOList.SelectedRows.Count > 0)
                {
                    bool blUnit = false;    //단위값을 넣을지 말지에 대한 bool
                    if (rbn_GrdALot.Checked)
                    {
                        if (!CheckCondition(grd_FIFOList.SelectedRows[0]))
                        { return; }
                        //A Lot 그리드에 Row가 있고, FIFO_RANK 값이 있을 때 VISIBLE = FALSE 되있는 ROW를 TRUE로 변경해준다.
                        if (grd_ALot.RowCount == 1)
                        {
                            if (grd_ALot.Rows[0].Cells["FIFO_RANK"].Value.ToString() != "")
                            {
                                foreach (DataGridViewRow dgvr in grd_FIFOList.Rows)
                                {
                                    if (dgvr.Cells["FIFO_RANK"].Value.ToString() == grd_ALot.Rows[0].Cells["FIFO_RANK"].Value.ToString())
                                    {
                                        dgvr.Visible = true;
                                        break;
                                    }
                                }
                            }
                        }
                        grd_ALot.Rows.Clear();
                        // A Lot 그리드와 B Lot 그리드 둘다 Row가 없을때 단위값을 넣어준다.
                        //여기서는 단위값을 아직 받지 않았으므로 할지 안할지에 대한 여부만 체크
                        if (grd_ALot.RowCount == 0 && grd_BLot.RowCount == 0)
                        {
                            blUnit = true;
                        }
                        grd_ALot.Rows.Add();
                        for (int k = 0; k < grd_FIFOList.SelectedRows[0].Cells.Count; k++)
                        {
                            if (grd_FIFOList.SelectedRows[0].Cells[k].Value != null)
                            {
                                grd_ALot.Rows[0].Cells[k].Value = grd_FIFOList.SelectedRows[0].Cells[k].Value.ToString();
                            }
                        }
                        //단위값 넣어준다.
                        if (blUnit && grd_BLotSum.RowCount == 1)
                        {
                            grd_BLotSum.Rows[0].Cells["UnitClssName"].Value = grd_ALot.Rows[0].Cells["UnitClssName"].Value.ToString();
                            grd_AllLotSum.Rows[0].Cells["UnitClssName"].Value = grd_ALot.Rows[0].Cells["UnitClssName"].Value.ToString();
                        }

                        rbn_BLot.Checked = true;
                        rbn_GrdBLot.Checked = true;
                    }
                    else if (rbn_GrdBLot.Checked)
                    {
                        if (!CheckCondition(grd_FIFOList.SelectedRows[0]))
                        { return; }
                        // A Lot 그리드와 B Lot 그리드 둘다 Row가 없을때 단위값을 넣어준다.
                        //여기서는 단위값을 아직 받지 않았으므로 할지 안할지에 대한 여부만 체크
                        if (grd_ALot.RowCount == 0 && grd_BLot.RowCount == 0)
                        {
                            blUnit = true;
                        }
                        grd_BLot.Rows.Add();
                        for (int k = 0; k < grd_FIFOList.SelectedRows[0].Cells.Count; k++)
                        {
                            if (k == 0)
                            {
                                grd_BLot.Rows[grd_BLot.RowCount - 1].Cells[k].Value = grd_BLot.RowCount.ToString();
                            }
                            else if (grd_FIFOList.SelectedRows[0].Cells[k].Value != null)
                            {
                                grd_BLot.Rows[grd_BLot.RowCount - 1].Cells[k].Value = grd_FIFOList.SelectedRows[0].Cells[k].Value.ToString();
                            }
                        }
                        //단위값 넣어준다.
                        if (blUnit && grd_BLotSum.RowCount == 1)
                        {
                            grd_BLotSum.Rows[0].Cells["UnitClssName"].Value = grd_BLot.Rows[grd_BLot.RowCount - 1].Cells["UnitClssName"].Value.ToString();
                            grd_AllLotSum.Rows[0].Cells["UnitClssName"].Value = grd_BLot.Rows[grd_BLot.RowCount - 1].Cells["UnitClssName"].Value.ToString();
                        }

                        // 처음 B_LOT 에 들어올때, 총 잔량을 리스트[전역변수] 에 기입.
                        if (grd_FIFOList.SelectedRows[0].Cells["RemainQty"].Value != null)
                        {
                            decimal TotalRemainQty = 0;
                            string totalremain = grd_FIFOList.SelectedRows[0].Cells["RemainQty"].Value.ToString();

                            decimal.TryParse(totalremain, out TotalRemainQty);
                            list_B_Lot_TotalRemainQty.Insert(grd_BLot.RowCount - 1, TotalRemainQty);
                        }                                                    
                    }
                    grd_FIFOList.SelectedRows[0].Visible = false;
                    foreach (DataGridViewRow dgvr in grd_FIFOList.Rows)
                    {
                        if (dgvr.Visible)
                        {
                            dgvr.Selected = true;
                            break;
                        }
                    }
                    FillGrdBLotSum();
                    txtBarCode.Select();
                    txtBarCode.Focus();
                }
            }
            catch (Exception ex)
            {
                Message[0] = "[오류]";
                Message[1] = ex.Message;
                WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                return;
            }
        }

        private void grd_ALot_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0)
            {
                return;
            }
            if (e.ColumnIndex == grd_ALot.ColumnCount - 1)//마지막 컬럼(삭제버튼 클릭 시)
            {
                foreach (DataGridViewRow dgvr in grd_FIFOList.Rows)
                {
                    if (dgvr.Cells["FIFO_RANK"].Value.ToString() == grd_ALot.Rows[e.RowIndex].Cells["FIFO_RANK"].Value.ToString())
                    {
                        dgvr.Visible = true;
                    }
                }
                grd_ALot.Rows.RemoveAt(e.RowIndex);
                //A, B 모두의 GridRow가 0일때 단위 삭제
                if (grd_BLotSum.Rows.Count == 1)
                {
                    if (grd_ALot.RowCount == 0 && grd_BLot.RowCount == 0)
                    {
                        grd_BLotSum.Rows[0].Cells["UnitClssName"].Value = "";
                        grd_AllLotSum.Rows[0].Cells["UnitClssName"].Value = "";
                    }
                }
                FillGrdBLotSum();
                setCheckClearArticleID();
            }
        }

        #region 2020.07.20 데이터 그리드 삭제 버튼 클릭 시, 모든 데이터 그리드가 비워 졌을 경우에는, lblArticle 도 비워줘야 함

        private void setCheckClearArticleID()
        {
            if (grd_ALot.Rows.Count == 0
                && grd_BLot.Rows.Count == 0)
            {
                lblArticle.Text = "";
                lblArticle.Tag = "";
                lblBuyerArticleNo.Text = "";

                rbn_ALot.Checked = true;
            }
        }

        #endregion


        //2019.05.15 허윤구 기능추가. B_Lot에 대한 잔량수정.
        private void btnBRemainQtyChange_Click(object sender, EventArgs e)
        {
            try
            {
                //WizCommon.Popup.MyMessageBox.ShowBox(string.Format("작업중입니다. \r\n 기능은 담당자에게 문의하세요."), "[공사중]", 0, 1);
                //return;

                double db_NowRemainQty = 0;             // double
                string ReturnValue = string.Empty;
                double db_ReturnEditRemainQty = 0;      // return double

                if (grd_BLot.SelectedRows.Count > 0)
                {
                    int You = grd_BLot.SelectedRows[0].Index;

                    string SendValue = grd_BLot.SelectedRows[0].Cells["RemainQty"].Value.ToString();
                    db_NowRemainQty = Convert.ToDouble(SendValue);
                    SendValue = db_NowRemainQty.ToString();

                    POPUP.Frm_CMNumericKeypad numkeypad = new POPUP.Frm_CMNumericKeypad();
                    POPUP.Frm_CMNumericKeypad.KeypadStr = SendValue;
                    POPUP.Frm_CMNumericKeypad.g_Name = "잔량";
                    if (numkeypad.ShowDialog() == DialogResult.OK)
                    {
                        ReturnValue = numkeypad.tbInputText.Text;

                        decimal Check = list_B_Lot_TotalRemainQty[You];
                        if (Convert.ToDecimal(ReturnValue) > Check)
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox(string.Format("잔량 범위를 벗어났습니다. \r\n 총 잔량보다 작은값을 기입하세요. \r\n 총 잔량 : {0}", Check), "[잔량초과]", 0, 1);
                            return;
                        }
                        else
                        {
                            double.TryParse(ReturnValue, out db_ReturnEditRemainQty);
                            grd_BLot.SelectedRows[0].Cells["RemainQty"].Value = string.Format("{0:n2}", db_ReturnEditRemainQty);
                        }
                    }
                    FillGrdBLotSum();
                    txtBarCode.Select();
                    txtBarCode.Focus();
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                return;
            }
        }


        private void btnBLotRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (grd_BLot.SelectedRows.Count > 0)
                {
                    int You = grd_BLot.SelectedRows[0].Index;

                    foreach (DataGridViewRow dgvr in grd_FIFOList.Rows)
                    {
                        if (dgvr.Cells["FIFO_RANK"].Value.ToString() == grd_BLot.SelectedRows[0].Cells["FIFO_RANK"].Value.ToString())
                        {
                            dgvr.Visible = true;
                        }
                    }
                    grd_BLot.Rows.Remove(grd_BLot.SelectedRows[0]);

                    //총 잔량정보 삭제.
                    list_B_Lot_TotalRemainQty.RemoveAt(You);
                    if (grd_BLot.Rows.Count > 0)
                    {
                        int i = 0;
                        foreach (DataGridViewRow dgvr in grd_BLot.Rows)
                        {
                            i++;
                            dgvr.Cells["No"].Value = i.ToString();
                        }
                    }
                    FillGrdBLotSum();
                    txtBarCode.Select();
                    txtBarCode.Focus();
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                return;
            }
            
        }

        private void frm_mtr_RemainQtyMoveByLotID2_U_Activated(object sender, EventArgs e)
        {
            ((Frm_tprc_Main)(MdiParent)).LoadRegistry();
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