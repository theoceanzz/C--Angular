using FINAL_INTERN.Business.BaseService;
using FINAL_INTERN.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_INTERN.Business.OrderDetailDetailService
{
    public interface IOrderDetailService : IBaseService<OrderDetail>
    {
        Task<IEnumerable<OrderDetail>> SearchOrderDetailAsync(string items);

    }
}
