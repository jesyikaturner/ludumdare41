using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHUDHandler : MonoBehaviour {

    public enum State{buy, sell}
    public State state;
    public List<ItemType> items = new List<ItemType>();

    public Manager gm;

    //public int selectedItem;

	// Use this for initialization
	void Start () {
        gm = GameObject.Find("MANAGER").GetComponent<Manager>();
	}
	
	// Update is called once per frame
	void Update () {
        
        switch(state){
            case State.buy:
                Debug.Log("currently in buy mode.");
                break;

            case State.sell:
                Debug.Log("currently in sell mide");
                break;
            default:
                Debug.Log("State has been incorrectly set. Neither in buy or sell mode.");
                break;
        }
	}

    public void bullet_Field(string text){
        int i = int.Parse(text);
        if(i > 0){
            ItemType item = new ItemType();
            item.id = 1;
            item.quantity = i;
            items.Add(item);
        }
    }

    public void Fodder_Field(string text){
        int i = int.Parse(text);
        if(i > 0){
            ItemType item = new ItemType();
            item.id = 2;
            item.quantity = i;
            items.Add(item);
        }
    }

    public void Seeds_Field(string text){
        int i = int.Parse(text);
        if(i > 0){
            ItemType item = new ItemType();
            item.id = 3;
            item.quantity = i;
            items.Add(item);
        }
    }

    public void Medicine_Field(string text){
        int i = int.Parse(text);
        if(i > 0){
            ItemType item = new ItemType();
            item.id = 4;
            item.quantity = i;
            items.Add(item);
        }
    }

    public void Wood_Field(string text){
        int i = int.Parse(text);
        if(i > 0){
            ItemType item = new ItemType();
            item.id = 5;
            item.quantity = i;
            items.Add(item);
        }
    }

    public void Sell_Button(){
        if(items.Count < 1)
            return;
        foreach(ItemType item in items){
            if(gm.s.SellItem(gm.p,item.id,item.quantity)){

            }else{
                Debug.Log("Tell player what hasn't sold.");
            }
        }
    }

    /*
    public void ID_Field(string newText){
        id = int.Parse(newText);
    }

    public void Quantity_Field(string newText){
        quantity = int.Parse(newText);
    }*/


}

public class ItemType {
    public int id;
    public int quantity;

    public ItemType(){
    }
}
