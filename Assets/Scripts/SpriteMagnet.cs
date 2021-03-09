using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMagnet : MonoBehaviour
{
    private float _range = 4;
    void Update()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            var acctualDistance = Vector3.Distance(player.transform.position, transform.position); //distance between player and sprite

            if (acctualDistance < _range)
            {
                GetComponent<Rigidbody>().AddForce(player.transform.position - transform.position);
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);        
    }
}
