using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LukeMVC.Models
{
    public class SearchResultsModel
    {
        public string searchResults { get; set; }
        public List<VerseModel> verses { get; set; }
        public string chapter { get; set; }

        public SearchResultsModel ()
        {
            verses = new List<VerseModel>();
        }
    }
}