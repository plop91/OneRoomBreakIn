using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbo : MonoBehaviour
{
    private DialogueTrigger dt;
    private SpriteRenderer sr;
    public GameObject money; 
    void Start()
    {
        dt = GetComponent<DialogueTrigger>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (dt.dialogue.rewardGiven[0])
        {
            StartCoroutine(destroyCash());
            sr.enabled = false;
        }
    }
    IEnumerator destroyCash()
    {
        yield return new WaitForSeconds(2);
        Destroy(money);
    }
}
