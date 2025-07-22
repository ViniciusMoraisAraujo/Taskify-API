namespace TaskifyAPI.ViewModels;

public class ResultViewModel<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public List<string> Errors { get; private set; } = new();

    public ResultViewModel(bool success, string message , T data = default)
    {
        Success = success;
        Message = message;
        Errors = new List<string>();
        Data = data;
    }
}