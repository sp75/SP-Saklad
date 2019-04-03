﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.Common;
using SP_Sklad.EditForm;
using SP_Sklad.SkladData;
using EntityState = System.Data.Entity.EntityState;

namespace SP_Sklad.WBDetForm
{
    public partial class frmWBReturnDetOut : DevExpress.XtraEditors.XtraForm
    {
        BaseEntities _db { get; set; }
        private int? _PosId { get; set; }
        private WaybillList _wb { get; set; }
        private WaybillDet _wbd { get; set; }
        private List<GetPosIn_Result> pos_in { get; set; }
        private GetActualRemainByWh_Result mat_remain { get; set; }
        public int _ka_id { get; set; }

        public frmWBReturnDetOut(BaseEntities db, int? PosId, WaybillList wb, int ka_id)
        {
            InitializeComponent();

            _db = db;
            _PosId = PosId;
            _wb = wb;
            _ka_id = ka_id;
        }

        private void frmWBReturnDetOut_Load(object sender, EventArgs e)
        {
            WHComboBox.Properties.DataSource = DBHelper.WhList;

            int wh_id = _wb.WaybillMove != null ? _wb.WaybillMove.SourceWid : 0;

            MatComboBox.Properties.DataSource = _db.WhMatGet(0, wh_id, _ka_id, DBHelper.ServerDateTime(), 0, "*", 0, "", DBHelper.CurrentUser.UserId, 0).ToList();
           

            if (_wb.WType == 4)
            {
                WHComboBox.Enabled = false;
                WhEditBtn.Enabled = false;
            }

            if (_PosId == null)
            {
                _wbd = new WaybillDet()
                {
                    WbillId = _wb.WbillId,
                    Amount = 0,
                    OnValue = _wb.OnValue,
                    WId = _wb.WaybillMove != null ? (int?)_wb.WaybillMove.SourceWid : null,
                    Discount = 0,
                    Nds = _wb.Nds
                };
            }
            else
            {
                _wbd = _db.WaybillDet.Find(_PosId);
            }

            if (_wbd != null)
            {
                WaybillDetBS.DataSource = _wbd;

                if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Unchanged)
                {
                    var w_mat_turn = _db.WMatTurn.Where(w => w.SourceId == _wbd.PosId).ToList();
                    if (w_mat_turn.Count > 0)
                    {
                        _db.WMatTurn.RemoveRange(w_mat_turn);
                        _db.SaveChanges();
                    }

                    GetContent();

                    foreach (var item in w_mat_turn)
                    {
                        if (pos_in.Any(a => a.PosId == item.PosId))
                        {
                            pos_in.FirstOrDefault(a => a.PosId == item.PosId).Amount = item.Amount;
                        }
                    }
                }
            }

            GetOk();
        }

        bool GetOk()
        {
            bool recult = (MatComboBox.EditValue != DBNull.Value && WHComboBox.EditValue != DBNull.Value && BasePriceEdit.EditValue != DBNull.Value && AmountEdit.EditValue != DBNull.Value);

            OkButton.Enabled = recult;

            RSVCheckBox.Checked = (OkButton.Enabled && pos_in != null && mat_remain != null && pos_in.Count > 0 && AmountEdit.Value <= mat_remain.CurRemainInWh && pos_in.Sum(s => s.FullRemain) >= AmountEdit.Value);
            if (RSVCheckBox.Checked)
            {
                foreach (var item in pos_in)
                {
                    if (item.FullRemain < item.Amount)
                    {
                        RSVCheckBox.Checked = false;
                        break;
                    }
                }
            }

            BotAmountEdit.Text = AmountEdit.Text;


      //      PriceNotNDSEdit.EditValue = BasePriceEdit.Value;
     //       TotalSumEdit.EditValue = Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(PriceNotNDSEdit.EditValue);
     //       SummAllEdit.EditValue = Convert.ToDecimal(AmountEdit.EditValue) * Convert.ToDecimal(DiscountPriceEdit.EditValue);
     //       TotalNdsEdit.EditValue = Convert.ToDecimal(SummAllEdit.EditValue) - Convert.ToDecimal(TotalSumEdit.EditValue);

            return recult;
        }

        private void MatComboBox_EditValueChanged(object sender, EventArgs e)
        {
            var row = (WhMatGet_Result)MatComboBox.GetSelectedDataRow();
            if (row == null)
            {
                return;
            }

            if (MatComboBox.ContainsFocus)
            {
                _wbd.MatId = row.MatId;
                GetContent();
            }

            labelControl24.Text = row.MsrName;
            labelControl27.Text = row.MsrName;
        }

        private void GetContent()
        {
            if (_wb.WType == -6)
            {
                var remain_in_wh = _db.MatRemainByWh(_wbd.MatId, 0, _ka_id, DBHelper.ServerDateTime(), "*", DBHelper.CurrentUser.UserId).ToList();
                WHComboBox.Properties.DataSource = remain_in_wh;

                if (remain_in_wh != null && WHComboBox.EditValue == DBNull.Value)
                {
                    WHComboBox.EditValue = remain_in_wh.First().WId;
                }
            }

            if (_wbd.WId == null || _wbd.MatId == 0)
            {
                return;
            }

            mat_remain = _db.GetActualRemainByWh(_wbd.WId, _wbd.MatId).FirstOrDefault();

            if (mat_remain != null)
            {
                RemainWHEdit.EditValue = mat_remain.CurRemainInWh;
                RsvEdit.EditValue = mat_remain.Rsv;
                CurRemainEdit.EditValue = mat_remain.Remain;
            }

            pos_in = _db.GetPosIn(_wb.OnDate, _wbd.MatId, _wbd.WId, _ka_id, DBHelper.CurrentUser.UserId).OrderByDescending(o => o.OnDate).ToList();

            if (pos_in.Any())
            {
                _wbd.Price = pos_in.First().Price;

                _wbd.BasePrice = pos_in.First().BasePrice  ; //?? pos_in.First().Price;
                BasePriceEdit.EditValue = _wbd.BasePrice;

                _wbd.Nds = pos_in.First().Nds;
            }

            SetAmount();
        }

