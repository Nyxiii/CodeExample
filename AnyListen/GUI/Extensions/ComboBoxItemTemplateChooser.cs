using System.Windows;
using System.Windows.Controls;
using AnyListen.Utilities;

namespace AnyListen.GUI.Extensions
{
    public class ComboBoxItemTemplateChooser : DataTemplateSelector
    {
        #region SelectedTemplate

        public static DependencyProperty SelectedTemplateProperty =
            DependencyProperty.RegisterAttached("SelectedTemplate",
                typeof(DataTemplate),
                typeof(Comb