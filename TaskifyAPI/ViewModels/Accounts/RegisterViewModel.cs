namespace TaskifyAPI.ViewModels.Accounts;

public class RegisterViewModel
{

    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    
    public RegisterViewModel(int id, string userName, string email )
    {
        Id = id;
        UserName = userName;
        Email = email;
    }

    public RegisterViewModel()
    {
        
    }
}