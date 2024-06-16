using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int HealthPoints;
    [SerializeField] int MaxHealthPoints;
    [SerializeField] int defaultDamage;
    [Range(0f, 1f)] public float BlinkDamageTime;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] public Collider2D BulletTrigger;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Aturoii");
            LooseHP(defaultDamage);
            Destroy(other.gameObject);
        }
    }
  

    public void LooseHP(int dmg)
    {
        Debug.Log("LevouTiro");
        spriteRenderer.color = Color.red;
        HealthPoints -= dmg;
        Invoke("ResetColor", BlinkDamageTime);
        if(HealthPoints <= 0)
        {
            //SceneManager.LoadScene("MainMenu");
            Debug.Log("Morreu");
        }
    }
    public void ResetColor()
    {
        spriteRenderer.color = Color.white;
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
