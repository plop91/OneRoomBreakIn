using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one inventory!!!!!!");
            return;
        }
        instance = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;
    public List<Item> items = new List<Item>();
    private Animator am;
    public bool isOpen = false;
    public GameObject inventoryPanel;
    public Image image;

    private void Start()
    {
        am = inventoryPanel.GetComponent<Animator>();
        image = inventoryPanel.GetComponent<Image>();
        CloseInventory();
    }
    public void OpenInventory()
    {
        isOpen = true;
        image.enabled = true;
        am.SetBool("isOpen", true);
    }
    public void CloseInventory()
    {
        isOpen = false;
        am.SetBool("isOpen", false);
        image.enabled = false;
    }
    public void Add(Item item)
    {
        Debug.Log(item.name);
        if (items.Count >= space)
        {
            Debug.Log("No Room in inventory");
            return;
        }
        items.Add(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        ConditionManager.instance.CheckConditions(item);
    }
    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
    
}
