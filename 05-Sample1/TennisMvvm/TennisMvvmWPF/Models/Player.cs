using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TennisMvvmWPF.Models;

class Player
{
    public string Name { get; set; }
    public int Points { get; set; }

    public ObservableCollection<Results> Results { get; set; } = new ObservableCollection<Models.Results>();
}