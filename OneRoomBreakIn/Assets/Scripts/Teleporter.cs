using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Vector2 basic;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = new Vector3(basic.x,basic.y,0);
    }
}
