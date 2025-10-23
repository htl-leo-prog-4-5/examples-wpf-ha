using System;
using System.Net.Http;
using System.Threading.Tasks;
using AvgCalc.Contract;
using JetBrains.Annotations;

namespace GradeCalc.Core.AvgCalc
{
    /// <inheritdoc/>
    [UsedImplicitly]
    internal sealed class AvgCalcProxy : IAvgCalcProxy
    {
        private const string CONTROLLER_URL = "http://localhost:5000/api/calculation";
        private const string ACTION = "Calculate";
        private readonly HttpClient _client;

        /// <summary>
        ///     default ctor
        /// </summary>
        public AvgCalcProxy([NotNull] HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        /// <inheritdoc/>
        public async Task<CalculationResponse> RemoteCalcAvg(CalculationRequest request)
        {
            var fullUrl = $"{CONTROLLER_URL}/{ACTION}";
            var response = await _client.PostAsJsonAsync(fullUrl, request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsAsync<CalculationResponse>();
            return responseContent;
        }
    }
}