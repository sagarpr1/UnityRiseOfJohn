using UnityEngine;
using System.Collections;

public class Remover : MonoBehaviour
{
	public GameObject splash;
	public GameObject scoremgr;
	public int scoreval;
	public int score;
	public static int bestScore;
	public GameObject TextMsg;

	void Awake(){
		//scoreval = GetComponent<Score> ().score;
		bestScore = PlayerPrefs.GetInt("bestScore");
		TextMsg.SetActive (false);
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		// If the player hits the trigger...
		if(col.gameObject.tag == "Player")
		{
			// .. stop the camera tracking the player
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>().enabled = false;

			// .. stop the Health Bar following the player
			if(GameObject.FindGameObjectWithTag("HealthBar").activeSelf)
			{
				GameObject.FindGameObjectWithTag("HealthBar").SetActive(false);
			}

			// ... instantiate the splash where the player falls in.
			Instantiate(splash, col.transform.position, transform.rotation);

		//	score = GetComponent<Score> ().score;
			score = scoremgr.GetComponent<Score>().score;
			if (score > bestScore) {
				bestScore = score;
				PlayerPrefs.SetInt("bestScore", bestScore);
			}
			// ... destroy the player.
			Destroy (col.gameObject);
			// ... reload the level.
			StartCoroutine("ReloadGame");
		}
		else
		{
			// ... instantiate the splash where the enemy falls in.
			Instantiate(splash, col.transform.position, transform.rotation);

			// Destroy the enemy.
			Destroy (col.gameObject);	
		}
	}

	IEnumerator ReloadGame()
	{			
		// ... pause briefly
		TextMsg.SetActive (true);
		yield return new WaitForSeconds(2);

		// ... and then reload the level.
		//Application.LoadLevel(Application.loadedLevel);
		Application.LoadLevel ("gamemenu");
	}
}
