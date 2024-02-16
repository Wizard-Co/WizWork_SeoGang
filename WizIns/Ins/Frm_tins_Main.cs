using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using WizWork.Properties;
using Microsoft.Win32;
using WizCommon;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WizWork.SamJoo;
using WizWork.Popup;
using WizWork;

namespace WizIns
{
    public partial class Frm_tins_Main : Form
    {
        private int childFormNumber = 0;
        //전역 변수 선언
        string[] Message = new string[2];
        Button btn = null;//상단 조작부 버튼 클릭 시 사용할 변수
        int i = 0;//상단버튼 switch문용 정수
        WizWork.POPUP.Frm_CMNumericKeypad keypad = null;
        bool blOpen = false;
        //
        public static WizWorkLib Lib = new WizWorkLib();
        public static INI_GS gs = new INI_GS();
  
        //public static TagPrint tagPrint = new TagPrint();
        //public static GlobalVar gv = new GlobalVar();
        //정적 클래스 선언
        public static Frm_tins_Main_CodeView g_tBase = new Frm_tins_Main_CodeView();

        [DllImport("user32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string IPClassName, String IpWindowName);

        [DllImport("User32", EntryPoint = "SetForegroundWindow")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        //user32.dll 을 임포트 한다
        [DllImport("user32.dll", SetLastError = true)]
        //MoveWindow 함수를 호출한다.
        internal static extern bool MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        Form form = null;

        public Frm_tins_Main()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "창 " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "텍스트 파일 (*.txt)|*.txt|모든 파일 (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "텍스트 파일 (*.txt)|*.txt|모든 파일 (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void btnControl_Click(object sender, EventArgs e)
        {
            try
            {
                i = 0;//case문에 사용할 정수 초기화
                form = null;//폼 초기화
                blOpen = false;//폼 활성화 여부
                btn = sender as Button;
                Text = "검사/포장 시스템 - " + btn.Text.Trim();
                int.TryParse(btn.Tag.ToString(), out i);
                switch (i)
                {
                    case 0://공지사항
                        Frm_Info child0 = new Frm_Info();
                        child0.setLblName("검사/포장 시스템 - WizIns");
                        form = child0;
                        break;
                    case 1://미검사실적조회 : Frm_tins_NotInspect
                        Frm_tins_NotInspect child1 = new Frm_tins_NotInspect();
                        this.stsInfo_ProMac.Text = child1.Name.ToString();
                        form = child1;
                        break;                    
                    case 2://전수검사
                        Frm_tins_Order_Q child2 = new Frm_tins_Order_Q();
                        this.stsInfo_ProMac.Text = child2.Name.ToString();
                        form = child2;
                        break;
                    case 3://검사실적조회
                        Frm_tins_Result_Q child3 = new Frm_tins_Result_Q();
                        this.stsInfo_ProMac.Text = child3.Name.ToString();
                        form = child3;
                        break;

                    case 4://작업자 선택
                        Frm_PopUp_setPerson child4 = new Frm_PopUp_setPerson();
                        child4.Owner = this;
                        if (child4.ShowDialog() == DialogResult.OK)
                        {
                            
                        }
                        break;
                
                    case 9://Exit
                        Close();
                        SaveRegistry();
                        break;
                }
                if (i > 7 && i < 13 || i == 14)
                {
                    Text = Text + " 조회";
                }
                if (form != null)
                {
                    foreach (Form openForm in Application.OpenForms)//중복실행방지
                    {
                        if (openForm.Name == form.Name)
                        {
                            blOpen = true;
                            openForm.BringToFront();
                            openForm.Activate();
                            return;
                        }
                    }
                    form.MdiParent = this;
                    form.TopLevel = false;
                    form.Dock = DockStyle.Fill;

                    if (!blOpen)
                    {
                        form.BringToFront();
                        form.Show();
                    }
                }
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }
        }

        // 2020.07.14 자주검사만 여는 메서드
        public static void OpenInspectAuto(string label)
        {
            bool blOpen = false;

            frm_tins_InspectAuto_U child3 = new frm_tins_InspectAuto_U();
            Form form = child3;

            foreach (Form openForm in Application.OpenForms)//중복실행방지
            {
                if (openForm.Name == form.Name)
                {
                    openForm.Close();
                    break;
                }
            }
            form.MdiParent = Frm_tins_Main.ActiveForm;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;

            if (!blOpen)
            {
                form.BringToFront();
                form.Show();
            }

            child3.SetPlanInputLabel(label);
        }

        private void MDIParent1_Load(object sender, EventArgs e)
        {
            //ExecuteVB(); >> 시작할때 애가 FORM.FRONT 처리되서 거슬린데.            
            timer_Clock.Start();
            timer_Clock.Interval = 1000;//1초
            SetScreen();
            LoadRegistry();

            btnControl_Click(btnInfo, null);
        }
        private void ExecuteVB()
        {
            Process ps = new Process();//실행중인 프로세스가 없을때 WizWork.exe
            ps.StartInfo.FileName = Application.StartupPath.ToString() + "\\WizMes_Work2.exe";
            ps.Start();
        }

        private void VBProcessKill()
        {
            Process[] processes = Process.GetProcessesByName("WizMes_Work2");
            Process currentProcess = Process.GetCurrentProcess();

            foreach (Process proc in processes)
            {
                if (proc.Id != currentProcess.Id)
                    proc.Kill();
            }

        }

        private void SettingClick()
        {
            keypad = new WizWork.POPUP.Frm_CMNumericKeypad("비밀번호", "");
            if (keypad.ShowDialog() == DialogResult.OK)
            {
                if (keypad.tbInputText.Text.Trim() == "0000")
                {
                    FrmSetting form_set = new FrmSetting();
                    form_set.ShowDialog();
                }
                else
                {
                    WizCommon.Popup.MyMessageBox.ShowBox("비밀번호가 일치하지 않습니다", "[잘못된 비밀번호]", 3, 1);
                }
            }
            keypad = null;
        }

        private void Frm_tins_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer_Clock.Stop();
            SaveRegistry();
            Message[0] = "[WizWork - 생산프로그램 종료]";
            Message[1] = "생산프로그램을 종료합니다. 계속하시겠습니까?";

            if (WizCommon.Popup.MyMessageBox.ShowBox(Message[1], Message[0], 0, 0) == DialogResult.OK)//NO
            {
                Dispose();
                Close();
                //System.Diagnostics.Process.Start("ShutDown", "-s"); //shutdown 종료
                //System.Diagnostics.Process.Start("ShutDown", "-r"); //restart 재시작
            }
            else
            {
                timer_Clock.Start();
                timer_Clock.Interval = 1000;//1초
                e.Cancel = true;
                return;
            }
        }

        //protected override void OnInitialized(EventArgs e)
        //{
        //    //프로세스로 메모장을 열고 실행 한다
        //    Process proc = new Process();
        //    proc.StartInfo.FileName = "WizMes_Work2.exe";
        //    proc.Start();

        //    //잠시 정지후에..
        //    Thread.Sleep(10);

        //    //위에서 실행시킨 프로세스의 윈도우 핸들을 얻는다.
        //    IntPtr id = proc.MainWindowHandle;
        //    //함수호출!
        //    MoveWindow(proc.MainWindowHandle, 2000, 0, 500, 500, true);
        //    //base.OnInitialized(e);
        //}

        public void LoadRegistry()
        {
            try
            {
                //// 2020.09.18 주석
                //g_tBase.sInstID = gs.GetValue("Work", "SetInstID", "");         //' 작업지시번호
                //g_tBase.sPLotID = gs.GetValue("Work", "SetLOTID", "");          //' 지시LOTID번호
                //g_tBase.ProcessID = gs.GetValue("Work", "SetProcessID", "");    //' 공정 ID   
                //g_tBase.Process = gs.GetValue("Work", "SetProcess", "");        //' 공정명   
                //g_tBase.MachineID = gs.GetValue("Work", "SetMachineID", "");    //. 설비 ID
                //g_tBase.Machine = gs.GetValue("Work", "SetMachine", "");        //. 설비명
                g_tBase.TeamID = gs.GetValue("Work", "SetTeamID", "");          //' 작업조 코드
                g_tBase.Team = gs.GetValue("Work", "SetTeam", "");              //' 작업조
                g_tBase.PersonID = gs.GetValue("Work", "SetPersonID", "");      //' 작업자 코드
                g_tBase.Name = gs.GetValue("Work", "SetPerson", "");          //' 작업자명
                //g_tBase.sMoldID = gs.GetValue("Work", "SetMoldID", "");         //' 금형명
                //g_tBase.sMold = gs.GetValue("Work", "SetMold", "");             //' 금형ID
                stsInfo_Msg.Text = "";
                Set_stsInfo();


                ////LocalMachine\\Software\\WizWork    registry경로
                //string regSubkey = "Software\\WizWork";// 서브키를 얻어온다. 없으면 null
                //RegistryKey rk = Registry.LocalMachine.OpenSubKey(regSubkey, true);
                //if (rk != null)// 있으면 서브키의 값을 불러온다.
                //{
                //    //rk = Registry.CurrentUser.CreateSubKey(regSubkey);// 해당이름으로 서브키 생성
                //    g_tBase.sInstID = rk.GetValue("InstID", "").ToString();            //' 작업지시번호
                //    g_tBase.sPLotID = rk.GetValue("PLotID", "").ToString();
                //    //g_tBase.OrderNO = rk.GetValue("OrderNO", "").ToString();
                //    //g_tBase.Custom = rk.GetValue("Custom", "").ToString();
                //    //'---------------------------------
                //    g_tBase.ProcessID = rk.GetValue("ProcessID", "").ToString();
                //    g_tBase.Process = rk.GetValue("Process", "").ToString();
                //    //'--------------------------------------
                //    g_tBase.MachineID = rk.GetValue("MachineID", "").ToString();
                //    g_tBase.Machine = rk.GetValue("Machine", "").ToString();
                //    //'-----------------------------------------------------------
                //    g_tBase.TeamID = rk.GetValue("TeamID", "").ToString();
                //    g_tBase.Team = rk.GetValue("Team", "").ToString();
                //    g_tBase.PersonID = rk.GetValue("PersonID", "0000").ToString();
                //    g_tBase.Person = rk.GetValue("Person", "").ToString();

                //    g_tBase.sMold = rk.GetValue("Mold", "").ToString();                 //' 금형명
                //    g_tBase.sMoldID = rk.GetValue("MoldID", "").ToString();             //' 금형ID
                //}
            }
            catch (Exception excpt)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message), "[오류]", 0, 1);
            }

        }

