using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public enum States { MENU, GAME, SHOP, DEAD}
    public States state;

    public List<Item> itemList; // keeps track of items

    public Player p;
    public Shop s;

    public Queue enemyQueue;
    public Queue animalQueue;

    public bool isPaused = false;

    void Start()
    {
        p = GameObject.Find("Player").GetComponent<Player>();

        // Setting up item database and shop.
        itemList = new List<Item>();
        CreateItemDatabase();
        CreateShop();

        // Adding necessary items to the player's inventory
        SetupPlayerInventory();

        state = States.GAME;
    }

    public void CreateItemDatabase()
    {
        // NAME : ID : PRICE
        itemList.Add(CreateItem("Bullets", 1, 5));
        itemList.Add(CreateItem("Fodder", 2, 2));
        itemList.Add(CreateItem("Seeds", 3, 1));
        itemList.Add(CreateItem("Medicine", 4, 100));
        itemList.Add(CreateItem("Wood", 5, 10));
        itemList.Add(CreateItem("Gun", 6, 0)); // Necessary Player Item
        itemList.Add(CreateItem("Growth Potion", 7, 50));
        itemList.Add(CreateItem("Stamina Potion", 8, 25));
        itemList.Add(CreateItem("Bandages", 9, 25));
        itemList.Add(CreateItem("Sickle", 10, 0)); // Necessary Player Item
    }

    private Item CreateItem(string name, int id, int price)
    {
        return new Item(name, id, price);
    }


    public void Menu()
    {
        state = States.GAME;
    }


    public void CreateShop()
    {
        GameObject shop = new GameObject();
        shop.transform.SetParent(this.transform);
        shop.AddComponent<Shop>();
        s = shop.GetComponent<Shop>();
        s.SetItemList(itemList);

        s.CreatePossibleStock();
        s.Stock();
        s.inv.PrintInventory();
    }

    public void SetupPlayerInventory()
    {
        p.inv.AddItem(UtilityMethods.createItemContainer(itemList[5], 1));
        p.inv.AddItem(UtilityMethods.createItemContainer(itemList[0], 10));
        p.inv.AddItem(UtilityMethods.createItemContainer(itemList[9], 1));

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            PauseToggle();
        }

        switch (state)
        {
            case States.MENU:
                //Debug.Log("Menu is currently running.");
                break;
            case States.GAME:
                //Debug.Log("Game is currently running");
                break;
            case States.SHOP:
                //Debug.Log("Shop is open");
                break;
            case States.DEAD:
                //Debug.Log("Player has died");
                break;
            default:
                Debug.Log("Something has gone wrong with the game state.");
                break;
        }
    }

    public void PauseToggle()
    {
        if (isPaused)
            isPaused = false;
        else
            isPaused = true;
    }
}

public class ConstantVariables {
    public static int INVENTORYSLOTS = 5;
    public static int STORAGESLOTS = 10;
}

public class UtilityMethods {
    public static ItemContainer createItemContainer(Item item, int value){
        return new ItemContainer(item.Name, item.Id, value, item.Price);
    }
}
