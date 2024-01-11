using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozilan.Core.BaseService;
using Sozilan.Data.Model;

namespace Sozilan.Core.DataService
{
    public class EntryService : ServiceBase<Entry>
    {
        public override MessageService Insert(Entry dto)
        {
            if (context.Enter.Any(x => x.ID == dto.ID))
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

        public override MessageService Update(Entry dto)
        {
            if (dbset.Any(x => x.ID == dto.ID)) 
            {
                Entry ent = FindByID(dto.ID);
                ent.Content = dto.Content;
                ent.EntryUpdateDate = dto.EntryUpdateDate;
                ent.isUpdated = true;

                Title tit = context.Title.Where(x => x.ID == ent.Title.ID).FirstOrDefault();
                tit.UpdateDate = ent.EntryUpdateDate;

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
        public void UpdateRating(Entry dto)
        {
            if (dbset.Any(x => x.ID == dto.ID))
            {
                Entry ent = FindByID(dto.ID);
                ent.UpCount = dto.UpCount;
                ent.DownCount = dto.DownCount;

                result.ResultID = dto.ID;
                if (result.IsSuccess == true)
                {
                    context.SaveChanges();
                    result.Message = "Güncelleme işlemi başarılı..";
                }
                result.Message = "Güncelleme işlemi başarısız ..";
            }
        }
        public ICollection<Entry> GetEntry(int id)
        {
            return dbset.Where(x => x.ID == id && x.IsActive).ToList();
        }
        public ICollection<Entry> GetEntryList(int titleID)
        {
            var q = context.Enter.Where(x => x.TitleID == titleID && x.IsActive).ToList();
            return q;
        }
        public int? GetTitleID(int entryID)
        {
            int? id = context.Enter.Where(x => x.ID == entryID).Select(c => c.TitleID).FirstOrDefault();
            return id;
        }
        public int? GetUser(int entryID)
        {
            int? id = context.Enter.Where(x => x.ID == entryID).Select(c => c.UserID).FirstOrDefault();
            return id;
        }
        public string GetUserName(int entryID)
        {
            int? nameID = context.Enter.Where(x => x.ID == entryID).Select(c => c.UserID).FirstOrDefault();
            string name = context.User.Where(x => x.ID == nameID).Select(x => x.UserName).FirstOrDefault();
            return name;
        }
    }
}
