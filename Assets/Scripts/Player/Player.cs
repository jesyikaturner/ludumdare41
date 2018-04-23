using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

    public enum PlayerState {healthy, infected}
    public PlayerState playerState;

    // Player details
    public float maxStamina;
    public float currStamina;
    public int currMoney;

    // Player inventory
    public ItemContainer heldItem;
    public Inventory inv;

    public Shooter shootScript;
    public PlayerMovement movementScript;

    // player GUI reference
        
	void Start () {
        inv = new Inventory(ConstantVariables.INVENTORYSLOTS);
        shootScript = this.GetComponent<Shooter>();
        movementScript = this.GetComponent<PlayerMovement>();
        playerState = PlayerState.healthy;
	}
	
	// Update is called once per frame
	void Update () {
        movementScript.Movement(characterSpeed);
        ShootingChecks();

        /* If player is infected the player will lose stamina. If player is
        * healthy, they'll slowly regain stamina.
        */
        switch(playerState){
            case PlayerState.healthy:
                if(this.currStamina < maxStamina)
                    this.currStamina += Time.deltaTime;
                break;
            case PlayerState.infected:
                if(this.currStamina > 0)
                    this.currStamina -= Time.deltaTime;
                break;
            default:
                Debug.Log("Player isn't in either state. Something has gone wrong.");
                break;
        }

	}
        
    /* Checks to see if the player is holding the gun, and if there are
     * any bullets, the player will shoot. Player loses stamina and a bullet from
     * their inventory.
     */
    private void ShootingChecks(){
        if(heldItem == null)
            return;
        if(heldItem == inv.findItem(6) && inv.findItem(1).value > 0){
            if(this.currStamina > 0){
                shootScript.Shoot(this.transform);
                if(shootScript.isShooting){
                    loseStamina(5);
                    inv.RemoveItem(1,1);
                    shootScript.isShooting = false;
                }
            }
        }
    }

    private bool loseStamina(int amount){
        if(this.currStamina > 0){
            this.currStamina -= amount;
            return true;
        }else{
            Debug.Log("Character passes out or is unable to move til stamina refills");
            return false;
        }
    }
     
}
