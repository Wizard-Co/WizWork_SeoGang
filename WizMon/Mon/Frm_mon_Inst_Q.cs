using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;

namespace WizMon
{
    public partial class Frm_mon_Inst_Q : Frm_Base
    {
        WizWorkLib Lib = new WizWorkLib();

        public Frm_mon_Inst_Q()
        {
            InitializeComponent();
        }

        #region IProcedure 멤버

        public override void procClear()
        {
            this.Close();
        }

        public override void procExcel()
        {
            Frm_mon_Main.ExportExcel(true, dgdMain, new SaveFileDialog(), "Test.xls");
        }

        public override void procQuery()
        {
            try
            {
                // Procedure로 전달할 데이터 설정
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_prdMon_sProgressRateOfDailyPlan]", sqlParameter, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    DataTable dtDgv = dt.Select("Num <> 0").CopyToDataTable();
                    DataTable dtChart = dt.Select("Num = 0").CopyToDataTable();

                    BindingSource bs = new BindingSource();
                    bs.DataSource = dtDgv;
                    dgdMain.DataSource = bs;

                    setChartInst(dtChart);

                    dgdMain.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<cmdSave>\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
            finally
            {
                DataStore.Instance.CloseConnection();
            }
        }

        #endregion

        private void Frm_mon_Inst_Q_Load(object sender, EventArgs e)
        {
            setInitGrid();

            setInitChart();

            procQuery();
        }

        #region 검색 → 차트 세팅 : setChartInst()

        private void setChartInst(DataTable dt)
        {
            //Random rnd = new Random();
            //Chart mych = new Chart();
            //mych.Series.Add("duck");

            //mych.Series["duck"].SetDefault(true);
            //mych.Series["duck"].Enabled = true;
            //mych.Visible = true;

            //for (int q = 0; q < 10; q++)
            //{
            //    int first = rnd.Next(0, 10);
            //    int second = rnd.Next(0, 10);
            //    mych.Series["duck"].Points.AddXY(first, second);
            //    Debug.WriteLine(first + "  " + second);
            //}
            //mych.Show();
            //Controls.Add(mych);
            //mych.Show();

            #region DataTable 을 받아서 진행

            try
            {
                chartInst.Series[0].Points.Clear();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // y value
                    double ProcessRate = Math.Round(Lib.ConvertDouble(dt.Rows[i]["ProcessRate"].ToString()), 1);

                    chartInst.Series[0].Points.Add(ProcessRate);

                    // x value
                    string Process = dt.Rows[i]["Process"].ToString();

                    chartInst.Series[0].Points[i].AxisLabel = Process;
                }

                //chartInst.ChartAreas[0].AxisY.Maximum += 10;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            #endregion

            #region 데이터그리드의 정보로 소스에서 계산

            //try
            //{
            //    for (int i = 0; i < dgdMain.Rows.Count; i++)
            //    {
            //        // Y value
            //        double InstQty = Lib.ConvertDouble(dgdMain.Rows[i].Cells["InstQty"].Value.ToString());
            //        double WorkQty = Lib.ConvertDouble(dgdMain.Rows[i].Cells["WorkQty"].Value.ToString());
            //        double ProcessRate = InstQty == 0 ? 0 : WorkQty / InstQty * 100;

            //        chartInst.Series["Series1"].Points.Add(ProcessRate);

            //        // x value
            //        string Process = dgdMain.Rows[i].Cells["Process"].Value.ToString();

            //        chartInst.Series["Series1"].Points[i].AxisLabel = Process;

            //        //if (chartInst.Series["Series1"].Points.Count == 0
            //        //    || chartInst.se)
            //        //{

            //        //}
            //        //else
            //        //{
            //        //    chartInst.Series["Series1"].Points[i].AxisLabel = Process + (Lib.ConvertInt(chartInst.Series["Series1"].Points[i].AxisLabel.Replace(Process, "")) + 1);
            //        //}
            //    }

            //    chartInst.Series["Series1"].Name = "진척률";
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}

            #endregion
        }

        #endregion

        #region dgdMain - 데이터그리드뷰 초기 세팅 : setInitGrid()

        private void setInitGrid()
        {
            //
            // ColumnHeader
            // 

            // BackColor
            //this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#1E3964"); // 헤더 배경색 변경 ← 디자인에서 변경이 안됨 : EnableHeadersVisualStyles = false 로 해줘야 됨

            // Header Text
            dgdMain.Columns["InstQty"].HeaderText = "지시\r\n수량";
            dgdMain.Columns["WorkQty"].HeaderText = "작업\r\n수량";
            dgdMain.Columns["DefectQty"].HeaderText = "불량\r\n수량";
            dgdMain.Columns["DefectPPM"].HeaderText = "불량률\r\n(ppm)";

            // Header Width ← DisplayCell 상태일땐 안먹힘
            //dgdMain.Columns["Process"].Width = 120;
            //dgdMain.Columns["Machine"].Width = 120;
            //dgdMain.Columns["WorkQty"].Width = 180;

            // Header AutoSizeMode
            dgdMain.Columns["Process"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }

        #endregion

        #region chartInst 차트 초기 세팅

        private void setInitChart()
        {
            // Y 축 최대값
            chartInst.ChartAreas[0].AxisY.Maximum = 100;

            // Y 축 포맷
            chartInst.ChartAreas[0].AxisY.LabelStyle.Format = "###0\\%";
            
            // 바 값 라벨 보이도록
            //chartInst.Series[0].IsValueShownAsLabel = true;

            // 차트 안 Y축 선 안보이게
            chartInst.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;

            // 스마트 라벨 - 
            //chartInst.Series[0].SmartLabelStyle.AllowOutsidePlotArea = System.Windows.Forms.DataVisualization.Charting.LabelOutsidePlotAreaStyle.Yes;
            //chartInst.Series[0].SmartLabelStyle.IsMarkerOverlappingAllowed = false;
            //chartInst.Series[0].SmartLabelStyle.MovingDirection = System.Windows.Forms.DataVisualization.Charting.LabelAlignmentStyles.Top;
        }

        #endregion
    }

    class Frm_mon_Inst_Q_CodeView
    {
        public string Num { get; set; }
        public string Process { get; set; }
        public string Machine { get; set; }
        public string BuyerArticleNo { get; set; }
        public string Article { get; set; }
        public string CollectQty { get; set; }
        public string WorkQty { get; set; }
        public string DefectQty { get; set; }
    }
}
