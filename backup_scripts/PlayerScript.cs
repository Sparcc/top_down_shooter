using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
	//PLAYER STATS
	public float speed = 6f;            // The speed that the player will move at.
	public float turningRate = 400f;	// The speed that the player will rotate towards the mouse

	//shooting stats
	public float fireRate = 0;
	public float damage = 10;
	public float range = 100;
	public float bulletSpeed = 10f;

	float timeToFire = 0;

	//where cursor is detected
	Transform cursorSpace;

	AudioSource audio;

	//getting child object where bullet will shoot from
	Transform fireOrigin;

	//objects
	public LayerMask whatToHit;

	public Transform bullet;

	//public Rigidbody2D rb;

	float time = 0f;
	// Use this for initialization
	void Start () {
		// :^)

		//playerModel = transform.Find ("PlayerSprite").gameObject;
		//rb = GetComponent<Rigidbody2D> ();
		fireOrigin = transform.Find ("FireOrigin");
		//cursorSpace = transform.Find ("");
		audio = GetComponent<AudioSource>();
	}

	void Awake () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		time += Time.fixedDeltaTime;
		Aiming ();
		Movement ();

		DebugReport ();
	}

	void DebugReport (){
		//reporting
		if (time > 0.5f) {
			Vector3 playerLocation = Camera.main.WorldToScreenPoint (transform.localPosition);
			//Vector3 mouse = Input.mousePosition;
			Debug.Log("player model position = "+ playerLocation);
			time = 0f;
		}
	}

	void Movement ()
	{
		//Shooting ();
		Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

		transform.position += move * speed * Time.fixedDeltaTime;
	}

	void Aiming ()
	{
		//get mouse screen position
		Vector3 mouse = Input.mousePosition;

		//get object screen position
		Vector3 objectPosition = Camera.main.WorldToScreenPoint (transform.localPosition);

		//get direction
		Vector2 offset = new Vector2 (mouse.x - objectPosition.x, mouse.y - objectPosition.y);

		//get angle from direction
		float angle = Mathf.Atan2 (offset.y, offset.x) * Mathf.Rad2Deg;
		//playerModel.transform.rotation = Quaternion.Euler (0, 0, angle);
		//Debug.Log("Angle= " + angle);
		transform.rotation = Quaternion.RotateTowards(
				transform.rotation, Quaternion.Euler (0, 0, angle), 
				turningRate * Time.fixedDeltaTime
			);
		Shooting ();
	}

	void Shooting (){
		Vector3 forward = fireOrigin.transform.TransformDirection (Vector3.right) * 10;
		//Debug.DrawRay (fireOrigin.transform.position, forward, Color.cyan);

		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {

			}
			Fire();
			audio.Play ();
		} else {
			if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1 / fireRate;
				Fire ();
				audio.Play ();
			}
		}
	}

	void Fire(){

		//float angleToFire=Quaternion.Angle(Quaternion.Euler(new Vector3(0,0,0)),fireOrigin.transform.rotation);
		float angleToFire=Quaternion.Angle(
				Quaternion.Euler(new Vector3(0,0,0)),
				fireOrigin.transform.rotation
			);

		//Debug.Log ("angle of player = " + transform.rotation.z);
		//Debug.Log ("angle of player to cursor = " + angle);
		//Debug.Log ("angle of fire origin = " + angleToFire);
		//Debug.Log ("angle (transform.z) of fire origin = " + fireOrigin.transform.rotation.z);

		if (fireOrigin.transform.rotation.z < 0) {
			angleToFire -= (angleToFire*2);
		}

		//shoot a bullet
		/*
		Transform bulletInstance = Instantiate(
			bullet, 
			fireOrigin.position, 
			Quaternion.Euler(0, 0, angleToFire)
		) as Transform;

		bulletInstance.GetComponent <BulletScript>().bulletSpeed = bulletSpeed;*/
		Vector3 forward = fireOrigin.transform.TransformDirection (Vector3.right) * 10;
		Debug.DrawRay (fireOrigin.transform.position, forward, Color.cyan);

		Debug.Log ("Fire button pressed");
		Vector2 mousePosition = new Vector2 (
             Camera.main.ScreenToWorldPoint (Input.mousePosition).x,
             Camera.main.ScreenToWorldPoint (Input.mousePosition).y
     	);
		//Vector2 fireOriginPosition = new Vector2 (fireOrigin.position.x, fireOrigin.position.y);
		////RaycastHit2D hit = Physics2D.Raycast (fireOriginPosition, mousePosition-fireOriginPosition, range, whatToHit);
		//RaycastHit2D hit = Physics2D.Raycast (fireOriginPosition, fireOrigin.forward, range, whatToHit);
		////RaycastHit2D hit = Physics2D.Raycast (fireOriginPosition, fireOrigin., range, whatToHit);
		//
		////Debug.DrawLine (fireOriginPosition, (mousePosition-fireOriginPosition) * 100, Color.cyan);
		////RaycastHit2D hit = Physics2D.Raycast(fireOriginPosition, fireOrigin.forward, 1 << LayerMask.NameToLayer("Enemy"));
		//if (hit.collider.tag=="Enemy") {
		//	Debug.DrawRay (fireOrigin.transform.position, forward, Color.cyan);
		//	Debug.Log ("We hit " + hit.collider.name + " and did " + damage + " damage. ");
		//}
	}
}