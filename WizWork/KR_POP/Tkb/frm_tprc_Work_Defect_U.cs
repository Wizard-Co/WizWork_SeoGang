using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;   // DllImport
using WizCommon;

namespace WizWork
{
    public partial class frm_tprc_Work_Defect_U : Form
    {

        WizWorkLib Lib = new WizWorkLib();
        List<DefectButton> lstDefectButton = new List<DefectButton>();

        TableLayoutPanel tlpDefectList = new TableLayoutPanel();

        string JobID = "";
        public Dictionary<string, frm_tprc_Work_Defect_U_CodeView> dicDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>();
        // 리턴할 불량 합계 수량
        public string returnTotalQty = "";

        // 불량 수량은 작업수량 밑으로 입력 되도록 하기위해서 받아옴
        double WorkQty = 0;

        // 총 불량 종류 :  Horizontal x Vertical 세팅
        int Horizontal = 5;
        int Vertical = 5;

        public frm_tprc_Work_Defect_U()
        {
            InitializeComponent();
        }

        // 생산용
        public frm_tprc_Work_Defect_U(string JobID, Dictionary<string, frm_tprc_Work_Defect_U_CodeView> dicDefect, double WorkQty)
        {
            InitializeComponent();

            this.JobID = JobID;
            this.dicDefect = dicDefect;
            this.WorkQty = WorkQty;
        }

        private void frm_tprc_Work_Defect_U_Load(object sender, EventArgs e)
        {
            SetLayout(Horizontal, Vertical);
            InitForm();
            LoadBeforeData();
            
        }

        private void InitForm()
        {
            LoadData(Horizontal, Vertical);
            lblNowQty.Text = "";
            return;
        }

        // Horizontal x Vertical 레이아웃 세팅
        private void SetLayout(int Horizontal, int Vertical)
        {

            double widthPercent = 100.0 / Horizontal;
            double heightPercent = 100.0 / Vertical;

            // 가로
            for (int i = 0; i < Horizontal; i++)
            {
                tlpDefectList.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, (float)widthPercent));
            }

            // 세로
            for (int i = 0; i < Vertical; i++)
            {
                tlpDefectList.RowStyles.Add(new RowStyle(SizeType.Percent, (float)heightPercent));
            }

