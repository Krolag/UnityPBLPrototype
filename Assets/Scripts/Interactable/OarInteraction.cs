using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OarInteraction : Interactable
{
    [SerializeField] private bool isSeating;
    [SerializeField] private bool isLeft;
    [SerializeField] private int playerID;
    [SerializeField] private Rigidbody rbBoat;

    [SerializeField] private float force;
    [SerializeField] private float currentForce = 0;

    [SerializeField] private float rotation;
    [SerializeField] private float currentRotation = 0;


    Vector2 movementInput;

    public GameObject[] players;
    public GameObject seat;

    public override void Interact()
    {
        if (isSeating)
            isSeating = false;
        else
            isSeating = true;
    }

    private void Start()
    {
        rbBoat = GetComponentInParent<Rigidbody>();
    }

    public bool GetIsSeating()
    {
        return isSeating;
    }

    private void Update()
    {
        Vector3 boatRowMovement = new Vector3();
        Vector3 boatRowRotation = new Vector3();

        if (isSeating)
        {
            AddToSeat();
            var pInput = players[playerID].GetComponent<PlayerInput>();
            var pInteract = players[playerID].GetComponent<PlayerInteract>();
            movementInput = pInput.MovementVector;

            if (movementInput.y != 0)
            {
                if (pInteract.GetRowButton().triggered)
                {
                    currentForce = force;
                    currentRotation = rotation;
                }

                if (isLeft)
                {
                    if (movementInput.y < 0)
                    {
                        boatRowMovement.z = -currentForce;
                        boatRowRotation.y = currentRotation;
                    }
                    else
                    {
                        boatRowMovement.z = currentForce;
                        boatRowRotation.y = -currentRotation;
                    }
                }
                else
                {
                    if (movementInput.y > 0)
                    {
                        boatRowMovement.z = currentForce;
                        boatRowRotation.y = currentRotation;
                    }
                    else
                    {
                        boatRowMovement.z = -currentForce;
                        boatRowRotation.y = -currentRotation;
                    }
                }
            }
        }

        rbBoat.transform.Translate(boatRowMovement * Time.deltaTime);
        rbBoat.transform.Rotate(boatRowRotation * Time.deltaTime);

        if (currentForce > 0)
            currentForce -= 0.01f;
        if (currentRotation > 0)
            currentRotation -= 0.01f;
    }

    private void AddToSeat()
    {
        if (Vector3.Distance(players[0].transform.position, seat.transform.position) < Vector3.Distance(players[1].transform.position, seat.transform.position))
        {
            players[0].transform.position = seat.transform.position;
            playerID = 0;
        }
        else
        {
            players[1].transform.position = seat.transform.position;
            playerID = 1;
        }
    }
}
