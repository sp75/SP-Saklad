﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.Reports;
using SP_Sklad.SkladData;

namespace SP_Sklad
{
    public partial class frmMatRSV : DevExpress.XtraEditors.XtraForm
    {
        private int _mat_id { get; set; }
        private BaseEntities _db { get; set; }
        public frmMatRSV(int MatId , BaseEntities db)
        {
            InitializeComponent();
            _mat_id = MatId;
            _db = db;

        }

        private void frmMatRSV_Load(object sender, EventArgs e)
        {
            wTypeList.Properties.DataSource = new List<object>() { new { Id = 0, Name = "Усі" } }.Concat(new BaseEntities().DocType.Select(s => new { s.Id, s.Name })).ToList();
            wTypeList.EditValue = 0;

            KagentComboBox.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(_db.Kagent.Select(s => new { s.KaId, s.Name }));
            KagentComboBox.EditValue = 0;
            
            wbStartDate.DateTime = DateTimeDayOfMonthExtensions.FirstDayOfMonth(DateTime.Now);
            wbEndDate.DateTime = DateTime.Now.AddDays(1);

            Text = Text + "[ " + _db.Materials.Where(w => w.MatId == _mat_id).Select(s => s.Name).First() + " ]";

            GetRsv();
        }
        private void GetRsv()
        {
            DocListBindingSource.DataSource = _db.GetMatRsv( _mat_id, (int)KagentComboBox.EditValue,wbStartDate.DateTime, wbEndDate.DateTime, (int)wTypeList.EditValue).ToList();
        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
            if (wbStartDate.ContainsFocus || wbEndDate.ContainsFocus || KagentComboBox.ContainsFocus || wTypeList.ContainsFocus)
            {
                GetRsv();
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = bandedGridView1.GetFocusedRow() as GetMatRsv_Result;
            if (row != null)
            {
                PrintDoc.Show(row.DocId.Value, row.WType, DB.SkladBase());
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var path = Path.Combine(Application.StartupPath, "expotr.pdf");
            gridControl1.ExportToPdf(path);

            Process.Start(path);
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = bandedGridView1.GetFocusedRow() as GetMatRsv_Result;
            if (row != null)
            {
                FindDoc.Find(row.DocId, row.WType, row.OnDate);
            }
        }
    }
}
