using Microsoft.Playwright;
using ShopifyPlaywrightSitemapScraping;
using System.Text.Json;

namespace Shopify.Services
{
    public class PlaywrightController
    {
        //private readonly HttpClient _httpClient;
        private IPlaywright playwright;
        private IBrowser browser;
        private IPage page;
        private string baseUrl;
        private string baseName;
        private ParallelDownloader download = new();

        public PlaywrightController()
        {
            //_httpClient = new HttpClient();
        }

        public async Task Init(string _baseUrl)
        {
            baseUrl = _baseUrl;
            baseName = deriveName();
            playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new() { Headless = true });
            page = await browser.NewPageAsync();
            await page.RouteAsync("**/*.js", route => route.AbortAsync());
        }

        public async Task Dispose()
        {
            await browser.CloseAsync();
            playwright.Dispose();
            await browser.DisposeAsync();
        }

        public async ValueTask<string[]> GetSitemapLinks()
        {
            await page.GotoAsync($"{baseUrl}/sitemap.xml");
            var m = await page.QuerySelectorAllAsync("sitemapindex sitemap");
            string[] x = new string[m.Count];
            for (int i = 0; i < m.Count; ++i)
            {
                x[i] = (await m[i].TextContentAsync()).Trim().Replace("\n", "");
            }
            return x;
        }

        public async ValueTask<string[]> GetProductLinks(string collection)
        {
            await page.GotoAsync(collection);
            var m = await page.QuerySelectorAllAsync("urlset url loc");
            var textContents = await Task.WhenAll(m.Select(async element => await element.TextContentAsync()));
            return textContents.Where(x => !x.StartsWith("https://cdn.shopify.com")).Skip(1).ToArray();
        }

        public async ValueTask<string> GetProductJSON(string uri)
        {
            if (uri.EndsWith(".json") == false)
                uri += ".json";
            await page.GotoAsync(uri);
            return await page.EvalOnSelectorAsync<string>("pre", "element => element.textContent");
        }

        public async ValueTask<List<Product>> Download(int page, int perPage = 10)
        {
            List<Product> k = new List<Product>();
            var dir = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{baseName}\\");
            Directory.CreateDirectory(dir);
            var sitemap = await this.GetSitemapLinks();
            var productSitemap = await this.GetProductLinks(sitemap[0]);
            for (var i = 0; i < 20; i++)
            {
                string productJson = await this.GetProductJSON(productSitemap[i]);
                Root m = JsonSerializer.Deserialize<Root>(productJson);
                k.Add(m.Product);
            }
            return k;
        }

        public async Task DownloadToFS()
        {
            var dir = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\{baseName}\\");
            Directory.CreateDirectory(dir);
            var sitemap = await this.GetSitemapLinks();
            var productSitemap = await this.GetProductLinks(sitemap[0]);
            for (var i = 0; i < productSitemap.Length; i++)
            {
                string productJson = await this.GetProductJSON(productSitemap[i]);
                File.WriteAllText($"{dir}{productSitemap[i].Replace($"{baseUrl}/products/", "")}.json", productJson);
            }
        }

        public List<Product> RetrieveProductData()
        {
            List<Product> k = new List<Product>();
            var m = Directory.GetFiles($"../../../{baseName}/");
            foreach (var item in m)
            {
                string content = File.ReadAllText(item);
                Root rootList = JsonSerializer.Deserialize<Root>(content);

                k.Add(rootList.Product);
            }
            return k;
        }

        public void DownloadImages(List<Product> products)
        {
            string dir = $"../../../{baseName}IMG/";
            Directory.CreateDirectory(dir);
            List<string> list = new List<string>();
            foreach (var item in products)
            {
                foreach (var it in item.Images)
                {
                    list.Add(it.Src);
                }
            }
            download.Run(list, dir);
        }

        private string deriveName()
        {
            return baseUrl
                .Replace("https://", "")
                .Replace("www.", "")
                .Replace(".com", "");
        }
    }
}
