using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    [Header("Scenes Objects")]
    [SerializeField] GameObject Play;
    [SerializeField] private float PlayDelay;
    [SerializeField] GameObject Shop;
    [SerializeField] private float ShopDelay;
    [SerializeField] GameObject Stats;
    [SerializeField] private float StatsDelay;
    [SerializeField] GameObject Settings;
    [SerializeField] private float SettingsDelay;
    [SerializeField] GameObject Credits;
    [SerializeField] private float CreditsDelay;
    [SerializeField] GameObject Exit;
    [Header("Text Objs")]
    [SerializeField] private TMP_Text PlayText;
    [SerializeField] private TMP_Text ShopText;
    [SerializeField] private TMP_Text StatsText;
    [SerializeField] private TMP_Text SettingsText;
    [SerializeField] private TMP_Text CreditsText;
    [SerializeField] private TMP_Text ExitText;
    private string[] lines;
    [SerializeField] GameObject ArrowUp;
    [SerializeField] GameObject ArrowDown;

    //Starting with Play option
    private int Index = 0;

    private GameObject[] MenuElements;

    public void MenuPlay()
    {
        GlobalGameController.gameState = GlobalGameController.GameState.Play;
        Destroy(AudioMenuManager.instance.gameObject);
        LoadSceneAfterDelay("Prototype", PlayDelay);
    }
    public void MenuShop()
    {
        LoadSceneAfterDelay("MenuUpgrades", ShopDelay);
    }
    public void MenuStats()
    {
        LoadSceneAfterDelay("MenuStats", StatsDelay);
    }
    public void MenuSettings()
    {
        LoadSceneAfterDelay("MenuSettings", SettingsDelay);
    }
    public void MenuCredits()
    {
        LoadSceneAfterDelay("MenuCredits", CreditsDelay);
    }
    public void MenuExit()
    {
        Application.Quit();
    }
    
    //alternate menu elements
    public void UpList()
    {
        MenuElements[Index].SetActive(false);
        Index -= 1;
        if (Index <= -1)
        {
            Index = 5;
        }
        MenuElements[Index].SetActive(true);
    }
    public void DownList()
    {
        MenuElements[Index].SetActive(false);
        Index += 1;
        if (Index >= 6)
        {
            Index = 0;
        }
        MenuElements[Index].SetActive(true);
    }

    public void LoadSceneAfterDelay(string sceneName, float delay)
    {
        StartCoroutine(LoadSceneWithDelay(sceneName, delay));
    }

    IEnumerator LoadSceneWithDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        MenuElements = new GameObject[6];
        MenuElements[0] = Play;
        MenuElements[1] = Shop;
        MenuElements[2] = Stats;
        MenuElements[3] = Settings;
        MenuElements[4] = Credits;
        MenuElements[5] = Exit;
        lines = File.ReadAllLines("Assets/Texts/MassacrationText.txt");
        PlayText.text = lines[0];
        ShopText.text = lines[1];
        StatsText.text = lines[2];
        SettingsText.text = lines[3];
        CreditsText.text = lines[4];
        ExitText.text = lines[5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
