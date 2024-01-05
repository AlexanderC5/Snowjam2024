using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirGenerationManager : MonoBehaviour
{
    [SerializeField] public bool Win = false;
    public GameObject[] Zone1Prefabs;
    public GameObject[] Zone2Prefabs;
    public GameObject[] Zone3Prefabs;
    public GameObject[] Zone4Prefabs;

    [SerializeField] private List<int> zoneDensities = new List<int>(){5, 10, 15, 20};
    List<GameObject> obstacleObjects= new List<GameObject>();

    // Start is called before the first frame update
    private Player player;
    private GameManager gameManager;
    private Rigidbody2D playerRb;
    private int zone;
    private int checkInd = 0;
    private float spawnCountdown;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        playerRb = player.gameObject.GetComponent<Rigidbody2D>();
        gameManager = Object.FindFirstObjectByType<GameManager>();
        spawnCountdown = (1+Random.value) * (10/zoneDensities[zone]);
        zone = gameManager.zone;
    }

    // Update is called once per frame
    void Update()
    {
        spawnCountdown -= playerRb.velocity.magnitude * Time.deltaTime;
        if (spawnCountdown < 0)
        {
            // find next place to spawn 
            if (playerRb.velocity.magnitude != 0)
            {
                Debug.Log("Spawning random item");
                Vector2 nextLocation = (Vector2)player.transform.position + playerRb.velocity.normalized * 20 + 
                    new Vector2(playerRb.velocity.y, -playerRb.velocity.x) * 10 * Random.value;
                GameObject obstacle = spawnObs(gameManager.getZoneFromHeight(nextLocation.y), nextLocation.x, nextLocation.y + 20);
                obstacleObjects.Add(obstacle);
                spawnCountdown = (1+Random.value) * (10/zoneDensities[zone]);
            }
        }
        if (obstacleObjects.Count > 0)
        {
            if (obstacleObjects[checkInd] == null)
            {
                obstacleObjects.RemoveAt(checkInd);
            }
            else if (player.transform.position.x - obstacleObjects[checkInd].transform.position.x > 20)
            {
                Destroy(obstacleObjects[checkInd]);
                obstacleObjects.RemoveAt(checkInd);
            }
            else
                checkInd++;
            checkInd %= obstacleObjects.Count;
        }
    }

    // void generateRandObs(int zone)
    // {
    //     // NOTE: can spawn outside of diagonal, rectangular zones
    //     GameObject currZone = GameObject.FindWithTag("Player").GetComponent<Player>().Zone;
    //     float x = Random.Range(currZone.GetComponent<Renderer>().bounds.min.x, currZone.GetComponent<Renderer>().bounds.max.x);
    //     float y = Random.Range(currZone.GetComponent<Renderer>().bounds.min.y, currZone.GetComponent<Renderer>().bounds.max.y);
    //     spawnObs(zone, x, y);
    // }

    // void generateRandObsOnZone(int zone, GameObject currZone)
    // {
    //     // NOTE: can spawn outside of diagonal, rectangular zones
    //     float x = Random.Range(currZone.GetComponent<Renderer>().bounds.min.x, currZone.GetComponent<Renderer>().bounds.max.x);
    //     float y = Random.Range(currZone.GetComponent<Renderer>().bounds.min.y, currZone.GetComponent<Renderer>().bounds.max.y);
    //     spawnObs(zone, x, y);
    // }

    GameObject spawnObs(int zone, float x, float y)
    {
        //Random.Range(-1.88f, 2.1f), Random.Range(-7.81f, -3.1f)
        Vector3 position = new Vector3(x, y);
        if (zone == 1)
        {
            return Instantiate(Zone1Prefabs[Random.Range(0, Zone1Prefabs.Length)], position, Quaternion.identity);
        }
        else if (zone == 2)
        {
            return Instantiate(Zone2Prefabs[Random.Range(0, Zone2Prefabs.Length)], position, Quaternion.identity);
        }
        else if (zone == 3)
        {
            return Instantiate(Zone3Prefabs[Random.Range(0, Zone3Prefabs.Length)], position, Quaternion.identity);
        }
        else if (zone == 4)
        {
            return Instantiate(Zone4Prefabs[Random.Range(0, Zone4Prefabs.Length)], position, Quaternion.identity);
        }
        else
        {
            return Instantiate(Zone1Prefabs[Random.Range(0, Zone1Prefabs.Length)], position, Quaternion.identity);
        }
    }
}
