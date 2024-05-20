using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Range(0f, 1f)] public float RotationTotalTime;
    private bool opened = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("trigou");
            if (opened==false)
            {
                Vector3 DoorTargetRotation = new Vector3(gameObject.transform.rotation.x, gameObject.transform.rotation.y, gameObject.transform.rotation.z - 90f);
                gameObject.transform.DORotate(DoorTargetRotation, RotationTotalTime, RotateMode.Fast);
                opened = true;
            }
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYERtrigou");
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
