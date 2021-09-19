using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrapper
{
    class Scrapper
    {
        public string Url { get; init; }

        public Scrapper()
        {
            Url = "http://www.schnupfspruch.ch/sprueche_view.asp";
        }

        public IEnumerable<(string Header, string Value)> Scrap()
        {
            var result = Urls();


            foreach (var url in result)
            {

                IEnumerable<string> ienum = null;
                var web = new HtmlWeb();
                var doc = web.Load(url);

                var nodes = doc.DocumentNode.SelectNodes("//td");

                ienum = nodes.Skip(2).SkipLast(3).Select(i => i.InnerText);

                var list = ienum.ToList();

                var header = "";

                for (int i = 0; i < list.ToList().Count; i++)
                {
                    if (i % 2 == 0)
                    {
                        header = list[i];
                        continue;
                    }

                    yield return (header.CRLFToLF(), list[i].CRLFToLF());
                }
            }
        }

        private IEnumerable<string> Urls()
        {
            var urls = new List<string>();
            // 109
            for (int i = 1; i < 70; i++)
            {
                urls.Add($"{Url}?MOVE={i}&PrevPage=1");
            }

            return urls;
        }
    }
}
