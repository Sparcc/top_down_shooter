using UnityEngine;
using System.Collections;

public class SightRadiusScript : MonoBehaviour {
	float time = 2f;
	Transform target;
	// Use this for initialization
	EnemyTestScript parentObject;
	bool drawRay = false;

	void Start () {
		parentObject = gameObject.transform.parent.GetComponent<EnemyTestScript> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		time += Time.fixedDeltaTime;
		if (time > 2f){
			//Do shit
			time = 0f;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			target = other.transform;
			parentObject.target = other.transform;
			parentObject.updatingPath = true;
			//LineOfSight(target);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			parentObject.target = null;
			parentObject.updatingPath = false;
		}
	}

	void LineOfSight(Transform target){
		//StartCoroutine (DrawVisibleRay (true));

		Vector3 targetDir = target.position - transform.position;

		RaycastHit2D hit = Physics2D.Raycast (transform.position, targetDir);
		LineOfSight(target);

		if (hit.collider.tag != "Player") {
			parentObject.target = null;
			Debug.DrawRay (transform.position, targetDir, Color.white);
		} else {
			Debug.DrawRay (transform.position, targetDir, Color.red);
			parentObject.target = target;
			parentObject.updatingPath = true;
			if (!parentObject.pathAlreadyUpdated) {
				parentObject.updatingPath = true;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player") {

			Vector3 targetDir = target.position - transform.position;
			//Debug.DrawRay (transform.position, targetDir, Color.blue);

			parentObject.target = other.transform;
			if (!parentObject.pathAlreadyUpdated) {
				parentObject.updatingPath = true;
			}
		}
	}

	/*
	IEnumerator DrawVisibleRay(){
		if (drawRay && target!=null){
			Vector3 targetDir = target.position - transform.position;
			Debug.DrawRay (transform.position, targetDir);
			yield return new WaitForSeconds (0.1);
			StartCoroutine (DrawVisibleRay);
		}
	}*/
}