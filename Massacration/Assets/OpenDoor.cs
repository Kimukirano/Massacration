using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenDoor : MonoBehaviour
{
    [Range(0f, 1f)] public float RotationTotalTime;
    [SerializeField] GameObject DoorBarPrefab;
    [SerializeField] Canvas TargetCanvas;
    GameObject Door;
    bool DoorReadyToOpen = false;
    bool LockedDoorReadyToBeginOpen = false;
    public static bool LockedDoorReadyToOpen = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            Door = other.gameObject;
            DoorReadyToOpen = true;
        }
        else if (other.gameObject.CompareTag("LockedDoor"))
        {
            Door = other.gameObject;
            LockedDoorReadyToBeginOpen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            DoorReadyToOpen = false;
        }
        else if (other.gameObject.CompareTag("LockedDoor"))
        {
            LockedDoorReadyToBeginOpen = false;
        }
    }



    public void OpeningDoor(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (DoorReadyToOpen == true)
            {
                OpenDoorFunction();
            }        
        }
        if (ctx.performed)
        {
            if (LockedDoorReadyToBeginOpen == true)
            {
                Vector3 DoorBarSpawm = Door.transform.position;
                DoorBarSpawm.y += 500f;
                DoorBarSpawm.x += 630f;
                GameObject InstantiatedDoorBarPrefab = Instantiate(DoorBarPrefab, DoorBarSpawm, Quaternion.identity);
                InstantiatedDoorBarPrefab.transform.SetParent(TargetCanvas.transform);
            }
        }
        if (ctx.canceled)
        {
            GameObject DoorBar = GameObject.FindGameObjectWithTag("DoorBar");
            Destroy(DoorBar);
        }
    }



    public void OpenDoorFunction()
    {
        Vector3 DoorTargetRotation = new Vector3(Door.transform.rotation.x, Door.transform.rotation.y, Door.transform.rotation.z - 90f);
        Door.transform.DORotate(DoorTargetRotation, RotationTotalTime, RotateMode.Fast);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LockedDoorReadyToOpen == true)
        {
            OpenDoorFunction();
        }
    }
}
