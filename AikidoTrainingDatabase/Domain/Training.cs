using System.Collections.ObjectModel;

namespace AikidoTrainingDatabase.Domain
{
    public class Training : ITraining
    {
        private int id;
        private string name;
        private string description;
        private ObservableCollection<Excercise> excercises;

        private const int INVALID_ID = -1;

        #region Getter/setter
        public int ID { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public ObservableCollection<Excercise> Excercises { get => excercises; set => excercises = value; }
        #endregion

        #region Constructor
        public Training()
        {
            ID = INVALID_ID;
            Name = string.Empty;
            Description = string.Empty;
            Excercises = new ObservableCollection<Excercise>();
        }

        public Training(string name)
        {
            ID = INVALID_ID;
            Name = name;
            Description = string.Empty;
            Excercises = new ObservableCollection<Excercise>();
        }

        public Training(string name, string description)
        {
            ID = INVALID_ID;
            Name = name;
            Description = description;
            Excercises = new ObservableCollection<Excercise>();
        }

        public Training(string name, string description, ObservableCollection<Excercise> excercises)
        {
            ID = INVALID_ID;
            Name = name;
            Description = description;
            Excercises = new ObservableCollection<Excercise>(excercises);
        }
        
        public Training(int id)
        {
            ID = id;
            Name = string.Empty;
            Description = string.Empty;
            Excercises = new ObservableCollection<Excercise>();
        }

        public Training(int id, string name)
        {
            ID = id;
            Name = name;
            Description = string.Empty;
            Excercises = new ObservableCollection<Excercise>();
        }

        public Training(int id, string name, string description)
        {
            ID = id;
            Name = name;
            Description = description;
            Excercises = new ObservableCollection<Excercise>();
        }

        public Training(int id, string name, string description, ObservableCollection<Excercise> excercises)
        {
            ID = id;
            Name = name;
            Description = description;
            Excercises = new ObservableCollection<Excercise>(excercises);
        }
        #endregion
    }
}
