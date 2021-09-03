using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shoppingManagement.Dto;
using shoppingManagement.Models;

namespace shoppingManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]


    public class GetController : ControllerBase
    {

        private readonly shoppingContext _db;
        
        public GetController(shoppingContext context)
        {

            _db = context;
        }

        [HttpGet]
        public IActionResult GetMusteri()
        {
            using (shoppingContext db = new shoppingContext())
            {

                var result = (from mstri in db.Musteri
                              join spt in db.Sepet
                              on mstri.Id equals spt.MusteriId into temp
                              from spt in temp.DefaultIfEmpty()
                              orderby mstri.Id descending
                              select new
                              {
                                  mstri.Id,
                                  mstri.Sehir,
                                  mstri.Ad,
                                  mstri.Soyad,
                                  spt.SepetUrun
                              }).ToList();


                if (result.Count == 0)
                {
                    return NotFound("Müşteri Bulunamadı");
                }


                return Ok(result);
            }
        }


        [HttpGet]
        public IActionResult GetMusteriById(int musteriId)
        {
            using (shoppingContext db = new shoppingContext())
            {

                var result = (from mstri in db.Musteri
                              join spt in db.Sepet
                              on mstri.Id equals musteriId
                              select new
                              {
                                  mstri.Id,
                                  mstri.Sehir,
                                  mstri.Ad,
                                  mstri.Soyad,
                                  spt.SepetUrun
                              }).FirstOrDefault();


                if (result == null)
                {
                    return NotFound("Müşteri Bulunamadı");
                }


                return Ok(result);
            }
        }


        [HttpGet]
        public IActionResult GetSepetListesi(int musteriId)
        {

            using (shoppingContext db = new shoppingContext())
            {

                var result = _db.Sepet.Where(x => x.MusteriId == musteriId).ToList();


                if (result.Count == 0)
                {
                    return NotFound("Sepet Bulunamadı");
                }


                return Ok(result);
            }


        }

        [HttpGet]
        public IActionResult GetUrunler(int musteriId)
        {

            using (shoppingContext db = new shoppingContext())
            {

                var result = (from mstri in db.Musteri
                              join spt in db.Sepet
                              on musteriId equals spt.Id
                              select new
                              {
                                  spt.SepetUrun
                              }).FirstOrDefault();


                if (result == null)
                {
                    return NotFound("Ürün Bulunamadı");
                }

                return Ok(result);
            }

        }


        //[HttpGet]
        //public ActionResult<List<DtoSehirAnaliz>> SehirBazliAnalizYap()
        //{
        //    List<DtoSehirAnaliz> analizdata = new List<DtoSehirAnaliz>();

        //    using (shoppingContext db = new shoppingContext())
        //    {

        //        var result = (from mstri in db.Musteri
        //                      join spt in db.Sepet
        //                      on mstri.Id equals spt.MusteriId into temp
        //                      from spt in temp.DefaultIfEmpty()
        //                      join urun in db.SepetUrun
        //                      on spt.MusteriId equals urun.SepetId
        //                      orderby mstri.Id descending
        //                      select new
        //                      {
        //                          mstri.Id,
        //                          mstri.Sehir,
        //                          mstri.Ad,
        //                          mstri.Soyad,
        //                          spt.SepetUrun,
        //                          urun.Tutar

        //                      }).ToList();


        //        if (result.Count == 0)
        //        {
        //            return NotFound("Müşteri Bulunamadı");
        //        }


        //        foreach (var item in result)
        //        {
        //            var tutar = 0;

        //            foreach (var z in item.SepetUrun)
        //            {
        //                tutar += decimal.ToInt32(z.Tutar);

        //                analizdata.Add(new DtoSehirAnaliz()
        //                {
        //                    SehirAdi = item.Sehir,
        //                    ToplamTutar = tutar,
        //                    SepetAdet = z.Sepet != null ? z.Sepet.SepetUrun.Count() : 0
        //                });
        //            }
        //        }
        //        return (analizdata);
        //    }
        //}
    }
}