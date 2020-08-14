using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspBlog.Triedy
{
    public class Statistics
    {
        public List<ProgrammingLanguage> jazyky;
        public int pocetSuborovCelkovo;
        public int pocetRiadkovCelkovo;
        public int pocetRiadkovOkremPrazdnych;

        public Statistics()
        {
            jazyky = new List<ProgrammingLanguage>();
            pocetSuborovCelkovo = 0;
            pocetRiadkovCelkovo = 0;
            pocetRiadkovOkremPrazdnych = 0;
        }
        
            public  void nacitaj()
            {
           // char ch = '.';
                try
                {   // Open the text file using a stream reader.
                    using (StreamReader sr = new StreamReader("jazyky\\Jazyky.txt"))
                    {
                        // Read the stream to a string, and write the string to the console.
                        //String line = sr.ReadLine();
                    // Console.WriteLine(line);
                    while (sr.Peek() >= 0)
                    {
                        String line = sr.ReadLine();
                       // char pokus = line[0];
                        if(line[0].Equals('.'))
                        {
                            jazyky.Last<ProgrammingLanguage>().priponyJazyku.Add(line.Trim());
                        } else
                        {
                            ProgrammingLanguage j = new ProgrammingLanguage(line.Trim());
                            jazyky.Add(j);
                        }
                        //Console.WriteLine(sr.ReadLine());
                    }
                }
                }
                catch (IOException e)
                {
               // Console.WriteLine(e.Message);
            }
            }


        public void PocetRiadkovJazyky(string cesta)
        {
            foreach (var jazyk in jazyky)
            {
                PocetRiadkovJazyka(cesta, jazyk);
            }

            //return pocetRiadkov;
        }


        public void PocetRiadkovJazyka(string cesta, ProgrammingLanguage jazyk)
        {
            int pocRiadkov = 0;
            int pocRiadkovBezPrazdnych = 0;
            foreach (var pripona in jazyk.priponyJazyku)
            {
                pocRiadkov = pocRiadkov + PocetRiadkovPriponyJazyka(cesta, pripona);
                pocRiadkovBezPrazdnych = pocRiadkovBezPrazdnych + PocetRiadkovBezPrazdnychPriponyJazyka(cesta, pripona);
            }
            jazyk.pocetRiadkov = pocRiadkov;
            jazyk.pocetRiadkovOkremPrazdnych = pocRiadkovBezPrazdnych;
            //return pocetRiadkov;
        }



        public int PocetRiadkovPriponyJazyka(string cesta,string pripona)
        {
            FileInfo[] csFiles = new DirectoryInfo(cesta.Trim())
                                        .GetFiles("*" + pripona.Trim(), SearchOption.AllDirectories);

            int totalNumberOfLines = 0;
            Parallel.ForEach(csFiles, fo =>
            {
                Interlocked.Add(ref totalNumberOfLines, CountNumberOfLine(fo));
            });
            return totalNumberOfLines;
        }

        public int PocetRiadkovBezPrazdnychPriponyJazyka(string cesta, string pripona)
        {
            FileInfo[] csFiles = new DirectoryInfo(cesta.Trim())
                                        .GetFiles("*" + pripona.Trim(), SearchOption.AllDirectories);

            int totalNumberOfLines = 0;
            Parallel.ForEach(csFiles, fo =>
            {
                Interlocked.Add(ref totalNumberOfLines, CountNumberOfLineWithoutEmpty(fo));
            });
            return totalNumberOfLines;
        }

        public void PocetRiadkovCelkovo(string cesta)
        {

            foreach (var jazyk in jazyky)
            {
                pocetRiadkovCelkovo = pocetRiadkovCelkovo + jazyk.pocetRiadkov;
                pocetRiadkovOkremPrazdnych = pocetRiadkovOkremPrazdnych + jazyk.pocetRiadkovOkremPrazdnych;
            }
            /* FileInfo[] csFiles = new DirectoryInfo(cesta.Trim())
                                         .GetFiles("*", SearchOption.AllDirectories);

             int totalNumberOfLines = 0;
             Parallel.ForEach(csFiles, fo =>
             {
                 Interlocked.Add(ref totalNumberOfLines, CountNumberOfLine(fo));
             });
             pocetRiadkovCelkovo =  totalNumberOfLines;*/
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
                   // if(riadokJePrazdny(line.Trim()))
                  //  {
                 //   }
                  //  else { 
                    // if (IsRealCode(line.Trim(), ref inComment))
                    count++;
               // }
            }
            }
            return count;
        }

        private int CountNumberOfLineWithoutEmpty(Object tc)
        {
            FileInfo fo = (FileInfo)tc;
            int count = 0;
            int inComment = 0;
            using (StreamReader sr = fo.OpenText())
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                     if(riadokJePrazdny(line.Trim()))
                      {
                       }
                      else { 
                    // if (IsRealCode(line.Trim(), ref inComment))
                    count++;
                     }
                }
            }
            return count;
        }


        private bool riadokJePrazdny(string riadok)
        {
            if (riadok == "")
            {
                return true;
            }
            else return false;
            
        }

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



        public void PocetSuborovJazyky(string cesta)
        {
            
            foreach (var jazyk in jazyky)
            {
            PocetSuborovJazyka(cesta, jazyk);
            }            
            // return pocetSuborov;
        }

        public void PocetSuborovJazyka(string cesta,ProgrammingLanguage jazyk)
        {
            int pocSuborov = 0;
            foreach (var pripona in jazyk.priponyJazyku)
            {
                pocSuborov = pocSuborov + PocetSuborovPripony(cesta, pripona);
            }
            jazyk.pocetSuborov = pocSuborov;
           // return pocetSuborov;
        }

        public int PocetSuborovPripony(string cesta, string pripona)
        {
            var fileCount = (from file in Directory.EnumerateFiles(cesta.Trim(), "*"+ pripona.Trim(), SearchOption.AllDirectories)
                        select file).Count();
            return fileCount;
        }


        public void PocetSuborovCelkovo(string cesta)
        {
            var fileCount = (from file in Directory.EnumerateFiles(cesta.Trim(), "*" , SearchOption.AllDirectories)
                             select file).Count();
            pocetSuborovCelkovo = fileCount;
        }

        public void vymazNepotrebne()
        {
            List<ProgrammingLanguage> pomocnyNaMazanie = new List<ProgrammingLanguage>();
            foreach (var jazyk in jazyky)
            {
                if (jazyk.pocetSuborov == 0)
                {
                    pomocnyNaMazanie.Add(jazyk);
                }
            }

            foreach (var jazyk in pomocnyNaMazanie)
            {
                
                    jazyky.Remove(jazyk);
                
            }
        }

        public void spusti(string cesta)
        {
            nacitaj();
            PocetRiadkovJazyky(cesta);
            PocetSuborovJazyky(cesta);
            PocetRiadkovCelkovo(cesta);
            PocetSuborovCelkovo(cesta);
            vymazNepotrebne();
        }
    }
}
