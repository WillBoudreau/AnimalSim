using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Herbivore;
    public int numHerb = 10;
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
}
