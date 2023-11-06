using DataLayer.Models;

namespace DataLayer.Services;

public class AdminService : IAdminService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public AdminService(ApplicationDbContext melodyMineService)
    {
        _applicationDbContext = melodyMineService;
    }
    
    public bool LogIn(Admin admin)
    {
        Admin TempAadmin = _applicationDbContext.Admins
            .Where(a => a.Username == admin.Username)
            .Where(a => a.Password == admin.Password)
            .FirstOrDefault();
        if (TempAadmin != null)
        {
            TempAadmin.SignedIn = true;
            _applicationDbContext.SaveChanges();
            return true;
        }
        return false;
    }
    
    public bool LogOut(Admin admin)
    {
        Admin tempAadmin = _applicationDbContext.Admins
            .Where(a => a.Username == admin.Username)
            .FirstOrDefault();

        if (tempAadmin != null)
        {
            tempAadmin.SignedIn = false;
            _applicationDbContext.SaveChanges();
            return true;
        }
        return false;
    }
    
    public bool SignedIn(string username)
    {
        if (_applicationDbContext.Admins.Where(a => a.Username == username).Where(a => a.SignedIn == true).FirstOrDefault() != null)
        {
            return true;
        }
        return false;
    }
    
    public bool AnySignedIn()
    {
        Admin admin = _applicationDbContext.Admins.Where(a => a.SignedIn == true).FirstOrDefault();
        if (admin != null)
        {
            return true;
        }
        return false;
    }
}