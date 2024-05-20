using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] TMP_Text TimeClock;
    [SerializeField] float Seconds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Seconds -= Time.deltaTime;
        int Minutes = Mathf.FloorToInt(Seconds / 60f);
        int SecondsRemains = Mathf.FloorToInt(Seconds % 60f);
        TimeClock.text = Minutes.ToString("00") + " : " + SecondsRemains.ToString("00");
        
    }
}
