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
    public partial class Frm_mon_Info : Frm_Base
    {
        WizWorkLib Lib = new WizWorkLib();

        public Frm_mon_Info()
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
            //Frm_mon_Main.ExportExcel(true, dgdMain, new SaveFileDialog(), "Test.xls");
        }

        public override void procQuery()
        {
            try
            {
                // Procedure로 전달할 데이터 설정
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                sqlParameter.Add("sCompanyID", "");
                sqlParameter.Add("SDate", DateTime.Now.ToString("yyyyMMdd"));
                sqlParameter.Add("EDate", DateTime.Now.ToString("yyyyMMdd"));

                DataTable dt = DataStore.Instance.ProcedureToDataTable("xp_Info_sInfoByDate", sqlParameter, false);

                if (dt != null
                    && dt.Rows.Count > 0)
                {
                    txtInfo.Text = "오늘은 " + string.Format(@"{0:yyyy년 MM월 dd일}", DateTime.Now) + " 입니다 \r\n\r\n";

                    foreach(DataRow dr in dt.Rows)
                    {
                        txtInfo.Text += dr["Info"].ToString() + "\r\n";
                    }
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

        //// 로드로 했을때 텍스박스가 셀렉션 상태가 됨??
        //private void Frm_mon_Info_Load(object sender, EventArgs e)
        //{
        //    procQuery();
        //}

        private void Frm_mon_Info_Activated(object sender, EventArgs e)
        {
            procQuery();
        }
    }

    class Frm_mon_Info_CodeView
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
