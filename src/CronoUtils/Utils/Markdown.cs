using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CronoUtils
{

    public class Markdown
    {


        private Markdown()
        {
        }


        public static string Transform(string text)
        {
            var md = new MarkdownSharp.Markdown();
            return md.Transform(text);
        }



    }




}