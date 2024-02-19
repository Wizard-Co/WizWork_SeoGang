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
    public partial class Frm_InspectionLegend : Form
    {

        private DataSet ds = null;

        //System.Windows.Forms.RadioButton[] newRadioButton = null;
        System.Windows.Forms.Button[] newButton = null;

        
        //string InspectionFigure = string.Empty;

        public string DefectYN = string.Empty;
        public string Value = string.Empty;
        public bool CheckedAll = false;


        POPUP.Frm_CMKeypad keypad = new POPUP.Frm_CMKeypad();
        POPUP.Frm_CMNumericKeypad numkeypad = new POPUP.Frm_CMNumericKeypad();

        int sLegendRow = 0; //DailMachineCheck 폼에서 가져온 범례 그리드 로우 행의 갯수
        public int sCurrentRow = 0;//DailMachineCheck 폼의 범례 그리드의 현재 행의 위치

        public delegate void TextEventHandler(int a, string InspectionLegendName, string InspectionLegendID);    // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;          // 대리자 타입의 이벤트 처리기를 설정합니다. 
        
        

        /// <summary>
        /// 생성
        /// </summary>
        /// <param name="InsType">검사타입(정성/정량)</param>
        public Frm_InspectionLegend(int LegendRow, int CurrentRow)
        {
            InitializeComponent();
            sLegendRow = LegendRow;
            sCurrentRow = CurrentRow;
            LoadData(5, 4);
            //InspectType = InsType;
        }

        /// <summary>
        /// 취소버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 적합버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDefectN_Click(object sender, EventArgs e)
        {
            DefectYN = "N";
            DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 부적합버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDefectY_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        void LoadData(int Horizontal, int Vertical)
        {
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add(Code_sCmCode.CODEGBN, "MCLEGEND");
            sqlParameter.Add(Code_sCmCode.SRELATION, "");
            
            ds = DataStore.Instance.ProcedureToDataSet("xp_Code_sCmCode", sqlParameter, false);

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.newButton = new Button[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == Horizontal * Vertical - 1)
                    {
                        break;
                    }

                    Button newButton = new Button();
                    tlpReason.Controls.Add(newButton, (i % Horizontal), (i / Horizontal));
                    DataRow dr = ds.Tables[0].Rows[i];

                    newButton.Text = dr[Code_sCmCode.CODE_NAME].ToString();
                    newButton.Tag = dr[Code_sCmCode.CODE_ID].ToString();//ReasonCode;
                    newButton.Dock = DockStyle.Fill;
                    newButton.FlatStyle = FlatStyle.Popup;
                    //newButton.BackColor = Color.Black;
                    newButton.Font = new Font("맑은 고딕", 14, FontStyle.Bold);
                    newButton.ForeColor = Color.Black;
                    newButton.Click += new System.EventHandler(this.SelectReasonBtn);
                    this.newButton[i] = newButton;
                }
            }
        }
        private void SelectReasonBtn(object sender, EventArgs e)
        {
            if (sLegendRow >= sCurrentRow)
            {
                WriteTextEvent(sCurrentRow, ((Button)sender).Text.ToString(), ((Button)sender).Tag.ToString());
                sCurrentRow = sCurrentRow + 1;
                if (sLegendRow == sCurrentRow)
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}
