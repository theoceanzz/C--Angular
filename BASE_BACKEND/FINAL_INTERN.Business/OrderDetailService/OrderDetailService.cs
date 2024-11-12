using FINAL_INTERN.Business.BaseService;
using FINAL_INTERN.Data.BaseRepository;
using FINAL_INTERN.Data.OrderDetailRepository;
using FINAL_INTERN.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_INTERN.Business.OrderDetailDetailService
{
    public class OrderDetailService: BaseService<OrderDetail>, IOrderDetailService
    {
        private readonly IOrderDetailRepository _OrderDetailRepository;

        public OrderDetailService(IBaseRepository<OrderDetail> baseRepository,IOrderDetailRepository OrderDetailRepository): base(baseRepository)
        {
            _OrderDetailRepository = OrderDetailRepository;
        }

        public async Task<IEnumerable<OrderDetail>> SearchOrderDetailAsync(string items)
        {
            return await _OrderDetailRepository.SearchAsync(x => x.NameOfCar.Contains(items));
        }
    }
}
