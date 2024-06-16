using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static PlayerStats;

public class SecurityGuardAI : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] Transform Exit;
    EnemyDamage enemyDamage;
    [SerializeField] int TotalHP;
    [SerializeField] GameObject BloodVFX;
    [SerializeField] Sprite CrawllerImage;
    [SerializeField] float CrawllerSpeed;
    [SerializeField] float RunSpeed;
    [SerializeField] Sprite PretendDeadImage;
    [SerializeField] Sprite DeadImage;
    [SerializeField] int ScorePoints;
    [SerializeField] Transform SpawmBulletLocation;
    [SerializeField] GameObject Bullet;
    private bool OnShootDelay = false;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider2D;
    private Vector3 BloodVFXposition;
    private GameObject Player;
    [SerializeField] public int AtackDamage;
    [SerializeField] private float BulletSpeed;
    [Range(0f, 2f)] public float FirstAtackDelay;
    [Range(0f, 2f)] public float AtackCooldown;
    [SerializeField] public float rotationSpeed = 90f;
    private float t = 0.0f;
    public delegate void LooseHPdelegate(int dmg);
    public static event LooseHPdelegate looseHPdelegate;
    public PlayerStats playerStats;
    private Vector2 transformReference;
    [Header("Door")]
    private GameObject Door;
    [Range(0f, 1f)] public float RotationTotalTime;

    public enum State
    {
        Idle,
        HuntPlayer,
        Crawlling,
        PretendDeath,
        Death,
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
        if (N == 9)
        {
            CrawllerMode();
        }
        else if (N == 10)
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
        if (state == State.Idle)
        {
            HuntPlayer();
        }
    }

    public void HuntPlayer()
    {
        state = State.HuntPlayer;
    }
    public void ShootPlayer()
    {
        OnShootDelay = true;
        
        Vector3 CurrentAngle = SpawmBulletLocation.transform.position - transform.position;
        CurrentAngle.Normalize();
        CurrentAngle.z = Camera.main.transform.rotation.z;

        GameObject InstantiatedBullet = Instantiate(Bullet, SpawmBulletLocation.transform.position, Quaternion.identity);
        InstantiatedBullet.GetComponent<Rigidbody2D>().velocity = CurrentAngle * BulletSpeed;
        
        Invoke("PrepareForNextShoot", AtackCooldown);
    }
    private void RotateAimToPlayer()
    {
        Vector3 ShootDirection = Player.transform.position - gameObject.transform.position;
        ShootDirection.Normalize();

        Vector3 CurrentAngle = SpawmBulletLocation.transform.position - transform.position;
        CurrentAngle.Normalize();
        CurrentAngle.z = Camera.main.transform.rotation.z;

        // Incrementar t com base na velocidade e no tempo
        t += Time.deltaTime * rotationSpeed;

        // Interpolação linear entre os vetores
        Vector3 currentVector = Vector3.Lerp(CurrentAngle, ShootDirection, t).normalized;

        float angle = Mathf.Atan2(currentVector.y, currentVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Resetar t se ultrapassar 1
        if (t > 1.0f)
        {
            t = 0.0f;
        }


    }

    public void PrepareForNextShoot()
    {
        OnShootDelay = false;
    }

    public void Death()
    {
        agent.isStopped = true;
        //DummieAnimator.enabled = false;
        spriteRenderer.sprite = DeadImage;
        boxCollider2D.enabled = false;
        state = State.Death;
        ScoreUI.UpdateScore(ScorePoints);
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
        agent.SetDestination(Exit.position);
        state = State.Crawlling;
    }
    public void DealAtack()
    {
        looseHPdelegate = playerStats.LooseHP;
        looseHPdelegate(AtackDamage);
    }

    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
        Player = GameObject.FindWithTag("Player");
        state = State.Idle;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = RunSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        playerStats = FindObjectOfType<PlayerStats>();
        
        transformReference = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        //DummieAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerShooting.ShootingStarted == true)
        {
            ShootingReaction();
        }
        if (state == State.HuntPlayer)
        {
            NavMeshHit hit;
            if (agent.Raycast(Player.transform.position, out hit) == false)
            {
                agent.isStopped = true;

                RotateAimToPlayer();

                if (OnShootDelay == false)
                {
                    ShootPlayer();
                }
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(Player.transform.position);
            }
            
        }

        if (state == State.Death)
        {
            spriteRenderer.sprite = DeadImage;
        }
        else
        {
            if (gameObject.transform.position.x != transformReference.x || gameObject.transform.position.y != transformReference.y)
            {
                //DummieAnimator.enabled = true;
                if (gameObject.transform.position.x > transformReference.x)
                {
                    if (gameObject.transform.position.y == transformReference.y)
                    {
                        //right
                        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    }
                    else if (gameObject.transform.position.y > transformReference.y)
                    {
                        //UpRight
                        gameObject.transform.rotation = Quaternion.Euler(0, 0, 45);
                    }
                    else
                    {
                        //DownRight
                        gameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
                    }
                }
                if (gameObject.transform.position.x < transformReference.x)
                {
                    if (gameObject.transform.position.y == transformReference.y)
                    {
                        //left
                        gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
                    }
                    else if (gameObject.transform.position.y > transformReference.y)
                    {
                        //UpLeft
                        gameObject.transform.rotation = Quaternion.Euler(0, 0, 135);
                    }
                    else
                    {
                        //DownLeft
                        gameObject.transform.rotation = Quaternion.Euler(0, 0, 225);
                    }
                }
            }
            else
            {
                //DummieAnimator.enabled = false;
            }
        }


        transformReference = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);

    }
}
