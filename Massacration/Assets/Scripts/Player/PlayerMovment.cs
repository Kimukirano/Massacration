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
    private string Direction;
    public static Vector3 MouseDirection;
    public static GameObject Player;
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
    public String GetMouseAngle()
    {
        // Calcular o ângulo entre o vetor direção do mouse e o vetor para frente do objeto
        float angulo = Mathf.Atan2(GetMouseDirection().x, GetMouseDirection().y) * Mathf.Rad2Deg;

        if (angulo < 0){angulo += 360;}

        if (angulo >= 340f)
        {
            Direction = "Up";
        }
        else if (angulo <= 20f)
        {
            Direction = "Up";
        }
        else if (angulo >= 21f && angulo <= 69f)
        {
            Direction = "RightUp";
        }
        else if (angulo >= 70f && angulo <= 110f)
        {
            Direction = "Right";
        }
        else if (angulo >= 111f && angulo <= 159f)
        {
            Direction = "DownRight";
        }
        else if (angulo >= 160f && angulo <= 200f)
        {
            Direction = "Down";
        }
        else if (angulo >= 201f && angulo <= 249f)
        {
            Direction = "DownLeft";
        }
        else if (angulo >= 250f && angulo <= 290f)
        {
            Direction = "Left";
        }
        else if (angulo >= 291f && angulo <= 339f)
        {
            Direction = "LeftUp";
        }
        return Direction;
    }
    public void Start()
    {
       
    }
    private void Awake()
    {
        Player = gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
        NormalMovSpeedReference = NormalMovSpeed;
        
    }
    public void Update()
    {
        
    }
    private void FixedUpdate()
    {
        Vector2 CurrentPosition = transform.position;

        Vector3 Movimentdirection = new Vector3(moviment.x, moviment.y, 0f).normalized;

        transform.Translate(Movimentdirection * NormalMovSpeed * Time.deltaTime);

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
                    if(InAim==true)
                    {
                        switch (GetMouseAngle())
                        {
                            case "Up": 
                                //Right walk looking UP
                                return;
                            case "RightUp":
                                //Right walk looking UpRight
                                return;
                            case "Right":
                                //Right walk looking Right
                                return;
                            case "DownRight":
                                //Right walk looking DownRight
                                return;
                            case "Down":
                                //Right walk looking Down
                                return;
                            case "DownLeft":
                                //Right walk looking DownLeft
                                return;
                            case "Left":
                                //Right walk looking Left
                                return;
                            case "UpLeft":
                                //Right walk looking Up
                                return;
                        }
                    }
                    else
                    {
                        spriteRenderer.sprite = Right;
                    }
                }
                else if(NewPosition.y > CurrentPosition.y)
                {
                    if (InAim == true)
                    {
                        switch (GetMouseAngle())
                        {
                            case "Up":
                                //UpRight walk looking UP
                                return;
                            case "RightUp":
                                //UpRight walk looking UpRight
                                return;
                            case "Right":
                                //UpRight walk looking Right
                                return;
                            case "DownRight":
                                //UpRight walk looking DownRight
                                return;
                            case "Down":
                                //UpRight walk looking Down
                                return;
                            case "DownLeft":
                                //UpRight walk looking DownLeft
                                return;
                            case "Left":
                                //UpRight walk looking Left
                                return;
                            case "UpLeft":
                                //UpRight walk looking Up
                                return;
                        }
                    }
                    else
                    {
                        spriteRenderer.sprite = UpRight;
                    }
                }
                else
                {
                    if (InAim == true)
                    {
                        switch (GetMouseAngle())
                        {
                            case "Up":
                                //DownRight walk looking UP
                                return;
                            case "RightUp":
                                //DownRight walk looking UpRight
                                return;
                            case "Right":
                                //DownRight walk looking Right
                                return;
                            case "DownRight":
                                //DownRight walk looking DownRight
                                return;
                            case "Down":
                                //DownRight walk looking Down
                                return;
                            case "DownLeft":
                                //DownRight walk looking DownLeft
                                return;
                            case "Left":
                                //DownRight walk looking Left
                                return;
                            case "UpLeft":
                                //DownRight walk looking Up
                                return;
                        }
                    }
                    else
                    {
                        spriteRenderer.sprite = DownRight;
                    }
                }
            }
            else if (NewPosition.x < CurrentPosition.x)
            {
                if (NewPosition.y == CurrentPosition.y)
                {
                    if (InAim == true)
                    {
                        switch (GetMouseAngle())
                        {
                            case "Up":
                                //Left walk looking UP
                                return;
                            case "RightUp":
                                //Left walk looking UpRight
                                return;
                            case "Right":
                                //Left walk looking Right
                                return;
                            case "DownRight":
                                //Left walk looking DownRight
                                return;
                            case "Down":
                                //Left walk looking Down
                                return;
                            case "DownLeft":
                                //Left walk looking DownLeft
                                return;
                            case "Left":
                                //Left walk looking Left
                                return;
                            case "UpLeft":
                                //Left walk looking Up
                                return;
                        }
                    }
                    else
                    {
                        spriteRenderer.sprite = Left;
                    }   
                }
                else if (NewPosition.y > CurrentPosition.y)
                {
                    if (InAim == true)
                    {
                        switch (GetMouseAngle())
                        {
                            case "Up":
                                //UpLeft walk looking UP
                                return;
                            case "RightUp":
                                //UpLeft walk looking UpRight
                                return;
                            case "Right":
                                //UpLeft walk looking Right
                                return;
                            case "DownRight":
                                //UpLeft walk looking DownRight
                                return;
                            case "Down":
                                //UpLeft walk looking Down
                                return;
                            case "DownLeft":
                                //UpLeft walk looking DownLeft
                                return;
                            case "Left":
                                //UpLeft walk looking Left
                                return;
                            case "UpLeft":
                                //UpLeft walk looking Up
                                return;
                        }
                    }
                    else
                    {
                        spriteRenderer.sprite = UpLeft;
                    }
                }
                else
                {
                    if (InAim == true)
                    {
                        switch (GetMouseAngle())
                        {
                            case "Up":
                                //DownLeft walk looking UP
                                return;
                            case "RightUp":
                                //DownLeft walk looking UpRight
                                return;
                            case "Right":
                                //DownLeft walk looking Right
                                return;
                            case "DownRight":
                                //DownLeft walk looking DownRight
                                return;
                            case "Down":
                                //DownLeft walk looking Down
                                return;
                            case "DownLeft":
                                //DownLeft walk looking DownLeft
                                return;
                            case "Left":
                                //DownLeft walk looking Left
                                return;
                            case "UpLeft":
                                //DownLeft walk looking Up
                                return;
                        }
                    }
                    else
                    {
                        spriteRenderer.sprite = DownLeft;
                    }
                }
            }
            
            else
            {
                if (NewPosition.y > CurrentPosition.y)
                {
                    if (InAim == true)
                    {
                        switch (GetMouseAngle())
                        {
                            case "Up":
                                //Up walk looking UP
                                return;
                            case "RightUp":
                                //Up walk looking UpRight
                                return;
                            case "Right":
                                //Up walk looking Right
                                return;
                            case "DownRight":
                                //Up walk looking DownRight
                                return;
                            case "Down":
                                //Up walk looking Down
                                return;
                            case "DownLeft":
                                //Up walk looking DownLeft
                                return;
                            case "Left":
                                //Up walk looking Left
                                return;
                            case "UpLeft":
                                //Up walk looking Up
                                return;
                        }
                    }
                    else
                    {
                        spriteRenderer.sprite = Up;
                    }
                }
                else
                {
                    if (InAim == true)
                    {
                        switch (GetMouseAngle())
                        {
                            case "Up":
                                //Down walk looking UP
                                return;
                            case "RightUp":
                                //Down walk looking UpRight
                                return;
                            case "Right":
                                //Down walk looking Right
                                return;
                            case "DownRight":
                                //Down walk looking DownRight
                                return;
                            case "Down":
                                //Down walk looking Down
                                return;
                            case "DownLeft":
                                //Down walk looking DownLeft
                                return;
                            case "Left":
                                //Down walk looking Left
                                return;
                            case "UpLeft":
                                //Down walk looking Up
                                return;
                        }
                    }
                    else
                    {
                        spriteRenderer.sprite = Down;
                    }
                }
            }
        }
    }
}



