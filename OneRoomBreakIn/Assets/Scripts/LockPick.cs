using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockPick : MonoBehaviour
{
    public GameObject clerk;
    public DialogueTrigger dt;
    public Player player;
    private void Update()
    {
            if (dt.dialogue.rewardGiven[0])
            {
                player.AddKey("HotelInside");
                player.AddKey("Tech");
                Destroy(gameObject);
            }
        
    }
}
