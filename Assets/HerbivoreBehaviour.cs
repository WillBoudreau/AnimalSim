using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HerbivoreBehaviour : AnimalBehaviour
{
    public NavMeshAgent agent;
    
    public float speed = 2f;
    public float detectionRadius = 5f;
    public float runAwayRadius = 10f;

    public GameObject[] Plants;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Plants = GameObject.FindGameObjectsWithTag("Plant");
    }

    // Update is called once per frame
    void Update()
    {
        FindFood();
        RunAway();
    }
    public override void  FindFood()
    {
        foreach (GameObject plant in Plants)
        {
            if (Vector3.Distance(plant.transform.position, transform.position) < detectionRadius)
            {
                agent.SetDestination(plant.transform.position);
            }
        }
    }
    public override void RunAway()
    {

    }
}
