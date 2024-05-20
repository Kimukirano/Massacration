using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static PlayerStats;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform target;
    EnemyDamage enemyDamage;
    [SerializeField] int TotalHP;
    [SerializeField] GameObject BloodVFX;
    [SerializeField] Sprite CrawllerImage;
    [SerializeField] float CrawllerSpeed;
    [SerializeField] Sprite PretendDeadImage;
    [SerializeField] Sprite DeadImage;
    [SerializeField] int ScorePoints;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Vector3 BloodVFXposition;
    private GameObject Player;
    List<GameObject> HideSpots;
    List<GameObject> LockedRoomHideSpots;
    [SerializeField] public int AtackDamage;
    [Range(0f, 2f)] public float FirstAtackDelay;
    [Range(0f, 2f)] public float AtackCooldown;
    public delegate void LooseHPdelegate(int dmg);
    public static event LooseHPdelegate looseHPdelegate;
    public PlayerStats playerStats;
    [Header("Door")]
    private GameObject Door;
    [Range(0f, 1f)] public float RotationTotalTime;
    

    public enum IdleLocation
    {
        WalkWay,
        LockcableRoom1,
        LockcableRoom2,
        LockcableRoom3,
        LockcableRoom4,
        Room1,
        Room2,
        Room3,
        Room4,
    }
    public IdleLocation idleLocation;
    public enum State
    {
        Idle,
        AtackPlayer,
        EscapeToExit,
        BegToLive,
        LockingRoom,
        Hiding,
        Crawlling,
        PretendDeath,
        Death,
        Paralized,
    }
    public State state;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.GetComponent<BulletLife>().BulleLife--;
            if (other.GetComponent<BulletLife>().BulleLife <= 0)
            {
                Destroy(other.gameObject);
            }
            ReceiveDamage();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("", FirstAtackDelay, AtackCooldown);
        }
    }

    public void ReceiveDamage()
    {
        BloodVFXposition = transform.position;
        BloodVFXposition.y += 0.6f;
        Instantiate(BloodVFX, BloodVFXposition, Quaternion.identity);
        TotalHP -= 40; //trocar por bulletdamage
        int N = Random.Range(1, 11);
        if(N == 9)
        {
            CrawllerMode();
        }
        else if(N == 10)
        {
            PretendToBeDead();
        }
        if (TotalHP <= 0)
        {
            Death();
        }
    }
    public void ShootingReaction()
    {
        if(state == State.Idle)
        {
            //decidir proximo estado

            //se tiver no corredor 
            if(idleLocation == IdleLocation.WalkWay)
            {
                // ou hiding (pequena chance) ou implorar pela vida (mt pequena chance)
                int N = Random.Range(1, 10);
                if (N > 2)
                {
                    RunToExit();
                }
                else
                {
                    Hide();
                }
            }

            //se tiver numa sala trancavel
            else if (idleLocation == IdleLocation.LockcableRoom1 || idleLocation == IdleLocation.LockcableRoom2 || idleLocation == IdleLocation.LockcableRoom3 || idleLocation == IdleLocation.LockcableRoom4)
            {
                LockRoom();
            }
            else if(idleLocation == IdleLocation.Room1 || idleLocation == IdleLocation.Room2 || idleLocation == IdleLocation.Room3 || idleLocation == IdleLocation.Room4)
            {
                int N = Random.Range(1,2);
                if(N == 1)
                {
                    RunToExit();
                }
                else
                {
                    Hide();
                }
            }
        }
    }
    public void InRoomInvasion(string roomName)
    {
        Debug.Log("InRoomInvasion");
        Debug.Log(roomName);
        switch (roomName)
        {
            case "Door1": if(idleLocation == IdleLocation.Room1)
                {
                    InvasionReaction();
                }
                break;
            case "Door2":
                if (idleLocation == IdleLocation.Room2)
                {
                    InvasionReaction();
                }
                break;
            case "Door3":
                if (idleLocation == IdleLocation.Room2)
                {
                    InvasionReaction();
                }
                return;
            case "Door4":
                if (idleLocation == IdleLocation.Room4)
                {
                    InvasionReaction();
                }
                break;
            case "LockedDoor1":
                Debug.Log("LockedDoor1 Action");
                Debug.Log(idleLocation);
                if (idleLocation == IdleLocation.LockcableRoom1)
                {
                    InvasionReaction();
                }
                break;
            case "LockedDoor2":
                if (idleLocation == IdleLocation.LockcableRoom2)
                {
                    InvasionReaction();
                }
                break;
            case "LockedDoor3":
                if (idleLocation == IdleLocation.LockcableRoom3)
                {
                    InvasionReaction();
                }
                break;
            case "LockedDoor4":
                if (idleLocation == IdleLocation.LockcableRoom4)
                {
                    InvasionReaction();
                }
                break;
        }
    }

    public void InvasionReaction()
    {
        Debug.Log("InvasionReaciton");
        int N = Random.Range(0, 11);
        if (N >= 8)
        {
            Atack();
        }
        else if (N <= 3)
        {
            BeggingToLive();
        }
        else if (N >= 4 && N <=6)
        {
            RunToExit();
        }
        else
        {
            Paralized();
        }
    }
    public void Paralized()
    {
        agent.isStopped = true;
        Debug.Log("...");
        state = State.Paralized;
    }
    public void Death()
    {
        agent.isStopped = true;
        spriteRenderer.sprite = DeadImage;
        boxCollider2D.enabled = false;
        state = State.Death;
        ScoreUI.UpdateScore(ScorePoints);
    }
    public void RunToExit()
    {
        agent.SetDestination(target.position);
        state = State.EscapeToExit;
    }
    public void LockRoom()
    {
        //por padrão fica escondido
        int R = Random.Range(0, LockedRoomHideSpots.Count);
        agent.SetDestination(LockedRoomHideSpots[R].transform.position);
        state = State.LockingRoom;
    }
    public void Hide()
    {
        int R = Random.Range(0, HideSpots.Count);
        agent.SetDestination(HideSpots[R].transform.position);
        state = State.Hiding;
    }
    public void PretendToBeDead()
    {
        agent.isStopped = true;
        spriteRenderer.sprite = PretendDeadImage;
        state = State.PretendDeath;
    }

    public void CrawllerMode()
    {
        agent.speed = CrawllerSpeed;
        spriteRenderer.sprite = CrawllerImage;
        agent.SetDestination(target.position);
        state = State.Crawlling;
    }
    public void Atack()
    {
        state = State.AtackPlayer;
    }
    public void DealAtack()
    {
        looseHPdelegate = playerStats.LooseHP;
        looseHPdelegate(AtackDamage);
    }
    public void BeggingToLive()
    {
        agent.isStopped = true;
        Debug.Log("Please Dont Kill Me");
        state = State.BegToLive;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        Player = GameObject.FindWithTag("Player");
        state = State.Idle;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        playerStats = FindObjectOfType<PlayerStats>();

        //Buscando os lugares de se esconder da fase
        HideSpots = new List<GameObject>();
        GameObject[] objets = GameObject.FindGameObjectsWithTag("HideSpot");
        foreach (GameObject objet in objets)
        {
            HideSpots.Add(objet);
        }
        LockedRoomHideSpots = new List<GameObject>();
        GameObject[] Lockedobjets = GameObject.FindGameObjectsWithTag("LockedRoomHidingSpot");
        foreach (GameObject Lockedobjet in Lockedobjets)
        {
            LockedRoomHideSpots.Add(Lockedobjet);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
        if (PlayerShoot.ShootingStarted == true)
        {
            ShootingReaction();
        }
        if (state == State.AtackPlayer)
        {
            agent.SetDestination(Player.transform.position);
        }
        /**
        switch (state)
        {
            case State.Idle: 
                break;
            case State.Hiding: Hide(); 
                break;
            case State.EscapeToExit: RunToExit();
                break;
            case State.LockingRoom: LockRoom(); 
                break;
            case State.PretendDeath: PretendToBeDead();
                break;
            case State.Crawlling: CrawllerMode();
                break;
            case State.AtackPlayer: Atack();
                break;
            case State.BegToLive: BeggingToLive();
                break;
            case State.Death: Death();
                break;
        }
        **/
    }
}
