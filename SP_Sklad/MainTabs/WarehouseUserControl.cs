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

namespace SP_Sklad.MainTabs
{
    public partial class WarehouseUserControl : UserControl
    {
        int wid = 0;
        string wh_list = "*";
        int cur_wtype = 0;


        GetWhTree_Result focused_tree_node { get; set; }
        GetWayBillListWh_Result focused_row { get; set; }

        public WarehouseUserControl()
        {
            InitializeComponent();
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void WarehouseUserControl_Load(object sender, EventArgs e)
        {
            whContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            OnDateEdit.DateTime = DateTime.Now;

            wbKagentList.Properties.DataSource = new List<object>() { new { KaId = 0, Name = "Усі" } }.Concat(new BaseEntities().Kagent.Select(s => new { s.KaId, s.Name }));
            wbKagentList.EditValue = 0;

            WhComboBox.Properties.DataSource = new List<object>() { new { WId = "*", Name = "Усі" } }.Concat(new BaseEntities().Warehouse.Select(s => new { WId = s.WId.ToString(), s.Name }).ToList());
            WhComboBox.EditValue = "*";

            wbSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
            wbSatusList.EditValue = -1;

            wbStartDate.EditValue = DateTime.Now.AddDays(-30);
            wbEndDate.EditValue = DateTime.Now;

            GetTree(1);
        }

        void GetTree(int type)
        {
            WHTreeList.DataSource = new BaseEntities().GetWhTree(DBHelper.CurrentUser.UserId, type).ToList();
            WHTreeList.ExpandToLevel(0); //ExpandAll();
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
        }

        void GetWayBillList(int? wtyp)
        {
            if (wtyp == null && wbSatusList.EditValue == null || WhComboBox.EditValue == null || WHTreeList.FocusedNode == null)
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;

            var dr = WbGridView.GetRow(WbGridView.FocusedRowHandle) as GetWayBillListWh_Result;

            WBGridControl.DataSource = null;
            WBGridControl.DataSource = DB.SkladBase().GetWayBillListWh(satrt_date.Date, end_date.Date.AddDays(1), wtyp, (int)wbSatusList.EditValue, "*").OrderByDescending(o => o.OnDate);

            WbGridView.FocusedRowHandle = FindRowHandleByRowObject(WbGridView, dr);
        }

        private int FindRowHandleByRowObject(GridView view, GetWayBillListWh_Result dr)
        {
            if (dr != null)
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    if (dr.WBillId == (view.GetRow(i) as GetWayBillListWh_Result).WBillId)
                    {
                        return i;
                    }
                }
            }
            return GridControl.InvalidRowHandle;
        }


        private void ByGrpBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTree(1);
        }

        private void ByWhBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetTree(2);
        }

        private void WhMatGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var dr = WhMatGridView.GetRow(e.FocusedRowHandle) as WhMatGet_Result;

            if (dr != null)
            {
                RemainOnWhGrid.DataSource = DB.SkladBase().WMatGetByWh(dr.MatId, wid, (int)wbKagentList.EditValue, OnDateEdit.DateTime, wh_list);
                PosGridControl.DataSource = DB.SkladBase().PosGet(dr.MatId, wid, (int)wbKagentList.EditValue, OnDateEdit.DateTime, 0, wh_list);
            }
            else
            {
                RemainOnWhGrid.DataSource = null;
                PosGridControl.DataSource = null;
            }
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
                    break;
            }

            RefrechItemBtn.PerformClick();
        }

        private void WbGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            focused_row = WbGridView.GetRow(e.FocusedRowHandle) as GetWayBillListWh_Result;

            if (focused_row != null)
            {
                using (var db = DB.SkladBase())
                {
                    gridControl2.DataSource = db.GetWaybillDetIn(focused_row.WBillId).ToList();
                    gridControl3.DataSource = db.GetRelDocList(focused_row.DocId).ToList();
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

                    WhMatGridControl.DataSource = null;
                    WhMatGridControl.DataSource = DB.SkladBase().WhMatGet(grp_id, wid, (int)wbKagentList.EditValue, OnDateEdit.DateTime, 0, wh_list, 0, grp, DBHelper.CurrentUser.UserId, ViewDetailTree.Down ? 1:0).ToList();

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
    }
}
