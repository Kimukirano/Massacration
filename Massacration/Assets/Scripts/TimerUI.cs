using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEditor;

public class TimerUI : MonoBehaviour
{
    [Header("GlobalSetup")]
    [SerializeField] Volume GlobalVolume;
    [SerializeField] AudioSource GlobalAudioSource;
    [SerializeField] AudioSource GlobalAudioSource2;
    [SerializeField] TMP_Text TimeClock;
    [SerializeField] float Seconds;

    [Header("PoliceArrive")]
    [SerializeField] GameObject Police;
    [SerializeField] int PoliceQuantity;
    [SerializeField] VolumeProfile PoliceArriveEffect;
    [SerializeField] float HueShiftTransitionDelay;
    [SerializeField] AudioClip PoliceArriveSong;
    [SerializeField] AudioClip PoliceArriveSFX;
    List<GameObject> ArriveSpots;
    private bool PoliceArrived = false;

    public void PoliceArrivePosProcessingEffect()
    {
        GlobalVolume.profile = PoliceArriveEffect;
        InvokeRepeating("ChangeHueShift", HueShiftTransitionDelay, HueShiftTransitionDelay);
    }
    public void ChangeHueShift()
    {
        if (GlobalVolume.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments))
        {
            if (colorAdjustments.hueShift.value != 0f)
            {
                colorAdjustments.hueShift.value = 0f;
            }
            else
            {
                colorAdjustments.hueShift.value = -130f;
            }
        }
        else
        {
            Debug.LogWarning("ColorAdjustments não encontrado no Volume Profile!");
        }
    }

    public void PoliceArrive()
    {

        GlobalAudioSource.clip = PoliceArriveSong;
        GlobalAudioSource.Play();
        GlobalAudioSource2.clip = PoliceArriveSFX;
        GlobalAudioSource2.Play();
        PoliceArrivePosProcessingEffect();
        //Buscando spots de entrada/spawm
        ArriveSpots = new List<GameObject>();
        GameObject[] objets = GameObject.FindGameObjectsWithTag("PoliceArriveSpot");
        foreach (GameObject objet in objets)
        {
            ArriveSpots.Add(objet);
        }
        for (int i = 0; i < PoliceQuantity; i++)
        {
            int R = Random.Range(0, ArriveSpots.Count);
            Instantiate(Police, ArriveSpots[R].transform.position, Quaternion.identity);
        }
        PoliceArrived = true;
    }

    private void Awake()
    {
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Seconds -= Time.deltaTime;
        if (Seconds < 0) { Seconds = 0; }
        int Minutes = Mathf.FloorToInt(Seconds / 60f);
        int SecondsRemains = Mathf.FloorToInt(Seconds % 60f);
        TimeClock.text = Minutes.ToString("00") + " : " + SecondsRemains.ToString("00");
        if(Seconds <= 0 && !PoliceArrived)
        {
            PoliceArrive();
        }
    }
}
