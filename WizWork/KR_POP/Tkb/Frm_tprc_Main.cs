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

namespace WizWork
{
    public partial class Frm_tprc_Main : Form
    {
        private int childFormNumber = 0;
        //전역 변수 선언
        string[] Message = new string[2];
        Button btn = null;//상단 조작부 버튼 클릭 시 사용할 변수
        int i = 0;//상단버튼 switch문용 정수
        POPUP.Frm_CMNumericKeypad keypad = null;
        bool blOpen = false;
        //
        public static WizWorkLib Lib = new WizWorkLib();
        public static INI_GS gs = new INI_GS();
        //정적 클래스 선언
        public static TMold g_tMold = new TMold(); //금형 선택 정보
        public static List<TMold> list_tMold = new List<TMold>();
        public static TBaseSpec g_tBase = new TBaseSpec(); //기본 정보
        public static List<string> lstStartLabel = new List<string>();
        public static TWkResultDefect g_tInsSub = new TWkResultDefect();
        public static List<TWkResultDefect> list_g_tInsSub = new List<TWkResultDefect>();
        public static List<TSplit> list_g_tsplit = new List<TSplit>();

        public static TOUTWARE ow = new TOUTWARE();
        public static List<TOUTWARESUB> list_owsub = new List<TOUTWARESUB>();
        public static TTerminalSet g_tSet = new TTerminalSet(); //검사기준 정보
        public static List<Mold> list_g_tMold = new List<Mold>();// 추후에 수정해야한다. 레지스트리에 등록되어있는 Mold정보를 가지고 클래스배열의 갯수를 정해야한다.
        public static Sub_TWkLabelPrint Sub_TWkLabelPrint = new Sub_TWkLabelPrint();
        public static Sub_TtdChange Sub_Ttd = new Sub_TtdChange();
        public static TTag Sub_m_tTag = new TTag();
        public static TTagSub Sub_m_tItem = new TTagSub();
        public static List<TTagSub> list_m_tItem = new List<TTagSub>();
        public static List<Sub_TWkResultArticleChild> list_TWkResultArticleChild = new List<Sub_TWkResultArticleChild>();
        public static List<Sub_TWkResult> list_TWkResult = new List<Sub_TWkResult>();
        public static List<Sub_TWkLabelPrint> list_TWkLabelPrint = new List<Sub_TWkLabelPrint>();
        public static Sub_TWkResult Sub_TWkResult = new Sub_TWkResult();
        public static Sub_TWkResultArticleChild Sub_TWkResultArticleChild = new Sub_TWkResultArticleChild();
        //public static TagPrint tagPrint = new TagPrint();
        public static GlobalVar gv = new GlobalVar();
        //정적 클래스 선언
        [DllImport("user32", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string IPClassName, String IpWindowName);

        [DllImport("User32", EntryPoint = "SetForegroundWindow")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        //user32.dll 을 임포트 한다
        [DllImport("user32.dll", SetLastError = true)]
        //MoveWindow 함수를 호출한다.
        internal static extern bool MoveWindow(IntPtr hwnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        Form form = null;

        public static Form mainForm;

        public Frm_tprc_Main()
        {
            InitializeComponent();

            mainForm = this;
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
                Text = "생산정보시스템 - " + btn.Text.Trim();
                int.TryParse(btn.Tag.ToString(), out i);
                switch (i)
                {
                    case 0://공지사항
                        Frm_Info child0 = new Frm_Info();
                        form = child0;
                        break;
                    case 1://설비점검
                        Frm_tprc_DailMachineCheck child1 = new Frm_tprc_DailMachineCheck();
                        child1.mainForm = this;
                        form = child1;
                        break;                    
                    //case 2://자주검사
                    //    frm_tins_InspectAuto_U child3 = new frm_tins_InspectAuto_U();
                    //    child3.mainForm = this;
                    //    form = child3;
                        break;
                    case 3://공정작업
                        string[] sProcessID = Frm_tprc_Main.gs.GetValue("Work", "ProcessID", "ProcessID").Split('|'); // ProcessID
                        string[] sMachineID = Frm_tprc_Main.gs.GetValue("Work", "Machine", "Machine").Split('|');//배열에 설비아이디 넣기 Machine

                        if ((sProcessID.Length == 1
                            && sProcessID[0].Equals("ProcessID"))
                            ||
                            (sMachineID.Length == 1
                            && sMachineID[0].Equals("Machine")))
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox("환경설정에 공정과 호기를 선택해주세요.", "[환경설정 오류]", 0, 1);
                            return;
                        }

                        Frm_tprc_PlanInputMolded_Q child5 = new Frm_tprc_PlanInputMolded_Q();
                        form = child5;

                        //frm_tprc_Work_U child5 = new frm_tprc_Work_U();
                        //form = child5;
                        break;

                    case 4://무작업입력
                        string[] sProcessID2 = Frm_tprc_Main.gs.GetValue("Work", "ProcessID", "ProcessID").Split('|'); // ProcessID
                        string[] sMachineID2 = Frm_tprc_Main.gs.GetValue("Work", "Machine", "Machine").Split('|');//배열에 설비아이디 넣기 Machine
                        if ((sProcessID2.Length == 1
                           && sProcessID2[0].Equals("ProcessID"))
                           ||
                           (sMachineID2.Length == 1
                           && sMachineID2[0].Equals("Machine")))
                        {
                            WizCommon.Popup.MyMessageBox.ShowBox("환경설정에 공정과 호기를 선택해주세요.", "[환경설정 오류]", 0, 1);
                            return;
                        }

                        frm_tprc_setProcess child6 = new frm_tprc_setProcess(true);//NoWork == true라는 bool값
                        child6.Owner = this;
                        if (child6.ShowDialog() == DialogResult.OK)
                        {
                            Frm_tprc_NoWork_U child6_1 = new Frm_tprc_NoWork_U();
                            form = child6_1;
                        };
                        break;
                    case 5://잔량이동처리
                        //frm_mtr_RemainQtyMoveByLotID_U child4 = new frm_mtr_RemainQtyMoveByLotID_U();
                        ////child4.ShowDialog();
                        //form = child4;
                        //break;

                        //잔량이동처리 2 버전
                        frm_mtr_RemainQtyMoveByLotID2_U child4 = new frm_mtr_RemainQtyMoveByLotID2_U();
                        //child4.ShowDialog();
                        form = child4;
                        break;

                    case 6: // 공정전표 재발행
                        frm_tprc_CardRePrint_U child7 = new frm_tprc_CardRePrint_U();
                        form = child7;
                        break;

                    case 7://조회묶음 화면.
                        frm_tprc_SearchMenuCollection child8 = new frm_tprc_SearchMenuCollection();
                        form = child8;
                        break;

                    #region 주석 된것

                    //case 10://잔량이동처리실적 조회
                    //    frm_mtr_RemainMove_Q child10 = new frm_mtr_RemainMove_Q();
                    //    form = child10;
                    //    break;
                    //case 11://생산미달성 조회
                    //    Frm_tprc_MissingWorkQty child11 = new Frm_tprc_MissingWorkQty();
                    //    form = child11;
                    //    break;
                    //case 12://설비점검 조회
                    //    Frm_tprc_DailMachineCheck_Q child12 = new Frm_tprc_DailMachineCheck_Q();
                    //    form = child12;
                    //    break;
                    //case 13://금형점검 조회
                    //    if (btnDailyMoldQ.Text.Contains("금형"))
                    //    {
                    //        frm_tprc_DailMoldCheck_Q child13 = new frm_tprc_DailMoldCheck_Q();
                    //        form = child13;
                    //    }
                    //    else if (btnDailyMoldQ.Text.Contains("검사"))
                    //    {
                    //        if (!Lib.ReturnProcessRunStop("WizIns"))
                    //        {
                    //            Process ps = new Process();//실행중인 프로세스가 없을때 WizWork.exe
                    //            if (Environment.Is64BitOperatingSystem)//64비트일때
                    //            {
                    //                ps.StartInfo.FileName = "C:\\Program Files (x86)\\wizINS\\Upgrade_Ins.exe";
                    //            }
                    //            else//32비트일때
                    //            {
                    //                ps.StartInfo.FileName = "C:\\Program Files\\WizIns\\Upgrade_Ins.exe";
                    //            }
                    //            ps.Start();
                    //        }
                    //        else
                    //        {
                    //            Process[] process = Process.GetProcessesByName("WizIns");
                    //            if (process.Length > 0)
                    //            {
                    //                // 이미 실행중이라면, 뒤에 숨어있는 프로그램을 맨 앞으로 활성화시킨다.
                    //                string procTitle = null;
                    //                procTitle = process[0].MainWindowTitle;
                    //                IntPtr procHandler = FindWindow(null, procTitle);
                    //                SetForegroundWindow(procHandler);
                    //            }
                    //        }
                    //    }
                    //    break;

                    //case 18:
                    //    frm_tprc_setProcess child9 = new frm_tprc_setProcess(true);//NoWork == true라는 bool값
                    //    child9.Owner = this;
                    //    if (child9.ShowDialog() == DialogResult.OK)
                    //    {
                    //        // ok 리턴받은 후, 너는 더이상 할게 없음. 레지스트리 변경이면 충분.
                    //    };
                    //    //WizCommon.Popup.MyMessageBox.ShowBox(string.Format("작업중입니다. \r\n 기능은 담당자에게 문의하세요."), "[공사중]", 0, 1);
                    //    break;

                    //case 18:  // 검사 바로가기 버튼.
                    //    if (!Lib.ReturnProcessRunStop("WizIns"))
                    //    {
                    //        Process ps = new Process();//실행중인 프로세스가 없을때 WizWork.exe
                    //        if (Environment.Is64BitOperatingSystem)//64비트일때
                    //        {
                    //            ps.StartInfo.FileName = "C:\\Program Files (x86)\\wizINS\\Upgrade_5.exe";
                    //        }
                    //        else//32비트일때
                    //        {
                    //            ps.StartInfo.FileName = "C:\\Program Files\\WizIns\\Upgrade_5.exe";
                    //        }
                    //        ps.Start();
                    //    }
                    //    else
                    //    {
                    //        Process[] process = Process.GetProcessesByName("WizIns");
                    //        if (process.Length > 0)
                    //        {
                    //            // 이미 실행중이라면, 뒤에 숨어있는 프로그램을 맨 앞으로 활성화시킨다.
                    //            string procTitle = null;
                    //            procTitle = process[0].MainWindowTitle;
                    //            IntPtr procHandler = FindWindow(null, procTitle);
                    //            SetForegroundWindow(procHandler);
                    //        }
                    //    }
                    //    break;
                    #endregion

                    case 18:
                        frm_tprc_setProcess child9 = new frm_tprc_setProcess(true);//NoWork == true라는 bool값
                        child9.Owner = this;
                        if (child9.ShowDialog() == DialogResult.OK)
                        {
                            // ok 리턴받은 후, 너는 더이상 할게 없음. 레지스트리 변경이면 충분.
                        };
                        //WizCommon.Popup.MyMessageBox.ShowBox(string.Format("작업중입니다. \r\n 기능은 담당자에게 문의하세요."), "[공사중]", 0, 1);
                        break;

                    case 19://"Password/FrmSetting";
                        SettingClick();
                        
                        break;
                    case 20://Exit
                        Close();
                        SaveRegistry();
                        VBProcessKill();
                        break;
                    case 21://설비점검
                        Frm_tprc_DailMoldCheck child10 = new Frm_tprc_DailMoldCheck();
                        child10.mainForm = this;
                        form = child10;
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
            child3.mainForm = mainForm;
            Form form = child3;

            foreach (Form openForm in Application.OpenForms)//중복실행방지
            {
                if (openForm.Name == form.Name)
                {
                    //blOpen = true;
                    //openForm.BringToFront();
                    //openForm.Activate();
                    openForm.Close();
                    break;
                    //return;
                }
            }
            form.MdiParent = Frm_tprc_Main.ActiveForm;
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
            keypad = new POPUP.Frm_CMNumericKeypad("비밀번호", "");
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

        private void Frm_tprc_Main_FormClosing(object sender, FormClosingEventArgs e)
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
                g_tBase.sInstID = gs.GetValue("Work", "SetInstID", "");         //' 작업지시번호
                g_tBase.sPLotID = gs.GetValue("Work", "SetLOTID", "");          //' 지시LOTID번호
                g_tBase.ProcessID = gs.GetValue("Work", "SetProcessID", "");    //' 공정 ID   
                g_tBase.Process = gs.GetValue("Work", "SetProcess", "");        //' 공정명   
                g_tBase.MachineID = gs.GetValue("Work", "SetMachineID", "");    //. 설비 ID
                g_tBase.Machine = gs.GetValue("Work", "SetMachine", "");        //. 설비명
                g_tBase.TeamID = gs.GetValue("Work", "SetTeamID", "");          //' 작업조 코드
                g_tBase.Team = gs.GetValue("Work", "SetTeam", "");              //' 작업조
                g_tBase.PersonID = gs.GetValue("Work", "SetPersonID", "");      //' 작업자 코드
                g_tBase.Person = gs.GetValue("Work", "SetPerson", "");          //' 작업자명
                g_tBase.sMoldID = gs.GetValue("Work", "SetMoldID", "");         //' 금형명
                g_tBase.sMold = gs.GetValue("Work", "SetMold", "");             //' 금형ID
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
            //AFT의 경우 VB와 C#이 혼재 되어있는데, VB의 경우 특정경로에 레지스트리 값을 저장하지 못하므로
            //레지스트리 대신에 ini에 값들을 저장하고 불러온다.
            gs.SetValue("Work", "SetInstID", g_tBase.sInstID, ConnectionInfo.filePath);         //' 작업지시번호
            gs.SetValue("Work", "SetLOTID", g_tBase.sPLotID, ConnectionInfo.filePath);          //' 지시LOTID번호
            gs.SetValue("Work", "SetProcessID", g_tBase.ProcessID, ConnectionInfo.filePath);    //' 공정 ID   
            gs.SetValue("Work", "SetProcess", g_tBase.Process, ConnectionInfo.filePath);        //' 공정명   
            gs.SetValue("Work", "SetMachineID", g_tBase.MachineID, ConnectionInfo.filePath);    //. 설비 ID
            gs.SetValue("Work", "SetMachine", g_tBase.Machine, ConnectionInfo.filePath);        //. 설비명
            gs.SetValue("Work", "SetTeamID", g_tBase.TeamID, ConnectionInfo.filePath);          //' 작업조 코드
            gs.SetValue("Work", "SetTeam", g_tBase.Team, ConnectionInfo.filePath);              //' 작업조
            gs.SetValue("Work", "SetPersonID", g_tBase.PersonID, ConnectionInfo.filePath);      //' 작업자 코드
            gs.SetValue("Work", "SetPerson", g_tBase.Person, ConnectionInfo.filePath);          //' 작업자명
            gs.SetValue("Work", "SetDayOrNight", g_tBase.DayOrNightID, ConnectionInfo.filePath);    // 주 / 야 구별자

            gs.SetValue("Work", "SetMoldID", g_tBase.sMoldID, ConnectionInfo.filePath);         //' 금형명
            gs.SetValue("Work", "SetMold", g_tBase.sMold, ConnectionInfo.filePath);             //' 금형ID

            ////LocalMachine\\Software\\WizWork    registry경로
            //string regSubkey = "Software\\WizWork";// 서브키를 얻어온다. 없으면 null
            //RegistryKey rk = Registry.LocalMachine.OpenSubKey(regSubkey, true);// 없으면 서브키를 만든다.
            //if (rk == null)
            //{
            //    rk = Registry.CurrentUser.CreateSubKey(regSubkey);// 해당이름으로 서브키 생성
            //}
            //rk.SetValue("InstID", g_tBase.sInstID);             //' 작업지시번호
            //rk.SetValue("PLotID", g_tBase.sPLotID);             //' 지시LOTID번호
            ////rk.SetValue("OrderNO", g_tBase.OrderNO);            //' Order NO
            ////rk.SetValue("Custom", g_tBase.Custom);              //' 거래처 명

            //rk.SetValue("ProcessID", g_tBase.ProcessID);        //' 공정 ID
            //rk.SetValue("Process", g_tBase.Process);            //' 공정명
            //rk.SetValue("MachineID", g_tBase.MachineID);        //. 설비 ID
            //rk.SetValue("Machine", g_tBase.Machine);            //. 설비명

            //rk.SetValue("TeamID", g_tBase.TeamID);              //' 작업조 코드
            //rk.SetValue("Team", g_tBase.Team);                  //' 작업조
            //rk.SetValue("PersonID", g_tBase.PersonID);          //' 작업자 코드
            //rk.SetValue("Person", g_tBase.Person);              //' 작업자명

            //rk.SetValue("Mold", g_tBase.sMold);                 //' 금형명
            //rk.SetValue("MoldID", g_tBase.sMoldID);             //' 금형ID
        }

        public void Set_stsInfo()
        {
            stsInfo_Team.Text = g_tBase.Team;
            stsInfo_Team.Tag = g_tBase.TeamID;
            stsInfo_Person.Tag = g_tBase.PersonID;
            stsInfo_Person.Text = g_tBase.Person;          
            //stsInfo_ProMac.Text = g_tBase.Process + " " + g_tBase.Machine;
            stsInfo_ProMac.Tag = g_tBase.MachineID;
            stsInfo_Mold.Text = g_tBase.sMold;
            stsInfo_Mold.Tag = g_tBase.sMoldID;

            SaveRegistry();
        }

        private void SetScreen()
        {
            //버튼.Text 
            btnInfo.Text = "공지\r\n사항";
            btnDailyCheck.Text = "설비\r\n점검";
            btnInsInspectAuto.Text = "자주\r\n검사";
            btnWork.Text = "공정\r\n작업";
            btnNoWork.Text = "무작업";
            btnMove.Text = "잔량\r\n이동";

            btnRePrint.Text = "공정전표\r\n재발행";
            btnWorkQ.Text = "조회\r\n묶음";

            // 조회전용 묶음 페이지 생성을 통한 메인칸 절약 >> 2019.05.08 허윤구.
            //btnInsInspectAutoQ.Text = "자주검사실적";
            //btnDailyMoldQ.Text = "금형\r\n점검";
            //btnRemainMoveQ.Text = "잔량\r\n이동\r\n처리";
            //btnMissWorkQ.Text = "생산\r\n미달성";
            //btnDailyCheckQ.Text = "설비\r\n점검";

            btnChoiceWorker.Text = "작업자\r\n선택";
            btnSetting.Text = "환경\r\n설정";
            btnExit.Text = "작업\r\n종료";

            //버튼.Tag = 폼명
            btnInfo.Tag = "0";
            btnDailyCheck.Tag = "1";
            btnInsInspectAuto.Tag = "2";
            btnWork.Tag = "3";
            btnNoWork.Tag = "4";
            btnMove.Tag = "5";

            btnRePrint.Tag = "6";

            btnWorkQ.Tag = "7";

            // 조회전용 묶음 페이지 생성을 통한 메인칸 절약 >> 2019.05.08 허윤구.
            //btnInsInspectAutoQ.Tag = "9";
            //btnRemainMoveQ.Tag = "10";
            //btnMissWorkQ.Tag = "11";
            //btnDailyCheckQ.Tag = "12";
            //btnDailyMoldQ.Tag = "13";

            btnChoiceWorker.Tag = "18";
            btnSetting.Tag = "19";
            btnExit.Tag = "20";
            btnMold.Tag = "21";
            //
            double d_remainder = 0;
            foreach (Button btn in tlpTop.Controls)
            {
                btn.Click += btnControl_Click;
                d_remainder = 0;
                d_remainder = btn.Text.Length % 2;
                if (btn.Text.Length % 2 == 0)
                {
                    btn.Font = new Font("맑은 고딕", 9.75F, FontStyle.Bold);
                }
                else
                {
                    btn.Font = new Font("맑은 고딕", 8, FontStyle.Bold);
                }
            }

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
                string batchContent = "/c \"@ECHO OFF & timeout /t 6 > nul & start \"\" \"$[APPPATH]$\" & exit\"";
                batchContent = batchContent.Replace("$[APPPATH]$", Application.ExecutablePath);
                Process.Start("cmd", batchContent);
                Environment.Exit(0);
            }
        }

        private void Frm_tprc_Main_Activated(object sender, EventArgs e)
        {
            LoadRegistry();
            WorkInspectionCheck();
        }

        private void WorkInspectionCheck()
        {
            string strProcessID = gs.GetValue("Work", "ProcessID", "ProcessID");
            //if (strProcessID.Contains("6101"))
            //{
            //    btnDailyMoldQ.Text = "검사\r\n바로\r\n가기";
            //    btnDailyMoldQ.Image = Properties.Resources.comm3232;
            //    btnDailyMoldQ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(122)))), ((int)(((byte)(218)))));
            //}
            //else
            //{
            //    btnDailyMoldQ.Text = "금형\r\n점검";
            //    btnDailyMoldQ.Image = Properties.Resources.magnifying_glass;
            //    btnDailyMoldQ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(166)))), ((int)(((byte)(244)))));
            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {

        }

        
    }
}
