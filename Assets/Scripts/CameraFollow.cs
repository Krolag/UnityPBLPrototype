using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Script is responsible for camera movement and both players staing in the viewport
public class CameraFollow : MonoBehaviour
{
    public GameObject PlayerOne;
    public GameObject PlayerTwo;
    public float MinY, MaxY; // Minimal and maximal zoom of the camera (MinY - nearest, MaxY - farthest distance on vertical axis)
    public float Multiplier; // How fast the zooming will occur
    private Vector3 pointBetweenPlayers;
    private float distanceBetweenPlayers;

    // Updating position of the camera in the center between both players
    void LateUpdate()
    {
        // Calculating distance between players
        distanceBetweenPlayers = Mathf.Sqrt(
            (Mathf.Pow(PlayerTwo.transform.position.x - PlayerOne.transform.position.x, 2))
            + (Mathf.Pow(PlayerTwo.transform.position.z - PlayerOne.transform.position.z, 2)));
        
        // Calculating center point and zoom (point on y axis, depends on distance between players)
        pointBetweenPlayers.x = (PlayerOne.transform.position.x + PlayerTwo.transform.position.x) / 2;
        pointBetweenPlayers.y = Mathf.Clamp(distanceBetweenPlayers * Multiplier, MinY, MaxY);
        pointBetweenPlayers.z = (PlayerOne.transform.position.z + PlayerTwo.transform.position.z) / 2;
        
        // Assigning vector to the camera position
        transform.position = pointBetweenPlayers;
    }
}