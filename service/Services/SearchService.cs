using infrastructure.DataModels;
using infrastructure.Repositories;

namespace service.Services;

public class SearchService
{
    private readonly SearchRepository _searchRepository;

    public SearchService(SearchRepository searchRepository)
    {
        _searchRepository = searchRepository;
    }

    public IEnumerable<AvatarModel> SearchAvatar(string searchQuery)
    {
        return _searchRepository.SearchAvatar(searchQuery);
    }
}
