using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Spesifications;
using Microsoft.AspNetCore.Mvc;
using API.DTOs;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/carts/")]
    public class CartController : BaseController
    {
        private readonly IShoppingCartRepository _cartrepo;
        private readonly IMapper _mapper;
        //private readonly ICartService _cartservice;

        public CartController(IShoppingCartRepository cartRepository,IMapper mapper)
        {
            _cartrepo = cartRepository;
            _mapper = mapper;
            //_cartservice = cartservice;
        }

        [HttpPost]
        [Route("createcart/")]
        public async Task<ActionResult<ShoppingCart>> CreateOrGetCart(string cartsid)
        {
            var cart = await _cartrepo.GetCartAsync(cartsid);
            return cart;
        }
    }
}
