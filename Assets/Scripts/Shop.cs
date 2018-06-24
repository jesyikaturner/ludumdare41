using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    /*
     * Populates shop with possible stock. Stocks shop inventory.
     * Clears stock and restocks. Handles selling and buying.
     */

    private List<Item> items; // A copy of the item list database.
    public List<ItemContainer> possibleStock;
    public Inventory inv;

    public Shop(){
        inv = new Inventory(ConstantVariables.INVENTORYSLOTS);
        possibleStock = new List<ItemContainer>();
    }

    // Public setter for the item list database, so the shop pull from the list to populate the store.
    public void SetItemList(List<Item> itemList)
    {
        items = itemList;
    }

    // player selling an item - takes an reference of the player to access their variables
    public bool SellItem(Player p, int id, int amount){
        if(p.inv.RemoveQuantity(id,amount)){
            // Sell the item for half it's price.
            p.currMoney += inv.FindItemById(id).Price/2;
            // Add that item quantity to the shop.
            inv.AddQuantity(id, amount);
            Debug.Log("GUI - Tell player they sold the items.");
            return true;
        }else{
            Debug.Log("GUI - Tell the player they were unable to sell the items");
            return false;
        }
    }

    // player buying an item - takes an reference of the player to access their variables
    public bool BuyItem(Player p, int id){
        // TODO
        return false;
    }

    /* 
     * Seperates possible stock into regular and special items.
     * Chooses one special item and four regular. Adds them to
     * shop inventory.
    */
    public void Stock(){
        List<ItemContainer> regular = new List<ItemContainer>();
        List<ItemContainer> special = new List<ItemContainer>();

        foreach(ItemContainer ic in possibleStock){
            if(ic.Price >= 50){
                special.Add(ic);
                //Debug.Log("Special" + ic.PrintToString());
            }else{
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
        for(int i = 0; i < howMany; i++){
            //  Picks a random item
            ItemContainer temp = itemToAdd[random];

            /*
             *  If the item is already in the inventory, then randomly pick another item
             *  until its an item that doesn't exist in the inventory already.
             */
            while(inv.FindItemByContainer(temp) != null){
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
    public void Restock(){
        inv.RemoveAll();
        Stock();
    }
        
    public void CreatePossibleStock(){
        foreach(Item item in items){
            if(item.Price < 50 && item.Price > 0){
                possibleStock.Add(UtilityMethods.createItemContainer(item,Random.Range(20,40)));
            }else if (item.Price >= 50){
                possibleStock.Add(UtilityMethods.createItemContainer(item,Random.Range(1,5)));
            }
        }
    }

    // testing iventory management.
    public void TESTCASE(){
        inv.RemoveItem(1);
        inv.AddQuantity(2,50);
        inv.PrintInventory();
    }
}
