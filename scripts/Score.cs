using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public int score ;					// The player's score.
	public static int bestScore;


	private PlayerControl playerControl;	// Reference to the player control script.
	private int previousScore = 0;			// The score in the previous frame.


	void Awake ()
	{
		// Setting up the reference.
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
		score = 0;
		bestScore = PlayerPrefs.GetInt("bestScore");
	}


	void Update ()
	{
		// Set the score text.
		GetComponent<GUIText> ().text = "Score: " + score;

		// If the score has changed...
		if (previousScore != score) {
			// ... play a taunt.
			if (playerControl != null)
			{
				playerControl.StartCoroutine (playerControl.Taunt ());
			}
		}
	
		// Set the previous score to this frame's score.
		previousScore = score;

		if (score > bestScore) {
			bestScore = score;
		}
	}

}
