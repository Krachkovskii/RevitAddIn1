using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevitAddIn1.Core;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitAddIn1.Commands
{
    internal class ExternalEventHandler : IExternalEventHandler
    {
        public void Execute(UIApplication app)
        {

        }

        public string GetName()
        {
            string Return = "WTF is this";
            return Return;
        }
    }
}
