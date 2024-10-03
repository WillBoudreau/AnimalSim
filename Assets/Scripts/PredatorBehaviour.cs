using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PredatorBehaviour : AnimalBehaviour
{
    //Variables to influence the Predator's behavior
    public float detectionRadius = 5f;
    public float HungerMeter = 100f;
    public float runAwayRadius = 10f;
    public float speed = 2f;
    public float HungerRate = 1f;
    public NavMeshAgent agent;
    public int HerbCount = 0;

    //Arrays to store all Herbivores and Predators in the scene
    public GameObject[] Herbivores;
    public GameObject[] Predators;
    Renderer PredMat;
    // Start is called before the first frame update
    void Start()
    {
        PredMat = GetComponent<Renderer>();
        PredMat.material.color = Color.red;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectEnviroment();
        Hunger();
        if (HungerMeter < 0)
        {
            Death();
        }
        else if (HungerMeter < 70)
        {
           FindFood();
        }
        else
        {
           Move();
        }
    }
    //Detect the Herbivores and Predators in the scene
    void DetectEnviroment()
    {
        Herbivores = GameObject.FindGameObjectsWithTag("Herbivore");
        Predators = GameObject.FindGameObjectsWithTag("Predator");
    }
    void Hunger()
    {
        HungerMeter -= HungerRate * Time.deltaTime;
    }
    //Move the Predator
    public override void Move()
    {
        if (agent.remainingDistance < 0.5f)
        {
            Direction = Random.insideUnitSphere * 5f;
            Destination = transform.position + Direction;
            agent.SetDestination(Destination);
        }
    }
    //Find food based on the HungerMeter
    public override void FindFood()
    {
        if(HungerMeter <= 40)
        {
            FindPred();
        }
        int herbivoresInRadius = 0;
        foreach (GameObject herbivore in Herbivores)
        {
            if (Vector3.Distance(herbivore.transform.position, transform.position) < detectionRadius)
            {
            herbivoresInRadius++;
            Debug.Log("Herbivores in radius: " + herbivoresInRadius);
            }
        }
        if(HungerMeter <= 70)
        {
           FindHerb();
        }
    }
    void FindHerb()
    {
        foreach (GameObject herbivore in Herbivores)
        {
            agent.SetDestination(herbivore.transform.position);
            if (Vector3.Distance(herbivore.transform.position, transform.position) < 1f)
            {
                Eat(herbivore);
            }
        }
    }
    void FindPred()
    {
        foreach (GameObject predator in Predators)
        {
            agent.SetDestination(predator.transform.position);
            if (Vector3.Distance(predator.transform.position, transform.position) < 1f)
            {
                Eat(predator);
            }
        }
    }
    public override void Reproduce()
    {
        throw new System.NotImplementedException();
    }
    public override void RunAway()
    {
        Vector3 runDirection = Vector3.zero;
        foreach (GameObject herbivore in Herbivores)
        {
            if(Vector3.Distance(herbivore.transform.position, transform.position) < runAwayRadius)
            {
                runDirection += transform.position - herbivore.transform.position;
            }
        }
        agent.SetDestination(transform.position + runDirection);
    }
    public override void Death()
    {
        Destroy(gameObject);
    }
    public override void Eat(GameObject food)
    {
        GameObject.Destroy(food);
        HungerMeter = 100;
    }

}
