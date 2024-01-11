using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sozilan.Data.DataContext;
using Sozilan.Data.Model;

namespace Sozilan.Core.BaseService
{
    public abstract class ServiceBase<TEntity> where TEntity : BaseClass
    {
        protected ManagementSystemContext context;
        protected DbSet<TEntity> dbset;
        protected ServicePoint services;
        protected MessageService result;

        public ServiceBase()
        {
            context = new ManagementSystemContext();
            services = new ServicePoint();
            result = new MessageService();
            dbset = context.Set<TEntity>();
        }

        // tek bir değer döndürülmek istenirse bu kullanılacak
        public TEntity FindByID(int id)
        {
            var y=dbset.Where(x => x.ID == id && x.IsActive).FirstOrDefault();
            return y;
        }

        public ICollection<TEntity> ToList()
        {
            return dbset.Where(x => x.IsActive).ToList();
        }

        public MessageService Delete(int id)
        {
            TEntity entity = FindByID(id);

            if (entity == null) 
            {
                result.ResultID = 0;

                if (result.IsSuccess == false)
                {
                    result.Message = "Silme işlemi gerçekleştirilemedi..";
                    return result;
                }
            }
            // silmeden hallediyruz.
            entity.IsActive = false;
            result.ResultID = entity.ID;

            result.Message = "Başarıyla Silindi..";
            
            context.SaveChanges();
            return result;
        }

        // abstract methodlar override edilcek daha sonra sınıfların servislerinde
        public abstract MessageService Insert(TEntity dto);

        public abstract MessageService Update(TEntity dto);

        public string InsertBase(TEntity dto)
        {
            string mes;

            dto.IsActive = true;
            dbset.Add(dto);
         
            context.SaveChanges();
            mes = "Kayıt başarılı";
            return mes;
        }
        public string UpdateBase(TEntity dto)
        {
            string mes;

            if (dto.ID > 0)
            {
                context.SaveChanges();
            }
            mes = "Güncellendi";
            return mes;
        }
    }
}
