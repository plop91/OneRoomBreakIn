using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    
    private AudioManager am;
    public string audioToTrigger;

    private void Start()
    {
        am = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            //Debug.Log(gameObject.name + "  " + collision.gameObject.name);
            am.StopAll();
            am.Play(audioToTrigger);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            am.StopAll();
            am.Play("MainLoop");
        }
    }
}
