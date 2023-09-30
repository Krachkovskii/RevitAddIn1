using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitAddIn1.Core;
using Nice3point.Revit.Toolkit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAddIn1.Commands
{
    internal class MultipleExternalCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                ExternalEvent externalEvent = ExternalEvent.Create(new ExternalEventHandler(new EventData()));
                using (Transaction t = new Transaction(RevitApi.Document, "Updating parameters"))
                {
                    t.Start();
                    string msg = "Looks like Transaction is running smoothly!";
                    t.Commit();
                    string msg2 = "Transaction finished!";
                    string msg3 = msg + "\n" + msg2;

                    MessageBox.Show(msg3);
                }
                return Result.Succeeded;
            }

            catch (Exception ex)
            {
                return Result.Failed;
            }   
        }
    }
}
