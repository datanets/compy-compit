using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject enemy;
	public GUIText scoreText;
	public GUIText statusText;
	
	private bool restart;
	private static int score;	// static keeps score same even after loading level
	
	void Start ()
	{
		restart         = false;
		statusText.text = "";
		UpdateScore ();
	}
	
	void Update ()
	{
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Wins: " + score;
	}

	public void GameOver ()
	{
		statusText.text = "Game Over!\nPress 'R' for Restart";
		score = 0;
		restart = true;
	}
	
	public void NextLevel()
	{
		statusText.text += "You Win!\nPress 'R' to Continue";
		restart = true;
	}
}