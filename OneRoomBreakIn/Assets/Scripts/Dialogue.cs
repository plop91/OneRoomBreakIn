using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
    public string[] missions;
    public Item[] rewards;
    public bool[] rewardGiven;
    public Condition[] requirements;

    public void Update()
    {

    }
}
