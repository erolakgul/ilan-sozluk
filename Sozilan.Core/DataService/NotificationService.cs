using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozilan.Core.BaseService;
using Sozilan.Data.Model;

namespace Sozilan.Core.DataService
{
    public class NotificationService : ServiceBase<Notification>
    {
        public override MessageService Insert(Notification dto)
        {
            if (dbset.Any(x => x.IPAddress == dto.IPAddress && x.UserName == dto.UserName && x.BildirilenID == dto.BildirilenID && x.EntryID == dto.EntryID)) 
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

        public override MessageService Update(Notification dto)
        {
            if (dbset.Any(x => x.BildirilenID == dto.BildirilenID)) 
            {
                var list = context.Notification.Where(x => x.BildirilenID == dto.BildirilenID).ToList();

                foreach (var item in list)
                {
                    item.IsActive = false;
                }

                result.ResultID = dto.BildirilenID;
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
        public ICollection<Notification> GetList(int bildirilenID)
        {
            var list = context.Notification.Where(x => x.BildirilenID == bildirilenID ).ToList();
            return list;
        }
    }
}
