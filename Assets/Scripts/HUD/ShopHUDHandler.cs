using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHUDHandler : MonoBehaviour {

    /*
     * TODO create quantity fields and item images from shop inventory.
     * TODO create inventory fields and item images from player inventory.
     */

    public InventoryManagement currInv;
    public InventoryManagement itemsToSell;
    public Image[] itemIcons;
    public Text outputWarning;
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

        player = GameObject.Find("Player").GetComponent<Player>();
        shop = GameObject.Find("Shop").GetComponent<Shop>();

        currInv = player.inv;
        itemsToSell = new InventoryManagement();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    // parsing the text entered into the input fields then cloning the item from the player inventory.
    // the cloned items quantity is changed to the input amount and then its added to the items to sell
    // inventory.
    private bool ParseAndAdd(int index, string text)
    {
        outputWarning.text = "";
        ItemContainer item = currInv.GetItemFromIndex(index);

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

    public void Sell_Button(){
        bool sold = false;

        if (itemsToSell.GetInventorySize() < 1)
            return;

        for(int i = 0; i< itemsToSell.GetInventorySize(); i++)
        {
            ItemContainer item = itemsToSell.GetItemFromIndex(i);
            if (item != null)
            {
                sold = shop.SellItem(player, item.Id, item.Value);
                if (!sold)
                    outputWarning.text += string.Format("Item: {0} didn't sell.\n", item.Name);
            }
        }
        itemsToSell.RemoveAll();
        foreach (InputField input in inputs)
        {
            input.text = "";
        }
    }
}
