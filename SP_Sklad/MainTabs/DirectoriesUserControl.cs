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
using SP_Sklad.EditForm;
using SP_Sklad.Common;
using DevExpress.XtraTreeList;

namespace SP_Sklad.MainTabs
{
    public partial class DirectoriesUserControl : UserControl
    {
        GetDirTree_Result focused_tree_node { get; set; }
        public bool isDirectList { get; set; }
        public bool isMatList { get; set; }
        public List<CustomMatList> custom_mat_list { get; set; }
        public WaybillList wb { get; set; }
     //   public Object resut { get; set; }
        private int _archived { get; set; }

        public DirectoriesUserControl()
        {
            InitializeComponent();
            _archived = 0;
        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void DirectoriesUserControl_Load(object sender, EventArgs e)
        {
            mainContentTab.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;
            extDirTabControl.ShowTabHeader = DevExpress.Utils.DefaultBoolean.False;

            if (!DesignMode)
            {
                custom_mat_list = new List<CustomMatList>();
                MatListGridControl.DataSource = custom_mat_list;

                using (var db = new BaseEntities())
                {
                    repositoryItemLookUpEdit1.DataSource = DBHelper.WhList();

                    DirTreeBS.DataSource = db.GetDirTree(DBHelper.CurrentUser.UserId).ToList();
                    DirTreeList.ExpandToLevel(1);
                }
            }
        }

        private void DirTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DirTreeList.GetDataRecordByNode(e.Node) as GetDirTree_Result;

            RefrechItemBtn.PerformClick();
            mainContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;
        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var db = DB.SkladBase();

            switch (focused_tree_node.GType)
            {
                case 1:
                    //  KAgent->ParamByName("WDATE")->Value = frmMain->WorkDateEdit->Date;
                    var ka = DB.SkladBase().KagentList.Where(w => w.Archived == _archived || w.Archived == null);
                    if (focused_tree_node.Id != 10) ka = ka.Where(w => w.KType == focused_tree_node.GrpId);
                    KAgentDS.DataSource = ka.ToList();
                    break;

                case 2:
                    MatListDS.DataSource = DB.SkladBase().GetMatList(focused_tree_node.Id == 6 ? -1 : focused_tree_node.GrpId, 0, 0, 0);
                    break;

                case 3:
                    if (focused_tree_node.Id == 51) ServicesBS.DataSource = DB.SkladBase().v_Services.ToList();
                    else ServicesBS.DataSource = DB.SkladBase().v_Services.Where(w => w.GrpId == focused_tree_node.GrpId).ToList();
                    break;

                case 4: switch (focused_tree_node.Id)
                    {
                        //      case 25: cxGridLevel6->GridView = WarehouseGrid; break;
                        //      case 11: cxGridLevel6->GridView = BanksGrid; break;
                        //      case 2: cxGridLevel6->GridView = MeasuresGrid; break;
                        //      case 43: cxGridLevel6->GridView = CountriesGrid; break;
                        //      case 12: cxGridLevel6->GridView = AccountTypeGrid; break;
                        //      case 40: cxGridLevel6->GridView = PricetypesGrid; break;
                        //      case 102: cxGridLevel6->GridView = ChargetypeGrid; break;
                        //      case 64: cxGridLevel6->GridView = CashdesksGrid; break;
                        //      case 3: cxGridLevel6->GridView = CurrencyGrid; break;
                        case 53:
                            MatRecipeDS.DataSource = db.MatRecipe.Where(w => w.RType == 1).Select(s => new
                            {
                                s.RecId,
                                MatName = s.Materials.Name,
                                s.OnDate,
                                s.Amount,
                                s.Materials.Measures.ShortName,
                                s.Name,
                                GrpName = s.Materials.MatGroup.Name
                            }).ToList();

                            extDirTabControl.SelectedTabPageIndex = 0; break;
                        case 42:
                            MatRecipeDS.DataSource = db.MatRecipe.Where(w => w.RType == 2).Select(s => new
                            {
                                s.RecId,
                                MatName = s.Materials.Name,
                                s.OnDate,
                                s.Amount,
                                s.Materials.Measures.ShortName,
                                s.Name,
                                GrpName = s.Materials.MatGroup.Name
                            }).ToList();
                            extDirTabControl.SelectedTabPageIndex = 0; break;
                        //       case 68: cxGridLevel6->GridView = TaxesGrid; break;
                        //       case 112: cxGridLevel6->GridView = TechProcessGrid; break;
                    }
                    break;
            }

            db.Dispose();
        }

