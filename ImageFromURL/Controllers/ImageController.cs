using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using System.Drawing.Imaging;
using ImageFromURL.Services;

namespace ImageFromURL.Controllers
{
    public class ImageController : Controller
    {
        //private static HttpClient httpClient = new HttpClient();
        private IHttpClientService httpClientService = null;
        private readonly IHostingEnvironment hostingEnvironment;

        string uploadPath = @"\uploads\";
        string filename = "image.jpg";

        public ImageController(IHostingEnvironment env, IHttpClientService clientService)
        {
            hostingEnvironment = env;
            httpClientService = clientService;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string externalUrl)
        {
            if (string.IsNullOrWhiteSpace(externalUrl))
            {
                return View();
            }
            using (Image sourceImage = await LoadImageFromUrl(externalUrl))
            {

                if (sourceImage != null)
                {
                    string path = hostingEnvironment.WebRootPath + uploadPath;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fullPathName = path + filename;
                    sourceImage.Save(fullPathName, ImageFormat.Jpeg);
                }
            }
            return View();
        }

        private async Task<Image> LoadImageFromUrl(string url)
        {
            Image image = null;
            try
            {
                using (HttpResponseMessage response = await httpClientService.Client.GetAsync(url))
                using (Stream inputStream = await response.Content.ReadAsStreamAsync())
                using (Bitmap temp = new Bitmap(inputStream))
                    image = new Bitmap(temp);
            }
            catch
            {
                // Add error logging here
            }
            return image;
        }
    }
}