using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public TMP_Text optionText1;
    public GameObject buttonGO1;
    public TMP_Text optionText2;
    public GameObject buttonGO2;
    public TMP_Text optionText3;
    public GameObject buttonGO3;

    public Animator animator;

    public Queue<string> sentences;
    private Player player;
    private Dialogue lastDialogue;

    private Inventory inventory;
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateDialouge;
        player = FindObjectOfType<Player>();
        sentences = new Queue<string>();
        buttonGO1.SetActive(false);
        buttonGO2.SetActive(false);
        buttonGO3.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        lastDialogue = dialogue;
        animator.SetBool("IsOpen", true);
        nameText.text = dialogue.name;
        if (dialogue.missions.Length > 0)
        {
            optionText1.text = dialogue.missions[0];
            buttonGO1.SetActive(true);
            if (dialogue.missions.Length > 1)
            {
                optionText2.text = dialogue.missions[1];
                buttonGO2.SetActive(true);
                if (dialogue.missions.Length > 2)
                {
                    optionText3.text = dialogue.missions[2];
                    buttonGO3.SetActive(true);
                }
                else
                {
                    buttonGO3.SetActive(false);
                    optionText3.text = "";
                }
            }
            else
            {
                buttonGO2.SetActive(false);
                buttonGO3.SetActive(false);
                optionText2.text = "";
                optionText3.text = "";
            }
        }
        else
        {
            optionText1.text = "";
            optionText2.text = "";
            optionText3.text = "";
            buttonGO1.SetActive(false);
            buttonGO2.SetActive(false);
            buttonGO3.SetActive(false);
        }

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void UpdateDialouge()
    {
        
    }
    public void DisplayNextSentence()
    {
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
    public void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
    public void PressedButton1()
    {
        bool complete = false;
        bool hasCondition = true;
        //Debug.Log
        try
        {
            complete = ConditionManager.instance.GetComplete(lastDialogue.requirements[0].name);
        }
        catch
        {
            hasCondition = false;
        }
        Debug.Log(complete + "   " + hasCondition);
        if (complete)
        {
            try
            {
                if (lastDialogue.rewardGiven[0] != true)
                {
                    Inventory.instance.Add(lastDialogue.rewards[0]);
                    ConditionManager.instance.CheckConditions(lastDialogue.rewards[0]);
                    lastDialogue.rewardGiven[0] = true;
                }
            }
            catch
            {

            }

        }else if (!hasCondition)
        {
            try
            {
                if (lastDialogue.rewardGiven[0] != true)
                {
                    Inventory.instance.Add(lastDialogue.rewards[0]);
                    ConditionManager.instance.CheckConditions(lastDialogue.rewards[0]);
                    lastDialogue.rewardGiven[0] = true;
                    //Debug.Log(lastDialogue.rewards[0].name + "**Location.DialogueLN120**");
                }
            }
            catch
            {

            }
        }
    }
    public void PressedButton2()
    {
        bool complete = false;
        bool hasCondition = true;
        try
        {
            complete = ConditionManager.instance.GetComplete(lastDialogue.requirements[1].name);
        }
        catch
        {
            hasCondition = false;
        }

        if (complete)
        {
            try
            {
                if (lastDialogue.rewardGiven[1] != true)
                {
                    Inventory.instance.Add(lastDialogue.rewards[1]);
                    ConditionManager.instance.CheckConditions(lastDialogue.rewards[1]);
                    lastDialogue.rewardGiven[1] = true;
                }
            }
            catch
            {

            }

        }
        else if (!hasCondition)
        {
            try
            {
                if (lastDialogue.rewardGiven[1] != true)
                {
                    Inventory.instance.Add(lastDialogue.rewards[1]);
                    ConditionManager.instance.CheckConditions(lastDialogue.rewards[1]);
                    lastDialogue.rewardGiven[1] = true;
                    //Debug.Log(lastDialogue.rewards[0].name + "**Location.DialogueLN156**");
                }
            }
            catch
            {

            }
        }
    }
    public void PressedButton3()
    {
        bool complete = false;
        bool hasCondition = true;
        try
        {
            complete = ConditionManager.instance.GetComplete(lastDialogue.requirements[2].name);
        }
        catch
        {
            hasCondition = false;
        }

        if (complete)
        {
            try
            {
                if (lastDialogue.rewardGiven[2] != true)
                {
                    Inventory.instance.Add(lastDialogue.rewards[2]);
                    ConditionManager.instance.CheckConditions(lastDialogue.rewards[2]);
                    lastDialogue.rewardGiven[2] = true;
                }
            }
            catch
            {

            }

        }
        else if (!hasCondition)
        {
            try
            {
                if (lastDialogue.rewardGiven[2] != true)
                {
                    Inventory.instance.Add(lastDialogue.rewards[2]);
                    ConditionManager.instance.CheckConditions(lastDialogue.rewards[2]);
                    lastDialogue.rewardGiven[2] = true;
                    //Debug.Log(lastDialogue.rewards[0].name + "**Location.DialogueLN192**");
                }
            }
            catch
            {

            }
        }
    }
}
