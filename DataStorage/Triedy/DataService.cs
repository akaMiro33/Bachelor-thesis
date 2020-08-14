using AspBlog.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspBlog.Triedy
{
    public class DataService
    {
        private readonly ApplicationDbContext databaza;
        public DataService(ApplicationDbContext databaza)
        {
            this.databaza = databaza;
        }

        public void spusti()
        {
           
        }

        public string najdiDokumentaciuPrace(Guid uuid)
        {

            if (najdiAPresunDokumentaciuPodlaPripony(uuid,  ".pdf")!="")
            {
                return najdiAPresunDokumentaciuPodlaPripony(uuid,  ".pdf");
            }

            if (najdiAPresunDokumentaciuPodlaPripony(uuid,  ".doc") != "")
            {
                return najdiAPresunDokumentaciuPodlaPripony(uuid,  ".doc");
            }

            if (najdiAPresunDokumentaciuPodlaPripony(uuid,  ".docx") != "")
            {
                return najdiAPresunDokumentaciuPodlaPripony(uuid, ".docx");
            }

            if (najdiAPresunDokumentaciuPodlaPripony(uuid,  ".odt") != "")
            {
                return najdiAPresunDokumentaciuPodlaPripony(uuid,  ".odt");
            }

            if (najdiAPresunDokumentaciuPodlaPripony(uuid,  ".txt") != "")
            {
                return najdiAPresunDokumentaciuPodlaPripony(uuid,  ".txt");
            }


            //najdiAPresunDokumentaciuPodlaPripony(uuid, zlozka, ".txt");
           // najdiAPresunDokumentaciuPodlaPripony(uuid, zlozka, ".doc");
           // najdiAPresunDokumentaciuPodlaPripony(uuid, zlozka, ".docx");
           // najdiAPresunDokumentaciuPodlaPripony(uuid, zlozka, ".pdf");
           // najdiAPresunDokumentaciuPodlaPripony(uuid, zlozka, ".odt");

            return "";

        }

        public string najdiAPresunDokumentaciuPodlaPripony(Guid uuid,  String pripona)
        {
            foreach (var cesta in Directory.EnumerateFiles("subory\\" + uuid.ToString() + "\\Kod", "*" + pripona.Trim(), SearchOption.AllDirectories))
            {

                return cesta;
                //string[] nazovSuboru = file.Split("\\");
                //string nazovSuboru = file.Split("\\");
                //System.IO.File.Copy(file, zlozka.FullName + "\\" + nazovSuboru.Last());
                break;
            }
            return "";
        }

        public void presunPracePodlaUuid( DirectoryInfo zlozka, List<Guid> uuidka)
        {
            foreach (var uuid in uuidka)
            {
                presunPracuPodlaUuid(zlozka, uuid);
            }
        }



        public void presunPracuPodlaUuid( DirectoryInfo zlozka, Guid uuid)
        {

            string nazovPrace = (from t in this.databaza.Tasks
                                 where (uuid == t.Uuid)
                                 select t.NameOfTask).First().ToString();
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(@"C:\Users\work\Desktop\AspBlogTesty\AspBlog\subory\" + uuid.ToString(), "*",
                SearchOption.AllDirectories))
                Directory.CreateDirectory(dirPath.Replace(@"C:\Users\work\Desktop\AspBlogTesty\AspBlog\subory\" + uuid.ToString(), zlozka.FullName + "\\" + nazovPrace));

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(@"C:\Users\work\Desktop\AspBlogTesty\AspBlog\subory\" + uuid.ToString(), "*.*",
                SearchOption.AllDirectories))
                File.Copy(newPath, newPath.Replace(@"C:\Users\work\Desktop\AspBlogTesty\AspBlog\subory\" + uuid.ToString(), zlozka.FullName + "\\" + nazovPrace), true);




           // ZipFile.CreateFromDirectory("subory\\" + uuid.ToString(), @cesta + "\\" + uuid.ToString() + ".zip");
            
              //  System.IO.File.Copy("subory\\" + uuid.ToString(), "subory\\" + uuid.ToString());
            
        }

        public bool vsetkyPraceExistuju(List<Guid> uuidka)
        {
            foreach (var uuid in uuidka)
            {
                if (Directory.Exists("subory\\" + uuid.ToString())) 
                {

                } else { return false; }
            }
            return true;
        }
    }
}
