[System.Serializable]
public class Condition
{
    public string name;
    public bool isComplete;
    public Condition(string _name, bool _isComplete)
    {
        name = _name;
        isComplete = _isComplete;
    }
    public void SetComplete()
    {
        isComplete = true;
    }
}
