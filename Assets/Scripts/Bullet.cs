using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public float deathCounter = 5f;
    public float timer;

    public int damage = 6;
	// Use this for initialization
	void Start () {
        timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if(timer > deathCounter){
            Destroy(this.gameObject);
        }
	}

    public void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.name == "Bullet"){
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if(other.gameObject.name == "Player"){
            //Debug.Log("IS HITTING PLAYER");
            other.transform.GetComponentInParent<Player>().takeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
