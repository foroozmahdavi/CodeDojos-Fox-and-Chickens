using System.Collections.Generic;
using System.Linq;

namespace FoxAndChickens.Model
{
    public class Game
    {
        public IList<Item> CrossedItems { get; set; }
        public IList<Item> LeftItems { get; set; }
        public Queue<RiverAction> Actions { get; set; }

        public Game()
        {
            CrossedItems = new List<Item>();
            Actions = new Queue<RiverAction>();
        }

        public void Play()
        {
            var isCrossed = Cross();

            while (LeftItems.Any() && isCrossed)
            {
                GoBack();
                isCrossed = Cross();
            }
        }

        public bool Cross()
        {
            var item = LeftItems.FirstOrDefault(i => IsListSafeToRemoveItem(LeftItems, i));

            if (item == null)
                return false;

            LeftItems.Remove(item);
            CrossedItems.Add(item);

            Actions.Enqueue(new RiverAction
            {
                Item = item,
                ActionName = RiverActionName.Cross,
                LeftItems = LeftItems.ToList(),
                CrossedItem = CrossedItems.ToList()
            });

            return true;
        }

        public void GoBack()
        {
            var backItem = GetNotSafeToLeaveItem();

            Actions.Enqueue(new RiverAction
            {
                Item = backItem,
                ActionName = RiverActionName.Back,
                LeftItems = LeftItems.ToList(),
                CrossedItem = CrossedItems.ToList()
            });

        }

        private bool IsListSafeToRemoveItem(IList<Item> paramItems, Item item)
        {
            var items = paramItems.Where(x => x != item).ToList();

            return items.All(i => items.All(x => x.Food == null || x.Food.Name != i.Name));
        }

        private Item GetNotSafeToLeaveItem()
        {
            Item backItem = null;

            if (CrossedItems.Any(i => CrossedItems.Any(x => x.Food != null && x.Food.Name == i.Name)))
            {
                backItem = CrossedItems.FirstOrDefault(i => IsListSafeToRemoveItem(CrossedItems, i));
                CrossedItems.Remove(backItem);
                LeftItems.Add(backItem);
            }

            return backItem;
        }
    }
}