        private void SetAmount()
        {
            if (pos_in == null || mat_remain == null)
            {
                return;
            }

            decimal? sum_amount = pos_in.Sum(s => s.Amount);
            decimal? sum_full_remain = pos_in.Sum(s => s.FullRemain);

            if (pos_in.Count > 0 && AmountEdit.Value <= mat_remain.CurRemainInWh && sum_amount != AmountEdit.Value)
            {
                sum_amount = AmountEdit.Value;
                bool stop = false;
                foreach (var item in pos_in)
                {
                    decimal? remain = item.FullRemain;
                    if (!stop)
                    {
                        if (remain >= sum_amount)
                        {
                            item.Amount = sum_amount;
                            sum_amount -= remain;
                            stop = true;
                        }
                        else
                        {
                            item.Amount = remain;
                            sum_amount -= remain;
                        }
                    }
                    else item.Amount = 0;
                }
                RSVCheckBox.Checked = (sum_amount <= 0);

            }
            else RSVCheckBox.Checked = false;

            if (AmountEdit.Value <= sum_full_remain) RSVCheckBox.Checked = false;

            GetOk();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            int num = _wb.WaybillDet.Count();
            try
            {
                if (RSVCheckBox.Checked && !_db.WMatTurn.Any(w => w.SourceId == _wbd.PosId) && _db.UserAccessWh.Any(a => a.UserId == DBHelper.CurrentUser.UserId && a.WId == _wbd.WId && a.UseReceived))
                {
                    var sate = _db.Entry<WaybillDet>(_wbd).State;
                    if (sate == EntityState.Modified || sate == EntityState.Unchanged)
                    {
                        _db.WaybillDet.Remove(_wbd);
                    }

                    foreach (var item in pos_in.Where(w => w.Amount > 0))
                    {
                        var wbd = _db.WaybillDet.Add(new WaybillDet()
                        {
                            WbillId = _wb.WbillId,
                            Price = item.Price,
                            BasePrice = item.BasePrice,
                            Nds = item.Nds,
                            CurrId = item.CurrId,
                            OnDate = _wb.OnDate,
                            WId = item.WId,
                            Num = ++num,
                            Amount = item.Amount.Value,
                            MatId = item.MatId,
                        });
                        _db.SaveChanges();

                        _db.WMatTurn.Add(new WMatTurn
                        {
                            PosId = item.PosId,
                            WId = item.WId,
                            MatId = item.MatId,
                            OnDate = _wb.OnDate,
                            TurnType = 2,
                            Amount = Convert.ToDecimal(item.Amount),
                            SourceId = wbd.PosId
                        });
                        _db.SaveChanges();
                    }
                }

                Close();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException exp)
            {
                _db.UndoAllChanges();

                throw exp;
            }
        }

        private void AmountEdit_EditValueChanged(object sender, EventArgs e)
        {
            SetAmount();
        }

        private void RSVCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RSVCheckBox.Checked && RSVCheckBox.ContainsFocus)
            {
                GetOk();
            }
        }

        private void frmWBReturnDetOut_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_db.Entry<WaybillDet>(_wbd).State == EntityState.Modified)
            {
                _db.Entry<WaybillDet>(_wbd).Reload();
            }
        }

        private void WHComboBox_EditValueChanged(object sender, EventArgs e)
        {
            if (!WHComboBox.ContainsFocus)
            {
                return;
            }
            _wbd.WId = (int)WHComboBox.EditValue;

            GetContent();
            GetOk();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (pos_in == null)
            {
                return;
            }

            var pos = new frmInParty(pos_in);
            pos.Text = "Прибуткові партії: " + MatComboBox.Text;
            pos.ShowDialog();
            _wbd.Amount = pos_in.Sum(s => s.Amount).Value;
            AmountEdit.Value = _wbd.Amount;

            GetOk();
        }

        private void MatEditBtn_Click(object sender, EventArgs e)
        {
            var f = new frmWhCatalog(1);

            f.uc.whKagentList.EditValue = _ka_id;
            f.uc.whKagentList.Enabled = false;
            f.uc.OnDateEdit.Enabled = false;
            f.uc.bar3.Visible = false;
            f.uc.ByWhBtn.Down = true;
            f.uc.splitContainerControl1.SplitterPosition = 0;
            f.uc.WHTreeList.DataSource = new BaseEntities().GetWhTree(DBHelper.CurrentUser.UserId, 2).Where(w => w.GType == 1 && w.Num == _wbd.WId).ToList();
            f.uc.GrpNameGridColumn.GroupIndex = 0;

            f.uc.isDirectList = true;
            if (f.ShowDialog() == DialogResult.OK)
            {
                _wbd.MatId = f.uc.focused_wh_mat.MatId;
                MatComboBox.EditValue = _wbd.MatId;
                GetContent();
            }
        }

        private void WhEditBtn_Click(object sender, EventArgs e)
        {
            WHComboBox.EditValue = IHelper.ShowDirectList(WHComboBox.EditValue, 2);

            GetContent();
            GetOk();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(_wbd.MatId);
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(_wbd.MatId, _db);
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowTurnMaterial(_wbd.MatId);
        }


    }
}
