using AutoMapper;
using CoreWebAPI.Core;
using CoreWebAPI.Model;
using CoreWebAPI.Repository;
using System.Collections.Generic;
using System.Linq;

namespace CoreWebAPI.Business
{
    /// <summary>
    /// Population data service.
    /// </summary>
    /// <seealso cref="CoreWebAPI.Business.IPopulationDataService" />
    public class PopulationDataService : IPopulationDataService
    {
        /// <summary>
        /// The mapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// The unit of work
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="PopulationDataService"/> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public PopulationDataService(
            IMapper mapper, 
            IUnitOfWork unitOfWork)
        {
            this._mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the current location asynchronous.
        /// </summary>
        /// <param name="stateIdCollection">The state identifier collection.</param>
        /// <returns></returns>
        public List<Response> GetCurrentLocationAsync(int[] stateIdCollection)
        {
            var populationList = new List<Response>();
            foreach (var state in stateIdCollection)
            {
                var actualResult = _unitOfWork.GetRepository<Actual>().Get(b => b.State == state).FirstOrDefault();
                if (actualResult != null)
                {
                    var populationResponse = _mapper.Map<Response>(actualResult);
                    populationList.Add(populationResponse);
                }
                else
                {
                    var sumOfEstimatedPopulation = _unitOfWork.GetRepository<Estimate>().Get(b => b.State == state).GroupBy(x => x.State).
                        Select(filter => filter.Sum(c => c.EstimatesPopulation)).FirstOrDefault();
                    var populationResponse = _mapper.Map<Response>(sumOfEstimatedPopulation);
                    populationResponse.State = state;
                    populationList.Add(populationResponse);
                }
            }
            return populationList;
        }
    }
}
