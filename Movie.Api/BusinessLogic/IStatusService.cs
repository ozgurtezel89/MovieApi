using Movie.Api.Models;
using System.Collections.Generic;

namespace Movie.Api.BusinessLogic
{
    public interface IStatusService
    {
        IEnumerable<StatViewModel> GetViewingStatistics();
    }
}
