using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockBoat : MonoBehaviour
{
    public GameObject dockPosition;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Boat"))
        {
            collision.transform.rotation = Quaternion.identity;
            collision.transform.position = dockPosition.transform.position;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.CompareTag("Boat"))
        {
            collision.transform.rotation = Quaternion.identity;
            collision.transform.position = dockPosition.transform.position;
        }
    }
}
