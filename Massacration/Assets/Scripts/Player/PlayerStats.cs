using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] int HealthPoints;
    [SerializeField] int MaxHealthPoints;
    [Range(0f, 1f)] public float BlinkDamageTime;
    SpriteRenderer spriteRenderer;

    public void LooseHP(int dmg)
    {
        spriteRenderer.color = Color.red;
        HealthPoints -= dmg;
        Invoke("ResetColor", BlinkDamageTime);
    }
    public void ResetColor()
    {
        spriteRenderer.color = Color.white;
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
