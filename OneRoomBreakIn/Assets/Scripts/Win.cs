using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public Player player;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player testPlayer = collision.GetComponent<Player>();
        if(testPlayer != null)
        {
            player.canMove = false;
            StartCoroutine(WaitAndQuit());
        }
    }
    IEnumerator WaitAndQuit()
    {
        yield return new WaitForSeconds(7);
        Application.Quit();
    }
}
