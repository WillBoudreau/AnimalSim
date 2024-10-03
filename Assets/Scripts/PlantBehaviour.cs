using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehaviour : MonoBehaviour
{
    //Variables to influence the Plant's behavior
    Renderer PlantMat;
    [Header("Plant Life")]
    public float essence;
    public float lifespan = 30f;
    public float essenceMax;

    //Variables to store the Plant's stats
    [Header("Plant Stats")]
    public GameObject Seed;
    bool SeedPlanted;
    public float minPosX;
    public float maxPosX;
    public float minPosZ;
    public float maxPosZ;
    // Start is called before the first frame update
    void Start()
    {
        //Set the Plant's stats
        essenceMax = 100;
        minPosX = -25;
        maxPosX = 25;
        minPosZ = -25;
        maxPosZ = 25;
        PlantMat = GetComponent<Renderer>();
        PlantMat.material.color = Color.green;
        essence = 0;
        SeedPlanted = false;
    }

    // Update is called once per frame
    void Update()
    {
        essence += Time.deltaTime;
        lifespan -= Time.deltaTime;
        if(lifespan <= 0)
        {
            Death();
        }
        PlantSeed();
    }
    //Plant seed at random location if essense is high enough
    void PlantSeed()
    {
        if(essence > 15 && !SeedPlanted)
        {
            Vector3 randPOS = new Vector3(Random.Range(minPosX, maxPosX), 0f, Random.Range(minPosZ, maxPosZ));
            //Spawn Seed in a random position inside of a certain range
            GameObject seed = Instantiate(Seed, randPOS, Quaternion.identity);
            SeedPlanted = true;
            essence = 0;
            Regrow();
        }
    }
    //Spawn a plant at seed position after a certain amount of time
    void Regrow()
    {
        if(SeedPlanted == true)
        {
            Vector3 randPOS = new Vector3(Random.Range(minPosX, maxPosX), 0f, Random.Range(minPosZ, maxPosZ));
            GameObject plant = Instantiate(this.gameObject,randPOS, Quaternion.identity); 
        }
        
    }
    void Death()
    {
        Destroy(this.gameObject);
    }
}
