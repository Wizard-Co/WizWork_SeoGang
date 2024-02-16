using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;

namespace WizWork
{
    public partial class frm_mtr_InputMove_U2 : Form
    {
        string[] Message = new string[2];
        WizWorkLib Lib = new WizWorkLib();
        INI_GS gs = new INI_GS();
        WizCommon.Popup.Frm_CMKeypad FK = null;

        List<TOUTWARE> list_TOutware = null;
        List<TOUTWARESUB> list_TOutwareSub = null;
        List<WizCommon.Procedure> Prolist = null;
        List<Dictionary<string, object>> ListParameter = null;
        string m_BarCode = "";
        string mWorkAlarmID = "";
        string EffectDate = "";
        double qty = 0;

        public frm_mtr_InputMove_U2()
        {
            InitializeComponent();
        }

        public frm_mtr_InputMove_U2(string WorkAlarmID = "")
        {
            InitializeComponent();
            mWorkAlarmID = WorkAlarmID;
        }

        private void frm_mtr_InputMove_U2_Load(object sender, EventArgs e)
        {
            SetScreen();
            InitGrid();
            setComboBox();

            ClearData();

            EnabledTrue();
            if (mWorkAlarmID != "")
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@WorkAlarmID", mWorkAlarmID);
                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sArticleByWorkAlarmID", sqlParameter, false);

                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    lblArticle.Text = dr["Article"].ToString();
                    lblArticle.Tag = dr["ArticleID"].ToString();
                    lblSabun.Text = dr["Sabun"].ToString();
                    lblSabun.Tag = dr["UnitClss"].ToString();
                }
                else
                {
                    //품명을 확인해주세요.
                    WizCommon.Popup.MyMessageBox.ShowBox("요청한 자재의 사번을 다시 확인해주세요. 사번정보가 이상합니다.", "[오류]", 0, 1);
                }
            }

