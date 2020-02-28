﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using DocumentFormat.OpenXml.ReportBuilder;
using SP.Reports.Models;
using SP.Base;
using SP.Reports.Comon;
using SP.Base.Models;
using SP.Reports.Models.Views;

namespace SP.Reports
{
    public class PrintReportv2: BaseReport
    {
        public DateTime OnDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public GrpComboBoxItem MatGroup { get; set; }
        public KagentComboBoxItem Kagent { get; set; }
        public WhComboBoxItem Warehouse { get; set; }
        public MatComboBoxItem Material { get; set; }
        public String DocStr { get; set; }
        public object DocType { get; set; }
        public ChTypeComboBoxItem ChType { get; set; }
        public object Status { get; set; }
        public GrpKagentComboBoxItem KontragentGroup { get; set; }
        public String GrpStr { get; set; }
        public dynamic Person { get; set; }


        private int? _person_id { get; set; }
        private int _user_id { get; set; }


        private List<XLRPARAM> XLR_PARAMS
        {
            get
            {
                var obj = new List<XLRPARAM>
                {
                    new XLRPARAM
                    {
                        OnDate = OnDate.ToShortDateString(),
                        StartDate = StartDate.ToShortDateString(),
                        EndDate = EndDate.ToShortDateString(),
                        GRP = MatGroup != null ? MatGroup.Name : "",
                        WH = Warehouse != null ? Warehouse.Name : "",
                        KAID = Kagent != null ? Kagent.Name : "",
                        MatId = Material != null ? Material.Name : "",
                        CType = ChType != null ? ChType.Name : "",
                        KontragentGroupName = KontragentGroup != null ? KontragentGroup.Name : "",
                        Year = StartDate.Year.ToString()
                    }
                };

                return obj;
            }
        }

        private Dictionary<string, IList> data_for_report { get; set; }
        private List<object> realation { get; set; }
        private int  _rep_id { get; set; }

        public PrintReportv2(int rep_id,  int? person_id, int user_id)
        {
            _person_id = person_id; 
            _user_id = user_id;
            _rep_id = rep_id;

            data_for_report = new Dictionary<string, IList>();
            realation = new List<object>();
        }

        public byte[] CreateReport( string template_file, string file_format)
        {
            int report_mode = 0;

            switch (_rep_id)
            {
                case 1:
                    REP_1();
                    break;
                case 2:
                    REP_2();
                    break;
                case 3:
                    REP_3();
                    break;
                case 4:
                    REP_4();
                    break;
                case 5:
                    REP_5();
            //        report_mode = 1;
                    break;
                case 6:
                    REP_6();
           //         report_mode = 1;
                    break;
                case 7:
                    REP_7();
                    break;
                case 8:
                    REP_8();
                    report_mode = 1;
                    break;
                case 9:
                    REP_9();
                    report_mode = 1;
                    break;
                case 10:
                    REP_10();
                    break;
                case 11:
                    REP_11();
                    report_mode = 1;
                    break;
                case 13:
                    REP_13();
                    break;
                case 14:
                    REP_14();
                    break;
                case 15:
                    REP_15();
                    report_mode = 1;
                    break;
                case 16:
                    REP_16();
                    break;
                case 17:
                    REP_17();
                    break;
                case 18:
                    REP_18();
                    break;
                case 19:
                    REP_19();
                    break;
                case 20:
                    REP_20();
                    break;
                case 22:
                    REP_22();
                    break;
                case 23:
                    REP_23();
                    report_mode = 1;
                    break;
                case 25:
                    REP_25();
                    break;
                case 26:
                    REP_26();
                    report_mode = 1;
                    break;

                case 27:
                    REP_27();
                    report_mode = 1;
                    break;

                case 28:
                    REP_28();
                    break;
                case 29:
                    REP_29();
                    break;
                case 30:
                    REP_30();
                    report_mode = 1;
                    break;
                case 31:
                    REP_31();
                    break;
                case 32:
                    REP_32();
                    break;
                case 33:
                    REP_33();
                    break;
                case 34:
                    REP_34();
                    report_mode = 1;
                    break;
                case 35:
                    REP_35();
                    break;
                case 36:
                    REP_36();
                    report_mode = 1;
                    break;
                case 37:
                    REP_37();
                    break;
                case 38:
                    REP_38();
                    break;
                case 39:
                    REP_39();
                    break;
                case 40:
                    REP_40();
                    report_mode = 1;
                    break;
                case 41:
                    REP_41();
                    break;
                case 42:
                    REP_42();
                    break;

                default:
                    break;
            }

            return report_mode == 0 ? ReportBuilder.GenerateReport(data_for_report, template_file, false, file_format) : ReportBuilderXLS.GenerateReport(data_for_report, template_file).ToArray();
        }

