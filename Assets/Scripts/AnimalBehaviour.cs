using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimalBehaviour : MonoBehaviour
{
    public Vector3 Direction;
    public Vector3 Destination;
    public bool LookingForFood = false;


    public GameObject[] Plants;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Plants = GameObject.FindGameObjectsWithTag("Plant");
    }
    public abstract void FindFood();
    public abstract void Eat(GameObject food);
    public abstract void RunAway();
    public abstract void Move();
    public abstract void Death();
    public abstract void Reproduce();
}
