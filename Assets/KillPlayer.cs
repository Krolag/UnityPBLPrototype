using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public GameObject boat;
    public float xOffset;
    public float zOffset;
    public OarInteraction oar1, oar2;

    private bool kill;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            kill = true;
        }
    }

    private void Update()
    {
        if (!kill)
            this.transform.position = new Vector3(boat.transform.position.x + xOffset, this.transform.position.y, boat.transform.position.z + zOffset);
    }
}
