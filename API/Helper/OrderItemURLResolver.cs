using API.DTOs;
using AutoMapper;
using Core.Entities.Order;
using Microsoft.Extensions.Configuration;

namespace API.Helper
{
    public class OrderItemURLResolver : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        private readonly IConfiguration _config;
        public OrderItemURLResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ItemOrdered.ImgUrl))
            {
                return _config["ApiUrl"] + source.ItemOrdered.ImgUrl;
            }

            return null;
        }
    }
}
