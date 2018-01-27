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
	public Text dispUserText;
	//public Text inText;
	public InputField inText;
	public Text scoreText;
	public Text warningText;

	public GameObject RetryButton;
	public GameObject NewTextButton;
	//public QuoteListSO qList;

	// Use this for initialization
	void Start () {
		//keyText = new KeyText (qList.QuoteList[0].Quote, qList.QuoteList[0].Keys);
		//Debug.Log (keyText.Sentence);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void initTexter(KeyText inKeyText, string userInput){
		keyText = inKeyText;
		dispUserText.text = userInput;
		warningText.text = "";
		inText.text = "";
		scoreText.text = "";

		RetryButton.SetActive (false);
		NewTextButton.SetActive (false);

	}

	public void CheckTextForKey(){

		warningText.text = "";

		if (!SanitizeInput (inText.text)) {
			warningText.text = "Invalid characters. \nAllowed input: A-Z a-z . ? ' ,";
			return;
		}
			

		int score = 0;
		string hits = "";
		List<string> matchedWords = new List<string> ();



		string pattern = @"[\s\.\?',]";
		string[] words = Regex.Split (inText.text, pattern);

		foreach (string word in words) {
			foreach (string keyword in keyText.KeyWords) {
				if (!matchedWords.Contains(word.ToLower()) && word.ToLower() == keyword.ToLower()) {
					//Debug.Log ("Matched: " + keyword);
					score++;
					hits += keyword + "\n";
					matchedWords.Add (keyword.ToLower());
				}
			}

		}

		scoreText.text = hits + "Score:" + score.ToString ();

		RetryButton.SetActive (true);
		NewTextButton.SetActive (true);

	}

	bool SanitizeInput(string text){
		Regex rgx = new Regex (@"^[a-zA-Z\s\.\?',]+$");
		bool isClean = rgx.IsMatch (text);
		Debug.Log ("Is clean input: " + isClean);
		return isClean;
	}

	public void Retry()
	{
		this.initTexter (keyText, dispUserText.text);
	}
		
}
