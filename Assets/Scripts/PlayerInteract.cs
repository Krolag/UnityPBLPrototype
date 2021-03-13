using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private float _interactionRange = 1.5f;
    void Update()
    {
        Interactable[] interactableObjects = FindObjectsOfType<Interactable>();

        Interactable closest = closestInteractable(interactableObjects).Item1; // find closest object to interact with
        float distance = closestInteractable(interactableObjects).Item2;    // and distance between object and player

        if (distance < _interactionRange)
        {
            ///TODO button interaction
            closest.Interact();
        }

    }

    private (Interactable,float) closestInteractable(Interactable[] interactableObjects)
    {
        float tempDistance = Mathf.Infinity;
        Interactable closestInteractable = null;

        foreach (Interactable obj in interactableObjects)
        {
            float distance = Vector3.Distance(transform.position, obj.GetComponent<Collider>().ClosestPointOnBounds(transform.position));

            if (distance < tempDistance)
            {
                closestInteractable = obj;
                tempDistance = distance;
            }
        }

        return (closestInteractable,tempDistance);
    }

}
