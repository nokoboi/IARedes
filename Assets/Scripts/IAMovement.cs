using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IAMovement : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public GameObject player;

    public GameObject destination1;
    public GameObject destination2;

    public Transform path;
    public Vector3 destination;
    public int childrenIndex;
    // Start is called before the first frame update
    void Start()
    {
        destination = destination1.transform.position;
        GetComponent<NavMeshAgent>().SetDestination(destination);
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
    }

    public void Patrol()
    {
        if (Vector3.Distance(transform.position, destination) < 1.5f)
        {
            childrenIndex++;
            childrenIndex = childrenIndex % path.childCount;

            destination = path.GetChild(childrenIndex).position;
            GetComponent<NavMeshAgent>().SetDestination(destination);
        }

        if (CanSeePlayer())
        {
            enemyAgent.SetDestination(player.transform.position);
        }
    }

    public bool CanSeePlayer()
    {
        // Comprueba si el jugador está dentro del campo de visión y alcance del enemigo
        Vector3 direction = player.transform.position - transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {
            if (hit.collider.gameObject == player)
            {
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("He colisionado con el jugador");
        }
    }
}
