using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int TotalHP;
    [SerializeField] GameObject BloodVFX;
    private Vector3 BloodVFXposition;

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
        Instantiate(BloodVFX, BloodVFXposition, Quaternion.identity);
    }
    // Start is called before the first frame update
    void Start()
    {
        BloodVFXposition = this.gameObject.transform.position;
        BloodVFXposition.y += 0.6f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
