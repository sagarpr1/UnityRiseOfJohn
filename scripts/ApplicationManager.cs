using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour {
	

	public void Quit () 
	{
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}

	public void Start(){
	}

	public void LoadMission(){
		Application.LoadLevel ("mission");
	}

	public void LoadGame(){
		Application.LoadLevel ("towerdefence");
	}

	public void SaveScore(){
		Application.LoadLevel("savescore");
	}
}
