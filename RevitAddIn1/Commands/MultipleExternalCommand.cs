using System;
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
        internal enum OperationType
        {
            UpdateParameters
        }

        private OperationType _operationType;

        internal MultipleExternalCommand ( OperationType type )
        {
            _operationType = type;
        }
        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            switch (_operationType)
            {
                case OperationType.UpdateParameters:
                    try
                    {
                        using (Transaction t = new Transaction(RevitApi.Document, "Updating parameters"))
                        {
                            t.Start();
                            t.Commit();
                        }
                        return Result.Succeeded;
                    }

                    catch (Exception ex)
                    {
                        return Result.Failed;
                    }
                
                default: return Result.Cancelled;
            }
        }
    }
}
