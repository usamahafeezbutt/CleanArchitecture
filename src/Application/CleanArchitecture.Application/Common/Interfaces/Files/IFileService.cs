
namespace CleanArchitecture.Application.Common.Interfaces.Files
{
    public interface IFileService
    {
        Task<MemoryStream> ToCSV<T>(List<T> data);
        Task<MemoryStream> ToExcel<T>(List<T> data);
    }
}
