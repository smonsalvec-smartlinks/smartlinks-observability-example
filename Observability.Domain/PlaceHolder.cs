namespace Observability.Domain;

public class PlaceHolder( int id, int userId, string title, string body )
{
    public int Id { get; private set; } = id;
    public int UserId { get; private set; } = userId;
    public string Title { get; private set; } = title;
    public string Body { get; private set; } = body;
}
