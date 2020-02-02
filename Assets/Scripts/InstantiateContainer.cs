using UnityEngine;

public class InstantiateContainer : MonoBehaviour
{
    [SerializeField] private Transform _setParent;
    void Start()
    {
        transform.DetachChildren();
    }
}
