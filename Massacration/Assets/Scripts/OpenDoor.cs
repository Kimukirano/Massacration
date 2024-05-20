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
    bool DoorReadyToOpen1 = false;
    bool DoorReadyToOpen2 = false;
    bool DoorReadyToOpen3 = false;
    bool DoorReadyToOpen4 = false;
    bool LockedDoorReadyToBeginOpen1 = false;
    bool LockedDoorReadyToBeginOpen2 = false;
    bool LockedDoorReadyToBeginOpen3 = false;
    bool LockedDoorReadyToBeginOpen4 = false;
    public static bool LockedDoorReadyToOpen = false;
    public delegate void OnPlayerInvasion(string doorName);
    public static event OnPlayerInvasion onPlayerInvasion;
    EnemyAI[] enemyAI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door1"))
        {
            Door = other.gameObject;
            DoorReadyToOpen1 = true;
        }
        else if (other.gameObject.CompareTag("Door2"))
        {
            Door = other.gameObject;
            DoorReadyToOpen2 = true;
        }
        else if (other.gameObject.CompareTag("Door3"))
        {
            Door = other.gameObject;
            DoorReadyToOpen3 = true;
        }
        else if (other.gameObject.CompareTag("Door4"))
        {
            Door = other.gameObject;
            DoorReadyToOpen4 = true;
        }
        else if (other.gameObject.CompareTag("LockedDoor1"))
        {
            Door = other.gameObject;
            LockedDoorReadyToBeginOpen1 = true;
        }
        else if (other.gameObject.CompareTag("LockedDoor2"))
        {
            Door = other.gameObject;
            LockedDoorReadyToBeginOpen2 = true;
        }
        else if (other.gameObject.CompareTag("LockedDoor3"))
        {
            Door = other.gameObject;
            LockedDoorReadyToBeginOpen3 = true;
        }
        else if (other.gameObject.CompareTag("LockedDoor4"))
        {
            Door = other.gameObject;
            LockedDoorReadyToBeginOpen4 = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door1"))
        {
            DoorReadyToOpen1 = false;
        }
        else if (other.gameObject.CompareTag("Door2"))
        {
            DoorReadyToOpen2 = false;
        }
        else if (other.gameObject.CompareTag("Door3"))
        {
            DoorReadyToOpen3 = false;
        }
        else if (other.gameObject.CompareTag("Door4"))
        {
            DoorReadyToOpen4 = false;
        }
        else if (other.gameObject.CompareTag("LockedDoor1"))
        {
            LockedDoorReadyToBeginOpen1 = false;
        }
        else if (other.gameObject.CompareTag("LockedDoor2"))
        {
            LockedDoorReadyToBeginOpen2 = false;
        }
        else if (other.gameObject.CompareTag("LockedDoor3"))
        {
            LockedDoorReadyToBeginOpen3 = false;
        }
        else if (other.gameObject.CompareTag("LockedDoor4"))
        {
            LockedDoorReadyToBeginOpen4 = false;
        }
    }



    public void OpeningDoor(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            if (DoorReadyToOpen1 == true)
            {
                OpenDoorFunction();
            }        
        }
        if (ctx.performed)
        {
            if (LockedDoorReadyToBeginOpen1 == true)
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

        if (DoorReadyToOpen1 == true)
        {
            foreach (EnemyAI enemy in enemyAI)
            {
                onPlayerInvasion = enemy.InRoomInvasion;
                onPlayerInvasion("Door1");
            }
        }
        else if (DoorReadyToOpen2 == true)
        {
            onPlayerInvasion("Door2");
        }
        else if (DoorReadyToOpen3 == true)
        {
            onPlayerInvasion("Door3");
        }
        else if (DoorReadyToOpen4 == true)
        {
            onPlayerInvasion("Door4");
        }
        else if (LockedDoorReadyToBeginOpen1 == true)
        {
            foreach (EnemyAI enemy in enemyAI)
            {
                onPlayerInvasion = enemy.InRoomInvasion;
                onPlayerInvasion("LockedDoor1");
            }
        }
        else if (LockedDoorReadyToBeginOpen2 == true)
        {
            onPlayerInvasion("LockedDoor2");
        }
        else if (LockedDoorReadyToBeginOpen3 == true)
        {
            onPlayerInvasion("LockedDoor3");
        }
        else if (LockedDoorReadyToBeginOpen4 == true)
        {
            foreach (EnemyAI enemy in enemyAI)
            {
                onPlayerInvasion = enemy.InRoomInvasion;
                onPlayerInvasion("LockedDoor4");
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        enemyAI = FindObjectsOfType<EnemyAI>();
        
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
