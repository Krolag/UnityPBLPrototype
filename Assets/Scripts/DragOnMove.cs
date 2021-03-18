using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragOnMove : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }
}
