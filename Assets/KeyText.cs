using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Use this class for the special text. 
 * It holds one sentence
 * + keywords
 * + words banned from the clue text.
 * 
 */

public class KeyText{
	public string Sentence;
	public string[] KeyWords;
	public string[] BannedWords;

	public KeyText(string sent, string[] keys, string[] banned)
	{
		Sentence = sent; 
		KeyWords = keys; 
		BannedWords = banned;
	}
}
