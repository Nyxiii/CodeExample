// -- FILE ------------------------------------------------------------------
// name       : RangeColumn.cs
// created    : Jani Giannoudis - 2008.03.27
// language   : c#
// environment: .NET 3.0
// copyright  : (c) 2008-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;

namespace AnyListen.GUI.Extensions.ListViewLayoutManager
{

    // ------------------------------------------------------------------------
    public sealed class RangeColumn : LayoutColumn
    {

        // ----------------------------------------------------------------------
        public static readonly DependencyProperty MinWidthProperty =
            DependencyProperty.RegisterAttached(
                "MinWidth",
                typeof(double),
                typeof(RangeColumn));

        // ----------------------------------------------------------------------
        public static readonly DependencyProperty MaxWidthProperty =
            DependencyProperty.RegisterAttached(
                "MaxWidth",
                typeof(double),
                typeof(RangeColumn));

        // ----------------------------------------------------------------------
        public static readonly DependencyProperty IsFillColumnProperty =
            DependencyProperty.RegisterAttached(
                "IsFillColumn",
                typeof(bo