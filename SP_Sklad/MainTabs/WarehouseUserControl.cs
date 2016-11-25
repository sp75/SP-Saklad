﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.SkladData;
using SP_Sklad.WBForm;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;
using SP_Sklad.Common;
using SP_Sklad.Properties;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SP_Sklad.EditForm;
using SP_Sklad.Reports;

namespace SP_Sklad.MainTabs
{
    public partial class WarehouseUserControl : DevExpress.XtraEditors.XtraUserControl
    {
        int wid = 0;
        string wh_list = "*";
        int cur_wtype = 0;

        public bool isDirectList { get; set; }
        public bool isMatList { get; set; }
        public List<CustomMatListWH> custom_mat_list { get; set; }
        public WaybillList wb { get; set; }
        public Object resut { get; set; }
        public WhMatGet_Result focused_wh_mat
        {
            get { return WhMatGridView.GetFocusedRow() as WhMatGet_Result; }
        }

        public GetWayBillListWh_Result focused_wb
        {
            get { return WbGridView.GetFocusedRow() as GetWayBillListWh_Result; }
        }

        public class CustomMatListWH : CustomMatList
        {
            public decimal? Discount { get; set; }
            public int? PTypeId { get; set; }
        }

        GetWhTree_Result focused_tree_node { get; set; }
        GetWayBillListWh_Result focused_row { get; set; }

        public int? set_tree_node { get; set; }

        public WarehouseUserControl()
        {
            InitializeComponent();
        }

        private void WarehouseUserControl_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                custom_mat_list = new List<CustomMatListWH>();
                MatListGridControl.DataSource = custom_mat_list;

                whContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
                OnDateEdit.DateTime = DateTime.Now;

                whKagentList.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().Kagent.Select(s => new { s.KaId, s.Name }));
                if (whKagentList.EditValue == null || whKagentList.EditValue == DBNull.Value)
                {
                    whKagentList.EditValue = 0;
                }

                WhComboBox.Properties.DataSource = new List<object>() { new { WId = "*", Name = "Усі" } }.Concat(new BaseEntities().Warehouse.Select(s => new { WId = s.WId.ToString(), s.Name }).ToList());
                WhComboBox.EditValue = "*";

                wbSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
                wbSatusList.EditValue = -1;

                wbStartDate.EditValue = DateTime.Now.AddDays(-30);
                wbEndDate.EditValue = DateTime.Now;

                repositoryItemLookUpEdit1.DataSource = DBHelper.WhList();
                repositoryItemLookUpEdit2.DataSource = DB.SkladBase().PriceTypes.ToList();

