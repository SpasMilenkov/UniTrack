using UniTrackBackend.Api.ViewModels.ResultViewModels;
using UniTrackBackend.Data.Models;

namespace UniTrackBackend.Data.Commons;

public interface IMarkRepository : IRepository<Mark>
{
    Task<List<ClassAverage>> CalculateClassAverages(int gradeId);
}