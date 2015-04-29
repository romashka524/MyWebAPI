using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using WebApi.Models;

namespace WebApi.Controllers.api
{
    public class OrdersController : ApiController
    {
        private static readonly OrderRepository Repository = new OrderRepository(HttpContext.Current.Server.MapPath("~/App_Data/orders.json"));

        [HttpGet]
        public IEnumerable<Order> GetAll()
        {
            return Repository.OrderList;
        }

        [HttpPost]
        public void Delete(int id)
        {
            if (!Repository.DeleteOrder(id))
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        [HttpPost]
        public Order Add(Order order)
        {
            return Repository.AddOrder(order);
        }

    }
}
