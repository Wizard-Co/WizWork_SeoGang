using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WizCommon;

namespace WizWork.POPUP
{
    public partial class frm_tins_Find : Form
    {
        public string m_bFind ="";
        public string  m_bInspectPoint= "";

        public string itemCode { get { return this.txtItemCode.Text; } }
        public string itemName { get { return this.txtItemName.Text; } }

        public frm_tins_Find()
        {
            InitializeComponent();
        }

        public frm_tins_Find(string strbFind, string strbInspectPoin)
        {
            InitializeComponent();

            m_bFind = strbFind;
            m_bInspectPoint = strbInspectPoin;
        }

        private void frm_tins_Find_Load(object sender, EventArgs e)
        {

            this.txtItemCode.Text = "";
            this.txtItemName.Text = "";

            
            this.radKor.Checked = true;
            InitgrdDataGrid();
        }
        private void InitgrdDataGrid()
        {


            grdData.Columns.Clear();
            grdData.ColumnCount = 3;



            // Set the Colums Hearder Names
            grdData.Columns[0].Name = "No";
            grdData.Columns[0].Width = 60;


            grdData.Columns[1].Name = WizWork.TableData.Ins_InspectAutoSub.LOTID;
            grdData.Columns[1].HeaderText = "코드";
            grdData.Columns[1].Width = 140;
            grdData.Columns[1].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            grdData.Columns[2].Name = WizWork.TableData.Ins_InspectAutoSub.ARTICLE;
            grdData.Columns[2].HeaderText = "명칭";
            grdData.Columns[2].Width = 160;
            grdData.Columns[2].DefaultCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;

            
            grdData.Rows.Clear();

        }

