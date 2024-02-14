using UnityEngine;

public class ItemSO : ScriptableObject
{
    public int id;
    [Range(0f, 100f)] public int chanceDrop;
}
