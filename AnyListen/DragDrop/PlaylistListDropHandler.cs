using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using GongSolutions.Wpf.DragDrop;
using AnyListen.Music.Playlist;
using AnyListen.Music.Track;
using AnyListen.ViewModels;
using DropTargetInsertionAdorner = AnyListen.DragDrop.DropTargetAdorners.DropTargetInsertionAdorner;

namespace AnyListen.DragDrop
{
    class PlaylistListDropHandler : IDropTarget
    {
        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            if (((dropInfo.Data is PlayableBase || dropInfo.Data is IEnumerable<PlayableBase>) && dropInfo.TargetItem is IPlaylist && dropInfo.DragInfo.SourceCollection != Mai