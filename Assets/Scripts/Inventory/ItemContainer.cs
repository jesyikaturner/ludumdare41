using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : Item {

    public int value {get; set;}

    public ItemContainer(){
        this.value = 0;
    }

    public ItemContainer (string name, int id, int value, int price) : base(name,id,price){
        this.value = value;
    }

    // for testing purposes
    public string PrintToString(){
        return string.Format("Name: {0}, ID: {1}, Value: {2}, Price: {3}",this.name,this.id,this.value,this.price);
    }
}
