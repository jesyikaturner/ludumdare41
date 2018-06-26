using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    /*
     * Populates shop with possible stock. Stocks shop inventory.
     * Clears stock and restocks. Handles selling and buying.
     */

    private List<Item> items; // A copy of the item list database.
    public List<ItemContainer> possibleStock; // a list of items that can be chosen to be added to the inventory
    public InventoryManagement inv; // the shop's current inventory that is stocked.

    public Shop()
    {
        inv = new InventoryManagement(ConstantVariables.INVENTORYSLOTS);
        possibleStock = new List<ItemContainer>();
    }

    public void SetupShop(List<Item> itemList)
    {
        items = itemList;
        CreatePossibleStock();
        Stock();
        inv.PrintInventory();
    }

    // player selling an item - takes an reference of the player to access their variables
    public bool SellItem(Player p, int id, int amount)
    {
        // If the player is trying to sell key items, don't let them!
        if (p.inv.FindItemById(id).Price == 0)
        {
            Debug.Log("GUI - Tell the player they were unable to sell the item.");
            return false;
        }

        if(p.inv.RemoveQuantity(id,amount))
        {
            // Sell the item for half it's price.
            p.currMoney += inv.FindItemById(id).Price/2;
            // Add that item quantity to the shop.
            inv.AddQuantity(id, amount);
            Debug.Log("GUI - Tell player they sold the items.");
            return true;
        }else{
            Debug.Log("GUI - Tell the player they were unable to sell the item.");
            return false;
        }
    }

    // player buying an item - takes an reference of the player to access their variables
    public bool BuyItem(Player p, int id, int amount)
    {
        // work out how much it costs then compare with player's money
        // if the total is greater than the player's money, don't do anything.
        int total = inv.FindItemById(id).Price * amount;
        if (total > p.currMoney)
        {
            Debug.Log(string.Format("Couldn't buy {0} amount of item id: {1}. Player doesn't have enough money!",amount,id));
            return false;
        }

        if (inv.RemoveQuantity(id, amount))
        {

            ItemContainer item = inv.FindItemById(id);
            p.inv.AddQuantityByContainer(item, amount);
            p.currMoney -= total;
            return true;
        }
        Debug.Log(string.Format("Couldn't buy {0} amount of item id: {1}. Shop doesn't have enough in stock!", amount, id));
        return false;
    }

    /* 
     * Seperates possible stock into regular and special items.
     * Chooses one special item and four regular. Adds them to
     * shop inventory.
    */
    private void Stock()
    {
        List<ItemContainer> regular = new List<ItemContainer>();
        List<ItemContainer> special = new List<ItemContainer>();

        foreach(ItemContainer ic in possibleStock)
        {
            if(ic.Price >= 50)
            {
                special.Add(ic);
                //Debug.Log("Special" + ic.PrintToString());
            }
            else
            {
                regular.Add(ic);
                //Debug.Log("Regular" + ic.PrintToString());
            }
        }

        // add 1 random special to inventory
        RandomAdd(special,1);

        // add 4 random regular to inventory
        RandomAdd(regular,4);
    }

    // Randomly picks items from the list and adds them to the inventory.
    private void RandomAdd(List<ItemContainer> itemToAdd, int howMany){
        int random = Random.Range(0,itemToAdd.Count);
        for(int i = 0; i < howMany; i++)
        {
            //  Picks a random item
            ItemContainer temp = itemToAdd[random];

            /*
             *  If the item is already in the inventory, then randomly pick another item
             *  until its an item that doesn't exist in the inventory already.
             */
            while(inv.FindItemByContainer(temp) != null)
            {
                random = Random.Range(0,itemToAdd.Count);
                temp = itemToAdd[random];
                if(inv.FindItemByContainer(temp) == null)
                    break;
            }

            //  Adds the item to the inventory.
            inv.AddItem(itemToAdd[random]);
        }
    }

    // Clears inventory then adds more stock.
    public void Restock()
    {
        inv.RemoveAll();
        Stock();
    }
     
    // adding quantities to the stock
    private void CreatePossibleStock()
    {
        foreach(Item item in items)
        {
            if(item.Price < 50 && item.Price > 0)
            {
                possibleStock.Add(UtilityMethods.createItemContainer(item,Random.Range(20,40)));
            }
            else if (item.Price >= 50)
            {
                possibleStock.Add(UtilityMethods.createItemContainer(item,Random.Range(1,5)));
            }
        }
    }

    /*
     * ------------
     * TESTING SHOP
     * ------------
     */ 

    // setting up testing shop.
    public void SetupTestShop(List<Item> itemList)
    {
        items = itemList;
        CreatePossibleStock();
        SetStock();
        inv.PrintInventory();
    }

    // Hard sets stocks for the shop - for testing.
    public void SetStock()
    {
        for(int i = 0; i < 5; i++)
        {
            inv.AddItem(possibleStock[i]);
        }
    }
}
