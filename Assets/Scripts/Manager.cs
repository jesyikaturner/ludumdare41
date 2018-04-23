using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public enum States{menu, game, shop, dead}
    public States state;

    public List<Item> itemList; // keeps track of items

    public Player p;
    public Shop s;

    public Queue enemyQueue;
    public Queue animalQueue;

    void Start () {
        p = GameObject.Find("Player").GetComponent<Player>();

        // Setting up item database and shop.
        itemList = new List<Item>();
        CreateItemDatabase();
        CreateShop();

        // Adding necessary items to the player's inventory
        SetupPlayerInventory();

        state = States.shop;
    }

    public void CreateItemDatabase(){
        // NAME : ID : PRICE
        itemList.Add(createItem("Bullets", 1, 5));
        itemList.Add(createItem("Fodder", 2, 2));
        itemList.Add(createItem("Seeds", 3, 1));
        itemList.Add(createItem("Medicine", 4, 100));
        itemList.Add(createItem("Wood",5, 10));
        itemList.Add(createItem("Gun",6,0)); // Necessary Player Item
        itemList.Add(createItem("Growth Potion",7,50));
        itemList.Add(createItem("Stamina Potion",8,25));
        itemList.Add(createItem("Bandages",9,25));
        itemList.Add(createItem("Sickle",10,0)); // Necessary Player Item
    }

    private Item createItem(string name, int id, int price){
        return new Item(name, id, price);
    }


    public void Menu(){

    }
       

    public void CreateShop(){
        GameObject shop = new GameObject();
        shop.transform.SetParent(this.transform);
        shop.AddComponent<Shop>();
        s = shop.GetComponent<Shop>();

        s.items = itemList;

        s.CreatePossibleStock();
        s.Stock();
        s.inv.printInventory();
    }

    public void SetupPlayerInventory(){
        p.inv.addItem(UtilityMethods.createItemContainer(itemList[5],1));
        p.inv.addItem(UtilityMethods.createItemContainer(itemList[0],10));
        p.inv.addItem(UtilityMethods.createItemContainer(itemList[9],1));

    }
	
	// Update is called once per frame
	void Update () {
        switch(state){
            case States.menu:
                //Debug.Log("Menu is currently running.");
                break;
            case States.game:
                //Debug.Log("Game is currently running");
                break;
            case States.shop:
                //Debug.Log("Shop is open");
                break;
            case States.dead:
                //Debug.Log("Player has died");
                break;
        }
	}
}

public class ConstantVariables {
    public static int INVENTORYSLOTS = 5;
    public static int STORAGESLOTS = 10;
}

public class UtilityMethods {
    public static ItemContainer createItemContainer(Item item, int value){
        return new ItemContainer(item.name, item.id, value, item.price);
    }
}
