using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmpApp.Models;

// Короткое описание медицинской карты
public partial class PreviewCard: ObservableObject
{

    [ObservableProperty]
    int _id;

    [ObservableProperty]
    string _name;

    [ObservableProperty]
    DateTime _date;

    [ObservableProperty]
    CardType _type;

    [ObservableProperty]
    string _comment;
}
