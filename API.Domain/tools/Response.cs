namespace API.Domain.tools;

public class Response
{
    public dynamic Data { get; set; }
    public List<String> Errors { get; protected set; }
    public Boolean IsSuccess => Errors?.Count == 0;
    public Boolean IsFailed => Errors?.Count != 0;
    
    public Response()
    {
        Errors = new List<String>();
    }
    
    public Response(dynamic data)
    {
        Data = data;
        Errors = new List<string>();
    }

    public Response AddError(String error)
    {
        Errors.Add(error);

        return this;
    }
    
    public Response AddErrors(List<String> errors)
    {
        Errors.AddRange(errors);

        return this;
    }

    public Response Failed(String error)
    {
        Response response = new Response();
        return new Response().AddError(error);
    }
}