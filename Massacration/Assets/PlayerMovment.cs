using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] float Speed;

    //so pra demonstração, trocar por animações: // GetComponent<Animator>().Play("AnimaçãoX");
    [SerializeField] Sprite Up;
    [SerializeField] Sprite Down;
    [SerializeField] Sprite Left;
    [SerializeField] Sprite Right;
    [SerializeField] Sprite UpRight;
    [SerializeField] Sprite DownRight;
    [SerializeField] Sprite UpLeft;
    [SerializeField] Sprite DownLeft;
    private SpriteRenderer spriteRenderer;
    private Vector2 moviment;

    public void SetMoviment(InputAction.CallbackContext value)
    {
        moviment = value.ReadValue<Vector2>();
    }
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        Vector2 CurrentPosition = transform.position;

        Vector3 Movimentdirection = new Vector3(moviment.x, moviment.y, 0f).normalized;

        transform.Translate(Movimentdirection * Speed * Time.deltaTime);

        Vector2 NewPosition = transform.position;

        if (NewPosition == CurrentPosition)
        {
            // GetComponent<Animator>().Play("Parado");
        }
        else if (NewPosition != CurrentPosition)
        {
            if (NewPosition.x > CurrentPosition.x)
            {
                if(NewPosition.y == CurrentPosition.y)
                {
                    spriteRenderer.sprite = Right;
                }
                else if(NewPosition.y > CurrentPosition.y)
                {
                    spriteRenderer.sprite = UpRight;
                }
                //pode trocar esse else if logo abaixo por um else
                else if (NewPosition.y < CurrentPosition.y)
                {
                    spriteRenderer.sprite = DownRight;
                }
            }
            else if (NewPosition.x < CurrentPosition.x)
            {
                if (NewPosition.y == CurrentPosition.y)
                {
                    spriteRenderer.sprite = Left;
                }
                else if (NewPosition.y > CurrentPosition.y)
                {
                    spriteRenderer.sprite = UpLeft;
                }
                //pode trocar esse else if logo abaixo por um else
                else if (NewPosition.y < CurrentPosition.y)
                {
                    spriteRenderer.sprite = DownLeft;
                }
            }
            //pode trocar esse else if logo abaixo por um else
            else if (NewPosition.x == CurrentPosition.x)
            {
                if (NewPosition.y > CurrentPosition.y)
                {
                    spriteRenderer.sprite = Up;
                }
                //pode trocar esse else if logo abaixo por um else
                else if (NewPosition.y < CurrentPosition.y)
                {
                    spriteRenderer.sprite = Down;
                }
            }
        }
    }
}



