using Abp.VNext.Hello.Dtos;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Abp.VNext.Hello
{
    public interface ICityAppService :
        ICrudAppService< //Defines CRUD methods
            CityDto, //Used to show books
            int, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting on getting a list of books
            CityDto, //Used to create a new book
            CityDto> //Used to update a book
    {
  
    }
}