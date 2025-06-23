namespace Entities
{
    public class TaskModel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Category { get; set; }
        public required DateOnly Date {  get; set; }
        public required TimeOnly Time { get; set; }
        public bool Completed { get; set; }
    }
}
