public class Note
{
    public Note()
    {
        Tags = new List<Tag>(); // Initialize the Tags collection in the constructor.
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTimeOffset Reminder { get; set; }
    public List<Tag> Tags { get; set; }
}
