namespace TaskifyAPI.Exceptions;

public class EmailAlreadyExistException : Exception
{
    public EmailAlreadyExistException() : base("Email already exists"){ }
}