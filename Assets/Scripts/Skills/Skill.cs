using UnityEngine;

public class Skill : ScriptableObject
{
    public string skillName;
    public int actionCost;
    public Sprite image;
    public Transform transform;
    public virtual void Cast() {}
    public virtual void Init() {}
}