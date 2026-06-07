namespace Meet.Application.HandleResponse;

public class Response<T>
{
    public string Message { get; set; }
    public int Status { get; set; }
    public List<T> Data { get; set; }

    public Response(string message, int status)
    {
        Message = message;
        Status = status;
    }
    
    public Response(string message, int status, List<T> data)
    {
        Message = message;
        Status = status;
        Data = data;
    }
}