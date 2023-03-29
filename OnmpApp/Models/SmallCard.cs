using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnmpApp.Models;

public partial class SmallCard: ObservableObject
{
    [ObservableProperty]
    string _name;

    [ObservableProperty]
    DateTime _date;

    [ObservableProperty]
    CardType _type;
}
