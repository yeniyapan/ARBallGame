﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splash : MonoBehaviour {

	void Start(){
		StartCoroutine (Example ());
	}
	IEnumerator Example(){
		yield return new WaitForSeconds (3);
		Application.LoadLevel ("loading");
	}
}
