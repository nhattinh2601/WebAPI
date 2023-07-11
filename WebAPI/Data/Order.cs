﻿namespace WebAPI.Data
{

    public enum order_status
    {
        New = 0, Payment = 1, Complete = 2, Cancel = -1
    }

    public class Order
    {
        public Guid order_id { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime? shippingDate { get; set; }
        public order_status status { get; set; }
        public string user { get; set; }
        public string address { get; set; }
        public string phone { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}