/* 
 * Descarga wallpapers de la primer página de resultados encontrados en wallhaven según el
 * término de búsqueda pasado como argumento al programa.
 * 
 * Uso: dotnet run -- <nombre-carpeta-resultados> <término-de-búsqueda>
 * 
 * Ejemplo de término de búsqueda para buscar en wallhaven:
 * 
 * "birds animals nature -illustration -humor -illusion -memes -table -bars -flowers -dress -minimalism"
 * 
 * Busca wallpapers según los tags "birds", "animals" y "nature" y excluye los que empiezan con "-"
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace WallhavenScraper
{
    internal class Wallscraper
    {
        private static string _resultsFolderName;
        private static string _searchTerm;

        static async Task Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("usage: dotnet run -- <results-folder-name> <search-term>");

                return;
            }

            _resultsFolderName = args[0];
            _searchTerm = args[1];

            using var client = new HttpClient();

            await DownloadWallpapersAsync(client);
        }

        static async Task DownloadWallpapersAsync(HttpClient client)
        {
            string queryString = $"q={_searchTerm}&categories=110&purity=100&sorting=favorites&order=desc";
            await using Stream jsonStream = await client.GetStreamAsync(
                $"https://wallhaven.cc/api/v1/search?{queryString}"
            );

            var resultPage = await JsonSerializer.DeserializeAsync<SearchResult>(jsonStream);

            if (resultPage.meta.total == 0)
            {
                Console.WriteLine("No results were found");

                return;
            }

            string resultsFolderPath = $"wallpapers/{_resultsFolderName}";
            string fullSizeFolderPath = resultsFolderPath + "/fullsize";
            string thumbnailFolderPath = resultsFolderPath + "/preview";

            Console.WriteLine($"\nCreating folder to save images in: {resultsFolderPath}");
            Directory.CreateDirectory(fullSizeFolderPath);
            Directory.CreateDirectory(thumbnailFolderPath);

            for (int i = 0; i < resultPage.data.Count; i++)
            {
                string wallpaperUrl = resultPage.data[i].url;
                string fileType = resultPage.data[i].file_type.Replace("image/", ".");

                Console.WriteLine($"Downloading wallpaper and thumbnail in: {wallpaperUrl}");

                var response = await client.GetAsync(resultPage.data[i].path);
                var content = await response.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync($"{fullSizeFolderPath}/{i}{fileType}", content);

                response = await client.GetAsync(resultPage.data[i].thumbs.original);
                content = await response.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync($"{thumbnailFolderPath}/{i}{fileType}", content);
            }
        }

        public class Thumbs
        {
            public string large { get; set; }
            public string original { get; set; }
            public string small { get; set; }
        }

        public class Datum
        {
            public string id { get; set; }
            public string url { get; set; }
            public string short_url { get; set; }
            public int views { get; set; }
            public int favorites { get; set; }
            public string source { get; set; }
            public string purity { get; set; }
            public string category { get; set; }
            public int dimension_x { get; set; }
            public int dimension_y { get; set; }
            public string resolution { get; set; }
            public string ratio { get; set; }
            public int file_size { get; set; }
            public string file_type { get; set; }
            public string created_at { get; set; }
            public IList<string> colors { get; set; }
            public string path { get; set; }
            public Thumbs thumbs { get; set; }
        }

        public class Meta
        {
            public int current_page { get; set; }
            public int last_page { get; set; }
            public int per_page { get; set; }
            public int total { get; set; }
            public string query { get; set; }
            public object seed { get; set; }
        }

        public class SearchResult
        {
            public IList<Datum> data { get; set; }
            public Meta meta { get; set; }
        }
    }
}
