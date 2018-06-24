using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, DestroyableObjects {

    public string characterName;
    public float maxHealth;
    public float currHealth;
    public float characterSpeed;

    public void takeDamage(float damage){
        if(currHealth > 0){
            currHealth -= damage;
        }else{
            Debug.Log("Character dies.");
            destroySelf();
        }
    }

    public void destroySelf(){
        Destroy(obj: gameObject);
    }
        
}
