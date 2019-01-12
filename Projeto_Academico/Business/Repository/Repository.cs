using Projeto_Academico.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Projeto_Academico.Business.Repositorys;

namespace Projeto_Academico.Business
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {

        protected Projeto_AcademicoContext Db = new Projeto_AcademicoContext();

        public void Add(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
            Db.SaveChanges();
        }

        public TEntity GetById(int id)
        {
            return Db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().ToList();
        }

        public void Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
        }

        public void Remove(int id)
        {
            Db.Set<TEntity>().Remove(GetById(id));
            Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}