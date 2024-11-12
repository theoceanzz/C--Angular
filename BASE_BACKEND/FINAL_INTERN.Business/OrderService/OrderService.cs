using FINAL_INTERN.Business.BaseService;
using FINAL_INTERN.Data.BaseRepository;
using FINAL_INTERN.Data.OrderRepository;
using FINAL_INTERN.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_INTERN.Business.OrderService
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IBaseRepository<Order> baseService,IOrderRepository orderRepository ) : base(baseService)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IEnumerable<Order>> SearchOrderAsync(string items)
        {
            return await _orderRepository.SearchAsync(x => x.NameOfCustomer.Contains(items) || x.Dates.Equals(items) || x.Email.Contains(items));
        }
    }
}
