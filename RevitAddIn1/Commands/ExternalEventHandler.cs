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
    internal class EventData
    {
        internal List<int> Indices { get; set; }
        internal int ReferenceElementIndex { get; set; }
        internal OperationType operationType { get; set; }

    }

    public enum OperationType
    {
        UpdateParameters
    }

    internal class ExternalEventHandler : IExternalEventHandler
    {
        
        private EventData eventData;
        internal ExternalEventHandler(EventData data)
        {
            eventData = data;
        }

        public void Execute(UIApplication app)
        {
            switch(eventData.operationType)
            {
                case OperationType.UpdateParameters:
                    MultipleExternalCommand multipleExternalCommand = new MultipleExternalCommand();
                    break;

                default:
                    break;
            }
        }

        public string GetName()
        {
            return "ExternalEventHandler";
        }
    }
}
