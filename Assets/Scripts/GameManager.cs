using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject Herbivore;
    public GameObject Plant;
    public GameObject Predator;
    public float numHerb;
    public float numPlants;
    public int numPreds = 1;
    public float minPositionX = -10f;
    public float maxPositionX = 10f;
    public float minPositionZ = -10f;
    public float maxPositionZ = 10f;

    public Slider NumberOfHerb;
    public Slider NumberOfPlants;

    // Start is called before the first frame update
    void Start()
    {
        numHerb = 0f;
        numPlants = 0f;
        for(int i = 0; i <numPreds; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minPositionX, maxPositionX), 0f, Random.Range(minPositionZ, maxPositionZ));
            GameObject predator = Instantiate(Predator, randomPosition, Quaternion.identity);
            PredatorBehaviour predbehaviour = GetComponent<PredatorBehaviour>();
        }
    }
    void Update()
    {

    }
    public void spawnHerbivore()
    {
        numHerb = NumberOfHerb.value;
        for (int i = 0; i < numHerb; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minPositionX, maxPositionX), 0f, Random.Range(minPositionZ, maxPositionZ));
            GameObject herbivore = Instantiate(Herbivore, randomPosition, Quaternion.identity);
            HerbivoreBehaviour herbivoreBehaviour = herbivore.GetComponent<HerbivoreBehaviour>();
        }

    }
    public void SpawnPlants()
    {
        numPlants = NumberOfPlants.value;
        for (int i = 0; i < numPlants; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minPositionX, maxPositionX), 0f, Random.Range(minPositionZ, maxPositionZ));
            GameObject plant = Instantiate(Plant, randomPosition, Quaternion.identity);
        }
    }
}
