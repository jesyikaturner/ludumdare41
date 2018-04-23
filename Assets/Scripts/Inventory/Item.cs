using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public string name {get; set;}
    public int id{get; set;}
    public int price {get; set;}

    public Item(){
        this.name = "null";
        this.id = 0;
        this.price = 0;
    }
    public Item (string name, int id, int price){
        this.name = name;
        this.id = id;
        this.price = price;
    }

    // for testing purposes
    public string PrintToString(){
        return string.Format("Name: {0}, ID: {1}, Price: {2}",this.name,this.id,this.price);
    }

}
