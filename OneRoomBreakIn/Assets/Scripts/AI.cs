using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public bool AIEnabled;
    public Role role;
    public Transform player;

    public LayerMask blockingLayer;
    //private List<Path> bestPath = new List<Path>();
    private List<Path> closedPathList = new List<Path>();
    private List<Path> openPathList = new List<Path>();
    public List<Path> evaluationList = new List<Path>();
    private Path lastMove;
    public Vector2 grid;
    public bool seePlayer;
    public CircleCollider2D vision;
    public int visionDistance;

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    //int currentWaypoint = 0;
    //bool reachedEndOfPath = false;

    Rigidbody2D rb;
    private void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }
    private void Update()
    {
        if (AIEnabled)
        {
            Path bestPath = HappyPath();
            Debug.Log("X: " + bestPath.x + "Y: " + bestPath.y);
            transform.position = new Vector2(bestPath.x, bestPath.y);
            //if (bestPath.Count > 0)
            //{
            //    Path nextMove = bestPath[0];
            //    bestPath.RemoveAt(0);
            //}
        }
    }

    Path itemWithLowestFScore(List<Path> p)
    {
        int min = p[0].f;
        int choice = 0;
        for (int i = 1; i < p.Count; i++)
        {
            if (p[i].f < min)
            {
                min = p[i].f;
                choice = i;
            }
        }
        return p[choice];
    }
    bool doesPathListContain(List<Path> cpl, Path dest)
    {
        if (cpl.Contains(dest))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    Path HappyPath()
    {
        Path destinationSquare = new Path(BlocksToTarget(transform.position, player.position), 0, null, (int)player.position.x, (int)player.position.y);

        evaluationList = GetAdjacentSquares(destinationSquare);

        Path currentSquare = null;
        while (evaluationList.Count > 0)
        {
            currentSquare = itemWithLowestFScore(evaluationList);
            closedPathList.Add(currentSquare);
            evaluationList.Remove(currentSquare);
            // The target has been located
            if (doesPathListContain(closedPathList, destinationSquare))
            {
                return buildPath(currentSquare);
            }
            List<Path> adjacentSquares = GetAdjacentSquares(currentSquare);
            foreach (Path p in adjacentSquares)
            {
                if (doesPathListContain(closedPathList, p))
                    continue; // skip this one, we already know about it
                if (!doesPathListContain(evaluationList, p))
                {
                    openPathList.Add(p);
                }
                else if (p.h + currentSquare.g + 1 < p.f)
                    p.parent = currentSquare;
            }
        }
        Debug.Log("HappyPath Returned Null");
        return null;
    }
    //Simply used because at the end of our loop we have a path with parents in the reverse order.This reverses the list so it's from Enemy to Player
    private Path buildPath(Path p)
    {
        List<Path> bestPath = new List<Path>();
        Path currentLoc = p;
        bestPath.Insert(0, currentLoc);
        while (currentLoc.parent != null)
        {
            currentLoc = currentLoc.parent;
            if (currentLoc.parent != null)
                bestPath.Insert(0, currentLoc);
            else
                lastMove = currentLoc;
        }
        Path nextMove = bestPath[0];
        return nextMove;
    }
    private int BlocksToTarget(Vector2 a, Vector2 b)
    {
        int x = (int)(Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y));
        return x;
    }
    private List<Path> GetAdjacentSquares(Path p)
    {
        List<Path> ret = new List<Path>();
        int _x = p.x;
        int _y = p.y;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                int __x = _x + x; // easier than writing (_x + x) 5 times
                int __y = _y + y; // easier than writing (_y + y) 5 times

                // skip self and diagonal squares
                if ((x == 0 && y == 0) || (x != 0 && y != 0))
                    continue;
                else if (__x < grid.x && __y < grid.y && __x >= 0 && __y >= 0 && !CheckForCollision(new Vector2(_x, _y), new Vector2(__x, __y)))
                    ret.Add(new Path(p.g + 1, BlocksToTarget(new Vector2(__x, __y), target.position), p, __x, __y));
            }
        }
        return ret;
    }
    private bool CheckForCollision(Vector2 start, Vector2 end)
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
        RaycastHit2D hit = Physics2D.Linecast(start, end, blockingLayer);
        this.GetComponent<BoxCollider2D>().enabled = true;
        // trying to walk into a wall, change direction
        if (hit.transform != null && !hit.collider.tag.Equals("Player"))
        {

            return true;
        }
        return false;
    }

    public enum Role
    {
        Bystandard,
        Gaurd,
        Cop,
        ShopOwner,
        HotelOwner,
        TechWorker,
        Scientist,
        Kid
    }

}
//public class Path : object
//{
//    public int g;         // Steps from A to this
//    public int h;         // Steps from this to B
//    public Path parent;   // Parent node in the path
//    public int x;         // x coordinate
//    public int y;         // y coordinate
//    public Path(int _g, int _h, Path _parent, int _x, int _y)
//    {
//        g = _g;
//        h = _h;
//        parent = _parent;
//        x = _x;
//        y = _y;
//    }
//    public int f // Total score for this
//    {
//        get
//        {
//            return g + h;
//        }
//    }
//}
