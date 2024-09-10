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
    public float HungerRate = 1f;

    public GameObject[] Plants;
    public GameObject[] Predators;


    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        Direction = Random.insideUnitSphere * 10f;
        Destination = transform.position + Direction;
        agent.SetDestination(Destination);
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log("Direction" + Direction);
        // Debug.Log("Destination" + Destination);
        HungerMeter -= HungerRate * Time.deltaTime;
        Predators = GameObject.FindGameObjectsWithTag("Predator");
        Plants = GameObject.FindGameObjectsWithTag("Plant");
        //RunAway();
        Move();
        Herd();
        if(HungerMeter <= 50 && !LookingForFood)
        {
            FindFood();
        }
    }
    public override void Move()
    {
        Debug.Log("Remaining Distance" + agent.remainingDistance);
        if(agent.remainingDistance < 1f)
        {
            Direction = Random.insideUnitSphere * 10f;
            Destination = transform.position + Direction;
            agent.SetDestination(Destination);
        }
    }
    public override void  FindFood()
    {
        LookingForFood = true;
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
        Debug.Log("Herd");
        foreach (GameObject herbivore in GameObject.FindGameObjectsWithTag("Herbivore"))
        {
            if (Vector3.Distance(herbivore.transform.position, transform.position) < herdRadius && Vector3.Distance(herbivore.transform.position, transform.position) > 5f)
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
