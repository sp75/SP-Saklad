﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;

namespace SP_Sklad.EditForm
{
    public partial class frmCashdesksEdit : DevExpress.XtraEditors.XtraForm
    {
        private BaseEntities _db { get; set; }
        private int? _CashId { get; set; }
        private CashDesks cd { get; set; }

        public frmCashdesksEdit(int? CashId = null)
        {
            _CashId = CashId;
            _db = DB.SkladBase();

            InitializeComponent();

            EntEdit.Properties.DataSource = DBHelper.EnterpriseList;
        }

        private void frmCashdesksEdit_Load(object sender, EventArgs e)
        {
            if (_CashId == null)
            {
                cd = _db.CashDesks.Add(new CashDesks
                {
                    Deleted = 0,
                    Def = _db.CashDesks.Any(a => a.Def == 1) ? 0 : 1,
                    Name = ""
                });

                Text = "Додати нову касу:";
            }
            else
            {
                cd = _db.CashDesks.Find(_CashId);

                Text = "Властвості каси:";
            }

            CashDesksBS.DataSource = cd;

            DefCheckBox.Enabled = !DefCheckBox.Checked;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            _db.SaveChanges();
        }

        private void KTypeLookUpEdit_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 1)
            {
                EntEdit.EditValue = null;
            }
        }
    }
}
