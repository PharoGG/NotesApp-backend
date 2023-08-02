public class Note
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTimeOffset Reminder { get; set; }
    public List<Tag>? Tags { get; set; }
}