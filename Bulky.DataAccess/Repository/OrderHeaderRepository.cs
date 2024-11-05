using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db) : base(db) //this is to pass the db context to base call (i.e. Repository<Category>)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

		void IOrderHeaderRepository.UpdateStatus(int id, string orderStatus, string? paymentStatus)
		{
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.OrderId == id);
            if(orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
				if (!string.IsNullOrEmpty(paymentStatus))
				{
					orderFromDb.PaymentStatus = paymentStatus;
				}
			}
		}

        void UpdatePaymentId(int id, string sessionId, string paymentIntentId)
        {
			var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.OrderId == id);
			if (!string.IsNullOrEmpty(sessionId))
            {
                //orderFromDb.SessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                orderFromDb.PaymentIntentId = paymentIntentId;
                orderFromDb.PaymentDate = DateTime.Now;
            }
			}
    }
}
