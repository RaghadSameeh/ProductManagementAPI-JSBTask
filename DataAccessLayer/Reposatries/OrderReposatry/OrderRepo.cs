using DataAccessLayer.Data;
using DataAccessLayer.Models;
using DataAccessLayer.Reposatries.GenericReposatry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Reposatries.OrderReposatry
{
    public class OrderRepo : GenericReposatry<order> , IOrder
    {
        private readonly Context context;

        public OrderRepo(Context context) : base(context)
        { 
            this.context = context;
        }

    }
}
