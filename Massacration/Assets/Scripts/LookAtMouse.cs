using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    private SpriteRenderer Sp;
    public void Start()
    {
        Sp = this.gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if(PauseGame.Paused == false){

            Vector3 MousePosition = Input.mousePosition;

            MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);

            Vector2 MouseDirection = new Vector2(MousePosition.x - transform.position.x, MousePosition.y - transform.position.y);

            float angle = Mathf.Atan2(MouseDirection.y, MouseDirection.x) * Mathf.Rad2Deg;

            Quaternion rotacao = Quaternion.AngleAxis(angle + 10, Vector3.forward);

            transform.rotation = rotacao;


            if (this.gameObject.transform.rotation.z > 0.77f || this.gameObject.transform.rotation.z < -0.67f)
            {
                Sp.flipY = true;
            }
            else
            {
                Sp.flipY = false;
            }
        }

        

    }
}
