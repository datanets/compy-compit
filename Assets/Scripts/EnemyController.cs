using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    public GUIText statusText;
    public Vector3 speed = new Vector3(1.0f, 1.0f, 0.0f);	
	
    private Animator animator;
    private bool isGrounded;
    private float distance;
    private float lastDistance;
    private float visibleDistance;
    private GameObject player;
    private Quaternion initialRotation;
    private Transform target;
    private Vector3 movement;
	//private List<string> enemyMoveList = new List<string> { "S" };
	
	// lists were using up too much memory, I guess... switching to arrays
	private string[] enemyMoves = new string[2];
	
	

    // Use this for initialization
    void Start()
    {
        animator        = this.GetComponent<Animator>();
        initialRotation = transform.rotation;
        isGrounded      = false;
        visibleDistance = 4.0f;

        // make sure player is still alive
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // distance between player and enemy
            distance = Vector2.Distance(target.position, transform.position);

            // if enemy can "see" player
            if (distance > 1.0f && distance < visibleDistance)
            {
                // enemy moving toward player
                if (target.position.x > transform.position.x)
                {
                    transform.position += speed * Time.deltaTime;
                    animator.SetInteger("Direction", 3);
                    //Debug.Log("cpu moving right");
					shiftArrayAndAppend("MR", true);
                }
                else
                {
                    transform.position -= speed * Time.deltaTime;
                    animator.SetInteger("Direction", 1);
                    //Debug.Log("cpu moving left");
					shiftArrayAndAppend("ML", true);
                }
            }
            else if (distance <= 1.0f && (target.position.y > transform.position.y))
            {
                // enemy float/fly upward
                if (isGrounded)
                {
                    // enemy stay upright
                    transform.rotation = initialRotation;
                    transform.rigidbody2D.AddForce(Vector2.up * 350.0f);
					shiftArrayAndAppend("FJ", true);
                }
                animator.SetInteger("Direction", 0);
            }
            else
            {
                // enemy jumping in general
                if (isGrounded)
                {
                    rigidbody2D.AddForce(Vector2.up * 100.0f);
					shiftArrayAndAppend("J", true);
                }

                // enemy stand back up (original rotation)
                transform.rotation = initialRotation;
                animator.SetInteger("Direction", 0);
            }
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
    public string getEnemyLastMove()
    {
		return this.enemyMoveList[this.enemyMoveList.Count-1];
    }
    
	public List<string> getEnemyMovesList()
	{
		return this.enemyMoveList;
	}
	*/
	
	public string[] getEnemyMoves()
	{
		return enemyMoves;
		//return string.Join(",", this.enemyMoves);
	}
	
	public bool shiftArrayAndAppend(string value, bool compact)
	{
		if (!string.IsNullOrEmpty(value))
		{
			List<string> enemyMovesShift = new List<string>();
			
			if (enemyMoves[this.enemyMoves.Length-1] != null) {
				enemyMovesShift = enemyMoves.ToList();
				if (compact)
				{
					if (!enemyMovesShift[enemyMovesShift.Count-1].Equals(value))
					{
						enemyMovesShift.RemoveAt(0);
						enemyMovesShift.Add(value);
						enemyMoves = enemyMovesShift.ToArray();
					}
				}
				else
				{
					enemyMovesShift.RemoveAt(0);
					enemyMovesShift.Add(value);
					enemyMoves = enemyMovesShift.ToArray();
				}
			} else {
				if (compact)
				{
					enemyMovesShift = enemyMoves.ToList();
					if ((enemyMovesShift.Count) > 0 && (enemyMovesShift[enemyMovesShift.Count-1] != null))
					{
						if (!enemyMovesShift[enemyMovesShift.Count-1].Equals(value))
						{
							enemyMovesShift.Add(value);
							enemyMoves = enemyMovesShift.ToArray();
						}
					}
					else
					{
						enemyMovesShift.Add(value);
						enemyMoves = enemyMovesShift.ToArray();
					}
				}
				else
				{
					enemyMovesShift = enemyMoves.ToList();
					enemyMovesShift.Add(value);
					enemyMoves = enemyMovesShift.ToArray();
				}
			}
			return true;
		}
		else
		{
			return false;
		}
	}
}
