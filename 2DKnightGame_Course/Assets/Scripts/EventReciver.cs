using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventReciver : MonoBehaviour
{
    public EventSender _sender;

    private void OnEnable()
    {
        _sender.OnFloatSend += _sender_OnFloatSend;
    }

    private void _sender_OnFloatSend(float obj)
    {
        Debug.Log(obj);
    }
}