        private void MatGridView_DoubleClick(object sender, EventArgs e)
        {
            var row = MatGridView.GetFocusedRow() as GetMatList_Result;

            if (isMatList)
            {
                var p_type = (wb.Kagent != null ? wb.Kagent.PTypeId : null);
                var mat_price = DB.SkladBase().GetListMatPrices(row.MatId, wb.CurrId, p_type).FirstOrDefault();

                custom_mat_list.Add(new CustomMatList
                {
                    Num = custom_mat_list.Count() + 1,
                    MatId = row.MatId,
                    Name = row.Name,
                    Amount = 1,
                    Price = mat_price != null ? mat_price.Price : 0,
                    WId = row.WId != null ? row.WId.Value : DBHelper.WhList().FirstOrDefault(w => w.Def == 1).WId
                });

                MatListGridView.RefreshData();
            }
            else if (isDirectList)
            {
                var frm = this.Parent as frmCatalog;
                frm.OkButton.PerformClick();
            }
            else
            {
                EditItemBtn.PerformClick();
            }
        }

        private void MatGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //resut = MatGridView.GetFocusedRow();
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult result = DialogResult.Cancel;
            switch (focused_tree_node.GType)
            {
                case 1:
                    var ka = KaGridView.GetFocusedRow() as KagentList;
                    result = new frmKAgentEdit(null, ka.KaId).ShowDialog();
                    break;

                case 2:
                    var r = MatGridView.GetFocusedRow() as GetMatList_Result;
                    result = new frmMaterialEdit(r.MatId).ShowDialog();
                    break;

                case 3: var svc_row = ServicesGridView.GetFocusedRow() as v_Services;
                    result = new frmServicesEdit(svc_row.SvcId).ShowDialog();
                    break;

                case 4: switch (focused_tree_node.Id)
                    {
                        /*     case 25: frmWarehouseEdit = new TfrmWarehouseEdit(Application);
                                 frmWarehouseEdit->Warehouse->ParamByName("WID")->Value = SkladData->WarehouseWID->Value;
                                 frmWarehouseEdit->Warehouse->Open();
                                 frmWarehouseEdit->Warehouse->Edit();
                                 frmWarehouseEdit->ShowModal();
                                 delete frmWarehouseEdit;
                                 SkladData->Warehouse->FullRefresh();
                                 break;

                             case 11: frmBanksEdit = new TfrmBanksEdit(Application);
                                 frmBanksEdit->Banks->ParamByName("BANKID")->Value = SkladData->BanksBANKID->Value;
                                 frmBanksEdit->Banks->Open();
                                 frmBanksEdit->Banks->Edit();
                                 frmBanksEdit->ShowModal();
                                 delete frmBanksEdit;
                                 SkladData->Banks->FullRefresh();
                                 break;

                             case 2: frmMeasuresEdit = new TfrmMeasuresEdit(Application);
                                 frmMeasuresEdit->Measures->ParamByName("MID")->Value = SkladData->MeasuresMID->Value;
                                 frmMeasuresEdit->Measures->Open();
                                 frmMeasuresEdit->Measures->Edit();
                                 frmMeasuresEdit->ShowModal();
                                 delete frmMeasuresEdit;
                                 SkladData->Measures->FullRefresh();
                                 break;

                             case 40: frmPricetypesEdit = new TfrmPricetypesEdit(Application);
                                 frmPricetypesEdit->Pricetypes->ParamByName("PTYPEID")->Value = SkladData->PricetypesPTYPEID->Value;
                                 frmPricetypesEdit->Pricetypes->Open();
                                 frmPricetypesEdit->Pricetypes->Edit();
                                 frmPricetypesEdit->ShowModal();
                                 delete frmPricetypesEdit;
                                 SkladData->Pricetypes->FullRefresh();
                                 break;

                             case 12: frmAccountTypeEdit = new TfrmAccountTypeEdit(Application);
                                 frmAccountTypeEdit->AccountType->ParamByName("TYPEID")->Value = SkladData->AccountTypeTYPEID->Value;
                                 frmAccountTypeEdit->AccountType->Open();
                                 frmAccountTypeEdit->AccountType->Edit();
                                 frmAccountTypeEdit->ShowModal();
                                 delete frmAccountTypeEdit;
                                 SkladData->AccountType->FullRefresh();
                                 break;

                             case 43: frmCountriesEdit = new TfrmCountriesEdit(Application);
                                 frmCountriesEdit->Countries->ParamByName("CID")->Value = SkladData->CountriesCID->Value;
                                 frmCountriesEdit->Countries->Open();
                                 frmCountriesEdit->Countries->Edit();
                                 frmCountriesEdit->ShowModal();
                                 delete frmCountriesEdit;
                                 SkladData->Countries->FullRefresh();
                                 break;

                             case 102: frmChargetypeEdit = new TfrmChargetypeEdit(Application);
                                 frmChargetypeEdit->Chargetype->ParamByName("CTYPEID")->Value = SkladData->ChargetypeCTYPEID->Value;
                                 frmChargetypeEdit->Chargetype->Open();
                                 frmChargetypeEdit->Chargetype->Edit();
                                 frmChargetypeEdit->ShowModal();
                                 delete frmChargetypeEdit;
                                 SkladData->Chargetype->FullRefresh();
                                 break;

                             case 64: frmCashdesksEdit = new TfrmCashdesksEdit(Application);
                                 frmCashdesksEdit->Cashdesks->ParamByName("CASHID")->Value = SkladData->CashdesksCASHID->Value;
                                 frmCashdesksEdit->Cashdesks->Open();
                                 frmCashdesksEdit->Cashdesks->Edit();
                                 frmCashdesksEdit->ShowModal();
                                 delete frmCashdesksEdit;
                                 SkladData->Cashdesks->FullRefresh();
                                 break;*/

                        case 42:
                        case 53:
                            dynamic r_item = MatRecipeGridView.GetFocusedRow();
                            result = new frmMatRecipe(null, r_item.RecId).ShowDialog();

                            break;

                        /*   case 112: frmTechProcessEdit = new TfrmTechProcessEdit(Application);
                               frmTechProcessEdit->TechProcess->ParamByName("PROCID")->Value = SkladData->TechProcessPROCID->Value;
                               frmTechProcessEdit->TechProcess->Open();
                               frmTechProcessEdit->TechProcess->Edit();
                               frmTechProcessEdit->ShowModal();
                               delete frmTechProcessEdit;
                               SkladData->TechProcess->FullRefresh();
                               break;*/
                    }
                    break;

            }

