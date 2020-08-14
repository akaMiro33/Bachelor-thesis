using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspBlog.Triedy
{
    public class ProgrammingLanguage
    {
        public string NazovJazyku { get; set; }
        public List<String> priponyJazyku { get; set; }
        public int pocetSuborov { get; set; }
        public int pocetRiadkov { get; set; }
        public int pocetRiadkovOkremPrazdnych { get; set; }

        public ProgrammingLanguage(string paNazovJazyku)
        {
            NazovJazyku = paNazovJazyku;
            priponyJazyku = new List<string>();
            pocetRiadkov = 0;
            pocetSuborov = 0;
            pocetRiadkovOkremPrazdnych = 0;
        }
    }
}
