using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Web.Http;
using proje1.Models;
using proje1.ViewModel;

namespace proje1.Controllers
{
    public class ServisController : ApiController
    {
        DB02Entities db = new DB02Entities();
        SonucModel sonuc = new SonucModel();
        private string uyeId;

        [HttpGet]
        [Route("api/haberliste")]
        public List<HaberModel> HaberListe()
        {
            List<HaberModel> liste = db.haber.Select(x => new HaberModel()
            {
                haberId = x.haberId,
                haberBasligi = x.haberBasligi,
                haberİcerigi = x.haberİcerigi,
                haberTarihi = x.haberTarihi,
                haberResmi = x.haberResmi,
            }).ToList();

            return liste;

        }

        [HttpGet]
        [Route("api/haberbyid/{haberId}")]
        public HaberModel HaberByModel(string haberId)
        {
            HaberModel kayit = db.haber.Where(s => s.haberId == haberId).Select(x => new HaberModel()
            {
                haberId = x.haberId,
                haberBasligi = x.haberBasligi,
                haberİcerigi = x.haberİcerigi,
                haberTarihi = x.haberTarihi,
                haberResmi = x.haberResmi,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/haberekle")]
        public SonucModel HaberEkle(HaberModel model)
        {
            if (db.haber.Count(s => s.haberBasligi == model.haberBasligi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Haber Başlığı Kayıtlıdır!";
                return sonuc;
            }
            haber yeni = new haber();
            yeni.haberBasligi = model.haberBasligi;
            yeni.haberİcerigi = model.haberİcerigi;
            yeni.haberTarihi = model.haberTarihi;
            yeni.haberResmi = model.haberResmi;
            db.haber.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Haber Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/haberduzenle")]
        public SonucModel HaberDuzenle(HaberModel model)

        {
            haber kayit = db.haber.Where(s => s.haberId == model.haberId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.haberBasligi = model.haberBasligi;
            kayit.haberİcerigi = model.haberİcerigi;
            kayit.haberTarihi = model.haberTarihi;
            kayit.haberResmi = model.haberResmi;
            var v = db.SaveChanges(); sonuc.islem = true;
            sonuc.mesaj = "Haber Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/habersil/{haberId}")]
        public SonucModel HaberSil(string haberId)
        {
            haber kayit = db.haber.Where(s => s.haberId == haberId).FirstOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "KayıtBulunamadı!";
                return sonuc;
            }
            db.haber.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Haber Silindi";
            return sonuc;
        }
        [HttpGet]
        [Route("api/uyeliste")]
        public List<UyeModel> UyeListe()
        {
            List<UyeModel> liste = db.uye.Select(x => new UyeModel()
            {
                uyeId = x.uyeId,
                uyeAdiSoyadi = x.uyeAdiSoyadi,
                uyeTelefon = x.uyeTelefon,
                uyeMail = x.uyeMail,
                uyeParola = x.uyeParola,
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/uyebyid/{uyeId}")]
        public UyeModel UyeById(string uyeId)
        {
            UyeModel kayit = db.uye.Where(s => s.uyeId == uyeId).Select(x => new UyeModel()

            {
                uyeId = x.uyeId,
                uyeAdiSoyadi = x.uyeAdiSoyadi,
                uyeTelefon = x.uyeTelefon,
                uyeMail = x.uyeMail,
                uyeParola = x.uyeParola
            }).SingleOrDefault();
            return kayit;
        }
        [HttpPost]
        [Route("api/uyeekle")]
        public SonucModel UyeEkle(UyeModel model)
        {
            if (db.uye.Count(s => s.uyeAdiSoyadi == model.uyeAdiSoyadi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen Üye Adı Soyadı Kayıtlıdır!";
                return sonuc;
            }
            uye yeni = new uye();
            yeni.uyeId = Guid.NewGuid().ToString();
            yeni.uyeAdiSoyadi = model.uyeAdiSoyadi;
            yeni.uyeTelefon = model.uyeTelefon;
            yeni.uyeMail = model.uyeMail;
            yeni.uyeParola = model.uyeParola;
            db.uye.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Eklendi";
            return sonuc;
        }
        [HttpPut]
        [Route("api/uyeduzenle")]
        public SonucModel UyeDuzenle(UyeModel model)
        {
            uye kayit = db.uye.Where(s => s.uyeId == model.uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            kayit.uyeAdiSoyadi = model.uyeAdiSoyadi;
            kayit.uyeTelefon = model.uyeTelefon;
            kayit.uyeMail = model.uyeMail;
            kayit.uyeParola = model.uyeParola;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Düzenlendi";
            return sonuc;
        }
        [HttpDelete]
        [Route("api/uyesil/{uyeId}")]
        public SonucModel UyeSil(string uyeId)
        {
            uye kayit = db.uye.Where(s => s.uyeId == uyeId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Üye Bulunamadı!";
                return sonuc;
            }

            if (db.Kayit.Count(s => s.kayituyeId == uyeId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Haber üzerinde üye olamdığı için üye Silinemez!";
                return sonuc;
            }
            db.uye.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Üye Silindi";
            return sonuc;
        }
    }
}
