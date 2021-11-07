using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator; //DialogueBox Close and Open animation

    [Tooltip("Speed at which letters are written ")]
    public float typeSpeed = 0.01f;

    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach(string sentence in dialogue.senteces)
        {
            sentences.Enqueue(sentence);
        }

        animator.SetBool("isOpen", true);
        nameText.text = dialogue.name;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines(); //Stops all coroutines of this script (Dialogue typing)
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);

        }
    }

    private void EndDialogue()
    {
        animator.SetBool("isOpen", false);
    }
}
