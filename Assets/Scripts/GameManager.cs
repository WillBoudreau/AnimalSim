using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Herbivore;
    public GameObject Plant;
    public int numHerb = 10;
    public int numPlants = 10;
    public float minPositionX = -10f;
    public float maxPositionX = 10f;
    public float minPositionZ = -10f;
    public float maxPositionZ = 10f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numHerb; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minPositionX, maxPositionX), 0f, Random.Range(minPositionZ, maxPositionZ));
            GameObject herbivore = Instantiate(Herbivore, randomPosition, Quaternion.identity);
            HerbivoreBehaviour herbivoreBehaviour = herbivore.GetComponent<HerbivoreBehaviour>();
        }
        for(int i = 0; i < numPlants; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minPositionX, maxPositionX), 1f, Random.Range(minPositionZ, maxPositionZ));
            GameObject plant = Instantiate(Plant, randomPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
