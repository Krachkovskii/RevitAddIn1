using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CommunityToolkit.Mvvm.ComponentModel;
using RevitAddIn1.Commands;
using RevitAddIn1.Core;
using RevitAddIn1.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace RevitAddIn1.ViewModels
{
    public sealed class RevitAddIn1ViewModel : ObservableObject
    {
        internal event PropertyChangedEventHandler ElementListUpdated;

        public RevitAddIn1ViewModel()
        {
            RevitApi.ViewModelInstance = this;
        }

        private List<ElementToCompare> _elementList;
        internal List<ElementToCompare> ElementList
        {
            get
            {
                return _elementList;
            }
            set
            {
                if (_elementList != value)
                {
                    _elementList = value;
                    foreach (ElementToCompare e in _elementList)
                    {
                        //e.NotifyPropertyChanged();
                    }
                    NotifyElementListUpdated();
                }
            }
        }

        /// <summary>
        /// Replaces specific instance in selection list. Index of instance is passed through the button click arguments.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="number">Index to replace</param>
        public void UpdateSelection(object sender, int number)
        {
            ElementToCompare newEl = Command.PickElement("Select element to update");

            if (number == 0)
            {
                newEl.GetIdenticalParameters(ElementList[1]);
                ElementList[1].GetIdenticalParameters(newEl);
            }
            else if (number == 1)
            {
                newEl.GetIdenticalParameters(ElementList[0]);
                ElementList[0].GetIdenticalParameters(newEl);
            }

            ElementList.RemoveAt(number);
            ElementList.Insert(number, newEl);
        }

        private void NotifyElementListUpdated([CallerMemberName] string propertyName = "")
        {
            ElementListUpdated?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    
}