using DataLayer.Models;

namespace DataLayer.Services;

public interface IAdminService
{
    public bool LogIn(Admin admin);
    public bool LogOut(Admin admin);
    public bool SignedIn(string username);
    public bool AnySignedIn();
}