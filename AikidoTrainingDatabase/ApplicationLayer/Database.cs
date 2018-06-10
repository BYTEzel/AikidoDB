using AikidoTrainingDatabase.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AikidoTrainingDatabase.ApplicationLayer
{
    /// <summary>
    /// Collects the data which is currently present in the script.
    /// </summary>
    public class Database : IDatabase
    {
        private ObservableCollection<Category> categoryList;
        private IdDatabase categoryId;
        private ObservableCollection<Excercise> excerciseList;
        private IdDatabase excerciseId;

        public Database()
        {
            categoryList = new ObservableCollection<Category>();
            categoryId = new IdDatabase();
            excerciseList = new ObservableCollection<Excercise>();
            excerciseId = new IdDatabase();
        }

        public ObservableCollection<Category> CategoryList { get => categoryList; set => categoryList = value;  }
        public IdDatabase CategoryId { get => categoryId; set => categoryId = value; }
        public ObservableCollection<Excercise> ExcerciseList { get => excerciseList; set => excerciseList = value; }
        public IdDatabase ExcerciseId { get => excerciseId; set => excerciseId = value; }

        public void Create(ICategory category)
        {
            category.ID = categoryId.CreateId();
            categoryList.Add(category as Category);
        }

        public void Create(IExcercise excercise)
        {
            excercise.ID = excerciseId.CreateId();
            excerciseList.Add(excercise as Excercise);
        }

        public void Delete(ICategory category)
        {
            for (int c = 0; c < categoryList.Count; c++)
            {
                if (category.ID == categoryList[c].ID)
                {
                    categoryList.RemoveAt(c);
                    categoryId.DeleteEnty(category);
                    break;
                }
            }
            
            // Check the excercise list for occurences of this category
            foreach(Excercise e in excerciseList)
            {             
                for (int c = 0; c < e.Categories.Count; c++)
                {
                    // If the category is present in the list, delete it and stop the search in this excercise
                    // (categories may only be added once).
                    if(category.ID == e.Categories[c].ID)
                    {
                        e.Categories.RemoveAt(c);
                        break;
                    }
                }
            }

        }

        public void Delete(IExcercise excercise)
        {
            for (int e = 0; e < excerciseList.Count; e++)
            {
                if (excercise.ID == excerciseList[e].ID)
                {
                    excerciseList.RemoveAt(e);
                    excerciseId.DeleteEnty(excercise);
                    break;
                }
            }
                        
        }

        public void Edit(ICategory category)
        {
            for (int c=0; c < categoryList.Count; c++)
            {
                if (categoryList[c].ID == category.ID)
                {
                    categoryList[c] = category as Category;
                    break;
                }
            }

            // Look for all places where the the category may be used.
            for (int e = 0; e < excerciseList.Count; e++)
            {
                for (int c=0; c<excerciseList[e].Categories.Count; c++)
                {
                    if (excerciseList[e].Categories[c].ID == category.ID)
                    {
                        excerciseList[e].Categories[c] = category as Category;
                        break;
                    }
                }
            }
        }

        public void Edit(IExcercise excercise)
        {
            //excerciseList[index] = excercise as Excercise;
        }

    }

    /// <summary>
    /// This class is used to create the IDs for the entries of a database. It provides an integer value for the next item (-> if you want to create a new item)
    /// and furthermore provides the possibility to re-use old IDs by storing them in a list.
    /// </summary>
    public class IdDatabase
    {
        private int id;
        private List<int> blankIds;
        
        public int Id { get => id; set => id = value; }
        public List<int> BlankIds { get => blankIds; set => blankIds = value; }

        public int CreateId()
        {
            // Re-use Ids if there are not used anymore
            if (BlankIds.Count > 0)
            {
                int tmp = BlankIds[0];
                BlankIds.RemoveAt(0);
                return tmp;
            }
            else
            {
                id++;
                return id;
            }
        }

        public IdDatabase()
        {
            id = -1;
            BlankIds = new List<int>();
        }

        public void DeleteEnty(IID obj)
        {
            if (obj.ID > id)
            {
                throw new IndexOutOfRangeException("The index references an invalid dataset.");
            }
            else
            {
                BlankIds.Add(obj.ID);
            }
        }
    }
}
