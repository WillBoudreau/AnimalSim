using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimalBehaviour : MonoBehaviour
{
    public float HungerMeter = 100f;
    public Vector3 Direction;
    public Vector3 Destination;
    public int numHerb = 10;
    public GameObject Herbivore;
    public bool LookingForFood = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numHerb; i++)
        {
            GameObject herbivore = Instantiate(Herbivore, transform.position, Quaternion.identity);
            HerbivoreBehaviour herbivoreBehaviour = herbivore.GetComponent<HerbivoreBehaviour>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public abstract void FindFood();
    public abstract void RunAway();
    public abstract void Move();
}
