using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozilan.Core.BaseService;
using Sozilan.Data.Model;

namespace Sozilan.Core.DataService
{
    public class MesajService : ServiceBase<Message>
    {

        public override MessageService Insert(Message dto)
        {
            if (context.Message.Any(x => x.ID == dto.ID)) 
            {
                result.ResultID = 0;
                if (result.IsSuccess == false)
                {
                    result.Message = "Kayıt başarısız ...";
                    return result;
                }
            }
            bool isExist = (dto == null) ? false : true;
            if (isExist)
            {
                result.ResultID = dto.ID;
                result.Message = "Kayıt işlemi başarı ile gerçekleştirildi..";

                dto.IsActive = true;
                dbset.Add(dto);
                context.SaveChanges();
            }
            return result;
        }

        public override MessageService Update(Message dto)
        {
            if (dbset.Any(x => x.ID == dto.ID)) 
            {
                Message msg = FindByID(dto.ID);
                msg.IsActive = false;

                result.ResultID = dto.ID;
                if (result.IsSuccess == true)
                {
                    context.SaveChanges();
                    result.Message = "Güncelleme işlemi başarılı..";
                    return result;
                }
                result.Message = "Güncelleme işlemi başarısız ..";
            }
            return result;
        }

        public ICollection<Message> GetMessage(string str)
        {
            var a = context.Message.Where(c => c.MesajGUID == str).OrderByDescending(z => z.SendDate).ToList();
            return a;
        }

        public ICollection<Message> GetMessage(int id)
        {
            // userid ye ait tüm mesajlar listelenir// mesajlarpartial ı besler
            var s = context.Message.Where(x => x.ReceivingID == id && x.IsActive).ToList();
            return s;
        }
        public ICollection<Message> GetMessages(int id)
        {
            // userid ye ait tüm mesajlar listelenir// mesajlarpartial ı besler
            var s = context.Message.Where(x => x.UserID == id || x.ReceivingID == id).ToList();
            return s;
        }


        public ICollection<Message> GetMessage(int id,int id2)
        {
            var mesajguid = context.Message.Where(x => x.UserID == id && x.ReceivingID == id2 || x.UserID == id2 && x.ReceivingID == id).OrderByDescending(y => y.SendDate).Select(c=> c.MesajGUID).FirstOrDefault();

            var list = context.Message.Where(x => x.MesajGUID == mesajguid).ToList();
            return list;
        }

        public string KayıtVarMi(int id, int id2)
        {
            var k = context.Message.Where(x => x.UserID == id && x.ReceivingID == id2 || x.UserID == id2 && x.ReceivingID == id).Select(y => y.MesajGUID).FirstOrDefault();

            if (k == "" || k == null) 
            {
                return "boş";
            }
            return k;

        }

        public void Okundu(string username)
        {
            // tüm mesajların okundu bilgisi mesajlar index sayfası açılınca okunmuş olarak işaretlenir
            var id = context.User.Where(x => x.UserName == username).Select(c => c.ID).FirstOrDefault();

            var s = context.Message.Where(x => x.ReceivingID == id).ToList();

            foreach (var item in s)
            {
                item.IsActive = false;
            }
            context.SaveChanges();
        }

    }
}