        private void SaveRegistry()
        {
            // 2020.09.18 주석
            ////AFT의 경우 VB와 C#이 혼재 되어있는데, VB의 경우 특정경로에 레지스트리 값을 저장하지 못하므로
            ////레지스트리 대신에 ini에 값들을 저장하고 불러온다.
            //gs.SetValue("Work", "SetInstID", g_tBase.sInstID, ConnectionInfo.filePath);         //' 작업지시번호
            //gs.SetValue("Work", "SetLOTID", g_tBase.sPLotID, ConnectionInfo.filePath);          //' 지시LOTID번호
            //gs.SetValue("Work", "SetProcessID", g_tBase.ProcessID, ConnectionInfo.filePath);    //' 공정 ID   
            //gs.SetValue("Work", "SetProcess", g_tBase.Process, ConnectionInfo.filePath);        //' 공정명   
            //gs.SetValue("Work", "SetMachineID", g_tBase.MachineID, ConnectionInfo.filePath);    //. 설비 ID
            //gs.SetValue("Work", "SetMachine", g_tBase.Machine, ConnectionInfo.filePath);        //. 설비명
            gs.SetValue("Work", "SetTeamID", g_tBase.TeamID, ConnectionInfo.filePath);          //' 작업조 코드
            gs.SetValue("Work", "SetTeam", g_tBase.Team, ConnectionInfo.filePath);              //' 작업조
            gs.SetValue("Work", "SetPersonID", g_tBase.PersonID, ConnectionInfo.filePath);      //' 작업자 코드
            gs.SetValue("Work", "SetPerson", g_tBase.Name, ConnectionInfo.filePath);          //' 작업자명
            gs.SetValue("Work", "SetDayOrNight", g_tBase.DayOrNightID, ConnectionInfo.filePath);    // 주 / 야 구별자

            //gs.SetValue("Work", "SetMoldID", g_tBase.sMoldID, ConnectionInfo.filePath);         //' 금형명
            //gs.SetValue("Work", "SetMold", g_tBase.sMold, ConnectionInfo.filePath);             //' 금형ID
        }

