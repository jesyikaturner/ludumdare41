using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
    /*
     * Mouse Behaviour
     * 
     * Get what the player clicks on, if the player is within a certain distance. 
     */

    private float timer = 0f;
    private readonly float timeBetweenItemUse = 0.15f;
    public bool isUsingItem = false;

	// Use this for initialization
	void Start () {
		
	}

    /* 
     * I need to shoot a ray from the mouse I think. 
     * Then I need to check what that ray hits. I think I need an id to pass through
     * since it can all be in the one method. 
     * 
     * something like:
     */ 
    public void Interaction(int id){
        timer += Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && timer >= timeBetweenItemUse)
        {
            timer = 0f;
            if (ObjectClickedOn(id))
            {
                isUsingItem = true;
            }
        }
    }

    private bool ObjectClickedOn(int id)
    {
        return false;
    }

}
