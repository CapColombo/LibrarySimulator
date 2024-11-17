using AutoMapper;
using Library.DAL.Dto.QueryCommandResult;
using Library.DAL.Models.Books;

namespace Library.BLL.Modules.Books.AutoMapper;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookResultDto>();
    }
}
