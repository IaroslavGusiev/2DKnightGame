using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadFallZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Knight enter");
            Knight knight = collision.GetComponent<Knight>();
            if (knight != null)
            {
                knight.Die();
                Destroy(collision.gameObject);
            }
        }
    }
}
