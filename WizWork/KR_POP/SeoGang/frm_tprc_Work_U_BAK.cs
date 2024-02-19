using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KR_POP.WTS.Tools;
using KR_POP.Properties;
using KR_POP.Common;
using System.Transactions;
using System.Data.SqlClient;

namespace KR_POP.WTS
{
    public partial class Frm_tprc_Work_U_BAK : Form//, DataDeliver
    {
        //sInstID로 가져오는 OrderID
        string sOrderID = string.Empty;

        public delegate void TextEventHandler();    // string을 반환값으로 갖는 대리자를 선언합니다.
        public event TextEventHandler SendEvent;          // 대리자 타입의 이벤트 처리기를 설정합니다. 
        //생산불량폼에서 가져온 값을 저장할 변수들
        string[] sDefectID = null;          //생산불량폼에서 가져온 불량ID
        string[] sDefectQty = null;         //생산불량폼에서 가져온 불량수량
        string[] sXPos = null;              //생산불량폼에서 가져온 X열 갯수, 배열마다 값을 가지고 있지만 전부 동일함
        string[] sYPos = null;              //생산불량폼에서 가져온 Y열 갯수, 배열마다 값을 가지고 있지만 전부 동일함

        //(plinput_q->setprocess->work_u)
        public static string pl_ProcessID { get; set; }//plintput_q에서 가져온 ProcessID(plinput_q->setprocess->work_u)
        public static string pl_InstID { get; set; } //plintput_q에서 가져온 InstID
        //(setprocess->work_u)
        public static string set_sProcess { get; set; }//setProcess에서 가져온 ProcessID
        public static string set_sTeam { get; set; }//setProcess에서 가져온 TeamID(setprocess->work_u)
        public static string set_sMachine { get; set; }//setProcess에서 가져온 MachineID(setprocess->work_u)
        public static string set_sPersonID { get; set; }//setProcess에서 가져온 PersonID(setprocess->work_u)
        public static string set_sUserID { get; set; }//setProcess에서 가져온 UserID(setprocess->work_u)

        public static string Mold_2101 { get; set; }//Mold_Q에서 가져온 성형공정 전용 변수

        string sProcessID = string.Empty;
        string sMachineID = string.Empty;
        string sPersonID = string.Empty;
        string sTeam = string.Empty;
        string g_Number = string.Empty;
        string sUserID = string.Empty;

        private DataSet ds = null;
        bool DuringGrid = false;
        
        

        public bool _hasChanges = false;

        //decimal JobID = 0;//작업지시번호로 iWkResult에서 등록한 JobID를 조회한다.

        int JobID = 0;//작업지시번호로 iWkResult에서 등록한 JobID를 조회한다.

        public Frm_tprc_Work_U_BAK()
        {
            InitializeComponent();

            //setProcess폼에서 선택한 변수값들 work_u 변수에 집어넣어주기
            sProcessID = set_sProcess;//setProcess폼에서 선택한 ProcessID
            sMachineID = set_sMachine;//setProcess폼에서 선택한 MachineID
            sPersonID = set_sPersonID;//setProcess폼에서 선택한 PersonID
            sTeam = set_sTeam;//setProcess폼에서 선택한 TeamID
            sUserID = set_sUserID;//setProcess폼에서 선택한 UserID

            if (sProcessID == "2101")
            {
                lbl2101.Visible = true;
                lbl_sMold.Visible = true;
                lbl_sMold.Text = Mold_2101;
            }
            //Initialization();

            //AllClear();
            Console.WriteLine(sProcessID + sMachineID);
            setDTP();
        }
        private void setDTP()
        {
            dtpOrderDateFrom.Format = DateTimePickerFormat.Custom;
            dtpOrderDateFrom.CustomFormat = "yyyy-MM-dd";
            dtpOrderDateTo.Format = DateTimePickerFormat.Custom;
            dtpOrderDateTo.CustomFormat = "yyyy-MM-dd";
            dtpOrderTimeFrom.Format = DateTimePickerFormat.Custom;
            dtpOrderTimeFrom.CustomFormat = "HH:mm:ss";
            dtpOrderTimeTo.Format = DateTimePickerFormat.Custom;
            dtpOrderTimeTo.CustomFormat = "HH:mm:ss";
        }

        public void SetData(String Data1, String Data2)
        {
            sProcessID = Data1;
            sMachineID = Data2;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] DefectID = null;
            string[] DefectQty = null;
            string[] XPos = null;
            string[] YPos = null;
            int i = 0;

            GetData(DefectID, DefectQty, XPos, YPos, i);
        }


        //'* 제품 LotID 기준 제품 품명가져오기
        //xp_Work_sArticleByProdLotID
        //private void setDgv()
        //{
        //    //DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();     
        //    //buttonColumn.HeaderText = "Button"; 
        //    //buttonColumn.Name = "button";

        //    //dataGridView2.Columns.Add(buttonColumn);
        //    ////dataGridView2.Columns[0].Width = 50;
        //    //dataGridView2[0, 0].Value = "T/OD";
        //    //dataGridView2[0, 1].Value = "W/ID";
        //    //dataGridView2[0, 2].Value = "L";

        //    dgvCycleInfo.Rows.Add("T/OD", string.Empty, string.Empty, string.Empty, string.Empty, "초기화");
        //    dgvCycleInfo.Rows.Add("W/ID", string.Empty, string.Empty, string.Empty, string.Empty, "초기화");
        //    dgvCycleInfo.Rows.Add("L", string.Empty, string.Empty, string.Empty, string.Empty, "초기화");
        //    this.dgvCycleInfo.EditMode = DataGridViewEditMode.EditOnEnter;
        //}

        //public event DataGridViewCellEventHandler CellClick
        //{
        //    string g_Number = "규격";
        //    Frm_CMNumericKeypad CN = new Frm_CMNumericKeypad(g_Number);
        //    Frm_CMNumericKeypad.KeypadStr = splPersonID.Text;
        //    if (CN.ShowDialog() == DialogResult.OK)
        //    {
        //        splPersonID.Text = CN.tbInputText.Text;
        //    }
            
        //    if (splPersonID.Text.Length == 8)
        //    {
        //        dgvAllPerson.Rows.Clear();
        //        tabControl1.SelectedIndex = 0;
        //        tabControl1.SelectedIndex = 1;//전체사원명단 탭으로 이동
        //        visibleTF();
        //    }
        //    else
        //    {
        //        Console.WriteLine(splPersonID.Text.Length.ToString());
        //        MessageBox.Show("잘못된 사원번호입니다(사원번호 8자리 오류)");
        //    }
            
      
        //}

        //private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        //{
        //    if (e.ColumnIndex == 1 && e.RowIndex == -1)
        //    {
        //        e.PaintBackground(e.ClipBounds, false);

        //        Point pt = e.CellBounds.Location;  // where you want the bitmap in the cell

        //        int nChkBoxWidth = 40;
        //        int nChkBoxHeight = 40;
        //        int offsetx = (e.CellBounds.Width - nChkBoxWidth) / 2;
        //        int offsety = (e.CellBounds.Height - nChkBoxHeight) / 2;

        //        pt.X += offsetx;
        //        pt.Y += offsety;

        //        CheckBox cb = new CheckBox();
        //        Button bt = new Button();
        //        bt.Text = "수량";
        //        bt.BackColor = System.Drawing.Color.LightSkyBlue;
        //        //bt.Size = new Size(nChkBoxWidth, nChkBoxHeight);
        //        bt.Location = pt;
        //        //cb.CheckedChanged += new EventHandler(dataGridView1_CheckedChanged);

        //        ((DataGridView)sender).Controls.Add(bt);

        //        e.Handled = true;
        //    }
        //}

