using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HerbivoreBehaviour : AnimalBehaviour
{
    public NavMeshAgent agent;
    public float HungerMeter = 100f;
    public float speed = 2f;
    public float detectionRadius = 5f;
    public GameObject closestPlant = null;
    public float herdRadius = 10f;
    public float runAwayRadius = 10f;
    public float HungerRate = 10f;
    public GameObject closestHerbivore = null;
    public float ReproduceRate = 10f;

    public GameObject[] Herbivores;

    public GameObject[] Plants;
    public GameObject[] Predators;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        Herbivores = GameObject.FindGameObjectsWithTag("Herbivore");
        Predators = GameObject.FindGameObjectsWithTag("Predator");
        Plants = GameObject.FindGameObjectsWithTag("Plant");
        Hunger();
        if(HungerMeter > 50)
        {
            Reproduce();
            Move();
            Herd();
        }
        else if(HungerMeter <= 50)
        {
            FindFood();
        }
        Death();
    }
    public override void Reproduce()
    {
        ReproduceRate -= Time.deltaTime;
        Debug.Log("Reproduce rate " + ReproduceRate);
        if(ReproduceRate <= 0)
        {
            foreach (GameObject herbivore in Herbivores)
            {
                if (Vector3.Distance(herbivore.transform.position, transform.position) < 5f)
                {
                    GameObject newHerbivore = Instantiate(gameObject, transform.position, Quaternion.identity);
                }
            }
            ReproduceRate = 10f;
        }
    }
    public override void Move()
    {
        if(agent.remainingDistance < 1f)
        {
            Direction = Random.insideUnitSphere * 10f;
            Destination = transform.position + Direction;
            agent.SetDestination(Destination);
        }
    }
    public void Hunger()
    {
        HungerMeter -= HungerRate * Time.deltaTime;
    }
    public override void FindFood()
    {
        LookingForFood = true;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject plant in Plants)
        {
            float distance = Vector3.Distance(plant.transform.position, transform.position);
            if (distance < detectionRadius && distance < closestDistance)
            {
                closestPlant = plant;
                closestDistance = distance;
            }
        }

        if (closestPlant != null)
        {
            agent.SetDestination(closestPlant.transform.position);
            if (Vector3.Distance(closestPlant.transform.position, transform.position) < 1f)
            {
                Eat(closestPlant);
            }
        }
    }

    public override void Eat(GameObject plant)
    {
        HungerMeter += 50f;
        Destroy(plant);
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
    public override void Death()
    {
        if(HungerMeter <= 0)
        {
            HungerMeter = 0;
            Destroy(gameObject);
        }
    }
}