using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LukeMVC.Models
{
    public class VerseModel
    {
        public string unitId { get; set; }
        public string verseHeader { get; set; }
        public string  verseText { get; set; }
        public string cssClass { get; set; }
        public int verseNumber { get; set; }
        public string  verseBook { get; set; }


        public  VerseModel(string verseId)
        {
            // set the cssclass based on verseId
            this.unitId = verseId;
        }
    }
}