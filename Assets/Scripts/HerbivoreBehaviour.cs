using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HerbivoreBehaviour : AnimalBehaviour
{
    public NavMeshAgent agent;
    
    public float speed = 2f;
    public float detectionRadius = 5f;
    public float herdRadius = 10f;
    public float runAwayRadius = 10f;

    public GameObject[] Plants;
    public GameObject[] Predators;


    // Start is called before the first frame update
    void Start()
    {
        // for (int i = 0; i < numHerb; i++)
        // {
        //     GameObject herbivore = Instantiate(gameObject, transform.position, Quaternion.identity);
        //     HerbivoreBehaviour herbivoreBehaviour = herbivore.GetComponent<HerbivoreBehaviour>();
        // }
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Plants = GameObject.FindGameObjectsWithTag("Plant");
        Predators = GameObject.FindGameObjectsWithTag("Predator");
        //Move();
        FindFood();
        RunAway();
        Herd();
    }
    public override void Move()
    {
        agent.SetDestination(transform.position + Random.insideUnitSphere * 5f);
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
    public void Herd()
    {
        foreach (GameObject herbivore in GameObject.FindGameObjectsWithTag("Herbivore"))
        {
            if (Vector3.Distance(herbivore.transform.position, transform.position) < herdRadius)
            {
                agent.SetDestination(herbivore.transform.position);
            }
        }
    }
    public override void RunAway()
    {
        foreach (GameObject predator in Predators)
        {
            if (Vector3.Distance(predator.transform.position, transform.position) < runAwayRadius)
            {
                agent.SetDestination(transform.position - predator.transform.position);
            }
        }
    }
}
