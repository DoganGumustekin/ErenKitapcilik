using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Constans
{
    public static class Messages
    {
        //Book Messages
        public static string BooksAdded = "Kitaplar eklendi";
        public static string BookDeleted = "Kitap silindi";
        public static string BookUpdated = "Kitap güncellendi";
        public static string BookIsAvailable = "Bu Kitap zaten sisteme kayitli tekrar eklenemez";
        public static string BookUnAvailable = "Kitap Bulunamadi! Zaten silinmis";
        public static string BookUnAvailableForUpdate = "Kitap Bulunamadi!";
        public static string BookImageUpdated = "Kitap Fotografi Guncellendi";


        //User-Auth Messages
        public static string UserRegistered = "Kayit Basarili";
        public static string UserNotFound = "Kullanici Bulunamadi";
        public static string PasswordError = "Parola Hatali";
        public static string SuccessfulLogin = "Başarili giris";
        public static string UserAlreadyExists = "Kullanici Mevcut";
        public static string AccessTokenCreated = "Token Olusturuldu";
        public static string AuthorizationError = "Yetkiniz yok";
    }
}
