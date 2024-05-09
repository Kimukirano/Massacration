using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAtack : MonoBehaviour
{
    [SerializeField] Animator MeleeAnimator;
    [SerializeField] GameObject MeleeObject;

    public void Atack(InputAction.CallbackContext ctx)
    {
        if(ctx.started)
        {
            PeformMeleeAtack();
        }
    }
    
    public void PeformMeleeAtack()
    {
        Debug.Log("MeleeATACK");
        //MeleeAnimator.SetTrigger();
        MeleeAnimator.Play("Katana Animation");
        MeleeObject.tag = "Katana";
        MeleeAnimator.enabled = true;
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
