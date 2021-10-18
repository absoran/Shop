using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Extensions;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Entities.Order;
using Core.Interfaces;
using Core.Spesifications;

namespace API.Controllers //ToDo
{
    [Route("api/orders")]
    public class OrdersController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderservice;
        private readonly IShopRepository<Order> _orderrepo;
        private readonly IShopRepository<Core.Entities.Identity.User> _userrepo;
        public OrdersController(IOrderService orderservice,IMapper mapper, IShopRepository<Order> orderrepository, IShopRepository<Core.Entities.Identity.User> userrepository)
        {
            _orderservice = orderservice;
            _mapper = mapper;
            _orderrepo = orderrepository;
            _userrepo = userrepository;
        }
        [HttpPost]
        [Route("createorder/")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderdto,string username)
        {
            var user = await _userrepo.GetByNameAsync(username);
            var addressToShip = _mapper.Map<AdressDTO, OrderAddress>(orderdto.ShipToAddress);
            var order = await _orderservice.CreateOrderAsync(user.Email, orderdto.DeliveryMethodId, Convert.ToInt32(orderdto.ShoppingCartID), addressToShip);
            if (order == null)
            {
                return BadRequest(new APIResponse(400, "Error at creating order"));
            }
            return Ok(order);
        }
    }
}
