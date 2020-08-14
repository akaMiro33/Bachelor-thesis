using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspBlog.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.IO.Compression;
using Microsoft.Extensions.FileProviders;
using AspBlog.Data;
using System.Threading;
using AspBlog.Triedy;
using Microsoft.EntityFrameworkCore;

namespace AspBlog.Controllers
{
    public class HomeController : Controller
    {

        // private readonly DatabazovyKontext databazovyKontext;
          private readonly IFileProvider fileProvider;      
          private readonly ApplicationDbContext databaseContext;
          private int aktualneIdApi;  

          public HomeController(IFileProvider fileProvider,
                               ApplicationDbContext databaza
              )
          {
              this.fileProvider = fileProvider;
              this.databaseContext = databaza;
              aktualneIdApi = 0;
          }

        [HttpPost]
        public async Task<IActionResult> GenerateApiKey(ApiKey model)
        {
          //  string kluc = GenerovanieApiKey.generuj();
           // DateTime dat = (DateTime.Now).AddYears(1);
            
            ApiKey apiKey = new ApiKey();
            apiKey.Key = GeneratingApiKey.generuj();
            apiKey.Note = model.Note;
            apiKey.ExpirationDate = model.ExpirationDate;

            databaseContext.ApiKeys.Add(apiKey);
            aktualneIdApi = apiKey.Id;
            await databaseContext.SaveChangesAsync();

             return RedirectToAction(nameof(HomeController.NewApiKey), "Home"); ;
        }

        public IActionResult GetApiKeys()
        {
            var entities = databaseContext.ApiKeys;

            return View(entities.ToList());
            
            //return Json(apiKey);
        }

        public IActionResult NewApiKey()
        {
            var AktualnyKluc = new ApiKey();
            //foreach (var apiKey in databaza.ApiKeys.Last)
            //{
          //      if (apiKey.Id == aktualneIdApi)
           //     {
                    AktualnyKluc = databaseContext.ApiKeys.Last();
           //     }
       //     }
               // var entities = databaza.ApiKeys;

            return View(AktualnyKluc);

            //return Json(apiKey);
        }

        public IActionResult GenerateApiKey()
        {
           // string vygenerovanyKluc = GenerovanieApiKey.generuj();
            //ViewBag.VygenerovanyKluc = vygenerovanyKluc;
             return View();
        }

        /*   [HttpPost]
         //  [HttpPost("UploadFiles")]
           public async Task<IActionResult> UkladanieSuborov(List<IFormFile> files, List<string> keyValues)
           {
               long size = files.Sum(f => f.Length);

               // full path to file in temp location
               //var filePath = Path.GetTempFileName();

               foreach (var formFile in files)
               {
                   var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                  formFile.FileName);
                   if (formFile.Length > 0)
                   {
                       using (var stream = new FileStream(filePath, FileMode.Create))
                       {
                           await formFile.CopyToAsync(stream);
                       }
                   }
                   return Ok(new { count = files.Count, size, filePath });
               }

               // process uploaded files
               // Don't rely on or trust the FileName property without validation.

               return Ok(new { count = files.Count, size });
           }








           //GET api/values



           [HttpPost]
           public JsonResult Vyhladavanie(List<string> keyValues)
           {
               int pom = 0; // pomocna premenna aby kluc a hodnota boli na spravnom mieste
               string[] kluce = new string[keyValues.Count];
               string[] hodnoty = new string[keyValues.Count];
               foreach (var item in keyValues)
               {
                   string[] pomocna = item.Split("=");// rozdelim kluc a hodnotu do stringov
                   kluce[pom] = pomocna[0];// kluc
                   hodnoty[pom] = pomocna[1];// hodnota
                   pom++;
               }


             //  foreach (var item in databazovyKontext.Prace)
              // {

               //}


               Guid uuid = Guid.NewGuid();
               return Json(uuid);
           }

           public JsonResult UkazPracu(Guid uuid)
           {
               Praca praca = new Praca();
               string n = "bla";
               return Json(praca);
           }

           public JsonResult Rozbaliť(IFormFile file)
           {

               string n = "bla";
               return Json(file);
           }

           public JsonResult ExtrahovanieDokumentacie(Guid uuid)
           {

               string n = "bla";
               //vrati IFormFile
               return Json(n);
           }
           //vyriešiť


           public JsonResult HromadneStahovanieUloh(List<string> keyValues)
           {
               foreach (var item in keyValues)
               {
                   string[] klucAHodnota = item.Split("=");
               }

               Guid uuid = Guid.NewGuid();
               return Json(Download(uuid.ToString()));
           }


           public JsonResult PrilozitSuborKZadaniu(
               //Guid uuid, byte[] priloha
               )
           {
               using (ZipArchive archive = ZipFile.Open(@"C:\Users\work\Desktop\pozadi_sede.zip", ZipArchiveMode.Update))
               {
                   archive.CreateEntryFromFile(@"C:\Users\work\Desktop\subor.txt","Vitaztvo.txt");
               }

                   /* using (FileStream zipToOpen = new FileStream(@"C:\Users\work\Desktop\pozadi_sede.zip", FileMode.Open))
                    {
                        using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                        {

                            ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.txt");

                            using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                            {
                                writer.WriteLine("Information about this package.");
                                writer.WriteLine("===============================");
                            }
                        }
                    }
                   return Json(0)
                   //Json(Ok(uuid))
                   ;
           }
           [HttpPost]
           public JsonResult StatistikyKPraci(Guid uuid)
           {
               PrilozitSuborKZadaniu();
               var fileCount = (from file in Directory.EnumerateFiles(@"C:\Users\work\Desktop\HUdba", "*.mp3", SearchOption.AllDirectories)
                                select file).Count();
               System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo("subory");
               int count = dir.GetFiles().Length;
               string n = "bla";
               PocetRiadkovVSuboroch(@"C:\Users\work\Desktop\AspBlogNaPloche");
               return Json(n);
           }


           private int PocetRiadkovVSuboroch(string dirPath)
           {
               FileInfo[] csFiles = new DirectoryInfo(dirPath.Trim())
                                           .GetFiles("*.js", SearchOption.AllDirectories);

               int totalNumberOfLines = 0;
               Parallel.ForEach(csFiles, fo =>
               {
                   Interlocked.Add(ref totalNumberOfLines, CountNumberOfLine(fo));
               });
               return totalNumberOfLines;
           }

           private int CountNumberOfLine(Object tc)
           {
               FileInfo fo = (FileInfo)tc;
               int count = 0;
               int inComment = 0;
               using (StreamReader sr = fo.OpenText())
               {
                   string line;
                   while ((line = sr.ReadLine()) != null)
                   {
                      // if (IsRealCode(line.Trim(), ref inComment))
                           count++;
                   }
               }
               return count;
           }
           */
        private bool IsRealCode(string trimmed, ref int inComment)
        {
            if (trimmed.StartsWith("/*") && trimmed.EndsWith("*/"))
                return false;
            else if (trimmed.StartsWith("/*"))
            {
                inComment++;
                return false;
            }
            else if (trimmed.EndsWith("*/"))
            {
                inComment--;
                return false;
            }

            return
                   inComment == 0
                && !trimmed.StartsWith("//")
                && (trimmed.StartsWith("if")
                    || trimmed.StartsWith("else if")
                    || trimmed.StartsWith("using (")
                    || trimmed.StartsWith("else  if")
                    || trimmed.Contains(";")
                    || trimmed.StartsWith("public") //method signature
                    || trimmed.StartsWith("private") //method signature
                    || trimmed.StartsWith("protected") //method signature
                    );
        }


        
        public IActionResult Index()
        {
            string n = "bla";
            return View();
        }
        
