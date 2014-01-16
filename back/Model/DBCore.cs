using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using NDatabase;
using NDatabase.Api;

namespace Model
{
    public class BusinessBase<T> where T : class
    {
        private static string DbName
        {
            get
            {
                var dir = AppDomain.CurrentDomain.RelativeSearchPath;
                return Path.Combine(dir, ConfigurationManager.AppSettings["DbName"]);
            }
        }

        public virtual OID Add(T entity)
        {
            using (var odb = OdbFactory.Open(DbName))
            {
                var id = odb.Store(entity);
                return id;
            }
        }

        public virtual IList<T> QueryAll()
        {
            using (var odb = OdbFactory.Open(DbName))
            {
                //OdbFactory.Delete(DbName);
                return odb.QueryAndExecute<T>().ToList();
            }
        }

        public virtual void Add(IList<T> list)
        {
            using (var odb = OdbFactory.Open(DbName))
            {
                try
                {
                    list.ToList().ForEach(x => odb.Store(x));
                    odb.Commit();
                }
                catch (Exception e)
                {
                    odb.Rollback();
                    throw new Exception(e.Message, e);
                }
            }
        }

        public virtual void DelLists(Expression<Func<T, bool>> func)
        {
            using (var odb = OdbFactory.Open(DbName))
            {
                var entitys = odb.AsQueryable<T>().Where(func).ToList();
                entitys.ForEach(x => odb.Delete(x));
            }
        }

        public virtual void DelEntity(Expression<Func<T, bool>> func)
        {
            using (var odb = OdbFactory.Open(DbName))
            {
                var entity = odb.AsQueryable<T>().Where(func).FirstOrDefault();
                if (entity != null)
                {
                    odb.Delete(entity);
                }
            }
        }

        //public virtual void DelEntityByEntity(T entity)
        //{
        //    using (var odb = OdbFactory.Open(DbName))
        //    {
        //        odb.Delete(entity);
        //    }
        //}

        public virtual IEnumerable<T> Query(Expression<Func<T, bool>> func)
        {
            using (var odb = OdbFactory.Open(DbName))
            {
                //odb.Query<T>().Execute<T>(false, 1, 4).ToList().AsQueryable();
                return odb.AsQueryable<T>().Where(func).ToList();
            }
        }

        //public virtual IQueryable<T> Query(Expression<Func<T, bool>> func)
        //{
        //    using (var odb = OdbFactory.Open(DbName))
        //    {
        //        return odb.AsQueryable<T>().Where(func).ToList().AsQueryable();
        //    }
        //}

        public virtual QueryResponse<T> Query(Paging<T> paging)
        {
            using (var odb = OdbFactory.Open(DbName))
            {
                //odb.Query<T>().Descend("").Constrain("");
                var iQuery = odb.Query<T>();//.Execute<T>(true, paging.FromIndex - 1, paging.ToIndex).AsQueryable();
                if (paging.Where != null)
                {
                    //query = query.Where(paging.Where);
                }

                var allCount = iQuery.Count();
                var r = allCount % paging.PageSize;

                var query = iQuery.Execute<T>(true, paging.FromIndex - 1, paging.ToIndex).AsQueryable();

                var pageCount = r == 0 ? allCount / paging.PageSize : allCount / paging.PageSize + 1;
                return new QueryResponse<T> { page = paging.PageIndex, records = paging.PageSize, rows = query.ToList(), total = allCount, Pages = pageCount };
            }
        }
    }
}