            tlpDefectList.Dock = DockStyle.Fill;
            panelDefect.Controls.Add(tlpDefectList);
        }

        /// <summary>
        /// DB의 부적합원인 리스트 불러오기
        /// </summary>
        /// <param name="Horizontal"></param>
        /// <param name="Vertical"></param>
        private void LoadData(int Horizontal, int Vertical)
        {
            try
            {
                tlpDefectList.Controls.Clear();
                lstDefectButton.Clear();

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_prdWork_sDefectList", null, false);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (i > 24) { break; }

                        DataRow dr = dt.Rows[i];

                        var Defect = new frm_tprc_Work_Defect_U_CodeView()
                        {
                            DefectID = dr["DefectID"].ToString(),
                            DefectName = dr["Display1"].ToString(),
                            DefectClss = dr["DefectClss"].ToString(),
                        };

                        // 불량 버튼 생성
                        DefectButton btnDefect = new DefectButton();

                        //  // 폐기
                        if (Defect.DefectClss.Equals("1")) { btnDefect.BackColor = System.Drawing.Color.MediumSeaGreen; }
                        // 수리
                        else if (Defect.DefectClss.Equals("2")) { btnDefect.BackColor = System.Drawing.Color.DarkOrange; }

                        btnDefect.initDefectButton(Defect.DefectName);
                        btnDefect.Tag = Defect.DefectID;
                        btnDefect.Dock = DockStyle.Fill;
                        btnDefect.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
                        btnDefect.onClick += new EventHandler(this.SelectReasonBtn);

                        // 레이아웃에 추가
                        tlpDefectList.Controls.Add(btnDefect, (i % Horizontal), (i / Horizontal));
                        lstDefectButton.Add(btnDefect);
                    }

                    fillEmptyDefectButtons();
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        #region 불량   fillEmptyDefectButtons

        private void fillEmptyDefectButtons()
        {
            int count = tlpDefectList.Controls.Count;
            int max = Horizontal * Vertical;

            for (int i = count; i < max; i++)
            {
                // 불량 버튼 생성
                DefectButton btnDefect = new DefectButton();

                btnDefect.initDefectButton("");
                btnDefect.Tag = "";
                btnDefect.Dock = DockStyle.Fill;
                btnDefect.Font = new Font("맑은 고딕", 12, FontStyle.Bold);
                btnDefect.setEnabled(false);

                // 레이아웃에 추가
                tlpDefectList.Controls.Add(btnDefect, (i % Horizontal), (i / Horizontal));
            }
        }

        #endregion

        // 이전에 불량 등록해놓은 것이 있다면 불러오기
        private void LoadBeforeData()
        {
            for (int i = 0; i < lstDefectButton.Count; i++)
            {
                if (lstDefectButton[i].Tag != null
                    && dicDefect.ContainsKey(lstDefectButton[i].Tag.ToString()))
                {
                    string Key = lstDefectButton[i].Tag.ToString();

                    lstDefectButton[i].setDefectQty(ConvertDouble(dicDefect[Key].DefectQty));
                }
            }

            CalculateQty();
        }



        // 불량 클릭 이벤트
        public void SelectReasonBtn(object sender, EventArgs e)
        {
            try
            {
                DefectButton defect = sender as DefectButton;
                if (defect != null)
                {
                    var DefectInfo = defect.getDefectInfo() as DefectButton_CodeView;
                    if (DefectInfo != null)
                    {
                        string DefectName = Lib.CheckNull(DefectInfo.DefectName);
                        string DefectQty = Lib.CheckNull(DefectInfo.DefectQty);
                        POPUP.Frm_CMNumericKeypad FK = new POPUP.Frm_CMNumericKeypad(DefectName + " 입력", DefectQty.Replace(",", ""));
                        POPUP.Frm_CMNumericKeypad.g_Name = "";
                        FK.Owner = this;

                        if (FK.ShowDialog() == DialogResult.OK)
                        {
                            defect.setDefectQty(Lib.GetDouble(FK.InputTextValue.Trim()));
                            CalculateQty();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }
    
        // 총 수량 계산하기
        private void CalculateQty()
        {
            double TotalDefectQty = 0;
            for (int k = 0; k < lstDefectButton.Count; k++)
            {
                DefectButton defect = lstDefectButton[k] as DefectButton;

                if (defect != null)
                {
                    var DefectInfo = defect.getDefectInfo() as DefectButton_CodeView;
                    if (DefectInfo != null)
                    {
                        double DefectQty = Lib.GetDouble(Lib.CheckNull(DefectInfo.DefectQty));
                        if (DefectQty > 0)
                        {
                            TotalDefectQty += DefectQty;
                        }
                    }
                }
            }

            lblNowQty.Text = string.Format("{0:N0}", TotalDefectQty);
        }

        // 검사 완료 버튼
        private void cmdOk_Click(object sender, EventArgs e)
        {
            try
            {
                double TotalQty = 0;

                dicDefect = new Dictionary<string, frm_tprc_Work_Defect_U_CodeView>();

                for (int i = 0; i < lstDefectButton.Count; i++)
                {
                    string DefectID = lstDefectButton[i].Tag.ToString();
                    double DefectQty = lstDefectButton[i].getDefectQty();

                    if (DefectQty > 0
                        && dicDefect.ContainsKey(DefectID) == false)
                    {
                        var Defect = new frm_tprc_Work_Defect_U_CodeView()
                        {
                            JobID = this.JobID,
                            DefectID = DefectID,
                            XPos = this.Horizontal.ToString(),
                            YPos = this.Vertical.ToString(),
                            DefectQty = DefectQty.ToString(),
                        };

                        dicDefect.Add(DefectID, Defect);

                        TotalQty += DefectQty;
                    }
                }

                if (TotalQty > WorkQty)
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("불량 수량은 최대 " + Lib.stringFormatN0(WorkQty) + "까지 입력 가능합니다.\r\n불량수량을 확인해주세요.", "[불량 등록 전]", 0, 1);
                    return;
                }

                this.returnTotalQty = stringFormatN0(TotalQty);
                this.DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
            this.Close();
        }

        // 닫기 버튼
        private void cmdQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 초기화 버튼
        private void cmdInit_Click(object sender, EventArgs e)
        {
            InitForm();
        }

        // 불량 초기화
        private void cmdDefect_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DefectButton defectBtn in lstDefectButton)
                {
                    defectBtn.setDefectQty(0);
                }
                lblNowQty.Text = "";
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        #region 기타 메서드

        // 천단위 콤마로 변환
        private string stringFormatN0(object obj)
        {
            if (obj == null) { return ""; }

            return string.Format("{0:N0}", obj);
        }

        // 소수로 변환
        private double ConvertDouble(string str)
        {
            if (str == null) { return 0; }

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

    public class frm_tprc_Work_Defect_U_CodeView
    {
        public string JobID { get; set; }
        public string DefectID { get; set; }
        public string XPos { get; set; }
        public string YPos { get; set; }
        public string DefectQty { get; set; }

        public string DefectName { get; set; }
        public string DefectClss { get; set; }

        public frm_tprc_Work_Defect_U_CodeView Copy()
        {
            return this.MemberwiseClone() as frm_tprc_Work_Defect_U_CodeView;
        }
    }
}
