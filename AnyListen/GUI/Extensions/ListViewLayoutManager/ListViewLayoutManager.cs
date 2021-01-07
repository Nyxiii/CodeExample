// -- FILE ------------------------------------------------------------------
// name       : ListViewLayoutManager.cs
// created    : Jani Giannoudis - 2008.03.27
// language   : c#
// environment: .NET 3.0
// copyright  : (c) 2008-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace AnyListen.GUI.Extensions.ListViewLayoutManager
{

    // ------------------------------------------------------------------------
    public class ListViewLayoutManager
    {

        // ----------------------------------------------------------------------
        public static readonly DependencyProperty EnabledProperty = DependencyProperty.RegisterAttached(
            "Enabled",
            typeof(bool),
            typeof(ListViewLayoutManager),
            new FrameworkPropertyMetadata(new PropertyChangedCallback(OnLayoutManagerEnabledChanged)));

        // ----------------------------------------------------------------------
        public ListViewLayoutManager(ListView listView)
        {
            if (listView == null)
            {
                throw new ArgumentNullException("listView");
            }

            this.listView = listView;
            this.listView.Loaded += new RoutedEventHandler(ListViewLoaded);
            this.listView.Unloaded += new RoutedEventHandler(ListViewUnloaded);
        } // ListViewLayoutManager

        // ----------------------------------------------------------------------
        public ListView ListView => listView;
        // ListView

        // ----------------------------------------------------------------------
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return verticalScrollBarVisibility; }
            set { verticalScrollBarVisibility = value; }
        } // VerticalScrollBarVisibility

        // ----------------------------------------------------------------------
        public static void SetEnabled(DependencyObject dependencyObject, bool enabled)
        {
            dependencyObject.SetValue(EnabledProperty, enabled);
        } // SetEnabled

        // ----------------------------------------------------------------------
        public void Refresh()
        {
            InitColumns();
            DoResizeColumns();
        } // Refresh

        // ----------------------------------------------------------------------
        private void RegisterEvents(DependencyObject start)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(start); i++)
            {
                Visual childVisual = VisualTreeHelper.GetChild(start, i) as Visual;
                if (childVisual is Thumb)
                {
                    GridViewColumn gridViewColumn = FindParentColumn(childVisual);
                    if (gridViewColumn != null)
                    {
                        Thumb thumb = childVisual as Thumb;
                        if (ProportionalColumn.IsProportionalColumn(gridViewColumn) ||
                            FixedColumn.IsFixedColumn(gridViewColumn) || IsFillColumn(gridViewColumn))
                        {
                            thumb.IsHitTestVisible = false;
                        }
                        else
                        {
                            thumb.PreviewMouseMove += new MouseEventHandler(ThumbPreviewMouseMove);
                            thumb.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(ThumbPreviewMouseLeftButtonDown);
                            DependencyPropertyDescriptor.FromProperty(
                                GridViewColumn.WidthProperty,
                                typeof(GridViewColumn)).AddValueChanged(gridViewColumn, GridColumnWidthChanged);
                        }
                    }
                }
                else if (childVisual is GridViewColumnHeader)
                {
                    GridViewColumnHeader columnHeader = childVisual as GridViewColumnHeader;
                    columnHeader.SizeChanged += new SizeChangedEventHandler(GridColumnHeaderSizeChanged);
                }
                else if (scrollViewer == null && childVisual is ScrollViewer)
                {
                    scrollViewer = childVisual as ScrollViewer;
                    scrollViewer.ScrollChanged += new ScrollChangedEventHandler(ScrollViewerScrollChanged);
                    // assume we do the regulation of the horizontal scrollbar
                    scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    scrollViewer.VerticalScrollBarVisibility = verticalScrollBarVisibility;
                }

                RegisterEvents(childVisual);  // recursive
            }
        } // RegisterEvents

        // ----------------------------------------------------------------------
        private void UnregisterEvents(DependencyObject start)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(start); i++)
            {
                Visual childVisual = VisualTreeHelper.GetChild(start, i) as Visual;
                if (childVisual is Thumb)
                {
                    GridViewColumn gridViewColumn = FindParentColumn(childVisual);
                    if (gridViewColumn != null)
                    {
                        Thumb thumb = childVisual as Thumb;
                        if (ProportionalColumn.IsProportionalColumn(gridViewColumn) ||
                            FixedColumn.IsFixedColumn(gridViewColumn) || IsFillColumn(gridViewColumn))
                        {
                            thumb.IsHitTestVisible = true;
                        }
                        else
                        {
                            thumb.PreviewMouseMove -= new MouseEventHandler(ThumbPreviewMouseMove);
                            thumb.PreviewMouseLeftButtonDown -= new MouseButtonEventHandler(ThumbPreviewMouseLeftButtonDown);
                            DependencyPropertyDescriptor.FromProperty(
                                GridViewColumn.WidthProperty,
                                typeof(GridViewColumn)).RemoveValueChanged(gridViewColumn, GridColumnWidthChanged);
                        }
                    }
                }
                else if (childVisual is GridViewColumnHeader)
                {
                    GridViewColumnHeader columnHeader = childVisual as GridViewColumnHeader;
                    columnHeader.SizeChanged -= new SizeChangedEventHandler(GridColumnHeaderSizeChanged);
                }
                else if (scrollViewer == null && childVisual is ScrollViewer)
                {
                    scrollViewer = childVisual as ScrollViewer;
                    scrollViewer.ScrollChanged -= new ScrollChangedEventHandler(ScrollViewerScrollChanged);
                }

                UnregisterEvents(childVisual);  // recursive
            }
        } // UnregisterEvents

        // ----------------------------------------------------------------------
        private GridViewColumn FindParentColumn(DependencyObject element)
        {
            if (element == null)
            {
                return null;
            }

            while (element != null)
            {
                GridViewColumnHeader gridViewColumnHeader = element as GridViewColumnHeader;
                if (gridViewColumnHeader != null)
                {
                    return (gridViewColumnHeader).Column;
                }
                element = VisualTreeHelper.GetParent(element);
            }

            return null;
        } // FindParentColumn

        // ----------------------------------------------------------------------
        private GridViewColumnHeader FindColumnHeader(DependencyObject start, GridViewColumn gridViewColumn)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(start); i++)
            {
                Visual childVisual = VisualTreeHelper.GetChild(start, i) as Visual;
                if (childVisual is GridViewColumnHeader)
                {
                    GridViewColumnHeader gridViewHeader = childVisual as GridViewColumnHeader;
                    if (gridViewHeader.Column == gridViewColumn)
                    {
                        return gridViewHeader;
                    }
                }
                GridViewColumnHeader childGridViewHeader = FindColumnHeader(childVisual, gridViewColumn);  // recursive
                if (childGridViewHeader != null)
                {
                    return childGridViewHeader;
                }
            }
            return null;
        } // FindColumnHeader

        // ----------------------------------------------------------------------
        private void InitColumns()
        {
            GridView view = listView.View as GridView;
            if (view == null)
            {
                return;
            }

            foreach (GridViewColumn gridViewColumn in view.Columns)
            {
                if (!RangeColumn.IsRangeColumn(gridViewColumn))
                {
                    continue;
                }

                double? minWidth = RangeColumn.GetRangeMinWidth(gridViewColumn);
                double? maxWidth = RangeColumn.GetRangeMaxWidth(gridViewColumn);
                if (!minWidth.HasValue && !maxWidth.HasValue)
                {
                    continue;
                }

                GridViewColumnHeader columnHeader = FindColumnHeader(listView, gridViewColumn);
                if (columnHeader == null)
                {
                    continue;
                }

                double actualWidth = columnHeader.ActualWidth;
                if (minWidth.HasValue)
                {
                    columnHeader.MinWidth = minWidth.Value;
                    if (!double.IsInfinity(actualWidth) && actualWidth < columnHeader.MinWidth)
                    {
                        gridViewColumn.Width = columnHeader.MinWidth;
                    }
                }
                if (maxWidth.HasValue)
                {
                    columnHeader.MaxWidth = maxWidth.Value;
                    if (!double.IsInfinity(actualWidth) && actualWidth > columnHeader.MaxWidth)
                    {
                        gridViewColumn.Width = columnHeader.MaxWidth;
                    }
                }
            }
        } // InitColumns

        // ----------------------------------------------------------------------
        protected virtual void ResizeColumns()
        {
            GridView view = listView.View as GridView;
            if (view == null || view.Columns.Count == 0)
            {
                return;
            }

            // listview width
            double actualWidth = double.PositiveInfinity;
            if (scrollViewer != null)
            {
                actualWidth = scrollViewer.ViewportWidth;
            }
            if (double.IsInfinity(actualWidth))
            {
                actualWidth = listView.ActualWidth;
            }
            if (double.IsInfinity(actualWidth) || actualWidth <= 0)
            {
                return;
            }

            double resizeableRegionCount = 0;
            double otherColumnsWidth = 0;
            // determine column sizes
            foreach (GridViewColumn gridViewColumn in view.Columns)
            {
                if (ProportionalColumn.IsProportionalColumn(gridViewColumn))
                {
                    double? proportionalWidth = ProportionalColumn.GetProportionalWidth(gridViewColumn);
                    if (proportionalWidth != null)
                    {
                        resizeableRegionCount += proportionalWidth.Value;
                    }
                }
                else
                {
                    otherColumnsWidth += gridViewColumn.ActualWidth;
                }
            }

            if (resizeableRegionCount <= 0)
            {
                // no proportional columns present: commit the regulation to the scroll viewer
                if (scrollViewer != null)
                {
                    scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                }

                // search the first fill column
                GridViewColumn fillColumn = null;
                for (int i = 0; i < view.Columns.Count; i++)
                {
                    GridViewColumn gridViewColumn = view.Columns[i];
                    if (IsFillColumn(gridViewColumn))
                    {
                        fillColumn = gridViewColumn;
                        break;
                    }
                }

                if (fillColumn != null)
                {
                    double otherColumnsWithoutFillWidth = otherColumnsWidth - fillColumn.ActualWidth;
                    double fillWidth = actualWidth - otherColumnsWithoutFillWidth;
                    if (fillWidth > 0)
                    {
                        double? minWidth = RangeColumn.GetRangeMinWidth(fillColumn);
                        double? maxWidth = RangeColumn.GetRangeMaxWidth(fillColumn);

                        bool setWidth = !(minWidth.HasValue && fillWidth < minWidth.Value);
                        if (maxWidth.HasValue && fillWidth > maxWidth.Value)
                        {
                            setWidth = false;
                        }
                        if (setWidth)
                        {
                            if (scrollViewer != null)
                            {
                                scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                            }
                            fillColumn.Width = fillWidth;
                        }
                    }
                }
                return;
            }

            double resizeableColumnsWidth = actualWidth - otherColumnsWidth;
            if (resizeableColumnsWidth <= 0)
            {
                return; // missing space
            }

            // resize columns
            double resizeableRegionWidth = resizeableColumnsWidth / resizeableRegionCount;
            foreach (GridViewColumn gridViewColumn in view.Columns)
            {
                if (ProportionalColumn.IsProportionalColumn(gridViewColumn))
                {
                    double? proportionalWidth = ProportionalColumn.GetProportionalWidth(gridViewColumn);
                    if (proportionalWidth != null)
                    {
                        gridViewColumn.Width = proportionalWidth.Value * resizeableRegionWidth;
                    }
                }
            }
        } // ResizeColumns

        // --------------------