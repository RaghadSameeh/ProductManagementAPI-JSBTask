using DataAccessLayer.Data;
using DataAccessLayer.Models;
using DataAccessLayer.Reposatries.GenericReposatry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Reposatries.ProductReposatry
{
    public class ProductRepo : GenericReposatry <product>, IProduct
    {
        private readonly Context context;

        public ProductRepo(Context context) : base (context)
        {
            this.context = context;

        }


    }
}
