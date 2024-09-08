using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PredatorBehaviour : AnimalBehaviour
{
    public float detectionRadius = 5f;
    public float runAwayRadius = 10f;
    public float speed = 2f;
    public GameObject[] Herbivores;
    public float HungerMeter = 100f;
    public float HungerRate = 1f;
    public NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        Herbivores = GameObject.FindGameObjectsWithTag("Herbivore");
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(HungerMeter);
       HungerMeter -= HungerRate * Time.deltaTime;
       if(HungerMeter < 0)
       {
           Destroy(gameObject);
       }
       else if(HungerMeter < 50)
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
        agent.SetDestination(transform.position * Time.deltaTime);
    }
    public override void FindFood()
    {
        foreach(GameObject herbivore in Herbivores)
        {
            if(Vector3.Distance(herbivore.transform.position, transform.position) < detectionRadius)
            {
                agent.SetDestination(herbivore.transform.position);
            }
        }
    }
    public override void RunAway()
    {

    }

}
