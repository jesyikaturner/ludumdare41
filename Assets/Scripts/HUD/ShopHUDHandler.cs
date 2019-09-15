using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHUDHandler : MonoBehaviour {

    public InventoryManagement currInv;
    public InventoryManagement buyAndSellItems;

    public Image[] itemIcons;
    public InputField[] inputs;

    public Text outputWarning;
    public Text shopText;

    // this really will be messy.
    public GameObject shopPanel;
    public GameObject sellBuyOptionPanel;
    public GameObject sellButton;
    public GameObject buyButton;

    public Player player;
    public Shop shop;

	// Use this for initialization
	void Start () {
        // getting the item icons
        itemIcons = new Image[ConstantVariables.INVENTORYSLOTS];
        inputs = new InputField[ConstantVariables.INVENTORYSLOTS];

        for (int i = 0; i < ConstantVariables.INVENTORYSLOTS; i++)
        {
            itemIcons[i] = GameObject.Find("ICON_Item" + (i + 1)).GetComponent<Image>();
            inputs[i] = GameObject.Find("INPUT_Item" + (i + 1)).GetComponent<InputField>();
        }

        // getting the player and the shop
        player = GameObject.Find("Player").GetComponent<Player>();
        shop = GameObject.Find("Shop").GetComponent<Shop>();

        // inventory to hold items that the player is trying to sell/ buy;
        buyAndSellItems = new InventoryManagement();

        // setting this to inactive.
        shopPanel.SetActive(false);
        sellButton.SetActive(false);
        buyButton.SetActive(false);
    }

    public void DisplaySellBuyOption()
    {
        sellBuyOptionPanel.SetActive(true);
    }
	
    public void Button_SetShopSell()
    {
        // hide the sell buy option panel
        sellBuyOptionPanel.SetActive(false);

        // make the shop panel visible
        shopPanel.SetActive(true);

        // make the sell button visible
        sellButton.SetActive(true);

        if (shopText != null)
            shopText.text = "SELL";
        // current inventory is set to player's inventory
        currInv = player.inv;

        // set the hud icons to show the current inventory's items
        for(int i = 0; i < itemIcons.Length; i++)
        {
            if (currInv.GetItemFromIndex(i) != null)
                itemIcons[i].sprite = currInv.GetItemFromIndex(i).Image;
            else
            {
                itemIcons[i].gameObject.SetActive(false);
                inputs[i].gameObject.SetActive(false);
            }
                
        }
    }

    public void Button_Exit()
    {
        // TODO Handle closing
    }

    public void Button_SetShopBuy()
    {
        // hide the sell buy option panel
        sellBuyOptionPanel.SetActive(false);

        // make the shop panel visible
        shopPanel.SetActive(true);

        // make the buy button visible
        buyButton.SetActive(true);

        if (shopText != null)
            shopText.text = "BUY";

        currInv = shop.inv;

        // set the hud icons to show the current inventory's items
        for (int i = 0; i < itemIcons.Length; i++)
        {
            if (currInv.GetItemFromIndex(i) != null)
                itemIcons[i].sprite = currInv.GetItemFromIndex(i).Image;
            else
            {
                itemIcons[i].gameObject.SetActive(false);
                inputs[i].gameObject.SetActive(false);
            }

        }
    }

    // parsing the text entered into the input fields then cloning the item from the player inventory.
    // the cloned items quantity is changed to the input amount and then its added to the items to sell
    // inventory.
    private bool ParseAndAdd(int index, string text)
    {
        // Clear the HUD output warning.
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
                    buyAndSellItems.AddItem(clone);
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
        if (buyAndSellItems.GetInventorySize() < 1)
            return;

        for(int i = 0; i< buyAndSellItems.GetInventorySize(); i++)
        {
            // check if the item exists
            ItemContainer item = buyAndSellItems.GetItemFromIndex(i);
            if (item != null)
            {
                // check if it actually sold.
                bool sold = shop.SellItem(player, item.Id, item.Value);
                // tell player if it didnt sell.
                if (!sold)
                    outputWarning.text += string.Format("Item: {0} couldn't be sold.\n", item.Name);
            }
        }

        // clear the items to sell inventory
        buyAndSellItems.RemoveAll();

        // clear all the inputs
        foreach (InputField input in inputs)
        {
            input.text = "";
        }
    }

    public void Buy_Button()
    {
        // Make sure that the buy inventory has items
        if (buyAndSellItems.GetInventorySize() < 1)
            return;

        for(int i = 0; i < buyAndSellItems.GetInventorySize(); i++)
        {
            ItemContainer item = buyAndSellItems.GetItemFromIndex(i);
            if(item != null)
            {
                bool bought = shop.BuyItem(player, item.Id, item.Value);
                if (!bought)
                    outputWarning.text += string.Format("Item: {0} couldn't be bought.\n", item.Name);
            }
        }

        // clear the items to sell inventory
        buyAndSellItems.RemoveAll();

        // clear all the inputs
        foreach (InputField input in inputs)
        {
            input.text = "";
        }

        // TODO update player inventory with newly bought items.
    }
}
