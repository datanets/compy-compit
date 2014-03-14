using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	const int buttonWidth   = 80;
	const int buttonHeight  = 40;
	
	void OnGUI()
	{
		// draw "start game" button
		if (
			GUI.Button(
				new Rect(
					Screen.width / 2 - (buttonWidth / 2),
					(2 * Screen.height / 3.53f) - (buttonHeight / 2),
					buttonWidth,
					buttonHeight
				),
				"Play"
			)
		) {
			Application.LoadLevel("Stage1");
		}
			
	}
}
