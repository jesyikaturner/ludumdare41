using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryHUD : MonoBehaviour {

    public Player p;
    //public List<GameObject> slots;
    public List<Text> texts;

	// Use this for initialization
	void Start () {
        p = GameObject.Find("Player").GetComponent<Player>();
        texts = new List<Text>();
        for(int i = 0; i < 5; i++){
            // Getting the Quantity Child and adding its text object to the list
            texts.Add(transform.GetChild(0).GetChild(i).GetChild(1).GetComponent<Text>());
        }
        //Debug.Log(texts.Count);

	}
	
	// Update is called once per frame
	void Update () {
        // Updating the quantity text above inventory slots and deactivating text if quantity is 0
        for(int i = 0; i < texts.Count; i++){
            if(p.inv.GetValue(i) < 1){
                texts[i].gameObject.SetActive(false);
            }else{
                if(!texts[i].gameObject.activeSelf)
                    texts[i].gameObject.SetActive(true);
                texts[i].text = p.inv.GetValue(i).ToString();
            }
        }
	}

    public void Btn_SlotOne(){
        if(texts[0].gameObject.activeSelf)
            p.heldItem = p.inv.getItemFromIndex(0);
    }
    public void Btn_SlotTwo(){
        if(texts[1].gameObject.activeSelf)
            p.heldItem = p.inv.getItemFromIndex(1);
    }
    public void Btn_SlotThree(){
        if(texts[2].gameObject.activeSelf)
            p.heldItem = p.inv.getItemFromIndex(2);
    }
    public void Btn_SlotFour(){
        if(texts[3].gameObject.activeSelf)
            p.heldItem = p.inv.getItemFromIndex(3);
    }
    public void Btn_SlotFive(){
        if(texts[4].gameObject.activeSelf)
            p.heldItem = p.inv.getItemFromIndex(4);
    }

}
