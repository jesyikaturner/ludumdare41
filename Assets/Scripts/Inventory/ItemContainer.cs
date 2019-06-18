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
    public ItemContainer (string name, int id, int value, int price, Sprite image) : base(name,id,price,image){
        Value = value;
    }

    // Clone another Item Container
    public ItemContainer(ItemContainer other)
    {
        Name = other.Name;
        Id = other.Id;
        Value = other.Value;
        Price = other.Price;
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
