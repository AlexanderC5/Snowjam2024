using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    /*public GameObject[] Zone1Prefabs;
    public GameObject[] Zone2Prefabs;
    public GameObject[] Zone3Prefabs;
    public GameObject[] Zone4Prefabs;*/

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
        zone = getZoneFromHeight(player.height);
    }

}
