using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogue;

    [SerializeField]
    GameObject trigger;

    [SerializeField]
    GameObject Panel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

        trigger.SetActive(false);

        Panel.SetActive(true);

    }
}
