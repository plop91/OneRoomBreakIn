using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private void Start()
    {
        int numberOfRequirements = dialogue.requirements.Length;
        for (int i = 0; i < numberOfRequirements; i++)
        {
            ConditionManager.instance.Add(dialogue.requirements[i]);
            //Debug.Log("condition logged?:" + ConditionManager.instance.conditions.Count + "   " + dialogue.requirements[i].name + "**Location.DialogueTriggerLN12**");
        }

    }
    private void Update()
    {

    }


    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            //dialogue.Update();
            TriggerDialogue();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }

}