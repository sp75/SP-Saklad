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
using SP_Sklad;
using SP_Sklad.WBForm;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SP_Sklad.Properties;
using System.Data.SqlClient;
using DevExpress.XtraGrid;

namespace SP_Sklad.MainTabs
{
    public partial class DocsUserControl : UserControl
    {
        const int USER_ID = 0;
        int cur_wtype = 0;
        int show_null_balance = 1;
        BaseEntities _db { get; set; }
        v_GetDocsTree focused_tree_node { get; set; }

        public DocsUserControl()
        {
            InitializeComponent();
        }

        private void DocumentsPanel_Load(object sender, EventArgs e)
        {

        }

        public void OnLoad()
        {
            _db = new BaseEntities();

            wbKagentList.Properties.DataSource = new List<object>() { new { KAID = 0, NAME = "Усі" } }.Concat(_db.KAGENT.Select(s => new { s.KAID, s.NAME }));
            wbKagentList.EditValue = 0;

            wbSatusList.Properties.DataSource = new List<object>() { new { Id = -1, Name = "Усі" }, new { Id = 1, Name = "Проведені" }, new { Id = 0, Name = "Непроведені" } };
            wbSatusList.EditValue = -1;

            wbStartDate.EditValue = DateTime.Now.AddDays(-30);
            wbEndDate.EditValue = DateTime.Now;

            DocsTreeList.DataSource = _db.v_GetDocsTree.Where(w => w.UserId == null || w.UserId == USER_ID).OrderBy(o => o.Num).ToList();
            DocsTreeList.ExpandAll();
        }

        void GetWayBillList(int wtyp)
        {
            DeleteItemBtn.Enabled = false;
            ExecuteItemBtn.Enabled = false;
            EditItemBtn.Enabled = false;
            CopyItemBtn.Enabled = false;
            PrintItemBtn.Enabled = false;

            if (wbSatusList.EditValue == null || wbKagentList.EditValue == null || DocsTreeList.FocusedNode==null)
            {
                return;
            }

            var satrt_date = wbStartDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(-100) : wbStartDate.DateTime;
            var end_date = wbEndDate.DateTime < DateTime.Now.AddYears(-100) ? DateTime.Now.AddYears(100) : wbEndDate.DateTime;
         
            var dr = WbGridView.GetRow(WbGridView.FocusedRowHandle) as GetWayBillList_Result;

            gridControl1.DataSource = _db.GetWayBillList(satrt_date.Date, end_date.Date.AddDays(1), wtyp, (int)wbSatusList.EditValue, (int)wbKagentList.EditValue, show_null_balance, "*", 0).OrderByDescending(o => o.OnDate);

            WbGridView.FocusedRowHandle = FindRowHandleByRowObject(WbGridView, dr);
        }

        private int FindRowHandleByRowObject(GridView view, GetWayBillList_Result dr)
        {
            if (dr != null)
            {
                for (int i = 0; i < view.DataRowCount; i++)
                {
                    if (dr.WbillId == (view.GetRow(i) as GetWayBillList_Result).WbillId)
                    {
                        return i;
                    }
                }
            }
            return GridControl.InvalidRowHandle;
        }


        private void DocsTreeList_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            focused_tree_node = DocsTreeList.GetDataRecordByNode(e.Node) as v_GetDocsTree;

            cur_wtype = focused_tree_node.WType != null ? focused_tree_node.WType.Value : 0;
           
            switch (focused_tree_node.GType)
            {
                case 1:
                    //GET_RelDocList->DataSource = WayBillListDS;
                    GetWayBillList(cur_wtype);
                    //                    WayBillListAfterScroll(WayBillList);
                    break;

                /*        case 4: GET_RelDocList->DataSource = PayDocDS;
                            PayDocTopPanelDate->Edit();
                            if (DocsTreeDataID->Value == 103) PayDocTopPanelDateDOCTYPE->Value = -2;
                            if (DocsTreeDataID->Value == 30) PayDocTopPanelDateDOCTYPE->Value = -1;
                            if (DocsTreeDataID->Value == 29) PayDocTopPanelDateDOCTYPE->Value = 1;
                            PayDocTopPanelDate->Post();
                            PayDoc->FullRefresh();
                            PayDocAfterScroll(PayDoc);
                            break;

                        case 5: PriceList->CloseOpen(true);
                            PriceListAfterScroll(PriceList);
                            break;

                        case 6: GET_RelDocList->DataSource = ContractsListDS;
                            if (DocsTreeDataID->Value == 47) ContractsList->ParamByName("IN_DOCTYPE")->AsVariant = -1;
                            if (DocsTreeDataID->Value == 46) ContractsList->ParamByName("IN_DOCTYPE")->AsVariant = 1;
                            ContractsList->CloseOpen(true);
                            ContractsListAfterScroll(ContractsList);
                            break;

                        case 7: GET_RelDocList->DataSource = TaxWBListDS;
                            TaxWBList->CloseOpen(true);
                            break;*/
            }