            if (result == DialogResult.OK)
            {
                RefrechItemBtn.PerformClick();
            }
        }

        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            switch (focused_tree_node.GType)
            {
                case 1:
                    new frmKAgentEdit(focused_tree_node.GrpId).ShowDialog();
                    break;

                case 2:
                    if (DB.SkladBase().MatGroup.Any())
                    {
                        var mat_edit = new frmMaterialEdit(null, focused_tree_node.Id < 0 ? focused_tree_node.Id * -1 : DB.SkladBase().MatGroup.First().GrpId);
                        mat_edit.ShowDialog();
                    }
                    break;

                case 3: if (DB.SkladBase().SvcGroup.Any())
                    {
                        var svc_edit = new frmServicesEdit(null, focused_tree_node.Id < 0 ? focused_tree_node.Id * -1 : DB.SkladBase().SvcGroup.First().GrpId);
                        svc_edit.ShowDialog();
                    }
                    break;

                case 4: switch (focused_tree_node.Id)
                    {
                        /*   case 25: frmWarehouseEdit = new TfrmWarehouseEdit(Application);
                               frmWarehouseEdit->Warehouse->Open();
                               frmWarehouseEdit->Warehouse->Append();
                               frmWarehouseEdit->ShowModal();
                               delete frmWarehouseEdit;
                               SkladData->Warehouse->FullRefresh();
                               break;

                           case 11: frmBanksEdit = new TfrmBanksEdit(Application);
                               frmBanksEdit->Banks->Open();
                               frmBanksEdit->Banks->Append();
                               frmBanksEdit->ShowModal();
                               delete frmBanksEdit;
                               SkladData->Banks->FullRefresh();
                               break;

                           case 2: frmMeasuresEdit = new TfrmMeasuresEdit(Application);
                               frmMeasuresEdit->Measures->Open();
                               frmMeasuresEdit->Measures->Append();
                               frmMeasuresEdit->ShowModal();
                               delete frmMeasuresEdit;
                               SkladData->Measures->FullRefresh();
                               break;

                           case 40: frmPricetypesEdit = new TfrmPricetypesEdit(Application);
                               frmPricetypesEdit->Pricetypes->Open();
                               frmPricetypesEdit->Pricetypes->Append();
                               frmPricetypesEdit->ShowModal();
                               delete frmPricetypesEdit;
                               SkladData->Pricetypes->FullRefresh();
                               break;

                           case 12: frmAccountTypeEdit = new TfrmAccountTypeEdit(Application);
                               frmAccountTypeEdit->AccountType->Open();
                               frmAccountTypeEdit->AccountType->Append();
                               frmAccountTypeEdit->ShowModal();
                               delete frmAccountTypeEdit;
                               SkladData->AccountType->FullRefresh();
                               break;

                           case 43: frmCountriesEdit = new TfrmCountriesEdit(Application);
                               frmCountriesEdit->Countries->Open();
                               frmCountriesEdit->Countries->Append();
                               frmCountriesEdit->ShowModal();
                               delete frmCountriesEdit;
                               SkladData->Countries->FullRefresh();
                               break;

                           case 102: frmChargetypeEdit = new TfrmChargetypeEdit(Application);
                               frmChargetypeEdit->Chargetype->Open();
                               frmChargetypeEdit->Chargetype->Append();
                               frmChargetypeEdit->ShowModal();
                               delete frmChargetypeEdit;
                               SkladData->Chargetype->FullRefresh();
                               break;

                           case 64: frmCashdesksEdit = new TfrmCashdesksEdit(Application);
                               frmCashdesksEdit->Cashdesks->Open();
                               frmCashdesksEdit->Cashdesks->Append();
                               frmCashdesksEdit->ShowModal();
                               delete frmCashdesksEdit;
                               SkladData->Cashdesks->FullRefresh();
                               break;*/

                        case 42:
                            new frmMatRecipe(2).ShowDialog();
                            break;

                        case 53:
                            new frmMatRecipe(1).ShowDialog();
                            break;

                        /*    case 112: frmTechProcessEdit = new TfrmTechProcessEdit(Application);
                                frmTechProcessEdit->TechProcess->Open();
                                frmTechProcessEdit->TechProcess->Append();
                                frmTechProcessEdit->ShowModal();
                                delete frmTechProcessEdit;
                                SkladData->TechProcess->FullRefresh();
                                break;*/

                    }
                    break;

            }

