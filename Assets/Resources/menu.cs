using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour {

	public void GotoMainMenu(){
		Application.LoadLevel ("main_menu");
	}
	public void GoToARCamera(){
		Application.LoadLevel ("bitirme");
	}
	public void ExitApplication(){
		Application.Quit ();
	}
}
