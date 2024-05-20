using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

public class ProgressDoorBar : MonoBehaviour
{
    [SerializeField] float time;
    Slider progress;
    // Start is called before the first frame update
    void Start()
    {
        progress = GetComponent<Slider>();
        progress.DOValue(1f, time, false);
    }

    // Update is called once per frame
    void Update()
    {
        if(progress.value == 1f)
        {
            OpenDoor.LockedDoorReadyToOpen = true;
            Destroy(gameObject);
        }
    }
}
