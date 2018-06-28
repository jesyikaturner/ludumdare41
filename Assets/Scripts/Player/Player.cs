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
 * TODO Handle giving medicine to animals
 * TODO Handle fixing fences
 * TODO Handle planting seeds
 */ 
public class Player : Character {

    // Player details
    public float maxStamina;
    public float currStamina;
    public int currMoney;

    // Player inventory
    public ItemContainer heldItem;
    public InventoryManagement inv;

    public Shooter shootScript;
    public PlayerMovement movementScript;
    public PlayerInteraction interaction;

	private void Start () {
        inv = new InventoryManagement(ConstantVariables.INVENTORYSLOTS);
        shootScript = GetComponent<Shooter>();
        movementScript = GetComponent<PlayerMovement>();
        state = CharacterState.HEALTHY;
	}

    private void FixedUpdate()
    {
        movementScript.Movement(characterSpeed);
    }

    // Update is called once per frame
    private void Update () {
        // Using the gun
        ShootingCheck();

        // Take Medicine
        TakeMedicineCheck();

        /* If player is infected the player will lose stamina. If player is
        * healthy, they'll slowly regain stamina.
        */
        switch (state){
            case CharacterState.HEALTHY:
                if(currStamina <= maxStamina)
                    currStamina += Time.deltaTime;
                break;
            case CharacterState.INFECTED:
                if(currStamina > 0)
                    if (!LoseStamina(1f * Time.deltaTime))
                    {
                        // Player will die. I guess?
                    }
                break;
            case CharacterState.BLEEDING:
                TakeDamage(1f);
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

        if (heldItem == null)
            return;

        if (heldItem != inv.FindItemById(2))
            return;

        if (inv.FindItemById(2).Value > 0)
        {
            LoseStamina(5f);
            inv.RemoveQuantity(2,1);
        }
 
    }

    public void TakeMedicineCheck()
    {
        if (heldItem == null)
            return;

        if (heldItem != inv.FindItemById(4))
            return;

        if (inv.FindItemById(4).Value > 0)
        {
            interaction.Interaction(4);
            if (interaction.isUsingItem)
            {
                RegainHealth(50f);
                LoseStamina(5f);
                inv.RemoveQuantity(4, 1);
                interaction.isUsingItem = false;
            }

        }
    }

    public void GiveMedicineCheck()
    {
        // TODO
    }

    public void FixFenceCheck()
    {
        // TODO
    }

    public void PlantSeedsCheck()
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

    public void AddMoney(int amount)
    {
        if(currMoney == 0)
        {
            currMoney = amount;
        }
        else
        {
            currMoney += amount;
        }
    }
     
}