        public void Set_stsInfo()
        {
            stsInfo_Team.Text = g_tBase.Team;
            stsInfo_Team.Tag = g_tBase.TeamID;
            stsInfo_Person.Tag = g_tBase.PersonID;
            //stsInfo_Person.Text = g_tBase.Name;
            //stsInfo_ProMac.Tag = g_tBase.MachineID;
            //stsInfo_Mold.Text = g_tBase.sMold;
            //stsInfo_Mold.Tag = g_tBase.sMoldID;

            SaveRegistry();
        }

        private void SetScreen()
        {
            tlpTop.Dock = DockStyle.Top;
            foreach (Control control in tlpTop.Controls)
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
                    }
                }
            }
        }

        private void timer_Clock_Tick(object sender, EventArgs e)
        {
            stsInfo_Time.Text = DateTime.Now.ToLongTimeString();

            DateTime today = DateTime.Today;

            DateTime midNight = today.AddDays(1).AddSeconds(-1);
            DateTime mid = today.AddDays(1).AddSeconds(-3);

            // 매일저녁 11:59:57초에서 11:59:59초 사이에 프로그램을 종료하고 재시작 하는 배치프로그램 가동.
            // 자동 리스타트. (허윤구)
            if (mid < DateTime.Now && midNight > DateTime.Now)
            {
                //string batchContent = "/c \"@ECHO OFF & timeout /t 6 > nul & start \"\" \"$[APPPATH]$\" & exit\"";
                //batchContent = batchContent.Replace("$[APPPATH]$", Application.ExecutablePath);
                //Process.Start("cmd", batchContent);
                //Environment.Exit(0);
            }
        }

        private void Frm_tins_Main_Activated(object sender, EventArgs e)
        {
            LoadRegistry();
            WorkInspectionCheck();
        }

        private void WorkInspectionCheck()
        {
            string strProcessID = gs.GetValue("Work", "ProcessID", "ProcessID");
        }

        
    }

    public class Frm_tins_Main_CodeView
    {
        public string PersonID { get; set; }
        public string Name { get; set; }
        public string DayOrNightID { get; set; }
        public string DayOrNight { get; set; }
        public string TeamID { get; set; }
        public string Team { get; set; }
    }
}
