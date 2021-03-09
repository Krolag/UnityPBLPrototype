using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Script is responsible for camera movement and both players staing in the viewport
[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform PlayerOne, PlayerTwo;
    public Vector3 Offset; // Adds position to the center point
    public float SmoothAmount; // How smooth camera moves
    public float MinZoom, MaxZoom; 
    public float ZoomLimiter; // How fast zoom react

    private Vector3 velocity; // Need for the SmoothDamp function
    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }
    
    // Updating position of the camera in the center between both players and zooming
    void LateUpdate()
    {
        // Calculating boundaries around players
        var bounds = new Bounds(PlayerOne.position, Vector3.zero);
        bounds.Encapsulate(PlayerTwo.position);
        
        // Moving camera with smooth
        transform.position = Vector3.SmoothDamp(transform.position, bounds.center + Offset, ref velocity, SmoothAmount);
        
        // Calculate distance between players
        float distance = Mathf.Sqrt(
            Mathf.Pow(bounds.max.x - bounds.min.x, 2) +
            Mathf.Pow(bounds.max.z - bounds.min.z, 2));
        
        // Zooming the camera dependent on distance
        camera.fieldOfView = Mathf.Lerp(MinZoom, MaxZoom, distance / ZoomLimiter);
        
        // Debug.Log("distance : " + distance); // Testing max distance purposes
    }
}