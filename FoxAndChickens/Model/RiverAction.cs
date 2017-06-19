using System.Collections.Generic;

namespace FoxAndChickens.Model
{
    public class RiverAction
    {
        public Item Item { get; set; }
        public RiverActionName ActionName { get; set; }
        public IList<Item> LeftItems { get; set; }
        public IList<Item> CrossedItem { get; set; }
    }

    public enum RiverActionName
    {
        Cross,
        Back
    }
}
