using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GongSolutions.Wpf.DragDrop;

namespace AnyListen.DragDrop.DropTargetAdorners
{
    public class DropTargetInsertionAdorner : DropTargetAdorner
    {
        public DropTargetInsertionAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            var itemsControl = this.DropInfo.VisualTarget as ItemsControl;

            if (itemsControl != null)
            {
                // Get the position of the item at the insertion index. If the insertion point is
                // to be after the last item, then get the position of the last item and add an 
                // offset later to draw it at the end of the list.
                ItemsControl itemParent;

                if (this.DropInfo.VisualTargetItem != null)
                {
                    itemParent = ItemsControl.ItemsControlFromItemContainer(this.DropInfo.VisualTargetItem);
                }
                else
                {
                    itemParent = itemsControl;
                }

                var index = Math.Min(this.DropInfo.InsertIndex, itemParent.Items.Count - 1);

                var lastItemInGroup = false;
                var targetGroup = this.DropInfo.TargetGroup;
                if (targetGroup != null && targetGroup.IsBottomLevel && this.DropInfo.InsertPosition.HasFlag(RelativeInsertPosition.AfterTargetItem))
                {
                    var indexOf = targetGroup.Items.IndexOf(this.DropInfo.TargetItem);
                    lastItemInGroup = indexOf == targetGroup.ItemCount - 1;
                    if (lastItemInGroup && this.DropInfo