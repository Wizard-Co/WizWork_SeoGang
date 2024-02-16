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
    public partial class Frm_mon_Realtime_Q : Frm_Base
    {
        public Frm_mon_Realtime_Q()
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
            
        }

        public override void procQuery()
        {
            try
            {
                // Procedure로 전달할 데이터 설정
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                DataTable dt = DataStore.Instance.ProcedureToDataTable("[xp_prdMon_sWorkByProcessMachine]", sqlParameter, false);

                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dgdMain.DataSource = bs;
            }
            catch(Exception ex)
            {
                WizCommon.Popup.MyMessageBox.ShowBox(string.Format("오류! 관리자에게 문의<cmdSave>\r\n{0}", ex.Message), "[오류]", 0, 1);
            }
            finally
            {
                DataStore.Instance.CloseConnection();
            }
        }

        #endregion

        private void Frm_mon_Realtime_Q_Load(object sender, EventArgs e)
        {
            setInitGrid();

            procQuery();
        }

        private void setInitGrid()
        {
            //
            // ColumnHeader
            // 

            // BackColor
            //this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#1E3964"); // 헤더 배경색 변경 ← 디자인에서 변경이 안됨 : EnableHeadersVisualStyles = false 로 해줘야 됨

            // Header Text
            dgdMain.Columns["CollectQty"].HeaderText = "수집\r\n수량";
            dgdMain.Columns["DefectQty"].HeaderText = "불량\r\n수량";

            // Header Width ← DisplayCell 상태일땐 안먹힘
            //dgdMain.Columns["Process"].Width = 120;
            //dgdMain.Columns["Machine"].Width = 120;
            //dgdMain.Columns["WorkQty"].Width = 180;

            // Header AutoSizeMode
            dgdMain.Columns["Process"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgdMain.Columns["Machine"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dgdMain.Columns["WorkQty"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
        }
    }

    class Frm_mon_Realtime_Q_CodeView
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
