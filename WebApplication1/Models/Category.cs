namespace BethanysPieShop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        //steht nur da, damit EF erkennt, dass Category Id als FK in Pies zu setzen ist, dient der Erstellung einer Relation
        public List<Pie>? Pies { get; set; }
    }
}
