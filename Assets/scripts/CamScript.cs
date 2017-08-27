using UnityEngine;
using System.Collections;

public class CamScript : MonoBehaviour {
	//BELOW IS MY SHIT CODE; AFTER IS THE GUCCI SHIT
	int distance = 10;
	public Transform target;
	bool smoothTransition = false;
	//Use this for initialization
	void Start () {
	
	}
	/*
	// Update is called once per frame
	void Update () {
		//transform.position = Vector2.Lerp (transform.position, target.position, 0.5f);
		transform.position = target.position;
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - distance);
	}*/

	public float dampTime = 10f;
	private Vector3 velocity = Vector3.zero;
	//public Transform target;

	// Update is called once per frame
	void Update () 
	{
		/*
		if (target)
		{
			Vector3 point = Camera.main.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}*/

		//transform.position = Vector2.Lerp (transform.position, target.position, 0.5f);
		transform.position = target.position;
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.z - distance);
	}


}
