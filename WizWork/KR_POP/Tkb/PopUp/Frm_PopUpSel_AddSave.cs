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
    public partial class Frm_PopUpSel_AddSave : Form
    {
        string sProcessID = string.Empty;
        string m_ArticleID = "";
        public delegate void TextEventHandler(List<KeyValue> AddSaveResult, int i);                // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;           // 대리자 타입의 이벤트 처리기를 설정합니다.
        private string PartGBNID = "";
        WizWorkLib Lib = new WizWorkLib();
        WizCommon.Popup.Frm_CMNumericKeypad FK = null;
        WizCommon.Popup.Frm_CMKeypad FCK = null;
        List<KeyValue> AddSaveResult = new List<KeyValue>();
        int i = 0;

        public Frm_PopUpSel_AddSave()
        {
            InitializeComponent();
        }

        public Frm_PopUpSel_AddSave(string strProcessID, string strPartGBNID = "", int ii = 0, string ArticleID = "")
        {
            InitializeComponent();
            this.sProcessID = strProcessID;//.Substring(0, 2) + "01";
            if (strProcessID is "0405")//혼련공정일때
            {
                PartGBNID = strPartGBNID;
            }
            i = ii;
            m_ArticleID = ArticleID;
        }

        private void Frm_PopUpSel_AddSave_Load(object sender, EventArgs e)
        {
            InitGridByProcessID(sProcessID);
            SetScreen();
        }
        private void SetScreen()
        {
            tlpForm.Dock = DockStyle.Fill;
            foreach (Control control in tlpForm.Controls)
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
                        foreach (Control cont in contro.Controls)
                        {
                            cont.Dock = DockStyle.Fill;
                            cont.Margin = new Padding(0, 0, 0, 0);
                            foreach (Control con in contro.Controls)
                            {
                                con.Dock = DockStyle.Fill;
                                con.Margin = new Padding(0, 0, 0, 0);
                            }
                        }
                    }
                }
            }
            int ColWidth = 0;
            int ColHeight = 0;
            int RowHeight = 0;
            foreach (DataGridViewColumn dgvc in grdData.Columns)
            {
                ColWidth = ColWidth + dgvc.Width;
                
            }
            RowHeight = grdData.Rows.GetRowsHeight(DataGridViewElementStates.Visible);
            ColHeight = grdData.ColumnHeadersHeight;
            //ColHeight = grdData.Height + tlpOC.Height + label1.Height;
            ColWidth = ColWidth + 10;
            this.Size = new Size(ColWidth, RowHeight + ColHeight + tlpOC.Height);
            this.Text = "추가 데이터 입력";
        }
        /// <summary>
        /// int cellType = 0, cellType = 0 숫자버튼 
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="strHeader"></param>
        private void InitCol(string strName = "", string strHeader = "")
        {
            DataGridViewButtonColumn txtCol = new DataGridViewButtonColumn();
            {
                txtCol.HeaderText = strHeader;
                txtCol.Name = strName;
                if (strHeader.Length == 2)
                {
                    txtCol.Width = 50;
                }
                else if (strHeader.Length == 4)
                {
                    txtCol.Width = 100;
                }
                else
                {
                    //txtCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
                    txtCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                }
                txtCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                txtCol.Visible = true;
            }
            grdData.Columns.Insert(grdData.Columns.Count, txtCol);
        }

        private void InitGridByProcessID(string strProcessID)
        {
            grdData.Columns.Clear(); //체크박스나 콤보박스 사용시 필요하다.
            
            if (strProcessID is "0405")//9
            {// 03 : FMB, 04 : CMB
                if (PartGBNID == "03")//FMB
                {
                    InitCol("RoraTime", "로라 공정별 작업시간");
                }
                else if (PartGBNID == "04")//CMB
                {
                    InitCol("NidaTime", "니다 공정별 작업시간");
                }
                InitCol("EnterWaterTemp", "입수온도");
                InitCol("OutWaterTemp", "출수온도");
                InitCol("ChamberTemp", "챔버온도");
                InitCol("EmitTemp", "방출온도");
                InitCol("DumpingTemp", "덤핑온도");
                InitCol("Width", "폭");
                InitCol("Thickness", "두께");
            }

            else if (strProcessID is "1101")
            {
                InitCol("Length", "길이");
                InitCol("Thickness", "두께");
                InitCol("Weight", "중량");
                InitCol("EarlyWeight", "초품중량");
                InitCol("MiddleWeight", "중품중량");
                InitCol("BoxWeight", "재단의Box중량");
                //InitCol("WorkQty", "Box당수량");
                InitCol("JaturiQty", "자투리발생량");
            }
            else if (strProcessID is "3101")
            {
                InitCol("Temp", "온도");
                //InitCol("Time", "시간"); //앞에서 입력할텐데..?
                //InitCol("WorkQty", "작업수량");//앞에서 입력할듯..
                InitCol("BallSize", "볼크기");
                InitCol("RPM", "RPM");
            }
            else if (strProcessID is "4101")
            {
                InitCol("Temp", "온도");
                //InitCol("Time", "시간"); //앞에서 입력할텐데..?
                InitCol("Comments", "비고");
            }
            DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
            {
                btnCol.HeaderText = "입력";
                btnCol.Name = "입력";
                //btnCol.Width = 120;
                btnCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                //btnCol.CellTemplate.Style.BackColor = Color.Beige;
                btnCol.Visible = true;
            }
            grdData.Columns.Insert(grdData.Columns.Count, btnCol);

            grdData.Font = new Font("맑은 고딕", 14);
            grdData.ColumnHeadersDefaultCellStyle.Font = new Font("맑은 고딕", 12f);
            //grdData.ColumnHeadersHeight = 4;
            grdData.RowTemplate.Height = 40;
            //grdData.ColumnHeadersHeight = 30;
            //grdData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            grdData.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            grdData.ScrollBars = ScrollBars.None;
            grdData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grdData.MultiSelect = false;
            grdData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            foreach (DataGridViewColumn col in grdData.Columns)
            {
                col.DataPropertyName = col.Name;
                col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            grdData.Rows.Add("0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "", "", "", "", "", "");
            grdData.Rows[0].Cells["입력"].Value = "입력";
            

            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add("@ProcessID", sProcessID);
            sqlParameter.Add("@ArticleID", m_ArticleID);
            DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_WizWork_sLastDataByProcessID", sqlParameter, false);
            if (dt.Rows.Count == 1)
            {
                DataRow dr = dt.Rows[0];
                foreach (DataColumn dc in dt.Columns)
                {
                    foreach (DataGridViewColumn dgvc in grdData.Columns)
                    {
                        if (dc.ColumnName == dgvc.Name)
                        {
                            grdData.Rows[0].Cells[dgvc.Name].Value = dr[dc.ColumnName].ToString();
                        }
                        if (sProcessID == "1101" && dc.ColumnName.Contains("1101")) // 재단공정의 경우 길이와 두께를 mt_Article의 길이, 두께를 가져와서 넣어줌.
                        {
                            string[] dcName = null;
                            string[] delimeters = { "1101" };
                            dcName = dc.ColumnName.Split(delimeters, 5, StringSplitOptions.RemoveEmptyEntries);
                            if (dcName.Length == 1)
                            {
                                if (dgvc.Name.Contains(dcName[0]))
                                {
                                    grdData.Rows[0].Cells[dgvc.Name].Value = dr[dc.ColumnName].ToString();
                                }
                            }
                        }
                    }
                    
                }
            }
            //grdData.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
        private bool CheckData()
        {
            try
            {
                string Value = "";
                foreach (DataGridViewCell dgvc in grdData.Rows[0].Cells)
                {
                    Value = dgvc.Value.ToString();
                    if (Value is "")
                    {
                        if (WizCommon.Popup.MyMessageBox.ShowBox(dgvc.OwningColumn.Name + "의 값을 입력하지 않으면 " +
                            "0으로 입력됩니다.계속 진행하시겠습니까 ?", "[종료]", 0, 0) == DialogResult.OK)
                        {
                            dgvc.Value = "0";
                        }
                        else
                        {
                            return false;
                        }
                    }
                    
                }
                return true;
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
                return false;
            }
            
            
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            CheckData();
            try
            {
                KeyValue kv = new KeyValue();
                kv.key = "ProcessID";
                kv.value = sProcessID;
                AddSaveResult.Add(kv);
                foreach (DataGridViewCell dgvc in grdData.Rows[0].Cells)
                {
                    KeyValue kvp = new KeyValue();
                    kvp.key = dgvc.OwningColumn.Name;
                    kvp.value = dgvc.Value.ToString();
                    AddSaveResult.Add(kvp);
                }
                WriteTextEvent(AddSaveResult, i);
                DialogResult = DialogResult.OK;
                this.Dispose();
                this.Close();
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void grdData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int ci = e.ColumnIndex;
            string colName = grdData.Columns[ci].Name;
            string colHeader = grdData.Columns[ci].HeaderText;
            string Value = "";
            if (colName == "입력")
            {
                if (grdData.Rows.Count is 1 && e.RowIndex == 0)
                {
                    foreach (DataGridViewCell dgvc in grdData.Rows[0].Cells)
                    {                        
                        if (dgvc.Value.ToString() == "입력")
                        {
                            return;
                        }
                        
                        Value = dgvc.Value.ToString();
                        colHeader = grdData.Columns[dgvc.ColumnIndex].HeaderText;
                        if (dgvc.OwningColumn.Name != "Comments")
                        {
                            FK = new WizCommon.Popup.Frm_CMNumericKeypad(Value, colHeader);
                            if (FK.ShowDialog() == DialogResult.OK)
                            {
                                double d = 0;
                                d = Lib.GetDouble(FK.tbInputText.Text.Trim());
                                dgvc.Value = string.Format("{0:n0}", d);
                            }
                            //btnKeyShift_Click();
                        }
                        else
                        {
                            if (dgvc.Value.ToString() == "입력")
                            {
                                return;
                            }
                            dgvc.Selected = true;
                            //btnKeyShift_Click();
                            FCK = new WizCommon.Popup.Frm_CMKeypad(Value, colHeader);
                            if (FCK.ShowDialog() == DialogResult.OK)
                            {
                                dgvc.Value = FCK.tbInputText.Text.Trim();
                            }
                        }
                        
                    }
                }
            }
            else
            {
                if (grdData.Rows.Count is 1 && e.RowIndex == 0)
                {
                    colHeader = grdData.Columns[e.ColumnIndex].HeaderText;
                    Value = grdData.Rows[e.RowIndex].Cells[colName].Value.ToString();
                    FK = new WizCommon.Popup.Frm_CMNumericKeypad(Value, colHeader);
                    if (FK.ShowDialog() == DialogResult.OK)
                    {
                        double d = 0;
                        d = Lib.GetDouble(FK.tbInputText.Text.Trim());
                        grdData.Rows[e.RowIndex].Cells[colName].Value = string.Format("{0:n2}", d);
                    }
                }
            }
        }
        private void btnKeyShift_Click()
        {
            System.Diagnostics.Process ps = new System.Diagnostics.Process();
            ps.StartInfo.FileName = "osk.exe";
            ps.Start();
            //@"C:\Windows\System32\osk.exe";
            //@"\windows\SysWOW64\osk.exe";
            //"osk.exe";
            //ps.Start();
            //System.Diagnostics.Process.Start("osk.exe");
            //tbInputText.Focus();
            //tbInputText.ImeMode = ImeMode.HangulFull;
        }
    }
}

