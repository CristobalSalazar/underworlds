using UnityEngine;

public class Stairs : StaticTile, Interactable
{
  void Start()
  {
      StaticTile.AddTile(transform.position, this);
  }

  public void Interact(object interactor)
  {
      GameEvents.Emit("NextLevel");
  }
}
