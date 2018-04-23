using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour, DestroyableObjects {

    // be able to add a sprite - public
    public Rigidbody2D rb;

    public string animalName {get; set;}
    public int id {get; set;}
    private float speed;
    public int health {get; set;}

    private float timeTilDirectionChange = 2f;
    private float randomX, randomY;

    public Animal(){
        this.animalName = "null";
        this.id = 0;
        this.speed = 0.2f;
        this.health = 0;
    }
    public Animal(string name, int id, int speed, int health){
        this.animalName = name;
        this.id = id;
        this.speed = speed;
        this.health = health;
    }

	void Awake () {
        this.rb = this.gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(RandomMovement(timeTilDirectionChange));
	}

    public void Update(){
        move();
    }

    public void move(){
        rb.velocity = Vector2.zero;
        Vector2 impulse = new Vector2(randomX,randomY) * speed * Time.deltaTime;
        rb.AddForce(impulse, ForceMode2D.Impulse);
    }

    IEnumerator RandomMovement(float time){
        while(true){
            yield return new WaitForSeconds(time);
            randomX = Random.Range(-2.0f, 2.0f);
            randomY = Random.Range(-2.0f, 2.0f);
            time = Random.Range(3f, 5f);
        }
    }


    public void takeDamage(int damage){
        if(this.health > 0){
            this.health -= damage;
        }else{
            destroySelf();
        }
    }

    public void destroySelf(){
        Destroy(this);
        Debug.Log("Make character inactive, return to stack");
    }
}
