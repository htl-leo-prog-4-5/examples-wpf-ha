using Microsoft.AspNetCore.Mvc.RazorPages;

using Core.Entities;

namespace WebUi.Pages.Stations
{
    using Core.Contracts;

    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _uow;

        public IndexModel(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IList<Station> Station { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Station = await _uow.StationRepository
                .GetAsync(
                    null,
                    stations => stations.OrderBy(s => s.Name),
                    nameof(Core.Entities.Station.City),
                    nameof(Core.Entities.Station.Lines));
        }
    }
}