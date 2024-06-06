using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] float NormalMovSpeed;
    private float NormalMovSpeedReference;
    [SerializeField] float AimMovSpeed;
    public static bool InAim = false;
    //so pra demonstração, trocar por animações: // GetComponent<Animator>().Play("AnimaçãoX");
    [SerializeField] GameObject Legs;
    [SerializeField] Animator LegsAnimator;
    [SerializeField] GameObject Body;
    [SerializeField] Animator BodyAnimator;
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
    //private string Direction;
    public static Vector3 MouseDirection;
    public static GameObject Player;
    [SerializeField] GameObject Gun;
    private SpriteRenderer GunSp;

    public enum Direction
    {
        Right,
        UpRight,
        Up,
        UpLeft,
        Left,
        DownLeft,
        Down,
        DownRight,
    }
    public Direction direction;
    public void SetMoviment(InputAction.CallbackContext value)
    {
        moviment = value.ReadValue<Vector2>();
    }
    public void OnAim(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            
            InAim = true;
            NormalMovSpeed = AimMovSpeed;
            
        }
        if (ctx.canceled) {
            
            InAim = false;
            NormalMovSpeed = NormalMovSpeedReference;
            
        }   
        

    }
    public static Vector3 GetMouseDirection()
    {
        //GameObject Player = GameObject.FindGameObjectWithTag("Player");
        // Obter a posição do mouse em relação à câmera
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.transform.position.z;

        // Converter posição do mouse de tela para mundo
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // Calcular a direção do mouse em relação ao objeto
        MouseDirection = mouseWorldPos - Player.transform.position;

        // Normalizar a direção para garantir que tenha comprimento 1
        MouseDirection.Normalize();

        return MouseDirection;
    }
    public void GetMouseAngle()
    {
        // Calcular o ângulo entre o vetor direção do mouse e o vetor para frente do objeto
        float angulo = Mathf.Atan2(GetMouseDirection().x, GetMouseDirection().y) * Mathf.Rad2Deg;

        if (angulo < 0){angulo += 360;}

        if (angulo >= 340f || angulo <= 20f)
        {
            direction = Direction.Up;
        }
        else if (angulo >= 21f && angulo <= 69f)
        {
            direction = Direction.UpRight;
        }
        else if (angulo >= 70f && angulo <= 110f)
        {
            direction = Direction.Right;
        }
        else if (angulo >= 111f && angulo <= 159f)
        {
            direction = Direction.DownRight;
        }
        else if (angulo >= 160f && angulo <= 200f)
        {
            direction = Direction.Down;
        }
        else if (angulo >= 201f && angulo <= 249f)
        {
            direction = Direction.DownLeft;
        }
        else if (angulo >= 250f && angulo <= 290f)
        {
            direction = Direction.Left;
        }
        else if (angulo >= 291f && angulo <= 339f)
        {
            direction = Direction.UpLeft;
        }
    }

    public void LookAtMouse()
    {
        if (PauseGame.Paused == false)
        {

            Vector3 MousePosition = Input.mousePosition;

            MousePosition = Camera.main.ScreenToWorldPoint(MousePosition);

            Vector2 MouseDirection = new Vector2(MousePosition.x - transform.position.x, MousePosition.y - transform.position.y);

            float angle = Mathf.Atan2(MouseDirection.y, MouseDirection.x) * Mathf.Rad2Deg;

            Quaternion rotacao = Quaternion.AngleAxis(angle + 10, Vector3.forward);

            Body.transform.rotation = rotacao;

        }
    }
    public void Start()
    {
        GunSp = Gun.GetComponent<SpriteRenderer>();
    }
    private void Awake()
    {
        Player = gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        NormalMovSpeedReference = NormalMovSpeed;

    }
    public void Update()
    {
        LookAtMouse();
    }
    private void FixedUpdate()
    {
        Vector2 CurrentPosition = transform.position;

        Vector3 Movimentdirection = new Vector3(moviment.x, moviment.y, 0f).normalized;

        transform.Translate(Movimentdirection * NormalMovSpeed * Time.deltaTime);

        Vector2 NewPosition = transform.position;

        if (NewPosition == CurrentPosition)
        {
            LegsAnimator.enabled = false;
        }
        else
        {
            LegsAnimator.enabled = true;
            if (NewPosition.x > CurrentPosition.x)
            {
                if(NewPosition.y == CurrentPosition.y)
                {
                    //Right
                    Legs.transform.rotation = Quaternion.Euler(0 ,0 ,0 );
                }
                else if(NewPosition.y > CurrentPosition.y)
                {
                    //UpRight
                    Legs.transform.rotation = Quaternion.Euler(0, 0, 45);
                }
                else
                {
                    //DownRight
                    Legs.transform.rotation = Quaternion.Euler(0, 0, -45);
                }
            }
            else if (NewPosition.x < CurrentPosition.x)
            {
                if (NewPosition.y == CurrentPosition.y)
                {
                    //Left
                    Legs.transform.rotation = Quaternion.Euler(0, 0, 180);
                }
                else if (NewPosition.y > CurrentPosition.y)
                {
                    //UpLeft
                    Legs.transform.rotation = Quaternion.Euler(0, 0, 135);
                }
                else
                {
                    //DownLeft
                    Legs.transform.rotation = Quaternion.Euler(0, 0, 225);
                }
            }
            
            else
            {
                if (NewPosition.y > CurrentPosition.y)
                {
                    //Up
                    Legs.transform.rotation = Quaternion.Euler(0, 0, -90);
                }
                else
                {
                    //Down
                    Legs.transform.rotation = Quaternion.Euler(0, 0, 90);
                }
            }
        }
    }
}