            wbContentTab.SelectedTabPageIndex = focused_tree_node.GType.Value;

        }

        private void wbStartDate_Properties_EditValueChanged(object sender, EventArgs e)
        {
            GetWayBillList(cur_wtype);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            var dr = WbGridView.GetRow(e.FocusedRowHandle) as GetWayBillList_Result;

            if (dr != null)
            {
                gridControl2.DataSource = _db.GetWaybillDetIn(dr.WbillId);
                gridControl3.DataSource = _db.GetRelDocList(dr.DocId);
            }

            var tree_row = DocsTreeList.GetDataRecordByNode(DocsTreeList.FocusedNode) as v_GetDocsTree;

            DeleteItemBtn.Enabled = (dr!= null && dr.Checked == 0 && tree_row.CanDelete == 1);
            ExecuteItemBtn.Enabled = (dr != null && dr.WType != 2 && dr.WType != -16 && dr.WType != 16 && tree_row.CanPost == 1);
            EditItemBtn.Enabled = (dr != null && tree_row.CanModify == 1);
            CopyItemBtn.Enabled = EditItemBtn.Enabled;
            PrintItemBtn.Enabled = (dr != null);
        }


        private void NewItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //int GTYPE = (int)DocsTreeList.FocusedNode.GetValue("GTYPE");
     //       int ID = (int)DocsTreeList.FocusedNode.GetValue("ID");

            switch (focused_tree_node.GType)
            {
                case 1:	/*if(DocsTreeDataID->Value == 27 ||DocsTreeDataID->Value == 39 || DocsTreeDataID->Value == 107)
				 {
					frmWayBillOut = new  TfrmWayBillOut(Application);
					frmWayBillOut->WayBillList->Open();
					frmWayBillOut->WayBillList->Append();
					if(DocsTreeDataID->Value == 27) frmWayBillOut->WayBillListWTYPE->Value = -1 ;
					if(DocsTreeDataID->Value == 39) frmWayBillOut->WayBillListWTYPE->Value = 2 ;
					if(DocsTreeDataID->Value == 107) frmWayBillOut->WayBillListWTYPE->Value  = -16;
					frmWayBillOut->WayBillListENTID->Value = SkladData->EnterpriseKAID->Value ;
					frmWayBillOut->WayBillList->Post();
					frmWayBillOut->WayBillList->Edit();
					frmWayBillOut->ShowModal() ;
					delete frmWayBillOut;
				 }*/

                    if (cur_wtype == 1 || focused_tree_node.Id == 16)  //Прибткова накладна , замовлення постачальникам
                    {
                        using (var wb_in = new frmWayBillIn(cur_wtype, null))
                        {
                            wb_in.ShowDialog();
                        }
                    }

                    /*		if(DocsTreeDataID->Value == 57) // Повернення від клієнта
                            {
                                frmWBReturnIn = new  TfrmWBReturnIn(Application);
                                frmWBReturnIn->WayBillList->Open();
                                frmWBReturnIn->WayBillList->Append();
                                frmWBReturnIn->WayBillListWTYPE->Value  = 6;
                                frmWBReturnIn->WayBillList->Post();
                                frmWBReturnIn->WayBillList->Edit();
                                frmWBReturnIn->ShowModal() ;
                            }
                            if(DocsTreeDataID->Value == 56 ) //Повернення постачальнику
                            {
                                frmWBReturnOut = new  TfrmWBReturnOut(Application);
                                frmWBReturnOut->WayBillList->Open();
                                frmWBReturnOut->WayBillList->Append();
                                frmWBReturnOut->WayBillListWTYPE->Value  = -6;
                                frmWBReturnOut->WayBillList->Post();
                                frmWBReturnOut->WayBillList->Edit();
                                frmWBReturnOut->ShowModal() ;
                            }*/
                    break;

                /*	case 4: frmPayDoc = new  TfrmPayDoc(Application);
                            frmPayDoc->PayDoc->Open();
                            frmPayDoc->PayDoc->Append();
                            if(DocsTreeDataID->Value == 103) frmPayDoc->PayDocDOCTYPE->Value = -2;
                            if(DocsTreeDataID->Value == 30) frmPayDoc->PayDocDOCTYPE->Value = -1;
                            if(DocsTreeDataID->Value == 29) frmPayDoc->PayDocDOCTYPE->Value = 1;
                            frmPayDoc->ShowModal() ;
                            delete frmPayDoc;
                            break;

                    case 5: frmPriceList = new  TfrmPriceList(Application);
                            frmPriceList->PriceList->Open();
                            frmPriceList->PriceList->Append();
                            frmPriceList->ShowModal() ;
                            delete frmPriceList;
                            break;

                    case 6: frmContr = new  TfrmContr(Application);
                            frmContr->CONTRACTS->Open();
                            frmContr->CONTRACTS->Append();
                            if(DocsTreeDataID->Value == 47) frmContr->CONTRACTSDOCTYPE->Value = -1;
                            if(DocsTreeDataID->Value == 46) frmContr->CONTRACTSDOCTYPE->Value = 1;
                            frmContr->CONTRACTS->Post();
                            frmContr->CONTRACTS->Edit();

                            frmContr->CONTRPARAMS->Append();
                            frmContr->CONTRPARAMS->Post();
                            frmContr->CONTRRESULTS->Append();

                            frmContr->ShowModal() ;
                            delete frmContr;
                            break;

                    case 7: frmTaxWB = new  TfrmTaxWB(Application);
                            frmTaxWB->TaxWB->Open();
                            frmTaxWB->TaxWB->Append();
                            frmTaxWB->TaxWB->Post();
                            frmTaxWB->TaxWB->Edit();
                            frmTaxWB->ShowModal() ;
                            delete frmTaxWB;
                            break;*/
            }

