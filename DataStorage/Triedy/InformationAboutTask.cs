using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspBlog.Triedy
{
    public class InformationAboutTask
    {
        public List<String> zoznamPridanychSuborov { get; set; }
        public string nazovPrace { get; set; }
        public DateTime datumVlozenia { get; set; }

        public InformationAboutTask(List<String> paZoznam,string paNazovPrace,DateTime paDatumVlozenia)
        {
            zoznamPridanychSuborov = paZoznam;
            nazovPrace = paNazovPrace;
            datumVlozenia = paDatumVlozenia;
            
        }
    }
}
