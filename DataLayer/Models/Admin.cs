namespace DataLayer.Models;

public class Admin
{
    public int AdminId { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public bool SignedIn { get; set; }
}