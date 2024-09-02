using ProductListPreview.DataAcsess.Data;
using ProductListPreview.DataAcsess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductListPreview.DataAcsess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICategoryRepository categoryRepository { get; private set; }

        public IProductRepository ProductRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            categoryRepository = new CategoryRepository(_db);
            ProductRepository = new ProductRepository(_db);

        }
        public void Save()
        {
             _db.SaveChanges();

        }
    }
}
