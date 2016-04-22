﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using SP_Sklad.SkladData;
using SP_Sklad.WBDetForm;

namespace SP_Sklad.WBForm
{
    public partial class frmWBReturnIn : Form
    {
        private int _wtype { get; set; }
        BaseEntities _db { get; set; }
        private int? _wbill_id { get; set; }
        private DbContextTransaction current_transaction { get; set; }
        private WaybillList wb { get; set; }
        private GetWayBillDetOut_Result wbd_row { get; set; }
        private IQueryable<GetWaybillDetIn_Result> wbd_list { get; set; }

        public frmWBReturnIn(int wtype, int? wbill_id)
        {
            _wtype = wtype;
            _wbill_id = wbill_id;
            _db = new BaseEntities();
            current_transaction = _db.Database.BeginTransaction(/*IsolationLevel.RepeatableRead*/);

            InitializeComponent();
        }

        private void frmWBReturnIn_Load(object sender, EventArgs e)
        {
            KagentComboBox.Properties.DataSource = DBHelper.Kagents;
            PersonComboBox.Properties.DataSource = DBHelper.Persons;

            if (_wbill_id == null)
            {
                wb = _db.WaybillList.Add(new WaybillList()
                {
                    WType = _wtype,
                    OnDate = DBHelper.ServerDateTime(),
                    Num = DB.SkladBase().GetCounter("wb(6)").FirstOrDefault(),
                    CurrId = DBHelper.Currency.FirstOrDefault(w => w.Def == 1).CurrId,
                    OnValue = 1,
                    PersonId = DBHelper.CurrentUser.KaId
                });
                _db.SaveChanges();

                _wbill_id = wb.WbillId;
            }
            else
            {
                try
                {
                    UpdLockWB();
                    _db.Entry<WaybillList>(wb).State = System.Data.Entity.EntityState.Modified;
                    _db.Entry<WaybillList>(wb).Property(f => f.SummPay).IsModified = false;
                }
                catch
                {

                    Close();
                }

            }

            if (wb != null)
            {
                TurnDocCheckBox.EditValue = wb.Checked;

                KagentComboBox.DataBindings.Add(new Binding("EditValue", wb, "KaId", true, DataSourceUpdateMode.OnValidation));
                PersonComboBox.DataBindings.Add(new Binding("EditValue", wb, "PersonId", true, DataSourceUpdateMode.OnValidation));
                NumEdit.DataBindings.Add(new Binding("EditValue", wb, "Num"));
                OnDateDBEdit.DataBindings.Add(new Binding("EditValue", wb, "OnDate"));
                NotesEdit.DataBindings.Add(new Binding("EditValue", wb, "Notes"));
                ReasonEdit.DataBindings.Add(new Binding("EditValue", wb, "Reason"));

                payDocUserControl1.OnLoad(_db, wb);
            }

            RefreshDet();
        }

        private void UpdLockWB()
        {
            wb = _db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK, NOWAIT) where WbillId = {0}", _wbill_id).FirstOrDefault();
        }

        private void RefreshDet()
        {
            wbd_list = _db.GetWaybillDetIn(_wbill_id);
            var dr = WBDetReInGridView.GetRow(WBDetReInGridView.FocusedRowHandle) as GetWaybillDetIn_Result;

            WBDetReInGridControl.DataSource = null;
            WBDetReInGridControl.DataSource = wbd_list;

            WBDetReInGridView.FocusedRowHandle = FindRowHandleByRowObject(WBDetReInGridView, dr);

            GetOk();
        }

        private int FindRowHandleByRowObject(GridView view, GetWaybillDetIn_Result dr)
        {
            if (dr != null)
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    if (dr.PosId == (view.GetRow(i) as GetWaybillDetIn_Result).PosId)
                    {
                        return i;
                    }
                }
            }
            return GridControl.InvalidRowHandle;
        }

        bool GetOk()
        {
            bool recult = (!String.IsNullOrEmpty(NumEdit.Text) && KagentComboBox.EditValue != null && OnDateDBEdit.EditValue != null && WBDetReInGridView.DataRowCount > 0);


            AddMaterialBtn.Enabled = KagentComboBox.EditValue != DBNull.Value;

            EditMaterialBtn.Enabled = WBDetReInGridView.DataRowCount > 0;
            DelMaterialBtn.Enabled = EditMaterialBtn.Enabled;
            RsvInfoBtn.Enabled = EditMaterialBtn.Enabled;
            MatInfoBtn.Enabled = EditMaterialBtn.Enabled;

            KagentComboBox.Enabled = WBDetReInGridView.DataRowCount == 0;
            KAgentBtn.Enabled = KagentComboBox.Enabled;
            KagBalBtn.Enabled = KagentComboBox.EditValue != DBNull.Value;

            OkButton.Enabled = recult;
            return recult;
        }

        private void frmWBReturnIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (current_transaction.UnderlyingTransaction.Connection != null)
            {
                current_transaction.Rollback();
            }

            _db.Dispose();
            current_transaction.Dispose();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (!DBHelper.CheckOutDate(wb, _db, OnDateDBEdit.DateTime))
            {
                return;
            }

            wb.UpdatedAt = DateTime.Now;
            _db.SaveChanges();

            payDocUserControl1.Execute(wb.WbillId);

            current_transaction.Commit();

            if (TurnDocCheckBox.Checked)
            {
                _db.ExecuteWayBill(wb.WbillId, null);
            }

            Close();
        }

        private void AddMaterialBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var df = new frmWBReturnDetIn(_db, null, wb);
            if (df.ShowDialog() == DialogResult.OK)
            {
                current_transaction = current_transaction.CommitRetaining(_db);
                UpdLockWB();
                RefreshDet();
            }
        }

        private void OnDateDBEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (!OnDateDBEdit.ContainsFocus) return;

            wb.OnDate = OnDateDBEdit.DateTime;
        }

        private void KagentComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (!KagentComboBox.ContainsFocus) return;

            wb.KaId = (int)KagentComboBox.EditValue;
        }

        private void frmWBReturnIn_Shown(object sender, EventArgs e)
        {

            OnDateDBEdit.Enabled = (DBHelper.CurrentUser.EnableEditDate == 1);
            NowDateBtn.Enabled = OnDateDBEdit.Enabled;

            GetOk();
        }
    }
}
