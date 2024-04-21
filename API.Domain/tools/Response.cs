namespace API.Domain.tools;

public class Response
{
    public dynamic Data { get; set; }
    public List<String> Errors { get; set; }
    public Boolean IsSuccess => Errors.Count == 0;
    public Boolean IsFailed => Errors.Count != 0;

    public Response()
    {
        
    }

    public Response(dynamic data)
    {
        Data = data;
    }

    public Response AddError(String error)
    {
        Errors.Add(error);

        return this;
    }

    public Response Failed(String error)
    {
        Response response = new Response();
        return new Response().AddError(error);
    }
}