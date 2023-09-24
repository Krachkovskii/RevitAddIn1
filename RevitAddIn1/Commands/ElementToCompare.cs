using Autodesk.Revit.DB;
using RevitAddIn1.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RevitAddIn1.Commands
{
    internal static class Extensions
    {
        /// <summary>
        /// Gets a list of Element's parameters excluding those that are read-only.
        /// </summary>
        /// <param name="el"></param>
        /// <param name="sortParameters">If true, the parameters will be sorted alphabetically by their name.</param>
        /// <returns>List of parameters</returns>
        internal static List<Parameter> GetEditableParameters(this ElementToCompare el, bool sortParameters)
        {
            List<Parameter> parameters = new List<Parameter>();
            ParameterSet paramSet = el.DefiningElement.Parameters;
            foreach (Parameter p in paramSet)
            {
                if (p.IsReadOnly != true)
                {
                    parameters.Add(p);
                }
            }

            if (sortParameters)
            {
                List<Parameter> SortedParameters = parameters.OrderBy(p => p.Definition.Name).ToList();
                return SortedParameters;
            }
            else
            {
                return parameters;
            }
        }

        /// <summary>
        /// Get Parameters as string values. Parameters are obtained using GetEditableParameters method.
        /// </summary>
        /// <param name="el"></param>
        /// <returns>List of parameter values as strings.</returns>
        internal static List<string> GetParameterStringValues(this ElementToCompare el)
        {
            List<string> values = new();

            if(el.Parameters != null) {

                foreach (Parameter p in el.Parameters)
                {
                    if (p.StorageType == StorageType.String)
                    {
                        if (p.AsString() == "")
                        {
                            values.Add("<Empty value>");
                        }
                        else
                        {
                            values.Add(p.AsString());
                        }
                    }

                    else if (p.StorageType == StorageType.Integer)
                    {
                        if (p.Definition.ParameterType == ParameterType.YesNo)
                        {
                            if (p.AsInteger() == 0)
                            {
                                values.Add("No");
                            }
                            else if (p.AsInteger() == 1)
                            {
                                values.Add("Yes");
                            }
                        }
                        values.Add(p.AsInteger().ToString());
                    }

                    else if (p.StorageType == StorageType.Double)
                    {
                        values.Add(Math.Round(p.AsDouble(), 3).ToString());
                    }

                    else if (p.StorageType == StorageType.ElementId)
                    {
                        ElementId id = p.AsElementId();
                        if (id != null && id.IntegerValue != -1)
                        {
                            Element el2 = RevitApi.Document.GetElement(id);
                            try
                            {
                                values.Add(el2.Name);
                            }
                            catch
                            {
                                values.Add("Element does not have a name");
                            }
                        }
                        else
                        {
                            values.Add("Invalid ID");
                        }
                    }
                }
            }
            return values;
        }

        /// <summary>
        /// Gets a list of element Parameters that are identical (but not necessarily with the same value) to those of another Element instance. 
        /// This method will also set Parameters property of an Element instance.
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        internal static List<Parameter> GetIdenticalParameters(this ElementToCompare el1, ElementToCompare el2)
        {
            List<Parameter> matchingParameters = new List<Parameter>();

            if (el1.Parameters != null)
            {
                foreach (Parameter p1 in el1.Parameters)
                {
                    foreach (Parameter p2 in el2.GetEditableParameters(true))
                    {
                        if (p1.Definition.Name == p2.Definition.Name && p1.StorageType == p2.StorageType)
                        {
                            matchingParameters.Add(p1);
                            break;
                        }
                    }
                }
            }

            el1.Parameters = matchingParameters;
            return matchingParameters;
        }

        internal static List<string> GetParameterNames(this ElementToCompare el)
        {
            List<string> parameterNames = new ();
            if (el.Parameters != null)
            {
                foreach (Parameter p in el.Parameters)
                {
                    parameterNames.Add(p.Definition.Name);
                }
            }
            return parameterNames;
        }
    }

    internal class ElementToCompare
    {
        public event EventHandler ElementChanged;
        public event EventHandler ParameterValuesChanged;
        public ElementToCompare(Element el)
        {
            DefiningElement = el;
            Parameters = this.GetEditableParameters(true);
            StringValues = this.GetParameterStringValues();
            List<string> parameterNames = new List<string>();
            foreach (Parameter p in this.Parameters)
            {
                parameterNames.Add(p.Definition.Name);
            }
            ParameterNames = parameterNames;
        }

        private Element _definingElement;
        public Element DefiningElement
        {
            get { return _definingElement; }
            set 
            {
                if (_definingElement != value)
                {
                    _definingElement = value;
                    UpdateProperties();
                    NotifyElementChanged(nameof(DefiningElement));
                }
            }
        }

        internal List<Parameter> Parameters { get; set; }
        internal List<string> ParameterNames { get; private set; }

        private List<string> _stringValues;
        internal List<string> StringValues 
        {
            get 
            { 
                return _stringValues; 
            } 

            private set 
            { 
                if (_stringValues != value)
                {
                    _stringValues = value;
                    NotifyParameterValuesChanged(nameof(StringValues));
                }
            } 
        }

        internal void NotifyElementChanged([CallerMemberName] string propertyName = "")
        {
            ElementChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void NotifyParameterValuesChanged([CallerMemberName] string propertyName = "")
        {
            ParameterValuesChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateProperties()
        {
            this.GetEditableParameters(true);
            this.GetParameterNames();
            this.GetParameterStringValues();
        }

    }
}
