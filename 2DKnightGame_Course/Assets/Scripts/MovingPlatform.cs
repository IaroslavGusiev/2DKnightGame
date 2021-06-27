using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _pointA, _pointB;
    private bool _switching = false;

    void Update()
    {
        MoveTowards();
        if (transform.position == _pointA.transform.position)
        {
            _switching = false;
        }
        else if (transform.position == _pointB.transform.position)
        {
            _switching = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Knight is enter");
            collision.gameObject.transform.SetParent(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.parent = null;
        }
    }

    private void MoveTowards() 
    {
        if (_switching == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointB.position, _speed * Time.deltaTime);
        }
        else if (_switching == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, _pointA.position, _speed * Time.deltaTime);
        }
    }

    //private void MoveUpDown()
    //{
    //    if (_switching == false)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, _pointB.position, _speed * Time.deltaTime);
    //    }
    //    else if (_switching == true)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, _pointA.position, _speed * Time.deltaTime);
    //    }
    //}
}
