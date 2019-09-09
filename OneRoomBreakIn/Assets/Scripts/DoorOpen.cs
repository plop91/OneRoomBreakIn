using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public bool open;
    public bool isAutomatic;
    public bool isUnlocked;
    private Animator animator;
    private bool canOpen;
    public string openName;
    private GameObject canOpenText;
    //private AudioManager am;
    private void Start()
    {
        //am = FindObjectOfType<AudioManager>();
        animator = GetComponent<Animator>();
        Transform[] find = GetComponentsInChildren<Transform>();
        foreach (Transform t in find)
        {
            if (t.name.Contains("OpenText"))
            {
                canOpenText = t.gameObject;
                canOpenText.SetActive(false);
            }
        }
    }
    private void Update()
    {
        if (canOpen)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Open();
            }
        }
    }


    public void Open()
    {
        open = true;
        animator.SetBool("isOpen", true);
    }
    public void Close()
    {
        open = false;
        animator.SetBool("isOpen", false);
    }





    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        Character character = collision.gameObject.GetComponent<Character>();

        if (isAutomatic)
        {
            if (isUnlocked)
            {
                //am.Play("MainLoop");
                Open();
                canOpenText.SetActive(false);
            }
        }
        else if (player != null)
        {
            for (int i = 0; i < player.keys.Count; i++)
            {
                if (player.keys[i].canOpen.Contains(openName))
                {
                    canOpen = true;
                    if(!open)
                    canOpenText.SetActive(true);
                }
            }
        }
        else if (character != null)
        {
            Open();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        Character character = collision.gameObject.GetComponent<Character>();

        if (isAutomatic)
        {
            Close();
            canOpenText.SetActive(false);
        }
        if (player != null)
        {
            canOpen = false;
            canOpenText.SetActive(false);
        }

    }
}
