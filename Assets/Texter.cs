using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
/*
 * The class that reads in the
 * text input, and compares it
 * against the KeyText, and returns
 * the score.
 * 
 */

public class Texter : MonoBehaviour {

	public KeyText keyText;
	public Text inText;
	public Text scoreText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CheckTextForKey(){
		int score = 0;
		string hits = "";

		keyText = new KeyText ();

		string pattern = @" ";
		string[] words = Regex.Split (inText.text, pattern);

		foreach (string word in words) {
			foreach (string keyword in keyText.KeyWords) {
				if (word == keyword) {
					Debug.Log ("Matched: " + word);
					score++;
					hits += word + "\n";
				}
			}

		}

		scoreText.text = hits + "Score:" + score.ToString ();
		
	}

}
