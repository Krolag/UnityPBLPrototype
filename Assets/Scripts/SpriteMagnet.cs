using UnityEngine;

public class SpriteMagnet : MonoBehaviour
{
    private float _range = 4;
    void Update()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject player in players)
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
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<Points>().AddPoint();
            Destroy(this.gameObject);
        }             
    }
}
