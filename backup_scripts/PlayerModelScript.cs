using UnityEngine;
using System.Collections;

public class PlayerModelScript : MonoBehaviour {

	Vector3 myLastPosition;
	public Rigidbody2D rb;
	float time;
	//float angle;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		time = 2f;
		myLastPosition = transform.position;
		//myLastPosition = transform.localPosition;
		//rb = GetComponent<Rigidbody2D>();
		//angle=Quaternion.Angle(Quaternion.Euler(new Vector3(0,0,0)),transform.rotation);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		time += Time.fixedDeltaTime;
		//if (time > 0.1f) {
		//	myLastPosition = transform.position;
		//}

		//myLastPosition = transform.position;
		//DebugReport ();


	}

	void DebugReport (){
		//reporting
		if (time > 2f){
			Debug.Log("player position 100ms ago: "+ myLastPosition.ToString());
			time = 0f;
		}
	}

	void ReAdjust (){
		transform.position = myLastPosition;
		//transform.localPosition = new Vector3(0,0,0);
		//rb.isKinematic = true;
	}

	void OnCollisionEnter2D(Collision2D other){
		rb.velocity = Vector3.zero;
		myLastPosition = transform.position;
		Debug.Log ("Collision at position: " + myLastPosition.ToString());
		ReAdjust ();
		//rb.isKinematic = true;
	}

	void OnCollisionExit2D(Collision2D other){
		//Debug.Log ("Collision!");
		//ReAdjust ();
		//rb.isKinematic = false;
	}
}
