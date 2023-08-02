using System.Text.Json.Serialization;
public class NoteTag
{
    public int NoteId { get; set; }
    [JsonIgnore]
    public Note Note { get; set; }

    public int TagId { get; set; }
    public Tag Tag { get; set; }
}