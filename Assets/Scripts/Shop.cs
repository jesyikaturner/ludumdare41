using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

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
        if(p.currMoney > (p.currMoney - inv.findItem(id).price)){
            
        }
    }

    public void Stock(){

    }
    public void ClearStock(){

    }
        
    public void CreatePossibleStock(){
        foreach(Item item in items){
            if(item.price > 0){
                possibleStock.Add(createItemContainer(item,Random.Range(20,40)));
            }else if (item.price > 50) {
                possibleStock.Add(createItemContainer(item,Random.Range(1,5)));
            }
        }
    }

    // Adds a quantity to the item.
    private ItemContainer createItemContainer(Item item, int value){
        return new ItemContainer(item.name, item.id, value, item.price);
    }

    // testing iventory management.
    public void TESTCASE(){
        inv.RemoveItem(1);
        inv.addItem(2,50);
        inv.printInventory();
    }
}
