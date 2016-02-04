using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Linq;
using LukeMVC.Models;
using System.Text.RegularExpressions;

namespace ESVTest.Classes
{
    public class esvWrapper
    {
        const string baseUrl = "http://www.esvapi.org/v2/rest/";
        const string key = "key=IP";
        public string API { get; set; }
        public string Passage { get; set; }
        public string Output { get; set; }

        public esvWrapper(string api, string passage, string output)
        {
            // need to add 
            this.API = api;
            this.Passage = passage;
            this.Output = output;            
        }

        public string getESVPassage(string options = "")
        {
            using (var client = new HttpClient())
            {

                if (this.Passage.Length > 0)
                {

                    StringBuilder sUrl = new StringBuilder();
                    sUrl.Append("http://www.esvapi.org/v2/rest/");
                    sUrl.Append(this.API);
                    sUrl.Append("?");
                    sUrl.Append(key);

                    sUrl.Append("&passage=" + System.Web.HttpContext.Current.Server.UrlEncode(this.Passage).ToString());
                    sUrl.Append("&output-format=" + System.Web.HttpContext.Current.Server.UrlEncode(this.Output));
                    sUrl.Append(options);

                    // use the below for testing purposes but need to add an options builder tool 

                    sUrl.Append("&include-headings=true");

                    WebRequest oReq = WebRequest.Create(sUrl.ToString());
                    StreamReader sStream = new StreamReader(oReq.GetResponse().GetResponseStream());

                    StringBuilder sOut = new StringBuilder();
                    sOut.Append(sStream.ReadToEnd());
                    sStream.Close();

                    return sOut.ToString();
                }
                else
                { return string.Empty; }

            }
        }

        public SearchResultsModel getEsvXml()
        {

            SearchResultsModel sr = new SearchResultsModel();
            using (var client = new HttpClient())
            {

                if (this.Passage.Length > 0)
                {
                    String xml = string.Empty;

                    xml = getESVPassage("&include-simple-entities=true");

                    // may want to change this to the original search text followed by a seperate call for the xml
                    // that way i have both cached in memory
                    sr.searchResults = xml;

                    XDocument xDoc = XDocument.Parse(xml);

                    //xDoc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + @"\Gen1.xml");

                    var verses = (from v in xDoc.Descendants("verse-unit") select v);
                    StringBuilder sb = new StringBuilder();

                    var chapter = (from v in xDoc.Descendants("current") select v);

                    foreach(XElement c in chapter)
                    { sr.chapter = c.Value; }

                 


                    // below:
                    // instead of building verse html, i will build a verse model 
                    // and pass that model thru to the view for html parsing
                    foreach (XElement verse in verses)
                    {
                        sr.verses.Add(buildVerse(verse));
                    }
                    

                    // perform xml verse formatting prior to output.

                }

                return sr;

            }
        }        

        private VerseModel buildVerse(XElement verse)
        {
            VerseModel vm = new VerseModel(verse.Attribute("unit-id").Value.ToString());

            vm.verseHeader = (string)verse.Element("heading");
            vm.verseNumber = Int32.Parse(verse.Element("verse-num").Value);
            vm.cssClass = getCssClass(vm.unitId);

            vm.verseText = getVerseText(verse);

            return vm;
        }

        private string getVerseText(XElement verse)
        {
            StringBuilder sb = new StringBuilder();

            var textNodes = from c in verse.Nodes()
                            where c.NodeType == XmlNodeType.Text
                            select (XText)c;

            foreach (var t in textNodes)
            {
                sb.Append(t.Value);
            }

            return sb.ToString();
        }

        private string getCssClass(string vn)
        {
            Luke2432Entities db = new Luke2432Entities();

            var record = db.verse_category_log.Find(vn);
            var category = db.verse_category.Find(record.verse_category_id);
            return category.css_class_name;
        }

        string ReplaceFirst(string text, string search, string replace)
        {
            if (!String.IsNullOrEmpty(search))
            {
                int pos = text.IndexOf(search);
                if (pos < 0)
                {
                    return text;
                }
                return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
            }
            else
            { return text; }
        }
    }
}