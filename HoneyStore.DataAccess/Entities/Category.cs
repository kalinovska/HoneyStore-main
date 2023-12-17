using HoneyStore.DataAccess.Interfaces;

namespace HoneyStore.DataAccess.Entities
{
    public class Category : IIdentifier
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}