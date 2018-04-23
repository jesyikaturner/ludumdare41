using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    // This script shoots a bullet from player position to where the mouse position is

    public Rigidbody2D bullet;
    public float shootingSpeed = 2f;
    public float timeBetweenBullets = 0.15f;
    public bool isShooting = false;

    private float timer;

    // Creates a bullet clone and sets its direction and speed.
    public void Shoot(Transform playerPos){
        timer += Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && timer >= timeBetweenBullets){
            timer = 0f;
            isShooting = true;
            Vector3 shootDirection = GetDirection(playerPos);
            // play sound
            Rigidbody2D bulletInstance = Instantiate(bullet,playerPos.position+(shootDirection*0.5f), Quaternion.identity) as Rigidbody2D;
            bulletInstance.name= "Bullet";
            bulletInstance.velocity = shootDirection * shootingSpeed;

        }
    }
        
    // Getting direction from player position to the mouse position.
    private Vector3 GetDirection(Transform playerPos){
        Vector3 shootDirection = Input.mousePosition;
        shootDirection.z = 0f;
        shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);
        shootDirection = shootDirection - playerPos.position;
        return shootDirection;
    }

}
