using UnityEngine;
using System.Collections;

public class MagnetScript : MonoBehaviour {

	public float smoothing = 1f;
	public Transform target;


	void Start ()
	{
		StartCoroutine(MyCoroutine(target));
	}

	//pulling for values in an update function instead is less efficient
	IEnumerator MyCoroutine (Transform target){
		while(Vector3.Distance(transform.position, target.position) > 0.05f){
			transform.position = Vector3.Lerp(transform.position, target.position, smoothing * Time.deltaTime);

			yield return null;
		}

		print("Reached the target.");

		yield return new WaitForSeconds(3f);

		print("MyCoroutine is now finished.");
	}
}
