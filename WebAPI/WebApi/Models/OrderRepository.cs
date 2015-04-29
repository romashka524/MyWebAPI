using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using WebApi.Controllers.api;

namespace WebApi.Models
{
    public class OrderRepository
    {
        private ObservableCollection<Order> _orderList = new ObservableCollection<Order>();
        private int _id = 1;
        private readonly string _filePath;

        public ObservableCollection<Order> OrderList
        {
            get { return _orderList; }
        }

        public OrderRepository(string filePath)
        {
            _filePath = filePath;
            LoadData();
            OrderList.CollectionChanged += (sender, args) =>
            {
                var output = JsonConvert.SerializeObject(_orderList);
                using (var json = new StreamWriter(_filePath, false))
                {
                    json.Write(output);
                }
            };
        }

        public void LoadData()
        {
            using (var r = new StreamReader(_filePath))
            {
                string json = r.ReadToEnd();
                _orderList = JsonConvert.DeserializeObject<ObservableCollection<Order>>(json);
            }
        }


        public Order AddOrder(Order order)
        {
            order.Id = _id++;
            _orderList.Add(order);
            return order;
        }

        public bool DeleteOrder(int id)
        {
            var orderToDelete = _orderList.First(order => order.Id == id);
            return orderToDelete != null && _orderList.Remove(orderToDelete);
        }


    }
}