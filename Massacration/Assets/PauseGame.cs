using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] RawImage BackgroundShadow;
    [SerializeField] TMP_Text PauseText;
    [SerializeField] AudioSource BGMAudioSource;
    [Range(-3f, 3f)] public float PausePitch;
    [Range(0, 1f)] public float PauseVolume;
    public static bool Paused = false;
    public void Pause(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            if(Paused==false)
            {
                Paused = true;
                Time.timeScale = 0;
                BackgroundShadow.enabled = true;
                PauseText.enabled = true;
                BGMAudioSource.pitch = PausePitch;
                BGMAudioSource.volume = PauseVolume;

            }
            else if(Paused==true)
            {
                Paused = false;
                Time.timeScale = 1f;
                BackgroundShadow.enabled = false;
                PauseText.enabled = false;
                BGMAudioSource.pitch = 1f;
                BGMAudioSource.volume = 1f;
            }
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
