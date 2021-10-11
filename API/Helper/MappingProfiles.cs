using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using Core.Entities;
using Core.Entities.Order;
using AutoMapper;

namespace API.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product,ProductToReturnDTO>()
                .ForMember(d => d.ProductBrand,o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.Unit, o => o.MapFrom(s => s.Unit))
                .ForMember(d => d.ImgUrl, o => o.MapFrom(s => s.ImgPath))
                .ForMember(d => d.ImgUrl, o => o.MapFrom<ProductUrlResolver>());
            CreateMap<Product, ProductToAddDTO>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.Unit, o => o.MapFrom(s => s.Unit))
                .ForMember(d => d.ImgUrl, o => o.MapFrom(s => s.ImgPath));
            CreateMap<Core.Entities.Identity.Adress, AdressDTO>().ReverseMap();

            CreateMap<ShoppingCartDTO, ShoppingCart>();

            CreateMap<ShoppingCartItemDTO, ShoppingCartItem>();

            CreateMap<AdressDTO, Core.Entities.Order.OrderAddress>();

            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(d => d.ShippingMethod, o => o.MapFrom(s => s.Shipping.Name))
                .ForMember(d => d.ShippingMethod, o => o.MapFrom(s => s.Shipping.ShippingPrice));
            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
                .ForMember(d => d.ImgURL, o => o.MapFrom(s => s.ItemOrdered.ImgUrl))
                .ForMember(d => d.ImgURL, o => o.MapFrom<OrderItemURLResolver>());
        }
    }
}
