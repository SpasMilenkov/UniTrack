using UniTrackBackend.Api.ViewModels;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Services.Mappings;

public interface IMapper
{
    public Mark? MapMark(MarkViewModel model);
}