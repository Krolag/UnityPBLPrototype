using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OarInteraction : Interactable
{
    [SerializeField] private bool isSeating;
    [SerializeField] private Rigidbody rbBoat;
    [SerializeField] private float speed;
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

    private void Update()
    {
        if (isSeating)
            AddToSeat();
    }

    private void AddToSeat()
    {
        if (Vector3.Distance(players[0].transform.position, seat.transform.position) < Vector3.Distance(players[1].transform.position, seat.transform.position))
            players[0].transform.position = seat.transform.position;
        else
            players[1].transform.position = seat.transform.position;
    }
}
