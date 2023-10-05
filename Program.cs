namespace RecursiveLinq
{
    public class Item
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    public static class RecursiveLinqQuery
    {
        public static IEnumerable<Item> GetChildren(List<Item> items, int parentId)
        {
            return items.Where(item => item.Id == parentId)
                        .Concat(items.Where(item => item.ParentId == parentId)
                        .SelectMany(item => GetChildren(items, item.Id)))
                        .OrderBy(item => item.ParentId);
        }
    }


internal class Program
    {
        static void Main(string[] args)
        {
            // Пример использования
            var items = new List<Item>
            {
                new Item { Id = 1, ParentId = 0, Name = "Item 1" },
                new Item { Id = 2, ParentId = 1, Name = "Item 2" },
                new Item { Id = 3, ParentId = 1, Name = "Item 3" },
                new Item { Id = 4, ParentId = 2, Name = "Item 4" },
                new Item { Id = 5, ParentId = 3, Name = "Item 5" },
            };

            int rootId = 1;
            var result = RecursiveLinqQuery.GetChildren(items, rootId);

            foreach (var item in result)
            {
                Console.WriteLine($"Id: {item.Id}, ParentId: {item.ParentId}, Name: {item.Name}");
            }
        }

    }


}