            RefrechItemBtn.PerformClick();
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("Ви дійсно бажаєте відалити цей запис з довідника?", "Підтвердіть видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Information) != DialogResult.Yes)
            {
                return;
            }

            using (var db = DB.SkladBase())
            {
                switch (focused_tree_node.GType)
                {
                    case 1:
                        var ka = KaGridView.GetFocusedRow() as KagentList;

                        var item = db.Kagent.Find(ka.KaId);
                        if (item != null)
                        {
                            item.Deleted = 1;
                        }

                        break;

                    case 2:
                        var r = MatGridView.GetFocusedRow() as GetMatList_Result;

                        var mat = db.Materials.Find(r.MatId);
                        if (mat != null)
                        {
                            mat.Deleted = 1;
                        }

                        break;

                    case 3:
                        var svc_row = ServicesGridView.GetFocusedRow() as v_Services;

                        var svc = db.Services.Find(svc_row.SvcId);
                        if (svc != null)
                        {
                            svc.Deleted = 1;
                        }
                        break;

                    case 4: switch (focused_tree_node.Id)
                        {
                            /*     case 25: SkladData->Warehouse->Delete(); break;
                                 case 11: SkladData->Banks->Delete(); break;
                                 case 2: SkladData->Measures->Delete(); break;
                                 case 40: SkladData->Pricetypes->Delete(); break;
                                 case 12: SkladData->AccountType->Delete(); break;
                                 case 43: SkladData->Countries->Delete(); break;
                                 case 102: SkladData->Chargetype->Delete(); break;
                                 case 64: SkladData->Cashdesks->Delete(); break;*/
                            case 42:
                            case 53:
                                int mat_rec = ((dynamic)MatRecipeGridView.GetFocusedRow()).RecId;
                                db.DeleteWhere<MatRecipe>(w => w.RecId == mat_rec);
                                break;
                            //     case 112: SkladData->TechProcess->Delete(); break;
                        }
                        break;

                }

                db.SaveChanges();
            }
            RefrechItemBtn.PerformClick();
        }

