using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimal : Animal
{
    public float attackDamage;

    public ZombieAnimal()
    {

    }

    public ZombieAnimal(string name, float health, float speed, float damage)
    {
        characterName = name;
        maxHealth = health;
        currHealth = maxHealth;
        characterSpeed = speed;
        attackDamage = damage;

        state = CharacterState.INFECTED;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
