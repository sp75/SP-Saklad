using SkladEngine.ModelViews;
using SP.Base.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkladEngine.WayBills
{
    public static class GetWaybillDet
    {
        public static List<GetWaybillDetIn_Result> GetWaybillDetIn(int wbill_id, SPBaseModel db)
        {
            return db.WaybillDet.Where(w => w.WbillId == wbill_id && (w.WaybillList.WType != 6 || (w.WaybillList.WType == 6 && !db.WMatTurn.Any(a => a.SourceId == w.PosId)))).Select(s => new GetWaybillDetIn_Result
            {
                Num = s.Num,
                PosId = s.PosId,
                WbillId = s.WbillId,
                MatId = s.MatId,
                Wid = s.WId,
                Amount = s.Amount,
                Price = s.Price,
                Discount = s.Discount,
                Nds = s.Nds,
                CurrId = s.CurrId,
                OnDate = s.OnDate,
                PtypeId = s.PtypeId,
                Checked = s.Checked,
                MatName = s.Materials.Name,
                MsrName = s.Materials.Measures.ShortName,
                WhName = s.Warehouse.Name,
                Artikul = s.Materials.Artikul,
                CurrName = s.Currency.ShortName,
                OnValue = s.OnValue,
                PosType = s.WayBillDetAddProps == null ? 0 : 2,
                Norm = 1,
                Producer = s.WayBillDetAddProps == null ? "" : s.WayBillDetAddProps.Producer,
                Gtd = s.WayBillDetAddProps == null ? "" : s.WayBillDetAddProps.GTD,
                CertNum = s.WayBillDetAddProps == null ? "" : s.WayBillDetAddProps.CertNum,
                CertDate = s.WayBillDetAddProps == null ? null : s.WayBillDetAddProps.CertDate,
                Serials = s.Materials.Serials,
                Barcode = s.Materials.BarCode,
                GrpId = s.Materials.GrpId,
                Country = s.Materials.Countries.Name,
                Archived = s.Materials.Archived,
                SerialNo = s.Serials.Select(ss => ss.SerialNo).FirstOrDefault(),
                FullPrice = s.BasePrice,
                SvcToPrice = null,
                WbMaked = s.WayBillDetAddProps == null ? null : s.WayBillDetAddProps.WbMaked,
                Total = s.Total,
                BasePrice = s.BasePrice,
                SumNds = (s.Price * s.Amount) * (s.Nds / 100),
                GrpName = s.Materials.MatGroup.Name,
                DiscountPrice = ((s.BasePrice * s.OnValue) * s.Discount / 100),
                Notes = s.Notes,
                UpdateAt = s.UpdateAt,
                TotalInCurrency = s.Total * s.OnValue,
                MId = s.Materials.MId
            }).ToList();


        }
        /*
         
          select wbd.Num, wbd.PosId, wbd.WbillId, wbd.MatId, wbd.wid Wid, wbd.Amount, wbd.Price,
   wbd.Discount, wbd.Nds, wbd.CurrId, wbd.OnDate, wbd.PtypeId, wbd.Checked,
   mat.name as MatName, msr.shortname as MsrName, wh.name as WhName, mat.Artikul, curr.shortname as CurrName, 
   cast(wbd.onvalue as numeric(15,8)) as OnValue,
    (case
      when wbdap.wbmaked is null then 0
      else 2
    end) as PosType, cast(1 as numeric(15,8)) as Norm,
   wbdap.Producer, wbdap.Gtd, wbdap.CertNum, wbdap.CertDate, mat.Serials, mat.Barcode,
   mat.GrpId, c.name as Country, mat.Archived, s.SerialNo, wbd.BasePrice as FullPrice, 
   cast(null as integer) SvcToPrice, wbdap.WbMaked, wbd.Total, wbd.BasePrice,
   (cast(  cast(( cast(wbd.price as numeric(15,4)) *  wbd.amount) as numeric(15,2))  as numeric(15,2)) * (coalesce ( wbd.NDS,0) /100)  ) SumNds,
   grp.Name GrpName,
   ((wbd.BasePrice * wbd.OnValue) * wbd.Discount /100 ) DiscountPrice,
   wbd.Notes , wbd.UpdateAt,
   cast(wbd.Total * wbd.OnValue as numeric(15,2)) TotalInCurrency,
   mat.MId

  from waybilldet wbd
   join waybilllist wbl on wbl.wbillid = wbd.wbillid
   join materials mat on mat.matid=wbd.matid
   join MatGroup grp on mat.GrpId = grp.GrpId
   join measures msr on msr.mid=mat.mid
   join warehouse wh on wh.wid=wbd.wid
   left outer join currency curr on curr.currid=wbd.currid
   left outer join serials s on s.posid=wbd.posid
   left outer join waybilldetaddprops wbdap on wbdap.posid=wbd.posid
   left outer join countries c on c.cid=mat.cid

  where wbd.wbillid=@wbill_id and 
  ( wbl.wtype <> 6 or( wbl.wtype = 6 and (select count(*) count_turn from WMATTURN where WMATTURN.sourceid = wbd.posid) = 0))
  
  union all
   
  select wbs.num, -wbs.posid, wbl.wbillid, s.svcid, cast(0 as integer), wbs.amount, wbs.price,
    wbs.discount, wbs.nds, wbs.currid, wbl.ondate, cast(0 as integer), 0, cast(s.name as varchar(255)),
    ms.shortname, cast('' as varchar(64)), cast(s.artikul as varchar(255)),
    c.shortname, cast(wbl.onvalue as numeric(15,4)), 1, cast(wbs.norm as numeric(15,8)), cast('' as varchar(255)),
    cast('' as varchar(64)), cast('' as varchar(64)), cast(null as timestamp), 0,
    cast('' as varchar(64)), 0, cast('' as varchar(128)), 0, cast('' as varchar(64)),
    cast(null as numeric(15,8)), wbs.svctoprice, cast(null as integer) ,wbs.total , wbs.baseprice  ,
    (cast(  cast(( cast(wbs.price as numeric(15,4)) *  wbs.amount) as numeric(15,2))  as numeric(15,2)) * (coalesce ( wbs.NDS,0) /100)  ),
	grp.Name GrpName,
	 ((wbs.BasePrice * wbl.OnValue) * wbs.Discount /100 ) DiscountPrice,
	wbs.Notes,null,
	(wbs.Total * wbl.OnValue ) TotalInCurrency,
	s.MId

  from waybillsvc wbs
   join [services] s on s.svcid=wbs.svcid
   join SvcGroup grp on s.GrpId = grp.GrpId
   join measures ms on ms.mid=s.mid
   join waybilllist wbl on wbl.wbillid=wbs.wbillid
   join currency c on c.currid=wbs.currid
  where wbs.wbillid=@wbill_id

  union all
  select wbt.Num, wbt.PosId, wbt.WbillId, wbt.MatId, 0 Wid, wbt.Amount, 0 Price,
   0 Discount, 0 Nds, 0 CurrId, wbl.OnDate, 0 PtypeId, wbt.Checked,
   mat.name as MatName, msr.shortname as MsrName, '' WhName, mat.Artikul, '' CurrName, 
   0 OnValue,
   3 PosType, 
   0 Norm,
   '' Producer, '' Gtd, '' CertNum, null CertDate, mat.Serials, mat.Barcode,
   mat.GrpId, c.name as Country, mat.Archived, '' SerialNo, 0 FullPrice, 
   null SvcToPrice, 
   null WbMaked, 
   0 Total, 
   0 BasePrice,
   0 SumNds,
   mtg.Name GrpName,
   0 DiscountPrice,
   '' Notes,
   null,
   0 TotalInCurrency,
   mat.MId

  from WayBillTmc wbt
   join MATERIALS mat on mat.matid=wbt.matid
   join MATGROUP mtg on mtg.grpid = mat.grpid
   join MEASURES msr on msr.mid=mat.mid
   join waybilllist wbl on wbl.wbillid = wbt.wbillid
   left outer join countries c on c.cid=mat.cid

  where wbt.WbillId=@wbill_id
 
         
         */
    }
}