            txtBarCode.Select();
            txtBarCode.Focus();
        }

        private void ClearData()
        {
            lblArticle.Text = string.Empty;
            lblArticle.Tag = string.Empty;
            txtBarCode.Text = string.Empty;
            lblOutQty.Text = string.Empty;
            lblOutRoll.Text = string.Empty;
            lblSabun.Text = string.Empty;

            grdData.Rows.Clear();

            InitGrid();
        }

        private void EnabledFalse()
        {
            cboFromLoc.Enabled = false;
            cboToLoc.Enabled = false;
            btnBarcode.Enabled = false;
            txtBarCode.Enabled = false;
            btnSave.Enabled = false;
            btnInit.Enabled = false;
        }

        private void EnabledTrue()
        {
            cboFromLoc.Enabled = true;
            cboToLoc.Enabled = true;
            btnBarcode.Enabled = true;
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
            ///BOX별 검사실적 grdData///
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            grdData.ColumnCount = 13;

            int i = 0;

            grdData.Columns[i].Name = "Seq";
            grdData.Columns[i].HeaderText = "";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Barcode";
            grdData.Columns[i].HeaderText = "바코드번호";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "Qty";
            grdData.Columns[i].HeaderText = "이동제품수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;
            
            grdData.Columns[++i].Name = "MoveableQty";
            grdData.Columns[i].HeaderText = "이동수량";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            grdData.Columns[++i].Name = "OutSubSeq";
            grdData.Columns[i].HeaderText = "출고SubSeq";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "UnitPrice";
            grdData.Columns[i].HeaderText = "단가";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "Amount";
            grdData.Columns[i].HeaderText = "금액";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "VATINDYN";
            grdData.Columns[i].HeaderText = "부가세별도";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "OrderSeq";
            grdData.Columns[i].HeaderText = "OrderSeq";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "LabelGubun";
            grdData.Columns[i].HeaderText = "LabelGubun";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "OrderID";
            grdData.Columns[i].HeaderText = "OrderID";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "UnitClss";
            grdData.Columns[i].HeaderText = "UnitClss";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = false;

            grdData.Columns[++i].Name = "EffectDate";
            grdData.Columns[i].HeaderText = "유효기간";
            grdData.Columns[i].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            grdData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grdData.Columns[i].ReadOnly = true;
            grdData.Columns[i].Visible = true;

            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            {
                btnCol.HeaderText = "삭제";
                btnCol.Name = "btnDelete";
                btnCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                btnCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                btnCol.Visible = true;
            }
            grdData.Columns.Insert(++i, btnCol);

            grdData.Font = new Font("맑은 고딕", 25, FontStyle.Bold);
            grdData.RowTemplate.Height = 40;
            grdData.ColumnHeadersHeight = 45;
            grdData.ScrollBars = ScrollBars.Both;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            grdData.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(234, 234, 234);
            grdData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            grdData.ReadOnly = true;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


        }
        #endregion

        private void setComboBox()
        {
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@psCodeGroup", "LOC");
                sqlParameter.Add("@psUseYN", "Y");

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sGetComCodeList", sqlParameter, false);
                DataRow[] seldr = dt.Select("Relation <> 'NONE'");
                DataTable seldt = dt.Clone();

                foreach (DataRow dr in seldr)
                {
                    seldt.Rows.Add(dr.ItemArray);
                }

                dt = null;
                cboFromLoc.DataSource = seldt;

                cboFromLoc.DisplayMember = "CODE_NAME";
                cboFromLoc.ValueMember = "CODE_ID";

                dt = seldt.Copy();
                cboToLoc.DataSource = dt;

                cboToLoc.DisplayMember = "CODE_NAME";
                cboToLoc.ValueMember = "CODE_ID";

                if (cboFromLoc.Items.Count > 0)
                {
                    cboFromLoc.SelectedIndex = 0;
                }
                if (cboToLoc.Items.Count > 0)
                {
                    cboToLoc.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }


        }

        private void CalcRollSum()
        {
            int nTRoll = 0;
            double douTQty      = 0;
            double douTRealQty  = 0;
            int nNo = 0;

            foreach (DataGridViewRow dgvr in grdData.Rows)
            {
                nTRoll = nTRoll + 1;
                douTQty = douTQty + Lib.GetDouble(dgvr.Cells["MoveableQty"].Value.ToString());
                douTRealQty = douTRealQty + Lib.GetDouble(dgvr.Cells["MoveableQty"].Value.ToString());
                dgvr.Cells["Seq"].Value = (++nNo).ToString();
            }

            lblOutRoll.Text = string.Format("{0:n0}", nTRoll);
            lblOutQty.Text = string.Format("{0:n2}", douTQty);
        }

        private bool CheckData()
        {
            try
            {
                //'품명
                if (lblArticle.Text == "")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("품명을 입력해야 합니다.", "[오류]", 0, 1);
                    return false;
                }

                //20180928 주석처리 박스수량(wlp.qty) 이동수량(moveableqty) 중 무조건 moveableqty로 
                //이동처리하기로 변경하면서 주석처리
                //double outqty = 0;
                //double moveableqty = 0;

                //for (int i = grdData.Rows.Count - 1; i >= 0; i--)
                //{
                //    DataGridViewRow dgvr = grdData.Rows[i];
                //    outqty = Lib.GetDouble(dgvr.Cells["Qty"].Value.ToString());
                //    moveableqty = Lib.GetDouble(dgvr.Cells["MoveableQty"].Value.ToString());
                //    if (outqty > moveableqty || moveableqty < 0)
                //    {
                //        if (WizCommon.Popup.MyMessageBox.ShowBox(dgvr.Cells["Barcode"].Value.ToString() + "의 이동가능한 수량을 초과하였습니다.\r\n" +
                //            "이동가능수량 : " + dgvr.Cells["MoveableQty"].Value.ToString() + " / 이동제품수량 : " + dgvr.Cells["Qty"].Value.ToString() + "입니다.\r\n" +
                //            "[OK] 버튼을 누르시면 [" + dgvr.Cells["Barcode"].Value.ToString() + "] 롯트 제외 후 이동처리 합니다. \r\n계속 진행하시겠습니까?"
                //            , "[이동가능 수량초과]", 0, 0, 1) == DialogResult.OK)
                //        {
                //            grdData.Rows.RemoveAt(i);
                //        }
                //        else
                //        {
                //            txtBarCode.Select();
                //            txtBarCode.Focus();
                //            return false;
                //        }
                //    }
                //}
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


        private void FillGridData()
        {
            try
            {
                //xp_Outware_sOutwareSubGroup
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@OutwareID", ""/*txtOutWareNo.Text*/);

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_Outware_sOutwareSubGroup", sqlParameter, false);

                double outqty = 0;
                EffectDate = "";
                foreach (DataRow dr in dt.Rows)
                {
                    EffectDate = Lib.MakeDateTime("yyyyMMdd", dr["EffectDate"].ToString());
                    double.TryParse(dr["OutQty"].ToString(), out outqty);
                    grdData.Rows.Add(grdData.Rows.Count + 1,
                                    dr["LabelID"].ToString(),               //'라벨ID
                                    dr["LabelGubunName"].ToString(),        //'라벨구분명
                                    string.Format("{0:n3}", outqty),
                                    0,                                      //이동가능수량
                                    dr["OutSubSeq"].ToString(),
                                    dr["Unitprice"].ToString(),
                                    dr["Amount"].ToString(),
                                    dr["Vat_Ind_YN"].ToString(),
                                    dr["LabelGubun"].ToString(),
                                    dr["OrderID"].ToString(),
                                    EffectDate,
                                    "삭제"
                                    );
                }
            }
            catch (Exception e)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", e.Message), "[오류]", 0, 1);
            }
        }


        private void GetOrderID()
        {
            WizCommon.Popup.Frm_CMNumericKeypad FK = new WizCommon.Popup.Frm_CMNumericKeypad("", "관리번호");
            WizCommon.Popup.Frm_CMNumericKeypad.KeypadStr = txtBarCode.Text.Trim();
            if (FK.ShowDialog() == DialogResult.OK)
            {
                txtBarCode.Text = FK.tbInputText.Text;
                //chkOrderID.Focus();
            }
            else
            {
                //IsOK = true;
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 잘못된 관리번호입니다.\r\n{0}", FK.tbInputText.Text), "[오류]", 0, 1);
                txtBarCode.Text = string.Empty;
                txtBarCode.Enabled = false;
                //chkOrderID.Checked = false;
                //chkOrderID.Focus();
                GetOrderID();
            }
        }

        private bool SaveData()
        {
            try
            {
                if (grdData.Rows.Count == 0)
                {
                    return false;
                }

                //if (grdData.SelectedRows.Count == 0 || grdData.SelectedRows.Count > 1)
                //{
                //    //WizCommon.Popup.MyMessageBox.ShowBox(WizCommon.Properties.Resources._996, "[확인]", 0, 1);
                //    return false;
                //}
                else if (grdData.Rows.Count > 0)
                {
                    list_TOutware = new List<TOUTWARE>();
                    list_TOutwareSub = new List<TOUTWARESUB>();
                    Prolist = new List<WizCommon.Procedure>();
                    ListParameter = new List<Dictionary<string, object>>();

                    foreach (DataGridViewRow dgvr in grdData.Rows)
                    {
                        TOUTWARE tOUTWARE = new TOUTWARE();
                        tOUTWARE.OrderID = dgvr.Cells["OrderID"].Value.ToString();
                        tOUTWARE.CompanyID = "0001";

                        tOUTWARE.OutClss = "09";                //'이동구분 09: 재단 --> 생산 19: 재단 --> 생산
                        tOUTWARE.CustomID = "0001";                                 //'이동의 경우에는 거래처가 없으므로 해당 시스템이 설치된 업체의 코드를 가져옴(해당시스템 업체의 거래처명, 매출)
                        tOUTWARE.BuyerDirectYN = "N";
                        tOUTWARE.WorkID = "0001";                                   //'가공구분
                        tOUTWARE.ExchRate = "0";

                        tOUTWARE.InsStuffINYN = "Y";                               //'동시입고 Y, 이동이므로 출고와 동시입고 처리함

                        tOUTWARE.OutCustomID = "0001";                            //'이동의 경우에는 거래처가 없으므로 해당 시스템이 설치된 업체의 코드를 가져옴(해당시스템 업체의 거래처명, 매출)
                        tOUTWARE.OutCustom = "한국하이테크이동";
                        tOUTWARE.LossRate = "0";
                        tOUTWARE.LossQty = "0";
                        tOUTWARE.OutRoll = "1";// Lib.GetDouble(lblOutRoll.Text).ToString();                   //'건수
                        tOUTWARE.OutQty = Lib.GetDouble(dgvr.Cells["MoveableQty"].Value.ToString()).ToString();//Lib.GetDouble(lblOutQty.Text).ToString();                     //'출고량-정산에서 사용
                        tOUTWARE.OutRealQty = Lib.GetDouble(dgvr.Cells["MoveableQty"].Value.ToString()).ToString();//Lib.GetDouble(lblOutQty.Text).ToString();                 //'소요량-수불에서 사용

                        tOUTWARE.OutDate = DateTime.Now.ToString("yyyyMMdd");   // '출고일자
                        tOUTWARE.ResultDate = tOUTWARE.OutDate;

                        tOUTWARE.BoOutClss = "";
                        tOUTWARE.OutTime = DateTime.Now.ToString("HHmm");
                        tOUTWARE.LoadTime = DateTime.Now.ToString("HHmm");

                        tOUTWARE.Remark = "";
                        tOUTWARE.OutType = "3";                                    //'스켄출고
                        tOUTWARE.snVatAmount = "0";                                   //'이동의 경우 금액 0 -- lblVatINDYN.Caption
                        tOUTWARE.VatINDYN = "0";                                   //'이동의 경우 금액 0 -- lblVatINDYN.Caption
                        tOUTWARE.sUnitClss = lblSabun.Tag.ToString();// "09";

                        tOUTWARE.sFromLocID = cboFromLoc.SelectedValue.ToString(); //'From 창고
                        tOUTWARE.sToLocID = cboToLoc.SelectedValue.ToString();   //'TO 창고
                        tOUTWARE.sArticleID = lblArticle.Tag.ToString();            //          '품명코드

                        //list_TOutware.Add(tOUTWARE);
                        //tOUTWARE = null;

                        TOUTWARESUB tOUTWARESUB = new TOUTWARESUB();
                        tOUTWARESUB.sOutwareID = ""; 
                        tOUTWARESUB.OrderID = dgvr.Cells["OrderID"].Value.ToString();
                        tOUTWARESUB.OutSubSeq = "1";
                        tOUTWARESUB.OrderSeq = "0";
                        tOUTWARESUB.ArticleID = lblArticle.Tag.ToString();
                        tOUTWARESUB.RollSeq = "0";
                        tOUTWARESUB.LotNo = "0";
                        tOUTWARESUB.OutQty = Lib.GetDouble(dgvr.Cells["MoveableQty"].Value.ToString()).ToString(); Lib.GetDouble(dgvr.Cells["Qty"].Value.ToString()).ToString();
                        tOUTWARESUB.OutRoll = "1";
                        tOUTWARESUB.LabelID = dgvr.Cells["Barcode"].Value.ToString();
                        tOUTWARESUB.LabelGubun = dgvr.Cells["LabelGubun"].Value.ToString();
                        tOUTWARESUB.Unitprice = "0";

                        //list_TOutwareSub.Add(tOUTWARESUB);
                        //tOUTWARESUB = null;

                        Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                        sqlParameter.Add("@OrderID",    tOUTWARE.OrderID);
                        sqlParameter.Add("@CompanyID",  tOUTWARE.CompanyID);
                        sqlParameter.Add("@OutSeq",     tOUTWARE.OutSeq);
                        sqlParameter.Add("@OutwareID",  tOUTWARE.sOutwareID);         //'출고번호		
                        sqlParameter.Add("@OutClss",    tOUTWARE.OutClss);
                        sqlParameter.Add("@CustomID",   tOUTWARE.CustomID);
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
                        sqlParameter.Add("@UserID", "");
                        //output 2개 ow.OutSeq  //ow.sOutwareID
                        WizCommon.Procedure pro1 = new WizCommon.Procedure();
                        pro1.list_OutputName = new List<string>();
                        pro1.list_OutputLength = new List<string>();

                        pro1.Name = "xp_Outware_iOutware";
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
                        sqlParameter2.Add("@ArticleID", tOUTWARE.sArticleID);
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
                        sqlParameter2.Add("@UserID", "");
                        sqlParameter2.Add("@CustomBoxID", tOUTWARESUB.CustomBoxID); //'고객 BoxID

                        WizCommon.Procedure pro2 = new WizCommon.Procedure();
                        pro2.list_OutputName = new List<string>();
                        pro2.list_OutputLength = new List<string>();

                        pro2.Name = "xp_Outware_iOutwareSub";
                        pro2.OutputUseYN = "N";
                        pro2.list_OutputName.Add("sRtnMsg");
                        pro2.list_OutputLength.Add("30");

                        Prolist.Add(pro2);
                        ListParameter.Add(sqlParameter2);

                        if (tOUTWARE.InsStuffINYN == "Y" && tOUTWARE.BuyerDirectYN != "Y")
                        {
                            Dictionary<string, object> sqlParameter3 = new Dictionary<string, object>();
                            sqlParameter3.Add("@OutwareID", tOUTWARE.sOutwareID);
                            sqlParameter3.Add("@sUserID", "");
                            sqlParameter3.Add("@sOutmsg", "");

                            WizCommon.Procedure pro3 = new WizCommon.Procedure();
                            pro3.list_OutputName = new List<string>();
                            pro3.list_OutputLength = new List<string>();

                            pro3.Name = "xp_StuffIN_iStuffINByOutware";
                            pro3.OutputUseYN = "N";
                            pro3.list_OutputName.Add("sOutmsg");
                            pro3.list_OutputLength.Add("500");

                            Prolist.Add(pro3);
                            ListParameter.Add(sqlParameter3);
                        }
                        else
                        {
                            Dictionary<string, object> sqlParameter3 = new Dictionary<string, object>();
                            sqlParameter3.Add("@LabelID", tOUTWARESUB.LabelID);
                            sqlParameter3.Add("@LabelGubun", tOUTWARESUB.LabelGubun);
                            sqlParameter3.Add("@ArticleID", tOUTWARESUB.ArticleID);
                            sqlParameter3.Add("@OutDate", tOUTWARE.OutDate);
                            sqlParameter3.Add("@OutTime", tOUTWARE.OutTime);
                            sqlParameter3.Add("@UserID", "");

                            WizCommon.Procedure pro3 = new WizCommon.Procedure();
                            pro3.list_OutputName = new List<string>();
                            pro3.list_OutputLength = new List<string>();

                            pro3.Name = "xp_LabelMaster_UOutware";
                            pro3.OutputUseYN = "N";
                            pro3.list_OutputName.Add("sRtnMsg");
                            pro3.list_OutputLength.Add("30");

                            Prolist.Add(pro3);
                            ListParameter.Add(sqlParameter3);
                        }
                        if (mWorkAlarmID != "")//[xp_WizWork_iwkWorkAlarmMtr_Move]
                        {
                            Dictionary<string, object> sqlParameter4 = new Dictionary<string, object>();
                            sqlParameter4.Add("@WorkAlarmID", Lib.GetDouble(mWorkAlarmID));
                            sqlParameter4.Add("@OutWareID", "");
                            sqlParameter4.Add("@CreateUserID", "");

                            WizCommon.Procedure pro4 = new WizCommon.Procedure();
                            pro4.list_OutputName = new List<string>();
                            pro4.list_OutputLength = new List<string>();
                               
                            pro4.Name = "xp_WizWork_iwkWorkAlarmMtr_Move";
                            pro4.OutputUseYN = "N";

                            Prolist.Add(pro4);
                            ListParameter.Add(sqlParameter4);
                        }
                        
                    }

                    List<KeyValue> list_Result = new List<KeyValue>();
                    list_Result = DataStore.Instance.ExecuteAllProcedureOutputListGetCS(Prolist, ListParameter);

                    if (list_Result[0].key.ToLower() == "success")
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("이동처리가 완료되었습니다.", "[이동처리완료]", 3, 1);
                    }
                    else
                    {
                        WizCommon.Popup.MyMessageBox.ShowBox("[이동처리실패]\r\n" + list_Result[0].value.ToString(), "[오류]", 0, 1);
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
            if (WizCommon.Popup.MyMessageBox.ShowBox("이동 처리를 종료하시겠습니까?", "[종료]", 0, 0) == DialogResult.OK)
            {
                this.Dispose();
                this.Close();
            }
        }

        private void btnBarcode_Click(object sender, EventArgs e)
        {
            FK = new WizCommon.Popup.Frm_CMKeypad(txtBarCode.Text.Trim(), "스캔");
            if (FK.ShowDialog() == DialogResult.OK)
            {
                txtBarCode.Text = FK.tbInputText.Text.Trim();
                if (txtBarCode.Text.Length == 10)
                {
                    txtBarCode_Enter();
                    //임시주석 수정요망
                    //ScanLotNo();
                    //FillGridData();
                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n'{0}'는 올바른 바코드 번호가 아닙니다.", txtBarCode.Text), "[오류]", 0, 1);
                    txtBarCode.Text = string.Empty;
                }
            }
            else
            {
                txtBarCode.Text = string.Empty;
            }
            txtBarCode.Focus();
        }

        private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                txtBarCode_Enter();
            }
        }
        
        private void CheckLabelID()
        {
            string NowBarCode = txtBarCode.Text.Trim().ToUpper();
            //그리드에 같은 LabelID가 있는지
            foreach (DataGridViewRow dgvr in grdData.Rows)
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

        private void txtBarCode_Enter()
        {
            try
            {
                m_BarCode = txtBarCode.Text.ToUpper().Trim();

                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("@LotID", m_BarCode);
                sqlParameter.Add("@sFromLoc", cboFromLoc.SelectedValue.ToString());               

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sLotInfoByLotID", sqlParameter, false);

                if (dt.Rows.Count == 0)
                {
                    string str = "존재하지 않는 바코드 입니다. (" + txtBarCode.Text + ")";
                    throw new Exception(str);
                }

                if (dt.Rows.Count == 1)
                {
                    DataRow dr = dt.Rows[0];
                    if (dr["InspectApprovalYN"].ToString().ToUpper() != "Y")
                    {
                        string str = "승인되지 않은 건은 이동처리 불가 합니다. (" + txtBarCode.Text + ") 바코드품명: " + dr["Article"].ToString() + ", 출고품명: " + lblArticle.Text;
                        throw new Exception(str);
                    }

                    string[] sLblTerm =Frm_tprc_Main.gs.GetValue("Work", "LblTerm", "LblTerm").Split('|');//배열에 공정별 조건 넣기
                    foreach (string LblTerm in sLblTerm)
                    {
                        if (m_BarCode.Substring(0, 1) == LblTerm.Substring(0, 1))
                        {
                            if (LblTerm.Substring(4, 1).ToUpper() == "A")//숙성시간
                            {
                                //숙성시간YN AgingYN = Y면 숙성시간 24시간 지났으므로 사용가능
                                if (dr["AgingYN"].ToString() != "Y")
                                {
                                    Message[0] = "[숙성시간 24시간미만 재료 사용불가]";
                                    Message[1] = "재단 입고 이후 24시간 경과 되지 않은 " + Lib.CheckNull(dr["AgingTime"].ToString()) + "시간 경과된 )  재료 사용 불가능 합니다." +
                                        "숙성시작시간은 " + Lib.CheckNull(dr["AgingStartTime"].ToString()) + " 입니다.";
                                    throw new Exception();
                                }
                            }
                            else if (LblTerm.Substring(4, 1).ToUpper() == "D")//배치검사
                            {
                                //배치검사YN DefectYN == N일때 배치검사 통과
                                if (dr["DefectYN"].ToString() == "")//.Trim() == "")//값이 없으므로, 수행되지 않았음.
                                {
                                    Message[0] = "[배치검사 오류]";
                                    Message[1] = "배치검사가 수행되지 않았습니다. \r\n  실험실에서 배치검사를 진행하여주십시오.";
                                    throw new Exception();
                                }
                                else if (dr["DefectYN"].ToString().ToUpper() == "Y")
                                {
                                    Message[0] = "[배치검사 오류]";
                                    Message[1] = "배치검사를 통과하지 못했습니다. \r\n  해당 품목은 사용할 수 없습니다.";
                                    throw new Exception();
                                }
                            }
                            else if (LblTerm.Substring(4, 1).ToUpper() == "E")//유효기간
                            {
                                if (dr["ChkEffect"].ToString() == "Y")//유효기간 체크여부YN
                                {
                                    if (dr["EffectYN"].ToString() != "Y")//유효기간 EffectYN = Y일때 사용가능
                                    {
                                        Message[0] = "[유효기간 오류]";
                                        Message[1] = "유효기간이 지났습니다. 해당 품목은 사용할 수 없습니다.";
                                        throw new Exception();
                                    }
                                }
                            }
                        }
                    }

                    //'------------------------------------------------------------------------------------------
                    //'Grid에 Label 추가전 오류 check
                    //'------------------------------------------------------------------------------------------
                    if (lblArticle.Tag.ToString() != "" && dr["ArticleID"].ToString() != lblArticle.Tag.ToString())
                    {
                        //string str = "서로 다른 품명을 동시에 출고처리 할수 없습니다. (" + txtBarCode.Text.ToUpper() + ") 바코드품명: " + dr["Article"].ToString() + ", 출고품명: " + lblArticle.Text;
                        string str = "현재 다른 품명을 동시에 출고처리 할 수 없습니다. \r\n바코드 (" + txtBarCode.Text.ToUpper() + ")의 품명은 \r\n'" + dr["Article"].ToString() + "' 입니다. \r\n 출고대상품명은 \r\n'" + lblArticle.Text + "'입니다.";
                        throw new Exception(str);
                    }
                    else if(mWorkAlarmID == "")
                    {
                        lblArticle.Text = dr["Article"].ToString();
                        lblArticle.Tag = dr["ArticleID"].ToString();
                        lblSabun.Text = dr["Sabun"].ToString();
                        lblSabun.Tag = dr["UnitClss"].ToString();
                    }

                    foreach (DataGridViewRow dgvr in grdData.Rows)
                    {
                        if (dgvr.Cells["BarCode"].Value.ToString().ToLower() == txtBarCode.Text.Trim().ToLower())
                        {
                            string str = "이미 스캔된 바코드 입니다. (" + txtBarCode.Text.ToUpper() + ")";
                            throw new Exception(str);
                        }
                    }
                    //'------------------------------------------------------------------------------------------
                    //'Grid에 Label 추가
                    //'------------------------------------------------------------------------------------------
                    qty = 0;
                    EffectDate = string.Empty;
                    EffectDate = Lib.MakeDateTime("yyyyMMdd", dr["EffectDate"].ToString());
                    double.TryParse(dr["LocRemainQty"].ToString(), out qty);
                    grdData.Rows.Add(grdData.Rows.Count + 1,
                                                            txtBarCode.Text.ToUpper().Trim(),
                                                            //dr["LabelGubunName"].ToString(),
                                                            string.Format("{0:n3}", qty),
                                                            dr["LocRemainQty"].ToString(),       //이동가능수량
                                                            "", "", "", "", "",
                                                            dr["LabelGubun"].ToString(),
                                                            dr["OrderID"].ToString(),
                                                            dr["UnitClss"].ToString(),          //단위
                                                            EffectDate,                         //유효기간
                                                            "삭제"
                                                            );

                    


                    //그리드의 row에 롯트가 하나이상 추가되면 전창고, 후창고는 변경될수 없음.
                    //한번의 이동처리에 같은 ArticleID, 같은 전창고, 후창고만 사용할수있다.
                    //cboFromLoc.Enabled = false;
                    //cboToLoc.Enabled = false;
                    
                    if (txtBarCode.Text.ToUpper().Substring(0, 1) == "M")
                    {
                        cboFromLoc.SelectedValue = "A0002";
                        cboToLoc.SelectedValue = "A0003";
                    }
                    else if (txtBarCode.Text.ToUpper().Substring(0, 1) == "T")
                    {
                        cboFromLoc.SelectedValue = "A0003";
                        cboToLoc.SelectedValue = "A0004";
                    }
                    else if (txtBarCode.Text.ToUpper().Substring(0, 1) == "C")
                    {
                        cboFromLoc.SelectedValue = "A0004";
                        cboToLoc.SelectedValue = "A0005";
                    }
                    GetMoveableQty();//이동가능수량 조회
                }
                CalcRollSum();
                txtBarCode.Text = "";
            }
            catch (Exception ex)
            {
                int i = 0;
                if (ex.Message.Length > 50)
                {
                    i = 1;
                }
                if (Message[0] != "" && Message[1] != "")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1, i);
                }
                else if(ex.Message != "")
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1, i);
                }
                Message[0] = string.Empty;
                Message[1] = string.Empty;
                txtBarCode.Text = "";
                txtBarCode.Select();
                txtBarCode.Focus();
                //ClearData();
            }
        }
        
        private void btnInit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckData())
            {
                txtBarCode.Focus();
                return;
            }
            //'-------------------------------------------------------------------------------
            //'생산 후 최소 숙성시간 경과 안된 건 사용 불가, 유효시간 경  고무제품 사용 불가
            //'-------------------------------------------------------------------------------
            if (!CheckID(m_BarCode))
            {
                txtBarCode.Focus();
                return;
            }

            if (SaveData())
            {
                DialogResult = DialogResult.OK;
                //EnabledTrue();
                //FillGridData();
                //ClearData();
                //txtBarCode.Focus();
                this.Close();
            }
        }

        private bool CheckID(string strBoxID)
        {
            DataSet ds = null;
            DataRow dr = null;
            try
            {
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("CLotID", strBoxID);
                ds = DataStore.Instance.ProcedureToDataSet("[xp_WizWork_chkMtrLot]", sqlParameter, false);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    if (dr["EffectDateLeftYN"].ToString() == "N") // 유효기간이 남았다 Yes or No , N일 경우 유효기간 지난걸로 판정
                    {
                        int nProcessID = 0;
                        int.TryParse(Frm_tprc_Main.g_tBase.ProcessID, out nProcessID);
                        if (nProcessID <= 2101)
                        {
                            Message[0] = "[유효일자 초과]";
                            Message[1] = "유효일자 " + Lib.MakeDateTime("YYYYMMDDHHMM", dr["EffectDateTime"].ToString()) + "가 지난 재료 사용 불가능 합니다.";
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                            return false;
                        }
                    }
                    else if (dr["AgingCompleteYN"].ToString() == "N")// 숙성시간 24시간 넘었다 Yes or NO, N일 경우 숙성시간 24시간 미만으로 판정
                    {
                        int nProcessID = 0;
                        int.TryParse(Frm_tprc_Main.g_tBase.ProcessID, out nProcessID);
                        if (nProcessID <= 2101)//재단, 성형
                        {
                            Message[0] = "[숙성시간 미달 사용불가]";
                            Message[1] = "숙성시간 미달 사용불가 \r\n" + "(숙성시간 " + Lib.CheckNull(dr["AgingTime"].ToString()) + "시간 경과 되었습니다.";
                            WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 1);
                            return false;
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의 <CheckID>\r\n{0}", ex.Message), "[오류]", 0, 1);
                return false;
            }
        }

        private void cboFromLoc_SelectedValueChanged(object sender, EventArgs e)
        {
            GetMoveableQty();
        }

        private void GetMoveableQty(DataGridViewRow dgv = null)
        {
            if (dgv == null)//전체 이동가능수량 구하기
            {
                if (cboFromLoc.Items.Count > 0 && grdData.Rows.Count > 0)
                {
                    foreach (DataGridViewRow dgr in grdData.Rows)
                    {
                        GetData(dgr);
                    }
                }
            }
            else//하나의 row의 이동가능수량만 구하기
            {
                if (cboFromLoc.Items.Count > 0 && grdData.Rows.Count > 0)
                {
                    GetData(dgv);
                }
            }
            
            
        }
        void GetData(DataGridViewRow dgvr)
        {
            string BarCode = dgvr.Cells["Barcode"].Value.ToString();
            string sFromLoc = cboFromLoc.SelectedValue.ToString();
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("@LabelID", BarCode);
            sqlParameter.Add("@sLocID", sFromLoc);

            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sLabelQtyByStock", sqlParameter, false);

            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                double douStockQty = 0;
                double.TryParse(dr["StockQty"].ToString(), out douStockQty);
                dgvr.Cells["MoveableQty"].Value = string.Format("{0:n2}", douStockQty);
                if (douStockQty == 0)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox(dgvr.Cells["Barcode"].Value.ToString() + "의 이동가능수량이 0입니다. 이동가능수량이 0인 롯트는 이동처리 할 수 없습니다.", "[이동가능수량 없음]", 2, 1);
                }
            }
        }
        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (grdData.Rows.Count > 0)
            {
                if (grdData.Columns[e.ColumnIndex].Name == "btnDelete")
                {
                    grdData.Rows.RemoveAt(e.RowIndex);
                    CalcRollSum();
                    if (grdData.Rows.Count == 0)
                    {
                        ClearData();
                    }
                    txtBarCode.Select();
                    txtBarCode.Focus();
                }
            }
        }

        private void cboFromLoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFromLoc.Items.Count > 0)
            {
                foreach (DataGridViewRow dgvr in grdData.Rows)
                {
                    GetData(dgvr);
                }
            }
        }
    }
}
