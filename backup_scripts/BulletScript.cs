using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {

	//set by player's script
	public float bulletSpeed;

	Vector3 forward;
	// Use this for initialization
	void Start () {

		//direction for bullet to go
		forward = this.transform.TransformDirection (Vector3.right);
		Destroy(gameObject, 4.0f);
	}

	// Update is called once per frame
	void FixedUpdate () {
		//rotate bullet to towards angle set by player's script
		//transform.rotation = Quaternion.Euler (0, 0, angle);
		transform.GetComponent<Rigidbody2D>().AddForce(forward * bulletSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
		//transform.GetComponent<Rigidbody2D>().AddForce(forward * bulletSpeed * Time.fixedDeltaTime);
	}
}
