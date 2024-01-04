using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public int zone = 1;
    public GameObject[] Zone1Prefabs;
    public GameObject[] Zone2Prefabs;
    public GameObject[] Zone3Prefabs;
    public GameObject[] Zone4Prefabs;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void generateRandObs(int zone)
    {
        // NOTE: can spawn outside of diagonal, rectangular zones
        GameObject currZone = GameObject.FindWithTag("Player").GetComponent<Player>().Zone;
        float x = Random.Range(currZone.GetComponent<Renderer>().bounds.min.x, currZone.GetComponent<Renderer>().bounds.max.x);
        float y = Random.Range(currZone.GetComponent<Renderer>().bounds.min.y, currZone.GetComponent<Renderer>().bounds.max.y);
        spawnObs(zone, x, y);
    }

    void spawnObs(int zone, float x, float y)
    {
        //Random.Range(-1.88f, 2.1f), Random.Range(-7.81f, -3.1f)
        Vector3 position = new Vector3(x, y);
        if (zone == 1)
        {
            Instantiate(Zone1Prefabs[Random.Range(0, Zone1Prefabs.Length)], position, Quaternion.identity);
        }
        else if (zone == 2)
        {
            Instantiate(Zone2Prefabs[Random.Range(0, Zone2Prefabs.Length)], position, Quaternion.identity);
        }
        else if (zone == 3)
        {
            Instantiate(Zone3Prefabs[Random.Range(0, Zone3Prefabs.Length)], position, Quaternion.identity);
        }
        else if (zone == 4)
        {
            Instantiate(Zone4Prefabs[Random.Range(0, Zone4Prefabs.Length)], position, Quaternion.identity);
        }
        else
        {
            Instantiate(Zone1Prefabs[Random.Range(0, Zone1Prefabs.Length)], position, Quaternion.identity);
        }
    }
}
