using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public GameObject player;
	public float attackRange;
	public Transform firePoint;
	public GameObject prefabProjectile; // the bullet/laser/etc

	public float fireRate; // how often it fires
	private float nextFire;
	public bool doesNotDisengage = false; // after finding the player, don't stop attacking.
	private bool playerFound = false; // has the player been found

	private float damping = 6.0f;
	public float movementSpeed = 0.1f;


	// Use this for initialization
	void Start () {
	
		playerFound = false;

		if (rigidbody) {
			rigidbody.freezeRotation = true;
		}
	}
	// Update is called once per frame
	void Update () {
	
		if (player) {
			if ( attackRange > Vector3.Distance (player.transform.position,
			                                   	transform.position)
			    || playerFound )
			{
				// we found the player & we don't ever want to stop attacking
				if ( doesNotDisengage ) { playerFound = true;}

				// look at the player

				transform.LookAt ( player.transform );

				// moevemnt force
				//rigidbody2D.AddRelativeForce ( new Vector2(0, movementSpeed));

				// is it able to fire
				if ( Time.time > nextFire)
				{ 
					nextFire = Time.time + fireRate;

					GameObject bullet = Instantiate( prefabProjectile,
					                                firePoint.position,
					                                firePoint.rotation) as GameObject;
					bullet.GetComponent<projectile>().CreatedBy ("Enemy");
				}

			} else {
				//when not in range
				patrol();
			}
		} else {
			player = GameObject.FindGameObjectWithTag("Player"); 
				
}
	}

	public Transform[] wayPoints;
	public int currentWP; // current wayPoint it is traveling to 
	public float distanceToWayPoint = 2.0f; // we're close enough
	private void patrol() {
		if (wayPoints.Length == 0) {
		//nothing in the array,don't do anything		
		}
		if (wayPoints [currentWP] == null) {
			nextWayPoint();
			return;
		}
		if (distanceToWayPoint > Vector3.Distance (wayPoints [currentWP].position,
		                                           transform.position)) {
						nextWayPoint ();
		} else {
			transform.LookAt ( wayPoints[currentWP]);
			// add force 
		}
	}
	private void nextWayPoint()
	{
		currentWP++;
		if (currentWP > wayPoints.Length - 1) {
			currentWP = 0;}
	}
}