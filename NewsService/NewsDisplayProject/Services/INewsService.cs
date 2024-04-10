using NewsDisplay.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsDisplay.Services
{
    public interface INewsService
    {
        //הגדרת ממשק לצורך הפרדת תלויות
        Task<List<NewsItem>> GetNewsItemsAsync();

    }
}
