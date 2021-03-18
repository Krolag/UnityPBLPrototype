using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private InputAction _interactButton;

    private float _interactionRange = 1.5f;

    void Update()
    {
        Interactable[] interactableObjects = FindObjectsOfType<Interactable>();

        Interactable closest = ClosestInteractable(interactableObjects).Item1; // find closest object to interact with
        float distance = ClosestInteractable(interactableObjects).Item2;    // and distance between object and player

        if (distance < _interactionRange)
        {
            if (_interactButton.triggered)
            {
                closest.Interact();
            }           
        }
    }

    private void OnEnable()
    {
        _interactButton.Enable();
    }
    private void OnDisable()
    {
        _interactButton.Disable();
    }

    private (Interactable,float) ClosestInteractable(Interactable[] interactableObjects)
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
