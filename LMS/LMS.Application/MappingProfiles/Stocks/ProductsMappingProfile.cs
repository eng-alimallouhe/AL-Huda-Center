using AutoMapper;
using LMS.Application.DTOs.Admin.Dashboard;
using LMS.Application.DTOs.Stock;
using LMS.Domain.Entities.Stock;
using LMS.Domain.Entities.Stock.Products;
using LMS.Domain.Enums.Commons;

namespace LMS.Application.MappingProfiles.Stocks
{
    public class ProductsMappingProfile : Profile
    {
        public ProductsMappingProfile()
        {
            CreateMap<Product, StockInfromationDto>()
            .ForMember(dest => dest.ProductName,
             opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    if (context.Items.TryGetValue("lang", out var langObj) && langObj is int langInt)
                    {
                        var langEnum = (Language)langInt;
                        return src.Translations.FirstOrDefault(t => t.Language == langEnum)?.ProductName ?? "N/A";
                    }
                    return "N/A";
                }));


            CreateMap<InventoryLog, InventoryLogDetailsDto>()
                .ForMember(dest => dest.ProductName, 
                opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    if (context.Items.TryGetValue("lang", out var langObj) &&langObj is int langInt)
                    {
                        var langEnum = (Language)langInt;
                        return src.Product.Translations.FirstOrDefault(t => t.Language == langEnum)?.ProductName ?? "N/A";
                    }
                    return "N/A";
                }));

            CreateMap<Product, StockSnapshotDto>()
                .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    if (context.Items.TryGetValue("lang", out var langObj) && langObj is int langInt)
                    {
                        var langEnum = (Language)langInt;
                        return src.Translations.FirstOrDefault(t => t.Language == langEnum)?.ProductName ?? "N/A";
                    }
                    return "N/A";
                }))
                .ForMember(dest => dest.LogsCount, 
                opt => opt.MapFrom(src => src.Logs.Count()));


            CreateMap<Product, DeadStockDto>()
                .ForMember(dest => dest.ProductName,
                opt => opt.MapFrom((src, dest, destMember, context) =>
                {
                    if (context.Items.TryGetValue("lang", out var langObj) && langObj is int langInt)
                    {
                        var langEnum = (Language)langInt;
                        return src.Translations.FirstOrDefault(t => t.Language == langEnum)?.ProductName ?? "N/A";
                    }
                    return "N/A";
                }));
        }
    }
}