                if (WHTreeList.DataSource == null)
                {
                    GetTree(1);
                }
            }
        }

        void GetTree(int type)
        {
            WHTreeList.DataSource = new BaseEntities().GetWhTree(DBHelper.CurrentUser.UserId, type).ToList();
            WHTreeList.ExpandToLevel(0);

            if (set_tree_node != null)
            {
                WHTreeList.FocusedNode = WHTreeList.FindNodeByFieldValue("Id", set_tree_node);
                set_tree_node = null;
            }
        }

        private void WHTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            CopyItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;
            focused_tree_node = WHTreeList.GetDataRecordByNode(e.Node) as GetWhTree_Result;

            cur_wtype = focused_tree_node.WType != null ? focused_tree_node.WType.Value : 0;
            RefrechItemBtn.PerformClick();
 
            whContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;

            if (focused_tree_node.FunId != null)
            {
                History.AddEntry(new HistoryEntity { FunId = focused_tree_node.FunId.Value, MainTabs = 2 });
            }
        }

        void GetWayBillList(int? wtyp)
        {
            if (wtyp == null && wbSatusList.EditValue == null || WhComboBox.EditValue == null || WHTreeList.FocusedNode == null)
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;

            int top_row = WbGridView.TopRowIndex;
            GetWayBillListWhBS.DataSource = DB.SkladBase().GetWayBillListWh(satrt_date.Date, end_date.Date.AddDays(1), wtyp, (int)wbSatusList.EditValue, WhComboBox.EditValue.ToString()).OrderByDescending(o => o.OnDate).ToList();
            WbGridView.TopRowIndex = top_row;
        }

        private void ByGrpBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTree(1);
        }

        private void ByWhBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTree(2);
        }

        private void PosGridControl_Click(object sender, EventArgs e)
        {

        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 2:
                    if (focused_tree_node.Id == 48)
                    {
                        using (var wb_move = new frmWayBillMove())
                        {
                            wb_move.ShowDialog();
                        }
                    }

                    if (focused_tree_node.Id == 58)
                    {
                        using (var wb_on = new frmWBWriteOn())
                        {
                            wb_on.ShowDialog();
                        }
                    }

                    if (focused_tree_node.Id == 54)
                    {
                        using (var wb_on = new frmWBWriteOff())
                        {
                            wb_on.ShowDialog();
                        }
                    }

                    if (focused_tree_node.Id == 104)
                    {
                        using (var wb_inv = new frmWBInventory())
                        {
                            wb_inv.ShowDialog();
                        }
                    }

                    break;
            }

            RefrechItemBtn.PerformClick();
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            using (var db = new BaseEntities())
            {
                switch (focused_tree_node.GType)
                {
                    case 2:
                        WhDocEdit.WBEdit(WbGridView.GetFocusedRow() as GetWayBillListWh_Result);
                        break;

                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var dr = WbGridView.GetFocusedRow() as GetWayBillListWh_Result;
            if (dr == null)
            {
                return;
            }

            using (var db = new BaseEntities())
            {

                try
                {
                    switch (focused_tree_node.GType)
                    {
                        case 2: db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK) where WbillId = {0}", dr.WBillId).FirstOrDefault();
                            break;
                    }
                    if (MessageBox.Show(Resources.delete_wb, "Відалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        switch (focused_tree_node.GType)
                        {
                            case 2:
                                db.DeleteWhere<WaybillList>(w=> w.WbillId ==  dr.WBillId.Value);
                                break;

                        }
                        db.SaveChanges();
                    }
                }
                catch
                {
                    MessageBox.Show(Resources.deadlock);
                }
                /*    finally
                    {
               //         trans.Commit();
                    }*/
            }

            RefrechItemBtn.PerformClick();
        }

        private void WbGridView_DoubleClick(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            GridHitInfo info = view.CalcHitInfo(pt);

            if (info.InRow || info.InRowCell)
            {
                EditItemBtn.PerformClick();
            }
        }

        private void ExecuteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (var db = new BaseEntities())
            {
                switch (focused_tree_node.GType)
                {
                    case 2:
                        if (focused_row == null)
                        {
                            return;
                        }
                        
                        var wb = db.WaybillList.Find(focused_row.WBillId);
                        if (wb != null)
                        {
                            if (wb.Checked == 1)
                            {
                                DBHelper.StornoOrder(db, focused_row.WBillId.Value);
                            }
                            else
                            {
                                DBHelper.ExecuteOrder(db, focused_row.WBillId.Value);
                            }
                        }
                        else
                        {
                            MessageBox.Show(Resources.not_find_wb);
                        }
                        break;

                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node == null)
            {
                return;
            }

            switch (focused_tree_node.GType.Value)
            {
                case 1:
                    int grp_id = 0;

                    string grp = "";

                    if (ByGrpBtn.Down) grp_id = focused_tree_node.Num;
                    else wid = focused_tree_node.Num;

                    if (ViewDetailTree.Down && ByGrpBtn.Down && focused_tree_node.Num != 0)
                    {
                        grp = focused_tree_node.Num.ToString();
                    }

                    int top_row = WhMatGridView.TopRowIndex;
                    WhMatGetBS.DataSource =DB.SkladBase().WhMatGet(grp_id, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, ShowEmptyItemsCheck.Checked ? 1 : 0, wh_list, ShowAllItemsCheck.Checked ? 1 : 0, grp, DBHelper.CurrentUser.UserId, ViewDetailTree.Down ? 1 : 0).ToList();
                    WhMatGridView.TopRowIndex = top_row;
                    break;

                case 2:
                    GetWayBillList(focused_tree_node.WType);
                    /* cxGrid1Level1->GridView = cxGrid1DBTableView1;
                      cxGridLevel6->GridView = cxGridDBTableView1;
                      GET_RelDocList->DataSource = WayBillListDS;
                      WBTopPanelDate->Edit();
                      if (WhTreeDataID->Value == 48) WBTopPanelDateWTYPE->Value = 4;
                      if (WhTreeDataID->Value == 58) WBTopPanelDateWTYPE->Value = 5;
                      if (WhTreeDataID->Value == 54) WBTopPanelDateWTYPE->Value = -5;
                      if (WhTreeDataID->Value == 104)
                      {
                          WBTopPanelDateWTYPE->Value = 7;
                          cxGrid1Level1->GridView = cxGrid2DBTableView1;
                          cxGridLevel6->GridView = cxGrid7DBTableView1;
                      }

                      WBTopPanelDate->Post();
                      WayBillList->FullRefresh();
                      WayBillList->Refresh();*/

                    break;
            }
        }

        private void RefreshWhBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void ViewDetailTree_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ByGrpBtn.Down = true;
            RefrechItemBtn.PerformClick();
        }

        private void WhMatGridView_DoubleClick(object sender, EventArgs e)
        {
            if (isMatList)
            {
                AddItem.PerformClick();
            }
            else if (isDirectList)
            {
                var frm = this.Parent as frmWhCatalog;
                frm.OkButton.PerformClick();
            }
            else
            {
                IHelper.ShowTurnMaterial(focused_wh_mat.MatId.Value);
            }
        }

        private void AddItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var remain_in_wh = DB.SkladBase().MatRemainByWh(focused_wh_mat.MatId.Value, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, wh_list).ToList();
            var p_type = (wb.Kontragent != null ? (wb.Kontragent.PTypeId ?? DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId) : DB.SkladBase().PriceTypes.First(w => w.Def == 1).PTypeId);
            var price = DB.SkladBase().GetListMatPrices(focused_wh_mat.MatId, wb.CurrId, p_type).FirstOrDefault();

            custom_mat_list.Add(new CustomMatListWH
            {
                Num = custom_mat_list.Count() + 1,
                MatId = focused_wh_mat.MatId.Value,
                Name = focused_wh_mat.MatName,
                Amount = 1,
                Price = price.Price ?? 0,
                WId = remain_in_wh.Any() ? remain_in_wh.First().WId : DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId,
                PTypeId = price != null ? price.PType : null,
                Discount = DB.SkladBase().GetDiscount(wb.KaId, focused_wh_mat.MatId).FirstOrDefault() ?? 0.00m
            });

            MatListGridView.RefreshData();
        }

        private void DelItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MatListGridView.DeleteSelectedRows();
        }

        private void whKagentList_EditValueChanged(object sender, EventArgs e)
        {
            if (OnDateEdit.ContainsFocus || whKagentList.ContainsFocus)
            {
                RefrechItemBtn.PerformClick();
            }
        }

        private void BarCodeEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && !String.IsNullOrEmpty(BarCodeEdit.Text))
            {
                var BarCodeSplit = BarCodeEdit.Text.Split('+');
                String kod = BarCodeSplit[0];

                var row = WhMatGetBS.List.OfType<WhMatGet_Result>().ToList().Find(f => f.BarCode == kod);
                var pos = WhMatGetBS.IndexOf(row);
                WhMatGetBS.Position = pos;

                if (row != null && MatListTabPage.PageVisible)
                {
                    if (BarCodeSplit.Count() > 2)
                    {
                        var price = Convert.ToDecimal(BarCodeSplit[1] + "," + BarCodeSplit[2]);
                        var frm = new frmMatListEdit(row.MatName);
                        frm.PriceEdit.EditValue = price;

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            var wh_row = WhRemainGridView.GetFocusedRow() as MatRemainByWh_Result;

                            custom_mat_list.Add(new CustomMatListWH
                            {
                                Num = custom_mat_list.Count() + 1,
                                MatId = row.MatId.Value,
                                Name = row.MatName,
                                Amount = frm.AmountEdit.Value,
                                Price = frm.PriceEdit.Value,
                                WId = wh_row != null ? wh_row.WId : DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId,
                                PTypeId = null,
                                Discount = DB.SkladBase().GetDiscount(wb.KaId, row.MatId).FirstOrDefault() ?? 0.00m
                            });

                            MatListGridView.RefreshData();
                        }

                    }
                    else
                    {
                        AddItem.PerformClick();
                    }

                }

                BarCodeEdit.Text = "";
            }
        }

        private void WhMatGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            MatPopupMenu.ShowPopup(Control.MousePosition); 
        }

        private void MatTurnInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
             IHelper.ShowTurnMaterial(focused_wh_mat.MatId);
        }

        private void RsvInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatRSV(focused_wh_mat.MatId, DB.SkladBase());
        }

        private void MatInfoBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowMatInfo(focused_wh_mat.MatId);
        }

        private void RecalcRemainsMatBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
            using (var db = DB.SkladBase())
            {
                var pos = db.PosRemains.Where(w => w.MatId == focused_wh_mat.MatId).Select(s => new { s.PosId, s.WId, s.OnDate }).ToList();
                foreach (var item in pos)
                {
                    db.SP_RECALC_REMAINS(item.PosId, focused_wh_mat.MatId, item.WId, item.OnDate, 0);
                }

                db.SaveChanges();
            }
            
       //     DB.SkladBase().RecalcRemainsMat(focused_wh_mat.MatId);
            RefreshWhBtn.PerformClick();
        }

        private void RecalcRemainsAllMatBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            String errRECALC = "";

            for (int i = 0; WhMatGridView.RowCount > i; ++i)
            {
                var row = WhMatGridView.GetRow(i) as WhMatGet_Result;
                try
                {
                    DB.SkladBase().RecalcRemainsMat(row.MatId);
                }
                catch
                {
                    errRECALC += row.MatName + ", ";
                }
            }
            if (errRECALC != "") MessageBox.Show("Не вдалось перерахувати залишки по деяким позиціям: " + errRECALC);
            else MessageBox.Show("Залишки по всім позиціям перераховано!");

            RefreshWhBtn.PerformClick();
        }

        private void DeboningMatBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var rec = DB.SkladBase().MatRecipe.FirstOrDefault(w => w.MatId == focused_wh_mat.MatId && w.RType == 2);
            var wh_remain =  WhRemainGridView.GetFocusedRow() as MatRemainByWh_Result ;
            if (rec != null)
            {
                using (var f = new frmWBDeboning() { rec_id = rec.RecId, source_wid = wh_remain .WId})
                {
                    f.ShowDialog();
                }

                RefreshWhBtn.PerformClick();
            }
            else MessageBox.Show("Не можливо виконати овалку, не знайдено рецепт!");
        }

        private void ShowEmptyItemsCheck_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshWhBtn.PerformClick();
        }

        private void ShowAllItemsCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshWhBtn.PerformClick();
        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            GetWhBottomInfo(focused_wh_mat);
        }

        private void GetWhBottomInfo(WhMatGet_Result row)
        {
            if (row == null)
            {
                RemainOnWhGrid.DataSource = null;
                PosGridControl.DataSource = null;
                GetOrderedBS.DataSource = null;
                MatChangeGridControl.DataSource = null;
                return;
            }

            switch (xtraTabControl1.SelectedTabPageIndex)
            {
                case 0:

                    RemainOnWhGrid.DataSource = DB.SkladBase().MatRemainByWh(row.MatId, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, wh_list).ToList();
                    break;
                case 1:

                    PosGridControl.DataSource = DB.SkladBase().PosGet(row.MatId, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, 0, wh_list).ToList();
                    break;
                case 2:

                    GetOrderedBS.DataSource = DB.SkladBase().GetOrdered(row.MatId, wid, (int)whKagentList.EditValue, OnDateEdit.DateTime, 0, wh_list).ToList();
                    break;
                case 3:
                    MatChangeGridControl.DataSource = DB.SkladBase().GetMatChange(row.MatId).ToList();
                    break;
            }
        }

        private void DocsPopupMenu_Popup(object sender, EventArgs e)
        {
            if (focused_wb.WType != 7)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
        }

        private void WbGridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                this.DocsPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           /*  if (exists (SELECT * FROM  SP_GET_RELDOCIDS(@docid) where DOCTYPE = 5) and @wtype = 7) SET @WRITEON = 1 ;
        else SET @WRITEON = 0 ;
      if (exists (SELECT * FROM  SP_GET_RELDOCIDS(@docid) where DOCTYPE = -5) and @wtype = 7) SET @WRITEOFF = 1 ;
        else SET @WRITEOFF = 0 ;*/
            var wbl = DB.SkladBase().WaybillList.FirstOrDefault(w => w.WbillId == focused_wb.WBillId);
            if (wbl == null)
            {
                return;
            }

            if (wbl.Checked == 1 && !DB.SkladBase().GetRelDocList(wbl.DocId).Any(w => w.DocId == 5) && wbl.WType == 7)
            {
                using (var f = new frmWBWriteOn())
                {
                    var result = f._db.ExecuteWayBill(wbl.WbillId, 5).ToList().FirstOrDefault();
                    f.doc_id = result.NewDocId;
                    f.TurnDocCheckBox.Checked = true;
                    f.ShowDialog();
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var wbl = DB.SkladBase().WaybillList.FirstOrDefault(w => w.WbillId == focused_wb.WBillId);
            if (wbl == null)
            {
                return;
            }

            if (wbl.Checked == 1 && !DB.SkladBase().GetRelDocList(wbl.DocId).Any(w => w.DocId == -5) && wbl.WType == 7)
            {
                using (var f = new frmWBWriteOff())
                {
                    var result = f._db.ExecuteWayBill(wbl.WbillId, -5).ToList().FirstOrDefault();
                    f.doc_id = result.NewDocId;
                    f.TurnDocCheckBox.Checked = true;
                    f.ShowDialog();
                }
            }

            RefrechItemBtn.PerformClick();
        }

        private void PrintItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 2:
                    var dr = WbGridView.GetFocusedRow() as GetWayBillListWh_Result;
                    if (dr == null)
                    {
                        return;
                    }

                    PrintDoc.Show(dr.DocId.Value, dr.WType.Value, DB.SkladBase());
                    break;
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IHelper.ShowOrdered(0, 0, focused_wh_mat.MatId.Value);
        }

        private void CopyItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 2:
                    var dr = WbGridView.GetFocusedRow() as GetWayBillListWh_Result;
                    var doc = DB.SkladBase().DocCopy(dr.DocId).FirstOrDefault();

                    if (cur_wtype == 5) //Ввведення залишків
                    {
                        using (var wb_in = new frmWBWriteOn(doc.out_wbill_id))
                        {
                            wb_in.ShowDialog();
                        }

                    }
                    if (cur_wtype == -5)  //Акти списання товару
                    {
                        using (var wb_in = new frmWBWriteOff(doc.out_wbill_id))
                        {
                            wb_in.ShowDialog();
                        }
                    }

                    if (cur_wtype == 4) // Накладна переміщення
                    {
                        using (var wb_re_in = new frmWayBillMove(doc.out_wbill_id))
                        {
                            wb_re_in.ShowDialog();
                        }
                    }

                    break;
            }

            GetWayBillList(cur_wtype);

        }

        private void wbStartDate_EditValueChanged(object sender, EventArgs e)
        {
         //   if (OnDateEdit.ContainsFocus || whKagentList.ContainsFocus)
       //     {
                RefrechItemBtn.PerformClick();
       //     }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = gridView3.GetFocusedRow() as GetRelDocList_Result;
            FindDoc.Find(row.DocId, row.DocType, row.OnDate);
        }

        private void gridView3_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.HitInfo.InRow)
            {
                Point p2 = Control.MousePosition;
                BottomPopupMenu.ShowPopup(p2);
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var row = gridView3.GetFocusedRow() as GetRelDocList_Result;
            PrintDoc.Show(row.DocId.Value, row.DocType.Value, DB.SkladBase());
        }

        private void WbGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            focused_row = e.Row as GetWayBillListWh_Result;
            WayBillListInfoBS.DataSource = focused_row;

            if (focused_row != null)
            {
                using (var db = DB.SkladBase())
                {
                    gridControl2.DataSource = db.GetWaybillDetIn(focused_row.WBillId).ToList();
                    gridControl3.DataSource = db.GetRelDocList(focused_row.DocId).OrderBy(o => o.OnDate).ToList();
                }
            }
            else
            {
                gridControl2.DataSource = null;
                gridControl3.DataSource = null;
            }

            DeleteItemBtn.Enabled = (focused_row != null && focused_row.Checked == 0 && focused_tree_node.CanDelete == 1);
            ExecuteItemBtn.Enabled = (focused_row != null && focused_row.WType != 2 && focused_row.WType != -16 && focused_row.WType != 16 && focused_tree_node.CanPost == 1);
            EditItemBtn.Enabled = (focused_row != null && focused_tree_node.CanModify == 1);
            CopyItemBtn.Enabled = EditItemBtn.Enabled;
            PrintItemBtn.Enabled = (focused_row != null);
        }

        private void WhMatGridView_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            GetWhBottomInfo(focused_wh_mat);
        }

        private void WhMatGridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            if ( e.RowHandle < 0 )
            {
                return;
            }

            var wh_row = WhMatGridView.GetRow(e.RowHandle) as WhMatGet_Result;

            if (wh_row != null && wh_row.Remain < wh_row.MinReserv)
            {
                e.Appearance.ForeColor = Color.Red;
            }
        }

    }
}
