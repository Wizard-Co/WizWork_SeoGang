using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WizMon
{
    public partial class Frm_mon_Main : Form
    {
        WizWorkLib Lib = new WizWorkLib();

        private static Frm_Base _SubForm = null;
        private static List<string> lstSubForm = new List<string>();

        private Dictionary<int, string> dicMenuList = new Dictionary<int, string>();

        private List<string> lstMenuList = new List<string>();

        public static Stopwatch sw = new Stopwatch();

        public Frm_mon_Main()
        {
            InitializeComponent();

            // 타이머를 테이블 레이아웃 안에 배치 한 후 → 타이머를 가동 시켰을 때, 배경 이미지가 반짝 거리는 현상을 잡기 위해서 쓴 건데 안통함!!!!!! → 스탑워치를 절대값으로 위치 시키고, 사이즈 변경될때마다 좌표를 잡는 불편한 기능을 적용 中
            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
        }

        // 폼 로드 이벤트
        private void Frm_mon_Main_Load(object sender, EventArgs e)
        {
            setMenuList();

            // 타이머 세팅 ← ( 테이블 레이아웃 안에 세팅 → 타이머가 돌면서 이미지가 깜빡거림 현상 발생 → 절대값 좌표로 세팅 )
            setLocation_watchTimer();

            lblTime.BringToFront();
            watch_timer.Start();

            //if (offset > 0)
            //{
            //    using (var blurBrush = new SolidBrush(Color.FromArgb((shadowAlpha / 2), color)))
            //    {
            //        graphics.DrawString(text, font, blurBrush, (point.X + 1), point.Y);
            //        graphics.DrawString(text, font, blurBrush, (point.X - 1), point.Y);
            //    }
            //}

            // 첫번째 화면 로드
            loadMenu(lstMenuList[0]);
        }

        // 메뉴 세팅
        private void setMenuList()
        {
            ToolStripMenuItem m = new ToolStripMenuItem("화면");

            ToolStripMenuItem m0 = new ToolStripMenuItem();
            m0.Text = "공지사항";
            m0.Name = "Frm_mon_Info";
            m0.Click += Menu_Click;

            dicMenuList.Add(1, m0.Name);
            lstMenuList.Add(m0.Name);

            ToolStripMenuItem m1 = new ToolStripMenuItem();
            m1.Text = "호기별 실시간 작업 현황";
            m1.Name = "Frm_mon_Realtime_Q";
            m1.Click += Menu_Click;

            dicMenuList.Add(2, m1.Name);
            lstMenuList.Add(m1.Name);

            ToolStripMenuItem m2 = new ToolStripMenuItem();
            m2.Text = "작업지시 진행 현황";
            m2.Name = "Frm_mon_Inst_Q";
            m2.Click += Menu_Click;

            dicMenuList.Add(3, m2.Name);
            lstMenuList.Add(m2.Name);

            // Frm_mon_Stock_Q
            ToolStripMenuItem m3 = new ToolStripMenuItem();
            m3.Text = "공정별 재고 현황";
            m3.Name = "Frm_mon_Stock_Q";
            m3.Click += Menu_Click;

            dicMenuList.Add(4, m3.Name);
            lstMenuList.Add(m3.Name);

            m.DropDownItems.Add(m1);
            m.DropDownItems.Add(m2);
            m.DropDownItems.Add(m3);

            p_menuStrip.Items.Add(m);
        }

        // 폼 크기 변경시
        private void Frm_mon_Main_SizeChanged(object sender, EventArgs e)
        {
            // 타이머 세팅 ← ( 테이블 레이아웃 안에 세팅 → 타이머가 돌면서 이미지가 깜빡거림 현상 발생 → 절대값 좌표로 세팅 )
            setLocation_watchTimer();
        }

        // 타이머 세팅 ← ( 테이블 레이아웃 안에 세팅 → 타이머가 돌면서 이미지가 깜빡거림 현상 발생 → 절대값 좌표로 세팅 )
        private void setLocation_watchTimer()
        {
            int watchX = tlpButtons.Location.X;
            lblTime.Location = new Point(watchX - 160, tlpButtons.Location.Y + 60 + (p_menuStrip.Visible == false ? -(p_menuStrip.Height) : 0)); // 
        }

        // 로고 버튼 클릭 이벤트
        private void btnLogo_Click(object sender, EventArgs e)
        {
            if (p_menuStrip.Visible == true)
            {
                p_menuStrip.Visible = false;
                menuItem_MenuBar.Checked = false;
            }
            else
            {
                p_menuStrip.Visible = true;
                menuItem_MenuBar.Checked = true;
            }

            setLocation_watchTimer();
            panelHeader.Focus();
        }

        // 메뉴 클릭
        private void Menu_Click(object sender, EventArgs e)
        {
            var tsmiSender = sender as ToolStripMenuItem;

            loadMenu(tsmiSender.Name);
        }

        // 화면 로드
        private void loadMenu(string programName)
        {
            // 페이징 처리
            lblPage.Text = lstMenuList.IndexOf(programName) + 1 + " / " + lstMenuList.Count;

            foreach (Form openForm in Application.OpenForms)//중복실행방지
            {
                if (openForm.Name == programName)
                {
                    openForm.BringToFront();
                    openForm.Activate();

                    _SubForm = (Frm_Base)openForm;
                    if (openForm.Name.Equals("Frm_mon_Stock_Q"))
                    {
                        p_timer.Stop();
                        _SubForm.procQuery();
                    }

                    //lblTitle.Text = openForm.Text;
                    lblTitle.setText(openForm.Text);

                    lstSubForm.Remove(programName);
                    lstSubForm.Add(programName);
                    return;
                }
            }

            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            Frm_Base mSub = (Frm_Base)currentAssembly.CreateInstance(String.Format("{0}.{1}", "WizMon", programName));
            mSub.MdiParent = this;
            mSub.Dock = DockStyle.Fill;

            // 품목 재고 화면이면, 모든 품목의 재고(최소 3-4개의 품목 재고) 를 보여주고 다시 시작
            if (programName.Equals("Frm_mon_Stock_Q")) 
            {
                p_timer.Stop();
                mSub.FormClosing += new FormClosingEventHandler(child_Closing); 
            } 

            mSub.Show();

            _SubForm = (Frm_Base)mSub;

            //lblTitle.Text = mSub.Text;
            lblTitle.setText(mSub.Text);
            mSub.WindowState = FormWindowState.Maximized;

            // 활성화된 목록에 추가.
            lstSubForm.Add(programName);

        }

        #region 자식 폼에서 메인의 타이머 조작을 위해

        private void child_Closing(object sender, FormClosingEventArgs e)
        {
            lstSubForm.Remove(_SubForm.Name);
            setNextForm();

            if (btnPlay.Tag.ToString().Equals("Pause")) { p_timer.Start(); }
        }

        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _SubForm.procQuery();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            _SubForm.procExcel();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //_SubForm.procClear();

            if (lstSubForm.Count > 0)
            {
                lstSubForm.Remove(_SubForm.Name);
                _SubForm.Close();

                if (lstSubForm.Count == 0) {
                    lblTitle.setText("모니터링 화면");
                    lblPage.Text = "0 / " + lstMenuList.Count;
                    return; 
                }

                loadMenu(lstSubForm[lstSubForm.Count - 1]);
            }
            else
            {
                if (MessageBox.Show("모니터링 프로그램을 종료하시겠습니까?", "[프로그램 종료 전]", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        #region 메뉴 로테이션 버튼 모음

        private void btnPlay_Click(object sender, EventArgs e)
        {
            // 플레이 버튼 ↔ 일시정지 버튼 변환
            var btnSender = sender as Button;
            if (btnSender.Tag.ToString().Equals("Play")) // 로테이션 플레이
            {
                if (txtInterval.Text.Trim().Equals("")) { txtInterval.Text = "10"; }

                btnSender.Tag = "Pause";
                btnSender.BackgroundImage = global::WizWork.Properties.Resources.baseline_pause;
                int Interval = Lib.ConvertInt(txtInterval.Text);
                p_timer.Interval = Interval * 1000; // 0초일때는 기본 10초로 변경
                p_timer.Start();
            }
            else // 로테이션 멈춤
            {
                btnSender.Tag = "Play";
                btnSender.BackgroundImage = global::WizWork.Properties.Resources.baseline_play;
                p_timer.Stop();
            }
        }

        private void btnBefore_Click(object sender, EventArgs e)
        {
            // 현재 화면이 없을때는 리턴
            if (_SubForm == null) { return; }

            int nextIndex = lstMenuList.IndexOf(_SubForm.Name) - 1;

            // 이전 화면이 없을때 리턴
            if (nextIndex < 0) { return; }

            string programName = lstMenuList[nextIndex];
            loadMenu(programName);
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // 현재 화면이 없을때는 리턴
            if (_SubForm == null) { return; }

            int nextIndex = lstMenuList.IndexOf(_SubForm.Name) + 1;
            //nextIndex = nextIndex < lstMenuList.Count ? nextIndex : 0;

            // 다음 화면이 없을때 리턴
            if (nextIndex >= lstMenuList.Count) { return; }

            string programName = lstMenuList[nextIndex];
            loadMenu(programName);
        }

        #endregion

        // 다른 화면에서 p_timer 제어

        // 화면 로테이션 타이머 이벤트
        private void p_timer_Tick(object sender, EventArgs e)
        {
            setNextForm();
        }

        // 다음 화면으로
        private void setNextForm()
        {
            // 메뉴가 없을 경우 리턴
            if (lstMenuList.Count == 0) { return; }

            int nextIndex = 0;

            if (_SubForm != null)
            {
                nextIndex = lstMenuList.IndexOf(_SubForm.Name) + 1;
                nextIndex = nextIndex < lstMenuList.Count ? nextIndex : 0; // 마지막 Index 면 0

                string programName = lstMenuList[nextIndex];
                loadMenu(programName);
            }
            else
            {
                loadMenu(lstMenuList[nextIndex]);
            }
        }

        // 갱신주기 텍스트박스 변경 이벤트
        private void txtInterval_TextChanged(object sender, EventArgs e)
        {
            p_timer.Interval = Lib.ConvertInt(txtInterval.Text) * 1000;
        }

        // 현재시간 타이머 이벤트
        private void watch_timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        #region 테스트

        /// <summary>
        /// 참조: Microsoft.Office.Interop.Excel 추가
        /// Methods.cs 소스 최상단에 Excel = Microsoft.Office.Interop.Excel 추가
        /// 사용법 : 그리드뷰에 태그이름으로 ListBox가 입력됨
        /// List<string> list = new List<string>();
        ///    list.Add(Convert.ToString(this.p_dgv_Notice.Tag));
        /// Frm_GridViewSelect mSub = new Frm_GridViewSelect(list);
        /// DialogResult result = mSub.ShowDialog();
        /// if (result == DialogResult.OK) 
        ///         mSub.Value으로 리턴 됨.
        /// </summary>
        /// <param name="captions">캡션명 true일때 Excel에 찍힘</param>
        /// <param name="dataGridView">선택 혹은 특정 DataGridView를 넘겨주면 그 DataGridView 출력</param>
        /// <param name="saveFileDialog">저장SaveFileDialog</param>
        /// //기존 셀하나씩 값을 대입하는방식에서 한번에 넣는 방식으로 속도개선.
        public static void ExportExcel(bool captions, DataGridView dataGridView, SaveFileDialog saveFileDialog, string filename)
        {
            saveFileDialog.FileName = filename;       //파일명
            saveFileDialog.DefaultExt = "xls";          //확장자
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog.InitialDirectory = Environment.SpecialFolder.Recent.ToString();
            //saveFileDialog.InitialDirectory = "c:\\";       //경로

            DialogResult result = saveFileDialog.ShowDialog();      //저장다이얼로그

            if (result == DialogResult.OK)      //다이얼로그 OK일 경우
            {
                sw.Start();

                int num = 0;
                object missingType = Type.Missing;


                Excel.Application objApp = null;
                Excel._Workbook objBook = null;
                Excel.Workbooks objBooks = null;
                Excel.Sheets objSheets = null;
                Excel._Worksheet objSheet = null;
                Excel.Range range = null;

                string[] headers = new string[dataGridView.ColumnCount];
                string[] columns = new string[dataGridView.ColumnCount];

                for (int c = 0; c < dataGridView.ColumnCount; c++)
                {
                    if (dataGridView.Rows.Count == 0)           //그리드뷰에 Row가 없을 경우
                    {
                        headers[c] = dataGridView.Columns[c].HeaderText.ToString();         //컬럼에 있는 HeaderText값을 headers에 넣어줌
                        num = c + 65;                                                   // 순차적으로 65=A, 66=B 엑셀의 위치를 Columns위치를 잡아줌
                        columns[c] = Convert.ToString((char)num);                       // columns에 차례대로 넣음.
                    }
                    else if (c > 25)        //Columns가 엑셀에서 25자리가 A~Z까지고 26부터는 AA~AZ라서
                    {
                        headers[c] = dataGridView.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();
                        columns[c] = Convert.ToString((char)(Convert.ToInt32(c / 26) - 1 + 65)) + Convert.ToString((char)(c % 26 + 65));
                    }
                    else            //Row가 없는데 else로 들어오면 Row[0]을 찾지못해서 에러남.
                    {
                        headers[c] = dataGridView.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();
                        num = c + 65;
                        columns[c] = Convert.ToString((char)num);
                    }
                }

                try
                {
                    objApp = new Excel.Application();
                    objBooks = objApp.Workbooks;
                    objBook = objBooks.Add(Missing.Value);
                    objSheets = objBook.Worksheets;
                    objSheet = (Excel._Worksheet)objSheets.get_Item(1);

                    if (captions)   //Captions가 True일때
                    {
                        for (int c = 0; c < dataGridView.ColumnCount; c++) //캡션명
                        {
                            range = objSheet.get_Range(columns[c] + "1", Missing.Value);
                            range.set_Value(Missing.Value, headers[c]);
                            range.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                            range.Borders.Color = ColorTranslator.ToOle(Color.Black);
                        }
                    }


                    //엑셀에 넣을 배열 생성 **** 추가
                    object[,] saNames = new object[dataGridView.RowCount, dataGridView.ColumnCount];

                    //DataGridView의 타입저장**** 추가
                    string tp;
                    for (int i = 0; i < dataGridView.RowCount; i++)    //DataGirdView에 Row값
                    {
                        for (int j = 0; j < dataGridView.ColumnCount; j++)
                        {

                            //Cell 타입 가져오기**** 추가
                            tp = dataGridView.Rows[i].Cells[j].ValueType.Name;
                            //Cell Merge가 있는지 확인
                            DataGridViewCell cell = dataGridView.Rows[i].Cells[j] as DataGridViewCell;
                            //배열에 그리드값 가져와 대입**** 추가
                            if (tp == "String") //2000-01-01 형태의 날짜 필터하기 위함(숫자로 변환 방지)
                                saNames[i, j] = "'" + dataGridView.Rows[i].Cells[j].Value.ToString();
                            else
                                saNames[i, j] = dataGridView.Rows[i].Cells[j].Value;
                        }
                    }

                    //해당 시트에 Range를 가져 와서 Value2에 한번에 밀어 넣는다   **** 추가
                    //2번째줄부터, 가로길이 + 세로길이 .Value2 = 배열에 담긴 값 한번에 밀어넣기
                    objSheet.get_Range(columns[0] + "2", columns[columns.Length - 1] + (dataGridView.RowCount + 1)).Value2 = saNames;

                    sw.Stop();
                    sw.Elapsed.ToString();


                    objApp.Visible = false;
                    objApp.UserControl = false;

                    objBook.SaveAs(@saveFileDialog.FileName,
                               Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
                               missingType, missingType, missingType, missingType,
                               Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                               missingType, missingType, missingType, missingType, missingType);
                    objBook.Close(false, missingType, missingType);

                    Cursor.Current = Cursors.Default;

                    MessageBox.Show("Save Success!!!");     //성공했을경우 메세지박스

                    //엑셀프로세스 종료 
                    objBooks.Close();
                    objApp.Quit();

                    objSheets = null;
                    objBooks = null;
                    objApp = null;
                }
                catch (Exception theException)
                {
                    String errorMessage;
                    errorMessage = "Error: ";
                    errorMessage = String.Concat(errorMessage, theException.Message);
                    errorMessage = String.Concat(errorMessage, " Line: ");
                    errorMessage = String.Concat(errorMessage, theException.Source);

                    MessageBox.Show(errorMessage, "Error");
                }
                finally
                {
                    // Clean up
                    ReleaseExcelObject(objSheets);
                    ReleaseExcelObject(objBooks);
                    ReleaseExcelObject(objApp);
                }

                System.Diagnostics.Process.Start(saveFileDialog.FileName);
            }
        }

        /// <summary>
		/// Excel 처리후 정리
		/// </summary>
		/// <param name="obj"></param>
		private static void ReleaseExcelObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                throw ex;
            }
            finally
            {
                GC.Collect();
            }
        }


        public static void ExportExcel2(bool captions, DataGridView dataGridView, SaveFileDialog saveFileDialog, string filename)
        {
            saveFileDialog.FileName = filename;       //파일명
            saveFileDialog.DefaultExt = "xls";          //확장자
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog.InitialDirectory = Environment.SpecialFolder.Recent.ToString();
            //saveFileDialog.InitialDirectory = "c:\\";       //경로

            DialogResult result = saveFileDialog.ShowDialog();      //저장다이얼로그

            if (result == DialogResult.OK)      //다이얼로그 OK일 경우
            {
                sw.Start();

                //int num = 0;
                object missingType = Type.Missing;

                Excel.Application objApp = null;
                Excel._Workbook objBook = null;
                Excel.Workbooks objBooks = null;
                Excel.Sheets objSheets = null;
                Excel._Worksheet objSheet = null;
                Excel.Range range = null;

                string[] headers = new string[dataGridView.ColumnCount];

                for (int c = 0; c < dataGridView.ColumnCount; c++)
                {
                    headers[c] = dataGridView.Rows[0].Cells[c].OwningColumn.HeaderText.ToString();
                }

                try
                {
                    objApp = new Excel.Application();
                    objBooks = objApp.Workbooks;
                    objBook = objBooks.Add(Missing.Value);
                    objSheets = objBook.Worksheets;
                    objSheet = (Excel._Worksheet)objSheets.get_Item(1);

                    Excel.Worksheet sheet = new Excel.Worksheet();

                    if (captions)   //Captions가 True일때
                    {
                        for (int c = 0; c < dataGridView.ColumnCount; c++) //캡션명
                        {
                            range = objSheet.get_Range(objSheet.Cells[1, 1], objSheet.Cells[1, c + 1]);
                            range.set_Value(Missing.Value, headers[c]);
                            range.Interior.Color = ColorTranslator.ToOle(Color.LightGray);
                            range.Borders.Color = ColorTranslator.ToOle(Color.Black);
                        }
                    }


                    //엑셀에 넣을 배열 생성 **** 추가
                    object[,] saNames = new object[dataGridView.RowCount, dataGridView.ColumnCount];

                    //DataGridView의 타입저장**** 추가
                    string tp;
                    for (int i = 0; i < dataGridView.RowCount; i++)    //DataGirdView에 Row값
                    {
                        for (int j = 0; j < dataGridView.ColumnCount; j++)
                        {

                            //Cell 타입 가져오기**** 추가
                            tp = dataGridView.Rows[i].Cells[j].ValueType.Name;
                            //Cell Merge가 있는지 확인
                            DataGridViewCell cell = dataGridView.Rows[i].Cells[j] as DataGridViewCell;
                            //배열에 그리드값 가져와 대입**** 추가
                            if (tp == "String") //2000-01-01 형태의 날짜 필터하기 위함(숫자로 변환 방지)
                                saNames[i, j] = "'" + dataGridView.Rows[i].Cells[j].Value.ToString();
                            else
                                saNames[i, j] = dataGridView.Rows[i].Cells[j].Value;
                        }
                    }

                    //해당 시트에 Range를 가져 와서 Value2에 한번에 밀어 넣는다   **** 추가
                    //2번째줄부터, 가로길이 + 세로길이 .Value2 = 배열에 담긴 값 한번에 밀어넣기
                    objSheet.get_Range(objSheet.Cells[1, 1], objSheet.Cells[headers.Length - 1, dataGridView.RowCount]).Value2 = saNames;

                    sw.Stop();
                    sw.Elapsed.ToString();

                    objApp.Visible = false;
                    objApp.UserControl = false;

                    objBook.SaveAs(@saveFileDialog.FileName,
                               Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal,
                               missingType, missingType, missingType, missingType,
                               Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                               missingType, missingType, missingType, missingType, missingType);
                    objBook.Close(false, missingType, missingType);

                    Cursor.Current = Cursors.Default;

                    MessageBox.Show("Save Success!!!");     //성공했을경우 메세지박스

                    //엑셀프로세스 종료 
                    objBooks.Close();
                    objApp.Quit();

                    objSheets = null;
                    objBooks = null;
                    objApp = null;
                }
                catch (Exception theException)
                {
                    String errorMessage;
                    errorMessage = "Error: ";
                    errorMessage = String.Concat(errorMessage, theException.Message);
                    errorMessage = String.Concat(errorMessage, " Line: ");
                    errorMessage = String.Concat(errorMessage, theException.Source);

                    MessageBox.Show(errorMessage, "Error");
                }
                finally
                {
                    // Clean up
                    ReleaseExcelObject(objSheets);
                    ReleaseExcelObject(objBooks);
                    ReleaseExcelObject(objApp);
                }

                System.Diagnostics.Process.Start(saveFileDialog.FileName);
            }
        }



        #endregion

        #region 시스템 메뉴아이템 모음

        // 타이틀바 보이기 / 안보이기
        private void menuItem_TitleBar_CheckedChanged(object sender, EventArgs e)
        {
            if (menuItem_TitleBar.Checked == true)
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            }
            else
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            }
        }

        // 메뉴바 보이기 / 안보이기
        private void menuItem_MenuBar_CheckedChanged(object sender, EventArgs e)
        {
            if (menuItem_MenuBar.Checked == true)
            {
                p_menuStrip.Visible = true;
            }
            else
            {
                p_menuStrip.Visible = false;
            }
        }

        #endregion


        #region 자식 폼에서 정보 가져오기 모음

        public int getInterval()
        {
            return Lib.ConvertInt(txtInterval.Text);
        }

        public bool getPlayState()
        {
            return btnPlay.Tag.ToString().Equals("Play") ? false : true;
        }

        #endregion
    }
}
