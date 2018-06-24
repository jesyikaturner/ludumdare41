using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : Item {

    public int Value {get; set;}

    // basic constructor extending the Item constructor
    public ItemContainer(){
        Value = 0;
    }

    // // Constructor to populate item container object extending the item object's constructor
    public ItemContainer (string name, int id, int value, int price) : base(name,id,price){
        Value = value;
    }

    // for testing purposes
    public override string PrintToString
    {
        get
        {
            return string.Format("Name: {0}, ID: {1}, Value: {2}, Price: {3}", Name, Id, Value, Price);
        }
    }
}
