using DataLayer.Models;

namespace DataLayer.Services;

public interface IRecordLabelService
{
    public IQueryable<RecordLabel> GetRecordLabelById(int recordLabelId);
    public RecordLabel GetRecordLabelByIdSimple(int recordLabelId);
    public IQueryable<RecordLabel> GetAllRecordLabels();
    public void CreateRecordLabel(RecordLabel recordLabel);
    public void DeleteRecordLabel(RecordLabel recordLabel);
}