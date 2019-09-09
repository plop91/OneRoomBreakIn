using System.Collections.Generic;
using UnityEngine;

public class ConditionManager : MonoBehaviour
{
    #region Singleton
    public static ConditionManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one conditionmanager!!!!!!");
            return;
        }
        instance = this;
    }
    #endregion



    public List<Condition> conditions = new List<Condition>();

    public void Add(Condition condition)
    {
        conditions.Add(condition);
    }
    public void Remove(Condition condition)
    {
        conditions.Remove(condition);
    }
    public void MarkComplete(string _name)
    {
        conditions.Find(x => x.name.Contains(_name)).SetComplete();
    }
    public bool GetComplete(string _name)
    {
        return conditions.Find(x => x.name.Contains(_name)).isComplete;
    }
    public void TestMarkComplete()
    {
        MarkComplete("Test");
        Debug.Log(conditions.Find(x => x.name.Contains("Test")).isComplete + "**Location.ConditionManagerLN42**");
    }
    public void CheckConditions(Item item)
    {
        foreach(string s in item.fulfilledCondition)
        {
            for (int i = 0; i < ConditionManager.instance.conditions.Count; i++)
            {
                if (ConditionManager.instance.conditions[i].name.Contains(s))
                {
                    ConditionManager.instance.conditions[i].isComplete = true;
                }
            }
        }

    }
}
