using AutoMapper;
using BookLibrary.BLL.Models.CustomModels;
using BookLibrary.BLL.Models.CustomModels.OrderModel;
using BookLibrary.Web.Models.CustomModels.ViewModel;

namespace BookLibrary.Web.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<OrderData, OrderViewModel>();
            CreateMap<OrderViewModel, OrderData>();
            CreateMap<OrderDetailData, OrderDetailViewModel>();
            CreateMap<BookData, BookViewModel>();
            CreateMap<BookViewModel, BookData>();
        }
    }
}