        private void REP_1()
        {
            var mat = _db.REP_1(StartDate, EndDate, MatGroup.GrpId, Kagent.KaId, Warehouse.WId, DocStr, _user_id).ToList();

            var mat_grp = mat.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                Name = s.Key.GrpName,
                Summ = s.Sum(xs => xs.SumPrice)
            }).OrderBy(o => o.Name).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatInDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp);
            data_for_report.Add("MatInDet", mat);
            data_for_report.Add("_realation_", realation);
        }
        private void REP_2()
        {
            int grp = Convert.ToInt32(MatGroup.GrpId);
            string wh = Convert.ToString(Warehouse.WId);
            int status = Convert.ToInt32(Status);

            var mat = _db.REP_2(StartDate, EndDate, grp, Kagent.KaId, wh, DocStr, status, _user_id).AsQueryable().OrderBy(GetSortedList(_rep_id)).ToList();

            if (!mat.Any())
            {
                return;
            }

            var mat_grp = mat.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                Name = s.Key.GrpName,
                Summ = s.Sum(xs => xs.SumPrice)
            }).OrderBy(o => o.Name).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatOutDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp);
            data_for_report.Add("MatOutDet", mat);
            data_for_report.Add("_realation_", realation);
        }
        private void REP_3()
        {
            var mat = _db.REP_3_14(StartDate, EndDate, MatGroup.GrpId, Kagent.KaId, Warehouse.WId, "-1,", _user_id, KontragentGroup.Id).ToList();

            var enterprise_list = EnterpriseList(_person_id).Select(s => (int?)s.KaId);

            var kagents = (from k in _db.KagentList
                           join ew in _db.EnterpriseWorker on k.KaId equals ew.WorkerId into gj
                           from subfg in gj.DefaultIfEmpty()
                           where (subfg.EnterpriseId == null || enterprise_list.Contains(subfg.EnterpriseId)) 
                           select k
                      ).Distinct().ToList().Where(w => w.KaId == Kagent.KaId || Kagent.KaId == 0).Select(s => new { s.KaId, s.Name }).ToList();

            realation.Add(new
            {
                pk = "KaId",
                fk = "KaId",
                master_table = "MatGroup",
                child_table = "MatOutDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", kagents.Where(w => mat.Select(s => s.KaId).Contains(w.KaId)).ToList());
            data_for_report.Add("MatOutDet", mat);
            data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
            {
                Amount = s.Sum(a => a.Amount),
                Summ = s.Sum(ss => ss.Summ),
                ReturnAmountIn = s.Sum(r => r.ReturnAmountIn),
                ReturnSummIn = s.Sum(r => r.ReturnSummIn)
            }).ToList());
            data_for_report.Add("_realation_", realation);
        }
        private void REP_4()
        {
            var mat = _db.REP_4_25(StartDate, EndDate, MatGroup.GrpId, Kagent.KaId, Warehouse.WId, "1,", _user_id).ToList();
            var enterprise_list = EnterpriseList(_person_id).Select(s => (int?)s.KaId);
            var kagents = (from k in _db.KagentList
                           join ew in _db.EnterpriseWorker on k.KaId equals ew.WorkerId into gj
                           from subfg in gj.DefaultIfEmpty()
                           where (subfg.EnterpriseId == null || enterprise_list.Contains(subfg.EnterpriseId))
                           select k
                      ).Distinct().ToList().Where(w => w.KaId == Kagent.KaId || Kagent.KaId == 0).Select(s => new { s.KaId, s.Name }).ToList();

            realation.Add(new
            {
                pk = "KaId",
                fk = "KaId",
                master_table = "MatGroup",
                child_table = "MatInDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", kagents.Where(w => mat.Select(s => s.KaId).Contains(w.KaId)).ToList());
            data_for_report.Add("MatInDet", mat);
            data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
            {
                SummPrice = s.Sum(r => r.SummPrice),
                ReturnSummPriceOut = s.Sum(r => r.ReturnSummPriceOut)
            }).ToList());

            data_for_report.Add("_realation_", realation);
        }
        private void REP_5()
        {
            var kagents = _db.REP_4_5(OnDate).Where(w => w.Saldo > 0).ToList().Select((s, index) => new
            {
                N = index + 1,
                s.Name,
                s.Saldo
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("Kagent", kagents.ToList());
        }
        private void REP_6()
        {
            var kagents = _db.REP_4_5(OnDate).Where(w => w.Saldo < 0).ToList().Select((s, index) => new
            {
                N = index + 1,
                s.Name,
                s.Saldo
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("Kagent", kagents.ToList());
        }
        private void REP_7()
        {
            int grp = Convert.ToInt32(MatGroup.GrpId);
            int wid = Warehouse.WId == "*" ? 0 : Convert.ToInt32(Warehouse.WId);

            var mat = _db.WhMatGet(grp, wid, 0, OnDate, 0, "*", 0, GrpStr, _user_id, 0).Select(s => new
            {
                s.BarCode,
                s.MatName,
                s.MsrName,
                s.Remain,
                s.Rsv,
                s.AvgPrice,
                s.OutGrpId,
                s.GrpName,
            }).OrderBy(o => o.MatName).ToList();

            var mat_grp = mat.GroupBy(g => new { g.GrpName, g.OutGrpId }).Select(s => new
            {
                s.Key.OutGrpId,
                Name = s.Key.GrpName,
                Total = s.Sum(xs => (xs.AvgPrice * xs.Remain)),
            }).OrderBy(o => o.Name).ToList();

            realation.Add(new
            {
                pk = "OutGrpId",
                fk = "OutGrpId",
                master_table = "MatGroup",
                child_table = "MatList"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp);
            data_for_report.Add("MatList", mat);
            data_for_report.Add("_realation_", realation);
        }
        private void REP_8()
        {
            var list = _db.GetDocList(StartDate, EndDate, Kagent.KaId, 0).OrderBy(o => o.OnDate).Select(s => new
            {
                s.Num,
                s.TypeName,
                s.OnDate,
                SummAll = s.SummInCurr,
                s.Saldo,
                DocName = s.TypeName + " №" + s.Num

            }).ToList();

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("KADocList", list.ToList());
        }
        private void REP_9()
        {
            int wid = Warehouse.WId == "*" ? 0 : Convert.ToInt32(Warehouse.WId);

            var list = _db.GetMatMove(Material.MatId, StartDate, EndDate, wid, Kagent.KaId, (int)DocType, "*", KontragentGroup.Id, _user_id).ToList();

            if (!list.Any())
            {
                return;
            }

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatList", list.ToList());
        }
        private void REP_10()
        {
            var mat = _db.REP_10(StartDate, EndDate, MatGroup.GrpId, Warehouse.WId, 0, _user_id).OrderBy(o => o.MatId).ToList();

            var mat_grp = mat.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                Name = s.Key.GrpName,
                SummIn = s.Sum(xs => xs.SummIn),
                SummOut = s.Sum(xs => xs.SummOut),
                SummStart = s.Sum(xs => xs.SummStart),
                SummEnd = s.Sum(xs => xs.SummEnd)
            }).OrderBy(o => o.Name).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatList"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp);
            data_for_report.Add("MatList", mat);
            data_for_report.Add("_realation_", realation);
        }
        private void REP_11()
        {
            var list = _db.GetDocList(StartDate, EndDate, 0, (int)DocType).OrderBy(o => o.OnDate).Select(s => new
            {
                s.OnDate,
                s.KaName,
                SummAll = s.SummInCurr,
                s.Saldo,
                DocName = s.TypeName + " №" + s.Num
            }).ToList();

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("DocList", list.ToList());
        }
        private void REP_13()
        {
            var mat = _db.REP_13(StartDate, EndDate, MatGroup.GrpId, Kagent.KaId, Warehouse.WId, 0, GrpStr).ToList();

            var gs = !String.IsNullOrEmpty(GrpStr) ? GrpStr.Split(',').Select(s => Convert.ToInt32(s)).ToList() : new List<int>();
            var mat_grp = _db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == MatGroup.GrpId || MatGroup.GrpId == 0 || gs.Contains(w.GrpId))).Select(s => new { s.GrpId, s.Name }).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatList"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
            data_for_report.Add("MatList", mat);
            data_for_report.Add("_realation_", realation);
            data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
            {
                SummIn = s.Sum(r => r.SummIn),
                SummOut = s.Sum(r => r.SummOut),
                Income = s.Sum(r => r.Income),
                Rentabelnist = s.Average(a => (a.SummOut - a.SummIn - a.Income) > 0 && a.Income > 0 ? a.Income / (a.SummOut - a.SummIn - a.Income) : 0)
            }).ToList());
        }
        private void REP_14()
        {
            var mat = _db.REP_3_14(StartDate, EndDate, MatGroup.GrpId, Kagent.KaId, Warehouse.WId, DocStr, _user_id, KontragentGroup.Id).ToList();
            var mat_grp = _db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == MatGroup.GrpId || MatGroup.GrpId == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatOutDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
            data_for_report.Add("MatOutDet", mat);
            data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
            {
                Amount = s.Sum(a => a.Amount),
                Summ = s.Sum(ss => ss.Summ),
                ReturnAmountIn = s.Sum(r => r.ReturnAmountIn),
                ReturnSummIn = s.Sum(r => r.ReturnSummIn)
            }).ToList());

            data_for_report.Add("_realation_", realation);
        }
        private void REP_15()
        {
            var wb_list = _db.REP_15(StartDate, EndDate, Kagent.KaId, Material.MatId).OrderBy(o => o.OnDate).ToList();

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("WbList", wb_list);
        }
        private void REP_16()
        {
            var paydoc = _db.REP_16(StartDate, EndDate, Kagent.KaId, ChType.CTypeId, 1).ToList();

            var ch_t = paydoc.GroupBy(g => new { g.CTypeId, g.ChargeName }).Select(s => new { s.Key.CTypeId, s.Key.ChargeName }).OrderBy(o => o.ChargeName).ToList();

            realation.Add(new
            {
                pk = "CTypeId",
                fk = "CTypeId",
                master_table = "ChargeTypeGroup",
                child_table = "DocList"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("ChargeTypeGroup", ch_t);
            data_for_report.Add("DocList", paydoc);
            data_for_report.Add("_realation_", realation);
            data_for_report.Add("SummaryField", paydoc.GroupBy(g => 1).Select(s => new
            {
                Total = s.Sum(r => r.Total)
            }).ToList());
        }
        private void REP_17()
        {
            decimal? total = 0;
            data_for_report.Add("XLRPARAMS", XLR_PARAMS);

            var mat = _db.REP_13(StartDate, EndDate, 0, 0, "*", 0, GrpStr).ToList();
            var mat_grp = mat.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                Name = s.Key.GrpName,
                Income = s.Sum(xs => xs.SummOut - (xs.AmountOut * xs.AvgPrice))
            }).OrderBy(o => o.Name).ToList();
            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatList"
            });
            data_for_report.Add("MatGroup", mat_grp);
            data_for_report.Add("MatList", mat);
            total += mat_grp.Sum(s => s.Income);


            var mat2 = _db.REP_13(StartDate, EndDate, 0, 0, "*", 1, GrpStr).ToList();
            var mat_grp2 = mat2.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                Name = s.Key.GrpName,
                Income = s.Sum(xs => xs.SummIn - (xs.AmountIn * xs.AvgPrice))
            }).OrderBy(o => o.Name).ToList();
            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup2",
                child_table = "MatSelPr"
            });
            data_for_report.Add("MatGroup2", mat_grp2);
            data_for_report.Add("MatSelPr", mat2);
            total -= mat_grp2.Sum(s => s.Income);


            var svc = _db.REP_20(StartDate, EndDate, 0, 0).ToList();
            var svc_grp = svc.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                Name = s.Key.GrpName,
                Total = s.Sum(xs => xs.Summ)
            }).OrderBy(o => o.Name).ToList();
            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "SvcGroup",
                child_table = "SvcOutDet"
            });
            data_for_report.Add("SvcGroup", svc_grp.Where(w => svc.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
            data_for_report.Add("SvcOutDet", svc);
            total += svc_grp.Sum(s => s.Total);

            var paydoc = _db.REP_16(StartDate, EndDate, 0, 0, 0).ToList();
            data_for_report.Add("DocList", paydoc);
            total -= paydoc.Sum(s => s.Total);


            var mat3 = _db.REP_17(StartDate, EndDate, 0, 0).ToList();
            var mat_grp3 = mat3.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                Name = s.Key.GrpName,
                Summ = s.Sum(xs => xs.Summ)
            }).OrderBy(o => o.Name).ToList();
            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup3",
                child_table = "WBWriteOff"
            });
            data_for_report.Add("MatGroup3", mat_grp3);
            data_for_report.Add("WBWriteOff", mat3);
            total -= mat_grp3.Sum(s => s.Summ);

            data_for_report.Add("_realation_", realation);


            var obj = new List<object>();
            obj.Add(new
            {
                Total = total  //=K12-K22+K32-K39-K49
            });
            data_for_report.Add("Summary", obj);
        }
        private void REP_18()
        {
            int grp = Convert.ToInt32(MatGroup.GrpId);
            int wid = Warehouse.WId == "*" ? 0 : Convert.ToInt32(Warehouse.WId);

            var mat = _db.WhMatGet(grp, wid, 0, OnDate, 0, "*", 0, "", _user_id, 0).Where(w => w.Remain < w.MinReserv && w.MinReserv != null).ToList();

             var mat_grp = _db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == grp || grp == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "OutGrpId",
                master_table = "MatGroup",
                child_table = "MatList"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.OutGrpId).Contains(w.GrpId)).ToList());
            data_for_report.Add("MatList", mat);
            data_for_report.Add("_realation_", realation);
        }
        private void REP_19()
        {
            int wid = Warehouse.WId == "*" ? 0 : Convert.ToInt32(Warehouse.WId);
            int mat_id = (int)this.Material.MatId;
            Guid grp_kg = KontragentGroup.Id;
            var list = _db.GetMatMove((int)this.Material.MatId, StartDate, EndDate, wid, (int)Kagent.KaId, (int)DocType, "*", grp_kg, _user_id).ToList();

            var satrt_remais = _db.MatRemainByWh(mat_id, wid, (int)Kagent.KaId, StartDate, "*", _user_id).Sum(s => s.Remain);
            var sart_avg_price = _db.v_MatRemains.Where(w => w.MatId == mat_id && w.OnDate <= StartDate).OrderByDescending(o => o.OnDate).Select(s => s.AvgPrice).FirstOrDefault();
            var end_remais = _db.MatRemainByWh(mat_id, wid, (int)Kagent.KaId, EndDate, "*", _user_id).Sum(s => s.Remain);
            var end_avg_price = _db.v_MatRemains.Where(w => w.MatId == mat_id && w.OnDate <= EndDate).OrderByDescending(o => o.OnDate).Select(s => s.AvgPrice).FirstOrDefault();

            var balances = new List<object>();
            balances.Add(new
            {
                SARTREMAIN = satrt_remais,
                SARTAVGPRICE = sart_avg_price,
                ENDREMAIN = end_remais,
                ENDAVGPRICE = end_avg_price

            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("Balances", balances);
            data_for_report.Add("MatList", list.ToList());
        }
        private void REP_20()
        {
            var svc = _db.REP_20(StartDate, EndDate, MatGroup.GrpId, (int)Kagent.KaId).ToList();

            var svc_grp = _db.SvcGroup.Where(w => w.Deleted == 0 && (w.GrpId == MatGroup.GrpId || MatGroup.GrpId == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "SvcGroup",
                child_table = "SvcOutDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("SvcGroup", svc_grp.Where(w => svc.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
            data_for_report.Add("SvcOutDet", svc);
            data_for_report.Add("_realation_", realation);
        }
        private void REP_22()
        {
            int person = (int)Person.KaId;

            var sql_1 = @"
   	            select m.GrpId, m.name Name, wbd.amount Amount, wbd.total Summ, ms.ShortName, wbl.WbillId, person.Name PersonName , person.KaId PersonId , wbl.KaId , ka.Name KontragentName

                from waybilldet wbd
                join waybilllist wbl on wbl.wbillid = wbd.wbillid
                join materials m on m.matid = wbd.matid
                join measures ms on ms.mid = m.mid
			    join kagent person on person.kaid = wbl.PersonId
                join kagent ka on ka.kaid = wbl.KaId

                where  wbl.checked = 1 and wbl.WType = -1
                       and wbl.ondate between {0} and {1}
                       and person.KaId = {2}
   
			    order by  m.name ";

            var waybill_list = _db.Database.SqlQuery<rep_22>(sql_1, StartDate, EndDate, person).ToList();

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("WbList", waybill_list.GroupBy(g => new { g.Name, g.ShortName, g.PersonName }).Select(s => new
            {
                Name = s.Key.Name,
                ShortName = s.Key.ShortName,
                PersonName = s.Key.PersonName,
                Amount = s.Sum(a => a.Amount),
                Summ = s.Sum(su => su.Summ)
            }).ToList());

            data_for_report.Add("MeasuresList", waybill_list.GroupBy(g => new { g.ShortName }).Select(s => new
            {
                ShortName = s.Key.ShortName,
                Amount = s.Sum(a => a.Amount),
                Summ = s.Sum(su => su.Summ)
            }).ToList());


            data_for_report.Add("KagentList", waybill_list.GroupBy(g => g.KontragentName).Select(s => new
            {
                Name = s.Key,
                Amount = s.Select(d => d.WbillId).Distinct().Count(),
                Summ = s.Sum(su => su.Summ)
            }).ToList());
        }
        private void REP_23()
        {
            data_for_report.Add("XLRPARAMS", XLR_PARAMS);

            data_for_report.Add("DocList1", _db.GetPayDocList("1", StartDate, EndDate, 0, 1, -1, _person_id).ToList());
            data_for_report.Add("DocList2", _db.GetPayDocList("-1", StartDate, EndDate, 0, 1, -1, _person_id).ToList());
            data_for_report.Add("DocList3", _db.GetPayDocList("-2", StartDate, EndDate, 0, 1, -1, _person_id).ToList());
            data_for_report.Add("DocList4", _db.GetPayDocList("6", StartDate, EndDate, 0, 1, -1, _person_id).ToList());

            var m = _db.MoneyOnDate(EndDate).GroupBy(g => new { g.SaldoType, g.Currency }).Select(s => new { s.Key.SaldoType, s.Key.Currency, Saldo = s.Sum(sum => sum.Saldo) }).ToList();
            data_for_report.Add("MONEY1", m.Where(w => w.SaldoType == 0).ToList());
            data_for_report.Add("MONEY2", m.Where(w => w.SaldoType == 1).ToList());
        }
        private void REP_25()
        {
            var mat = _db.REP_4_25(StartDate, EndDate, MatGroup.GrpId, Kagent.KaId, Warehouse.WId, DocStr, _user_id).ToList();

            var mat_grp = _db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == MatGroup.GrpId || MatGroup.GrpId == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatInDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
            data_for_report.Add("MatInDet", mat);
            data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
            {
                SummPrice = s.Sum(r => r.SummPrice),
                ReturnSummPriceOut = s.Sum(r => r.ReturnSummPriceOut)
            }).ToList());
            data_for_report.Add("_realation_", realation);
        }
        private void REP_26()
        {
            var make = _db.WBListMake(StartDate, EndDate, 1, Warehouse.WId, MatGroup.GrpId, -20).ToList().Concat(_db.WBListMake(StartDate, EndDate, 1, Warehouse.WId, MatGroup.GrpId, -22).ToList());

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MakedProduct", make.ToList());
        }
        private void REP_27()
        {
            var mat = _db.REP_27(StartDate, EndDate, Kagent.KaId, MatGroup.GrpId, Material.MatId, KontragentGroup.Id, (int)Person.KaId).ToList();

            if (!mat.Any())
            {
                return;
            }

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatList", mat.GroupBy(g => new
            {
                g.KaName,
                g.MatName,
                g.MsrName,
                g.BarCode
            }).Select(s => new
            {
                s.Key.BarCode,
                s.Key.MatName,
                s.Key.MsrName,
                s.Key.KaName,
                AmountOrd = s.Sum(su => su.AmountOrd),
                TotalOrd = s.Sum(su => su.TotalOrd),
                AmountOut = s.Sum(su => su.AmountOut),
                TotalOut = s.Sum(su => su.TotalOut),
                PersonName = String.Join(", ", s.Select(su => su.PersonName).Distinct())
            }).ToList());
        }
        private void REP_28()
        {
            var mat = _db.OrderedList(StartDate, EndDate, 0, Kagent.KaId, -16, 0, KontragentGroup.Id).Where(w => w.GrpId == MatGroup.GrpId || MatGroup.GrpId == 0).GroupBy(g => new
            {
                g.BarCode,
                g.GrpId,
                g.MatId,
                g.MatName,
                g.CurrencyName,
                g.MsrName

            }).Select(s => new
            {
                s.Key.MatId,
                s.Key.BarCode,
                s.Key.GrpId,
                s.Key.MatName,
                s.Key.MsrName,
                Amount = s.Sum(a => a.Amount),
                OnSum = s.Sum(sum => sum.Price * sum.Amount)

            }).OrderBy(o => o.MatId).ToList();

            var mat_grp = _db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == MatGroup.GrpId || MatGroup.GrpId == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatInDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).OrderBy(o => o.Name).ToList());
            data_for_report.Add("MatInDet", mat);
            data_for_report.Add("_realation_", realation);
        }
        private void REP_29()
        {
            var mat = _db.REP_29(StartDate, EndDate, Kagent.KaId, MatGroup.GrpId, Warehouse.WId).ToList();

            var mat_grp = _db.MatGroup.Where(w => w.Deleted == 0 && (w.GrpId == MatGroup.GrpId || MatGroup.GrpId == 0)).Select(s => new { s.GrpId, s.Name }).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatInDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp.Where(w => mat.Select(s => s.GrpId).Contains(w.GrpId)).ToList());
            data_for_report.Add("MatInDet", mat);
            data_for_report.Add("_realation_", realation);

        }
        private void REP_30()
        {
            var list = _db.GetDocList(StartDate, EndDate, Kagent.KaId, 0).OrderBy(o => o.OnDate).ToList().Where(w => new int[] { 1, -1, 3, -3, -6, 6, -23, 23 }.Any(a => a == w.WType)).Select((s, index) => new
            {
                idx = index + 1,
                s.OnDate,
                SummAll = s.SummInCurr,
                s.Saldo,
                DocName = s.TypeName + " №" + s.Num,
                PN = s.WType == 1 ? s.SummInCurr : null,
                VN = s.WType == -1 ? s.SummInCurr : null,
                PKO = s.WType == 3 ? s.SummInCurr : null,
                VKO = s.WType == -3 ? s.SummInCurr : null,
                PDP = s.WType == -6 ? s.SummInCurr : null,
                PVK = s.WType == 6 ? s.SummInCurr : null,
                SZP = s.WType == -23 ? s.SummInCurr : null,
                SZK = s.WType == 23 ? s.SummInCurr : null
            }).OrderBy(o => o.OnDate);

            if (!list.Any())
            {
                return;
            }

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("KADocList", list.ToList());
        }
        private void REP_31()
        {
            var mat = _db.REP_31(StartDate, EndDate, MatGroup.GrpId, Material.MatId).ToList().OrderBy(o => o.MatName).ToList();

            var mat_grp = mat.GroupBy(g => new { g.GrpName, g.GrpId }).Select(s => new
            {
                s.Key.GrpId,
                Name = s.Key.GrpName,
                TotalOrd = s.Sum(xs => xs.TotalOrd),
                TotalOut = s.Sum(xs => xs.TotalOut)
            }).OrderBy(o => o.Name).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "MatList"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", mat_grp);
            data_for_report.Add("MatList", mat);
            data_for_report.Add("_realation_", realation);
        }
        private void REP_32()
        {
            var mat = _db.REP_32(StartDate, EndDate).ToList();

            var drivers = mat.Select(s => new { s.DriverId, s.DriverName }).Distinct().ToList();
            var routes = mat.Select(s => new { s.DriverId, s.RouteName }).Distinct().ToList();
            var mat_out = mat.GroupBy(g => new { g.DriverId, g.DriverName, g.MatName }).Select(s => new
            {
                s.Key.DriverId,
                s.Key.MatName,
                Amount = s.Sum(sum => sum.Amount)
            }).ToList();

            realation.Add(new
            {
                pk = "DriverId",
                fk = "DriverId",
                master_table = "Drivers",
                child_table = "Routes"
            });

            realation.Add(new
            {
                pk = "DriverId",
                fk = "DriverId",
                master_table = "Drivers",
                child_table = "MatList"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("Drivers", drivers);
            data_for_report.Add("Routes", routes);
            data_for_report.Add("MatList", mat_out);
            data_for_report.Add("_realation_", realation);
        }
        private void REP_33()
        {
            var make = _db.REP_33(StartDate, EndDate, MatGroup.GrpId, Material.MatId).OrderBy(o => o.OnDate).ToList();

            realation.Add(new
            {
                pk = "MatId",
                fk = "MatId",
                master_table = "MatList",
                child_table = "WBList"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatList", make.GroupBy(o => new
            {
                o.MatId,
                o.MatName
            }).Select(s => new { s.Key.MatId, s.Key.MatName }).ToList());
            data_for_report.Add("WBList", make.ToList());
            data_for_report.Add("_realation_", realation);
        }
        private void REP_34()
        {
            var r = _db.WaybillList.Where(w => w.WType == -20 && (w.Checked == 0 || w.Checked == 2) && w.OnDate <= OnDate).SelectMany(s => s.TechProcDet).Where(w => w.MatId != null).Select(s => new
            {
                s.MatId,
                s.WaybillList.WayBillMake.MatRecipe.Materials.Name
            }).ToList();

            var list = _db.Materials.Where(w => w.TypeId == 1).ToList().Select(s => new
            {
                s.Name,
                s.Artikul,
                s.Weight,
                Status = r.Any(a => a.MatId == s.MatId) ? r.FirstOrDefault(f => f.MatId == s.MatId).Name : "Вільна"
            });


            if (!list.Any())
            {
                return;
            }

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("DocList", list.ToList());
        }
        private void REP_35()
        {
            var make = _db.REP_35(StartDate, EndDate, MatGroup.GrpId, Material.MatId).OrderBy(o => o.OnDate).ToList();

            realation.Add(new
            {
                pk = "MatId",
                fk = "MatId",
                master_table = "MatList",
                child_table = "WBList"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatList", make.GroupBy(o => new { o.MatId, o.MatName }).Select(s => new { s.Key.MatId, s.Key.MatName }).ToList());
            data_for_report.Add("WBList", make.ToList());
            data_for_report.Add("_realation_", realation);
        }
        private void REP_36()
        {
            var disc = _db.DiscCards.Select(s => new
            {
                s.Num,
                s.OnValue,
                KaName = s.Kagent != null ? s.Kagent.Name : "",
                Total = _db.WayBillDetAddProps.Where(w => w.CardId == s.CardId && w.WaybillDet.OnDate <= OnDate).Sum(t => t.WaybillDet.Total)
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("DiscCards", disc.ToList());
        }
        private void REP_37()
        {
            int wh_id = Convert.ToInt32(Warehouse.WId);
            var make = _db.REP_37(wh_id, StartDate, EndDate).OrderBy(o => o.Num).ToList();

            realation.Add(new
            {
                pk = "GrpId",
                fk = "GrpId",
                master_table = "MatGroup",
                child_table = "WayBillItems"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", make.GroupBy(o => new { o.GrpId, o.GrpName }).Select(s => new { s.Key.GrpId, s.Key.GrpName }).OrderBy(o => o.GrpName).ToList());
            data_for_report.Add("WayBillItems", make.ToList());
            data_for_report.Add("_realation_", realation);
            data_for_report.Add("SummaryField", make.GroupBy(g => 1).Select(s => new
            {
                SummAll = s.Sum(a => a.SumAll),
            }).ToList());
        }
        private void REP_38()
        {
            var sql_1 = @"
   select [WayBillMake].WbillId  , [WaybillList].Num, [WaybillList].OnDate, rec.Name as RecipeName, [WayBillMake].Amount
  from  [sp_base].[dbo].[WayBillMake] 
  inner join [sp_base].[dbo].[WaybillList] on [WaybillList].WbillId = [WayBillMake].WbillId
  join [sp_base].[dbo].[MatRecipe] on [MatRecipe].RecId = [WayBillMake].RecId 
  join [sp_base].[dbo].[Materials] rec on rec.MatId = [MatRecipe].MatId
  where [WaybillList].OnDate between  {0} and {1} and [WaybillList].WType = -20
  order by rec.Name , [WaybillList].OnDate
     ";

            var waybill_list = _db.Database.SqlQuery<make_wb>(sql_1, StartDate, EndDate).ToList();


            if (!waybill_list.Any())
            {
                return;
            }

            var sql_2 = @"
  select [WayBillMake].WbillId , s_mat.Name, [WaybillDet].Amount, ( ROUND( ([WayBillMake].Amount / [MatRecipe].Amount), 0) * [MatRecDet].AMOUNT )  as  RecAmount
  from  [WayBillMake] 
  join [sp_base].[dbo].[WaybillList] on [WaybillList].WbillId = [WayBillMake].WbillId
  join [sp_base].[dbo].[MatRecipe] on [MatRecipe].RecId = [WayBillMake].RecId 
  join [sp_base].[dbo].[WaybillDet] on [WayBillMake].[WbillId] = [WaybillDet].[WbillId]
  join [sp_base].[dbo].[Materials] s_mat on s_mat.MatId = [WaybillDet].MatId
  left outer join [sp_base].[dbo].[MatRecDet] on [MatRecDet].RecId = [WayBillMake].RecId and [WaybillDet].MatId = [MatRecDet].MatId
  where [WaybillList].OnDate between {0} and {1} and [WaybillList].WType = -20 and [MatRecipe].Amount > 0";

            var use_rec_mat = _db.Database.SqlQuery<use_rec_mat>(sql_2, StartDate, EndDate).ToList().OrderBy(o => o.Name).ToList();

            realation.Add(new
            {
                pk = "WbillId",
                fk = "WbillId",
                master_table = "WaybillList",
                child_table = "UseMatRecipe"
            });

            var sql_3 = @"
  select [WayBillMake].WbillId , rec_mat.Name, [MatRecDet].Amount as RecAmount 
  from  [WayBillMake] 
  inner join [WaybillList] on [WaybillList].WbillId = [WayBillMake].WbillId
  join [MatRecDet] on [MatRecDet].RecId = [WayBillMake].RecId 
  join [Materials] rec_mat on rec_mat.MatId = [MatRecDet].MatId
  where [WaybillList].OnDate between {0} and {1} and [WaybillList].WType = -20 
  and [MatRecDet].MatId not in (select MatId from [sp_base].[dbo].[WaybillDet] where WbillId = [WayBillMake].WbillId)";

            var not_use_rec_mat = _db.Database.SqlQuery<not_use_rec_mat>(sql_3, StartDate, EndDate).ToList().OrderBy(o => o.Name).ToList();

            realation.Add(new
            {
                pk = "WbillId",
                fk = "WbillId",
                master_table = "WaybillList",
                child_table = "NotUseMatRecipe"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("WaybillList", waybill_list);
            data_for_report.Add("UseMatRecipe", use_rec_mat);
            data_for_report.Add("NotUseMatRecipe", not_use_rec_mat);
            data_for_report.Add("_realation_", realation);
        }
        private void REP_39()
        {
            int grp = Convert.ToInt32(MatGroup.GrpId);
            string wh = Convert.ToString(Warehouse.WId);
            int kid = Convert.ToInt32(Kagent.KaId);
            Guid grp_kg = KontragentGroup.Id;
            var mat = _db.REP_39(StartDate, EndDate, grp, kid, wh, "-1,", _user_id, grp_kg).OrderBy(GetSortedList(_rep_id)).ToList();

            if (!mat.Any())
            {
                return;
            }

            var kagents = mat.GroupBy(g => new
            {
                g.KaId,
                g.KaName
            }).Select(s => new
            {
                s.Key.KaId,
                Name = s.Key.KaName,
                TotalAmount = s.Sum(a => a.Amount)
            }).OrderByDescending(o => o.TotalAmount).ToList();

            realation.Add(new
            {
                pk = "KaId",
                fk = "KaId",
                master_table = "MatGroup",
                child_table = "MatOutDet"
            });

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", kagents);
            data_for_report.Add("MatOutDet", mat);
            data_for_report.Add("SummaryField", mat.GroupBy(g => 1).Select(s => new
            {
                Amount = s.Sum(a => a.Amount),
                Summ = s.Sum(ss => ss.Summ),
                ReturnAmountIn = s.Sum(r => r.ReturnAmountIn),
                ReturnSummIn = s.Sum(r => r.ReturnSummIn)
            }).ToList());
            data_for_report.Add("_realation_", realation);
        }
        private void REP_40()
        {
            var list = _db.GetUsedMaterials(Material.MatId, OnDate.Date.AddDays(1), -1).OrderBy(o => o.KaName).ToList();

            var k = Kagent.KaId;
            if (k > 0)
            {
                list = list.Where(w => w.KaId == k).ToList();
            }

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("DiscCards", list.ToList());
        }
        private void REP_41()
        {
            var kagent = _db.REP_41(new DateTime(StartDate.Year, 1, 1), KontragentGroup.Id).OrderBy(GetSortedList(_rep_id)).ToList();

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("KagentRange", kagent.ToList());
        }

        private void REP_42()
        {
            var report_query = _db.WaybillDet.Where(w => w.WaybillList.WType == -20 && w.WaybillList.OnDate >= StartDate && w.WaybillList.OnDate < EndDate).Select(s => new
            {
                s.WaybillList.Num,
                RecipeName = s.WaybillList.WayBillMake.MatRecipe.Materials.Name,
                s.WaybillList.OnDate,
                RawMatName = s.Materials.Name,
                MsrName = s.Materials.Measures.ShortName,
                s.Amount,
                s.Total,
                s.Price,
                s.MatId,
                PersonName = s.WaybillList.Kagent.Name,
                s.Materials.GrpId
            });

            if (GrpStr.Any())
            {
                var groups = GrpStr.Split(',').Select(s => (int?)Convert.ToInt32(s)).ToList();

                report_query = report_query.Where(w => groups.Contains(w.GrpId));
            }
            else if (MatGroup.GrpId > 0)
            {
                report_query = report_query.Where(w => w.GrpId == MatGroup.GrpId);
            }

            if (Material.MatId > 0)
            {
                report_query = report_query.Where(w => w.MatId == Material.MatId);
            }

            var report = report_query.OrderBy(o => o.OnDate).ToList();

            var recipe = report.GroupBy(g => new { g.MatId, g.RawMatName }).Select(s => new
            {
                s.Key.MatId,
                s.Key.RawMatName,
                TotalAmount = s.Sum(sa => sa.Amount)
            }).ToList();

            realation.Add(new
            {
                pk = "MatId",
                fk = "MatId",
                master_table = "MatGroup",
                child_table = "MatOutDet"
            });

            data_for_report.Add("_realation_", realation);

            data_for_report.Add("XLRPARAMS", XLR_PARAMS);
            data_for_report.Add("MatGroup", recipe);
            data_for_report.Add("MatOutDet", report);
        }


        private string GetSortedList(int rep_id)
        {
            string result = "";
            var list = _db.ReportSortedFields.Where(w => w.RepId == rep_id && w.OrderDirection != 0).OrderBy(o => o.Idx).ToList();

            foreach (var i in list)
            {
                result += $"{i.FieldName} {(i.OrderDirection == 2 ? "desc" : "asc")},";
            }

            return result.Trim(',');
        }

        private List<Enterprise> EnterpriseList(int? currentuser_kaid)
        {

            using (var db = Database.SPBase())
            {
                return db.Kagent.Where(w => w.KType == 3 && w.Deleted == 0 && (w.Archived == null || w.Archived == 0))
                    .Join(db.EnterpriseWorker.Where(ew => ew.WorkerId == currentuser_kaid), w => w.KaId, ew => ew.EnterpriseId, (w, ew) => new Enterprise
                    {
                        KaId = w.KaId,
                        Name = w.Name,
                        NdsPayer = w.NdsPayer
                    }).ToList();
            }

        }

    }


   


   

    
}
