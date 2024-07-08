using DataAccessLayer.Models;
using DataAccessLayer.Reposatries.GenericReposatry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Reposatries.OrderReposatry
{
    public interface IOrder : IGenericReposatry<order>
    {
    }
}
