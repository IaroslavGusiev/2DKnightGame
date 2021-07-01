using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventReciver : MonoBehaviour
{
    public EventSender _sender;

    // Start is called before the first frame update
    void Start()
    {
        _sender.OnFloatSend += _sender_OnFloatSend;
    }

    private void _sender_OnFloatSend(float obj)
    {
        Debug.Log(obj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
