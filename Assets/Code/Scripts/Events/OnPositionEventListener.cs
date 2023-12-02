using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PositionEvent : UnityEvent<Vector3> { };

public class OnPositionEventListener : MonoBehaviour
{
    [SerializeField] private GameEventOnPositionSO _channel = default;

    public PositionEvent response;

    private void OnEnable()
    {
        _channel.onEventRequested += Respond;
    }

    private void OnDisable()
    {
        _channel.onEventRequested -= Respond;
    }

    public void Respond(Vector3 pos)
    {
        response.Invoke(pos);
    }
}
