﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SP_Sklad.FinanseForm;
using SP_Sklad.Properties;
using SP_Sklad.SkladData;
using SP_Sklad.WBForm;

namespace SP_Sklad.Common
{
   public class DocEdit
    {
        private const int DeadlockErrorNumber = 1205;
        private const int LockingErrorNumber = 1222;
        private const int UpdateConflictErrorNumber = 3960;

       public static void WBEdit(GetWayBillList_Result dr)
       {
           int? result = 0;

           if (dr == null)
           {
               return;
           }

           using (var db = new BaseEntities())
           {
               var trans = db.Database.BeginTransaction();
               try
               {
                   var wb = db.Database.SqlQuery<WaybillList>("SELECT * from WaybillList WITH (UPDLOCK, NOWAIT) where WbillId = {0}", dr.WbillId).FirstOrDefault();

                   if (wb == null)
                   {
                       MessageBox.Show(Resources.not_find_wb);
                       return;
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
                       return;
                   }
                   trans.Commit();

                   if (dr.WType == 1 || dr.WType == 16)
                   {
                       using (var wb_in = new frmWayBillIn(dr.WType, wb.WbillId))
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


               }

               catch (EntityCommandExecutionException exception)
               {
                   var e = exception.InnerException as SqlException;
                   if (e != null)
                   {
                       if (!e.Errors.Cast<SqlError>().Any(error =>
                              (error.Number == DeadlockErrorNumber) ||
                              (error.Number == LockingErrorNumber) ||
                              (error.Number == UpdateConflictErrorNumber)))
                       {
                           MessageBox.Show(e.Message);
                       }
                       else
                       {
                           MessageBox.Show(Resources.deadlock);
                       }
                   }
                   else
                   {
                       MessageBox.Show(exception.Message);
                   }

                   return;
               }
           }
       }

       public static void PDEdit(GetPayDocList_Result pd_row)
       {
           if (pd_row == null)
           {
               return;
           }

           using (var db = new BaseEntities())
           {
               var trans = db.Database.BeginTransaction();
               try
               {
                   var pd = db.Database.SqlQuery<PayDoc>("SELECT * from PayDoc WITH (UPDLOCK, NOWAIT) where PayDocId = {0}", pd_row.PayDocId).FirstOrDefault();
                   if (pd == null)
                   {
                       MessageBox.Show(Resources.not_find_wb);
                       return;
                   }

                   if (pd.Checked == 1)
                   {
                       if (MessageBox.Show(Resources.edit_info, "Відміна проводки", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                       {
                           pd.Checked = 0;
                           db.Entry<PayDoc>(pd).State = System.Data.Entity.EntityState.Modified;
                           db.SaveChanges();
                       }
                   }
                   trans.Commit();

                   if (pd.Checked == 0)
                   {
                       using (var pd_form = new frmPayDoc(pd_row.DocType, pd_row.PayDocId))
                       {
                           pd_form.ShowDialog();
                       }
                   }

               }
               catch (EntityCommandExecutionException exception)
               {
                   var e = exception.InnerException as SqlException;
                   if (e != null)
                   {
                       if (!e.Errors.Cast<SqlError>().Any(error =>
                              (error.Number == DeadlockErrorNumber) ||
                              (error.Number == LockingErrorNumber) ||
                              (error.Number == UpdateConflictErrorNumber)))
                       {
                           MessageBox.Show(e.Message);
                       }
                       else
                       {
                           MessageBox.Show(Resources.deadlock);
                       }
                   }
                   else
                   {
                       MessageBox.Show(exception.Message);
                   }

                   return;
               }
           }
       }
    }
}