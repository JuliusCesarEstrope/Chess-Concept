using UnityEngine;
using System.Collections;

public class P2CharacterControl : MonoBehaviour {
	
	bool facingRight;
	Animator anim;
	float currentPosition = 0;
	float previousPosition = 0;
	
	public bool grounded = false;
	public Transform groundCheck;
	float groundRadius;
	public LayerMask whatIsGround; 

	public Transform enemyCheck;
	public float enemyRadius;
	public LayerMask whatIsEnemy;

	// Use this for initialization
	void Start () {
	
		facingRight = true;
		groundRadius = 0.2f;
		enemyRadius = 0.5f;
		anim = GetComponent<Animator>();

	}

	void OnTriggerEnter2D (Collider2D info) {

		Debug.Log ("Triggered");
		
		if (Physics2D.OverlapCircle(enemyCheck.position, enemyRadius, whatIsEnemy))
			Destroy (gameObject);
		
	}

	void FixedUpdate () {
		
		grounded = IsGrounded ();
		
		anim.SetBool("Ground", grounded);
		
		currentPosition = transform.position.x;

		float move = currentPosition - previousPosition;

		previousPosition = currentPosition;
		
		if (!grounded)
			anim.SetFloat ("Speed", 1);
		else {
			anim.SetFloat ("Speed", 0);
		}
		 
		if(move > 0 && !facingRight) 
			Flip();
		else if (move < 0 && facingRight) 
			Flip();
	}
	
	void Flip() {
		
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		
	}

	public bool IsGrounded(){

		return Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

	}

}
