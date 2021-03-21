using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    public GameObject boat;
    public float xOffset;
    public float zOffset;
    public OarInteraction oar1, oar2;
    public float time;
    public float timeLeft;
    public Transform[] island;

    private Vector3 lastBoatPosition;
    private bool kill;
    private bool endGame;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            kill = true;
        }
    }

    private void Start()
    {
        timeLeft = time;
    }

    private void Update()
    {
        bool isNearIsland = false;

        if (!endGame)
        {
            for (int i = 0; i < 4; i++)
            {
                if (Vector3.Distance(boat.transform.position, island[i].position) < 50)
                {
                    isNearIsland = true;
                }
                else
                {
                    isNearIsland = false;
                }
            }

            if ((oar1.GetIsSeating() || oar2.GetIsSeating()) && !isNearIsland)
            {
                timeLeft -= Time.deltaTime;
                if (time - timeLeft < 2)
                {
                    lastBoatPosition = boat.transform.position;
                }

                if (Vector3.Distance(boat.transform.position, lastBoatPosition) < 0.2)
                {
                    if (timeLeft <= 0)
                    {
                        endGame = true;
                    }
                }
                else
                    timeLeft = time;
            }
            if (!kill)
            {
                this.transform.position = new Vector3(boat.transform.position.x + xOffset, this.transform.position.y, boat.transform.position.z + zOffset);
                var mesh = GetComponentInChildren<SkinnedMeshRenderer>();
                mesh.enabled = false;
            }
        }
        else
        {
            var mesh = GetComponentInChildren<SkinnedMeshRenderer>();
            mesh.enabled = true;
            this.transform.position += Vector3.up / 3;
        }
    }
}
