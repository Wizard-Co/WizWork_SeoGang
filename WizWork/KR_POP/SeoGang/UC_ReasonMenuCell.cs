using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizWork
{
    public partial class UC_ReasonMenuCell : UserControl
    {
        Frm_SelectReason FormReason = null;

        public UC_ReasonMenuCell()
        {
            InitializeComponent();
        }

        public UC_ReasonMenuCell(Frm_SelectReason ParentForm)
        {
            InitializeComponent();

            FormReason = ParentForm;
        }

        // 부적합 원인 선택없이 창 닫기
        private void btnClose_Click(object sender, EventArgs e)
        {
            FormReason.btnClose_Click(sender, e);
        }

        // 부적합 원인 가로세로 칸 수 설정
        private void btnSetCellCount_Click(object sender, EventArgs e)
        {
            FormReason.btnCellSetting_Click(sender, e);
        }

        // label 크기에 따라 폰트 설정
        public Font AutoFontSize(Label label, String text)
        {
            Font ft;
            Graphics gp;
            SizeF sz;
            Single Faktor, FaktorX, FaktorY;

            gp = label.CreateGraphics();
            sz = gp.MeasureString(text, label.Font);
            gp.Dispose();

            FaktorX = (label.Width) / sz.Width;
            FaktorY = (label.Height) / sz.Height;

            if (FaktorX > FaktorY)
                Faktor = FaktorY;
            else
                Faktor = FaktorX;
            ft = label.Font;

            return new Font(ft.Name, ft.SizeInPoints * (Faktor), FontStyle.Bold);
        }

        // 리사이즈 된 lbX의 폰트를 각 컨트롤에 적용
        private void tlpChildTlp_SizeChanged(object sender, EventArgs e)
        {
            lbX.Font = AutoFontSize(lbX, "X");
            lbY.Font = lbX.Font;
            cbbCountX.Font = lbX.Font;
            cbbCountY.Font = lbX.Font;
            btnSetCellCount.Font = lbX.Font;
            btnClose.Font = lbX.Font;
        }
    }
}
