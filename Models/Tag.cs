using System.Text.Json.Serialization;
public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]
    public List<Note> Notes { get; set; }
}