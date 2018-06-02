namespace AikidoTrainingDatabase.Domain
{
    public class Category : ICategory
    {
        private string name;
        private string description;
        private int _id;

        private const int INVALID_ID = -1;

        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }

        public int ID { get => _id; set => _id=value; }

        #region Constructors
        public Category()
        {
            _id = INVALID_ID;
            Name = string.Empty;
            Description = string.Empty;
        }

        public Category(string name)
        {
            _id = INVALID_ID;
            Name = name;
            Description = string.Empty;
        }

        public Category(string name, string description)
        {
            _id = INVALID_ID;
            Name = name;
            Description = description;
        }

        public Category(int id)
        {
            _id = id;
            Name = string.Empty;
            Description = string.Empty;
        }

        public Category(int id, string name)
        {
            _id = id;
            Name = name;
            Description = string.Empty;
        }

        public Category(int id, string name, string description)
        {
            _id = id;
            Name = name;
            Description = description;
        }

        public Category(ICategory category)
        {
            _id = category.ID;
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
