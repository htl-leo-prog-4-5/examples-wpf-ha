using System.Collections.ObjectModel;
using AvgCalc.Contract;
using AvgCalc.Service.Core;
using Microsoft.AspNetCore.Mvc;

namespace AvgCalc.Service.Controllers
{
    public class CalculationController : Controller
    {
        [HttpPost]
        [Route("api/[controller]/calculate")]
        public CalculationResponse Calculate([FromBody] CalculationRequest request)
        {
            var mgr = new CalculationManager();
            var calcRes = mgr.CalculateAverage(new ReadOnlyCollection<int>(request.Numbers),
                request.IncludeDuplicates);
            return new CalculationResponse
            {
                IncludeDuplicates = request.IncludeDuplicates,
                Result = calcRes
            };
        }
    }
}