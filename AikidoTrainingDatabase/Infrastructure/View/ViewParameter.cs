using AikidoTrainingDatabase.ApplicationLayer;
using System;
using System.Collections.Generic;

namespace AikidoTrainingDatabase.Infrastructure.View
{
    /// <summary>
    /// View parameter collects all data which should be given to a GUI.
    /// </summary>
    public class ViewParameter
    {
        public enum Action { Empty, CategoryCreate, CategoryShow, CategoryEdit, ExcerciseShow, ExcerciseCreate, ExcerciseEdit, TrainingShow, TrainingEdit, TrainingCreate, MainMenuShow };
        private List<Object> paramList;
        
        public ViewParameter(Action action, IGui gui, IApplication application)
        {
            paramList = new List<object>();
            paramList.Add(action);
            paramList.Add(gui);
            paramList.Add(application);
        }

        public ViewParameter(Object[] objList)
        {
            paramList = new List<object>();
            foreach (Object obj in objList)
            {
                paramList.Add(obj);
            }
        }

        public void AddParameter(Object obj)
        {
            paramList.Add(obj);
        }

        public void RemoveParameter(Object obj)
        {
            paramList.Remove(obj);
        }

        /// <summary>
        /// Return all additional parameter which are not part of the constructor.
        /// </summary>
        /// <returns></returns>
        public Object[] GetParameter()
        {
            List<Object> paramListSub = new List<Object>();
            for (int i = 3; i < paramList.Count; i++)
            {
                paramListSub.Add(paramList[i]);
            }
            return paramListSub.ToArray();
        }

        public Object[] GetParameterComplete()
        {
            return paramList.ToArray();
        }

        public Action GetAction()
        {
            return (Action)paramList[0];
        }

        public IGui GetGui()
        {
            return (IGui)paramList[1];
        }

        public IApplication GetApplication()
        {
            return (IApplication)paramList[2];
        }
    }
}