        private void KaGridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            ;
        }

        private void KaGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void KaGridView_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        {
            KAgentPopupMenu.ShowPopup(Control.MousePosition); 
        }

        private void DirTreeList_PopupMenuShowing(object sender, DevExpress.XtraTreeList.PopupMenuShowingEventArgs e)
        {
            Point p2 = Control.MousePosition;
            ExplorerPopupMenu.ShowPopup(p2);
        }

        private void DirTreeList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                TreeList treeList = sender as TreeList;
                TreeListHitInfo info = treeList.CalcHitInfo(e.Location);
                if (info.Node != null)
                {
                    treeList.FocusedNode = info.Node;
                }
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 2: new frmMatGroupEdit(focused_tree_node.GrpId).ShowDialog();
                    break;

              /*  case 3: frmServGroupEdit = new TfrmServGroupEdit(Application);
                    frmServGroupEdit->SvcGroup->ParamByName("GRPID")->Value = DirectTreeGRPID->Value;
                    frmServGroupEdit->SvcGroup->Open();
                    frmServGroupEdit->SvcGroup->Edit();
                    frmServGroupEdit->ShowModal();
                    delete frmServGroupEdit;
                    break;*/
            }

            ExplorerRefreshBtn.PerformClick();
        }

        private void ExplorerRefreshBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DirTreeBS.DataSource = DB.SkladBase().GetDirTree(DBHelper.CurrentUser.UserId).ToList();
        }

        private void AddGroupMatBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node.ImageIndex == 2 && focused_tree_node.GType == 2)
            {
                new frmMatGroupEdit(null, focused_tree_node.GrpId).ShowDialog();
            }
            else new frmMatGroupEdit().ShowDialog();

            ExplorerRefreshBtn.PerformClick();
        }

        private void DelExplorerBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            switch (focused_tree_node.GType)
            {
                case 2: DB.SkladBase().DeleteWhere<MatGroup>(w => w.GrpId == focused_tree_node.GrpId).SaveChanges();
                    break;
                case 3: DB.SkladBase().DeleteWhere<SvcGroup>(w => w.GrpId == focused_tree_node.GrpId).SaveChanges();
                    break;
            }

            ExplorerRefreshBtn.PerformClick();
        }

        private void MatRecipeGridView_DoubleClick(object sender, EventArgs e)
        {
            EditItemBtn.PerformClick();
        }

        private void KagentBalansBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var ka = KaGridView.GetFocusedRow() as KagentList;

            IHelper.ShowKABalans(ka.KaId);
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (focused_tree_node.ImageIndex == 2 && focused_tree_node.GType == 3)
            {
                new frmSvcGroupEdit(null, focused_tree_node.GrpId).ShowDialog();
            }
            else new frmSvcGroupEdit().ShowDialog();

            ExplorerRefreshBtn.PerformClick();
        }
    }
}
