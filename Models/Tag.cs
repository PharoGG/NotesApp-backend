using System.Text.Json.Serialization;
public class Tag
{
    public Tag()
    {
        Notes = new List<Note>(); // Initialize the Notes collection in the constructor.
    }

    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public List<Note> Notes { get; set; }
}
