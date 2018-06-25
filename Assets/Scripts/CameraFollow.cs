using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // Very simple camera follow object script.

    public Transform objectToFollow;
    public float speed;
	
	// Update is called once per frame
	private void Update () {
        Vector3 newPos = new Vector3(objectToFollow.position.x, objectToFollow.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
	}
}
