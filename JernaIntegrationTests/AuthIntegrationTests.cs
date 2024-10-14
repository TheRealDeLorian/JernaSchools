using FluentAssertions;
using JernaClassLib.Data;
using JernaClassLib.Data.DatabaseObjects;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace JernaIntegrationTests;

public class AuthIntegrationTests : IClassFixture<JernaWebAppFactory>
{

    JernaWebAppFactory _factory;
    public AuthIntegrationTests(JernaWebAppFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public void WebAppFactoryIsSetUp()
    {
        Assert.True(true);
    }

    //[Fact]
    //public async Task CreateUser()
    //{
    //    var client = _factory.CreateClient();
    //    var user = await client.GetFromJsonAsync<UserDTO>("api/authentication/generate_random_user");
    //    user.Should().NotBeNull();
    //}

    //[Fact]
    //public async Task GetCartItems()
    //{
    //    var client = _factory.CreateClient();
    //    var user = await client.GetFromJsonAsync<UserDTO>("api/authentication/generate_random_user");
    //    var items = await client.PostAsJsonAsync("api/cart/all", user!.User.Id);
    //    items.Should().NotBeNull();
    //}

    //[Fact]
    //public async Task GetItems()
    //{
    //    var client = _factory.CreateClient();
    //    var user = await client.GetFromJsonAsync<UserDTO>("api/authentication/generate_random_user");
    //    var items = await client.GetFromJsonAsync<Item>("api/item/all");
    //    items.Should().NotBeNull();
    //}

    //[Fact]
    //public async Task CreateUserGetItemsChangeItems()
    //{
    //    var client = _factory.CreateClient();
    //    var user = await client.GetFromJsonAsync<UserDTO>("api/authentication/generate_random_user");
    //    var allItems = await client.GetFromJsonAsync<List<Item>>("api/item/all");
    //    await client.PostAsJsonAsync("api/cart/add-item", new AddThisToCart() { ItemId = allItems![0].Id, UserId = user!.User.Id, Amount = 1 });
    //    var response = await client.PostAsJsonAsync("api/cart/all", user!.User.Id);
    //    var cartItems = await response.Content.ReadFromJsonAsync<List<CartItem>>();

    //    if (cartItems is null)
    //    {
    //        cartItems.Should().NotBeNull();
    //        return;
    //    }

    //    cartItems.Should().Contain(ci => ci.ItemId == allItems[0].Id);
    //}
}
