using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
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
