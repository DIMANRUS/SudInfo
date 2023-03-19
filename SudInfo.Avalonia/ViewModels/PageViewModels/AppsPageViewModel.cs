using Avalonia.Controls.Models.TreeDataGrid;
using SudInfo.Avalonia.Services;
using System;
using System.IO;

namespace SudInfo.Avalonia.ViewModels.PageViewModels;
public class AppsPageViewModel : BaseRoutableViewModel
{
    private readonly AppService _appService;
    private readonly DialogService _dialogService;
    [Reactive]
    public ObservableCollection<AppEntity> Apps { get; set; }
    public HierarchicalTreeDataGridSource<AppEntity> Source { get; }
    //public ObservableCollection<Node> Items { get; }
    //public ObservableCollection<Node> SelectedItems { get; }
    //public string strFolder { get; set; }

    public AppsPageViewModel(AppService appService, DialogService dialogService)
    {
        _appService = appService;
        _dialogService = dialogService;


        //strFolder = @"C:\Users\dmitr\SurfaceDuoEmulator_API32"; // EDIT THIS FOR AN EXISTING FOLDER

        //Items = new ObservableCollection<Node>();

        //Node rootNode = new Node(strFolder);
        //rootNode.Subfolders = GetSubfolders(strFolder);

        //Items.Add(rootNode);
    }

    public async Task LoadApps()
    {
        var result = await _appService.GetApps();
        if (!result.Success)
        {
            await _dialogService.ShowMessageBox("Ошибка", $"Ошибка загрузки! Ошибка: {result.Message}", icon: Icon.Error);
            return;
        }
        Apps = new(result.Object);
    }

    //public ObservableCollection<Node> GetSubfolders(string strPath)
    //{
    //    ObservableCollection<Node> subfolders = new ObservableCollection<Node>();
    //    string[] subdirs = Directory.GetDirectories(strPath, "*", SearchOption.TopDirectoryOnly);

    //    foreach (string dir in subdirs)
    //    {
    //        Node thisnode = new Node(dir);

    //        if (Directory.GetDirectories(dir, "*", SearchOption.TopDirectoryOnly).Length > 0)
    //        {
    //            thisnode.Subfolders = new ObservableCollection<Node>();

    //            thisnode.Subfolders = GetSubfolders(dir);
    //        }

    //        subfolders.Add(thisnode);
    //    }

    //    return subfolders;
    //}

    //public class Node
    //{
    //    public ObservableCollection<Node> Subfolders { get; set; }

    //    public string strNodeText { get; }
    //    public string strFullPath { get; }

    //    public Node(string _strFullPath)
    //    {
    //        strFullPath = _strFullPath;
    //        strNodeText = Path.GetFileName(_strFullPath);
    //    }
    //}
}