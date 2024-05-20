using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int TotalHP;
    [SerializeField] GameObject BloodVFX;
    private Vector3 BloodVFXposition;
    public bool IsDeath = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            other.GetComponent<BulletLife>().BulleLife--;
            if (other.GetComponent<BulletLife>().BulleLife <=0)
            {
                Destroy(other.gameObject);
            }
            ReceiveDamage();
        }
    }
    public void ReceiveDamage()
    {
        BloodVFXposition = transform.position;
        BloodVFXposition.y += 0.6f;
        Instantiate(BloodVFX, BloodVFXposition, Quaternion.identity);
        TotalHP -= 40;
        if(TotalHP <= 0)
        {
            Invoke("Death",0f);
        }
    }
    public void Death()
    {
        IsDeath = true;
        Debug.Log("Morreu");
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
