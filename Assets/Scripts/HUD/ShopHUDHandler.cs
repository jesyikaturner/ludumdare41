using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHUDHandler : MonoBehaviour {

    public Sprite[] playerInvIcons;
    public Sprite[] shopInvIcons;

    public InventoryManagement currInv;
    public InventoryManagement itemsToSell;
    public Image[] itemIcons;
    public Text outputWarning;
    public Text shopText;
    public InputField[] inputs;

    public Player player;
    public Shop shop;

	// Use this for initialization
	void Start () {
        // getting the item icons
        itemIcons = new Image[ConstantVariables.INVENTORYSLOTS];
        for (int i = 0; i < ConstantVariables.INVENTORYSLOTS; i++)
        {
            itemIcons[i] = GameObject.Find("ICON_Item" + (i + 1)).GetComponent<Image>();
        }

        // getting the placeholder text child of each input box
        inputs = GetComponentsInChildren<InputField>();

        // getting the output warning text.
        outputWarning = GameObject.Find("TXT_Warning").GetComponent<Text>();

        // getting the player and the shop
        player = GameObject.Find("Player").GetComponent<Player>();
        shop = GameObject.Find("Shop").GetComponent<Shop>();

        // an inventory to hold the items the player chooses to sell.
        itemsToSell = new InventoryManagement();

        // setting the current inventory to display to the player
        Button_SetShopSell();
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Button_SetShopSell()
    {
        if(shopText != null)
            shopText.text = "SELL";
        currInv = player.inv;

        for(int i = 0; i < itemIcons.Length; i++)
        {
            itemIcons[i].sprite = currInv.GetItemFromIndex(i).Image;
        }

    }

    public void Button_SetShopBuy()
    {
        if (shopText != null)
            shopText.text = "BUY";
        currInv = shop.inv;

        for (int i = 0; i < itemIcons.Length; i++)
        {
            itemIcons[i].sprite = shopInvIcons[i];
        }
    }


    // parsing the text entered into the input fields then cloning the item from the player inventory.
    // the cloned items quantity is changed to the input amount and then its added to the items to sell
    // inventory.
    private bool ParseAndAdd(int index, string text)
    {
        outputWarning.text = "";
        ItemContainer item = currInv.GetItemFromIndex(index);

        if(text == "" || text == "0")
        {
            return false;
        }

        if(item == null)
        {
            outputWarning.text = "ERROR: Item doesn't exist?";
            return false;
        }

        if(item.Price == 0)
        {
            outputWarning.text = string.Format("Item {0} cannot be sold.\n", item.Name);
            return false;
        }
        else
        {
            int i;

            if (int.TryParse(text, out i))
            {
                if (i > 0)
                {
                    ItemContainer clone = new ItemContainer(item)
                    {
                        Value = i
                    };
                    itemsToSell.AddItem(clone);
                    return true;
                }
                outputWarning.text = string.Format("Invalid amount entered for: {0}", item.Name);
                return false;
            }
            outputWarning.text = string.Format("Invalid input entered for: {0}", item.Name);
            return false;
        }
    }

    /*
     * ------------
     * INPUT FIELDS
     * ------------
     */ 

    public void Item1_Field(string text){
        ParseAndAdd(0,text);
    }

    public void Item2_Field(string text){
        ParseAndAdd(1,text);
    }

    public void Item3_Field(string text){
        ParseAndAdd(2,text);
    }

    public void Item4_Field(string text){
        ParseAndAdd(3,text);
    }

    public void Item5_Field(string text){
        ParseAndAdd(4,text);
    }

    /*
     * -----------
     * SELL BUTTON
     * -----------
     */ 

    public void Sell_Button()
    {
        // if theres nothing in the items to sell inventory do nothing.
        if (itemsToSell.GetInventorySize() < 1)
            return;

        for(int i = 0; i< itemsToSell.GetInventorySize(); i++)
        {
            // check if the item exists
            ItemContainer item = itemsToSell.GetItemFromIndex(i);
            if (item != null)
            {
                // check if it actually sold.
                bool sold = shop.SellItem(player, item.Id, item.Value);
                // tell player if it didnt sell.
                if (!sold)
                    outputWarning.text += string.Format("Item: {0} didn't sell.\n", item.Name);
            }
        }
        // clear the items to sell inventory
        itemsToSell.RemoveAll();

        // clear all the inputs
        foreach (InputField input in inputs)
        {
            input.text = "";
        }
    }
}
