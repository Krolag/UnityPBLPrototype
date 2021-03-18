using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OarInteraction : Interactable
{
    [SerializeField] private bool isLeft;
    [SerializeField] private bool isUsed;
    [SerializeField] private Rigidbody boat;
    [SerializeField] private float force;

    private void Start()
    {
        boat = GetComponentInParent<Rigidbody>();   
    }

    public override void Interact()
    {
        if (isLeft)
        {
            Debug.Log("left");
            boat.AddForce(new Vector3(-force, 0.0f, force));
            boat.transform.Rotate(Vector3.up, -2);
        }
        if (!isLeft)
        {
            Debug.Log("right");
            boat.AddForce(new Vector3(force, 0.0f, force));
            boat.transform.Rotate(Vector3.up, 2);
        }
    }

    private void Update()
    {
        
    }
}
