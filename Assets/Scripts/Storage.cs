using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour {

    public Inventory inv;

	// Use this for initialization
	void Start () {
        inv = new Inventory(ConstantVariables.INVENTORYSLOTS);
	}

    public bool Withdraw(Player p, int id, int amount){
        if(inv.RemoveQuantity(id, amount))
        {
            p.inv.AddQuantity(id, amount);
            return true;
        }
        else
        {
            Debug.Log("Let player know that they can't withdraw the items.");
            return false;
        }
    }

    public bool Deposit(Player p, int id, int amount){
        if(p.inv.RemoveQuantity(id,amount)){
            inv.AddQuantity(id,amount);
            return true;
        }else {
            return false;
        }
    }

}
