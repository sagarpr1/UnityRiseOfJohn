using UnityEngine;
using System.Collections;

public class ForegroundSpawner : MonoBehaviour
{
	public GameObject foregroundProp;		// The prop to be instantiated.
	public float rangeX;				// The x coordinate of position if it's instantiated on the left.
//	public float rightSpawnPosX;			// The x coordinate of position if it's instantiated on the right.
	public float minSpawnPosY;				// The lowest possible y coordinate of position.
	public float maxSpawnPosY;				// The highest possible y coordinate of position.

	public Transform target;
	public Camera mainCam;
	public float length;
	public float distance;
	public float maxrightX;
	public float waittime;
	public float xincr;
	public bool des=false;

	public int numgen=0;
	public int startindx=0;

	private GameObject[] tran;

	public Transform propInstance;
	void Start ()
	{
		// Set the random seed so it's not the same each game.
		//Random.seed = System.DateTime.Today.Millisecond;

		xincr = maxrightX;
		numgen = 0;
//		propInstance = new Transform[100];
		// Start the Spawn coroutine.
		//StartCoroutine("Spawn");
	}

	/*
	IEnumerator Spawn ()
	{
		// Create a random wait time before the prop is instantiated.
		//float waitTime = Random.Range(minTimeBetweenSpawns, maxTimeBetweenSpawns);
		float waitTime = waittime;

		Transform propInstance;


		// Wait for the designated period.
		yield return new WaitForSeconds(waitTime);

				if (target != null) {
			distance = xincr - target.transform.position.x;//maxrightX = 1000
			
			if (distance < rangeX) { //rangeX = 200;
				// Randomly decide whether the prop should face left or right.
				//bool facingLeft = Random.Range(0,2) == 0;


				// If the prop is facing left, it should start on the right hand side, otherwise it should start on the left.
				float posX = xincr;
				xincr = posX +maxrightX;
				
				// Create a random y coordinate for the prop.
				float posY = Random.Range(minSpawnPosY,maxSpawnPosY);
				
				// Set the position the prop should spawn at.
				Vector3 spawnPos = new Vector3(posX, posY, transform.position.z);
				
				// Instantiate the prop at the desired position.
				 propInstance = Instantiate(foregroundProp, spawnPos, Quaternion.identity) as Transform;
				
				// Restart the coroutine to spawn another prop.
				StartCoroutine(Spawn());
				
				// While the prop exists...
				while(propInstance != null)
				{
					// ... and if it's beyond the left spawn position...
					if(propInstance.position.x < target.position.x - 20){
						// ... destroy the prop.
						Destroy(propInstance.gameObject);
						des = true;
					}
					
					// Return to this point after the next update.
					yield return null;
				}
			}
			else {
				StartCoroutine(Spawn());
			}

		}


	}
	*/

	void FixedUpdate() {
//		Transform propInstance;
		
		
		// Wait for the designated period.
//		yield return new WaitForSeconds(waitTime);
		
		if (target != null) {
			distance = xincr - target.transform.position.x;//maxrightX = 1000
			
			if (distance < rangeX) { //rangeX = 200;
				// Randomly decide whether the prop should face left or right.
				//bool facingLeft = Random.Range(0,2) == 0;
				
				
				// If the prop is facing left, it should start on the right hand side, otherwise it should start on the left.
				float posX = xincr;
				xincr = posX +maxrightX;
				
				// Create a random y coordinate for the prop.
				float posY = Random.Range(minSpawnPosY,maxSpawnPosY);
				
				// Set the position the prop should spawn at.
				Vector3 spawnPos = new Vector3(posX, posY, transform.position.z);
				
				// Instantiate the prop at the desired position.
				propInstance = Instantiate(foregroundProp, spawnPos, Quaternion.identity) as Transform;
				numgen = numgen + 1;

				// Restart the coroutine to spawn another prop.
				//StartCoroutine(Spawn());
				
				// While the prop exists...

				tran = GameObject.FindGameObjectsWithTag("grnd");
				foreach (GameObject tran1 in tran)
					{
						// ... and if it's beyond the left spawn position...
						if(tran1.gameObject.transform.position.x < target.position.x - 60){
							// ... destroy the prop.
							Destroy(tran1);
							des = true;
							//startindx = i;
						}
						
					}

			}
		}
}
}