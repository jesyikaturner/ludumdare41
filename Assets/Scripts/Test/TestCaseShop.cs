using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCaseShop : MonoBehaviour {

    public Player player;
    public Shop s;

    private List<Item> items; // keeps track of items

    private void Start()
    {

    }

    public void SetupTestShop(List<Item> itemList)
    {
        items = itemList;

        GameObject shop = new GameObject();
        shop.transform.SetParent(transform);
        s = shop.AddComponent<Shop>();

        // setting up the shop.
        s.SetupTestShop(items);
    }

    public void TestPlayerBuy()
    {
        //giving the player money to test buying.
        player.currMoney = 500;
        s.BuyItem(player, 2, 15);

        player.inv.PrintInventory();
    }

    public void TestPlayerSell()
    {
        s.SellItem(player, 2, 15);
        player.inv.PrintInventory();
    }
}
