using UnityEngine;
using System.Collections;

public class projectile : MonoBehaviour {

	public float damage;
	public float fireRate;
	private string creator; // who created this projectile
	public float speed;
	public float lifetime = 5.0f;

	// Use this for initialization
	void Start () {
		rigidbody2D.velocity = transform.forward * speed;

		Destroy (this.gameObject, 5.0f);
	}
		
		void OnTriggerEnter2D( Collider2D other ) {
			if( other.tag == "Player" && creator == "Enemy") {
				//deal the damage
				Debug.Log ("Hit Player");
			} else if (other.tag == "Enemy" && creator == "Player"){
				//deal the damage to the enemy
				Debug.Log ("Hit Enemy");
			}
        }
		public void CreatedBy (string tag) {
			creator = tag;
		}

		public float FireRate() {
			return fireRate;
		}
	}