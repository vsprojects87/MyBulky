using Bulky.DataAccess.Repository.IRepository;
using MyBulky.Data;
using MyBulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{

		private AppDBContext _db;

		// we are getting dbContext and passing it to base class
		public CategoryRepository(AppDBContext db) : base(db)
		{
			_db = db;
		}


		public void Update(Category obj)
		{
			_db.Categories.Update(obj);
		}
	}
}
