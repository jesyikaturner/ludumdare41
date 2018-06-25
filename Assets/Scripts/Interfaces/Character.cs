using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, DestroyableObjects {

    public enum CharacterState {HEALTHY, INFECTED, BLEEDING}
    public CharacterState state;

    public string characterName;
    public float maxHealth;
    public float currHealth;
    public float characterSpeed;

    public void TakeDamage(float damage){
        if(currHealth > 0){
            currHealth -= damage;
        }else{
            Debug.Log("Character dies.");
            DestroySelf();
        }
    }

    // virtual so i can override for the player, enemies and animals if i want
    public virtual void DestroySelf(){
        Destroy(obj: gameObject);
    }

    public void RegainHealth(float amount)
    {
        currHealth += amount;
        if (currHealth > maxHealth)
            currHealth = maxHealth;
    }
        
}
