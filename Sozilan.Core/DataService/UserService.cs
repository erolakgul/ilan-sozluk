using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozilan.Core.BaseService;
using Sozilan.Data.Model;

namespace Sozilan.Core.DataService
{
    public class UserService : ServiceBase<User>
    {
        public override MessageService Insert(User dto)
        {
            if (context.User.Any(x => x.Email == dto.Email)) 
            {
                result.ResultID = 0;
                if (result.IsSuccess == false)
                {
                    result.Message = "Kayıt başarısız ..";
                    return result;
                }
            }

            bool isExist = (dto == null) ? false : true;

            if (isExist)
            {
                dto.IsActive = false;  // mail doğrulaması bekleyeceğiz
                dbset.Add(dto);
                context.SaveChanges();

                result.ResultID = dto.ID;
                result.Message = "Kayıt başarılı..";
            }
            return result;
        }

        public override MessageService Update(User dto)
        {
            if (dbset.Any(x => x.ID == dto.ID))
            {
                User user = FindByID(dto.ID);
                user.Password = dto.Password;  // kullanıcının sadece şifresi değiştirilebilir

                result.ResultID = user.ID;
                if (result.IsSuccess == true)
                {
                    result.Message = "Güncelleme başarılı..";
                    context.SaveChanges();
                    return result;
                }
            }
            result.Message = "Güncelleme başarısız..";
            return result;
        }
        public int GetID(string username)
        {
            int id = context.User.Where(x => x.UserName == username && x.IsActive).Select(y => y.ID).SingleOrDefault();
            return id;
        }
        public string GetName(int id)
        {
            string isim =context.User.Where(x=> x.ID==id).Select(c=> c.UserName).FirstOrDefault();
            return isim;
        }
        public string GetGUID(string email)
        {
            string id = context.User.Where(x => x.Email == email).Select(y => y.UserGUID).SingleOrDefault();
            return id;
        }

        public bool ConfirmGuid(string guid)
        {  // doğrulama linkine tıklama sonrası bu havuza gelir method
            int id = context.User.Where(x => x.UserGUID == guid).Select(y => y.ID).SingleOrDefault();

            if (id == 0 ) 
            {
                return false;
            }
            User user = context.User.Where(x => x.ID == id).FirstOrDefault();
            user.IsActive = true;
            context.SaveChanges();

            return true;
        }

        public string[] ParolaUnuttum(string str)
        {
            var kullanici = context.User.Where(x => x.UserName == str || x.Email == str).Select(y => y.UserName).SingleOrDefault();

            if (kullanici == null) 
            {
                return null;
            }
            var sifre = context.User.Where(x => x.UserName == str || x.Email == str).Select(y => y.Password).SingleOrDefault();
            var email = context.User.Where(x => x.UserName == str || x.Email == str).Select(y => y.Email).SingleOrDefault();

            string[] dizi = new string[3];
            dizi[0] = kullanici;
            dizi[1] = sifre;
            dizi[2] = email;

            return dizi;
        }
    }
}
