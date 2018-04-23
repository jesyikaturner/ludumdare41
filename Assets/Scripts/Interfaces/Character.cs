using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, DestroyableObjects {

    public string characterName;
    public float maxHealth;
    public float currHealth;
    public float characterSpeed;

    public void takeDamage(int damage){
        if(this.currHealth > 0){
            this.currHealth -= damage;
        }else{
            destroySelf();
        }
    }

    public void destroySelf(){
        Destroy(this.gameObject);
    }
        
}
