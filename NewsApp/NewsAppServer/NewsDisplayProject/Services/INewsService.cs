using NewsDisplay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsDisplay.Services
{
    public interface INewsService
    {
        //Defining an interface for separating dependencies
        Task<List<NewsItem>> GetNewsItemsAsync();

    }
}
