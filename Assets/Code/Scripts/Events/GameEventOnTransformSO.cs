using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/GameEventOnTransform")]
public class GameEventOnTransformSO : ScriptableObject
{
    public UnityAction<Transform> onEventRequested;
    
    public void RaiseEvent(Transform pos)
    {
        onEventRequested?.Invoke(pos);
    }

}
