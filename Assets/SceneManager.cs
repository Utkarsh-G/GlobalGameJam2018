using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

/*
 *  This class can switch between panels, 
 * and pass along data accordingly. 
 * 
 * It also loads the game initially. 
 * 
 */

public class SceneManager : MonoBehaviour {
	public TextChecker myTexter;
	public GameObject QuoteLoaderPanel;
	public GameObject TextCheckerPanel;
	public GameObject MenuPanel;

	public QuoteLoader quoteLoader;

	public QuoteListSO qList;
	KeyText keyText;

	// Use this for initialization
	void Start () {
		GoToMenu ();
	}

	public void GoToMenu()
	{
		QuoteLoaderPanel.SetActive (false);
		TextCheckerPanel.SetActive (false);
		MenuPanel.SetActive (true);
	}

	public void StartLoader()
	{
		QuoteLoaderPanel.SetActive (true);
		TextCheckerPanel.SetActive (false);
		MenuPanel.SetActive (false);

		QuoteObj myQuote = qList.QuoteList [0];

		keyText = new KeyText (myQuote.Quote, myQuote.Keys, myQuote.Banned);
		quoteLoader.initQuoteLoader (keyText);

	}

	public void FromLoaderToChecker(){

		string userText = quoteLoader.SubmitUserText ();

		if (userText != "") {
			QuoteLoaderPanel.SetActive (false);
			TextCheckerPanel.SetActive (true);
			myTexter.initTexter (keyText, userText);
		}

	}

}
