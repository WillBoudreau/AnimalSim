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
    public GameObject[] Herbivores;
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
        Herbivores = GameObject.FindGameObjectsWithTag("Herbivore");
        Move();
        FindFood();
        //HungerMeter -= HungerRate * Time.deltaTime;
        //if (HungerMeter < 0)
        //{
        //    Destroy(gameObject);
        //}
        //else if (HungerMeter < 50)
        //{
        //    FindFood();
        //}
        //else
        //{
        //    Move();
        //}
    }
    public override void Move()
    {
        if (agent.remainingDistance < 0.5f)
        {
            Direction = Random.insideUnitSphere * 10f;
            Destination = transform.position + Direction;
            agent.SetDestination(Destination);
        }
    }
    public override void FindFood()
    {
        foreach (GameObject herbivore in Herbivores)
        {
            if (Vector3.Distance(herbivore.transform.position, transform.position) < detectionRadius)
            {
                agent.SetDestination(herbivore.transform.position);
                if(Vector3.Distance(herbivore.transform.position,transform.position) < 1f)
                {
                    Eat(herbivore);
                }
            }
        }
    }
    public override void Reproduce()
    {
        //foreach (GameObject predator in Herbivores)
        //{
        //    if (Vector3.Distance(predator.transform.position, transform.position) < 5f)
        //    {
        //        GameObject newPredator = Instantiate(gameObject, transform.position, Quaternion.identity);
        //    }
        //}
    }
    public override void RunAway()
    {

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
