namespace AikidoTrainingDatabase.Domain
{
    public class Category : ICategory
    {
        private string name;
        private string description;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }

        #region Constructors
        public Category()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        public Category(string name)
        {
            Name = name;
            Description = string.Empty;
        }

        public Category(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public Category(ICategory category)
        {
            Name = category.Name;
            Description = category.Name;
        }
        #endregion

        public override string ToString()
        {
            return Name;
        }
    }
}