        private void SetKorSetting()
        {
            this.chkEtc.Text = "기타";
            this.chkChar1.Text = "가";
            this.chkChar2.Text = "나";
            this.chkChar3.Text = "다";
            this.chkChar4.Text = "라";
            this.chkChar5.Text = "마";
            this.chkChar6.Text = "바";
            this.chkChar7.Text = "사";
            this.chkChar8.Text = "아";
            this.chkChar9.Text = "자";
            this.chkChar10.Text = "차";
            this.chkChar11.Text = "카";
            this.chkChar12.Text = "타";
            this.chkChar13.Text = "파";
            this.chkChar14.Text = "하";

            ///////////////////////////
            this.chkChar15.Text = "";
            this.chkChar15.Visible = false;

            this.chkChar16.Text = "";
            this.chkChar16.Visible = false;

            this.chkChar17.Text = "";
            this.chkChar17.Visible = false;

            this.chkChar18.Text = "";
            this.chkChar18.Visible = false;

            this.chkChar19.Text = "";
            this.chkChar19.Visible = false;

            this.chkChar20.Text = "";
            this.chkChar20.Visible = false;

            this.chkChar21.Text = "";
            this.chkChar21.Visible = false;

            this.chkChar22.Text = "";
            this.chkChar22.Visible = false;

            this.chkChar23.Text = "";
            this.chkChar23.Visible = false;

            this.chkChar24.Text = "";
            this.chkChar24.Visible = false;

            this.chkChar25.Text = "";
            this.chkChar25.Visible = false;

            this.chkChar26.Text = "";
            this.chkChar26.Visible = false;

            this.chkEtc.Checked = false;
            this.chkChar1.Checked = false;
            this.chkChar2.Checked = false;
            this.chkChar3.Checked = false;
            this.chkChar4.Checked = false;
            this.chkChar5.Checked = false;
            this.chkChar6.Checked = false;
            this.chkChar7.Checked = false;
            this.chkChar8.Checked = false;
            this.chkChar9.Checked = false;
            this.chkChar10.Checked = false;
            this.chkChar11.Checked = false;
            this.chkChar12.Checked = false;
            this.chkChar13.Checked = false;
            this.chkChar14.Checked = false;
            this.chkChar15.Checked = false;
            this.chkChar16.Checked = false;
            this.chkChar17.Checked = false;
            this.chkChar18.Checked = false;
            this.chkChar19.Checked = false;
            this.chkChar20.Checked = false;
            this.chkChar21.Checked = false;
            this.chkChar22.Checked = false;
            this.chkChar23.Checked = false;
            this.chkChar24.Checked = false;
            this.chkChar25.Checked = false;
            this.chkChar26.Checked = false;
            return;
        }
        private void SetEngSetting()
        {
            this.chkEtc.Text = "ELSE";
            this.chkChar1.Text = "A";
            this.chkChar2.Text = "B";
            this.chkChar3.Text = "C";
            this.chkChar4.Text = "D";
            this.chkChar5.Text = "E";
            this.chkChar6.Text = "F";
            this.chkChar7.Text = "G";
            this.chkChar8.Text = "H";
            this.chkChar9.Text = "I";
            this.chkChar10.Text = "J";
            this.chkChar11.Text = "K";
            this.chkChar12.Text = "L";
            this.chkChar13.Text = "M";
            this.chkChar14.Text = "N";

            ///////////////////////////
            this.chkChar15.Text = "O";
            this.chkChar15.Visible = true;

            this.chkChar16.Text = "P";
            this.chkChar16.Visible = true;

            this.chkChar17.Text = "Q";
            this.chkChar17.Visible = true;

            this.chkChar18.Text = "R";
            this.chkChar18.Visible = true;

            this.chkChar19.Text = "S";
            this.chkChar19.Visible = true;

            this.chkChar20.Text = "T";
            this.chkChar20.Visible = true;

            this.chkChar21.Text = "U";
            this.chkChar21.Visible = true;

            this.chkChar22.Text = "V";
            this.chkChar22.Visible = true;

            this.chkChar23.Text = "W";
            this.chkChar23.Visible = true;

            this.chkChar24.Text = "X";
            this.chkChar24.Visible = true;

            this.chkChar25.Text = "Y";
            this.chkChar25.Visible = true;

            this.chkChar26.Text = "Z";
            this.chkChar26.Visible = true;

            this.chkEtc.Checked = false;
            this.chkChar1.Checked = false;
            this.chkChar2.Checked = false;
            this.chkChar3.Checked = false;
            this.chkChar4.Checked = false;
            this.chkChar5.Checked = false;
            this.chkChar6.Checked = false;
            this.chkChar7.Checked = false;
            this.chkChar8.Checked = false;
            this.chkChar9.Checked = false;
            this.chkChar10.Checked = false;
            this.chkChar11.Checked = false;
            this.chkChar12.Checked = false;
            this.chkChar13.Checked = false;
            this.chkChar14.Checked = false;
            this.chkChar15.Checked = false;
            this.chkChar16.Checked = false;
            this.chkChar17.Checked = false;
            this.chkChar18.Checked = false;
            this.chkChar19.Checked = false;
            this.chkChar20.Checked = false;
            this.chkChar21.Checked = false;
            this.chkChar22.Checked = false;
            this.chkChar23.Checked = false;
            this.chkChar24.Checked = false;
            this.chkChar25.Checked = false;
            this.chkChar26.Checked = false;

           
            return;
        }

