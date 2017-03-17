using UnityEngine;
using System.Collections;

public class P2Move : MonoBehaviour {

	GameObject Character;

	public bool JumpButton;
	public bool LeapButton;
	public float HorizontalAxis;
	public float VerticalAxis;

	public float maxSpeed;
	public float jumpSpeed;
	public float Jump;
	public float leapSpeed;
	public float Leap;

	public float moveTime = 0;
	public float currentTime;
	public bool movable = true;
	public float xSpeed;
	public float ySpeed;

	public bool grounded = false;
	public float groundRadius = 0.2f;
	public Transform groundCheck;
	public LayerMask whatIsGround; 

	void Start () {
			
		Physics2D.IgnoreLayerCollision (11, 11, true);

	}

	void Update () {

		currentTime = Time.time;
		
		if (currentTime - moveTime >= 2)
			movable = true;

		JumpButton = Input.GetButton ("P2Jump");
		LeapButton = Input.GetButton ("P2Leap");
		HorizontalAxis = Input.GetAxis ("P2Horizontal");
		VerticalAxis = Input.GetAxis ("P2Vertical");

		if (KnightButtonCheck ())
			KnightControl ();

		if (BishopButtonCheck ())
			BishopControl ();

		if (RookButtonCheck ())
			RookControl ();

		if (PawnButtonCheck ())
			PawnControl ();

		if (QueenButtonCheck ())
			QueenControl ();

		if (KingButtonCheck ())
			KingControl ();

//		moveStop ();
	}

	void FixedUpdate () {

	}

	void KnightControl (){
		
		Character = GameObject.Find ("P2Knight");
		
		grounded = Character.GetComponent<P2CharacterControl>().IsGrounded();

		jumpSpeed = 20f;
		Jump = 70f;
		leapSpeed = 30f;
		Leap = 50f;

		if (grounded && JumpButton)
			moveJump (HorizontalAxis);
		
		if (grounded && LeapButton)
			moveLeap (HorizontalAxis);
		
		xSpeed = (float)Character.GetComponent<Rigidbody2D>().velocity.x;
		ySpeed = (float)Character.GetComponent<Rigidbody2D>().velocity.y;

	}

	void BishopControl (){

		Character = GameObject.Find ("P2Bishop");
		
		grounded = Character.GetComponent<P2CharacterControl>().IsGrounded();
		
		maxSpeed = 25f;

		moveDiag (HorizontalAxis, VerticalAxis);
		
		xSpeed = (float)Character.GetComponent<Rigidbody2D>().velocity.x;
		ySpeed = (float)Character.GetComponent<Rigidbody2D>().velocity.y;

	}

	void RookControl () {
		
		Character = GameObject.Find ("P2Rook");
		
		grounded = Character.GetComponent<P2CharacterControl>().IsGrounded();
		
		maxSpeed = 30f;
		
		moveLateral (HorizontalAxis, VerticalAxis);
		
		xSpeed = (float)Character.GetComponent<Rigidbody2D>().velocity.x;
		ySpeed = (float)Character.GetComponent<Rigidbody2D>().velocity.y;


	}

	void PawnControl () {
		
		Character = GameObject.Find ("P2Pawn");
		
		grounded = Character.GetComponent<P2CharacterControl>().IsGrounded();
		
		maxSpeed = 20f;
		jumpSpeed = 20f;
		Jump = 50f;
		leapSpeed = 20f;
		Leap = 50f;

		if(grounded && !JumpButton && !LeapButton)
			moveLateral (-Mathf.Abs (HorizontalAxis), 0);

		if (grounded && JumpButton)
			moveJump (-Mathf.Abs (HorizontalAxis));
		
		if (grounded && LeapButton)
			moveLeap (-Mathf.Abs (HorizontalAxis));
		
		xSpeed = (float)Character.GetComponent<Rigidbody2D>().velocity.x;
		ySpeed = (float)Character.GetComponent<Rigidbody2D>().velocity.y;

	}

