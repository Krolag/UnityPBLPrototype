using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject ObjectA;
    public GameObject ObjectB;
    public Vector3 BetweenPlayers;

    // Update is called once per frame
    void LateUpdate()
    {
        BetweenPlayers.x = (ObjectA.transform.position.x + ObjectB.transform.position.x) / 2;
        BetweenPlayers.z = (ObjectA.transform.position.x + ObjectB.transform.position.x) / 2;
        transform.position = BetweenPlayers;
    }
}
