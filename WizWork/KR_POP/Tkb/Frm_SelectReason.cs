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
    public partial class Frm_SelectReason : Form
    {
        // ini 읽기/쓰기 함수
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        // ini 파일 경로
        //string FilePath = Application.StartupPath + "\\INI\\Preference.ini";
        //string FilePath = "C:\\TestWizardT.ini";             //Wizard.ini 파일위치
        string FilePath = ConnectionInfo.filePath;            //Wizard.ini 파일위치

        System.Windows.Forms.RadioButton[] newRadioButton = null;

        private DataSet ds = null;

        UC_ReasonMenuCell MenuCell = null;

        public string StrReason = string.Empty;
        public string StrReasonCode = string.Empty;

        /// <summary>
        /// 폼간 데이터전달을 위한 소스
        /// </summary>
        /// <param name="text"></param>
        /// 
            
        public delegate void TextEventHandler(string[] DefectID, string[] DefectQty, string[] XPos, string[] YPos, int i);    // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler WriteTextEvent;          // 대리자 타입의 이벤트 처리기를 설정합니다. 

        

        // ini의 부적합원인 선택창 칸 수 불러오기
            StringBuilder Horizontal = new StringBuilder();
            StringBuilder Vertical = new StringBuilder();

        /// <summary>
        /// 생성
        /// </summary>
        public Frm_SelectReason()
        {
            InitializeComponent();

            //// ini의 부적합원인 선택창 칸 수 불러오기
            //StringBuilder Horizontal = new StringBuilder();
            //StringBuilder Vertical = new StringBuilder();
            GetPrivateProfileString("REASON", "Horizontal", "(NONE)", Horizontal, 5, FilePath);
            GetPrivateProfileString("REASON", "Vertical", "(NONE)", Vertical, 5, FilePath);

            int intHorizontal = Convert.ToInt16(Horizontal.ToString());
            int intVertical = Convert.ToInt16(Vertical.ToString());

            SetLayout(intHorizontal, intVertical);  // ini 설정값으로 창 변경
            LoadData(intHorizontal, intVertical);   // 데이터 불러오기

            // 부적합원인 선택창 설정컨트롤 설정 
            //MenuCell = new UC_ReasonMenuCell(this);
            //tlpReason.Controls.Add(MenuCell.tlpChildTlp, intHorizontal - 1, intVertical - 1);
            //MenuCell.cbbCountX.SelectedItem = intHorizontal.ToString();
            //MenuCell.cbbCountY.SelectedItem = intVertical.ToString();

            //MenuCell.Visible = false; // 크기 고정(7,7)을 위해 필요하다. 사용자가 변경하지 못하도록 하기위함도 있음.
        }


        
        /// <summary>
        /// INI의 설정값으로 칸 나눔
        /// </summary>
        /// <param name="Horizontal"></param>
        /// <param name="Vertical"></param>
        void SetLayout(int Horizontal, int Vertical)
        {
            while (tlpReason.ColumnCount != Horizontal)
            {
                if (tlpReason.ColumnCount > Horizontal)
                {
                    tlpReason.ColumnCount--;
                    tlpReason.ColumnStyles.RemoveAt(tlpReason.ColumnCount);
                }
                else if (tlpReason.ColumnCount < Horizontal)
                {
                    tlpReason.ColumnCount++;
                    tlpReason.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
                }
            }
            foreach (ColumnStyle style in tlpReason.ColumnStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Width = 100.0f / tlpReason.ColumnCount;
            }

            while (tlpReason.RowCount != Vertical)
            {
                if (tlpReason.RowCount > Vertical)
                {
                    tlpReason.RowCount--;
                    tlpReason.RowStyles.RemoveAt(tlpReason.RowCount);
                }
                else if (tlpReason.RowCount < Vertical)
                {
                    tlpReason.RowCount++;
                    tlpReason.RowStyles.Add(new RowStyle(SizeType.Percent, 10f));
                }
            }
            foreach (RowStyle style in tlpReason.RowStyles)
            {
                style.SizeType = SizeType.Percent;
                style.Height = 100.0f / tlpReason.RowCount;
            }
        }

        /// <summary>
        /// DB의 부적합원인 리스트 불러오기
        /// </summary>
        /// <param name="Horizontal"></param>
        /// <param name="Vertical"></param>
        void LoadData(int Horizontal, int Vertical)
        {
            //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            //sqlParameter.Add(com_InspectDefectReason.REASONCODE, "");
            //sqlParameter.Add(com_InspectDefectReason.REASON, "");
            //sqlParameter.Add(com_InspectDefectReason.USEYN, "Y");           // 사용
            //sqlParameter.Add(com_InspectDefectReason.DEFECTPOSITION, "5");  // 최종검사
            //sqlParameter.Add(com_InspectDefectReason.DEFECTITEM, "");

            ds = DataStore.Instance.ProcedureToDataSet("xp_WizIns_sDefectSub", null/*sqlParameter*/, false);

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.newRadioButton = new RadioButton[ds.Tables[0].Rows.Count];
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (i == Horizontal * Vertical - 1)
                    {
                        break;
                    }


                    RadioButton newRadioButton = new RadioButton();

                    //Label newLabel = new Label();

                    //tlpReason.Controls.Add(newRadioButton, (i / Horizontal), (i % Horizontal));

                    tlpReason.Controls.Add(newRadioButton, (i % Horizontal), (i / Horizontal));
                    
                    //tlpReason.Controls.Add(newLabel, (i / Horizontal), (i % Horizontal));

                    //tlpReason.SetColumnSpan(newRadioButton, (i / Horizontal));
                    
                    
                    

                    DataRow dr = ds.Tables[0].Rows[i];

                    //string ReasonName = dr[com_DefectReason.REASONNAME].ToString();
                    //string ReasonCode = dr[com_DefectReason.REASONCODE].ToString();
                    //sqlParameter = new Dictionary<string, object>();
                    //sqlParameter.Add(com_InspectDefectReason.LCODE, ReasonCode.Substring(0, 1));
                    //sqlParameter.Add(com_InspectDefectReason.MCODE, ReasonCode.Substring(1, 2));
                    //sqlParameter.Add(com_InspectDefectReason.SCODE, ReasonCode.Substring(3, 2));
                    //sqlParameter.Add(com_InspectDefectReason.CODEGBN, "S");
                    //sqlParameter.Add(com_InspectDefectReason.USEYN, "Y");
                    if (dr[WizIns_sDefectSub.DEFECTCLSS].ToString() == "1")
                    {
                        newRadioButton.BackColor = System.Drawing.Color.MediumSeaGreen;
                    }
                    else if(dr[WizIns_sDefectSub.DEFECTCLSS].ToString() == "2")
                    {
                        newRadioButton.BackColor = System.Drawing.Color.DarkOrange;
                    }
                    //DataSet dsSub = DataStore.Instance.ProcedureToDataSet("xp_com_DefectReasonlvl_s", sqlParameter, false);
                    //DataRow drSub = dsSub.Tables[0].Rows[0];
                    newRadioButton.Appearance = System.Windows.Forms.Appearance.Button;
                    newRadioButton.Text = dr[WizIns_sDefectSub.DISPLAY12].ToString();/*drSub[com_DefectReason.SCODENAME] + "\n" + */
                    newRadioButton.Tag = dr[WizIns_sDefectSub.DEFECTID].ToString();//ReasonCode;
                    newRadioButton.Dock = DockStyle.Fill;
                    //newRadioButton.Dock = DockStyle.None;
                    newRadioButton.Font = new Font("맑은 고딕", 12, FontStyle.Bold);

                    //newLabel.Text = "클릭해서 수량을 입력해주세요.";
                    //newLabel.Dock = DockStyle.Bottom;
                    //newLabel.Font = new Font("맑은 고딕", 14, FontStyle.Bold);
                    //newLabel.BringToFront();


                    //newLabel.Click += new System.EventHandler(this.SelectReasonBtn);

                    newRadioButton.Click += new System.EventHandler(this.SelectReasonBtn);


                    this.newRadioButton[i] = newRadioButton;
                }                
            }
        }

        ///// <summary>
        ///// 부적합원인 버튼 선택//기존의 메소드
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void SelectReasonBtn(object sender, EventArgs e)
        //{
            //StrReason = ((Button)sender).Text;              // 버튼의 텍스트(부적합원인명) 저장
        //    StrReasonCode = ((Button)sender).Tag.ToString();// 버튼의 태그에 저장된 부적합원인코드 저장
        //    DialogResult = DialogResult.OK;
        //    this.Close();
        //}

        private void SelectReasonBtn(object sender, EventArgs e)
        {
            //StrReason = ((Button)sender).Text;             // 버튼의 텍스트(부적합원인명) 저장
            //StrReasonCode = ((Button)sender).Tag.ToString();// 버튼의 태그에 저장된 부적합원인코드 저장
            //DialogResult = DialogResult.OK;
            //this.Close();
            //string g_Number = "불량수량";
            POPUP.Frm_CMNumericKeypad FK = new POPUP.Frm_CMNumericKeypad();
            POPUP.Frm_CMNumericKeypad.g_Name = "불량수량";
            if (FK.ShowDialog() == DialogResult.OK)
            {
                FK.tbInputText.SelectAll();
                if (FK.tbInputText.Text.Trim().Length == 0)//입력받은 문자가 공백이거나 없을때
                { FK.tbInputText.Text = "0"; }//0을 넣어라.
                if (((RadioButton)sender).Text.Contains("\r\n") == false)
                {
                    //((Button)sender).Tag = FK.tbInputText.Text.ToString();//키패드 숫자 입력
                    ((RadioButton)sender).Text = ((RadioButton)sender).Text + "\r\n" + FK.tbInputText.Text.ToString();
                }
                else
                {
                    //((Button)sender).Tag = string.Empty;//버튼 Tag속성 초기화
                    //((Button)sender).Tag = FK.tbInputText.Text.ToString();//키패드 숫자 입력
                    //((Button)sender).Text = string.Empty;//버튼 Text속성 초기화
                    ((RadioButton)sender).Text = ((RadioButton)sender).Text.Substring(0, ((RadioButton)sender).Text.IndexOf("\r\n") + 2) + FK.tbInputText.Text.ToString();
                    //((Button)sender).Text = ((Button)sender).Text + "\r\n" + ((Button)sender).Tag.ToString();
                }

            }
        }


        /// <summary>
        /// 부적합원인 창의 칸 설정 적용버튼 클릭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnCellSetting_Click(object sender, EventArgs e)
        {
            int Horizontal = Convert.ToInt16(MenuCell.cbbCountX.Text);
            int Vertical = Convert.ToInt16(MenuCell.cbbCountY.Text);

            tlpReason.Controls.Clear();
            SetLayout(Horizontal, Vertical);
            tlpReason.Controls.Add(MenuCell.tlpChildTlp, Horizontal - 1, Vertical - 1);
            LoadData(Horizontal, Vertical);

            WritePrivateProfileString("REASON", "Horizontal", tlpReason.ColumnCount.ToString(), FilePath);
            WritePrivateProfileString("REASON", "Vertical", tlpReason.RowCount.ToString(), FilePath);
        }

        /// <summary>
        /// 나가기 버튼 클릭(UC_Qlt_ReasonMenuCell)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_SelectReason_Load(object sender, EventArgs e)
        {

        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            for(int i=0;i<ds.Tables[0].Rows.Count;i++)
            {
                if (newRadioButton[i].Text.Contains("\r\n") == true)
                {
                    newRadioButton[i].Text = newRadioButton[i].Text.Substring(0, newRadioButton[i].Text.IndexOf("\r\n"));
                }
            }
            
        }

        private void cmdsetProcess_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (newRadioButton[i].Text.Contains("\r\n") == true && newRadioButton[i].Checked == true)
                {
                    newRadioButton[i].Text = newRadioButton[i].Text.Substring(0, newRadioButton[i].Text.IndexOf("\r\n"));
                }
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            int a = 0;
            string[] DefectID = new string[ds.Tables[0].Rows.Count];
            string[] DefectQty = new string[ds.Tables[0].Rows.Count];
            string[] XPos = new string[ds.Tables[0].Rows.Count];
            string[] YPos = new string[ds.Tables[0].Rows.Count];

            
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {

                if (newRadioButton[i].Text.Contains("\r\n") == true)
                {
                    //newRadioButton[i].Tag
                    //SetData();
                    
                    DefectID[i] = newRadioButton[i].Tag.ToString();
                    //Console.WriteLine(newRadioButton[i].Text.Length);
                    //Console.WriteLine(newRadioButton[i].Text.Substring(0, newRadioButton[i].Text.IndexOf("\r\n")) + "@@@@@@@@@@");
                    //Console.WriteLine(newRadioButton[i].Text.Length);
                    //Console.WriteLine(newRadioButton[i].Text.Substring(5));

                    DefectQty[i] = newRadioButton[i].Text.Substring(newRadioButton[i].Text.IndexOf("\r\n") + 2);//+1, newRadioButton[i].Text.Length);
                    XPos[i] = Horizontal.ToString();
                    YPos[i] = Vertical.ToString();
                    //WriteTextEvent(DefectID, DefectQty, XPos, YPos, ds.Tables[0].Rows.Count);
                }
                else//입력 값이 없을때 NullException 방지차원에서 불량수량을 0으로 넘겨준다.
                {
                    DefectID[i] = newRadioButton[i].Tag.ToString();
                    DefectQty[i] = "0";
                    XPos[i] = Horizontal.ToString();
                    YPos[i] = Vertical.ToString();
                }
                if(Convert.ToDouble(DefectQty[i])>0)
                {
                    a = a + 1;
                }
            }

            string[] sDefectID = new string[a];
            string[] sDefectQty = new string[a];
            string[] sXPos = new string[ds.Tables[0].Rows.Count];
            string[] sYPos = new string[ds.Tables[0].Rows.Count];

            int k = 0;

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (Convert.ToDouble(DefectQty[i]) > 0)
                {
                        sDefectID[k] = DefectID[i].ToString();
                        sDefectQty[k] = DefectQty[i].ToString();
                        sXPos[k] = XPos[i].ToString();
                        sYPos[k] = YPos[i].ToString();
                        k = k + 1;
                        if (k == a) { break; }
                }
            }
            
            //WriteTextEvent(DefectID, DefectQty, XPos, YPos, ds.Tables[0].Rows.Count);
            WriteTextEvent(sDefectID, sDefectQty, sXPos, sYPos, a);
            this.Close();
        }

        //void SetData()
        //{
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        DefectID[i] = newRadioButton[i].Tag.ToString();
        //        DefectQty[i] = newRadioButton[i].Text.Substring(newRadioButton[i].Text.IndexOf("\r\n"), newRadioButton[i].Text.Length);
        //        XPos[i] = Horizontal.ToString();
        //        YPos[i] = Vertical.ToString();
        //        WriteTextEvent(DefectID, DefectQty, XPos, YPos, i);
        //    }
        //}
    }
}
