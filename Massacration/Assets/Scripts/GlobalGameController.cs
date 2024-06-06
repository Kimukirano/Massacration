using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameController : MonoBehaviour
{

    public enum GameState
    {
        Menu,
        Play,
        Pause,
    }

    public static GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Menu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