        private void radKor_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radKor.Checked == true)
            { 
                this.radEng.Checked = false;

                SetKorSetting();
            }

        }

        private void radEng_CheckedChanged(object sender, EventArgs e)
        {
            if (this.radEng.Checked == true)
            {
                
                this.radKor.Checked = false;

                SetEngSetting();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();

        }

        private void chkChar1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar1.Checked == true)
            {
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar1.Text.ToString(), this.chkChar2.Text.ToString());
            }

        }

        private void btnAooly_Click(object sender, EventArgs e)
        {
            SetItemApply();

        }
        public void SetItemApply()
        {
             DataGridViewRow drd = null;
            if (grdData.RowCount > 0)
            {
                  drd = grdData.SelectedRows[0];
                this.txtItemCode.Text = drd.Cells[1].Value.ToString();
                this.txtItemName.Text = drd.Cells[2].Value.ToString();
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("선택항목이 없습니다.");
                return;
            }
            this.Close();
        }

        private void chkChar2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar2.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar2.Text.ToString(), this.chkChar3.Text.ToString());
            }

        }

        private void chkChar3_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar3.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar3.Text.ToString(), this.chkChar4.Text.ToString());
            }

        }

        private void chkChar4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar4.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar4.Text.ToString(), this.chkChar5.Text.ToString());
            }
        }

        private void cmdUp_Click(object sender, EventArgs e)
        {
            if (grdData.RowCount > 0)
            {
                if (int.Parse(grdData.SelectedRows[0].Index.ToString()) != 0)
                {
                    grdData.Rows[int.Parse(grdData.SelectedRows[0].Index.ToString()) - 1].Selected = true;
                    grdData.CurrentCell = grdData.Rows[int.Parse(grdData.SelectedRows[0].Index.ToString())].Cells[0];
                }
            }
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            if (grdData.RowCount > 0)
            {

                if (int.Parse(grdData.SelectedRows[0].Index.ToString()) != (grdData.RowCount - 1))
                {
                    grdData.Rows[int.Parse(grdData.SelectedRows[0].Index.ToString()) + 1].Selected = true;
                    grdData.CurrentCell = grdData.Rows[int.Parse(grdData.SelectedRows[0].Index.ToString())].Cells[0];
                }
            }
        }

        private void chkChar5_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar5.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar5.Text.ToString(), this.chkChar6.Text.ToString());
            }
        }

        private void chkChar6_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar6.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar6.Text.ToString(), this.chkChar7.Text.ToString());
            }
        }

        private void chkChar7_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar7.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar7.Text.ToString(), this.chkChar8.Text.ToString());
            }
        }

        private void chkChar8_CheckedChanged(object sender, EventArgs e)
        {

            if (this.chkChar8.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar8.Text.ToString(), this.chkChar9.Text.ToString());
            }
        }

        private void chkChar9_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar9.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar9.Text.ToString(),this.chkChar10.Text.ToString());
            }
        }

        private void chkChar10_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar10.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar10.Text.ToString(), this.chkChar11.Text.ToString());
            }
        }

        private void chkChar11_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar11.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar11.Text.ToString(), this.chkChar12.Text.ToString());
            }
        }

        private void chkChar12_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar12.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar12.Text.ToString(), this.chkChar13.Text.ToString());
            }
        }

        private void chkChar13_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar13.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar13.Text.ToString(), this.chkChar14.Text.ToString());
            }
        }

        private void chkChar14_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar14.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar14.Text.ToString(), this.chkChar15.Text.ToString());
            }
        }

        private void chkChar15_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar15.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar15.Text.ToString(), this.chkChar16.Text.ToString());
            }
        }

        private void chkChar16_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar16.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar16.Text.ToString(), this.chkChar17.Text.ToString());
            }
        }

        private void chkChar17_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar17.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar17.Text.ToString(), this.chkChar18.Text.ToString());
            }
        }

        private void chkChar18_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar18.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar18.Text.ToString(), this.chkChar19.Text.ToString());
            }
        }

        private void chkChar19_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar19.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar19.Text.ToString(), this.chkChar20.Text.ToString());

            }
        }

        private void chkChar20_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar20.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar20.Text.ToString(), this.chkChar21.Text.ToString());

            }
        }

        private void chkChar21_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar21.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar21.Text.ToString(), this.chkChar22.Text.ToString());

            }
        }

        private void chkChar22_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar22.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar22.Text.ToString(), this.chkChar23.Text.ToString());

            }
        }

        private void chkChar23_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar23.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar23.Text.ToString(), this.chkChar24.Text.ToString());

            }
        }

        private void chkChar24_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar24.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar24.Text.ToString(), this.chkChar25.Text.ToString());

            }
        }

        private void chkChar25_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkChar25.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkChar25.Text.ToString(), this.chkChar26.Text.ToString());

            }
        }

        private void chkChar26_CheckedChanged(object sender, EventArgs e)
        {
            

            if (this.chkChar26.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;

                procQuery(this.chkChar26.Text.ToString(), this.chkChar26.Text.ToString());
            }
        }

        #region 프로시저호출


        public void procQuery(string strCharValue, string strNextValue)
        {

            int  intLangType = 9;
            string strNextValue2 = "";

            DataGridViewRow row = null;

            if (this.radKor.Checked == true)
            {
                intLangType = 0;           // 한글
            }
            if (this.radEng.Checked == true)
            {
                intLangType = 1;           // 영어
            }

            if(strCharValue =="ELSE" || strCharValue =="기타")
            {
                 intLangType = 3;          // 기타
            }
            if (intLangType == 0 || strCharValue == "기타")
            {
                strNextValue2 = "힝힝힝";
            }
            else
            {
                strNextValue2 = strNextValue;
            }

            if (intLangType == 1 || strCharValue == "ELSE")
            {
                strNextValue2 = "zzzz";
            }
            else
            {
                strNextValue2 = strNextValue;
            }

            try
            {
                //조회중일때 True
                Dictionary<string, object> sqlParameter = new Dictionary<string, object>();

                sqlParameter.Add("Value", strCharValue);
                sqlParameter.Add("NextValue", strNextValue2);
                sqlParameter.Add("Index", intLangType);
                sqlParameter.Add("Kind", m_bFind);
                sqlParameter.Add("InspectPoint", m_bInspectPoint); 


                DataSet dataSet = null;
                DataRow dr = null;

                //프로시저 호출
                dataSet = DataStore.Instance.ProcedureToDataSet("[xp_WizIns_sInspectItem]", sqlParameter, false);

                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    grdData.Rows.Clear();


                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        dr = null;
                        dr = dataSet.Tables[0].Rows[i];
                       
                        grdData.Rows.Add(i + 1,
                                            dr["ArticleID"],
                                            dr["Article"] 

                                            );

                      
                        row = grdData.Rows[i];
                        row.Height = 33;

                        grdData.Rows[0].Selected = true;
                    }
                }
                else
                {

                    grdData.Rows.Clear();
                }




            }
            catch (Exception excpt)
            {
                MessageBox.Show(string.Format("오류! 관리자에게 문의\r\n{0}", excpt.Message));
            }
        }
        #endregion

        private void chkEtc_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkEtc.Checked == true)
            {
                this.chkChar1.Checked = false;
                this.chkChar2.Checked = false;
                this.chkChar3.Checked = false;
                this.chkChar4.Checked = false;
                this.chkChar5.Checked = false;
                this.chkChar6.Checked = false;
                this.chkChar7.Checked = false;
                this.chkChar8.Checked = false;
                this.chkChar9.Checked = false;
                this.chkChar10.Checked = false;
                this.chkChar11.Checked = false;
                this.chkChar12.Checked = false;
                this.chkChar13.Checked = false;
                this.chkChar14.Checked = false;
                this.chkChar15.Checked = false;
                this.chkChar16.Checked = false;
                this.chkChar17.Checked = false;
                this.chkChar18.Checked = false;
                this.chkChar19.Checked = false;
                this.chkChar20.Checked = false;
                this.chkChar21.Checked = false;
                this.chkChar22.Checked = false;
                this.chkChar23.Checked = false;
                this.chkChar24.Checked = false;
                this.chkChar25.Checked = false;
                this.chkChar26.Checked = false;
                procQuery(this.chkEtc.Text.ToString(), this.chkEtc.Text.ToString());
            }
        }

        private void grdData_MouseClick(object sender, MouseEventArgs e)
        {
            SetItemApply();
        }
    }
}
