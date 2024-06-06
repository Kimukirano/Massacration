using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMenuManager : MonoBehaviour
{
    public static AudioMenuManager instance;
    private void Awake()
    {
        if (instance == null && GlobalGameController.gameState == GlobalGameController.GameState.Menu)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
