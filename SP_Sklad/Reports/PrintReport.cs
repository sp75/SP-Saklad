﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SP_Sklad.SkladData;
using SpreadsheetReportBuilder;

namespace SP_Sklad.Reports
{
    public class PrintReport
    {
        public DateTime OnDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public dynamic MatGroup { get; set; }
        public dynamic Kagent { get; set; }
        public dynamic Warehouse { get; set; }
        public dynamic Material { get; set; }
        public String DocStr { get; set; }
        public object DocType { get; set; }
        public dynamic ChType { get; set; }

        private List<object> XLRPARAMS
        {
            get
            {
                var obj = new List<object>();
                obj.Add(new
                {
                    OnDate = OnDate.ToShortDateString(),
                    StartDate = StartDate.ToShortDateString(),
                    EndDate = EndDate.ToShortDateString(),
                    GRP = MatGroup != null ? MatGroup.Name : "",
                    WH = Warehouse != null ? Warehouse.Name : "",
                    KAID = Kagent != null ? Kagent.Name : "",
                    MatId = Material != null ? Material.Name : "",
                    CType = ChType!= null ? ChType.Name :""
                });
                return obj;
            }
        }

        public PrintReport()
        {

        }

        public static string template_path
        {
            get
            {
#if DEBUG
                return Path.Combine(@"c:\WinVSProjects\SP-Sklad\SP_Sklad\", "TempLate");
#else
               return Path.Combine(Application.StartupPath, "TempLate" );
#endif
            }
        }

        public static string rep_path
        {
            get
            {
#if DEBUG
                return Path.Combine(@"c:\WinVSProjects\SP-Sklad\SP_Sklad\", "Rep");
#else
               return Path.Combine(Application.StartupPath, "Rep" );
#endif
            }
        }

        public void CreateReport(int idx)
        {
            //  if( !frmRegistrSoft->unLock ) return ;
            var db = DB.SkladBase();
            var data_for_report = new Dictionary<string, IList>();
            var rel = new List<object>();

            if (idx == 1)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wh = Convert.ToString(Warehouse.WId);
                var mat = db.REP_1(StartDate, EndDate, grp, (int)Kagent.KaId, wh, DocStr).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == grp || grp == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "MatInDet"
                });

                //   var ob = new List<object>();
                //     ob.Add(new { StartDate = StartDate.ToShortDateString(), EndDate = EndDate.ToShortDateString(), GRP = MatGroup.Name, WH = Warehouse.Name, KAID = Kagent.Name });
                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatInDet", mat);
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_1);
            }

