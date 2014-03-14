using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
	public int scoreValue;

	private GameController gameController;
	
	void Start ()
	{
        // make sure GameController still exists
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Platform") {
			return;
		}
		if (other.tag == "Player") {
			gameController.GameOver ();
		} else {
			// only add score if the player doesn't lose...
			gameController.AddScore (1);
			gameController.NextLevel ();
		}
		Destroy (other.gameObject);
		
        // destroy related objects
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
		foreach (GameObject obj in gameObjects)
		{
			GameObject.Destroy(obj);
		}
	}
}