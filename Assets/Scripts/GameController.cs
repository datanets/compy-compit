using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameController : MonoBehaviour
{
	public GameObject player;
	public GameObject enemy;
	public GUIText scoreText;
	public GUIText statusText;

	//private GameObject player;	
	private bool restart;
	private static int score;	// static keeps score same even after loading level
	
	private PlayerController playerController;
	private EnemyController enemyController;
	private string winner;
	
	private static List<List<string>> enemyMoveMemory = new List<List<string>>();
	
	void Start ()
	{
		restart         = false;
		statusText.text = "";
		winner 			= "";
		UpdateScore ();

		player = GameObject.FindWithTag("Player");
		if (player != null)
		{
			playerController = player.GetComponent<PlayerController>();
		}
		
		enemy = GameObject.FindWithTag("Enemy");
		if (enemy != null)
		{
			enemyController = enemy.GetComponent<EnemyController>();
		}
		
	}
	
	void Update ()
	{
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		/*
		if (playerController != null)
			Debug.Log("player last move: " + playerController.getPlayerLastMove());
		if (enemyController != null)
			Debug.Log ("enemy last move: " + enemyController.getEnemyLastMove());
		*/
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
	
	public void NextLevel()
	{
		//if (playerController != null)
		//	Debug.Log("player moves: " + listPlayerMoves() + ",W");
		if (enemyController != null)
			Debug.Log ("enemy moves: " + listEnemyMoves() + ",L");
		
		winner = "player";
		Debug.Log ("winner: " + winner);
		statusText.text += "You Win!\nPress 'R' to Continue";
		restart = true;
	}
	
	public void GameOver ()
	{
		//if (playerController != null)
		//	Debug.Log("player moves: " + listPlayerMoves() + ",L");
		if (enemyController != null)
		{
			Debug.Log ("enemy moves: " + listEnemyMoves() + ",W");
			// save enemy move list to enemyMoveMemory
			List<string> combinedMoveListAndWeight = enemyController.getEnemyMoves().ToList();
			combinedMoveListAndWeight.Add("1");
			enemyMoveMemory.Add(combinedMoveListAndWeight);
			Debug.Log("enemy move memory: " + listEnemyMoveMemory());
		}
		
		winner = "enemy";
		Debug.Log ("winner: " + winner);
		statusText.text = "Game Over!\nPress 'R' for Restart";
		score = 0;
		restart = true;
	}

/*	
	public string listPlayerMoves()
	{
		List<string> compactedMoves = new List<string>();
		string lastItem = "";
		
		if (playerController != null)
		{
			for (int i = 0; i < playerController.getPlayerMovesList().Count; i++)
			{
				if (!playerController.getPlayerMovesList()[i].Equals(lastItem))
				{
					compactedMoves.Add(playerController.getPlayerMovesList()[i]);
				}
				lastItem = playerController.getPlayerMovesList()[i];
			}
		}
		return string.Join(",", compactedMoves.ToArray());
	}
*/

	public string listEnemyMoves()
	{
		/*
		List<string> compactedMoves = new List<string>();
		string lastItem = "";
		
		if (enemyController != null)
		{
			for (int i = 0; i < enemyController.getEnemyMoves().Length; i++)
			{
				if (!enemyController.getEnemyMoves()[i].Equals(lastItem))
				{
					compactedMoves.Add(enemyController.getEnemyMoves()[i]);
				}
				lastItem = enemyController.getEnemyMoves()[i];
			}
		}
		return string.Join(",", compactedMoves.ToArray());
		*/
		return string.Join (",", enemyController.getEnemyMoves());
	}
	
	public static string listEnemyMoveMemory()
	{
		string result = "";
		for (int i = 0; i < enemyMoveMemory.Count; i++)
		{
			result += string.Join (",", enemyMoveMemory[i].ToArray()) + " | ";
		}
		return result;
	}
}