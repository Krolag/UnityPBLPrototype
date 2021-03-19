using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

// Script is responsible for camera movement and both players staing in the viewport
[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform PlayerOne, PlayerTwo, CameraTransform;
    public GameObject LeftCollider, RightCollider, TopCollider, BottomCollider;
    public Vector3 Offset; // Adds position to the center point
    public Vector2 CameraSpacing;
    public float SmoothAmount; // How smooth camera moves
    public float MinZoom, MaxZoom; 
    public float ZoomLimiter; // How fast zoom react

    private Vector3 velocity; // Need for the SmoothDamp function
    private Camera camera;

    void Start()
    {
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        // Viewport to world cords mapping
        // 10 in he third function attribute is the distance between camera and players in the Y axis
        Vector3 cameraBottomLeft = camera.ViewportToWorldPoint(new Vector3(0.0f, 0.0f, Offset.y));
        Vector3 cameraCenter = camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Offset.y));
        Vector3 cameraTopRight = camera.ViewportToWorldPoint(new Vector3(1.0f, 1.0f, Offset.y));
        
        // Updating scale and position of camera border colliders
        LeftCollider.transform.position = new Vector3(cameraBottomLeft.x + CameraSpacing.x, 0f, cameraCenter.z);
        LeftCollider.transform.localScale = new Vector3(0.3f, 1f, cameraTopRight.z - cameraBottomLeft.z);    
        
        RightCollider.transform.position = new Vector3(cameraTopRight.x - CameraSpacing.x, 0f, cameraCenter.z);
        RightCollider.transform.localScale = new Vector3(0.3f, 1f, cameraTopRight.z - cameraBottomLeft.z);
        
        TopCollider.transform.position = new Vector3(cameraCenter.x, 0f, cameraTopRight.z - CameraSpacing.y);
        TopCollider.transform.localScale = new Vector3(cameraTopRight.x - cameraBottomLeft.x, 1f, 0.3f);

        BottomCollider.transform.position = new Vector3(cameraCenter.x, 0f, cameraBottomLeft.z + CameraSpacing.y);
        BottomCollider.transform.localScale = new Vector3(cameraTopRight.x - cameraBottomLeft.x, 1f, 0.3f);
    }

    // Updating position of the camera in the center between both players and zooming
    void LateUpdate()
    {
        // Calculating boundaries around players
        var bounds = new Bounds(PlayerOne.position, Vector3.zero);
        bounds.Encapsulate(PlayerTwo.position);
        
        // Calculate distance between players
        float distance = Mathf.Sqrt(
            Mathf.Pow(bounds.max.x - bounds.min.x, 2) +
            Mathf.Pow(bounds.max.z - bounds.min.z, 2)
        );
        
        // Updating camera Y position
        Offset.y = Mathf.Lerp(MinZoom, MaxZoom, distance / ZoomLimiter);
        
        // Moving camera smoothly
        CameraTransform.position = Vector3.SmoothDamp(transform.position, bounds.center + Offset, ref velocity, SmoothAmount);
    }
}