using API_WebApplication.DTO.HocPhiModel;
using API_WebApplication.DTO.HocPhis;
using API_WebApplication.DTO.NhatKy;
using API_WebApplication.DTO.ThoiKhoaBieus;
using API_WebApplication.DTO.XinNghiPheps;
using API_WebApplication.Models;
using AutoMapper;

namespace API_WebApplication
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<OTKBDTO, OTKB>();
            CreateMap<TKB1DTO, TKB1>();
            CreateMap<HocPhiDTO, HocPhi>();
            CreateMap<ChiTietHocPhiDTO, ChiTietHocPhi>();
            CreateMap<ChiTietHocPhiTheoMonthDTO, ChiTietHocPhiTheoMonth>();
            CreateMap<XinNghiPhepDTO, XinNghiPhepModel>();
            CreateMap<ChiTietXinNghiPhepDTO, ChiTietXinNghiPhep>();
            CreateMap<NhatKyDTO,NhatKy>();
            CreateMap<TableImageDTO, TableImage>();
            CreateMap<TableLikeDTO, TableLike>();
            CreateMap<BinhLuanDTO, BinhLuan>();
            CreateMap<HocPhiModel2DTO, HocPhiModel2>();
            CreateMap<HocPhiChiTietModel2DTO, HocPhiChiTietModel2>();
        }
    }
}
