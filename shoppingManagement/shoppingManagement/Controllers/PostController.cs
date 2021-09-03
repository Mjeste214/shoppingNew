using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shoppingManagement.Dto;
using shoppingManagement.ErrorMessage;
using shoppingManagement.Models;

namespace shoppingManagement.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly shoppingContext _db;
        private static Random random = new Random();

        public PostController(shoppingContext context)
        {
            _db = context;
        }

        [HttpPost]
        public ActionResult Addmusteri([FromBody]Musteri musteri)
        {
            ErrorModel error = new ErrorModel();
            var id = 0;

            try
            {
                var response = _db.Musteri.Add(musteri);
                id = _db.SaveChanges();

                Sepet sepet = new Sepet();
                sepet.MusteriId = musteri.Id;

                _db.Sepet.Add(sepet);
                _db.SaveChanges();

                error.message = "Ekleme Yapıldı";
                error.isSuccess = true;
            }
            catch (DbUpdateConcurrencyException expt)
            {
                if (id == 0)
                {
                    error.message = "Ekleme Yapılamadı !";
                    error.isSuccess = false;

                    return NotFound(error);
                }
                else
                {
                    ArgumentException exception = new ArgumentException("Kayıt Başarısız", expt.Message);
                    throw exception;
                }
            }
            return Ok(error);
        }


        //[HttpPost]
        //public ActionResult AddSepet (int musteriId)
        //{

        //    Sepet sepet = _db.Sepet.Select(x => new Sepet()
        //    {
        //      Id = x.Id,
        //      MusteriId = musteriId
        //    }).First();

        //    _db.Sepet.Add(sepet);
        //    _db.SaveChanges();

        //    return Ok("Sepet Eklendi");
        //}


        //[HttpPost]
        //public ActionResult AddSepet(Sepet sepet)
        //{// gerek olmayabilir

        //    //Sepet selectedsepet = _db.Sepet.Select(x => new Sepet()
        //    //{
        //    //    Id = x.Id,
        //    //    MusteriId = x.MusteriId,
        //    //    SepetUrun = sepet.SepetUrun[0],
        //    //}).First();

        //    _db.Sepet.Add(sepet);
        //    _db.SaveChanges();

        //    return Ok("Sepet Eklendi");
        //}


        [HttpPost]
        public ActionResult AddUrun([FromBody]SepetUrun sepeturun,int musteriId)
        {
            try
            {
                Sepet s = new Sepet();
                s.MusteriId = musteriId;
                _db.Sepet.Add(s);
                _db.SaveChanges();

                sepeturun.Id = s.Id;

                _db.SepetUrun.Add(sepeturun);
                _db.SaveChanges();

                


            }
            catch (Exception expt)
            {

                ArgumentException exception = new ArgumentException("Kayıt Başarısız", expt.Message);
                throw exception;
            }


            return Ok("Sepet Eklendi");
        }


        [HttpPost]
        public ActionResult DeleteMusteri(int MusteriId)
        {

            try
            {
                _db.Musteri.Remove(_db.Musteri.FirstOrDefault(x => x.Id == MusteriId));
                _db.SaveChanges();
            }
            catch (Exception exp)
            {
                ArgumentException exception = new ArgumentException("Silme Başarısız", exp.Message);
                throw exception;
            }

            return Ok("Başarılı");
        }


        [HttpPost]
        public ActionResult DeleteUrun(int sepetId)
        {

            try
            {
                _db.SepetUrun.Remove(_db.SepetUrun.FirstOrDefault(x => x.Id == sepetId));
                _db.SaveChanges();
            }
            catch (Exception exp)
            {
                ArgumentException exception = new ArgumentException("Silme Başarısız", exp.Message);
                throw exception;
            }

            return Ok("Başarılı");


        }


        [HttpPost]
        public ActionResult VeriOlustur(int musteriAdet, int sepetAdet)
        {
            List<string> sehirler = new List<string>() { "Ankara", "İstanbul", "İzmir", "Bursa", "Edirne", "Konya", "Antalya", "Diyarbakır", "Van", "Rize" };
            List<Sepet> sepet = new List<Sepet>();
            List<Musteri> selectedmusteri = new List<Musteri>();
            List<SepetUrun> sepetUrun = new List<SepetUrun>();
            List<int> musteriIds =  new List<int>();
            List<int> randomSepet = new List<int>() {1,2,3,4,5};

            int randomSepetSayısı = random.Next(randomSepet.Count);

            for (int i = 0; i < musteriAdet; i++)
            {
                int r = random.Next(sehirler.Count);

                Musteri m = new Musteri();
                m.Ad = RandomString(5);
                m.Soyad = RandomString(5);
                m.Sehir = sehirler[r];

                _db.Musteri.Add(m);
                _db.SaveChanges();

                selectedmusteri.Add(m);
                
            }

            foreach (var item in selectedmusteri)
            {
                musteriIds.Add(item.Id);
            }

            for (int i = 0; i < sepetAdet; i++)
            {
                int index = random.Next(musteriIds.Count);
                int musteriId = musteriIds[index];

                Sepet s = new Sepet();
                s.MusteriId = musteriId;
                             
                _db.Sepet.Add(s);

                int urunAdet = random.Next(1,5);

                for (int u = 0; u < urunAdet; u++)
                {
                    SepetUrun urun = new SepetUrun();
                    urun.Aciklama = RandomString(3);
                    urun.Tutar = random.Next(100, 1000);
                    urun.SepetId = s.Id;

                    _db.SepetUrun.Add(urun);
                }
                
            }

            _db.SaveChanges();
            return BadRequest("df");
        }


        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
