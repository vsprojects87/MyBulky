using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using MyBulky.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly AppDBContext _db;
		internal DbSet<T> dbSet;

		public Repository(AppDBContext db)
		{
			_db = db;
			this.dbSet = _db.Set<T>();
		}

		public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public void Delete(T entity)
		{
			dbSet.Remove(entity);
		}

		public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(filter);
			return query.FirstOrDefault();
		}

		public IEnumerable<T> GetAll(string? includeProperties = null)
		{
			IQueryable<T> query = dbSet;
			return query.ToList();
		}

		public void RemoveRange(IEnumerable<T> entity)
		{
			dbSet.RemoveRange(entity);
		}
	}
}
