using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogueManager : MonoBehaviour
{

	[SerializeField]
	GameObject Panel;

	public Text nameText;
	public Text dialogueText;

	bool isJeff = true;


	private Queue<string> sentences;

	void Start()
	{
		sentences = new Queue<string>();
	}

	public void StartDialogue(Dialogue dialogue)
	{

		Time.timeScale = 0;


		nameText.text = dialogue.name;

		sentences.Clear();

		foreach (string sentence in dialogue.sentences)
		{
			sentences.Enqueue(sentence);
		}



		DisplayNextSentence();

	}

	public void DisplayNextSentence()
	{
		isJeff = !isJeff;

		if (isJeff)
		{
			name = "Jeff";
			nameText.text = name;
		}
		else
		{
			name = "Uncle";
			nameText.text = name;
		}

		if (sentences.Count == 0)
		{
			EndDialogue();
			return;
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));

	}

	IEnumerator TypeSentence(string sentence)
	{


		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
	}

	void EndDialogue()
	{
		Panel.SetActive(false);
		Time.timeScale = 1;
	}
}
