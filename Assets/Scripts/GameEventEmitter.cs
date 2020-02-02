using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventEmitter : MonoBehaviour
{
  public void EmitEvent(string eventName)
  {
      GameEvents.Emit(eventName);
  }
}
