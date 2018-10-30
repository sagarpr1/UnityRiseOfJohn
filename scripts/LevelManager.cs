using UnityEngine;
using System.Collections;



public class LevelManager : MonoBehaviour {
	
	///*************************************************************************///
	/// Main Menu Controller.
	/// This class handles all touch events on buttons, and also updates the 
	/// player status on screen.
	///*************************************************************************///
	
	private float buttonAnimationSpeed = 9;		//speed on animation effect when tapped on button
	private bool canTap = true;					//flag to prevent double tap
	public AudioClip tapSfx;					//tap sound for buttons click
	
	//Reference to GameObjects
	public GameObject availableGem;				//UI 3d text object
	public GameObject btnMed;	
	public GameObject lockedMed;	
	public GameObject btnHard;	
	public GameObject lockedHard;
	public GameObject MedUnlockTxt;	
	public GameObject HardUnlockTxt;	
	private bool MedAlreadyUnlock = false;
	private bool HardAlreadyUnlock = false;
	//private bool MedUnlock = false;
	//private bool HardUnlock = false;


	//*****************************************************************************
	// Init. Updates the 3d texts with saved values fetched from playerprefs.
	//*****************************************************************************
	void Awake (){
		availableGem.GetComponent<TextMesh>().text = PlayerPrefs.GetInt("availableGem").ToString();
	}


	//*****************************************************************************
	// FSM
	//*****************************************************************************
	void Update (){	
		if(canTap) {
			StartCoroutine(tapManager());
		}

		if(PlayerPrefs.GetInt("availableGem") > 49) {
			StartCoroutine(unlockMed());
		}

		if(PlayerPrefs.GetInt("availableGem") > 77) {
			StartCoroutine(unlockHard());
		}
	}


	//*****************************************************************************
	// This function monitors player touches on menu buttons.
	// detects both touch and clicks and can be used with editor, handheld device and 
	// every other platforms at once.
	//*****************************************************************************
	private RaycastHit hitInfo;
	private Ray ray;
	IEnumerator tapManager (){
		
		//Mouse of touch?
		if(	Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Ended)  
			ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
		else if(Input.GetMouseButtonUp(0))
			ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		else
			yield break;
		
		if (Physics.Raycast(ray, out hitInfo)) {
			GameObject objectHit = hitInfo.transform.gameObject;
			switch(objectHit.name) {
				
			

			case "btnEasy":
				playSfx(tapSfx);							//play touch sound
				StartCoroutine(animateButton(objectHit));	//touch animation effect
				yield return new WaitForSeconds(1.0f);		//Wait for the animation to end
				Application.LoadLevel("Game");				//Load the next scene
				break;

			case "btnMed":
				playSfx(tapSfx);							//play touch sound
				StartCoroutine(animateButton(objectHit));	//touch animation effect
				yield return new WaitForSeconds(1.0f);		//Wait for the animation to end
				Application.LoadLevel("GameMed");				//Load the next scene
				break;

			case "btnHard":
				playSfx(tapSfx);							//play touch sound
				StartCoroutine(animateButton(objectHit));	//touch animation effect
				yield return new WaitForSeconds(1.0f);		//Wait for the animation to end
				Application.LoadLevel("GameHard");				//Load the next scene
				break;
				
			case "btnHome":
				playSfx(tapSfx);
				StartCoroutine(animateButton(objectHit));
				yield return new WaitForSeconds(1.0f);
				Application.LoadLevel("Menu");
				break;	
			}	
		}
	}

	
	//*****************************************************************************
	// This function animates a button by modifying it's scales on x-y plane.
	// can be used on any element to simulate the tap effect.
	//*****************************************************************************
	IEnumerator animateButton ( GameObject _btn  ){
		canTap = false;
		Vector3 startingScale = _btn.transform.localScale;	//initial scale	
		Vector3 destinationScale = startingScale * 1.1f;	//target scale
		
		//Scale up
		float t = 0.0f; 
		while (t <= 1.0f) {
			t += Time.deltaTime * buttonAnimationSpeed;
			_btn.transform.localScale = new Vector3(Mathf.SmoothStep(startingScale.x, destinationScale.x, t),
			                                        _btn.transform.localScale.y,
			                                        Mathf.SmoothStep(startingScale.z, destinationScale.z, t));
			yield return 0;
		}
		
		//Scale down
		float r = 0.0f; 
		if(_btn.transform.localScale.x >= destinationScale.x) {
			while (r <= 1.0f) {
				r += Time.deltaTime * buttonAnimationSpeed;
				_btn.transform.localScale = new Vector3(Mathf.SmoothStep(destinationScale.x, startingScale.x, r),
				                                        _btn.transform.localScale.y,
				                                        Mathf.SmoothStep(destinationScale.z, startingScale.z, r));
				yield return 0;
			}
		}
		
		if(r >= 1)
			canTap = true;
	}


	//*****************************************************************************
	// Play sound clips
	//*****************************************************************************
	void playSfx ( AudioClip _clip  ){
		GetComponent<AudioSource>().clip = _clip;
		if(!GetComponent<AudioSource>().isPlaying) {
			GetComponent<AudioSource>().Play();
		}
	}

	IEnumerator unlockMed() {
		if (MedAlreadyUnlock == false) {
			MedAlreadyUnlock = true;

		}
		//MedUnlock = true;
		btnMed.SetActive(true);
		MedUnlockTxt.SetActive(false);
		lockedMed.SetActive(false);
		yield return 0;

	}

	IEnumerator unlockHard() {
		if (HardAlreadyUnlock == false) {
			HardAlreadyUnlock = true;

		}
		//HardUnlock = true;
		btnHard.SetActive(true);
		HardUnlockTxt.SetActive(false);
		lockedHard.SetActive(false);
		yield return 0;
		
	}


	}