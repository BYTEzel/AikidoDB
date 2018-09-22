using AikidoTrainingDatabase.Domain;

namespace AikidoTrainingDatabaseSql.HtmlExport
{
    /// <summary>
    /// Class for building training-specific HTML code.
    /// </summary>
    class HtmlBuilderTraining : HtmlBuilder
    {
        public HtmlBuilderTraining(ITraining training)
        {
            AddTitle(training.Name);
            AddParagraph(training.Description);

            string categoriesString = "";

            foreach (IExcercise excercise in training.Excercises)
            {
                // Concatenate the categories
                if (excercise.Categories.Count > 0)
                {
                    categoriesString = excercise.Categories[0].Name;
                    for (int i = 1; i < excercise.Categories.Count; i++)
                    {
                        categoriesString += ", " + excercise.Categories[i].Name;
                    }
                }
                AddSection(excercise.Name, categoriesString, excercise.Description);
            }
        }

        public string GetDocument()
        {
            string doc = "";

            foreach(string line in HtmlCode)
            {
                doc += line + "\n";
            }

            return doc;
        }
    }
}
