using DataLayer.Models;

namespace DataLayer.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _ApplicationDbContext;

    public AdminService(ApplicationDbContext melodyMineService)
    {
        _ApplicationDbContext = melodyMineService;
    }
    
    public bool LogIn(Admin admin)
    {
        Admin TempAadmin = _ApplicationDbContext.Admins
            .Where(a => a.Username == admin.Username)
            .Where(a => a.Password == admin.Password)
            .FirstOrDefault();
        if (TempAadmin != null)
        {
            TempAadmin.SignedIn = true;
            _ApplicationDbContext.SaveChanges();
            return true;
        }
        return false;
    }
    
    public bool LogOut(Admin admin)
    {
        Admin tempAadmin = _ApplicationDbContext.Admins
            .Where(a => a.Username == admin.Username)
            .FirstOrDefault();

        if (tempAadmin != null)
        {
            tempAadmin.SignedIn = false;
            _ApplicationDbContext.SaveChanges();
            return true;
        }
        return false;
    }
    
    public bool SignedIn(string username)
    {
        if (_ApplicationDbContext.Admins.Where(a => a.Username == username).Where(a => a.SignedIn == true).FirstOrDefault() != null)
        {
            return true;
        }
        return false;
    }
    
    public bool AnySignedIn()
    {
        Admin admin = _ApplicationDbContext.Admins.Where(a => a.SignedIn == true).FirstOrDefault();
        if (admin != null)
        {
            return true;
        }
        return false;
    }
}