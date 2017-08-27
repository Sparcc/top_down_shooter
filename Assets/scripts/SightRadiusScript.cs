using UnityEngine;
using System.Collections;

public class SightRadiusScript : MonoBehaviour {
	float time = 2f;
	public Transform target;
	// Use this for initialization
	EnemyTestScript parentObject;

	public float range = 100;

	//objects
	public LayerMask whatToHit;

	bool DrawVisibleRayCRStart = false;

	Color debugLineColour = Color.yellow;

	void Start () {
		parentObject = gameObject.transform.parent.GetComponent<EnemyTestScript> ();
		StartCoroutine (CheckLineOfSight());
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
			//parentObject.target = other.transform;
			//parentObject.updatingPath = true;
			//LineOfSight(target);
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			parentObject.target = null;
			parentObject.updatingPath = false;
			target = null;
			debugLineColour = Color.yellow;
			//DrawVisibleRayCRStart = false;
		}
	}

	void LineOfSight(Transform target){
		//StartCoroutine (DrawVisibleRay (true));

		Vector3 targetDir = target.position - transform.position;
		Vector2 targetDirV2 = new Vector2 (targetDir.x, targetDir.y);
		Vector2 thisPosition = new Vector2 (transform.position.x, transform.position.y);
		RaycastHit2D hit = Physics2D.Raycast (thisPosition, targetDirV2, range, whatToHit);

		//Debug.DrawRay (transform.position, targetDir, Color.yellow);


		if (hit.collider.tag == "Player") {
			debugLineColour = Color.green;
			parentObject.target = target;
			parentObject.updatingPath = true;
			if (!parentObject.pathAlreadyUpdated) {
				parentObject.updatingPath = true;
			}
		} else {
			parentObject.target = null;
			debugLineColour = Color.red;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			Vector3 targetDir = target.position - transform.position;
			Debug.DrawRay (transform.position, targetDir, debugLineColour);
			/*
			//parentObject.target = other.transform;

			if (!parentObject.pathAlreadyUpdated) {
				parentObject.updatingPath = true;
			}*/
		}
	}

	IEnumerator CheckLineOfSight(){
		if (target != null) {
			Vector3 targetDir = target.position - transform.position;
			Vector2 targetDirV2 = new Vector2 (targetDir.x, targetDir.y);
			Vector2 thisPosition = new Vector2 (transform.position.x, transform.position.y);
			RaycastHit2D hit = Physics2D.Raycast (thisPosition, targetDirV2, range, whatToHit);

			//Debug.DrawRay (transform.position, targetDir, Color.yellow);


			if (hit.collider.tag == "Player") {
				debugLineColour = Color.green;
				parentObject.target = target;
				parentObject.updatingPath = true;
				if (!parentObject.pathAlreadyUpdated) {
					parentObject.updatingPath = true;
				}
			} else {
				parentObject.target = null;
				debugLineColour = Color.red;
			}
		}
		yield return new WaitForSeconds (0.1f);
		StartCoroutine (CheckLineOfSight());
	}
	//IEnumerator DrawVisibleRay(bool drawLineToTarget){
	//	if (drawLineToTarget && DrawVisibleRayCRStart){
	//		Vector3 targetDir = target.position - transform.position;
	//		Debug.DrawRay (transform.position, targetDir);
	//		yield return new WaitForSeconds (0.1);
	//		StartCoroutine (DrawVisibleRay);
	//	}
	//}
}