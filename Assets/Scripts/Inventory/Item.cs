using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public string Name {get; set;}
    public int Id{get; set;}
    public int Price {get; set;}

    // Basic constructor
    public Item(){
        Name = "null";
        Id = 0;
        Price = 0;
    }

    // Constructor to populate item object
    public Item (string name, int id, int price){
        Name = name;
        Id = id;
        Price = price;
    }

    // for testing purposes
    public virtual string PrintToString
    {
        get
        {
            return string.Format("Name: {0}, ID: {1}, Price: {2}", Name, Id, Price);
        }
    }
}
