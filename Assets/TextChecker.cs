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

public class TextChecker : MonoBehaviour {
	public KeyText keyText;
	public Text dispUserText;
	public InputField inText;
	public Text scoreText;
	public Text warningText;

	public GameObject RetryButton;
	public GameObject NewTextButton;


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
					score++;
					hits += keyword + "\n";
					matchedWords.Add (keyword.ToLower());
				}
			}

		}
		string exactMatchPattern = "^" + keyText.Sentence + "$";
		bool isExactMatch = Regex.IsMatch (inText.text, exactMatchPattern);

		string exactMatchLog = isExactMatch ? "\nExact Match! +2" : "";
		score = isExactMatch ? score + 2 : score;

		scoreText.text ="Hits:\n" + hits + exactMatchLog + "\nScore:" + score.ToString ();

		RetryButton.SetActive (true);
		NewTextButton.SetActive (true);

	}

	bool SanitizeInput(string text){
		Regex rgx = new Regex (@"^[a-zA-Z\s\.\?',]+$");
		bool isClean = rgx.IsMatch (text);
		return isClean;
	}

	public void Retry()
	{
		this.initTexter (keyText, dispUserText.text);
	}
		
}
