using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Animal : Character {

    public enum AnimalBehaviour {WANDERING, FLEEING, ATTACKING}
    public AnimalBehaviour behaviour;

    // be able to add a sprite - public
    public Rigidbody2D rb;

    public int id;

    public float timeTilDirectionChange = 2f;
    private float randomX, randomY;

    public float fullness = 100f; // how full the animal is.
    public bool isInfected = false;

    public float infectionTimer = 0f;

    public Animal(){
        
    }
    public Animal(string name, int id, int speed, int health){
        characterName = name;
        this.id = id;
        maxHealth = health;
        characterSpeed = speed;
    }

	private void Start () {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(ChooseNewDirection(timeTilDirectionChange));

        state = CharacterState.HEALTHY;
        behaviour = AnimalBehaviour.WANDERING;
	}

    private void FixedUpdate()
    {
        switch (behaviour)
        {
            case AnimalBehaviour.WANDERING:
                Move();
                break;
            case AnimalBehaviour.FLEEING:
                break;
            case AnimalBehaviour.ATTACKING:
                break;
        }
    }

    private void Update(){
        Starve(0.5f*Time.deltaTime);

        switch(state){
            case CharacterState.HEALTHY:
                infectionTimer = 0f;
                break;
            case CharacterState.INFECTED:
                infectionTimer += Time.deltaTime;
                break;
            case CharacterState.BLEEDING:
                break;
        }
    }

    private void Move(){
        rb.velocity = Vector2.zero;
        Vector2 impulse = new Vector2(randomX,randomY) * characterSpeed * Time.deltaTime;
        rb.AddForce(impulse, ForceMode2D.Impulse);
    }

    // Coroutine to choose a new direction for the animal to walk.
    private IEnumerator ChooseNewDirection(float time){
        while(true){
            yield return new WaitForSeconds(time);
            randomX = Random.Range(-2.0f, 2.0f);
            randomY = Random.Range(-2.0f, 2.0f);
            time = Random.Range(3f, 5f);
        }
    }

    // To be accessed outside the script. 
    public void Feed(){
        fullness = 100;
    }

    // Animal is slowing starving and has to be constantly fed. If fullness goes below 0, it dies.
    private void Starve(float amount)
    {
        if (fullness > 0)
            fullness -= amount;
        else
            DestroySelf();
    }

    private void TurnIntoZombie()
    {
        // TODO
    }

    // Overrided DestroySelf method in Character, so if the animal is infected, it will turn into a zombie.
    public override void DestroySelf()
    {
        Debug.Log(string.Format("{0} is dead",characterName));
        if(isInfected)
            TurnIntoZombie();
        else
        {
            base.DestroySelf();
        }
    }
}
