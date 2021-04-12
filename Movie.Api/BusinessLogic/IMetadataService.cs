using Movie.Api.Models;
using System.Collections.Generic;

namespace Movie.Api.BusinessLogic
{
    public interface IMetadataService
    {
        IEnumerable<MetadataViewModel> GetMetadatas(int movieId);

        MetadataViewModel GetMetadata(int movieId);

        MetadataViewModel SaveMetadata(MetadataViewModel metadataViewModel);
    }
}
