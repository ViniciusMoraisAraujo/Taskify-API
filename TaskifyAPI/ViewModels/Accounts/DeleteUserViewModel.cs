namespace TaskifyAPI.ViewModels.Accounts;

public class DeleteUserViewModel
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    
    public DeleteUserViewModel(int id, string userName, string email )
    {
        Id = id;
        UserName = userName;
        Email = email;
    }

    public DeleteUserViewModel()
    {
        
    }
}