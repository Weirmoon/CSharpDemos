namespace DataLib.Model;

public class TestCategoryDTO
{
    public Guid _id { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int Version { get; set; }
}