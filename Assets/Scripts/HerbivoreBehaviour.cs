using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HerbivoreBehaviour : AnimalBehaviour
{
    public float HungerMeter = 100f;
    public float speed = 2f;
    public float detectionRadius = 5f;
    public float herdRadius = 10f;
    public float runAwayRadius = 10f;
    public float HungerRate = 1f;
    public float ReproduceRate = 10f;

    public NavMeshAgent agent;
    public GameObject closestHerbivore = null;
    public GameObject closestPlant = null;

    public GameObject[] Herbivores;
    public GameObject[] Predators;
    public GameObject[] Plants;
    Renderer HerbMat;

    // Start is called before the first frame update
    void Start()
    {
        HerbMat = GetComponent<Renderer>();
        HerbMat.material.color = Color.blue;
        agent = GetComponent<NavMeshAgent>();
        Move();
    }

    // Update is called once per frame
    void Update()
    {
        Predators = GameObject.FindGameObjectsWithTag("Predator");
        Herbivores = GameObject.FindGameObjectsWithTag("Herbivore");
        Plants = GameObject.FindGameObjectsWithTag("Plant");
        if(HungerMeter > 50f)
        {
            Move();
            RunAway();
            //Reproduce();
        }
        if(HungerMeter <= 50f)
        {
            LookingForFood = true;
            FindFood();
            RunAway();
        }
        if(HungerMeter <= 0)
        {
            Death();
        }
        Hunger();
        Move();
        //FindFood();

    }
    public override void Reproduce()
    {
        if(HungerMeter >= ReproduceRate)
        {
            Debug.Log(ReproduceRate);
            ReproduceRate += 10f;
            Vector3 randomPosition = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            GameObject herbivore = Instantiate(gameObject, randomPosition, Quaternion.identity);
        }
    }
    Vector3 Cohesion()
    {
        Vector3 center = Vector3.zero;
        int count = 0;
        foreach (GameObject herbivore in Herbivores)
        {
            if (herbivore != this.gameObject && Vector3.Distance(herbivore.transform.position, transform.position) < herdRadius)
            {
                center += herbivore.transform.position;
                count++;
            }
        }
        if (count > 0)
        {
            center /= count;
            return (center - transform.position).normalized;
        }
        return Vector3.zero;
    }

    Vector3 Separation()
    {
        Vector3 avoid = Vector3.zero;
        int count = 0;
        foreach (GameObject herbivore in Herbivores)
        {
            if (herbivore != this.gameObject && Vector3.Distance(herbivore.transform.position, transform.position) < detectionRadius)
            {
                avoid += (transform.position - herbivore.transform.position);
                count++;
            }
        }
        if (count > 0)
        {
            avoid /= count;
            return avoid.normalized;
        }
        return Vector3.zero;
    }

    Vector3 Alignment()
    {
        Vector3 alignment = Vector3.zero;
        int count = 0;
        foreach (GameObject herbivore in Herbivores)
        {
            if (herbivore != this.gameObject && Vector3.Distance(herbivore.transform.position, transform.position) < herdRadius)
            {
                alignment += herbivore.GetComponent<NavMeshAgent>().velocity;
                count++;
            }
        }
        if (count > 0)
        {
            alignment /= count;
            return alignment.normalized;
        }
        return Vector3.zero;
    }

    public override void Move()
    {
        if (agent.remainingDistance < 0.5f)
        {
            Vector3 flockDirection = Cohesion() + Separation() + Alignment();
            if (flockDirection == Vector3.zero)
            {
                Direction = Random.insideUnitSphere * 10f; // Random movement if no flock influence
            }
            else
            {
                Direction = flockDirection * 10f;
            }
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
            if (distance < closestDistance)
            {
                closestPlant = plant;
                closestDistance = distance;
            }
        }

        if (closestPlant != null)
        {
            agent.SetDestination(closestPlant.transform.position);
            if (Vector3.Distance(closestPlant.transform.position, transform.position) < detectionRadius)
            {
                Eat(closestPlant);
            }
        }
    }

    public override void Eat(GameObject plant)
    {
        HungerMeter += 50f;
        Destroy(plant);
        if(HungerMeter > 100)
        {
            HungerMeter = 100;
        }
        LookingForFood = false;
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