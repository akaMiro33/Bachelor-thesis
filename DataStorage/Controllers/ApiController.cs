using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspBlog.Data;
using AspBlog.Models;
using AspBlog.Triedy;
using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;


namespace AspBlog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : Controller
    {
        private ILoggerManager logger;
        //  private readonly DatabazovyKontext databazovyKontext;
        private readonly IFileProvider fileProvider;
        private readonly ApplicationDbContext databaseContext;
      

        public ApiController(IFileProvider fileProvider,
                           ApplicationDbContext database, //,
                           ILoggerManager logger  //  DatabazovyKontext databazovyKontext
                           )
        {
            this.fileProvider = fileProvider;
            this.databaseContext = database;
            this.logger = logger;

            //  this.databazovyKontext = databazovyKontext;
        }


        [HttpPost("Searching")]
        public IActionResult Searching(List<string> listOfMetaData)
        {
            
            int index = 0; // splitted premenna aby kluc a hodnota boli na spravnom mieste
            string[] keys = new string[listOfMetaData.Count];
            string[] values = new string[listOfMetaData.Count];
            foreach (var tag in listOfMetaData)
            {
                string[] splitted = tag.Split("=");// rozdelim kluc a hodnotu do stringov
                keys[index] = splitted[0];// kluc
                values[index] = splitted[1];// hodnota
                index++;
            }
            List<Guid> UuidNajdenychPrac = new List<Guid>();
         
            List<Guid> UuidNajdenychTagov = (from t in this.databaseContext.PracaTagy
                                    where (keys.Contains(t.Key) &&
                                          values.Contains(t.Value))
                                    select t.TaskUuid).ToList();
            List<PocetUuid> pocetVyskytovUuid = new List<PocetUuid>();

         
            foreach (var uuid in UuidNajdenychTagov)                
            {
                bool jeTam = false;
                foreach (var pocetVyskytov in pocetVyskytovUuid)
               {

                    if(uuid == pocetVyskytov.guid)
                    {
                        jeTam = true;
                        pocetVyskytov.pocet++;
                        if (pocetVyskytov.pocet == listOfMetaData.Count())
                        {
                            UuidNajdenychPrac.Add(pocetVyskytov.guid);
                        }
                    }
               }

                if (jeTam==false)
                {
                    PocetUuid novy = new PocetUuid(uuid);
                    novy.pocet++;
                    pocetVyskytovUuid.Add(novy);
                    if ( listOfMetaData.Count() == 1)
                    {
                        UuidNajdenychPrac.Add(novy.guid);
                    }
                }
             
            }


     

            return Json(UuidNajdenychPrac);

        }
        /*   [HttpPost("UkazPracu")]
           public IActionResult UkazPracu()
           {


               Praca praca = new Praca();
               string n = "bla";
               return Json(praca);
           }*/


      //  [Authorize]
        [HttpPost("GetNameOfAddedFiles")]
        public IActionResult GetNameOfAddedFiles(Guid tasksUuid)
        {
            List<string> zoznamSuborov = new List<string>();

          //  try { 
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo("subory\\" + tasksUuid.ToString() + "\\Extra");
            var count = dir.GetFiles();
            //string n = "bla";
            foreach (var nazov in count)
            {
                zoznamSuborov.Add(nazov.Name);
            }
          //  Praca praca = new Praca();
         //   string n = "bla";
            //zoznam
            return Json(zoznamSuborov);
       // }catch (Exception ex)
         //   {
               // _logger.LogError($"Something went wrong: {ex}");
               // return StatusCode(500, "Internal server error");
          //  }
    }



        [HttpPost("GetAddedFileByName")]
        public async Task<IActionResult> GetAddedFileByName(Guid tasksUuid, string nameOfFile)
        {
            string path ="";
            // DirectoryInfo zlozka = Directory.CreateDirectory(@cesta);
            //ZipFile.CreateFromDirectory("subory\\" + uuid.ToString() + "\\Extra", "subory\\" + uuid.ToString() + "\\Extra.zip");
            if (Directory.Exists("subory\\" + tasksUuid.ToString()))
            {
                //ZipFile.CreateFromDirectory("subory\\" + uuid.ToString() + "\\Extra", @cesta + "\\Extra.zip");


                foreach (var cesta in Directory.EnumerateFiles("subory\\" + tasksUuid.ToString() + "\\Extra",nameOfFile.Trim()+"*", SearchOption.AllDirectories))
                {
                    path = cesta;
                 
                    //string[] nazovSuboru = file.Split("\\");
                    //string nazovSuboru = file.Split("\\");
                    //System.IO.File.Copy(file, zlozka.FullName + "\\" + nazovSuboru.Last());
                    break;
                }

            

            // var path = Path.Combine(
            //            Directory.GetCurrentDirectory(),
            //            "subory", uuid.ToString() + "\\Extra.zip");


            //   FileInfo f = new FileInfo(path);
            //     f.Delete();
            //mazanie Zip suboru
             ContentType contentType = new ContentType();

               var memory = new MemoryStream();
               using (var stream = new FileStream(path, FileMode.Open))
                 {
                     await stream.CopyToAsync(memory);
               }
               memory.Position = 0;
                 return File(memory, contentType.GetContentType(path), Path.GetFileName(path));
            //return Json("Uspesne");

            }
            //ZipArchive zipArchive = new ZipArchive()
            else   // treba pridať test ci uz ci nazov suboru v danej zlozke neexistuje
            {
                return StatusCode(500);
            }





        }
        [HttpPost("GetDocumentationOfTaskByUuid")]
        public async Task<IActionResult> GetDocumentationOfTaskByUuid(Guid tasksUuid)
        {
            // DirectoryInfo zlozka = Directory.CreateDirectory("subory\\" + uuid.ToString() + "\\Dokumentacia");
            if (Directory.Exists("subory\\" + tasksUuid.ToString()))
            {
                //DirectoryInfo zlozka = Directory.CreateDirectory(@cesta + "\\Dokumentacia");
          

            DataService extrahovanieDokumentacie = new DataService(databaseContext);
           // extrahovanieDokumentacie.najdiDokumentaciuPrace(uuid, zlozka);



           // ZipFile.CreateFromDirectory(zlozka.FullName, zlozka.FullName + ".zip");

            //FileInfo i = new FileInfo(zlozka.FullName);
            //    i.Delete();
         /*   foreach (FileInfo f in zlozka.GetFiles())
            {

                f.Delete();
            }
            foreach (DirectoryInfo dir in zlozka.GetDirectories())
            {

                dir.Delete(true);
            }
            zlozka.Delete(true);*/

            //var path = Path.Combine(
             //  Directory.GetCurrentDirectory(),
             //  "subory", uuid.ToString() + "\\Dokumentacia.zip");


             var path = extrahovanieDokumentacie.najdiDokumentaciuPrace(tasksUuid); 
             if(path == "")
                {
                   return StatusCode(200, "Neobsahuje žiadnu dokumentaciu");
                }
                ContentType contentType = new ContentType();

             var memory = new MemoryStream();
             using (var stream = new FileStream(path, FileMode.Open))
              {
                  await stream.CopyToAsync(memory);
              }
              memory.Position = 0;
               return File(memory, contentType.GetContentType(path), Path.GetFileName(path));
           // return Json("Uspesne");

            }
            else  // treba pridať test ci uz ci nazov suboru v danej zlozke neexistuje
            {
                return StatusCode(500, "Neexistujuca praca alebo cesta");
            }


        }
        //vyriešiť
        [HttpPost("MultiDownloadOfTasks")]
        public async Task<IActionResult> MultiDownloadOfTasks( List<Guid> uuidList)
        {
            DataService hromadneStahovanieUloh = new DataService(databaseContext);
            if (hromadneStahovanieUloh.vsetkyPraceExistuju(uuidList))
            { 
                DirectoryInfo zlozka = Directory.CreateDirectory("subory" + "\\Ulohy");
        
        //podla uuideciek , nie key values
        //  foreach (var u in UuidNajdenychPrac)
        //  {
        //string[] klucAHodnota = item.Split("=");
        //   }

        hromadneStahovanieUloh.presunPracePodlaUuid(zlozka, uuidList);

            ZipFile.CreateFromDirectory(zlozka.FullName, zlozka.FullName + ".zip");


            foreach (FileInfo f in zlozka.GetFiles())
            {

                f.Delete();
            }
            foreach (DirectoryInfo dir in zlozka.GetDirectories())
            {

                dir.Delete(true);
            }
            zlozka.Delete(true);
                // Guid uuid = Guid.NewGuid();
                //return Ok(uuid);
                //return Json(Download(uuid));

                var path = zlozka.FullName + ".zip";

                ContentType contentType = new ContentType();

                var memory = new MemoryStream();
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                FileInfo mazanie = new FileInfo(path);
                mazanie.Delete();
                return File(memory, contentType.GetContentType(path), Path.GetFileName(path));


                //return Json("Uspesne");
        } else
            {
                return Json(StatusCode(500, "Minimalne jedno Uuid je neplatne alebo je zla cesta."));
            }
    }
        [HttpPost("AddFileToTheTask")]
        public IActionResult AddFileToTheTask(Guid tasksUuid, IFormFile file, string nameOfFile
            //byte[] prilohaA
            )
        {
                 

            string[] nazovAFormatSuboru;
            nazovAFormatSuboru = file.FileName.Split(".");
            System.IO.File.Copy(file.FileName, @"C:\Users\work\Desktop\AspBlogTesty\AspBlog\subory\" + tasksUuid.ToString() + "\\Extra\\" + nameOfFile + "." + nazovAFormatSuboru.Last());

            return Json(nameOfFile);
        }

        [HttpPost("StatisticsOfTask")]
        [Produces("application/json")]
        public IActionResult StatisticsOfTask(Guid tasksUuid)
        {
            
         
            Statistics nacitavanieZoSuboru = new Statistics();
            nacitavanieZoSuboru.spusti("subory\\" + tasksUuid.ToString() + "\\Kod");

            //return Json(JsonConvert.SerializeObject(nacitavanieZoSuboru));
            return Json(nacitavanieZoSuboru);
        }


        [HttpPost("Download")]
        public async Task<IActionResult> Download(Guid tasksUuid)
        {
            if (tasksUuid.ToString() == null)
                return Content("Práca neexistuje.");


            //    DirectoryInfo zlozka = Directory.CreateDirectory(@cesta + "\\" + uuid.ToString());

            //     try
            //    {
            if (Directory.Exists("subory\\" + tasksUuid.ToString()))
            {

             //   ZipFile.CreateFromDirectory("subory\\" + uuid.ToString(), @cesta + "\\" + uuid.ToString() + ".zip");
           
            
        //      }catch (Exception ex)
       //    {
            
       //        return StatusCode(500, "Neexistujuca Praca");
        //    }
            string nazovPrace = (from t in this.databaseContext.Tasks
                                 where (tasksUuid == t.Uuid)
                                 select t.NameOfTask).First().ToString();
                ZipFile.CreateFromDirectory("subory\\" + tasksUuid.ToString(), "subory\\" + nazovPrace + ".zip");
            var path = Path.Combine(
                         Directory.GetCurrentDirectory(),
                          "subory", nazovPrace + ".zip");


               //FileInfo f = new FileInfo(path);
                // f.Delete();
            //mazanie Zip suboru

            ContentType contentType = new ContentType();


              var memory = new MemoryStream();
              using (var stream = new FileStream(path, FileMode.Open))
             {
                 await stream.CopyToAsync(memory);
              }
             memory.Position = 0;

             FileInfo f = new FileInfo(path);
             f.Delete();

                return  File(memory, contentType.GetContentType(path), Path.GetFileName(path));
            return Json("Uspesne");
            }
            else  // treba pridať test ci uz ci nazov suboru v danej zlozke neexistuje
            {
                return StatusCode(500, "Neexistujuca Praca");
            }
        }


        //  [HttpPost("Stiahnutie")]
        //  public async Task<IActionResult> Stiahnutie(Guid uuid)
        // {


        //  }



        [HttpPost("AddFile")]
        public async Task<IActionResult> AddFile(IFormFile file, List<string> listOfMetaData)
        {
            Guid uuid = Guid.NewGuid();
            Models.Task praca = new Models.Task();
            praca.Uuid = uuid;
            praca.DateOfInsertion = DateTime.Now;
            

            int pom = 0; // splitted premenna aby kluc a hodnota boli na spravnom mieste
           
            string[] nazovAFormatSuboru;
            nazovAFormatSuboru = file.FileName.Split(".");
            //string nazovSuboru;
            praca.NameOfTask = nazovAFormatSuboru[0].Split("\\").Last();
            if (nazovAFormatSuboru.Last() != "zip") {
                return StatusCode(500, "Subor nebol v zip formate");
            }
        /*    DirectoryInfo zlozka = Directory.CreateDirectory("subory\\" + uuid.ToString());
            DirectoryInfo zlozkaKod = Directory.CreateDirectory("subory\\" + uuid.ToString() + "\\Kod");
            DirectoryInfo zlozkaExtra = Directory.CreateDirectory("subory\\" + uuid.ToString() + "\\Extra");
            //nazovAFormatSuboru = file.FileName.Split(".");
            string zipAdresa = "subory\\" + uuid.ToString() + "\\Kod";
            string adresa = "subory\\" + uuid.ToString() + "." + nazovAFormatSuboru.Last();
           
        //    using (var stream = new FileStream(adresa, FileMode.Create))
        //    {
         //       await file.CopyToAsync(stream);
        //    }
            ZipFile.ExtractToDirectory(file.FileName, zipAdresa);*/
           // ZipFile.CreateFromDirectory("subory\\" + uuid.ToString(), "subory\\"+ uuid.ToString()+ ".zip");
            //ZipFile.ExtractToDirectory(adresa, zipAdresa);
           // tu vlozit statistiky z praci
        /*    foreach (FileInfo f in zlozka.GetFiles())
            {
                
                f.Delete();
            }
            foreach (DirectoryInfo dir in zlozka.GetDirectories())
            {

                dir.Delete(true);
            }
            zlozka.Delete(true);*/

            List <TaskMetaData> PracaTags = new List<TaskMetaData>();

            foreach (var item in listOfMetaData)
            {
                TaskMetaData tag = new TaskMetaData();
                if (item == null)
                    continue;
                //if (item.Contains('=')) { 
                string[] pomocna = item.Split("=");// rozdelim kluc a hodnotu do stringov
               
                
                tag.Key = pomocna[0];
                tag.Value = pomocna[1];
                //}
                tag.task = praca;
                tag.TaskUuid = uuid;
               // tag.uuidPrace = uuid;
               // tag.PracaId = uuid.ToString;
                PracaTags.Add(tag);
                pom++;

            }


            foreach (var tag in PracaTags)
            {
               praca.Task_MetaData.Add(tag);
            }


            databaseContext.Tasks.Add(praca);
            await databaseContext.SaveChangesAsync();
            // databaza.Prace.Remove(praca);
            // await databaza.SaveChangesAsync();

            DirectoryInfo zlozka = Directory.CreateDirectory("subory\\" + uuid.ToString());
            DirectoryInfo zlozkaKod = Directory.CreateDirectory("subory\\" + uuid.ToString() + "\\Kod");
            DirectoryInfo zlozkaExtra = Directory.CreateDirectory("subory\\" + uuid.ToString() + "\\Extra");
            //nazovAFormatSuboru = file.FileName.Split(".");
            string zipAdresa = "subory\\" + uuid.ToString() + "\\Kod";
            string adresa = "subory\\" + uuid.ToString() + "." + nazovAFormatSuboru.Last();

            //    using (var stream = new FileStream(adresa, FileMode.Create))
            //    {
            //       await file.CopyToAsync(stream);
            //    }
            ZipFile.ExtractToDirectory(file.FileName, zipAdresa);

            return Json(uuid);
        }

        [HttpPost("GetInformationAboutTaskByUuid")]
       public IActionResult GetInformationAboutTaskByUuid(Guid tasksUuid
          //byte[] prilohaA
          )
        {

            List<string> zoznamSuborov = new List<string>();

            //  try { 
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo("subory\\" + tasksUuid.ToString() + "\\Extra");
            var count = dir.GetFiles();
            //string n = "bla";
            foreach (var nazov in count)
            {
                zoznamSuborov.Add(nazov.Name);
            }


            string nazovPrace = (from t in this.databaseContext.Tasks
                                 where (tasksUuid == t.Uuid)
                                 select t.NameOfTask).First().ToString();

            var datumVlozenia = (from t in this.databaseContext.Tasks
                                 where (tasksUuid == t.Uuid)
                                 select t.DateOfInsertion).First();


            InformationAboutTask informacie = new InformationAboutTask(zoznamSuborov, nazovPrace, datumVlozenia);
            return Json(informacie);
        }

        //[Authorize]
         [HttpPost("GenerovanieApiKeys")]
         public async Task<IActionResult> GenerovanieApiKeysAsync(string paPoznamka)
         {
             string kluc = GeneratingApiKey.generuj();
             DateTime dat = (DateTime.Now).AddYears(1);

             ApiKey apiKey = new ApiKey();
             apiKey.Key = kluc;
             apiKey.Note = paPoznamka;
             apiKey.ExpirationDate = dat;

             databaseContext.ApiKeys.Add(apiKey);
             await databaseContext.SaveChangesAsync();

             return Json(apiKey);
         }


    }
}