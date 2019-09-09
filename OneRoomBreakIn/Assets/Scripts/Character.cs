using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    public bool canMove;
    public int enemySpeed;
    private float speedModifier = .5f;
    public float XMoveDirection;
    public float YMoveDirection;
    Rigidbody2D rb;
    private bool setupComplete = false;

    public List<Vector2> path = new List<Vector2>();
    //private Vector2 location;
    private Vector2 destination;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        //path.Add(new Vector2(rb.position.x, rb.position.y));
        setupComplete = true;
    }
    void FixedUpdate()
    {
        if (setupComplete)
        {
            if (path.Count > 0)
            {
                if (canMove)
                {
                    if (destination.x == transform.position.x && destination.y == transform.position.y)
                    {
                        ReachedDestination();
                    }
                    else
                    {
                        Move();
                    }
                }
            }
        }
    }
    public void Move()
    {
        rb.MovePosition((rb.position - CalculateVector())*enemySpeed * Time.fixedDeltaTime);
    }
    private Vector2 CalculateVector()
    {
        float movex = Mathf.Clamp(rb.position.x -destination.x,-speedModifier, speedModifier);
        float movey = Mathf.Clamp(rb.position.y -destination.y,-speedModifier, speedModifier);
        XMoveDirection = movex;
        YMoveDirection = movey;

        //Debug.Log("x:" + movex + " y:" + movey);
        Vector2 pathTo = new Vector2(movex,movey);
        return pathTo;
    }
    public void MoveTo(Vector2 dest)
    {
        destination = dest;
        //location = GetVectorFromPosition();
    }
    //public Vector2 GetVectorFromPosition()
    //{
    //    return new Vector2(transform.position.x, transform.position.y);
    //}
    public void ReachedDestination()
    {
        Debug.Log("PathReached");
        path.Remove(destination);
        destination = path[0];
        MoveTo(destination);
    }
}
