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
    public partial class Frm_mon_Stock_Q : Frm_Base
    {
        WizWorkLib Lib = new WizWorkLib();

        // 전체 품명 재고 나눠서 담기
        List<DataTable> lstDt = new List<DataTable>();

        // 전체 품명을 몇개로 할것인지
        int RowCnt = 17;

        // 지금 인덱스
        int nowIndex = 0;

        public Frm_mon_Stock_Q()
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
                nowIndex = 0;

                // Procedure로 전달할 데이터 설정
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_prdMon_sArticleStock]", sqlParameter, false);

                // DataTable 을 RowCnt 만큼 나눠서 담기.
                lstDt = divideDataTableByRowCount(dt, RowCnt);

                //BindingSource bs = new BindingSource();
                //bs.DataSource = lstDt[0];
                //dgdMain.DataSource = bs;

                //dgdMain.ClearSelection();

                // 초기 세팅
                if (lstDt.Count >= 2)
                {
                    setDataGrid(lstDt[0], lstDt[1]);
                }
                else if (lstDt.Count == 1)
                {
                    setDataGrid(lstDt[0], null);
                }

                nowIndex += 2;

                // 타이머 세팅 및 시작
                if (timer_ArticleStock.Enabled == true) { timer_ArticleStock.Stop(); }

                int interval = 0;
                if (this.MdiParent != null)
                {
                    Frm_mon_Main parentForm = this.MdiParent as Frm_mon_Main;
                    interval = parentForm.getInterval();
                }

                timer_ArticleStock.Interval = interval != 0 ? interval * 1000 : 10 * 1000;
                timer_ArticleStock.Start();
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

        private List<DataTable> divideDataTableByRowCount(DataTable dt, int rowCnt)
        {
            List<DataTable> lstDataTable = new List<DataTable>();
            // 5
            // Num >= 1 AND Num <= 5

            // Num >= 6 AND Num <= 10

            // Num >= 11 AND Num <= 15

            int dtCnt = (int)Math.Ceiling(1.0 * dt.Rows.Count / rowCnt);
            for(int i = 0; i < dtCnt; i++)
            {
                int start = (i * rowCnt) + 1;
                int end = (i * rowCnt) + rowCnt;

                lstDataTable.Add(dt.Select("Num >= " + start + " AND Num <= " + end).CopyToDataTable());
            }

            return lstDataTable;
        }

        #endregion

        private void Frm_mon_Stock_Q_Load(object sender, EventArgs e)
        {
            setInitGrid();

            procQuery();
        }

        #region dgdMain - 데이터그리드뷰 초기 세팅 : setInitGrid()

        private void setInitGrid()
        {

        }

        #endregion

        // 품목을 잘라서 저장한 후에, 갱신시간을 가져와서 순서대로 보여주도록
        private void timer_ArticleStock_Tick(object sender, EventArgs e)
        {
            nextDgvSetting();
        }

        private void nextDgvSetting()
        {
            if (lstDt.Count == 0) { return; }

            int maxIndex = lstDt.Count - 1;

            if (nowIndex > maxIndex)
            {
                timer_ArticleStock.Stop();

                if ((this.MdiParent as Frm_mon_Main).getPlayState() == true)
                {
                    this.Close();
                }
                //Program.MainForm.hid
            }
            else if (nowIndex == maxIndex)
            {
                setDataGrid(lstDt[nowIndex - 1], lstDt[nowIndex]);
            }
            else
            {
                setDataGrid(lstDt[nowIndex], lstDt[nowIndex + 1]);
            }

            nowIndex += 2;
        }

        #region 메인, 서브 그리드 채우기

        private void setDataGrid(DataTable dtMain, DataTable dtSub)
        {
            //Main DataGridView
            BindingSource bs = new BindingSource();
            bs.DataSource = dtMain;
            dgdMain.DataSource = bs;

            // Sub DataGridView
            bs = new BindingSource();
            bs.DataSource = dtSub;
            dgdSub.DataSource = bs;
        }

        #endregion 
    }

    class Frm_mon_Stock_Q_CodeView
    {
        public string Num { get; set; }
        public string BuyerArticleNo { get; set; }
        public string Article { get; set; }
        public string StockQty { get; set; }
    }
}
