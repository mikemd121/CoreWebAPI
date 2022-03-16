using CoreWebAPI.Core;
using System.Collections.Generic;

namespace CoreWebAPI.Business
{
    public interface IPopulationDataService
    {
        /// <summary>
        /// Gets the current location asynchronous.
        /// </summary>
        /// <param name="state">The state.</param>
        /// <returns></returns>
        List<Response> GetCurrentLocationAsync(int[] state);
    }
}
