using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public enum States { MENU, GAME, SHOP, DEAD}
    public States state;

    public List<Item> itemList; // keeps track of items

    public Player player;
    public Shop s;
    public TestCaseShop testShop;
    public ImageManager im;

    public Queue enemyQueue;
    public Queue animalQueue;

    public bool isPaused = false;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();

        // Setting up item database and shop.
        itemList = new List<Item>();
        CreateItemDatabase();

        CreateShop();

        // Adding necessary items to the player's inventory
        SetupPlayerInventory();

         //Testing test shop.
         /*
        if (GetComponent<TestCaseShop>())
        {
            testShop = GetComponent<TestCaseShop>();
            testShop.SetupTestShop(itemList);
            testShop.TestPlayerBuy();
            testShop.TestPlayerSell();
        }*/

        state = States.GAME;
    }

    public void CreateItemDatabase()
    {
        // NAME : ID : PRICE : IMAGE
        itemList.Add(new Item("Bullets", 1, 5, im.bullet));
        itemList.Add(new Item("Fodder", 2, 2, im.fodder));
        itemList.Add(new Item("Seeds", 3, 1, im.seeds));
        itemList.Add(new Item("Medicine", 4, 100, im.medicine));
        itemList.Add(new Item("Wood", 5, 10, im.wood));
        itemList.Add(new Item("Gun", 6, 0, im.gun)); // Necessary Player Item
        itemList.Add(new Item("Growth Potion", 7, 50, im.growthPot));
        itemList.Add(new Item("Stamina Potion", 8, 25, im.stamPot));
        itemList.Add(new Item("Bandages", 9, 25, im.bandages));
        itemList.Add(new Item("Sickle", 10, 0, im.sickle)); // Necessary Player Item
    }

    public void Menu()
    {
        state = States.GAME;
    }


    public void CreateShop()
    {
        GameObject shop = new GameObject();
        shop.transform.SetParent(transform);
        shop.name = "Shop";
        shop.AddComponent<Shop>();
        s = shop.GetComponent<Shop>();
        s.SetupShop(itemList);
    }

    public void SetupPlayerInventory()
    {
        player.inv.AddItem(UtilityMethods.createItemContainer(itemList[5], 1)); // gun
        player.inv.AddItem(UtilityMethods.createItemContainer(itemList[0], 10)); // bullets
        player.inv.AddItem(UtilityMethods.createItemContainer(itemList[9], 1)); // sickle

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            isPaused = !isPaused;
            Debug.Log("Pause has been toggled.");
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
}

public class ConstantVariables {
    public static int INVENTORYSLOTS = 5;
    public static int STORAGESLOTS = 10;
}

public class UtilityMethods {
    public static ItemContainer createItemContainer(Item item, int value){
        return new ItemContainer(item.Name, item.Id, value, item.Price, item.Image);
    }
}
