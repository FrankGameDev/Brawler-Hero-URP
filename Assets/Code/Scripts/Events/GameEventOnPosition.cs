using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Game Event On Position")]
public class GameEventOnPosition
    : ScriptableObject
{
    public UnityAction<Transform> onEventRaised;

    public List<OnPositionEventListener> listeners = new List<OnPositionEventListener>();

    
    public void Raise(Transform transf)
    {
        onEventRaised?.Invoke(transf);
    }

}
