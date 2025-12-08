using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using TqkLibrary.Avalonia.ToolKit.Collections.ObservableCollections;
using TqkLibrary.Avalonia.ToolKit.ViewModels;

namespace TqkLibrary.Avalonia.ToolKit.Interfaces.Services
{
    public interface INavigationService<TBaseViewModel>
    {
        TBaseViewModel? CurrentView { get; }
        void NavigateTo<TViewModel>() where TViewModel : TBaseViewModel;
        void NavigateTo(Type type);
    }
}
