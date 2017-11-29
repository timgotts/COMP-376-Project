using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour {

	float TextDelay = 0.04f;
	private string currentText;
	[SerializeField] string text;
	[SerializeField] public Text content;


	// Use this for initialization
	void Start () {

		StartCoroutine(ShowText(text, content));
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator ShowText(string text, Text content)
	{
		for (int i = 0; i <= text.Length; i++)
		{
			 currentText = text.Substring(0, i);
			 content.text = currentText.ToUpper();
			 yield return new WaitForSeconds(TextDelay);
		}
	   }
}
