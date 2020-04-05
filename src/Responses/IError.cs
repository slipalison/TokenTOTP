namespace Responses
{
    public interface IError
    {
        string Code { get; set; }
        string Message { get; set; }
        LayerEnum Layer { get; set; }
        string ApplicationName { get; set; }
    }
}