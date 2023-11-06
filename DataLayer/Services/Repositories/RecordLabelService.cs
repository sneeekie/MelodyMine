using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Services;

public class RecordLabelService : IRecordLabelService
{
    private readonly ApplicationDbContext _ApplicationDbContext;

    public RecordLabelService(ApplicationDbContext melodyMineService)
    {
        _ApplicationDbContext = melodyMineService;
    }
    
    public IQueryable<RecordLabel> GetRecordLabelById(int recordLabelId)
    {
        IQueryable<RecordLabel> tempRecordLabel = _ApplicationDbContext.RecordLabels
            .Include(i => i.Address)
            .Include(i => i.Vinyls)
            .Where(mId => mId.RecordLabelId == recordLabelId);

        return tempRecordLabel;
    }
    
    public RecordLabel GetRecordLabelByIdSimple(int recordLabelId)
    {
        RecordLabel tempRecordLabel = _ApplicationDbContext.RecordLabels
            .Where(mId => mId.RecordLabelId == recordLabelId)
            .FirstOrDefault();


        return tempRecordLabel;
    }
    
    public IQueryable<RecordLabel> GetAllRecordLabels()
    {
        return _ApplicationDbContext.RecordLabels;
    }
    
    public void CreateRecordLabel(RecordLabel recordLabel)
    {
        _ApplicationDbContext.RecordLabels.Add(recordLabel);
        _ApplicationDbContext.SaveChanges();
    }
    
    public void DeleteRecordLabel(RecordLabel recordLabel)
    {
        var existingRecordLabel = _ApplicationDbContext.RecordLabels.Find(recordLabel.RecordLabelId);
        if (existingRecordLabel != null)
        {
            _ApplicationDbContext.RecordLabels.Remove(existingRecordLabel);
            _ApplicationDbContext.SaveChanges();
        }
    }
}