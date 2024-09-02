using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductListPreview.DataAcsess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository categoryRepository { get; }
        IProductRepository ProductRepository { get; }


        void Save();
    }
}
