using AutoMapper;
using Library.BLL.Modules.Dto.ResultDto;
using Library.DAL.Models.Books;

namespace Library.BLL.Modules.AutoMapper;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookResultDto>();
    }
}
