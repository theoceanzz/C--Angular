using FINAL_INTERN.Business.BaseService;
using FINAL_INTERN.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_INTERN.Business.OrderService
{
    public interface IOrderService : IBaseService<Order>
    {
        Task<IEnumerable<Order>> SearchOrderAsync(string items);
    }
}