            GetWayBillList(cur_wtype);
        }

        private void EditItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int gtype = (int)DocsTreeList.FocusedNode.GetValue("GType");

            using (var db = new BaseEntities())
            {

                //   var current_transaction = db.Database.BeginTransaction(IsolationLevel.RepeatableRead);
                switch (gtype)
                {
                    case 1:
                        var dr = WbGridView.GetFocusedRow() as GetWayBillList_Result;
                        int? result = 0;

                        if (dr == null)
                        {
                            break;
                        }

                        var wb = new WaybillList();
                        try
                        {
                            wb = _db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK) where WbillId = {0}", dr.WbillId).FirstOrDefault();//  db.WaybillList.AsNoTracking().FirstOrDefault(w => w.WbillId == dr.WbillId);
                        }
                        catch (SqlException exception)
                        {
                            MessageBox.Show(Resources.deadlock);
                            break;
                        }

                        if (wb == null)
                        {
                            MessageBox.Show(Resources.not_find_wb);
                            break;
                        }

                        if (wb.Checked == 1)
                        {
                            if (MessageBox.Show(Resources.edit_info, "Відміна проводки", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                            {
                                result = DBHelper.StornoOrder(db, dr.WbillId);
                            }
                            else
                            {
                                result = 1;
                            }
                        }

                        if (result == 1)
                        {
                            break;
                        }

                        if (cur_wtype == 1 || cur_wtype == 16)
                        {
                            using (var wb_in = new frmWayBillIn(cur_wtype, wb.WbillId))
                            {
                                wb_in.ShowDialog();
                            }
                        }
                       


                        /*	if(DocsTreeDataID->Value == 27 || DocsTreeDataID->Value == 39 || DocsTreeDataID->Value == 107)
                             {
                                try
                                {
                                  try
                                  {
                                    frmWayBillOut = new  TfrmWayBillOut(Application);
                                    frmWayBillOut->WayBillList->ParamByName("WBILLID")->Value = WayBillListWBILLID->Value;
                                    frmWayBillOut->WayBillList->Open();
                                    frmWayBillOut->WayBillList->Edit();
                                    frmWayBillOut->WayBillList->LockRecord(true)  ;
                                    frmWayBillOut->ShowModal();
                                  }
                                  catch(const Exception& e)
                                  {
                                    frmWayBillOut->Close();
                                    if(e.Message.Pos("Deadlock") > 0) 	ShowMessage(Deadlock) ;
                                    else   ShowMessage(e.Message) ;
                                  }
                                }
                                __finally
                                {
                                   delete frmWayBillOut ;
                                }

                             }

                            if(DocsTreeDataID->Value == 57) // Повернення від кліента
                             {
                                try
                                {
                                  try
                                  {
                                    frmWBReturnIn  = new TfrmWBReturnIn(Application);
                                    frmWBReturnIn->WayBillList->ParamByName("WBILLID")->Value = WayBillListWBILLID->Value;
                                    frmWBReturnIn->WayBillList->Open();
                                    frmWBReturnIn->WayBillList->Edit();
                                    frmWBReturnIn->WayBillList->LockRecord()  ;
                                    frmWBReturnIn->ShowModal();
                                  }
                                  catch(const Exception& e)
                                  {
                                    frmWBReturnIn->Close();
                                    if(e.Message.Pos("Deadlock") > 0) 	ShowMessage(Deadlock) ;
                                    else   ShowMessage(e.Message) ;
                                  }
                                }
                                __finally
                                {
                                    delete frmWBReturnIn ;
                                }
                             }

                            if(DocsTreeDataID->Value == 56) //Повернення постачальнику
                             {
                                try
                                {
                                  try
                                  {
                                    frmWBReturnOut  = new  TfrmWBReturnOut(Application);
                                    frmWBReturnOut->WayBillList->ParamByName("WBILLID")->Value = WayBillListWBILLID->Value;
                                    frmWBReturnOut->WayBillList->Open();
                                    frmWBReturnOut->WayBillList->Edit();
                                    frmWBReturnOut->WayBillList->LockRecord()  ;
                                    frmWBReturnOut->ShowModal();
                                  }
                                  catch(const Exception& e)
                                  {
                                    frmWBReturnOut->Close();
                                    if(e.Message.Pos("Deadlock") > 0) 	ShowMessage(Deadlock) ;
                                      else ShowMessage(e.Message) ;
                                  }
                                }
                                __finally
                                {
                                      delete frmWBReturnOut ;
                                }
                             }*/

                        break;
                    /*
                            case 4: PayDoc->Refresh();
                                    if(PayDocCHECKED->Value == 1)
                                        if(MessageDlg(msg1,mtConfirmation,TMsgDlgButtons() << mbYes << mbNo ,0)==mrYes)
                                           ExecuteBtn->Click();
                                    if(PayDocCHECKED->Value == 0)
                                     {
                                         TfrmPayDoc *frmPD = new  TfrmPayDoc(Application);
                                         try
                                         {
                                            try
                                            {
                                              frmPD = new  TfrmPayDoc(Application);
                                              frmPD->PayDoc->ParamByName("PAYDOCID")->Value = PayDocPAYDOCID->Value;
                                              frmPD->PayDoc->Open();
                                              frmPD->PayDoc->Edit();
                                              frmPD->PayDoc->LockRecord()  ;
                                              frmPD->ShowModal() ;
                                            }
                                            catch(const Exception& e)
                                            {
                                                if(e.Message.Pos("Deadlock") > 0) 	ShowMessage(Deadlock) ;
                                                   else  ShowMessage(e.Message) ;
                                            }
                                         }
                                         __finally
                                         {
                                              delete frmPD ;
                                         }
                                     }
                                    break;

                            case 5: {
                                      try
                                      {
                                        try
                                        {
                                           frmPriceList = new  TfrmPriceList(Application);
                                           frmPriceList->PriceList->ParamByName("PLID")->Value = PriceListPLID->Value;
                                           frmPriceList->PriceList->Open();
                                           frmPriceList->PriceList->Edit();
                                           frmPriceList->PriceList->LockRecord()  ;
                                           frmPriceList->ShowModal() ;
                                        }
                                        catch(const Exception& e)
                                        {
                                           frmPriceList->Close();
                                           if(e.Message.Pos("Deadlock") > 0) 	ShowMessage(Deadlock) ;
                                               else ShowMessage(e.Message) ;
                                        }
                                      }
                                      __finally
                                      {
                                         delete frmPriceList ;
                                      }
                                    }
                                    break;

                            case 6: ContractsList->Refresh();
                                    if(ContractsListCHECKED->Value == 1)
                                        if(MessageDlg(msg1,mtConfirmation,TMsgDlgButtons() << mbYes << mbNo ,0)==mrYes)
                                           ExecuteBtn->Click();

                                    if(ContractsListCHECKED->Value == 0)
                                     {
                                        try
                                        {
                                          try
                                          {
                                            frmContr = new  TfrmContr(Application);
                                            frmContr->CONTRACTS->ParamByName("CONTRID")->Value = ContractsListCONTRID->Value;
                                            frmContr->CONTRACTS->Open();
                                            frmContr->CONTRACTS->Edit();
                                            frmContr->CONTRACTS->LockRecord()  ;
                                            frmContr->ShowModal() ;

                                          }
                                          catch(const Exception& e)
                                          {
                                             frmContr->Close();
                                             if(e.Message.Pos("Deadlock") > 0) 	ShowMessage(Deadlock) ;
                                                else   ShowMessage(e.Message) ;
                                          }
                                        }
                                        __finally
                                        {
                                            delete frmContr;
                                        }
                                     }
                                    break;

                            case 7: try
                                    {
                                       try
                                       {
                                         frmTaxWB = new  TfrmTaxWB(Application);
                                         frmTaxWB->TaxWB->ParamByName("TWBID")->Value = TaxWBListTWBID->Value;
                                         frmTaxWB->TaxWB->Open();
                                         frmTaxWB->TaxWB->Edit();
                                         frmTaxWB->TaxWB->LockRecord()  ;
                                         frmTaxWB->ShowModal() ;
                                       }
                                       catch(const Exception& e)
                                       {
                                          frmTaxWB->Close();
                                          if(e.Message.Pos("Deadlock") > 0) 	ShowMessage(Deadlock) ;
                                                else   ShowMessage(e.Message) ;
                                       }
                                    }
                                    __finally
                                    {
                                        delete frmTaxWB;
                                    }
                                    break;*/

                }
                //    current_transaction.Rollback();
            }

            GetWayBillList(cur_wtype);
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

        private void WbGridView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            
        }

        private void DeleteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int gtype = (int)DocsTreeList.FocusedNode.GetValue("GType");
            var dr = WbGridView.GetFocusedRow() as GetWayBillList_Result;
            try
            {
                switch (gtype)
                {
                    case 1: _db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK) where WbillId = {0}", dr.WbillId).FirstOrDefault(); break;
                    //	case 4: PayDoc->LockRecord();  break;
                    //	case 5: PriceList->LockRecord();  break;
                    //	case 6: ContractsList->LockRecord();  break;
                    //	case 7: TaxWBList->LockRecord();  break;
                }
                if (MessageBox.Show(Resources.delete_wb, "Відалення документа", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    switch (gtype)
                    {
                        case 1:
                            _db.WaybillList.Remove(_db.WaybillList.Find(dr.WbillId));
                            break;
                        //	   case 4: PayDoc->Delete();  break;
                        //	   case 5: PriceList->Delete();  break;
                        //	   case 6: ContractsList->Delete();  break;
                        //	   case 7: TaxWBList->Delete();  break;
                    }
                }
                _db.SaveChanges();
                GetWayBillList(cur_wtype);
            }
            catch
            {
                MessageBox.Show(Resources.deadlock);
            }

        }

        private void RefrechItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GetWayBillList(cur_wtype);
        }

        private void ExecuteItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var g_type = (int)DocsTreeList.FocusedNode.GetValue("GType");

            using (var db = new BaseEntities())
            {
                switch (g_type)
                {
                    case 1:
                        var dr = WbGridView.GetFocusedRow() as GetWayBillList_Result;
                        if (dr == null)
                        {
                            return;
                        }

                        var wb = db.WaybillList.Find(dr.WbillId);
                        if (wb != null)
                        {
                            if (wb.Checked == 1)
                            {
                                DBHelper.StornoOrder(db, dr.WbillId);
                            }
                            else
                            {
                                if (wb.WType == -1)
                                {
                                    //   if (!SkladData->CheckActiveSuppliers(WayBillListWBILLID->Value, DocPAnelTransaction)) return;
                                }
                                DBHelper.ExecuteOrder(db, dr.WbillId);
                            }
                        }
                        else
                        {
                            MessageBox.Show(Resources.not_find_wb);
                        }
                        break;
                }
            }

            GetWayBillList(cur_wtype);
        }

        private void PrintItemBtn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

    }
}
