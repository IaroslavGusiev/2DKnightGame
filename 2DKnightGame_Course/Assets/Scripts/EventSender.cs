using System;
using UnityEngine;

public class EventSender : MonoBehaviour
{
    public event Action<float> OnFloatSend;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnFloatSend.Invoke(5.5f);
        }
    }
}