        public IActionResult Vyhladavanie()
        {
            string n = "bla";
            return View();
        }

        public IActionResult StatistikyKPraci()
        {
            return View();
        }


        
        public IActionResult NahravanieSuborov()
        {
            return View();
        }


        public IActionResult SuboryNaStahovanie()
        {
            var entities = databaseContext.Tasks;

            return View(entities.ToList());
        }




        public async Task<IActionResult> Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            //var path = Path.Combine(
             //              Directory.GetCurrentDirectory(),
              //             "subory", filename);



            ZipFile.CreateFromDirectory("subory\\" + filename, "subory\\" + filename + ".zip");
            var path = Path.Combine(
                         Directory.GetCurrentDirectory(),
                          "subory", filename + ".zip");


            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            FileInfo f = new FileInfo(path);
            f.Delete();

            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".zip", "subory/zip"},
                {".rar", "subory/rar"}
            };
        }

        [HttpPost]
        public async Task<IActionResult> NahravanieSuborov(IFormFile files, List<string> keyValues)
        {
            Guid uuid = Guid.NewGuid();
            Models.Task praca = new Models.Task();
            praca.Uuid = uuid;

            int pom = 0; // pomocna premenna aby kluc a hodnota boli na spravnom mieste
            string[] kluce = new string[keyValues.Count];
            string[] hodnoty = new string[keyValues.Count];
            string[] nazovAFormatSuboru;
            //nazovAFormatSuboru = files.FileName.Split("\\");
            nazovAFormatSuboru = files.FileName.Split(".");


            //  long size = files.Sum(f => f.Length);"Subor.zip"
            //using (var stream = new FileStream("subory\\" + nazovAFormatSuboru.Last(), FileMode.Create))
            using (var stream = new FileStream("subory\\" + uuid.ToString() + "." + nazovAFormatSuboru.Last(), FileMode.Create))

            {
                await files.CopyToAsync(stream);
            }

            foreach (var item in keyValues)
            {
                string[] pomocna = item.Split("=");// rozdelim kluc a hodnotu do stringov
                kluce[pom] = pomocna[0];// kluc
                hodnoty[pom] = pomocna[1];// hodnota
                pom++;
            }

          //  praca.keyValue1 = hodnoty[0];
          //  praca.keyValue2 = hodnoty[1];

            databaseContext.Tasks.Add(praca);
            await databaseContext.SaveChangesAsync();
            databaseContext.Tasks.Remove(praca);
            await databaseContext.SaveChangesAsync();

            return Ok(uuid);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var APIkeyPomocne = await databaseContext.ApiKeys
                .FirstOrDefaultAsync(m => m.Id == id);
            if (APIkeyPomocne == null)
            {
                return NotFound();
            }

            return View(APIkeyPomocne);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var article = await databaseContext.ApiKeys.FindAsync(id);
            databaseContext.ApiKeys.Remove(article);
            await databaseContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Pomocná metoda ověřující existenci článku
        private bool ArticleExists(int id)
        {
            return databaseContext.ApiKeys.Any(e => e.Id == id);
        }

    }
}
