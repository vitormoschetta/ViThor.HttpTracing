namespace ViThor.HttpTracing.Sample.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsComplete { get; set; }
    }
}