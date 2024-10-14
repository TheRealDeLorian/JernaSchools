using JernaClassLib.Data.DatabaseObjects;
using Bunit;
using JernaClassLib.Components;

namespace JernaUnitTests
{
    public class ItemCardsUnitTests : TestContext
    {

        [Fact]
        public void RenderItems_WhenEvenNumberOfItems_RendersPairs()
        {
            // Arrange
            var items = new List<Item>
            {
                new Item { Id = 1, ItemName = "Item1" },
                new Item { Id = 2, ItemName = "Item2" },
                new Item { Id = 3, ItemName = "Item3" },
                new Item { Id = 4, ItemName = "Item4" },
            };


            var component = RenderComponent<ItemCards>(parameters =>
                parameters.Add(p => p.items, items));
            // Act
            var itemCards = component.FindComponents<ItemCard>();

            // Assert
            Assert.Equal(4, itemCards.Count); 
        }

      
    }
}
