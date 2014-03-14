using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float weight;
    public GUIText statusText;
    public Vector2 speed = new Vector2 (50, 50);
	
	private Animator animator;
    private bool isGrounded;
    private Quaternion initialRotation;
	private Vector3 movement;
	//private List<string> playerMoveList = new List<string> { "S" };

	void Start ()
	{
		animator        = this.GetComponent<Animator>();
		initialRotation = transform.rotation;
		isGrounded      = false;
		weight          = rigidbody2D.mass;
	}

	// Update is called once per frame
	void Update ()
	{
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = 0.0f;

		// faster floating (gravity)
		inputY -= 50.0f * Time.deltaTime;

		movement = new Vector2 (
			speed.x * inputX,
			speed.y * inputY
		);
		
        // player moving left
		if (inputX < 0)
		{
			animator.SetInteger("Direction", 1);
			//playerMoveList.Add("ML");
		}
        // player moving right
		else if (inputX > 0)
		{
			animator.SetInteger ("Direction", 3);
			//playerMoveList.Add("MR");
        // player facing forward/player is still
		} else if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.DownArrow)) {
			animator.SetInteger ("Direction", 0);
			//playerMoveList.Add("S");
		}
	}

	void FixedUpdate ()
	{
        // player jump (if grounded)
		if (Input.GetKeyDown (KeyCode.Space))
        {
			if (isGrounded) {
				rigidbody2D.AddForce(Vector2.up * 4000.0f);
				//playerMoveList.Add("J");
			}
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			// player stand up (original rotation)
			transform.rotation = initialRotation;
			//playerMoveList.Add("SU");
		}
		
		rigidbody2D.velocity = movement;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == "Boundary") {
			statusText.text = "Sorry, you Lose!\nPress 'r' to play again!";
		}
	}
	
	void OnCollisionEnter2D(Collision2D other)
	{
		isGrounded = true;
	}
	
	void OnCollisionExit2D(Collision2D other)
	{
		isGrounded = false;
	}
	
	/*
	public string getPlayerLastMove()
	{
		return this.playerMoveList[this.playerMoveList.Count - 1];
	}
	
	public List<string> getPlayerMovesList()
	{
		return this.playerMoveList;
	}
	*/
}