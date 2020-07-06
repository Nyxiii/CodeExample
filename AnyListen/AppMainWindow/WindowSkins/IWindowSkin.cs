using System;
using System.Windows.Input;

namespace AnyListen.AppMainWindow.WindowSkins
{
    public interface IWindowSkin
    {
        event EventHandler DragMoveStart;
        event Event