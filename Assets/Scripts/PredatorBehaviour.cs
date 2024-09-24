using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PredatorBehaviour : AnimalBehaviour
{
    public float detectionRadius = 5f;
    public float HungerMeter = 100f;
    public float runAwayRadius = 10f;
    public float speed = 2f;
    public float HungerRate = 1f;
    public NavMeshAgent agent;
    public int HerbCount = 0;
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
        Predators = GameObject.FindGameObjectsWithTag("Predator");
        Herbivores = GameObject.FindGameObjectsWithTag("Herbivore");
        HungerMeter -= HungerRate * Time.deltaTime;
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
    public override void Move()
    {
        if (agent.remainingDistance < 0.5f)
        {
            Direction = Random.insideUnitSphere * 5f;
            Destination = transform.position + Direction;
            agent.SetDestination(Destination);
        }
    }
    public override void FindFood()
    {
        if(HungerMeter <= 40)
        {
            foreach(GameObject predator in Predators)
            {
                agent.SetDestination(predator.transform.position);
                if(Vector3.Distance(predator.transform.position,transform.position) < 1f)
                {
                    Eat(predator);
                }
            }
        }
        foreach (GameObject herbivore in Herbivores)
        {
                 agent.SetDestination(herbivore.transform.position);
                if(Vector3.Distance(herbivore.transform.position,transform.position) < 1f)
                {
                    Eat(herbivore);
                }
        }
    }
    public override void Reproduce()
    {
        throw new System.NotImplementedException();
    }
    public override void RunAway()
    {
        foreach (GameObject herbivore in Herbivores)
        {
            if (Vector3.Distance(herbivore.transform.position, transform.position) < runAwayRadius)
            {
                HerbCount++;
                Debug.Log("HerbCount " + HerbCount);
            }
            if (HerbCount > 2)
            {
                agent.SetDestination(transform.position - herbivore.transform.position);
            }
        }
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
