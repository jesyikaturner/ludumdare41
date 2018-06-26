using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inventory Management

public class InventoryManagement {

    public ItemContainer[] items;

    public InventoryManagement(){
        items = new ItemContainer[ConstantVariables.INVENTORYSLOTS];
    }

    public InventoryManagement(int slots){
        items = new ItemContainer[slots];
    }

    /*
     * ----------
     * ADD ITEMS 
     * ----------
     */

    // Add new item to inventory
    public bool AddItem(ItemContainer itemToAdd){
        // First check to see if the item doesn't already exist.
        if(FindItemByContainer(itemToAdd) == null)
        {
            // Add item to the first avialable null position
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    items[i] = itemToAdd;
                    return true;
                }
            }
        }
        // If the item already exists then return false.
        return false;
    }

    // add quantity to already existing item
    public bool AddQuantity(int id, int quantity)
    {
        ItemContainer i = FindItemById(id);
        if(i != null)
        {
            i.Value += quantity;
            return true;
        }
        return false;
    }

    // better version of add quantity
    public void AddQuantityByContainer(ItemContainer itemToAdd, int amount)
    {
        ItemContainer item = FindItemByContainer(itemToAdd);
        if (item != null)
        {
            item.Value += amount;
        }
        else
        {
            itemToAdd.Value = amount;
            AddItem(itemToAdd);
        }
    }

    /*
     * -----------
     * FIND ITEMS
     * ----------
     */ 

    /* 
     * Find items by ID
     * Since the item containers aren't in a static public array, I have to reference the ID 
     * poistion I put them in when creating the array.
     */ 
    public ItemContainer FindItemById(int id)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].Id == id)
                return items[i];
        }
        return null;

    }

    // Finds by the object and returns it if it exists.
    public ItemContainer FindItemByContainer(ItemContainer item)
    {
        foreach(ItemContainer obj in items){
            if (obj == item)
                return obj;
        }
        return null;
    }

    // Gets whatever item is at that index in the inventory array.
    public ItemContainer GetItemFromIndex(int index)
    {
        return items[index];
    }

    public int GetItemValueFromIndex(int index)
    {
        if (items[index] == null)
            return 0;
        if (items[index].Value < 1)
            return 0;
        return items[index].Value;
    }

    /*
     * -----------
     * REMOVE ITEM
     * -----------
     */

    public bool RemoveItemById(int id)
    {
        for(int i = 0; i < items.Length; i++){
            if(items[i].Id == id){
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
    public bool RemoveQuantity(int id, int quantity)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i] != null && items[i].Id == id)
            {
                if(quantity > items[i].Value)
                    return false;

                if(items[i].Value == quantity)
                {
                    items[i] = null;
                    return true;
                }

                if(items[i].Value > 0)
                {
                    items[i].Value -= quantity;
                    return true;
                }
            }
        }
        return false;
    }

    // Removes all items from the inventory
    public void RemoveAll()
    {
        for(int i = 0; i < items.Length; i++)
        {
            items[i] = null;
        }
    }

    // for testing purposes
    public void PrintInventory()
    {
        foreach(ItemContainer obj in items)
        {
            if(obj != null)
                Debug.Log(obj.PrintToString);
        }
    }
}
