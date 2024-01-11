using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozilan.Core.BaseService;
using Sozilan.Data.Model;

namespace Sozilan.Core.DataService
{
    public class TitleService : ServiceBase<Title>
    {
        public override MessageService Insert(Title dto)
        {
            if (context.Title.Any(x => x.TitleLink == dto.TitleLink))
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

        public override MessageService Update(Title dto)
        {
            if (dbset.Any(x => x.ID == dto.ID))
            {
                Title title = FindByID(dto.ID);
                title.Content = dto.Content;
                title.UpdateDate = dto.UpdateDate;

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
        public IList<Title> GetTitle(string str)
        {
            // js ile gönderilen veri alınıp ayrıştırılıyor
            string[] gonderilenVeri = str.Split('-');

            string tag = gonderilenVeri[0];
            string ara = gonderilenVeri[1];

            var q2 = dbset.Where(x => x.Content.Contains(ara)).Where(y => y.TitleTag == tag).ToList();

            return q2;
        }

        public int GetID(string str)
        {
            int id = context.Title.Where(x => x.Content == str).Select(y => y.ID).SingleOrDefault();

            return id;
        }
        public int GetID(int entryID)
        {
            int id = context.Title.Join(context.Enter, _title => _title.ID, _enter => _enter.TitleID, (_title, _enter) => new { _title, _enter }).Where(a => a._title.ID == a._enter.TitleID && a._enter.ID == entryID).Select(x => x._title.ID).SingleOrDefault();
            return id;
        }

        public int LastID()
        {
            int sonId = context.Title.Where(x => x.IsActive).OrderByDescending(x => x.ID).Select(x => x.ID).Take(1).FirstOrDefault();
            return sonId;
        }

        public IList<Title> RandomTitle(int[] dizi)
        {
            var query = context.Title.Where(x => x.IsActive && dizi.Contains(x.ID)).ToList(); //Linq to Entity

            //var query = (from e in context.Title
            //             where dizi.Contains(e.ID)
            //             select e
            //            ).ToList();   //Linq to SQL

            return query;
        }
    }
}
