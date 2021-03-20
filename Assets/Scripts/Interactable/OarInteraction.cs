using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OarInteraction : Interactable
{
    [SerializeField] private bool isLeft;
    [SerializeField] private bool isUsedLeft;
    [SerializeField] private bool isUsedRight;
    [SerializeField] private Rigidbody rbBoat;
    [SerializeField] private float speed;
    public GameObject[] players;
    public GameObject seat;

    public override void Interact()
    {
        if (isLeft)
        {
            isUsedLeft = true;
            Debug.Log("left");
        }
        else
        {
            isUsedRight = true;
            Debug.Log("right");
        }
    }

    private void Start()
    {
        rbBoat = GetComponentInParent<Rigidbody>();   
    }

    private void Update()
    {
        if (isUsedLeft && isLeft)
        {
            if (Vector3.Distance(players[0].transform.position, seat.transform.position) < Vector3.Distance(players[1].transform.position, seat.transform.position))
            {
                players[0].transform.position = seat.transform.position;
            }
            else
            {
                players[1].transform.position = seat.transform.position;
            }
        }


        if (isUsedLeft)
        {
            Debug.Log("Zajête kurwa nie widzisz");
        }
    }

   
}
