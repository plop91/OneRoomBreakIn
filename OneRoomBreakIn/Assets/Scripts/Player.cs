using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private int initalspeed = 5;
    private int speed;
    public bool canMove = true;
    private Vector2 movement;
    private Rigidbody2D rb;
    private CinemachineVirtualCamera cm;
    private SpriteRenderer sr;
    public List<Key> keys = new List<Key>();
    private AudioManager am;
    public Item money;
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
        am.Play("MainLoop");
        rb = GetComponent<Rigidbody2D>();
        cm = FindObjectOfType<CinemachineVirtualCamera>();
        sr = GetComponent<SpriteRenderer>();
        cm.Follow = transform;
        Inventory.instance.Add(money);
    }
    void Update()
    {
        if (canMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            if ((Input.GetAxisRaw("Horizontal") > 0.01 || Input.GetAxisRaw("Horizontal") < 0.01) && (Input.GetAxisRaw("Vertical") > 0.01 || Input.GetAxisRaw("Vertical") < 0.01))
            {
                movement.x = movement.x / 2;
                movement.y = movement.y / 2;
            }
            if (movement.x > 0.01)
            {
                sr.flipX = true;
            }
            else if (movement.x < -0.01)
            {
                sr.flipX = false;
            }
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = initalspeed * 2;
        }
        else
        {
            speed = initalspeed;
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (Inventory.instance.isOpen == false)
            {
                Inventory.instance.OpenInventory();
            }
            else if (Inventory.instance.isOpen == true)
            {
                Inventory.instance.CloseInventory();
            }
        }
        //Debug.Log(inventory.Length);
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            if (rb != null)
            {
                rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            }
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Cop cop = collision.collider.GetComponent<Cop>();
        if (cop != null)
        {

        }
    }
    public void AddKey(string canOpen)
    {
        keys.Add(new Key(canOpen));
    }
    
}