	void QueenControl () {
		
		Character = GameObject.Find ("P2Queen");
		
		grounded = Character.GetComponent<P2CharacterControl>().IsGrounded();
		
		maxSpeed = 35f;

		if(Mathf.Abs (HorizontalAxis) > 0.1 && Mathf.Abs (VerticalAxis) > 0.1)
			moveDiag (HorizontalAxis, VerticalAxis);

		else
			moveLateral (HorizontalAxis, VerticalAxis);
		
		xSpeed = (float)Character.GetComponent<Rigidbody2D>().velocity.x;
		ySpeed = (float)Character.GetComponent<Rigidbody2D>().velocity.y;

	}

	void KingControl (){

		Character = GameObject.Find ("P2King");
		
		grounded = Character.GetComponent<P2CharacterControl>().IsGrounded();
		
		maxSpeed = 15f;
		jumpSpeed = 15f;
		Jump = 50f;
		leapSpeed = 15f;
		Leap = 50f;

		if(grounded && !JumpButton && !LeapButton)
			moveLateral (HorizontalAxis, 0);
		
		if (grounded && JumpButton)
			moveJump (HorizontalAxis);
		
		if (grounded && LeapButton)
			moveLeap (HorizontalAxis);
		
		xSpeed = (float)Character.GetComponent<Rigidbody2D>().velocity.x;
		ySpeed = (float)Character.GetComponent<Rigidbody2D>().velocity.y;

	}
	
	bool KnightButtonCheck (){
		
		return Input.GetButton ("P2Knight");
		
	}
	
	bool BishopButtonCheck (){
		
		return Input.GetButton ("P2Bishop");
		
	}
	
	bool RookButtonCheck (){
		
		return Input.GetButton ("P2Rook");
		
	}
	
	bool PawnButtonCheck (){
		
		return Input.GetButton ("P2Pawn");
		
	}
	
	bool QueenButtonCheck (){
		
		return Input.GetButton ("P2Queen");
		
	}
	
	bool KingButtonCheck (){
		
		return Input.GetButton ("P2King");
		
	}

	public void moveJump(float axis) {
		
		float move = axisSnap(axis);
		
		if (move != 0 && movable) {
			Character.GetComponent<Rigidbody2D> ().velocity = new Vector2 (move * jumpSpeed, Jump);
		}

	}
	
	public void moveLeap (float axis) {

		float move = axisSnap(axis);
		
		if (move != 0 && movable) 
			Character.GetComponent<Rigidbody2D>().velocity = new Vector2 (move * leapSpeed, Leap);

	}

	public void moveDiag (float axis1, float axis2){

		if (Mathf.Abs (axis1) < 0.1 && Mathf.Abs (axis2) > 0.1)
			axis1 = axis2;
		else if (Mathf.Abs (axis2) < 0.1 && Mathf.Abs (axis1) > 0.1)
			axis2 = axis1;
		else if (Mathf.Abs (axis1) < 0.1 && Mathf.Abs (axis2) < 0.1) {
			axis1 = 0;
			axis2 = 0;
		} else {
			axis1 = axisSnap (axis1);
			axis2 = axisSnap (axis2);
		}

		if (movable) 
			Character.GetComponent<Rigidbody2D> ().velocity = new Vector2 (axis1 * maxSpeed, axis2 * maxSpeed);

	}

	void moveLateral (float axis1, float axis2) {

		if (Mathf.Abs (axis1) < 0.1 && Mathf.Abs (axis2) > 0.1)
			axis1 = 0;
		else if (Mathf.Abs (axis2) < 0.1 && Mathf.Abs (axis1) > 0.1)
			axis2 = 0;
		else if (Mathf.Abs (axis1) > 0.1 && Mathf.Abs (axis2) > 0.1) {
			axis1 = 0;
			axis2 = 0;
		} else {
			axis1 = axisSnap (axis1);
			axis2 = axisSnap (axis2);
		}

		if (movable)
			Character.GetComponent<Rigidbody2D> ().velocity = new Vector2 (axis1 * maxSpeed, axis2 * maxSpeed);

	}

	float axisSnap (float axis) {
		
		if (axis > 0.1)
			return 1;
		else if (axis < -0.1)
			return -1;
		else
			return 0;
		
	}

	void moveStop (){

		if (xSpeed < 0.1 && ySpeed < 0.1){
			
			movable = false;
			moveTime = Time.time;
			
		}

	}
}