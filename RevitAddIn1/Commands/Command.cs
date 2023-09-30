using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using RevitAddIn1.Utils;
using RevitAddIn1.ViewModels;
using RevitAddIn1.Views;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.DependencyInjection;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitAddIn1.Core;
using Autodesk.Revit.DB.Visual;
using System.Linq.Expressions;
using System.Windows;

namespace RevitAddIn1.Commands
{
    [UsedImplicitly]
    [Transaction(TransactionMode.Manual)]
    public class Command : ExternalCommand
    {
        public override void Execute()
        {
            RevitApi.UiApplication = this.UiApplication;

            UIDocument uidoc = RevitApi.UiDocument;
            Document doc = uidoc.Document;

            if (WindowController.Focus<RevitAddIn1View>()) return;

            var viewModel = new RevitAddIn1ViewModel();
            RevitApi.ViewModelInstance = viewModel;
            var view = new RevitAddIn1View(viewModel);
            WindowController.Show(view, UiApplication.MainWindowHandle);

            ElementToCompare el1 = PickElement("Select reference element");
            ElementToCompare el2 = PickElement("Select element to update");

            List<ElementToCompare> elList = new List<ElementToCompare>
            {
                el1,
                el2
            };

            viewModel.ElementList = elList;
        }

        /// <summary>
        /// Prompts user to select an element in active Revit window.
        /// </summary>
        /// <param name="text">Annotation that will be displayed in lower left corner of the window.</param>
        /// <returns>An element in current Revit document.</returns>
        internal static ElementToCompare PickElement(string text)
        {
            UIDocument uidoc = RevitApi.UiDocument;
            Selection sel = uidoc.Selection;
            Reference selRef = sel.PickObject(ObjectType.Element, text);
            
            Element el = uidoc.Document.GetElement(selRef);
            ElementToCompare result = new(el);

            return result;
        }

        /// <summary>
        /// Copies parameter value from the first element to the second. This method handles all types of parameter data.
        /// </summary>
        /// <param name="p1">Reference parameter</param>
        /// <param name="p2">Parameter to be updated</param>
        private static void CloneParameterValue(Parameter p1, Parameter p2)
        {
            if(p1.StorageType == StorageType.Integer)
            {
                p2.Set(p1.AsInteger());                
            }
            else if (p1.StorageType == StorageType.String)
            {
                p2.Set(p1.AsString());                
            }
            else if (p1.StorageType == StorageType.Double)
            {
                p2.Set(p1.AsDouble());
            }
            else if (p1.StorageType == StorageType.ElementId)
            {
                p2.Set(p1.AsElementId());
            }                          
        }

    }
}