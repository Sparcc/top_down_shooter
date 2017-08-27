using UnityEngine;
using System.Collections;

public class DogScript : MonoBehaviour {
	AudioSource audio;
	float time;
	//System.Random rnd;
	float randomCooldown;
	// Use this for initialization
	void Start () {
		time = 0f;
		//rnd = new System.Random ((int) Time.time);
		//randomCooldown = (float)(2/rnd.Next(1,3));
		randomCooldown = Random.Range(0.5f, 3f);
		audio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		time += Time.fixedDeltaTime;

		if (time > randomCooldown){
			audio.pitch = Random.Range (0.5f,1.5f);
			audio.Play ();
			time = 0f;
		}
	}
}