        private void cmdBarCodeScan_Click(object sender, EventArgs e)
        {
            POPUP.Frm_CMKeypad.g_Name = "바코드 스캔";
            POPUP.Frm_CMKeypad FK = new POPUP.Frm_CMKeypad();
            POPUP.Frm_CMKeypad.KeypadStr = txtBarCodeScan.Text;
            if (FK.ShowDialog() == DialogResult.OK)
            {
                txtBarCodeScan.Text = FK.tbInputText.Text;
            }
            //Initialization();
            procQuery();
            txtBarCodeScan.Text = string.Empty;
        }
        //그리드뷰 행추가               
        private void Initialization()
        {
            //dgvCycleInfo.Rows.Add("T/OD", string.Empty, string.Empty, string.Empty, string.Empty, "초기화");
            //dgvCycleInfo.Rows.Add("W/ID", string.Empty, string.Empty, string.Empty, string.Empty, "초기화");
            //dgvCycleInfo.Rows.Add("L", string.Empty, string.Empty, string.Empty, string.Empty, "초기화");
            //dgvSogulInfo.Rows.Add("소결", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "초기화");
            //dgvSogulInfo.Rows.Add("여유", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "초기화");
            //dgvSogulInfo.Rows.Add("조형", string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, "초기화");

            if (sProcessID.Equals("0401") || sProcessID.Equals("2101") || sProcessID.Equals("5101") || sProcessID.Equals("7101"))
            {
                dgvCycleInfo.Visible = true;
                dgvSogulInfo.Visible = false;
                dgvCycleInfo.Rows.Add("T/OD", "0.00", "0.00", "0.00", "0.00", "초기화");
                dgvCycleInfo.Rows.Add("W/ID", "0.00", "0.00", "0.00", "0.00", "초기화");
                dgvCycleInfo.Rows.Add("L", "0.00", "0.00", "0.00", "0.00", "초기화");
            }
            else if(sProcessID.Equals("3101")|| sProcessID.Equals("6101"))
            {
                dgvCycleInfo.Visible = false;
                dgvSogulInfo.Visible = true;
                dgvSogulInfo.Rows.Add("소결", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00","초기화");
                dgvSogulInfo.Rows.Add("여유", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00","초기화");
                dgvSogulInfo.Rows.Add("조형", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00","초기화");
            }
            
        }
        
        public void procQuery()
        {
            Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            sqlParameter.Add(WizWork_sInstID.SINSTID, txtBarCodeScan.Text);
            sqlParameter.Add(WizWork_sInstID.SPROCESSID, sProcessID);           //생산할 공정 =======setProcess 화면에서 변수값 받아와서 선택할것 수정요망
            
            ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_sInstID", sqlParameter, false);
            DataStore.Instance.CloseConnection();

            if (ds.Tables[0].Rows.Count == 1)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                lblInstID.Text = dr[WizWork_sInstID.INSTID].ToString();                                                //지시번호
                lblInstID.Tag = dr[WizWork_sInstID.INSTDETSEQ].ToString();                                              //지시번호SEQ
                //lblArticle.Text = dr[WizWork_sInstID.INSTID].ToString();                                              //재종
                lblArticle.Text = dr[WizWork_sInstID.ARTICLE].ToString();                                              //재종
                lblArticle.Tag = dr[WizWork_sInstID.ARTICLEID].ToString();                                              //재종코드 = 품명코드
                lblOrderSpec.Text = dr[WizWork_sInstID.ORDERSPEC].ToString();                                          //재종규격=품명규격

                lblOrderNO.Text = dr[WizWork_sInstID.ORDERNO].ToString();                                              //수주번호 OrderNO
                 //= dr[WizWork_sInstID.CUSTOMID].ToString();                                                           //발주업체코드
                lblKCustom.Text = dr[WizWork_sInstID.KCUSTOM].ToString();                                              //발주업체
                lblRemark.Text = dr[WizWork_sInstID.REMARK].ToString();                                                //지시 커멘트
                sOrderID = dr[WizWork_sInstID.ORDERID].ToString();                                                      //OrderID
                dgvMoldInfo.Rows.Add(dr[WizWork_sInstID.MOLDID],             //성형금형
                        dr[WizWork_sInstID.MOLDSPEC],                        //금형규격
                        dr[WizWork_sInstID.CHUNKRATE],                       //수축률 
                        dr[WizWork_sInstID.MOLDHEIGHT],                      //성형높이
                        dr[WizWork_sInstID.MOLDWEIGHT],                      //성형중량
                        dr[WizWork_sInstID.MOLDQTY]                          //성형수량
                        );
                //공정에 따른 dgvCycleInfo 및 dgvSogulInfo Visible 과 그에 따른 그리드뷰 초기화 및 기준값 조회
                if (sProcessID.Equals("2101"))// || sProcessID.Equals("5101") || sProcessID.Equals("7101"))
                {
                    dgvCycleInfo.Visible = true;
                    dgvSogulInfo.Visible = false;
                    dgvCycleInfo.Rows.Add("T/OD", dr[WizWork_sInstID.CYCLETHICKDOUTERDIAMETER].ToString(), "0.00", "0.00", "0.00", "초기화");       //*******T/OD 기준값
                    dgvCycleInfo.Rows.Add("W/ID", dr[WizWork_sInstID.CYCLEWIDTHDINNERDIAMETER].ToString(), "0.00", "0.00", "0.00", "초기화");       //*******W/ID 기준값
                    dgvCycleInfo.Rows.Add("L", dr[WizWork_sInstID.CYCLELENGTH].ToString(), "0.00", "0.00", "0.00", "초기화");                       //*******L 기준값
                }

                //공정에 따른 dgvCycleInfo 및 dgvSogulInfo Visible 과 그에 따른 그리드뷰 초기화 및 기준값 조회
                //if (sProcessID.Equals("0401") || sProcessID.Equals("2101") || sProcessID.Equals("5101") || sProcessID.Equals("7101"))
                //{
                //    dgvExCycleInfo.Visible = true;
                //    dgvSogulInfo.Visible = false;
                //    dgvExCycleInfo.Rows.Add("T/OD", dr[WizWork_sInstID.CYCLETHICKDOUTERDIAMETER].ToString(), "0.00", "0.00", "0.00", "초기화");       //*******T/OD 기준값
                //    dgvExCycleInfo.Rows.Add("W/ID", dr[WizWork_sInstID.CYCLEWIDTHDINNERDIAMETER].ToString(), "0.00", "0.00", "0.00", "초기화");       //*******W/ID 기준값
                //    dgvExCycleInfo.Rows.Add("L", dr[WizWork_sInstID.CYCLELENGTH].ToString(), "0.00", "0.00", "0.00", "초기화");                       //*******L 기준값
                //}

                else if (sProcessID.Equals("3101") || sProcessID.Equals("6101"))
                {
                    dgvCycleInfo.Visible = false;
                    dgvSogulInfo.Visible = true;
                    dgvSogulInfo.Rows.Add("소결", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "초기화");
                    dgvSogulInfo.Rows.Add("여유", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "초기화");
                    dgvSogulInfo.Rows.Add("조형", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "0.00", "초기화");
                }
                lblMsWeight.Text = "0.00";
                lblMsLength.Text = "0.00";
                lblWorkQty.Text = "0.00";
                lblInputQty.Text = "0.00";
                //

                //if (dgvCycleInfo.Visible == true)
                //{
                //    dgvCycleInfo[1, 0].Value = dr[WizWork_sInstID.CYCLETHICKDOUTERDIAMETER].ToString();    //*******T/OD 기준값
                //    dgvCycleInfo[1, 1].Value = dr[WizWork_sInstID.CYCLEWIDTHDINNERDIAMETER].ToString();    //*******W/ID 기준값
                //    dgvCycleInfo[1, 2].Value = dr[WizWork_sInstID.CYCLELENGTH].ToString();                 //*******L 기준값
                //}
                
                lblHexAngle.Text = dr[WizWork_sInstID.HEXANGLE].ToString();                                //HEX각도
                lblLead.Text = dr[WizWork_sInstID.LEAD].ToString();                                        //리드
                lblLAngle.Text = dr[WizWork_sInstID.LANGLE].ToString();                                    //L-각도             
                lblC40.Text = dr[WizWork_sInstID.C40].ToString();                                          //C40        
                lblTG.Text = dr[WizWork_sInstID.TG].ToString();                                            //TG  
                lblNalQty.Text = dr[WizWork_sInstID.NALQTY].ToString();                                     //날수
                lblWheelAngle.Text = dr[WizWork_sInstID.WHEELANGLE].ToString();                            //휠각도    
                if (sProcessID != "2101")
                {
                    lblInputQty.Text = dr[WizWork_sInstID.BEFOREWORKQTY].ToString();                           //이전공정의 생산수량
                }
                

                //Initialization();
            }
            else if (ds.Tables[0].Rows.Count == 0 && dgvCycleInfo.Rows.Count ==1)
            {
                MessageBox.Show("입력된 관리번호에 해당하는 제품이 없습니다.");
            }
                DuringGrid = false;
            //Console.WriteLine(dgvCycleInf
                dgvMoldInfo.ClearSelection();
                dgvCycleInfo.ClearSelection();   
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        //public void procSave_1()
        //{
        //    dgvCycleInfo.EndEdit();
        //    dgvCycleInfo.CommitEdit(DataGridViewDataErrorContexts.CurrentCellChange);
        //    //dgvCycleInfo.CurrentCell = null;




        //    // 저장하시겠습니까? 라는 메시지에 YES를 누른다면
        //    if (MessageBox.Show(Resources.MSG_SaveQuestion, Resources.MSG_CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //    {
        //        DataTable dt = dgvExCycleInfo.DataSource as DataTable;
        //        //List<object> successedDeletedKey = new List<object>();
        //        List<string> lstFaildItem = new List<string>();
        //        int lastUpdateIndex = 0;





        //        // 추가사항들 DB로 전송하기.

        //        int index = -1; 

        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            index++;
        //            switch (dr.RowState)
        //            {
        //                //case DataRowState.Added:
        //                //    {
        //                //        if (procSaveAdded(dr) == true)
        //                //        {
        //                //            dr.AcceptChanges();
        //                //            lastUpdateIndex = index;
        //                //        }
        //                //        else
        //                //        {
        //                //            break;
        //                //        }
        //                //    }
        //                //    break;
        //                case DataRowState.Modified:    // 수정.
        //                    {
        //                        if (procSaveModified(dr) == true)
        //                        {
        //                            dr.AcceptChanges();
        //                            lastUpdateIndex = index;
        //                        }
        //                        else
        //                        {
        //                            break;
        //                        }
        //                    }
        //                    break;



                       

        //                default:
        //                    {
        //                    }
        //                    break;
        //            }










        //        }   // end   foreach (DataRow dr in dt.Rows)





        //        if (lstFaildItem.Count > 0)
        //        {
        //            StringBuilder sb = new StringBuilder();
        //            sb.Append("다음 항목들을 DB에 저장하는데 실패했습니다.");
        //            sb.Append("\r\n");

        //            foreach (string pk in lstFaildItem)
        //            {
        //                sb.Append(pk);
        //                sb.Append("\r\n");
        //            }

        //            MessageBox.Show(sb.ToString(), Resources.MSG_CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            DataStore.Instance.CloseConnection();
        //            Globals.ShowStatusMessage("저장에 실패했습니다.");
        //        }
        //        //데이터를 다시 가져온다.
        //        procQuery();

        //        Globals.ShowStatusMessage("저장이 완료되었습니다.");







        //    }
        //}



        //생산수량등을 등록
        private void iRegSuryang()
        {
            Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();
            sqlParameter1.Add(WizWork_iWkResult.JOBID, JobID);              //JobID 입력안함. 지금 프로시저 수행 후 만들어짐~~

            sqlParameter1.Add(WizWork_iWkResult.INSTID, lblInstID.Text); //작업지시번호입력
            sqlParameter1.Add(WizWork_iWkResult.INSTDETSEQ, Convert.ToInt32(lblInstID.Tag)); //작업지시순위??
            sqlParameter1.Add(WizWork_iWkResult.LABELID, lblInstID.Text); //작업지시번호로 입력함 ----------컨펌됨
            sqlParameter1.Add(WizWork_iWkResult.LABELGUBUN, "2"); //작업지시번호 구분? 2로 고정함
            sqlParameter1.Add(WizWork_iWkResult.PROCESSID, sProcessID); //선택되있는 sProcessID(setProcess에서 선택한)

            sqlParameter1.Add(WizWork_iWkResult.MACHINEID, sMachineID); //선택되있는 sMachineID(setProcess에서 선택한)
            sqlParameter1.Add(WizWork_iWkResult.SCANDATE, dtpOrderDateTo.Value.ToString("yyyyMMdd")); //년월일
            sqlParameter1.Add(WizWork_iWkResult.SCANTIME, dtpOrderTimeTo.Value.ToString("HHmm")); //시분초
            sqlParameter1.Add(WizWork_iWkResult.WORKSTARTDATE, dtpOrderDateFrom.Value.ToString("yyyyMMdd")); //작업시작날짜
            sqlParameter1.Add(WizWork_iWkResult.WORKSTARTTIME, dtpOrderTimeFrom.Value.ToString("HHmm")); //작업시작시간

            sqlParameter1.Add(WizWork_iWkResult.WORKENDDATE, dtpOrderDateTo.Value.ToString("yyyyMMdd")); //작업종료날짜
            sqlParameter1.Add(WizWork_iWkResult.WORKENDTIME, dtpOrderTimeTo.Value.ToString("HHmm")); //작업종료시간
            sqlParameter1.Add(WizWork_iWkResult.ARTICLEID, lblArticle.Tag.ToString()); //품명코드=재종코드
            sqlParameter1.Add(WizWork_iWkResult.INPUTQTY, Convert.ToDouble(lblInputQty.Text)); //투입수량
            sqlParameter1.Add(WizWork_iWkResult.WORKQTY, Convert.ToDouble(lblWorkQty.Text)); //생산수량

            sqlParameter1.Add(WizWork_iWkResult.MSWEIGHT, Convert.ToDouble(lblMsWeight.Text)); //MS중량
            sqlParameter1.Add(WizWork_iWkResult.MSLENGTH, Convert.ToDouble(lblMsLength.Text)); //MS측정치
            sqlParameter1.Add(WizWork_iWkResult.COMMENTS, lblRemark.Text);//지시커멘트
            sqlParameter1.Add(WizWork_iWkResult.JOBGBN, "1");//작업구분 1:정상,2:무작업,3:재작업 work_u폼에서는 1번 정상으로 처리
            
            sqlParameter1.Add(WizWork_iWkResult.REWORKOLDYN, "N"); //재작업여부 NO
            sqlParameter1.Add(WizWork_iWkResult.REWORKLINKPRODID, "");//????????????????????????
            sqlParameter1.Add(WizWork_iWkResult.WORKPERSONID, sPersonID); //작업자ID
            sqlParameter1.Add(WizWork_iWkResult.TEAMID, sTeam);//작업조ID
            
            sqlParameter1.Add(WizWork_iWkResult.NOREWORKCODE, "");//무작업코드_정상작업이므로 NULL입력,, 무재작업코드
            sqlParameter1.Add(WizWork_iWkResult.NOREWORKREASON, "");//무작업사유_정상작업이므로 NULL입력

            sqlParameter1.Add(WizWork_iWkResult.WDNO, "");// 대차번호
            sqlParameter1.Add(WizWork_iWkResult.WDID, "");// 대차ID
            sqlParameter1.Add(WizWork_iWkResult.WDQTY, "0");// 대차장임량
            sqlParameter1.Add(WizWork_iWkResult.CREATEUSERID, sUserID);// 작업자

            //ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_iWkResult", sqlParameter, false);
            //DataStore.Instance.CloseConnection();
            





            string[] result = DataStore.Instance.ExecuteTranProcedure("xp_WizWork_iWkResult", sqlParameter1, true);
            DataStore.Instance.CloseConnection();

            MessageBoxInvalidation(result);

            //JobID = Convert.ToDecimal(DataStore.Instance.ExecuteQuery("select max(JobID) from wk_Result where InstID = '" + lblInstID.Text + "'", false)[1]);//작업지시번호로 iWkResult에서 등록한 JobID를 조회한다.
            Console.WriteLine(JobID);
        }

        private bool procSaveModified(DataRow row)
        {
            //하위품 등록
            //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            
            //for (int i = 0; i < dgvGpLotSuryang.Rows.Count; i++)
            //{
                Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                sqlParameter2.Add(WizWork_iWkResultArticleChild.JOBID, JobID);//iWkResult 프로시저로 만들어진 JoibID
                sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDLABELID, row[0].ToString()); //GP LOT번호 &&바코드로 받아옴
                sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDLABELGUBUN, "2");//????
                sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDINPUTQTY, row[1].ToString());//GP LOT별 수량
                sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDARTICLEID, lblArticle.Tag.ToString());//재종코드
                sqlParameter2.Add(WizWork_iWkResultArticleChild.REWORKOLDYN, "N");//
                sqlParameter2.Add(WizWork_iWkResultArticleChild.REWORKLINKCHILDPRODID, "");//
                sqlParameter2.Add(WizWork_iWkResultArticleChild.CREATEUSERID, sUserID);//작업자

                //ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_iWkResultArticleChild", sqlParameter, false);
                //DataStore.Instance.CloseConnection();

                string[] result = DataStore.Instance.ExecuteTranProcedure("xp_WizWork_iWkResultArticleChild", sqlParameter2, true);

                if (result[0].Equals(Resources.success) == false)
                {
                    return false;
                }

                return true;
            //DataStore.Instance.CloseConnection();

