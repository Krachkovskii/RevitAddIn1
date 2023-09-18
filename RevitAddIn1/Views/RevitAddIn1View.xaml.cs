using Autodesk.Revit.DB;
using RevitAddIn1.Commands;
using RevitAddIn1.Core;
using RevitAddIn1.ViewModels;
using System.Drawing.Text;
using System.Windows;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace RevitAddIn1.Views
{
    public partial class RevitAddIn1View
    {
        public RevitAddIn1View(RevitAddIn1ViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;

            viewModel.ElementListUpdated += ViewModel_ElementListUpdated;
            ParameterNames.SelectionChanged += ListBox_SelectionChanged;
            Element1Parameters.SelectionChanged += ListBox_SelectionChanged;
            Element2Parameters.SelectionChanged += ListBox_SelectionChanged;

            this.Left = SystemParameters.PrimaryScreenWidth - this.Width - 20;

            ParameterNames.AddHandler(ScrollViewer.ScrollChangedEvent, new RoutedEventHandler(ParameterList_ScrollChanged));
            Element1Parameters.AddHandler(ScrollViewer.ScrollChangedEvent, new RoutedEventHandler(ParameterList_ScrollChanged));
            Element2Parameters.AddHandler(ScrollViewer.ScrollChangedEvent, new RoutedEventHandler(ParameterList_ScrollChanged));
        }

        private List<int> selectedIndicesParameterNames = new List<int>();
        private List<int> selectedIndicesElement1Parameters = new List<int>();
        private List<int> selectedIndicesElement2Parameters = new List<int>();

        private void ViewModel_ElementListUpdated(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            List<ElementToCompare> ElementList = RevitApi.ViewModelInstance.ElementList;
            if (e.PropertyName == "ElementList")
            {
                Element1Parameters.ItemsSource = ElementList[0].StringValues;
                Element2Parameters.ItemsSource = ElementList[1].StringValues;

                ObservableCollection<string> ParameterNamesCollection = new ObservableCollection<string>(ElementList[0].ParameterNames);
                ParameterNames.ItemsSource = ParameterNamesCollection;

                element1Id.Text = ElementList[0].DefiningElement.Id.IntegerValue.ToString();
                element2Id.Text = ElementList[1].DefiningElement.Id.IntegerValue.ToString();

                StartingTextBlock.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void selectNewId1_Click(object sender, RoutedEventArgs e)
        {
            RevitApi.ViewModelInstance.UpdateSelection(sender, 0);
        }

        private void selectNewId2_Click(object sender, RoutedEventArgs e)
        {
            RevitApi.ViewModelInstance.UpdateSelection(sender, 1);
        }

        // Methods that synchronize the scrolling
        private void ParameterList_ScrollChanged(object sender, RoutedEventArgs e)
        {
            ScrollViewer scrollViewer = FindVisualChild<ScrollViewer>((ListBox)sender);

            double verticalOffset = scrollViewer.VerticalOffset;

            ApplyScrollOffset(ParameterNames, verticalOffset);
            ApplyScrollOffset(Element1Parameters, verticalOffset);
            ApplyScrollOffset(Element2Parameters, verticalOffset);
        }

        private void ApplyScrollOffset(ListBox listBox, double verticalOffset)
        {
            ScrollViewer scrollViewer = FindVisualChild<ScrollViewer>(listBox);

            if (scrollViewer != null)
            {
                scrollViewer.ScrollToVerticalOffset(verticalOffset);
            }
        }


        // Methods that synchronize selections
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected items in the sender ListBox
            var selectedItems = ((ListBox)sender).SelectedItems;

            if (sender == ParameterNames)
            {
                selectedIndicesParameterNames.Clear();
                foreach (var item in selectedItems)
                {
                    selectedIndicesParameterNames.Add(ParameterNames.Items.IndexOf(item));
                }
            }
            else if (sender == Element1Parameters)
            {
                selectedIndicesElement1Parameters.Clear();
                foreach (var item in selectedItems)
                {
                    selectedIndicesElement1Parameters.Add(Element1Parameters.Items.IndexOf(item));
                }
            }
            else if (sender == Element2Parameters)
            {
                selectedIndicesElement2Parameters.Clear();
                foreach (var item in selectedItems)
                {
                    selectedIndicesParameterNames.Add(Element2Parameters.Items.IndexOf(item));
                }
            }

            // Synchronize selected items in the other ListBox controls
            SynchronizeSelectedIndices(ParameterNames, selectedIndicesParameterNames);
            SynchronizeSelectedIndices(Element1Parameters, selectedIndicesElement1Parameters);
            SynchronizeSelectedIndices(Element2Parameters, selectedIndicesElement2Parameters);
        }

        /// <summary>
        /// Method suggested by ChatGTP to synchronize selection indexes across multiple ListBoxes
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="selectedIndices"></param>
        private void SynchronizeSelectedIndices(ListBox listBox, IList<int> selectedIndices)
        {
            listBox.SelectedItems.Clear();

            foreach (var index in selectedIndices)
            {
                if (index >= 0 && index < listBox.Items.Count)
                {
                    listBox.SelectedItems.Add(listBox.Items[index]);
                }
            }
        }


        /// <summary>
        /// Method suggested by ChatGPT that finds visual children of elements - in this case, it is used for getting a ScrollViewer object from a ListView.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="depObj"></param>
        /// <returns></returns>
        private T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) return null;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child != null && child is T)
                    return (T)child;
                T childItem = FindVisualChild<T>(child);
                if (childItem != null) return childItem;
            }
            return null;
        }

        private void TransferParameters1to2_Click(object sender, RoutedEventArgs e)
        {
            Command.CloneSelectedValues(SelectedIndices);
        }
        private List<int> SelectedIndices { get; set; } = new List<int>();
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int index = ParameterNames.Items.IndexOf(checkBox.Content);

            List<int> selInd = SelectedIndices;
            if ((bool)checkBox.IsChecked)
            {
                if ( ! selInd.Contains(index))
                {
                    selInd.Add(index);
                }
            }
            else
            {
                if (selInd.Contains(index))
                {
                    selInd.Remove(index);
                }
            }

            SelectedIndices = selInd;

            string message = string.Join(", ", SelectedIndices.ToArray());
            MessageBox.Show(message);
            
        }
    }
}