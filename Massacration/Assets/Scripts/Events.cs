using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Events : MonoBehaviour
{
    [Header("GlobalSetup")]
    [SerializeField] Volume GlobalVolume;
    [SerializeField] AudioSource GlobalAudioSource;
    [SerializeField] AudioSource GlobalAudioSource2;

    [Header("PoliceArrive")]
    [SerializeField] GameObject Police;
    [SerializeField] int PoliceQuantity;
    [SerializeField] VolumeProfile PoliceArriveEffect;
    [SerializeField] float HueShiftTransitionDelay;
    [SerializeField] AudioClip PoliceArriveSong;
    [SerializeField] AudioClip PoliceArriveSFX;
    List<GameObject> ArriveSpots;
    public TimerUI timerUI;

    public void PoliceArrivePosProcessingEffect()
    {
        GlobalVolume.profile = PoliceArriveEffect;
        InvokeRepeating("ChangeHueShif", HueShiftTransitionDelay, HueShiftTransitionDelay);
    }
    public void ChangeHueShift()
    {
        if (GlobalVolume.profile.TryGet<ColorAdjustments>(out ColorAdjustments colorAdjustments))
        {
            if(colorAdjustments.hueShift.value != 0f)
            {
                colorAdjustments.hueShift.value = 0f;
            }
            else
            {
                colorAdjustments.hueShift.value = 125f;
            }  
        }
        else
        {
            Debug.LogWarning("Film Grain não encontrado no Volume Profile!");
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
