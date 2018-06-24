using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    // Very Basic Top-down movement script.

    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody2D>();
	}

    public void Movement(float characterSpeed){
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        rb.velocity = Vector2.zero;
        Vector2 impulse = new Vector2(h,v).normalized * characterSpeed;
        rb.AddForce(impulse,ForceMode2D.Impulse);
    }
}
