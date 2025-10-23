using Core.Contracts;
using Core.Entities.Visitors;

using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using WpfMvvmBase;

namespace WpfTadeotAdmin.ViewModels;

public class MainViewModel : BaseViewModel
{
    public ObservableCollection<Visitor> Visitors { get; set; } = new ObservableCollection<Visitor>();


    private int _visitorsCount;

    public int VisitorsCount
    {
        get => _visitorsCount;
        set
        {
            _visitorsCount = value;
            OnPropertyChanged();
            CommandManager.InvalidateRequerySuggested();
        }
    }

    private IUnitOfWork _uow;

    public MainViewModel(IUnitOfWork uow)
    {
        _uow = uow;
    }

    private async Task DownloadCsvAsync()
    {
        var visitors = await _uow.Visitors
            .GetAsync(null,
                v => v.OrderBy(v => v.DateTime),
                nameof(Visitor.City));
        var lines = visitors
            .Select(visitor =>
                visitor.Id + ";"
                           + visitor.DateTime.ToShortDateString() + ";"
                           + visitor.DateTime.ToShortTimeString() + ";"
                           + visitor.Adults + ";"
                           + visitor.InterestHIF + ";"
                           + visitor.InterestHITM + ";"
                           + visitor.InterestHBG + ";"
                           + visitor.InterestHEL + ";"
                           + visitor.InterestFEL + ";"
                           + visitor.IsMale + ";"
                           + visitor.City!.Name + ";"
                           + visitor.City!.ZipCode + ";"
                           + visitor.Comment + ";"
                           + visitor.ReasonForVisit + ";"
                           + visitor.SchoolType + ";"
                           + visitor.SchoolLevel)
            .Aggregate((l1, l2) => l1 + "\n" + l2);

        lines = "id; date; time; adults; interestINF; interestHITM; interestHEL; interestHBG; interestFEL; isMale; city; zipCode; comment; reasonForVisit; schoolType; schoolLevel\n" + lines;

        var csvFilename = Controller?.AskSaveToCsvFile();
        if (csvFilename != null)
        {
            File.WriteAllText(csvFilename, lines);
        }
    }


    public async Task LoadDataAsync()
    {
        Visitors.Clear();
        var visitors = await _uow.Visitors.GetAllUntrackedAsync();
        foreach (var v in visitors)
        {
            Visitors.Add(v);
        }

        VisitorsCount = Visitors.Count;
    }
}