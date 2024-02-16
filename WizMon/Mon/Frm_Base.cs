using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WizMon
{
    public partial class Frm_Base : Form, IProcedure
    {
        public Frm_Base()
        {
            InitializeComponent();
        }

        #region IProcedure 멤버

        public virtual void procClear()
        {
            
        }

        public virtual void procDelete()
        {
           
        }

        public virtual void procExcel()
        {
            
        }

        public virtual void procInsert()
        {
            
        }

        public virtual void procPrint()
        {
            
        }

        public virtual void procQuery()
        {
            
        }

        public virtual void procSave()
        {
            
        }

        public virtual void procUpdate()
        {
            
        }

        #endregion


    }
}
