using Core.Contracts;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMvvmBase;

namespace WpfTadeotAdmin.ViewModels
{
    public class RegistrationConfigViewModel : BaseViewModel
    {
        private IUnitOfWork _uow;

        private string? _dbReasons;
        private string? _reasonsText;
        public string? ReasonsText
        {
            get => _reasonsText;
            set
            {
                _reasonsText = value;
                OnPropertyChanged();
            }
        }

        private string? _dbTypes;
        private string? _typesText;
        public string? TypesText
        {
            get => _typesText;
            set
            {
                _typesText = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand UpdateReasons { get; set; }
        public RelayCommand UpdateTypes { get; set; }
        public RelayCommand CloseWindow { get; set; }
        public RegistrationConfigViewModel() : this(null, new UnitOfWork())
        {

        }
        public RegistrationConfigViewModel(IWindowController? controller, IUnitOfWork uow) : base(controller)
        {
            _uow = uow;
            UpdateReasons = new RelayCommand(
                async _ => await SaveReasonsAsync(),
                _ => _dbReasons != ReasonsText);
            UpdateTypes = new RelayCommand(
                async _ => await SaveTypesAsync(),
                _ => _dbTypes != TypesText);
            CloseWindow = new RelayCommand
            (
                _ => Controller!.CloseWindow(),
                _ => true
            );
        }

        public async Task LoadDataAsync()
        {
            await LoadReasonsAsync();

            await LoadSchoolTypesAsync();
        }

        private async Task LoadSchoolTypesAsync()
        {
            var types = await _uow.SchoolTypes.GetAsync(null,
                 r => r.OrderBy(type => type.Rank));
            var texts = types.Select(t => t.Type).ToArray();
            _dbTypes = String.Join('\n', texts);
            TypesText = _dbTypes;
        }

        private async Task LoadReasonsAsync()
        {
            var reasons = await _uow.ReasonsForVisit
                .GetAsync(null,
    r => r.OrderBy(reason => reason.Rank));
            var texts = reasons.Select(r => r.Reason).ToArray();

            _dbReasons = string.Join('\n', texts);
            ReasonsText = _dbReasons;
        }

        private async Task SaveTypesAsync()
        {
            var types = TypesText!.Split('\n');
            await _uow.SchoolTypes.UpdateAllAsync(types);
            await _uow.SaveChangesAsync();
            await LoadSchoolTypesAsync();
        }

        private async Task SaveReasonsAsync()
        {
            var reasons = ReasonsText!.Split('\n');
            await _uow.ReasonsForVisit.UpdateAllAsync(reasons);
            await _uow.SaveChangesAsync();
            await LoadReasonsAsync();
        }
    }
}
