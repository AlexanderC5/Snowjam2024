using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public enum GameStates
    {
        start, // we may want special behavior when loading in
        play, // game is ongoing
        win, // reached the end
        lose,
        exit // return to menu?
    }
    private GameStates gameState = GameStates.start;
    public GameStates GameState {
        get { return gameState; }
        set {
            // basically, if player sets gamestate to win/lose, everything else needs to know.
            switch(value)
            {
                case GameStates.win:
                    airGen.Win = true;
                    groundGen.Win = true;
                    break;
                case GameStates.lose:
                    // probably take the player back to the main menu
                    break;
                default:
                    break;
            }
            gameState = value;
        }
    }
    public List<int> zoneHeights = new List<int>(){100, 200, 300, 99999};
    public int getZoneFromHeight(float height)
    {
        int z = 0;
        while (zoneHeights[z] < player.height) z++;
        return z;
    }

    public int zone = 0;

    private Player player; // script controlling the player
    private AirGenerationManager airGen;
    private GroundGenerationManager groundGen;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (!TryGetComponent<AirGenerationManager>(out airGen))
            Debug.Log("Manager needs air obstacle generator");
        if (!TryGetComponent<GroundGenerationManager>(out groundGen))
            Debug.Log("Manager needs ground terrain generator");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void generateRandObs(int zone)
    {
        // NOTE: can spawn outside of diagonal, rectangular zones
        GameObject currZone = GameObject.FindWithTag("Player").GetComponent<Player>().Zone;
        float x = Random.Range(currZone.GetComponent<Renderer>().bounds.min.x, currZone.GetComponent<Renderer>().bounds.max.x);
        float y = Random.Range(currZone.GetComponent<Renderer>().bounds.min.y, currZone.GetComponent<Renderer>().bounds.max.y);
        spawnObs(zone, x, y);
    }

    void generateRandObsOnZone(int zone, GameObject currZone)
    {
        // NOTE: can spawn outside of diagonal, rectangular zones
        float x = Random.Range(currZone.GetComponent<Renderer>().bounds.min.x, currZone.GetComponent<Renderer>().bounds.max.x);
        float y = Random.Range(currZone.GetComponent<Renderer>().bounds.min.y, currZone.GetComponent<Renderer>().bounds.max.y);
        spawnObs(zone, x, y);
    }

    void generateRandObstacle(GameObject currZone)
    {
        // NOTE: can spawn outside of diagonal, rectangular zones
        float x = Random.Range(currZone.GetComponent<Renderer>().bounds.min.x, currZone.GetComponent<Renderer>().bounds.max.x);
        float y = Random.Range(currZone.GetComponent<Renderer>().bounds.min.y, currZone.GetComponent<Renderer>().bounds.max.y);
        int zone = currZone.GetComponent<Zone>().zone;
        spawnObs(zone, x, y);
    }*/

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
        zone = getZoneFromHeight(player.height);
    }
}