                //MessageBoxInvalidation(result);
            //}
        }

        private void iRegGPLot()
        {
            //하위품 등록
            //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            for (int i = 0; i < dgvGpLotSuryang.Rows.Count; i++)
            {
                Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                sqlParameter2.Add(WizWork_iWkResultArticleChild.JOBID, JobID);//iWkResult 프로시저로 만들어진 JoibID
                sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDLABELID, dgvGpLotSuryang.Rows[i].Cells[0].Value.ToString()); //GP LOT번호 &&바코드로 받아옴
                sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDLABELGUBUN, "2");//????
                sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDINPUTQTY, dgvGpLotSuryang.Rows[i].Cells[1].Value.ToString());//GP LOT별 수량
                sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDARTICLEID, lblArticle.Tag.ToString());//재종코드
                sqlParameter2.Add(WizWork_iWkResultArticleChild.REWORKOLDYN, "N");//
                sqlParameter2.Add(WizWork_iWkResultArticleChild.REWORKLINKCHILDPRODID, "");//
                sqlParameter2.Add(WizWork_iWkResultArticleChild.CREATEUSERID, sUserID);//작업자

                //ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_iWkResultArticleChild", sqlParameter, false);
                //DataStore.Instance.CloseConnection();

                string[] result = DataStore.Instance.ExecuteTranProcedure("xp_WizWork_iWkResultArticleChild", sqlParameter2, true);
                DataStore.Instance.CloseConnection();

                MessageBoxInvalidation(result);
            }
        }

        private void iRegCycle()
        {
            //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
            for (int j = 0; j < dgvCycleInfo.Rows.Count; j++)
            ///for (int i = 2; i < dgvCycleInfo.Rows[0].Cells.Count - 1; i++)
            {
                //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                //for (int j = 0; j < dgvCycleInfo.Rows.Count; j++)
                for (int i = 2; i < dgvCycleInfo.Rows[0].Cells.Count - 1; i++)
                
                    //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                    if (dgvCycleInfo.Rows[j].Cells[i].Value != null)
                    {
                        Dictionary<string, object> sqlParameter3 = new Dictionary<string, object>();      
                        sqlParameter3.Add(WizWork_iSamleValue.JOBID, JobID);//iWkResult 프로시저로 만들어진 JobID
                        sqlParameter3.Add(WizWork_iSamleValue.SSAMPLEGBN, j + 1);//1:TOD 측정, 2:WID측정,3:L측정,4:소결측정*******이 if문에서는 소결값 측정안함
                        sqlParameter3.Add(WizWork_iSamleValue.NSAMPLESEQ, i - 1);//측정값번호
                        sqlParameter3.Add(WizWork_iSamleValue.NSAMPLEVALUE, Convert.ToDouble(dgvCycleInfo.Rows[j].Cells[i].Value));//측정값
                        sqlParameter3.Add(WizWork_iSamleValue.NCOMPOSURE, Convert.ToDouble("0.00"));//소결의 여유값
                        sqlParameter3.Add(WizWork_iSamleValue.NSHAPEVALUE, Convert.ToDouble("0.00"));//소결공정일 경유 조형 결과값, 계산결과
                        sqlParameter3.Add(WizWork_iSamleValue.CREATEUSERID, sUserID);//작업자

                        //ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_iSamleValue", sqlParameter, false);
                        //DataStore.Instance.CloseConnection();
                        
                        string[] result = DataStore.Instance.ExecuteTranProcedure("xp_WizWork_iSamleValue", sqlParameter3, true);
                        DataStore.Instance.CloseConnection();

                        MessageBoxInvalidation(result);
                        
                    }
                }
            
            //DataStore.Instance.CloseConnection();
        }

        private void iRegSogul()
        {
            
            for (int i = 0; i < dgvSogulInfo.Rows.Count; i++)
            {
                Dictionary<string, object> sqlParameter4 = new Dictionary<string, object>();
                for (int j = 1; j < dgvSogulInfo.Rows[0].Cells.Count - 1; j++)
                {
                    if (dgvSogulInfo.Rows[i].Cells[j].Value != null)
                    {
                        sqlParameter4.Add(WizWork_iSamleValue.JOBID, JobID);//iWkResult 프로시저로 만들어진 JobID
                        sqlParameter4.Add(WizWork_iSamleValue.SSAMPLEGBN, "4");//1:TOD 측정, 2:WID측정,3:L측정,4:소결측정*******이 if문에서는 소결값만 측정함 
                        sqlParameter4.Add(WizWork_iSamleValue.NSAMPLESEQ, j);//측정값번호
                        if (i == 0)//소결 측정값 입력
                        {
                            sqlParameter4.Add(WizWork_iSamleValue.NSAMPLEVALUE, dgvCycleInfo.Rows[i].Cells[j].Value.ToString());//소결 측정값
                            sqlParameter4.Add(WizWork_iSamleValue.NCOMPOSURE, Convert.ToDouble("0.00"));//소결의 여유값
                            sqlParameter4.Add(WizWork_iSamleValue.NSHAPEVALUE, Convert.ToDouble("0.00"));//소결공정일 경유 조형 결과값, 계산결과
                        }
                        else if (i == 1)//여유 측정값 입력
                        {
                            sqlParameter4.Add(WizWork_iSamleValue.NSAMPLEVALUE, Convert.ToDouble("0.00"));//소결 측정값
                            sqlParameter4.Add(WizWork_iSamleValue.NCOMPOSURE, dgvCycleInfo.Rows[i].Cells[j].Value.ToString());//소결의 여유값
                            sqlParameter4.Add(WizWork_iSamleValue.NSHAPEVALUE, Convert.ToDouble("0.00"));//소결공정일 경유 조형 결과값, 계산결과
                        }
                        else if (i == 2)//조형 결과값 입력
                        {
                            sqlParameter4.Add(WizWork_iSamleValue.NSAMPLEVALUE, Convert.ToDouble("0.00"));//소결 측정값
                            sqlParameter4.Add(WizWork_iSamleValue.NCOMPOSURE, Convert.ToDouble("0.00"));//소결의 여유값
                            sqlParameter4.Add(WizWork_iSamleValue.NSHAPEVALUE, dgvCycleInfo.Rows[i].Cells[j].Value.ToString());//소결공정일 경유 조형 결과값, 계산결과
                        }
                        sqlParameter4.Add(WizWork_iSamleValue.CREATEUSERID, sUserID);//작업자

                        //ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_iSamleValue", sqlParameter, false);
                        //DataStore.Instance.CloseConnection();

                        string[] result = DataStore.Instance.ExecuteTranProcedure("xp_WizWork_iSamleValue", sqlParameter4, true);
                        DataStore.Instance.CloseConnection();

                        MessageBoxInvalidation(result);
                    }
                }
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            //procSave_1();
            //procSave();
            iTrans();



            //CmdSaveClick();


            //iRegSuryang();
            //iRegGPLot();
            //if (dgvCycleInfo.Visible == true)
            //{
            //    iRegCycle();
            //}
            //else if(dgvSogulInfo.Visible == true)
            //{
            //    iRegSogul();
            //}

        }

        public void procSave()
        {

            if (lblInstID.Text == string.Empty)
                //dgvClosedMonths.DataSource == null)
            {
                MessageBox.Show("조회를 해야 저장할 수 있습니다");
                return;
            }
            //dgvClosedMonths.EndEdit();

            if (MessageBox.Show(Resources.MSG_SaveQuestion, Resources.MSG_CAPTION_CONFIRM, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
				Frm_Cmn_Message dlg = new Frm_Cmn_Message("저장중입니다.");
				dlg.Show();
				dlg.Refresh();


				try
				{
                    DataTable source = dgvCycleInfo.DataSource as DataTable;
					List<string> failedPKValues = new List<string>();

					foreach (DataRow row in source.Rows)
					{
						switch (row.RowState)
						{
							case DataRowState.Modified:
                                if (procSaveModified(row) == true)
                                {
                                    row.AcceptChanges();
                                }
                                else
                                {
                                    failedPKValues.Add(Methods.GetDataString(row[com_MonthClose.YYYYMM]));
                                }
                                //if (row[com_MonthClose.CLOSEDATE].ToString().Equals("") == false)
                                //{
                                //    if (Methods.IsDate(row[com_MonthClose.CLOSEDATE].ToString()) == false)
                                //    {
                                //        MessageBox.Show("날짜일자가 유효한 일자가 아닙니다");
                                //        dlg.Close();
                                //        return;
                                //    }
                                //}
								//선택필드가 체크되있을 경우
								break;
							default:
								break;

						}
					}
					if (failedPKValues.Count > 0)
					{
						StringBuilder sb = new StringBuilder();
						sb.Append("다음 항목들을 DB에 저장하는데 실패했습니다.");
						sb.Append("\r\n");

						foreach (string pk in failedPKValues)
						{
							sb.Append(pk);
							sb.Append("\r\n");
						}

						MessageBox.Show(sb.ToString(), Resources.MSG_CAPTION_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
						DataStore.Instance.CloseConnection();
						//procQuery();
						failedPKValues.Clear();
						Globals.ShowStatusMessage("저장에 실패하였습니다");
						return;
					}
				}
				catch (Exception excpt)
				{
					MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
				}
				finally
				{
					DataStore.Instance.CloseConnection();
					if (dlg != null && dlg.IsDisposed == false)
					{
						dlg.Close();
					}
				}				
            }

            //procQuery();
            Globals.ShowStatusMessage("저장되었습니다.");
       
        }


        private void cmdsetProcess_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("현재화면의 작업중인 내용들은 모두 삭제됩니다.\r\n그래도 계속 진행하시겠습니까?", "[확인]", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
                SendEvent();
                //Frm_tprc_setProcess set_Pro = new Frm_tprc_setProcess();
                ////set_Pro.Show();
                //set_Pro.ShowDialog();
            }
        }

        private void cmdWorkDefect_Click(object sender, EventArgs e)
        {
            Frm_SelectReason SelRea = new Frm_SelectReason();
            SelRea.WriteTextEvent += new Frm_SelectReason.TextEventHandler(GetData);
            SelRea.Show();
        }
        void GetData(string[] DefectID, string[] DefectQty, string[] XPos, string[] YPos,int i)
        {
            sDefectID = new string[i];
            sDefectQty= new string[i];
            sXPos= new string[i];
            sYPos = new string[i];
            for (int j = 0; j < i; j++)//빈값도 가져옴 그러므로 DefectQty가 0인 애들은 빈값임.
            {
                //if(DefectID[j].ToString() != string.Empty)
                ////if (j == i)
                //{
                    sDefectID[j] = DefectID[j];
                    sDefectQty[j] = DefectQty[j];
                    sXPos[j] = XPos[j];
                    sYPos[j] = YPos[j];
                    //Console.WriteLine(sDefectID[j].ToString());
                    //Console.WriteLine(sDefectQty[j].ToString());
                    //Console.WriteLine(sXPos[j].ToString());
                    //Console.WriteLine(sYPos[j].ToString());
                    //break;
                //}
            }
            //this.textbox1.text = txt;
        }

        private void cmdMoldList_Click(object sender, EventArgs e)
        {
            Frm_tprc_Mold_Q Mq = new Frm_tprc_Mold_Q();
            Mq.Show();
        }

        private void btnGpLotClr_Click(object sender, EventArgs e)
        {
            dgvGpLotSuryang.Rows.Clear();
            txtBarCodeScan.Select();
        }
        //전체 초기화
        private void cmdClear_Click(object sender, EventArgs e)
        {
            AllClear();
        }
        private void AllClear()
        {
            //txtBarCodeScan.Text = "";
            lblInstID.Text = string.Empty;
            lblOrderNO.Text = string.Empty;
            lblKCustom.Text = string.Empty;
            lblArticle.Text = string.Empty;
            lblOrderSpec.Text = string.Empty;
            lblRemark.Text = string.Empty;
            //
            dgvMoldInfo.Rows.Clear();
            dgvGpLotSuryang.Rows.Clear();
            if (dgvCycleInfo.Visible == true)
            {
                for (int i = 2; i < 5; i++)
                {
                    //dgvCycleInfo.Rows[0].Cells[i].Value = Convert.ToDouble("0.00");
                    //dgvCycleInfo.Rows[1].Cells[i].Value = Convert.ToDouble("0.00");
                    //dgvCycleInfo.Rows[2].Cells[i].Value = Convert.ToDouble("0.00");
                    dgvCycleInfo.Rows[0].Cells[i].Value = "0.00";
                    dgvCycleInfo.Rows[1].Cells[i].Value = "0.00";
                    dgvCycleInfo.Rows[2].Cells[i].Value = "0.00";
                }
            }
            else if(dgvSogulInfo.Visible == true)
            {
                for (int i = 1; i < 11; i++)
                {
                    //dgvSogulInfo.Rows[0].Cells[i].Value = Convert.ToDouble("0.00");
                    //dgvSogulInfo.Rows[1].Cells[i].Value = Convert.ToDouble("0.00");
                    dgvSogulInfo.Rows[0].Cells[i].Value = "0.00";
                    dgvSogulInfo.Rows[1].Cells[i].Value = "0.00";
                }
            }
            //
            //lblMsWeight.Text = string.Empty;
            //lblMsLength.Text = string.Empty;
            //lblWorkQty.Text = string.Empty;
            //lblInputQty.Text = string.Empty;

            lblMsWeight.Text = "0.00";
            lblMsLength.Text = "0.00";
            lblWorkQty.Text = "0.00";
            lblInputQty.Text = "0.00";
            //
            lblHexAngle.Text = "0.00";
            lblLead.Text = "0.00";
            lblLAngle.Text = "0.00";
            lblC40.Text = "0.00";
            lblTG.Text = "0.00";
            lblNalQty.Text = "0.00";
            lblWheelAngle.Text = "0.00";
            //
            

            txtBarCodeScan.Select();
        }

        private void txtBarCodeScan_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter && txtBarCodeScan.Text.Length > 0)
            {
                string a = txtBarCodeScan.Text.Trim();
                a = a.ToUpper();
                //if (a.Equals(null))
                if(a.Length==0)
                {
                    a = "";
                }
                if (a.Substring(0,1).Equals("P"))//첫글자가 P인 텍스트를 스캔한다. 작업지시번호
                {
                    procQuery();
                }
                else if (a.Substring(0, 1).Equals("G"))//첫글자가 G인 텍스트를 스캔한다.
                {
                    int b = dgvGpLotSuryang.Rows.Count;
                    int c = 0;
                    for (int i = 0; i < b; i++)
                    {
                        if (a.Equals(dgvGpLotSuryang.Rows[i].Cells[0].Value))//입력되있는 값과 똑같은 값을 스캔했을 경우 멈춤
                        {
                            AutoClosingMessageBox.Show("동일한 GP LOT 번호가 있습니다. 다시 스캔해주십시오.", "GP LOT 오류", 2000);
                            c=1;
                            break;
                        }
                    }
                    if (c.Equals(0))
                    {
                        dgvGpLotSuryang.Rows.Add(a, string.Empty);
                    }
                   
                    
                }
                else//첫글자가 P,G가 아닌 경우 에러메시지 발생
                {
                    AutoClosingMessageBox.Show("작업지시번호가 잘못되었습니다. 다시 스캔해주십시오.", "작업지시번호 오류", 2000);
                    procQuery();///////////////위 조건의 데이터가 없어서 임시로 해놓은거임 막아야함 결국
                }
                txtBarCodeScan.Text = string.Empty;
            }
        }

        private void btnSuRyang_Click(object sender, EventArgs e)
        {
            int b = dgvGpLotSuryang.Rows.Count;
            Console.WriteLine(b);
            if (b.Equals(0))
            {
                AutoClosingMessageBox.Show("GP LOT번호가 스캔 되지 않았습니다. 다시 스캔해주십시오.", "GP LOT 스캔 오류", 2000);
            }
            else
            {
                POPUP.Frm_CMNumericKeypad.g_Name = "수량";
                string message = "수량을 재입력하려면 초기화 버튼을 클릭해주세요.";
                POPUP.Frm_CMNumericKeypad FK = new POPUP.Frm_CMNumericKeypad();
                for (int i = 0; i < b; i++)
                {
                    if (String.IsNullOrEmpty(dgvGpLotSuryang.Rows[i].Cells[1].Value.ToString()) || Convert.ToInt32(dgvGpLotSuryang.Rows[i].Cells[1].Value).Equals(0))//i번째 측정값에 값이 없다. 그러므로 키보드 호출 후 입력한다. 취소버튼 클릭 시 멈춘다. 
                    {
                        if (FK.ShowDialog() == DialogResult.OK)
                        {
                            FK.tbInputText.SelectAll();
                            if (Convert.ToInt32(FK.tbInputText.Text).Equals(0))
                            { dgvGpLotSuryang.Rows[i].Cells[1].Value = "0.00"; }
                            else
                            { 
                                dgvGpLotSuryang.Rows[i].Cells[1].Value = FK.tbInputText.Text.ToString();
                                if (sProcessID == "2101")
                                {
                                    lblInputQty.Text = (Convert.ToDouble(lblInputQty.Text) + Convert.ToDouble(dgvGpLotSuryang.Rows[i].Cells[1].Value)).ToString();
                                }
                            }
                        }
                        else
                        {
                            dgvGpLotSuryang.Rows[i].Cells[1].Value = "0.00";
                            break;
                        }
                    }
                    else
                    {
                        if (i == b-1) { AutoClosingMessageBox.Show(message, "수량" + "을 입력할 수 없습니다.", 1500); }
                    }
                }
            }
        }
        //dgvCycleInfo 입력 및 초기화 
        private void dgvCycleInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //T/OD 입력
            if(e.RowIndex.Equals(0) && e.ColumnIndex.Equals(0))
                //(dgvCycleInfo.Rows[0].Cells[0].Selected)
            {
                SetDgvCyc("T/OD");
            }
            //W/ID 입력
            else if(e.RowIndex.Equals(1) && e.ColumnIndex.Equals(0))
                //(dgvCycleInfo.Rows[1].Cells[0].Selected)
            {
                SetDgvCyc("W/ID");
            }
            //L 입력
            else if (e.RowIndex.Equals(2) && e.ColumnIndex.Equals(0))
                //(dgvCycleInfo.Rows[2].Cells[0].Selected)
            {
                SetDgvCyc("L");
            }
            //T/OD 초기화
            if(e.RowIndex.Equals(0) && e.ColumnIndex.Equals(5))
                //(dgvCycleInfo.Rows[0].Cells[5].Selected)
            {
                for (int i = 2; i < 5; i++)
                {
                    //dgvCycleInfo.Rows[0].Cells[i].Value = string.Empty;
                    dgvCycleInfo.Rows[0].Cells[i].Value = "0.00";
                }
            }
            //W/ID 초기화
            else if(e.RowIndex.Equals(1) && e.ColumnIndex.Equals(5))
                //(dgvCycleInfo.Rows[1].Cells[5].Selected)
            {
                for (int i = 2; i < 5; i++)
                {
                    //dgvCycleInfo.Rows[1].Cells[i].Value = string.Empty;
                    dgvCycleInfo.Rows[1].Cells[i].Value = "0.00";
                }
            }
            //L 초기화
            else if (e.RowIndex.Equals(2) && e.ColumnIndex.Equals(5))
                //(dgvCycleInfo.Rows[2].Cells[5].Selected)
            {
                for (int i = 2; i < 5; i++)
                {
                    //dgvCycleInfo.Rows[2].Cells[i].Value = string.Empty;
                    dgvCycleInfo.Rows[2].Cells[i].Value = "0.00";
                }
            }
        }
        private void SetDgvCyc(string G_Number)
        {
            int k = 0; // 입력여부 확인용 변수
            string message = "측정값을 재입력하려면 초기화 버튼을 클릭해주세요.";
            POPUP.Frm_CMNumericKeypad.g_Name = G_Number;
            POPUP.Frm_CMNumericKeypad FK = new POPUP.Frm_CMNumericKeypad();
            int j=0;
            if (G_Number.Equals("T/OD") || G_Number.Equals("소결"))
            { j = 0; }
            else if (G_Number.Equals("W/ID") || G_Number.Equals("여유"))
            { j = 1; }
            else if (G_Number.Equals("L"))
            { j = 2; }

            if (G_Number.Equals("T/OD") || G_Number.Equals("W/ID") || G_Number.Equals("L"))
            {
                for (int i = 2; i < 5; i++)
                {
                    //Console.WriteLine(dgvCycleInfo.Rows[j].Cells[i].Value.ToString() + "@@@@@@@@@@@@@@@@@@@@");
                    //Console.WriteLine(Convert.ToDouble(dgvCycleInfo.Rows[j].Cells[i].Value.ToString()) + "@@@@@@@@@@@@@@@@@@@@");

                    //Console.WriteLine(dgvCycleInfo.Rows[j].Cells[i].Value + "@@@@@@@@@@@@@@@@@@@@");
                    //if (Convert.ToDouble(dgvCycleInfo.Rows[i].Cells[1].Value.ToString()).Equals(0) || dgvCycleInfo.Rows[j].Cells[i].Value.Equals(string.Empty))
                   // if (dgvCycleInfo.Rows[j].Cells[i].Value.ToString()=="0.00" || dgvCycleInfo.Rows[j].Cells[i].Value.ToString().Equals(string.Empty))
                    //    if (dgvExCycleInfo.Rows[j].Cells[i].Value.ToString() == "0.00" || dgvCycleInfo.Rows[j].Cells[i].Value.ToString().Equals(string.Empty))



                    if (Convert.ToDouble(dgvCycleInfo.Rows[j].Cells[i].Value.ToString()).Equals(0) || dgvCycleInfo.Rows[j].Cells[i].Value.Equals(string.Empty))
                    //if (dgvCycleInfo.Rows[j].Cells[i].Value.ToString().Equals(0.00) || dgvCycleInfo.Rows[j].Cells[i].Value.ToString().Equals(string.Empty))
                    //i번째 측정값에 값이 없다. 그러므로 키보드 호출 후 입력한다. 취소버튼 클릭 시 멈춘다.
                    //dgvCycleInfo.Rows[0].Cells[i].Value == null || 
                    {
                        if (FK.ShowDialog() == DialogResult.OK)
                        {
                            FK.tbInputText.SelectAll();
                            Console.WriteLine("@@@@@@@@@@@@@@@@@@@22"+FK.tbInputText.Text.ToString() + "33@@@@@@@@@@@@@@@@@@@");
                            if (FK.tbInputText.Text.Length == 0 || Convert.ToDouble(FK.tbInputText.Text).Equals(0.0)// || FK.tbInputText.Text.ToString() == "" 
                                || FK.tbInputText.Text.ToString() == string.Empty)
                                //|| FK.tbInputText.Text.ToString()==null)
                                { dgvCycleInfo.Rows[j].Cells[i].Value = "0.00"; }
                                //{ dgvExCycleInfo.Rows[j].Cells[i].Value = "0.00"; }
                            else
                            {
                                dgvCycleInfo.Rows[j].Cells[i].Value = FK.tbInputText.Text.ToString();//키패드 숫자 입력
                                //dgvExCycleInfo.Rows[j].Cells[i].Value = FK.tbInputText.Text.ToString();//키패드 숫자 입력
                            }
                            k = k + 1;
                        }
                        else
                        {
                            dgvCycleInfo.Rows[i].Cells[i].Value = "0.00";
                            //dgvExCycleInfo.Rows[i].Cells[i].Value = "0.00";
                            break;//취소 눌렀을때 숫자키패드 끔
                        }
                    }
                    else
                    {
                        if (dgvCycleInfo.Rows[j].Cells[2].Value.ToString() != string.Empty && dgvCycleInfo.Rows[j].Cells[2].Value.ToString() != "0.00" &&
                            dgvCycleInfo.Rows[j].Cells[3].Value.ToString() != string.Empty && dgvCycleInfo.Rows[j].Cells[3].Value.ToString() != "0.00" &&
                            dgvCycleInfo.Rows[j].Cells[4].Value.ToString() != string.Empty && dgvCycleInfo.Rows[j].Cells[4].Value.ToString() != "0.00" &&
                            k < 1)//전체 측정값이 입력되어 있을때 메시지박스 호출
                        //if (dgvExCycleInfo.Rows[j].Cells[2].Value.ToString() != string.Empty && dgvExCycleInfo.Rows[j].Cells[2].Value.ToString() != "0.00" &&
                        //dgvExCycleInfo.Rows[j].Cells[3].Value.ToString() != string.Empty && dgvExCycleInfo.Rows[j].Cells[3].Value.ToString() != "0.00" &&
                        //dgvExCycleInfo.Rows[j].Cells[4].Value.ToString() != string.Empty && dgvExCycleInfo.Rows[j].Cells[4].Value.ToString() != "0.00" &&
                        //k < 1)//전체 측정값이 입력되어 있을때 메시지박스 호출
                        {
                            AutoClosingMessageBox.Show(message, G_Number + "을 입력할 수 없습니다.", 1500);
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 1; i < 11; i++)
                {
                    //Console.WriteLine(Convert.ToInt32(dgvSogulInfo.Rows[j].Cells[i].Value)+"@@@@@@@@@@@@@@@@@@@@");
                    //Console.WriteLine(dgvSogulInfo.Rows[j].Cells[i].Value + "@@@@@@@@@@@@@@@@@@@@");

                    if (dgvSogulInfo.Rows[j].Cells[i].Value.ToString() == "0.00" || dgvSogulInfo.Rows[j].Cells[i].Value.Equals(string.Empty))
                    //if (Convert.ToInt32(dgvSogulInfo.Rows[j].Cells[i].Value).Equals(0) || dgvSogulInfo.Rows[j].Cells[i].Value.Equals(string.Empty))
                    //i번째 측정값에 값이 없다. 그러므로 키보드 호출 후 입력한다. 취소버튼 클릭 시 멈춘다.
                    //dgvCycleInfo.Rows[0].Cells[i].Value == null || 
                    {
                        if (FK.ShowDialog() == DialogResult.OK)
                        {
                            FK.tbInputText.SelectAll();
                            string a = FK.tbInputText.Text;
                            if(FK.tbInputText.Text.Length == 0 || Convert.ToDouble(FK.tbInputText.Text).Equals(0.0)// || FK.tbInputText.Text.ToString() == "" 
                                || FK.tbInputText.Text.ToString() == string.Empty)

                            //if (Convert.ToDouble(a).Equals(0.0) || 
                            //    a == ""
                            //    || a == string.Empty
                            //    || a == null)// || FK.tbInputText.Text.ToString() == null)
                            //    //if (Convert.ToDouble(FK.tbInputText.Text).Equals(0.0) || FK.tbInputText.Text.ToString() == ""
                                //|| FK.tbInputText.Text.ToString() == string.Empty || FK.tbInputText.Text.ToString() == null)
                            { dgvSogulInfo.Rows[j].Cells[i].Value = "0.00"; }
                            else
                            {
                                dgvSogulInfo.Rows[j].Cells[i].Value = FK.tbInputText.Text.ToString();//키패드 숫자 입력
                            } 
                            k = k + 1;
                        }
                        else
                        {
                            dgvSogulInfo.Rows[j].Cells[i].Value = "0.00";
                            break;//취소 눌렀을때 숫자키패드 끔
                        }
                    }
                    else
                    {
                        if (dgvSogulInfo.Rows[j].Cells[1].Value.ToString() != string.Empty && dgvSogulInfo.Rows[j].Cells[2].Value.ToString() != string.Empty
                            && dgvSogulInfo.Rows[j].Cells[3].Value.ToString() != string.Empty && dgvSogulInfo.Rows[j].Cells[4].Value.ToString() != string.Empty
                            && dgvSogulInfo.Rows[j].Cells[5].Value.ToString() != string.Empty && dgvSogulInfo.Rows[j].Cells[6].Value.ToString() != string.Empty
                            && dgvSogulInfo.Rows[j].Cells[7].Value.ToString() != string.Empty && dgvSogulInfo.Rows[j].Cells[8].Value.ToString() != string.Empty
                            && dgvSogulInfo.Rows[j].Cells[9].Value.ToString() != string.Empty && dgvSogulInfo.Rows[j].Cells[10].Value.ToString() != string.Empty
                            && dgvSogulInfo.Rows[j].Cells[1].Value.ToString() != "0.00" && dgvSogulInfo.Rows[j].Cells[2].Value.ToString() != "0.00"
                            && dgvSogulInfo.Rows[j].Cells[3].Value.ToString() != "0.00" && dgvSogulInfo.Rows[j].Cells[4].Value.ToString() != "0.00"
                            && dgvSogulInfo.Rows[j].Cells[5].Value.ToString() != "0.00" && dgvSogulInfo.Rows[j].Cells[6].Value.ToString() != "0.00"
                            && dgvSogulInfo.Rows[j].Cells[7].Value.ToString() != "0.00" && dgvSogulInfo.Rows[j].Cells[8].Value.ToString() != "0.00"
                            && dgvSogulInfo.Rows[j].Cells[9].Value.ToString() != "0.00" && dgvSogulInfo.Rows[j].Cells[10].Value.ToString() != "0.00" 
                            && k < 1)//전체 측정값이 입력되어 있을때 메시지박스 호출
                        {
                            AutoClosingMessageBox.Show(message, G_Number + "을 입력할 수 없습니다.", 1500);
                            break;
                        }
                    }
                }
            }
        }

        private void dgvSogulInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        //    if (dgvMoldInfo.Rows[0].State != )
        //    {
                //소결 입력
                
                
                if(e.RowIndex.Equals(0) && e.ColumnIndex.Equals(0))
                {
                    SetDgvCyc("소결");
                }
                else if (e.RowIndex.Equals(1) && e.ColumnIndex.Equals(0))
                {
                    SetDgvCyc("여유");
                }
                else if (e.RowIndex.Equals(2) && e.ColumnIndex.Equals(0))
                {
                    for (int j = 1; j < 11; j++)
                    {

                        double Suchuk = 0.00;//수축률
                        double Result = 0.00;//조형 결과값 
                        Suchuk = double.Parse(dgvMoldInfo.Rows[0].Cells[2].Value.ToString()) / 100;//dgvMoldInfo에서 수축률(문자열)을 가져와서 0.xx(double)으로 변경해준다.
                        Result = (Convert.ToDouble(dgvSogulInfo.Rows[0].Cells[j].Value) + Convert.ToDouble(dgvSogulInfo.Rows[1].Cells[j].Value))
                            / (1 - Suchuk);//조형계산식 ****(소결 + 여유) / 1 - 수축률
                        Result = Result + 0.005;//반올림하기 ex) Result가 x.xxy일때 y값 반올림하기위해서
                        int JJUM = Result.ToString().IndexOf(".");//소수점두자리까지 자르기위해서 "." 시작위치 구하기
                        //Result = Math.Round(Result, 2);//라운드함수는 반올림조건이 통계학함수라서 지멋대로임
                        if (Result != 0) dgvSogulInfo.Rows[2].Cells[j].Value = Result.ToString().Substring(0, JJUM + 3);//Result(결과값)에서 문자열 "."뒤로 두자리(소수점두자리)까지의 숫자를 짤라서 그리드뷰에 넣는다

                    }
                }
                else if (e.RowIndex.Equals(0) && e.ColumnIndex.Equals(11))
                    for (int i = 1; i < 11; i++)
                    {
                        //dgvSogulInfo.Rows[0].Cells[i].Value = string.Empty;
                        dgvSogulInfo.Rows[0].Cells[i].Value = "0.00";
                    }
                else if (e.RowIndex.Equals(1) && e.ColumnIndex.Equals(11))
                    for (int i = 1; i < 11; i++)
                    {
                        //dgvSogulInfo.Rows[0].Cells[i].Value = string.Empty;
                        dgvSogulInfo.Rows[1].Cells[i].Value = "0.00";
                    }
                else if (e.RowIndex.Equals(2) && e.ColumnIndex.Equals(11))
                    for (int i = 1; i < 11; i++)
                    {
                        dgvSogulInfo.Rows[0].Cells[i].Value = "0.00";
                        dgvSogulInfo.Rows[1].Cells[i].Value = "0.00";
                        dgvSogulInfo.Rows[2].Cells[i].Value = "0.00";
                
                    }
                
                

                //if (dgvSogulInfo.Rows[0].Cells[0].Selected)
                //{
                //    SetDgvCyc("소결");
                //}
                ////여유 입력
                //else if (dgvSogulInfo.Rows[1].Cells[0].Selected)
                //{
                //    SetDgvCyc("여유");
                //}
                ////조형 입력X, 계산값 뿌려줘야함
                //else if (dgvSogulInfo.Rows[2].Cells[0].Selected)
                //{

                //    for (int j = 1; j < 11; j++)
                //    {

                //        double Suchuk = 0.00;//수축률
                //        double Result = 0.00;//조형 결과값 
                //        Suchuk = double.Parse(dgvMoldInfo.Rows[0].Cells[2].Value.ToString()) / 100;//dgvMoldInfo에서 수축률(문자열)을 가져와서 0.xx(double)으로 변경해준다.
                //        Result = (Convert.ToDouble(dgvSogulInfo.Rows[0].Cells[j].Value) + Convert.ToDouble(dgvSogulInfo.Rows[1].Cells[j].Value))
                //            / (1 - Suchuk);//조형계산식 ****(소결 + 여유) / 1 - 수축률
                //        Result = Result + 0.005;//반올림하기 ex) Result가 x.xxy일때 y값 반올림하기위해서
                //        int JJUM = Result.ToString().IndexOf(".");//소수점두자리까지 자르기위해서 "." 시작위치 구하기
                //        //Result = Math.Round(Result, 2);//라운드함수는 반올림조건이 통계학함수라서 지멋대로임
                //        if (Result != 0) dgvSogulInfo.Rows[2].Cells[j].Value = Result.ToString().Substring(0, JJUM + 3);//Result(결과값)에서 문자열 "."뒤로 두자리(소수점두자리)까지의 숫자를 짤라서 그리드뷰에 넣는다

                //    }



                //}
                ////소결 초기화
                //if (dgvSogulInfo.Rows[0].Cells[11].Selected)
                //{
                //    for (int i = 1; i < 11; i++)
                //    {
                //        //dgvSogulInfo.Rows[0].Cells[i].Value = string.Empty;
                //        dgvSogulInfo.Rows[0].Cells[i].Value = "0.00";
                //    }

                //}
                ////여유 초기화
                //else if (dgvSogulInfo.Rows[1].Cells[11].Selected)
                //{
                //    for (int i = 1; i < 11; i++)
                //    {
                //        //dgvSogulInfo.Rows[1].Cells[i].Value = string.Empty;
                //        dgvSogulInfo.Rows[1].Cells[i].Value = "0.00";
                //    }

                //}
                ////조형 초기화(소결, 여유도 같이 초기화함)
                //else if (dgvSogulInfo.Rows[2].Cells[11].Selected)
                //{
                //    for (int i = 1; i < 11; i++)
                //    {
                //        //dgvSogulInfo.Rows[0].Cells[i].Value = string.Empty;
                //        //dgvSogulInfo.Rows[1].Cells[i].Value = string.Empty;
                //        //dgvSogulInfo.Rows[2].Cells[i].Value = string.Empty;
                //        dgvSogulInfo.Rows[0].Cells[i].Value = "0.00";
                //        dgvSogulInfo.Rows[1].Cells[i].Value = "0.00";
                //        dgvSogulInfo.Rows[2].Cells[i].Value = "0.00";
                //    }
                //}
            //}
            //else
            //{
            //    AutoClosingMessageBox.Show("지시번호 스캔 후 입력해주세요", "스캔 값 없음", 1500);
            //}
        }
        //조형 계산 **** 그냥 더하기임 쓰지말것
        //private void dgvSogulInfo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dgvSogulInfo.Rows.Count > 0) 
        //    {
        //        for (int i = 1; i < 11; i++)
        //        {
        //            //소결과 여유의 값이 둘다 입력되어있을때 수행한다.
        //            if (dgvSogulInfo.Rows[0].Cells[i].Value != null && dgvSogulInfo.Rows[1].Cells[i].Value != null)
        //            {
        //                double b = 0;
        //                b = float.Parse(dgvSogulInfo.Rows[0].Cells[i].Value.ToString()) + float.Parse(dgvSogulInfo.Rows[1].Cells[i].Value.ToString());
        //                dgvSogulInfo.Rows[2].Cells[i].Value = b.ToString();
        //            }
        //        }
        //    }
        //}

        private void btnMsWeight_Click(object sender, EventArgs e)
        {
            POPUP.Frm_CMNumericKeypad.g_Name = "중량";
            POPUP.Frm_CMNumericKeypad FK = new POPUP.Frm_CMNumericKeypad();
            if (FK.ShowDialog() == DialogResult.OK)
            {
                FK.tbInputText.SelectAll();
                lblMsWeight.Text = FK.tbInputText.Text.ToString();//키패드 숫자 입력
            }
        }

        private void btnMsLength_Click(object sender, EventArgs e)
        {
            POPUP.Frm_CMNumericKeypad.g_Name = "MS";
            POPUP.Frm_CMNumericKeypad FK = new POPUP.Frm_CMNumericKeypad();
            if (FK.ShowDialog() == DialogResult.OK)
            {
                FK.tbInputText.SelectAll();
                lblMsLength.Text = FK.tbInputText.Text.ToString();//키패드 숫자 입력
            }
        }

        private void btnInputQty_Click_1(object sender, EventArgs e)
        {
            POPUP.Frm_CMNumericKeypad.g_Name = "투입수량";
            POPUP.Frm_CMNumericKeypad FK = new POPUP.Frm_CMNumericKeypad();
            if (FK.ShowDialog() == DialogResult.OK)
            {
                FK.tbInputText.SelectAll();
                lblInputQty.Text = FK.tbInputText.Text.ToString();//키패드 숫자 입력
            }
        }

        private void btnWorkQty_Click_1(object sender, EventArgs e)
        {
            POPUP.Frm_CMNumericKeypad.g_Name = "생산수량";
            POPUP.Frm_CMNumericKeypad FK = new POPUP.Frm_CMNumericKeypad();
            if (FK.ShowDialog() == DialogResult.OK)
            {
                FK.tbInputText.SelectAll();
                lblWorkQty.Text = FK.tbInputText.Text.ToString();//키패드 숫자 입력
            }
            if (double.Parse(lblWorkQty.Text) > double.Parse(lblInputQty.Text))
            { 
                AutoClosingMessageBox.Show("생산수량은 투입수량보다 많을 수 없습니다. 다시 확인해주십시오.", "수량 입력 오류", 1500);
                lblWorkQty.Text = "0.00";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            dtpOrderDateFrom.Select();
            SendKeys.Send("%{DOWN}");
            
        }

        private void btnOrderDateTo_Click(object sender, EventArgs e)
        {
            dtpOrderDateTo.Select();
            SendKeys.Send("%{DOWN}");
        }

        private void btnOrderTimeFrom_Click(object sender, EventArgs e)
        {
            //g_Number = "시작시간";
            TimeCheck("시작시간");
        }

        private void btnOrderTimeTo_Click(object sender, EventArgs e)
        {
            //g_Number = "종료시간";
            TimeCheck("종료시간");
        }

        private void TimeCheck(string g_Number)
        {
            POPUP.Frm_CMNumericKeypad FK = new POPUP.Frm_CMNumericKeypad();
            POPUP.Frm_CMNumericKeypad.g_Name = g_Number;
            if (FK.ShowDialog() == DialogResult.OK)
            {
                FK.tbInputText.SelectAll();
                string g_Date = FK.tbInputText.Text;
                for (int i = g_Date.Length; i < 6; i++)//입력받은 시작시간의 길이가 6자리(HHmmss)가 아닐때 나머지 자리수는 0으로 채운다.
                {
                    g_Date = g_Date + "0";
                }
                //Console.WriteLine(g_Date+"@@@@@@@@@@@");
                if (Convert.ToInt32(g_Date.Substring(0, 2)) > 23)
                {
                    g_Date = g_Date.Replace(g_Date.Substring(0, 2), "00");
                }
                if (Convert.ToInt32(g_Date.Substring(2, 2)) > 59)
                {
                    g_Date = g_Date.Replace(g_Date.Substring(2, 2), "00");
                }
                if (Convert.ToInt32(g_Date.Substring(4, 2)) > 59)
                {
                    g_Date = g_Date.Replace(g_Date.Substring(4, 2), "00");
                }
                //Console.WriteLine(g_Date + "@@@@@@@@@@@");
                DateTime dt = DateTime.ParseExact(g_Date, "HHmmss", null);
                if (g_Number == "시작시간") { dtpOrderTimeFrom.Value = dt; }
                else if(g_Number == "종료시간") { dtpOrderTimeTo.Value = dt; }
            }
        }
        /// <summary>
        /// 프로시저 실행후 성공 여부 체크
        /// </summary>
        /// <param name="result">DB성공, 에러 등 메세지</param>
        private void MessageBoxInvalidation(string[] result)
        {
            if (result[0].Equals("success") && !result[1].Equals("null"))
            {
                //MessageBox.Show("처리되었습니다");
            }
            else if (result[0].Equals("failure"))
            {
                MessageBox.Show("" + result[1]);
            }
            else
            {
                MessageBox.Show("NullReferenceException 에러");
            }
        }
        private void CmdSaveClick()
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {

                        Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();
                        sqlParameter1.Add(WizWork_iWkResult.JOBID, JobID);              //JobID 입력안함. 지금 프로시저 수행 후 만들어짐~~

                        sqlParameter1.Add(WizWork_iWkResult.INSTID, lblInstID.Text); //작업지시번호입력
                        sqlParameter1.Add(WizWork_iWkResult.INSTDETSEQ, Convert.ToInt32(lblInstID.Tag)); //작업지시순위??
                        sqlParameter1.Add(WizWork_iWkResult.LABELID, lblInstID.Text); //작업지시번호로 입력함 ----------컨펌됨
                        sqlParameter1.Add(WizWork_iWkResult.LABELGUBUN, "2"); //작업지시번호 구분? 2로 고정함
                        sqlParameter1.Add(WizWork_iWkResult.PROCESSID, sProcessID); //선택되있는 sProcessID(setProcess에서 선택한)

                        sqlParameter1.Add(WizWork_iWkResult.MACHINEID, sMachineID); //선택되있는 sMachineID(setProcess에서 선택한)
                        sqlParameter1.Add(WizWork_iWkResult.SCANDATE, DateTime.Now.ToString("yyyyMMdd")); //년월일
                        sqlParameter1.Add(WizWork_iWkResult.SCANTIME, DateTime.Now.ToString("HHmm")); //시분초
                        sqlParameter1.Add(WizWork_iWkResult.WORKSTARTDATE, dtpOrderDateFrom.Value.ToString("yyyyMMdd")); //작업시작날짜
                        sqlParameter1.Add(WizWork_iWkResult.WORKSTARTTIME, dtpOrderTimeFrom.Value.ToString("HHmm")); //작업시작시간

                        sqlParameter1.Add(WizWork_iWkResult.WORKENDDATE, dtpOrderDateTo.Value.ToString("yyyyMMdd")); //작업종료날짜
                        sqlParameter1.Add(WizWork_iWkResult.WORKENDTIME, dtpOrderTimeTo.Value.ToString("HHmm")); //작업종료시간
                        sqlParameter1.Add(WizWork_iWkResult.ARTICLEID, lblArticle.Tag.ToString()); //품명코드=재종코드
                        sqlParameter1.Add(WizWork_iWkResult.INPUTQTY, Convert.ToDouble(lblInputQty.Text)); //투입수량
                        sqlParameter1.Add(WizWork_iWkResult.WORKQTY, Convert.ToDouble(lblWorkQty.Text)); //생산수량

                        sqlParameter1.Add(WizWork_iWkResult.MSWEIGHT, Convert.ToDouble(lblMsWeight.Text)); //MS중량
                        sqlParameter1.Add(WizWork_iWkResult.MSLENGTH, Convert.ToDouble(lblMsLength.Text)); //MS측정치
                        sqlParameter1.Add(WizWork_iWkResult.COMMENTS, lblRemark.Text);//지시커멘트
                        sqlParameter1.Add(WizWork_iWkResult.JOBGBN, "1");//작업구분 1:정상,2:무작업,3:재작업 work_u폼에서는 1번 정상으로 처리
                        
                        sqlParameter1.Add(WizWork_iWkResult.REWORKOLDYN, "N"); //재작업여부 NO
                        sqlParameter1.Add(WizWork_iWkResult.REWORKLINKPRODID, "");//????????????????????????
                        sqlParameter1.Add(WizWork_iWkResult.WORKPERSONID, sPersonID); //작업자ID
                        sqlParameter1.Add(WizWork_iWkResult.TEAMID, sTeam);//작업조ID
                        
                        sqlParameter1.Add(WizWork_iWkResult.NOREWORKCODE, "");//무작업코드_정상작업이므로 NULL입력,, 무재작업코드
                        sqlParameter1.Add(WizWork_iWkResult.NOREWORKREASON, "");//무작업사유_정상작업이므로 NULL입력

                        sqlParameter1.Add(WizWork_iWkResult.WDNO, "");// 대차번호
                        sqlParameter1.Add(WizWork_iWkResult.WDID, "");// 대차ID
                        sqlParameter1.Add(WizWork_iWkResult.WDQTY, "0");// 대차장임량
                        sqlParameter1.Add(WizWork_iWkResult.CREATEUSERID, sUserID);// 작업자

                        //ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_iWkResult", sqlParameter, false);
                        //DataStore.Instance.CloseConnection();

                        string[] result = DataStore.Instance.ExecuteTranProcedure("xp_WizWork_iWkResult", sqlParameter1, true);
                        DataStore.Instance.CloseConnection();

                        MessageBoxInvalidation(result);

                        //JobID = Convert.ToDecimal(DataStore.Instance.ExecuteQuery("select max(JobID) from wk_Result where InstID = '" + lblInstID.Text + "'", false)[1]);//작업지시번호로 iWkResult에서 등록한 JobID를 조회한다.
                        Console.WriteLine(JobID);
        
        
                    //하위품 등록
                    for (int i = 0; i < dgvGpLotSuryang.Rows.Count; i++)
                    {
                        Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                        sqlParameter2.Add(WizWork_iWkResultArticleChild.JOBID, JobID);//iWkResult 프로시저로 만들어진 JoibID
                        sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDLABELID, dgvGpLotSuryang.Rows[i].Cells[0].Value.ToString()); //GP LOT번호 &&바코드로 받아옴
                        sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDLABELGUBUN, "2");//????
                        sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDINPUTQTY, dgvGpLotSuryang.Rows[i].Cells[1].Value.ToString());//GP LOT별 수량
                        sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDARTICLEID, lblArticle.Tag.ToString());//재종코드
                        sqlParameter2.Add(WizWork_iWkResultArticleChild.REWORKOLDYN, "N");//
                        sqlParameter2.Add(WizWork_iWkResultArticleChild.REWORKLINKCHILDPRODID, "");//
                        sqlParameter2.Add(WizWork_iWkResultArticleChild.CREATEUSERID, sUserID);//작업자

                        //ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_iWkResultArticleChild", sqlParameter, false);
                        //DataStore.Instance.CloseConnection();

                        string[] result1 = DataStore.Instance.ExecuteTranProcedure("xp_WizWork_iWkResultArticleChild", sqlParameter2, true);
                        DataStore.Instance.CloseConnection();

                        MessageBoxInvalidation(result1);
                    }


                    if (dgvCycleInfo.Visible == true)
                    {
                        for (int j = 0; j < dgvCycleInfo.Rows.Count; j++)
                        ///for (int i = 2; i < dgvCycleInfo.Rows[0].Cells.Count - 1; i++)
                        {
                            //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                            //for (int j = 0; j < dgvCycleInfo.Rows.Count; j++)
                            for (int i = 2; i < dgvCycleInfo.Rows[0].Cells.Count - 1; i++)

                                //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                                if (dgvCycleInfo.Rows[j].Cells[i].Value != null)
                                {
                                    Dictionary<string, object> sqlParameter3 = new Dictionary<string, object>();
                                    sqlParameter3.Add(WizWork_iSamleValue.JOBID, JobID);//iWkResult 프로시저로 만들어진 JobID
                                    sqlParameter3.Add(WizWork_iSamleValue.SSAMPLEGBN, j + 1);//1:TOD 측정, 2:WID측정,3:L측정,4:소결측정*******이 if문에서는 소결값 측정안함
                                    sqlParameter3.Add(WizWork_iSamleValue.NSAMPLESEQ, i - 1);//측정값번호
                                    sqlParameter3.Add(WizWork_iSamleValue.NSAMPLEVALUE, Convert.ToDouble(dgvCycleInfo.Rows[j].Cells[i].Value));//측정값
                                    sqlParameter3.Add(WizWork_iSamleValue.NCOMPOSURE, Convert.ToDouble("0.00"));//소결의 여유값
                                    sqlParameter3.Add(WizWork_iSamleValue.NSHAPEVALUE, Convert.ToDouble("0.00"));//소결공정일 경유 조형 결과값, 계산결과
                                    sqlParameter3.Add(WizWork_iSamleValue.CREATEUSERID, sUserID);//작업자

                                    //ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_iSamleValue", sqlParameter, false);
                                    //DataStore.Instance.CloseConnection();

                                    string[] result2 = DataStore.Instance.ExecuteTranProcedure("xp_WizWork_iSamleValue", sqlParameter3, true);
                                    DataStore.Instance.CloseConnection();

                                    MessageBoxInvalidation(result2);

                                }
                        }
                    }
                    else if (dgvSogulInfo.Visible == true)
                    {
                        for (int i = 0; i < dgvSogulInfo.Rows.Count; i++)
                        {
                            Dictionary<string, object> sqlParameter4 = new Dictionary<string, object>();
                            for (int j = 1; j < dgvSogulInfo.Rows[0].Cells.Count - 1; j++)
                            {
                                sqlParameter4.Add(WizWork_iSamleValue.JOBID, JobID);//iWkResult 프로시저로 만들어진 JobID
                                sqlParameter4.Add(WizWork_iSamleValue.SSAMPLEGBN, "4");//1:TOD 측정, 2:WID측정,3:L측정,4:소결측정*******이 if문에서는 소결값만 측정함 
                                sqlParameter4.Add(WizWork_iSamleValue.NSAMPLESEQ, j);//측정값번호
                                if (i == 0)//소결 측정값 입력
                                {
                                    sqlParameter4.Add(WizWork_iSamleValue.NSAMPLEVALUE, dgvSogulInfo.Rows[i].Cells[j].Value.ToString());//소결 측정값
                                    sqlParameter4.Add(WizWork_iSamleValue.NCOMPOSURE, Convert.ToDouble("0.00"));//소결의 여유값
                                    sqlParameter4.Add(WizWork_iSamleValue.NSHAPEVALUE, Convert.ToDouble("0.00"));//소결공정일 경유 조형 결과값, 계산결과
                                }
                                else if (i == 1)//여유 측정값 입력
                                {
                                    sqlParameter4.Add(WizWork_iSamleValue.NSAMPLEVALUE, Convert.ToDouble("0.00"));//소결 측정값
                                    sqlParameter4.Add(WizWork_iSamleValue.NCOMPOSURE, dgvSogulInfo.Rows[i].Cells[j].Value.ToString());//소결의 여유값
                                    sqlParameter4.Add(WizWork_iSamleValue.NSHAPEVALUE, Convert.ToDouble("0.00"));//소결공정일 경유 조형 결과값, 계산결과
                                }
                                else if (i == 2)//조형 결과값 입력
                                {
                                    sqlParameter4.Add(WizWork_iSamleValue.NSAMPLEVALUE, Convert.ToDouble("0.00"));//소결 측정값
                                    sqlParameter4.Add(WizWork_iSamleValue.NCOMPOSURE, Convert.ToDouble("0.00"));//소결의 여유값
                                    sqlParameter4.Add(WizWork_iSamleValue.NSHAPEVALUE, dgvSogulInfo.Rows[i].Cells[j].Value.ToString());//소결공정일 경유 조형 결과값, 계산결과
                                }
                                sqlParameter4.Add(WizWork_iSamleValue.CREATEUSERID, sUserID);//작업자


                                string[] result3 = DataStore.Instance.ExecuteTranProcedure("xp_WizWork_iSamleValue", sqlParameter4, true);
                                DataStore.Instance.CloseConnection();

                                MessageBoxInvalidation(result3);
                            }
                        }
                    }
       
                    scope.Complete();

                }

            }
            catch (TransactionAbortedException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //static public int CreateTransactionScope(string connectString1, string connectString2, string commandText1, string commandText2, string commandText3)
        //{
        //    // Initialize the return value to zero and create a StringWriter to display results.
        //    int returnValue = 0;
        //    System.IO.StringWriter writer = new System.IO.StringWriter();

        //    try
        //    {
        //        // Create the TransactionScope to execute the commands, guaranteeing
        //        // that both commands can commit or roll back as a single unit of work.
        //        using (TransactionScope scope = new TransactionScope())
        //        {
        //            using (SqlConnection connection1 = new SqlConnection(connectString1))
        //            {
        //                // Opening the connection automatically enlists it in the 
        //                // TransactionScope as a lightweight transaction.
        //                connection1.Open();

        //                // Create the SqlCommand object and execute the first command.
        //                SqlCommand command1 = new SqlCommand(commandText1, connection1);
        //                returnValue = command1.ExecuteNonQuery();
        //                writer.WriteLine("Rows to be affected by command1: {0}", returnValue);

        //                // If you get here, this means that command1 succeeded. By nesting
        //                // the using block for connection2 inside that of connection1, you
        //                // conserve server and network resources as connection2 is opened
        //                // only when there is a chance that the transaction can commit.   
        //                using (SqlConnection connection2 = new SqlConnection(connectString2))
        //                {
        //                    // The transaction is escalated to a full distributed
        //                    // transaction when connection2 is opened.
        //                    connection2.Open();

        //                    // Execute the second command in the second database.
        //                    returnValue = 0;
        //                    SqlCommand command2 = new SqlCommand(commandText2, connection2);
        //                    returnValue = command2.ExecuteNonQuery();
        //                    writer.WriteLine("Rows to be affected by command2: {0}", returnValue);
        //                    using (SqlConnection connection3 = new SqlConnection(connectString2))
        //                    {
        //                        // The transaction is escalated to a full distributed
        //                        // transaction when connection2 is opened.
        //                        connection3.Open();

        //                        // Execute the second command in the second database.
        //                        returnValue = 0;
        //                        SqlCommand command3 = new SqlCommand(commandText3, connection3);
        //                        returnValue = command3.ExecuteNonQuery();
        //                        writer.WriteLine("Rows to be affected by command3: {0}", returnValue);
        //                    }
        //                }
        //            }

        //            // The Complete method commits the transaction. If an exception has been thrown,
        //            // Complete is not  called and the transaction is rolled back.
        //            scope.Complete();

        //        }

        //    }
        //    catch (TransactionAbortedException ex)
        //    {
        //        writer.WriteLine("TransactionAbortedException Message: {0}", ex.Message);
        //    }
        //    catch (ApplicationException ex)
        //    {
        //        writer.WriteLine("ApplicationException Message: {0}", ex.Message);
        //    }

        //    // Display messages.
        //    Console.WriteLine(writer.ToString());

        //    return returnValue;
        //}
        private void iTrans()
        {
                        
                        try
                        {
                            //DataStore.Instance.TransactionBegin(); // 트랜잭션 Begin

                            //첫번째 프로시저 데이터값 셋팅
                            Dictionary<string, object> sqlParameter1 = new Dictionary<string, object>();
                            //sqlParameter1.Add(WizWork_iWkResult.JOBID, "");              //JobID 입력안함. 지금 프로시저 수행 후 만들어짐~~

                            sqlParameter1.Add("JobID", "");              //JobID 입력안함. 지금 프로시저 수행 후 만들어짐~~

                            sqlParameter1.Add(WizWork_iWkResult.INSTID, lblInstID.Text); //작업지시번호입력
                            sqlParameter1.Add(WizWork_iWkResult.INSTDETSEQ, Convert.ToInt32(lblInstID.Tag)); //작업지시순위??
                            sqlParameter1.Add(WizWork_iWkResult.LABELID, lblInstID.Text); //작업지시번호로 입력함 ----------컨펌됨
                            sqlParameter1.Add(WizWork_iWkResult.LABELGUBUN, "2"); //작업지시번호 구분? 2로 고정함
                            sqlParameter1.Add(WizWork_iWkResult.PROCESSID, sProcessID); //선택되있는 sProcessID(setProcess에서 선택한)

                            sqlParameter1.Add(WizWork_iWkResult.MACHINEID, sMachineID); //선택되있는 sMachineID(setProcess에서 선택한)
                            sqlParameter1.Add(WizWork_iWkResult.SCANDATE, DateTime.Now.ToString("yyyyMMdd")); //년월일
                            sqlParameter1.Add(WizWork_iWkResult.SCANTIME, DateTime.Now.ToString("HHmm")); //시분초
                            sqlParameter1.Add(WizWork_iWkResult.WORKSTARTDATE, dtpOrderDateFrom.Value.ToString("yyyyMMdd")); //작업시작날짜
                            sqlParameter1.Add(WizWork_iWkResult.WORKSTARTTIME, dtpOrderTimeFrom.Value.ToString("HHmm")); //작업시작시간

                            sqlParameter1.Add(WizWork_iWkResult.WORKENDDATE, dtpOrderDateTo.Value.ToString("yyyyMMdd")); //작업종료날짜
                            sqlParameter1.Add(WizWork_iWkResult.WORKENDTIME, dtpOrderTimeTo.Value.ToString("HHmm")); //작업종료시간
                            sqlParameter1.Add(WizWork_iWkResult.ARTICLEID, lblArticle.Tag.ToString()); //품명코드=재종코드
                            sqlParameter1.Add(WizWork_iWkResult.INPUTQTY, Convert.ToDouble(lblInputQty.Text)); //투입수량
                            sqlParameter1.Add(WizWork_iWkResult.WORKQTY, Convert.ToDouble(lblWorkQty.Text)); //생산수량

                            sqlParameter1.Add(WizWork_iWkResult.MSWEIGHT, Convert.ToDouble(lblMsWeight.Text)); //MS중량
                            sqlParameter1.Add(WizWork_iWkResult.MSLENGTH, Convert.ToDouble(lblMsLength.Text)); //MS측정치
                            sqlParameter1.Add(WizWork_iWkResult.COMMENTS, lblRemark.Text);//지시커멘트
                            sqlParameter1.Add(WizWork_iWkResult.JOBGBN, "1");//작업구분 1:정상,2:무작업,3:재작업 work_u폼에서는 1번 정상으로 처리

                            sqlParameter1.Add(WizWork_iWkResult.REWORKOLDYN, "N"); //재작업여부 NO
                            sqlParameter1.Add(WizWork_iWkResult.REWORKLINKPRODID, "");//????????????????????????
                            sqlParameter1.Add(WizWork_iWkResult.WORKPERSONID, sPersonID); //작업자ID
                            sqlParameter1.Add(WizWork_iWkResult.TEAMID, sTeam);//작업조ID

                            sqlParameter1.Add(WizWork_iWkResult.NOREWORKCODE, "");//무작업코드_정상작업이므로 NULL입력,, 무재작업코드
                            sqlParameter1.Add(WizWork_iWkResult.NOREWORKREASON, "");//무작업사유_정상작업이므로 NULL입력

                            sqlParameter1.Add(WizWork_iWkResult.WDNO, "");// 대차번호
                            sqlParameter1.Add(WizWork_iWkResult.WDID, "");// 대차ID
                            sqlParameter1.Add(WizWork_iWkResult.WDQTY, "0");// 대차장임량
                            sqlParameter1.Add(WizWork_iWkResult.CREATEUSERID, sUserID);// 작업자

                            Dictionary<string, int> outputParam = new Dictionary<string, int>();
                            //int JobIDs = 0;
                            outputParam.Add("JobID", 10);

                            Dictionary<string, string> dicResult = DataStore.Instance.ExecuteProcedureOutputNoTran("xp_WizWork_iWkResult", sqlParameter1, outputParam, true);
                            string JobID = string.Empty;
                            JobID = dicResult["JobID"];


                            //하위품 등록
                            //DataRow dr =;
                            for (int i = 0; i < dgvGpLotSuryang.Rows.Count; i++)
                            {
                                Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                                sqlParameter2.Add(WizWork_iWkResultArticleChild.JOBID, JobID);//iWkResult 프로시저로 만들어진 JoibID
                                sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDLABELID, dgvGpLotSuryang.Rows[i].Cells[0].Value.ToString()); //GP LOT번호 &&바코드로 받아옴
                                sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDLABELGUBUN, "2");//????



                                sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDINPUTQTY, dgvGpLotSuryang.Rows[i].Cells[1].Value.ToString());//GP LOT별 수량

                                sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDARTICLEID, lblArticle.Tag.ToString());//재종코드
                                sqlParameter2.Add(WizWork_iWkResultArticleChild.REWORKOLDYN, "N");//
                                sqlParameter2.Add(WizWork_iWkResultArticleChild.REWORKLINKCHILDPRODID, "");//
                                sqlParameter2.Add(WizWork_iWkResultArticleChild.CREATEUSERID, sUserID);//작업자


                                //ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_iWkResultArticleChild", sqlParameter, false);
                                //DataStore.Instance.CloseConnection();

                                //string[] result1 = DataStore.Instance.ExecuteTranProcedure("xp_WizWork_iWkResultArticleChild", sqlParameter2, true);
                                


                                //string[] result2 = DataStore.Instance.ExecuteTranProcedure_NoBeginNoCom("xp_WizWork_iWkResultArticleChild", sqlParameter2, true);


                                
                                //string[] result2 = DataStore.Instance.ExecuteTranProcedure_NoBeginNoCom("xp_WizWork_iWkResultArticleChild", sqlParameter2, true);

                                string[] result2 = DataStore.Instance.ExecuteProcedureWithoutTransaction("xp_WizWork_iWkResultArticleChild", sqlParameter2, true);
                                //DataStore.Instance.CloseConnection();

                                //MessageBoxInvalidation(result1);
                            }


                            if (dgvCycleInfo.Visible == true)
                            {
                                for (int j = 0; j < dgvCycleInfo.Rows.Count; j++)
                                ///for (int i = 2; i < dgvCycleInfo.Rows[0].Cells.Count - 1; i++)
                                {
                                    //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                                    //for (int j = 0; j < dgvCycleInfo.Rows.Count; j++)
                                    for (int i = 2; i < dgvCycleInfo.Rows[0].Cells.Count - 1; i++)

                                        //Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
                                        if (dgvCycleInfo.Rows[j].Cells[i].Value != null)
                                        {
                                            Dictionary<string, object> sqlParameter3 = new Dictionary<string, object>();
                                            sqlParameter3.Add(WizWork_iSamleValue.JOBID, JobID);//iWkResult 프로시저로 만들어진 JobID

                                            //트랜잭션 테스트중~~`
                                            sqlParameter3.Add(WizWork_iSamleValue.SSAMPLEGBN, j + 1);//1:TOD 측정, 2:WID측정,3:L측정,4:소결측정*******이 if문에서는 소결값 측정안함


                                            sqlParameter3.Add(WizWork_iSamleValue.NSAMPLESEQ, i - 1);//측정값번호
                                            sqlParameter3.Add(WizWork_iSamleValue.NSAMPLEVALUE, Convert.ToDouble(dgvCycleInfo.Rows[j].Cells[i].Value));//측정값
                                            sqlParameter3.Add(WizWork_iSamleValue.NCOMPOSURE, Convert.ToDouble("0.00"));//소결의 여유값
                                            sqlParameter3.Add(WizWork_iSamleValue.NSHAPEVALUE, Convert.ToDouble("0.00"));//소결공정일 경유 조형 결과값, 계산결과
                                            sqlParameter3.Add(WizWork_iSamleValue.CREATEUSERID, sUserID);//작업자

                                            //ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_iSamleValue", sqlParameter, false);
                                            //DataStore.Instance.CloseConnection();

                                            //string[] result2 = DataStore.Instance.ExecuteTranProcedure("xp_WizWork_iSamleValue", sqlParameter3, true);


                                            //string[] result3 = DataStore.Instance.ExecuteTranProcedure_NoBeginNoCom("xp_WizWork_iSamleValue", sqlParameter3, true);

                                            //string[] result3 = DataStore.Instance.ExecuteTranProcedure_NoBeginNoCom("xp_WizWork_iSamleValue", sqlParameter3, true);

                                            string[] result3 = DataStore.Instance.ExecuteProcedureWithoutTransaction("xp_WizWork_iSamleValue", sqlParameter3, true);

                                            //DataStore.Instance.CloseConnection();

                                            //MessageBoxInvalidation(result2);

                                        }
                                }
                            }
                            else if (dgvSogulInfo.Visible == true)
                            {
                                for (int i = 0; i < dgvSogulInfo.Rows.Count; i++)
                                {
                                      
                                    for (int j = 1; j < dgvSogulInfo.Rows[0].Cells.Count - 1; j++)
                                    {
                                        Dictionary<string, object> sqlParameter4 = new Dictionary<string, object>();  
                                        sqlParameter4.Add(WizWork_iSamleValue.JOBID, JobID);//iWkResult 프로시저로 만들어진 JobID        
                                        sqlParameter4.Add(WizWork_iSamleValue.SSAMPLEGBN, "4");//1:TOD 측정, 2:WID측정,3:L측정,4:소결측정*******이 if문에서는 소결값만 측정함 
                                        sqlParameter4.Add(WizWork_iSamleValue.NSAMPLESEQ, j);//측정값번호
                                        if (i == 0)//소결 측정값 입력
                                        {
                                            sqlParameter4.Add(WizWork_iSamleValue.NSAMPLEVALUE, dgvSogulInfo.Rows[i].Cells[j].Value.ToString());//소결 측정값
                                            sqlParameter4.Add(WizWork_iSamleValue.NCOMPOSURE, Convert.ToDouble("0.00"));//소결의 여유값
                                            sqlParameter4.Add(WizWork_iSamleValue.NSHAPEVALUE, Convert.ToDouble("0.00"));//소결공정일 경유 조형 결과값, 계산결과
                                        }
                                        else if (i == 1)//여유 측정값 입력
                                        {
                                            sqlParameter4.Add(WizWork_iSamleValue.NSAMPLEVALUE, Convert.ToDouble("0.00"));//소결 측정값
                                            sqlParameter4.Add(WizWork_iSamleValue.NCOMPOSURE, dgvSogulInfo.Rows[i].Cells[j].Value.ToString());//소결의 여유값
                                            sqlParameter4.Add(WizWork_iSamleValue.NSHAPEVALUE, Convert.ToDouble("0.00"));//소결공정일 경유 조형 결과값, 계산결과
                                        }
                                        else if (i == 2)//조형 결과값 입력
                                        {
                                            sqlParameter4.Add(WizWork_iSamleValue.NSAMPLEVALUE, Convert.ToDouble("0.00"));//소결 측정값
                                            sqlParameter4.Add(WizWork_iSamleValue.NCOMPOSURE, Convert.ToDouble("0.00"));//소결의 여유값
                                            sqlParameter4.Add(WizWork_iSamleValue.NSHAPEVALUE, dgvSogulInfo.Rows[i].Cells[j].Value.ToString());//소결공정일 경유 조형 결과값, 계산결과
                                        }
                                        sqlParameter4.Add(WizWork_iSamleValue.CREATEUSERID, sUserID);//작업자


                                        //string[] result4 = DataStore.Instance.ExecuteTranProcedure_NoBeginNoCom("xp_WizWork_iSamleValue", sqlParameter4, true);

                                        //string[] result4 = DataStore.Instance.ExecuteTranProcedure_NoBeginNoCom("xp_WizWork_iSamleValue", sqlParameter4, true);

                                        string[] result4 = DataStore.Instance.ExecuteProcedureWithoutTransaction("xp_WizWork_iSamleValue", sqlParameter4, true);
                                        //DataStore.Instance.CloseConnection();

                                        //MessageBoxInvalidation(result3);
                                    }
                                }
                            }
                            if (sDefectID != null) //불량등록
                            {
                                for (int i = 0; i < sDefectID.Length; i++)
                                {
                                    Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
                                    sqlParameter2.Add("WkDefectID", "");              //JobID 입력안함. 지금 프로시저 수행 후 만들어짐~~
                                    sqlParameter2.Add(WizWork_iWkResultInspect.ORDERID, sOrderID); //????????????????????????
                                    sqlParameter2.Add(WizWork_iWkResultInspect.ORDERSEQ, "0");//??????????????????????????
                                    sqlParameter2.Add(WizWork_iWkResultInspect.PROCESSID, sProcessID);//GP LOT별 수량
                                    sqlParameter2.Add(WizWork_iWkResultInspect.MACHINEID, sMachineID);//재종코드

                                    sqlParameter2.Add(WizWork_iWkResultInspect.DEFECTQTY, Convert.ToDouble(sDefectQty[i].ToString()));//
                                    sqlParameter2.Add(WizWork_iWkResultInspect.BOXID, "");//빈값
                                    sqlParameter2.Add(WizWork_iWkResultInspect.DEFECTID, sDefectID[i].ToString());//
                                    sqlParameter2.Add(WizWork_iWkResultInspect.XPOS, Convert.ToInt32(sXPos[i].ToString()));//
                                    sqlParameter2.Add(WizWork_iWkResultInspect.YPOS, Convert.ToInt32(sYPos[i].ToString()));//

                                    sqlParameter2.Add(WizWork_iWkResultInspect.INSPECTDATE, DateTime.Now.ToString("yyyyMMdd")); //년월일
                                    sqlParameter2.Add(WizWork_iWkResultInspect.INSPECTTIME, DateTime.Now.ToString("HHmm")); //시분초
                                    sqlParameter2.Add(WizWork_iWkResultInspect.PERSONID, sPersonID);//작업자
                                    sqlParameter2.Add(WizWork_iWkResultInspect.JOBID, JobID);//작업자
                                    sqlParameter2.Add(WizWork_iWkResultInspect.CREATEUSERID, sUserID);//작업자
                                    Dictionary<string, int> outputParam1 = new Dictionary<string, int>();
                                    //int JobIDs = 0;
                                    outputParam1.Add("WkDefectID", 10);

                                    Dictionary<string, string> dicResult1 = DataStore.Instance.ExecuteProcedureOutputNoTran("xp_WizWork_iWkResultInspect", sqlParameter2, outputParam1, true);
                                    string WkDefectID = string.Empty;
                                    WkDefectID = dicResult1["WkDefectID"];
                                }
                            }


                            //DataStore.Instance.TransactionCommit();
                        }
                        
                        catch (NullReferenceException)
                        {
                            DataStore.Instance.TransactionRollBack();
                            //if (p_Command.Transaction != null)
                            //{
                            //    p_Command.Transaction.Commit();
                            //}

                            //return new String[] { Resources.success, "NullReferenceException" };
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            DataStore.Instance.TransactionRollBack();
                            
                            //if (p_Command.Transaction != null)
                            //{
                            //    //오류 발생시 Rollback
                            //    p_Command.Transaction.Rollback();
                            //}
                            //return new String[] { Resources.failure, ex.Message };
                        }
                        finally
                        {
                            //DataStore.Instance.TransactionCommit();
                            DataStore.Instance.CloseConnection();
                            //if (p_Connection.State != ConnectionState.Closed)
                            //{
                            //    p_Connection.Close();
                            //}
                            AutoClosingMessageBox.Show("저장 되었습니다", "Save", 2000);
                        }
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("현재화면의 작업중인 내용들은 모두 삭제됩니다.\r\n그래도 계속 진행하시겠습니까?", "[확인]", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
                foreach (Form openForm in Application.OpenForms)//중복실행방지
                {
                    if (openForm.Name == "Frm_tprc_PlanInput")
                    {
                        openForm.Activate();
                        return;
                    }
                }
                
            }
            

        }

        private void dtpOrderDateFrom_Enter(object sender, EventArgs e)
        {
            dtpOrderDateFrom.Select();
            SendKeys.Send("%{DOWN}");
        }

        private void dtpOrderDateFrom_MouseDown(object sender, MouseEventArgs e)
        {
            dtpOrderDateFrom.Select();
            SendKeys.Send("%{DOWN}");
        }

        private void dtpOrderDateTo_MouseDown(object sender, MouseEventArgs e)
        {
            dtpOrderDateTo.Select();
            SendKeys.Send("%{DOWN}");
        }

        private void dtpOrderTimeFrom_MouseDown(object sender, MouseEventArgs e)
        {
            TimeCheck("시작시간");
        }

        private void btnOrderTimeTo_MouseDown(object sender, MouseEventArgs e)
        {
            TimeCheck("종료시간");
        }

        //private void dgvExCycleInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        //{
        //     //dgvCycleInfo 입력 및 초기화 
        //    //T/OD 입력
        //    if(e.RowIndex.Equals(0) && e.ColumnIndex.Equals(0))
        //        //(dgvCycleInfo.Rows[0].Cells[0].Selected)
        //    {
        //        SetDgvCyc("T/OD");
        //    }
        //    //W/ID 입력
        //    else if(e.RowIndex.Equals(1) && e.ColumnIndex.Equals(0))
        //        //(dgvCycleInfo.Rows[1].Cells[0].Selected)
        //    {
        //        SetDgvCyc("W/ID");
        //    }
        //    //L 입력
        //    else if (e.RowIndex.Equals(2) && e.ColumnIndex.Equals(0))
        //        //(dgvCycleInfo.Rows[2].Cells[0].Selected)
        //    {
        //        SetDgvCyc("L");
        //    }
        //    //T/OD 초기화
        //    if(e.RowIndex.Equals(0) && e.ColumnIndex.Equals(5))
        //        //(dgvCycleInfo.Rows[0].Cells[5].Selected)
        //    {
        //        for (int i = 2; i < 5; i++)
        //        {
        //            //dgvCycleInfo.Rows[0].Cells[i].Value = string.Empty;
        //            dgvExCycleInfo.Rows[0].Cells[i].Value = "0.00";
        //        }
        //    }
        //    //W/ID 초기화
        //    else if(e.RowIndex.Equals(1) && e.ColumnIndex.Equals(5))
        //        //(dgvCycleInfo.Rows[1].Cells[5].Selected)
        //    {
        //        for (int i = 2; i < 5; i++)
        //        {
        //            //dgvCycleInfo.Rows[1].Cells[i].Value = string.Empty;
        //            dgvExCycleInfo.Rows[1].Cells[i].Value = "0.00";
        //        }
        //    }
        //    //L 초기화
        //    else if (e.RowIndex.Equals(2) && e.ColumnIndex.Equals(5))
        //        //(dgvCycleInfo.Rows[2].Cells[5].Selected)
        //    {
        //        for (int i = 2; i < 5; i++)
        //        {
        //            //dgvCycleInfo.Rows[2].Cells[i].Value = string.Empty;
        //            dgvExCycleInfo.Rows[2].Cells[i].Value = "0.00";
        //        }
        //    }
        
        //}
        //셀값 바뀌면 DataRow에 강제로 값을 변경해주고 RowState를 Modify로 변경, 저장버튼 비활성화
        //private void dgvCycleInfo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    GridViewCellChanged(this.dgvCycleInfo, e.RowIndex, e.ColumnIndex);
        //}
        //private void GridViewCellChanged(DataGridView gridview, int RowIndex, int ColumnsIndex)
        //{
        //    if (RowIndex > -1 && ColumnsIndex > -1)
        //    {
        //        DataRow row = Methods.GetDataRow(gridview.Rows[RowIndex]);
        //        //ds.Tables[0].Rows
        //        string colName = gridview.Columns[ColumnsIndex].Name;

        //        row.BeginEdit();

        //        row[colName] = gridview.Rows[RowIndex].Cells[ColumnsIndex].Value;

        //        row.EndEdit();
        //        _hasChanges = true;
        //    }
        //}

        //조형 계산 **** 
        //private void dgvSogulInfo_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (dgvSogulInfo.Rows.Count > 0)
        //    {
        //        for (int i = 1; i < 11; i++)
        //        {
        //            //소결과 여유의 값이 둘다 입력되어있을때 수행한다.
        //            if (dgvSogulInfo.Rows[0].Cells[i].Value != null && dgvSogulInfo.Rows[1].Cells[i].Value != null)
        //            {
        //                double b = 0;
        //                b = float.Parse(dgvSogulInfo.Rows[0].Cells[i].Value.ToString()) + float.Parse(dgvSogulInfo.Rows[1].Cells[i].Value.ToString());
        //                dgvSogulInfo.Rows[2].Cells[i].Value = b.ToString();
        //            }
        //        }
        //    }
        
        //}

        //private void Transac()
        //{

        //    SqlConnection conn = null;
        //    SqlTransaction tran = null;
        //    SqlCommand cmd = null;

        //    try
        //    {
        //        conn = new SqlConnection(ConnectionString);
        //        cmd = new SqlCommand();
        //        conn.Open();
        //        tran = conn.BeginTransaction();
 
        //          // 프로시져 1 실행
                
        //        cmd.Connection.BeginTransaction();
        //        cmd.CommandText = "sp_Insert";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add(new SqlParameter("id", DbType.Varchar2, "test1"));

        //        int retval = cmd.ExecuteNonQuery();
        //        cmd.Parameters.Clear();
                  
        //           // 프로시져 2 실행
        //        cmd.Connection.BeginTransaction();
        //        cmd.CommandText = "sp_update";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.Add(new SqlParameter("id", DbType.Varchar2, "test2"));

        //        //int retval = cmd.ExecuteNonQuery();
        //        retval = cmd.ExecuteNonQuery();
        //        cmd.Parameters.Clear();
                
        //        tran.Commit();
        //        conn.BeginTransaction().Commit();
        //    }
        //    catch (Exception ex)
        //    {
        //          tran.Rollback();
        //    }
        //    finally
        //    {

        //    }
        //}
        //private void OneTransaction()
        //{
        //    Dictionary<string, object> sqlParameter = new Dictionary<string, object>();
        //     sqlParameter1.Add("JobID", "");              //JobID 입력안함. 지금 프로시저 수행 후 만들어짐~~

        //                    sqlParameter1.Add(WizWork_iWkResult.INSTID, lblInstID.Text); //작업지시번호입력
        //                    sqlParameter1.Add(WizWork_iWkResult.INSTDETSEQ, Convert.ToInt32(lblInstID.Tag)); //작업지시순위??
        //                    sqlParameter1.Add(WizWork_iWkResult.LABELID, lblInstID.Text); //작업지시번호로 입력함 ----------컨펌됨
        //                    sqlParameter1.Add(WizWork_iWkResult.LABELGUBUN, "2"); //작업지시번호 구분? 2로 고정함
        //                    sqlParameter1.Add(WizWork_iWkResult.PROCESSID, sProcessID); //선택되있는 sProcessID(setProcess에서 선택한)

        //                    sqlParameter1.Add(WizWork_iWkResult.MACHINEID, sMachineID); //선택되있는 sMachineID(setProcess에서 선택한)
        //                    sqlParameter1.Add(WizWork_iWkResult.SCANDATE, DateTime.Now.ToString("yyyyMMdd")); //년월일
        //                    sqlParameter1.Add(WizWork_iWkResult.SCANTIME, DateTime.Now.ToString("HHmm")); //시분초
        //                    sqlParameter1.Add(WizWork_iWkResult.WORKSTARTDATE, dtpOrderDateFrom.Value.ToString("yyyyMMdd")); //작업시작날짜
        //                    sqlParameter1.Add(WizWork_iWkResult.WORKSTARTTIME, dtpOrderTimeFrom.Value.ToString("HHmm")); //작업시작시간

        //                    sqlParameter1.Add(WizWork_iWkResult.WORKENDDATE, dtpOrderDateTo.Value.ToString("yyyyMMdd")); //작업종료날짜
        //                    sqlParameter1.Add(WizWork_iWkResult.WORKENDTIME, dtpOrderTimeTo.Value.ToString("HHmm")); //작업종료시간
        //                    sqlParameter1.Add(WizWork_iWkResult.ARTICLEID, lblArticle.Tag.ToString()); //품명코드=재종코드
        //                    sqlParameter1.Add(WizWork_iWkResult.INPUTQTY, Convert.ToDouble(lblInputQty.Text)); //투입수량
        //                    sqlParameter1.Add(WizWork_iWkResult.WORKQTY, Convert.ToDouble(lblWorkQty.Text)); //생산수량

        //                    sqlParameter1.Add(WizWork_iWkResult.MSWEIGHT, Convert.ToDouble(lblMsWeight.Text)); //MS중량
        //                    sqlParameter1.Add(WizWork_iWkResult.MSLENGTH, Convert.ToDouble(lblMsLength.Text)); //MS측정치
        //                    sqlParameter1.Add(WizWork_iWkResult.COMMENTS, lblRemark.Text);//지시커멘트
        //                    sqlParameter1.Add(WizWork_iWkResult.JOBGBN, "1");//작업구분 1:정상,2:무작업,3:재작업 work_u폼에서는 1번 정상으로 처리

        //                    sqlParameter1.Add(WizWork_iWkResult.REWORKOLDYN, "N"); //재작업여부 NO
        //                    sqlParameter1.Add(WizWork_iWkResult.REWORKLINKPRODID, "");//????????????????????????
        //                    sqlParameter1.Add(WizWork_iWkResult.WORKPERSONID, sPersonID); //작업자ID
        //                    sqlParameter1.Add(WizWork_iWkResult.TEAMID, sTeam);//작업조ID

        //                    sqlParameter1.Add(WizWork_iWkResult.NOREWORKCODE, "");//무작업코드_정상작업이므로 NULL입력,, 무재작업코드
        //                    sqlParameter1.Add(WizWork_iWkResult.NOREWORKREASON, "");//무작업사유_정상작업이므로 NULL입력

        //                    sqlParameter1.Add(WizWork_iWkResult.WDNO, "");// 대차번호
        //                    sqlParameter1.Add(WizWork_iWkResult.WDID, "");// 대차ID
        //                    sqlParameter1.Add(WizWork_iWkResult.WDQTY, "0");// 대차장임량
        //                    sqlParameter1.Add(WizWork_iWkResult.CREATEUSERID, sUserID);// 작업자

        //                    Dictionary<string, int> outputParam = new Dictionary<string, int>();
        //                    //int JobIDs = 0;
        //                    outputParam.Add("JobID", 10);

        //                    Dictionary<string, string> dicResult = DataStore.Instance.ExecuteProcedureOutputNoTran("xp_WizWork_iWkResult", sqlParameter1, outputParam, true);
        //                    string JobID = string.Empty;
        //                    JobID = dicResult["JobID"];


        //                    //하위품 등록
        //                    //DataRow dr =;
        //                    for (int i = 0; i < dgvGpLotSuryang.Rows.Count; i++)
        //                    {
        //                        Dictionary<string, object> sqlParameter2 = new Dictionary<string, object>();
        //                        sqlParameter2.Add(WizWork_iWkResultArticleChild.JOBID, JobID);//iWkResult 프로시저로 만들어진 JoibID
        //                        sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDLABELID, dgvGpLotSuryang.Rows[i].Cells[0].Value.ToString()); //GP LOT번호 &&바코드로 받아옴
        //                        sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDLABELGUBUN, "2");//????
                                

                                
        //                        sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDARTICLEQTY, dgvGpLotSuryang.Rows[i].Cells[1].Value.ToString());//GP LOT별 수량

        //                        sqlParameter2.Add(WizWork_iWkResultArticleChild.CHILDARTICLEID, lblArticle.Tag.ToString());//재종코드
        //                        sqlParameter2.Add(WizWork_iWkResultArticleChild.REWORKOLDYN, "N");//
        //                        sqlParameter2.Add(WizWork_iWkResultArticleChild.REWORKLINKCHILDPRODID, "");//
        //                        sqlParameter2.Add(WizWork_iWkResultArticleChild.CREATEUSERID, sUserID);//작업자

        //                        //ds = DataStore.Instance.ProcedureToDataSet("xp_WizWork_iWkResultArticleChild", sqlParameter, false);
        //                        //DataStore.Instance.CloseConnection();

        //                        //string[] result1 = DataStore.Instance.ExecuteTranProcedure("xp_WizWork_iWkResultArticleChild", sqlParameter2, true);
                                


        //                        //string[] result2 = DataStore.Instance.ExecuteTranProcedure_NoBeginNoCom("xp_WizWork_iWkResultArticleChild", sqlParameter2, true);


                                
        //                        //string[] result2 = DataStore.Instance.ExecuteTranProcedure_NoBeginNoCom("xp_WizWork_iWkResultArticleChild", sqlParameter2, true);

        //                        string[] result2 = DataStore.Instance.ExecuteProcedureWithoutTransaction("xp_WizWork_iWkResultArticleChild", sqlParameter2, true);
        //                        //DataStore.Instance.CloseConnection();

        //                        //MessageBoxInvalidation(result1);
        //                    }


        //                    if (dgvCycleInfo.Visible == true)
        //                    {
        //                        for (int j = 0; j < dgvCycleInfo.Rows.Count; j++)
        //                        ///for (int i = 2; i < dgvCycleInfo.Rows[0].Cells.Count - 1; i++)
        //                        {
        //                            for (int i = 2; i < dgvCycleInfo.Rows[0].Cells.Count - 1; i++)                                     
        //                                if (dgvCycleInfo.Rows[j].Cells[i].Value != null)
        //                                {
        //                                    Dictionary<string, object> sqlParameter3 = new Dictionary<string, object>();
        //                                    sqlParameter3.Add(WizWork_iSamleValue.JOBID, JobID);//iWkResult 프로시저로 만들어진 JobID
        //                                    //트랜잭션 테스트중~~`
        //                                    sqlParameter3.Add(WizWork_iSamleValue.SSAMPLEGBN, j + 1);//1:TOD 측정, 2:WID측정,3:L측정,4:소결측정*******이 if문에서는 소결값 측정안함
        //                                    sqlParameter3.Add(WizWork_iSamleValue.NSAMPLESEQ, i - 1);//측정값번호
        //                                    sqlParameter3.Add(WizWork_iSamleValue.NSAMPLEVALUE, Convert.ToDouble(dgvCycleInfo.Rows[j].Cells[i].Value));//측정값
        //                                    sqlParameter3.Add(WizWork_iSamleValue.NCOMPOSURE, Convert.ToDouble("0.00"));//소결의 여유값
        //                                    sqlParameter3.Add(WizWork_iSamleValue.NSHAPEVALUE, Convert.ToDouble("0.00"));//소결공정일 경유 조형 결과값, 계산결과
        //                                    sqlParameter3.Add(WizWork_iSamleValue.CREATEUSERID, sUserID);//작업자
        //                                }
        //                        }
        //                    }
        //                    else if (dgvSogulInfo.Visible == true)
        //                    {
        //                        for (int i = 0; i < dgvSogulInfo.Rows.Count; i++)
        //                        {
        //                            Dictionary<string, object> sqlParameter4 = new Dictionary<string, object>();
        //                            for (int j = 1; j < dgvSogulInfo.Rows[0].Cells.Count - 1; j++)
        //                            {
        //                                sqlParameter4.Add(WizWork_iSamleValue.JOBID, JobID);//iWkResult 프로시저로 만들어진 JobID
        //                                sqlParameter4.Add(WizWork_iSamleValue.SSAMPLEGBN, "4");//1:TOD 측정, 2:WID측정,3:L측정,4:소결측정*******이 if문에서는 소결값만 측정함 
        //                                sqlParameter4.Add(WizWork_iSamleValue.NSAMPLESEQ, j);//측정값번호
        //                                if (i == 0)//소결 측정값 입력
        //                                {
        //                                    sqlParameter4.Add(WizWork_iSamleValue.NSAMPLEVALUE, dgvSogulInfo.Rows[i].Cells[j].Value.ToString());//소결 측정값
        //                                    sqlParameter4.Add(WizWork_iSamleValue.NCOMPOSURE, Convert.ToDouble("0.00"));//소결의 여유값
        //                                    sqlParameter4.Add(WizWork_iSamleValue.NSHAPEVALUE, Convert.ToDouble("0.00"));//소결공정일 경유 조형 결과값, 계산결과
        //                                }
        //                                else if (i == 1)//여유 측정값 입력
        //                                {
        //                                    sqlParameter4.Add(WizWork_iSamleValue.NSAMPLEVALUE, Convert.ToDouble("0.00"));//소결 측정값
        //                                    sqlParameter4.Add(WizWork_iSamleValue.NCOMPOSURE, dgvSogulInfo.Rows[i].Cells[j].Value.ToString());//소결의 여유값
        //                                    sqlParameter4.Add(WizWork_iSamleValue.NSHAPEVALUE, Convert.ToDouble("0.00"));//소결공정일 경유 조형 결과값, 계산결과
        //                                }
        //                                else if (i == 2)//조형 결과값 입력
        //                                {
        //                                    sqlParameter4.Add(WizWork_iSamleValue.NSAMPLEVALUE, Convert.ToDouble("0.00"));//소결 측정값
        //                                    sqlParameter4.Add(WizWork_iSamleValue.NCOMPOSURE, Convert.ToDouble("0.00"));//소결의 여유값
        //                                    sqlParameter4.Add(WizWork_iSamleValue.NSHAPEVALUE, dgvSogulInfo.Rows[i].Cells[j].Value.ToString());//소결공정일 경유 조형 결과값, 계산결과
        //                                }
        //                                sqlParameter4.Add(WizWork_iSamleValue.CREATEUSERID, sUserID);//작업자
        //                            }
        //                        }
        //    ExecuteProcedure
        //}
        
    }
}
