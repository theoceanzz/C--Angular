using FINAL_INTERN.Data.BaseRepository;
using FINAL_INTERN.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_INTERN.Data.OrderDetailRepository
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(finalInternDbContext finalInternDbContext) : base(finalInternDbContext)
        { }
    }
}
