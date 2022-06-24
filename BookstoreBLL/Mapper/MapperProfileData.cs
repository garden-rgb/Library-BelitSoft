using AutoMapper;
using BookLibrary.BLL.Models.CustomModels;
using BookLibrary.BLL.Models.CustomModels.OrderModel;
using BookLibrary.DAL.Entities;
using BookLibrary.DAL.Entities.OrderModel;


namespace BookLibrary.BLL.Mapper
{
    public class MapperProfileData : Profile
    {
        public MapperProfileData()
        {
            CreateMap<OrderData, Order>();
            CreateMap<Order, OrderData>();
            CreateMap<BookData, Book>();
            CreateMap<Book, BookData>();
            CreateMap<OrderDetail, OrderDetailData>();
            CreateMap<OrderDetailData, OrderDetail>();
        }
    }
}
