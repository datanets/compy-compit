using UnityEngine;
using System.Collections;

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
            // distance between player and opponent
            distance = Vector2.Distance(target.position, transform.position);

            // if opponent can "see" player
            if (distance > 1.0f && distance < visibleDistance)
            {
                // opponent moving toward player
                if (target.position.x > transform.position.x)
                {
                    transform.position += speed * Time.deltaTime;
                    animator.SetInteger("Direction", 3);
                    Debug.Log("cpu moving right");
                }
                else
                {
                    transform.position -= speed * Time.deltaTime;
                    animator.SetInteger("Direction", 1);
                    Debug.Log("cpu moving left");
                }
            }
            else if (distance <= 1.0f && (target.position.y > transform.position.y))
            {
                // opponent float/fly upward
                if (isGrounded)
                {
                    // opponent stay upright
                    transform.rotation = initialRotation;
                    transform.rigidbody2D.AddForce(Vector2.up * 350.0f);
                }
                animator.SetInteger("Direction", 0);
            }
            else
            {
                // opponent jumping in general
                if (isGrounded)
                    rigidbody2D.AddForce(Vector2.up * 100.0f);

                // opponent stand back up (original rotation)
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
}
