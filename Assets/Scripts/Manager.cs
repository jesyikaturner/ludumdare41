using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour {

    public enum States{menu, game, shop, dead}
    public States state;

    public List<Item> itemList; // keeps track of items
    public List<ItemContainer> itemContainerList; // keeps track of items with prices

    public Player p;
    public Shop s;

    public Queue enemyQueue;
    public Queue animalQueue;

    // menu hud
    // hud
    // shop hud
    // death hud


    public void CreateItemDatabase(){
        // NAME : ID : PRICE
        itemList.Add(createItem("Bullets", 1, 5));
        itemList.Add(createItem("Fodder", 2, 2));
        itemList.Add(createItem("Seeds", 3, 1));
        itemList.Add(createItem("Medicine", 4, 100));
        itemList.Add(createItem("Wood",5, 10));
        itemList.Add(createItem("Gun",6,0));
        itemList.Add(createItem("Growth Potion",7,50));
        itemList.Add(createItem("Stamina Potion",8,25));
        itemList.Add(createItem("Bandages",8,25));
    }

    private Item createItem(string name, int id, int price){
        return new Item(name, id, price);
    }
    private ItemContainer createItemContainer(Item item, int value){
        return new ItemContainer(item.name, item.id, value, item.price);
    }

    public void Menu(){

    }
       

    public void CreateShop(){
        GameObject shop = new GameObject();
        shop.transform.SetParent(this.transform);
        shop.AddComponent<Shop>();
        s = shop.GetComponent<Shop>();

        s.items = itemList;
    }


    public void CreateZombie(string name , int id, int health, int attack, int speed){

    }

	// Use this for initialization
	void Start () {
        state = States.shop;
        itemList = new List<Item>();
        itemContainerList = new List<ItemContainer>();
        CreateItemDatabase();
        //CreatePlayer("Player");
        p = GameObject.Find("Player").GetComponent<Player>();
        p.inv.addItem(new ItemContainer("Gun",6,1,0));
        p.inv.addItem(createItemContainer(itemList[0],10));

        CreateShop();
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
}
