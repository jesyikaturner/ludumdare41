using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Animal : Character {

    public enum AnimalState {healthy, infected, bleeding};
    public AnimalState state;
    // be able to add a sprite - public
    public Rigidbody2D rb;

    public int id;

    public float timeTilDirectionChange = 2f;
    public float randomX, randomY;

    // infection timer? 

    public float fullness; // how full the animal is.

    public Animal(){
        
    }
    public Animal(string name, int id, int speed, int health){
        this.characterName = name;
        this.id = id;
        this.maxHealth = health;
        this.characterSpeed = speed;
    }

	void Start () {
        this.rb = this.GetComponent<Rigidbody2D>();
        StartCoroutine(ChooseNewDirection(timeTilDirectionChange));
        fullness = 100f;

        state = AnimalState.healthy;
	}

    public void Update(){
        Move();
        fullness -= Time.deltaTime;

        switch(state){
            case AnimalState.healthy:
                break;
            case AnimalState.infected:
                break;
            case AnimalState.bleeding:
                break;
        }
    }

    public void Move(){
        rb.velocity = Vector2.zero;
        Vector2 impulse = new Vector2(randomX,randomY) * characterSpeed * Time.deltaTime;
        rb.AddForce(impulse, ForceMode2D.Impulse);
    }

    IEnumerator ChooseNewDirection(float time){
        while(true){
            yield return new WaitForSeconds(time);
            randomX = Random.Range(-2.0f, 2.0f);
            randomY = Random.Range(-2.0f, 2.0f);
            time = Random.Range(3f, 5f);
        }
    }

    public void Feed(){
        fullness = 100;
    }
}
