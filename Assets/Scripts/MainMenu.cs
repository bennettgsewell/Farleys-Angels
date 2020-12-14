using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	void OnGUI () {

		if(GUI.Button(new Rect(100,(Screen.height / 2 - 100),120,50), "Play Game")) {
			Application.LoadLevel("level_1");
		}
		if(GUI.Button(new Rect(100,(Screen.height / 2),120,50), "Instructions")) {
			Application.LoadLevel("instructions");
		}
		if(GUI.Button(new Rect(100,(Screen.height / 2 + 100),120,50), "Quit")) {
			Application.Quit();
		}
	}
}
