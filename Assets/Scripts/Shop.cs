using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    /*
     * Populates shop with possible stock. Stocks shop inventory.
     * Clears stock and restocks. Handles selling and buying.
     */

    public List<Item> items; // A copy of the item database.
    public List<ItemContainer> possibleStock;
    public Inventory inv;

    public Shop(){
        inv = new Inventory(ConstantVariables.INVENTORYSLOTS);
        possibleStock = new List<ItemContainer>();

    }

    // player selling an item - takes an reference of the player to access their variables
    public bool SellItem(Player p, int id, int amount){
        if(p.inv.RemoveItem(id,amount)){
            p.currMoney += inv.findItem(id).price/2;
            inv.addItem(id, amount);
            Debug.Log("Tell player they sold the items.");
            return true;
        }else{
            Debug.Log("Tell the player they were unable to sell the items");
            return false;
        }
    }

    // player buying and item - takes an reference of the player to access their variables
    public void BuyItem(Player p, int id){
        // TODO
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
            if(ic.price >= 50){
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

    private void RandomAdd(List<ItemContainer> itemToAdd, int howMany){
        int random = Random.Range(0,itemToAdd.Count);
        for(int i = 0; i < howMany; i++){
            ItemContainer temp = itemToAdd[random];

            while(inv.findItem(temp) != null){
                random = Random.Range(0,itemToAdd.Count);
                temp = itemToAdd[random];
                if(inv.findItem(temp) == null)
                    break;
            }

            inv.addItem(itemToAdd[random]);

        }
    }

    // Clears inventory then adds more stock.
    public void Restock(){
        inv.ClearInventory();
        Stock();
    }
        
    public void CreatePossibleStock(){
        foreach(Item item in items){
            if(item.price < 50 && item.price > 0){
                possibleStock.Add(UtilityMethods.createItemContainer(item,Random.Range(20,40)));
            }else if (item.price >= 50){
                possibleStock.Add(UtilityMethods.createItemContainer(item,Random.Range(1,5)));
            }
        }
    }

    // testing iventory management.
    public void TESTCASE(){
        inv.RemoveItem(1);
        inv.addItem(2,50);
        inv.printInventory();
    }
}
