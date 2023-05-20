using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using OnmpApp.Services.Database;

namespace OnmpApp.ViewModels.Catalog;

[QueryProperty(nameof(Name), nameof(Name))]
public partial class CatalogTextViewModel : ObservableObject
{
    [ObservableProperty]
    Models.Database.Catalog _catalogElement = new();

    [ObservableProperty]
    string _name;


    public async void InitPage()
    {
        CatalogElement = await DatabaseService.CatalogGet(Name);
    }

}