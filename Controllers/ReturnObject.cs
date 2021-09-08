namespace MinApi.Controllers;
public class ReturnObject
{
    public ReturnObject(string content)
    {
        Content = content;
    }

    public string Content { get; set; }
}