            if (idx == 2)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wh = Convert.ToString(Warehouse.WId);
                var mat = db.REP_2(StartDate, EndDate, grp, (int)Kagent.KaId, wh, DocStr).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == grp || grp == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "MatOutDet"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatOutDet", mat);
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_2);
            }

            if (idx == 3)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wh = Convert.ToString(Warehouse.WId);
                int kid = Convert.ToInt32(Kagent.KaId);
                var mat = db.REP_3_14(StartDate, EndDate, grp, kid, wh, "-1,").ToList();

                if (!mat.Any())
                {
                    return;
                }

                var kagents = db.Kagent.Where(w => w.Deleted == 0 && (w.KaId == kid || kid == 0)).Select(s => new { s.KaId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "KaId",
                    fk = "KaId",
                    master_table = "MatGroup",
                    child_table = "MatOutDet"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", kagents.Where(w => mat.Select(s => s.KaId).Contains(w.KaId)).ToList());
                data_for_report.Add("MatOutDet", mat);
                data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
                {
                    Amount = s.Sum(a => a.Amount),
                    Summ = s.Sum(ss => ss.Summ),
                    ReturnAmountIn = s.Sum(r => r.ReturnAmountIn),
                    ReturnSummIn = s.Sum(r => r.ReturnSummIn)
                }).ToList());
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_3);
            }

            if (idx == 14)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wh = Convert.ToString(Warehouse.WId);
                int kid = Convert.ToInt32(Kagent.KaId);
                var mat = db.REP_3_14(StartDate, EndDate, grp, kid, wh, "-1,").ToList();

                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == grp || grp == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "MatOutDet"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatOutDet", mat);
                data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
                {
                    Amount = s.Sum(a => a.Amount),
                    Summ = s.Sum(ss => ss.Summ),
                    ReturnAmountIn = s.Sum(r => r.ReturnAmountIn),
                    ReturnSummIn = s.Sum(r => r.ReturnSummIn)
                }).ToList());
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_3);
            }


            if (idx == 4)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                int kid = Convert.ToInt32(Kagent.KaId);
                string wh = Convert.ToString(Warehouse.WId);
                var mat = db.REP_4_25(StartDate, EndDate, grp, kid, wh, "1,").ToList();

                if (!mat.Any())
                {
                    return;
                }

                var kagents = db.Kagent.Where(w => w.Deleted == 0 && (w.KaId == kid || kid == 0)).Select(s => new { s.KaId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "KaId",
                    fk = "KaId",
                    master_table = "MatGroup",
                    child_table = "MatInDet"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", kagents.Where(w => mat.Select(s => s.KaId).Contains(w.KaId)).ToList());
                data_for_report.Add("MatInDet", mat);
                data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
                {
                    SummPrice = s.Sum(r => r.SummPrice),
                    ReturnSummPriceOut = s.Sum(r => r.ReturnSummPriceOut)
                }).ToList());
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_4);
            }

            if (idx == 25)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                int kid = Convert.ToInt32(Kagent.KaId);
                string wh = Convert.ToString(Warehouse.WId);
                var mat = db.REP_4_25(StartDate, EndDate, grp, kid, wh, "1,").ToList();

                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == grp || grp == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "MatInDet"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatInDet", mat);
                data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
                {
                    SummPrice = s.Sum(r => r.SummPrice),
                    ReturnSummPriceOut = s.Sum(r => r.ReturnSummPriceOut)
                }).ToList());
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_4);
            }


            if (idx == 5)
            {
                var kagents = db.Kagent.Where(w => w.Deleted == 0 && (w.Archived ?? 0) == 0 ).Select(s => new
                {
                    s.Name,
                    Saldo = db.KAgentSaldo.Where(ks => ks.KAId == s.KaId && ks.OnDate <= OnDate).OrderByDescending(o => o.OnDate).Select(kss => kss.Saldo).Take(1).FirstOrDefault()
                }).ToList().Where(w => w.Saldo > 0);

                if (!kagents.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("Kagent", kagents.ToList());

                Print(data_for_report, TemlateList.rep_5);
            }

            if (idx == 6)
            {
                var kagents = db.Kagent.Where(w => w.Deleted == 0 && (w.Archived ?? 0) == 0).Select(s => new
                {
                    s.Name,
                    Saldo = db.KAgentSaldo.Where(ks => ks.KAId == s.KaId && ks.OnDate <= OnDate).OrderByDescending(o => o.OnDate).Select(kss => kss.Saldo).Take(1).FirstOrDefault()
                }).ToList().Where(w => w.Saldo < 0);

                if (!kagents.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("Kagent", kagents.ToList());

                Print(data_for_report, TemlateList.rep_6);
            }

            if (idx == 7)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                int wid = Warehouse.WId == "*" ? 0 : Convert.ToInt32(Warehouse.WId);

                var mat = db.WhMatGet(grp, wid, 0, OnDate, 0, "*", 0, "", 0, 0).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == grp || grp == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "OutGrpId",
                    master_table = "MatGroup",
                    child_table = "MatList"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.OutGrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatList", mat);
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_7);
            }

            if (idx == 8)
            {
                var list = db.GetDocList(StartDate, EndDate, (int)Kagent.KaId, 0).OrderBy(o => o.OnDate).Select(s => new
                {
                    s.OnDate,
                    s.SummAll,
                    s.Saldo,
                    DocName = s.TypeName + " №" + s.Num

                }).ToList();

                if (!list.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("KADocList", list.ToList());

                Print(data_for_report, TemlateList.rep_8);
            }

            if (idx == 9)
            {
                int wid = Warehouse.WId == "*" ? 0 : Convert.ToInt32(Warehouse.WId);

                var list = db.GetMatMove((int)this.Material.MatId, StartDate, EndDate, wid, (int)Kagent.KaId, (int)DocType, "*").ToList();

                if (!list.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatList", list.ToList());

                Print(data_for_report, TemlateList.rep_9);
            }

            if (idx == 10)
            {

                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wid = Convert.ToString(Warehouse.WId);

                var mat = db.REP_10(StartDate, EndDate, grp, wid, 0).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == grp || grp == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "MatList"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatList", mat);
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_10);
            }

            if (idx == 11)
              {
                var list = db.GetDocList(StartDate, EndDate, 0, (int)DocType).OrderBy( o=> o.OnDate ).Select(s => new
                {
                    s.OnDate,
                    s.KaName,
                    s.SummAll,
                    s.Saldo,
                    DocName = s.TypeName + " №" + s.Num
                }).ToList();

                if (!list.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("DocList", list.ToList());

                Print(data_for_report, TemlateList.rep_11);
            }

            if (idx == 13)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                string wid = Convert.ToString(Warehouse.WId);

                var mat = db.REP_13(StartDate, EndDate, grp, (int)Kagent.KaId,  (String)Warehouse.WId, 0).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == grp || grp == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "MatList"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatList", mat);
                data_for_report.Add("_realation_", rel);
                data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
                {
                    SummIn = s.Sum(r => r.SummIn),
                    SummOut = s.Sum(r => r.SummOut),
                    Income = s.Sum(r => r.Income)
                }).ToList());

                Print(data_for_report, TemlateList.rep_13);
            }

            if (idx == 16)
            {
                var paydoc = db.REP_16(StartDate, EndDate, (int)Kagent.KaId, (int)ChType.CTypeId, 1).ToList();

                if (!paydoc.Any())
                {
                    return;
                }

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("DocList", paydoc);

                Print(data_for_report, TemlateList.rep_16);
            }

            if (idx == 19)
            {
                int wid = Warehouse.WId == "*" ? 0 : Convert.ToInt32(Warehouse.WId);
                int mat_id = (int)this.Material.MatId;
                var list = db.GetMatMove((int)this.Material.MatId, StartDate, EndDate, wid, (int)Kagent.KaId, (int)DocType, "*").ToList();

                if (!list.Any())
                {
                    return;
                }

                var satrt_remais = db.WMatGetByWh(mat_id, wid, (int)Kagent.KaId, StartDate, "*").Sum(s => s.Remain);
                var sart_avg_price = db.v_MatRemains.Where(w => w.MatId == mat_id && w.OnDate <= StartDate).OrderByDescending(o => o.OnDate).Select(s => s.AvgPrice).FirstOrDefault();
                var end_remais = db.WMatGetByWh(mat_id, wid, (int)Kagent.KaId, EndDate, "*").Sum(s => s.Remain);
                var end_avg_price = db.v_MatRemains.Where(w => w.MatId == mat_id && w.OnDate <= EndDate).OrderByDescending(o => o.OnDate).Select(s => s.AvgPrice).FirstOrDefault();

                var balances = new List<object>();
                balances.Add(new
                {
                    SARTREMAIN = satrt_remais,
                    SARTAVGPRICE = sart_avg_price,
                    ENDREMAIN = end_remais,
                    ENDAVGPRICE = end_avg_price

                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("Balances", balances);
                data_for_report.Add("MatList", list.ToList());

                Print(data_for_report, TemlateList.rep_19);
            }

            if (idx == 20)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
              
                var svc = db.REP_20(StartDate, EndDate, grp, (int)Kagent.KaId).ToList();

                if (!svc.Any())
                {
                    return;
                }

                var svc_grp = db.SvcGroup.Where(w => w.Deleted == 0 && (w.GrpId == grp || grp == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "SvcGroup",
                    child_table = "SvcOutDet"
                });

                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("SvcGroup", svc_grp.Where(w => svc.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("SvcOutDet", svc);
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_20);
            }

            if (idx == 28)
            {
                int grp = Convert.ToInt32(MatGroup.GrpId);
                var mat = db.OrderedList(StartDate, EndDate, 0, (int)Kagent.KaId, -16, 0).GroupBy(g => new
                {
                    g.BarCode,
                    g.GrpId,
                    g.MatId,
                    g.MatName,
                    g.CurrencyName,
                    g.MsrName

                }).Select(s => new
                {
                    s.Key.BarCode,
                    s.Key.GrpId,
                    s.Key.MatName,
                    s.Key.MsrName,
                    Amount = s.Sum(a => a.Amount),
                    OnSum = s.Sum(sum => sum.Price * sum.Amount)

                }).ToList();

                if (!mat.Any())
                {
                    return;
                }

                var mat_grp = db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == grp || grp == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

                rel.Add(new
                {
                    pk = "GrpId",
                    fk = "GrpId",
                    master_table = "MatGroup",
                    child_table = "MatInDet"
                });


                data_for_report.Add("XLRPARAMS", XLRPARAMS);
                data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
                data_for_report.Add("MatInDet", mat);
                data_for_report.Add("_realation_", rel);

                Print(data_for_report, TemlateList.rep_28);
            }


            /*      if(idx == 18)
                   {
                      SP_WMAT_GET->DataSource = MatGroupDS ;
                      MatGroup->ParamByName("grp")->Value = GRP ;

                      SP_WMAT_GET->ParamByName("ONDATE")->Value = OnDate ;
                      SP_WMAT_GET->ParamByName("GRPID")->Value = GRP ;
                      if(WH == "*" ) SP_WMAT_GET->ParamByName("WID")->Value = 0 ;
                         else SP_WMAT_GET->ParamByName("WID")->Value = WH;
                      SP_WMAT_GET->ParamByName("MINREST")->Value = 1 ;

                      xlReport_18->Params->Items[0]->Value = SkladData->WhComboBoxNAME->Value;
                      xlReport_18->Params->Items[1]->Value = OnDate;
                      xlReport_18->Params->Items[2]->Value = SkladData->MatGroupComboBoxNAME->Value;
                      xlReport_18->Report();
                      SP_WMAT_GET->DataSource = NULL ;
                   }


                  if(idx == 26)
                   {
                      MakedProduct->ParamByName("in_fromdate")->Value = StartDate ;
                      MakedProduct->ParamByName("in_todate")->Value =  EndDate ;
                      MakedProduct->ParamByName("GRPID")->Value = GRP ;
                      MakedProduct->ParamByName("WH")->Value = WH ;
                      xlReport_26->Params->Items[0]->Value = StartDate.DateString() ;
                      xlReport_26->Params->Items[1]->Value = EndDate.DateString() ;
                      xlReport_26->Params->Items[2]->Value = SkladData->MatGroupComboBoxNAME->Value;
                      xlReport_26->Params->Items[3]->Value = SkladData->WhComboBoxNAME->Value;
                      xlReport_26->Report();
                   }

                   if(idx == 27)
                   {
                      RepOrdKAID->ParamByName("IN_FROMDATE")->Value = StartDate;
                      RepOrdKAID->ParamByName("IN_TODATE")->Value =  EndDate;
                      RepOrdKAID->ParamByName("GRPID")->Value = GRP;
                      RepOrdKAID->ParamByName("IN_KAID")->Value = KAID;
                      RepOrdKAID->ParamByName("IN_MATID")->Value = MATID;
                      xlReport_27->Params->Items[0]->Value = StartDate.DateString() ;
                      xlReport_27->Params->Items[1]->Value = EndDate.DateString() ;
                      xlReport_27->Params->Items[2]->Value = SkladData->MatGroupComboBoxNAME->Value;
                      xlReport_27->Params->Items[3]->Value = SkladData->MatComboBoxNAME->Value;
                      xlReport_27->Params->Items[4]->Value = SkladData->KAgentComboBoxNAME->Value;
                      xlReport_27->Report();
                   }

                
                 

                   if(idx == 17)
                   {
                      MatGroup->ParamByName("grp")->Value = GRP ;
                      MatSelPr->DataSource = MatGroupDS ;
                      MatSelPr->ParamByName("IN_WID")->Value = WH;
                      MatSelPr->ParamByName("IN_FROMDATE")->Value = StartDate ;
                      MatSelPr->ParamByName("IN_TODATE")->Value =  EndDate ;
                      MatSelPr->ParamByName("IN_KAID")->Value =  0 ;

                      SvcGroup->ParamByName("grp")->Value = GRP ;
                      SvcOut_20->DataSource = SvcGroupDS ;
                      SvcOut_20->ParamByName("IN_KAID")->Value = 0 ;
                      SvcOut_20->ParamByName("IN_FROMDATE")->Value = StartDate ;
                      SvcOut_20->ParamByName("IN_TODATE")->Value =  EndDate ;

                      DocList_16->ParamByName("IN_KAID")->Value = 0 ;
                      DocList_16->ParamByName("IN_FROMDATE")->Value = StartDate ;
                      DocList_16->ParamByName("IN_TODATE")->Value =  EndDate ;
                      DocList_16->ParamByName("IN_ctype")->Value = 0 ;
                      DocList_16->ParamByName("showall")->Value = 0 ;

                     // WBWriteOff->DataSource = MatGroupDS ;
                      WBWriteOff->ParamByName("IN_FROMDATE")->Value = StartDate ;
                      WBWriteOff->ParamByName("IN_TODATE")->Value =  EndDate ;

                      xlReport_17->Params->Items[0]->Value = StartDate.DateString() ;
                      xlReport_17->Params->Items[1]->Value = EndDate.DateString() ;
                      xlReport_17->Report();

                      MatSelPr->DataSource = NULL ;
                      SvcOut_20->DataSource = NULL ;
                      WBWriteOff->DataSource = NULL ;
                   }

                   if(idx == 23)
                   {
                      PayDocList->ParamByName("IN_FROMDATE")->Value = StartDate ;
                      PayDocList->ParamByName("IN_TODATE")->Value =  EndDate ;
                      MONEY_ONDATE->ParamByName("IN_ONDATE")->Value =  EndDate ;

                      xlReport_23->Params->Items[0]->Value = StartDate.DateString() ;
                      xlReport_23->Params->Items[1]->Value = EndDate.DateString() ;
                    //  xlReport_16->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                    //  xlReport_16->Params->Items[3]->Value = SkladData->ChTypeComboBoxNAME->Value;
                      xlReport_23->Report();
                   }


                   if(idx == 29)
                   {
                      MatGroup->ParamByName("grp")->Value = GRP ;

                      MakedProductShort_29->DataSource = MatGroupDS ;
                      MakedProductShort_29->ParamByName("in_fromdate")->Value = StartDate ;
                      MakedProductShort_29->ParamByName("in_todate")->Value =  EndDate ;
                      MakedProductShort_29->ParamByName("IN_KAID")->Value = KAID ;
                      MakedProductShort_29->ParamByName("IN_WH")->Value = WH ;

                      xlReport_29->Params->Items[0]->Value = StartDate.DateString() ;
                      xlReport_29->Params->Items[1]->Value = EndDate.DateString() ;
                      xlReport_29->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                      xlReport_29->Params->Items[3]->Value = SkladData->WhComboBoxNAME->Value;
                      xlReport_29->Params->Items[4]->Value = SkladData->MatGroupComboBoxNAME->Value;
                      xlReport_29->Report();

                      MakedProductShort_29->DataSource = NULL ;
                   }
                  if(idx == 30)
                   {
                      Shahmatka->ParamByName("IN_KAID")->Value = KAID ;
                      Shahmatka->ParamByName("IN_WTYPE")->Value = 0;
                      Shahmatka->ParamByName("IN_FROMDATE")->Value = StartDate ;
                      Shahmatka->ParamByName("IN_TODATE")->Value =  EndDate ;
                      xlReport_30->Params->Items[0]->Value = StartDate.DateString() ;
                      xlReport_30->Params->Items[1]->Value = EndDate.DateString() ;
                      xlReport_30->Params->Items[2]->Value = SkladData->KAgentComboBoxNAME->Value;
                      xlReport_30->Report();
                   }*/


            //      db.PrintLog.Add(new PrintLog { PrintType = 1, RepId = idx, UserId = DBHelper.CurrentUser.UserId, OnDate = DateTime.Now });
            //     db.SaveChanges();

        }


        private void Print(Dictionary<string, IList> data_for_report, string temlate)
        {

            String result_file = Path.Combine(rep_path, temlate);
            String template_file = Path.Combine(template_path, temlate);
            if (File.Exists(template_file))
            {
                ReportBuilder.GenerateReport(data_for_report, template_file, result_file, false);
            }

            if (File.Exists(result_file))
            {
                Process.Start(result_file);
            }
        }

    }
}
