using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adding required components
[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInteraction))]


/* 
 * Player Script. This handles player information and states.
 * This handles player interaction with environment based on
 * what's currently held.
 * 
 * TODO Handle harvesting
 * TODO Handle feeding
 * TODO Handle taking medice/ giving medicine to animals
 * TODO Handle fixing fences
 */ 
public class Player : Character {

    public enum PlayerState {HEALTHY, INFECTED, BLEEDING}
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
    public PlayerInteraction interaction;

	void Start () {
        inv = new Inventory(ConstantVariables.INVENTORYSLOTS);
        shootScript = this.GetComponent<Shooter>();
        movementScript = this.GetComponent<PlayerMovement>();

        playerState = PlayerState.HEALTHY;
	}

    public void FixedUpdate()
    {
        movementScript.Movement(characterSpeed);
    }

    // Update is called once per frame
    void Update () {
        ShootingCheck();

        /* If player is infected the player will lose stamina. If player is
        * healthy, they'll slowly regain stamina.
        */
        switch(playerState){
            case PlayerState.HEALTHY:
                if(currStamina < maxStamina+1)
                    currStamina += Time.deltaTime;
                break;
            case PlayerState.INFECTED:
                if(currStamina > 0)
                    if (!LoseStamina(1f))
                    {
                        // Player will die. I guess?
                    }
                break;
            case PlayerState.BLEEDING:
                takeDamage(1f);
                break;
            default:
                Debug.Log("Player isn't in either state. Something has gone wrong.");
                break;
        }

	}

    public void UseItem()
    {
        if (heldItem == null)
            return;

        if(heldItem == inv.FindItemById(6)) // Gun
            ShootingCheck();

        if (heldItem == inv.FindItemById(9)) // Sickle
            HarvestCheck();

        if (heldItem == inv.FindItemById(1)) // Fodder
            FeedCheck();

        if (heldItem == inv.FindItemById(3)) // Medicine
            TakeMedicineCheck();

        if (heldItem == inv.FindItemById(3)) // Medicine
            GiveMedicineCheck();

        if (heldItem == inv.FindItemById(4)) // Wood
            FixFenceCheck();

    }
        
    /* Checks to see if the player is holding the gun, and if there are
     * any bullets, the player will shoot. Player loses stamina and a bullet from
     * their inventory.
     */
    private void ShootingCheck(){
        if(heldItem == null)
            return;

        if (heldItem != inv.FindItemById(6))
            return;

        if(inv.FindItemById(1) != null && inv.FindItemById(1).Value > 0){
            if(currStamina > 0){
                shootScript.Shoot(transform);
                if(shootScript.isShooting){
                    LoseStamina(5f);
                    inv.RemoveQuantity(1,1);
                    shootScript.isShooting = false;
                }
            }
        }
    }

    public void HarvestCheck()
    {
        // TODO
    }

    public void FeedCheck()
    {
        // TODO
    }

    public void TakeMedicineCheck()
    {
        // TODO
    }

    public void GiveMedicineCheck()
    {
        // TODO
    }

    public void FixFenceCheck()
    {
        // TODO
    }

    private bool LoseStamina(float amount){
        if(currStamina > 0){
            currStamina -= amount;
            return true;
        }else{
            Debug.Log("Character passes out or is unable to move til stamina refills");
            return false;
        }
    }
     
}
