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
        private readonly IShopRepository<ShoppingCart> _shoprepo;
        private readonly IMapper _mapper;
        private readonly ICartService _cartservice;

        public CartController(IShoppingCartRepository cartRepository,IMapper mapper,ICartService cartService, IShopRepository<ShoppingCart> shopRepository)
        {
            _cartrepo = cartRepository;
            _mapper = mapper;
            _shoprepo = shopRepository;
            _cartservice = cartService;
        }

        [HttpPost]
        [Route("createcart/")]
        public async Task<ActionResult<ShoppingCart>> CreateNewCart()
        {
            var cart = await _cartservice.CreateNewCart();
            return cart;
        }
        [HttpGet]
        [Route("getcartbyid/{id}")]
        public async Task<ActionResult<ShoppingCart>> GetCart(int id)
        {
            await _cartservice.ValidateCart(id);
            return await _cartservice.GetCartById(id);
        }
        [HttpDelete]
        [Route("deletecartbyid/{id}")]
        public async Task<ActionResult<ShoppingCart>> DeleteCart(int id)
        {
            return await _cartrepo.DeleteCartAsync(id);
        }
        [HttpPost]
        [Route("additemtocart")]
        public async Task<ActionResult<ShoppingCart>> AddItem(int productid,int cartid)
        {
            await _cartservice.ValidateCart(cartid);
            
            var cart = await _cartservice.AddItem(cartid, productid);
            return cart;
        }
        //[HttpPut]
        //[Route("updatecart/")]
        //public async Task<ActionResult<ShoppingCart>> UpdateCart(ShoppingCart cart)
        //{
        //    _cartservice.ValidateCart(cart.Id);
        //    return await _shoprepo.UpdateAsync(cart);
        //}
    }
}
