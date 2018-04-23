using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    // 5 slots
    // click on gui makes it selected

    public ItemContainer[] items;

    public Inventory(){
        items = new ItemContainer[ConstantVariables.INVENTORYSLOTS];
    }

    public Inventory(int slots){
        items = new ItemContainer[slots];
    }

    // add new item to inventory
    public bool addItem(ItemContainer itemToAdd){
        for(int i = 0; i < items.Length; i++){
            if(items[i] == null){
                items[i] = itemToAdd;
                return true;
            }
        }
        return false;
    }

    // add quantity to already existing item
    public bool addItem(int id, int quantity){
        ItemContainer i = findItem(id);
        if(i != null){
            i.value += quantity;
            return true;
        }
        return false;
    }

    // helper method to find items
    public ItemContainer findItem(int id){
        foreach(ItemContainer obj in items){
            if(obj != null && obj.id == id){
                return obj;
            }
        }
        return null;
    }
    public ItemContainer getItemFromIndex(int index){
        return items[index];
    }

    public int GetValue(int index){
        if(items[index] == null)
            return 0;
        if(items[index].value < 1)
            return 0;
        return items[index].value;
    }

    public bool RemoveItem(int id){
        for(int i = 0; i < items.Length; i++){
            if(items[i].id == id){
                items[i] = null;
                return true;
            }
        }
        return false;
    }

    /* Removes specified amount of items from the inventory
     * by id. If there aren't any items left in the inventory,
     * the item is removed.
     */ 
    public bool RemoveItem(int id, int quantity){
        ItemContainer i = findItem(id);
        if(i != null){
            if(i.value > 0){
               i.value -= quantity;
                return true;
            }else{
                RemoveItem(id);
            }
        }
        return false;
    }

    // for testing purposes
    public void printInventory(){
        foreach(ItemContainer obj in items){
            if(obj != null)
                Debug.Log(obj.PrintToString());
        }
    }
}
