﻿// -- FILE ------------------------------------------------------------------
// name       : ProportionalColumn.cs
// created    : Jani Giannoudis - 2008.03.27
// language   : c#
// environment: .NET 3.0
// copyright  : (c) 2008-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

using System.Windows;
using System.Windows.Controls;

namespace AnyListen.GUI.Extensions.ListViewLayoutManager
{

    // ------------------------------------------------------------------------
    public sealed class ProportionalColumn : LayoutColumn
    {

        // ----------------------------------------------------------------------
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.RegisterAttached(
                "Width",
                typeof(double),
                typeof(ProportionalColumn));

        // ----------------------------------------------------------------------
        private ProportionalColumn()
        {
        } // ProportionalColumn

        // ------------------------------------