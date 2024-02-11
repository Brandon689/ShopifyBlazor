namespace Shopify.Services
{
    internal class ParallelDownloader
    {
        public void Run(List<string> imageUrls, string outputDirectory)
        {
            Directory.CreateDirectory(outputDirectory);

            Parallel.ForEach(imageUrls, imageUrl =>
            {
                DownloadImage(imageUrl, outputDirectory);
            });

            Console.WriteLine("Download completed.");
        }

        static void DownloadImage(string imageUrl, string outputDirectory)
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] imageBytes = client.GetByteArrayAsync(imageUrl).Result;
                string fileName = Path.Combine(outputDirectory, GetImageName(imageUrl));
                File.WriteAllBytes(fileName, imageBytes);
            }
        }

        static string GetImageName(string imageUrl)
        {
            Uri uri = new Uri(imageUrl);
            string[] segments = uri.Segments;
            return segments[segments.Length - 1].TrimEnd('/');
        }
    }
}
