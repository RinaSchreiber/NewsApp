using System.Net;
using Microsoft.Extensions.Caching.Memory;
using System.Xml.Linq;
using NewsDisplay.Models;
using NewsDisplay.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class NewsService : INewsService
{
    private const string RssCacheKey = "RssFeed";
    private readonly IMemoryCache _memoryCache;
    private readonly string _rssFeedUrl;
    //נעילת המטמון מאפשרות גישה של כמה נתונים בו זמנית
    private readonly SemaphoreSlim _cacheLock = new SemaphoreSlim(1, 1);

    public NewsService(IMemoryCache memoryCache, IConfiguration configuration)
    {
        _memoryCache = memoryCache;
        //הצבת הלינק של אתר החדשות בבנאי
        _rssFeedUrl = configuration["NewsSettings:RssFeedUrl"];
    }

    public async Task<List<NewsItem>> GetNewsItemsAsync()
    {
        //נעילת המטמון
        await _cacheLock.WaitAsync();

        try
        {
            if (!_memoryCache.TryGetValue(RssCacheKey, out XDocument cachedRssFeed))
            {
                var rssUrl = _rssFeedUrl;

                using (var webClient = new WebClient())
                {
                    var rssContent = await webClient.DownloadStringTaskAsync(rssUrl);
                    cachedRssFeed = XDocument.Parse(rssContent);
                    //לדעתי מתאים שהמטמון יתעדכן כל 30 דקות
                    _memoryCache.Set(RssCacheKey, cachedRssFeed, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpiration = DateTime.Now.AddMinutes(30)
                    });
                }
            }
            //הצבת הערכים באוביקט שלי והכנסת האוביקטים לרשימה
            return cachedRssFeed.Descendants("item")
                .Select(item => new NewsItem
                {
                    Title = item.Element("title")?.Value,
                    Description = item.Element("description")?.Value,
                    Link = item.Element("link")?.Value
                })
                .ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetNewsItemsAsync: {ex.Message}");
            return new List<NewsItem>();
        }
        finally
        {
            //שחרור המטמון
            _cacheLock.Release();
        }
    }
}
