using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : MonoBehaviour {

    public Inventory inv;

	// Use this for initialization
	void Start () {
        inv = new Inventory(ConstantVariables.INVENTORYSLOTS);
	}

    public void Withdraw(Player p, int id, int amount){
        p.inv.addItem(id, amount);
        inv.RemoveItem(id,amount);
    }

    public bool Deposit(Player p, int id, int amount){
        if(p.inv.RemoveItem(id,amount)){
            inv.addItem(id,amount);
            return true;
        }else {
            return false;
        }
    }

}
