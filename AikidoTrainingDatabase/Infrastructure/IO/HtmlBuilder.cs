using System.Collections.Generic;
using System.Text;

namespace AikidoTrainingDatabaseSql.HtmlExport
{
    /// <summary>
    /// Class for building the HTML code
    /// </summary>
    class HtmlBuilder
    {
        public enum Language { en, de };
        public enum Font { Arial, Helvetica, Verdana };

        private List<string> htmlCode;
        private Language currentLanguage;
        private Font currentFont;

        #region Constructors
        public HtmlBuilder()
        {
            htmlCode = new List<string>();
            currentLanguage = Language.de;
            currentFont = Font.Arial;
            InitDocument();
        }

        public HtmlBuilder(Language language, Font font)
        {
            htmlCode = new List<string>();
            currentLanguage = language;
            currentFont = font;
            InitDocument();
        }
        #endregion

        #region Getter/setter
        /// <summary>
        /// Setting this variable is not supported. It holds the current content of the 
        /// specified HTML document.
        /// </summary>
        public List<string> HtmlCode
        {
            get
            {
                return EndDocument();
            }
            private set { }
        }
        #endregion

        #region Class methods
        public List<string> ClearDocument()
        {
            htmlCode.Clear();
            return htmlCode;
        }

        public List<string> InitDocument()
        {
            htmlCode.Clear();
            htmlCode.Add("<!DOCTYPE html>");
            htmlCode.Add("<html lang = \""+ currentLanguage.ToString() +  "\">");
            htmlCode.Add("<head>");
            htmlCode.Add("<meta charset = \"utf-8\">");
            htmlCode.Add("<title>Trainingsstunde</title>");
            htmlCode.Add("<style>");
            htmlCode.Add("body {");
            htmlCode.Add("  font-family: "+ currentFont.ToString() + ",sans-serif;");
            htmlCode.Add("  font-size: 0.9em;");
            htmlCode.Add("}");
            htmlCode.Add("");
            htmlCode.Add("  header, footer {");
            htmlCode.Add("    padding: 10px;");
            htmlCode.Add("    color: white;");
            htmlCode.Add("    background-color: black;");
            htmlCode.Add("  }");
            htmlCode.Add("");
            htmlCode.Add("  section {");
            htmlCode.Add("    margin: 5px;");
            htmlCode.Add("    padding: 10px;");
            htmlCode.Add("    background-color: lightgrey;");
            htmlCode.Add("    }");
            htmlCode.Add("");
            htmlCode.Add("  article {");
            htmlCode.Add("      margin: 5px;");
            htmlCode.Add("      padding: 10px;");
            htmlCode.Add("      background-color: white;");
            htmlCode.Add("  }");
            htmlCode.Add("");
            htmlCode.Add("  nav ul {");
            htmlCode.Add("      padding: 0;");
            htmlCode.Add("  }");
            htmlCode.Add("");
            htmlCode.Add("  nav ul li {");
            htmlCode.Add("      display: inline;");
            htmlCode.Add("      margin: 5px;");
            htmlCode.Add("  }");
            htmlCode.Add("</style>");
            htmlCode.Add("</head>");
            htmlCode.Add("<body>");
            htmlCode.Add("<section>");
            return HtmlCode;
        }

        public List<string> AddTitle(string title)
        {
            htmlCode.Add("<h1>" + title + "</h1>");
            return HtmlCode;
        }

        public List<string> AddSection(string title)
        {
            htmlCode.Add("<h2>" + title + "</h2>");
            return HtmlCode;
        }

        public List<string> AddSection(string title, string content)
        {
            AddOpenArticle();
            AddSection(title);
            AddParagraph(content);
            AddCloseArticle();
            return HtmlCode;
        }

        public List<string> AddSection(string title, string subtitle, string content)
        {
            AddOpenArticle();
            AddSection(title);
            AddItalic(subtitle);
            AddParagraph(content);
            AddCloseArticle();
            return HtmlCode;
        }

        public List<string> AddSection(string title, List<string> imagePaths, string subtitle, string content)
        {
            AddOpenArticle();
            AddSection(title);
            AddItalic(subtitle);
            AddLineBreak();
            foreach (string s in imagePaths)   
            {
                AddImage(s);
            }
            AddLineBreak();
            AddParagraph(content);
            AddCloseArticle();
            return HtmlCode;
        }

        protected void AddLineBreak()
        {
            htmlCode.Add("<br>");
        }

        protected void AddParagraph(string content)
        {
            htmlCode.Add("<p>" + content + "</p>");
        }

        protected void AddItalic(string content)
        {
            htmlCode.Add("<i>" + content + "</i>");
        }

        private void AddOpenArticle()
        {
            htmlCode.Add("<article>");
        }

        private void AddCloseArticle()
        {
            htmlCode.Add("</article>");
        }

        protected void AddImage(string path)
        {
            htmlCode.Add("<img src = \"" + path + "\" style = \"width:128px;height:128px;\"/>");
        }

        private List<string> EndDocument()
        {
            List<string> tmp = htmlCode;
            tmp.Add("</section>");
            tmp.Add("</body>");
            tmp.Add("</html>");
            return tmp;
        }
        #endregion
    }
}
