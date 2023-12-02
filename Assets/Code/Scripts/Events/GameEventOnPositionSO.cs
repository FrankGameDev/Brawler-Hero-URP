using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Game Event On Position")]
public class GameEventOnPositionSO : ScriptableObject
{
    public UnityAction<Vector3> onEventRequested;
    
    public void RaiseEvent(Vector3 pos)
    {
        onEventRequested?.Invoke(pos);
    }

}
