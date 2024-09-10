using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimalBehaviour : MonoBehaviour
{
    public float HungerMeter = 100f;
    public Vector3 Direction;
    public Vector3 Destination;
    public bool LookingForFood = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public abstract void FindFood();
    public abstract void RunAway();
    public abstract void Move();
}
