using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] GUNSO Slot1SO;
    [SerializeField] GUNSO Slot2SO;
    [SerializeField] GUNSO Slot3SO;
    [SerializeField] GUNSO Slot4SO;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] float RecoilCutOffTime;
    [SerializeField] Transform Transformcamera;
    private GameObject Bullet;
    private float BulletSpeed;
    private int BulletLife;
    private bool Automatic;
    private string WeaponName;
    private int TotalAmmo;
    private int Ammo;
    private int MagSize;
    private int Damage;
    private float ReloadingTime;
    private int BulletHP;
    [SerializeField] TMP_Text AmmoText;
    private AudioClip ShootSound;
    private AudioClip OutOfAmmoSound;
    private AudioClip ReloadingSound;
    private float DelayShoot;
    [SerializeField] GameObject Gun;
    [SerializeField] Transform GunEffectSpawmPosition;
    [SerializeField] Transform SpawmCartridgesLocation;
    private bool OnReloading = false;
    private SpriteRenderer GunSpriteRenderer;
    [SerializeField] GameObject WeaponPanel;
    [SerializeField] GameObject GunBulletUIimage;
    private Image GunBulletUIimageImage;
    private GameObject CaseCartridge;
    public static Sprite GunSpriteMirror;
    public static Sprite GunSprite;
    private GameObject ShootLightEffect;
    public enum RecoilMode
    {
        Force,
        velocity,
    }
    public RecoilMode recoilMode;
    [SerializeField] Color UnselectedWeaponColor;
    [SerializeField] Color SelectedWeaponColor;
    public enum UsingWeapon
    {
        Slot1,
        Slot2,
        Slot3,
        Slot4,
        Nothing,
    }
    public static UsingWeapon usingWeapon;

    [Header("Slot1")]
    private string Slot1_WeaponName;
    private bool Slot1_Automatic;
    private int Slot1_TotalAmmo;
    private int Slot1_Ammo;
    private int Slot1_MagSize;
    private GameObject Slot1_BulletPrefab;
    private float Slot1_BulletSpeed;
    private float Slot1_DelayShoot;
    private float Slot1_RecoilForce;
    private float Slot1_RecoilVelocity;
    private int Slot1_Damage;
    private int Slot1_BulletHP;
    public static Sprite Slot1_Sprite;
    private float Slot1_ReloadingTime;
    private AudioClip Slot1_ShootingSound;
    private AudioClip Slot1_ReloadingSound;
    private AudioClip Slot1_EmptyMagSound;
    private GameObject Slot1_CaseCartridge;
    public static Sprite Slot1_GunSpriteMirror;
    public static Sprite Slot1_GunSprite;
    private GameObject Slot1_ShootLightEffect;
    [SerializeField] private GameObject Slot1_Panel;
    private Image Slot1_PanelImage;

    [Header("Slot2")]
    private string Slot2_WeaponName;
    private bool Slot2_Automatic;
    private int Slot2_TotalAmmo;
    private int Slot2_Ammo;
    private int Slot2_MagSize;
    private GameObject Slot2_BulletPrefab;
    private float Slot2_BulletSpeed;
    private float Slot2_DelayShoot;
    private float Slot2_RecoilForce;
    private float Slot2_RecoilVelocity;
    private int Slot2_Damage;
    private int Slot2_BulletHP;
    private float Slot2_ReloadingTime;
    private AudioClip Slot2_ShootingSound;
    private AudioClip Slot2_ReloadingSound;
    private AudioClip Slot2_EmptyMagSound;
    private GameObject Slot2_CaseCartridge;
    public static Sprite Slot2_GunSpriteMirror;
    public static Sprite Slot2_GunSprite;
    private GameObject Slot2_ShootLightEffect;
    [SerializeField] private GameObject Slot2_Panel;
    private Image Slot2_PanelImage;

    [Header("Slot3")]
    private string Slot3_WeaponName;
    private bool Slot3_Automatic;
    private int Slot3_TotalAmmo;
    private int Slot3_Ammo;
    private int Slot3_MagSize;
    private GameObject Slot3_BulletPrefab;
    private float Slot3_BulletSpeed;
    private float Slot3_DelayShoot;
    private float Slot3_RecoilForce;
    private float Slot3_RecoilVelocity;
    private int Slot3_Damage;
    private int Slot3_BulletHP;
    private float Slot3_ReloadingTime;
    private AudioClip Slot3_ShootingSound;
    private AudioClip Slot3_ReloadingSound;
    private AudioClip Slot3_EmptyMagSound;
    private GameObject Slot3_CaseCartridge;
    public static Sprite Slot3_GunSpriteMirror;
    public static Sprite Slot3_GunSprite;
    private GameObject Slot3_ShootLightEffect;
    [SerializeField] private GameObject Slot3_Panel;
    private Image Slot3_PanelImage;

    [Header("Slot4")]
    private string Slot4_WeaponName;
    private bool Slot4_Automatic;
    private int Slot4_TotalAmmo;
    private int Slot4_Ammo;
    private int Slot4_MagSize;
    private GameObject Slot4_BulletPrefab;
    private float Slot4_BulletSpeed;
    private float Slot4_DelayShoot;
    private float Slot4_RecoilForce;
    private float Slot4_RecoilVelocity;
    private int Slot4_Damage;
    private int Slot4_BulletHP;
    private float Slot4_ReloadingTime;
    private AudioClip Slot4_ShootingSound;
    private AudioClip Slot4_ReloadingSound;
    private AudioClip Slot4_EmptyMagSound;
    private GameObject Slot4_CaseCartridge;
    public static Sprite Slot4_GunSpriteMirror;
    public static Sprite Slot4_GunSprite;
    private GameObject Slot4_ShootLightEffect;
    [SerializeField] private GameObject Slot4_Panel;
    private Image Slot4_PanelImage;

    public static bool ShootingStarted = false;

    public void ActualizeSlot(int Slot)
    {
        switch (Slot)
        {
            case 1:
                Slot1_WeaponName = Slot1SO.WeaponName;
                Slot1_Automatic = Slot1SO.Automatic;
                Slot1_TotalAmmo = Slot1SO.TotalAmmo;
                Slot1_Ammo = Slot1SO.Ammo;
                Slot1_MagSize = Slot1SO.MagSize;
                Slot1_BulletPrefab = Slot1SO.BulletPrefab;
                Slot1_BulletSpeed = Slot1SO.BulletSpeed;
                Slot1_DelayShoot = Slot1SO.DelayShoot;
                Slot1_RecoilForce = Slot1SO.RecoilForce;
                Slot1_RecoilVelocity = Slot1SO.RecoilVelocity;
                Slot1_Damage = Slot1SO.Damage;
                Slot1_BulletHP = Slot1SO.BulletHP;
                Slot1_GunSprite = Slot1SO.GunSprite;
                Slot1_ReloadingTime = Slot1SO.ReloadingTime;
                Slot1_ShootingSound = Slot1SO.ShootingSound;
                Slot1_ReloadingSound = Slot1SO.ReloadingSound;
                Slot1_EmptyMagSound = Slot1SO.EmptyMagSound;
                Slot1_CaseCartridge = Slot1SO.CaseCartridge;
                Slot1_GunSpriteMirror = Slot1SO.GunSpriteMirror;
                Slot1_GunSprite = Slot1SO.GunSprite;
                Slot1_ShootLightEffect = Slot1SO.ShootLightEffect;
                break;
            case 2:
                Slot2_WeaponName = Slot2SO.WeaponName;
                Slot2_Automatic = Slot2SO.Automatic;
                Slot2_TotalAmmo = Slot2SO.TotalAmmo;
                Slot2_Ammo = Slot2SO.Ammo;
                Slot2_MagSize = Slot2SO.MagSize;
                Slot2_BulletPrefab = Slot2SO.BulletPrefab;
                Slot2_BulletSpeed = Slot2SO.BulletSpeed;
                Slot2_DelayShoot = Slot2SO.DelayShoot;
                Slot2_RecoilForce = Slot2SO.RecoilForce;
                Slot2_RecoilVelocity = Slot2SO.RecoilVelocity;
                Slot2_Damage = Slot2SO.Damage;
                Slot2_BulletHP = Slot2SO.BulletHP;
                Slot2_GunSprite = Slot2SO.GunSprite;
                Slot2_ReloadingTime = Slot2SO.ReloadingTime;
                Slot2_ShootingSound = Slot2SO.ShootingSound;
                Slot2_ReloadingSound = Slot2SO.ReloadingSound;
                Slot2_EmptyMagSound = Slot2SO.EmptyMagSound;
                Slot2_CaseCartridge = Slot2SO.CaseCartridge;
                Slot2_GunSpriteMirror = Slot2SO.GunSpriteMirror;
                Slot2_ShootLightEffect = Slot2SO.ShootLightEffect;
                break;
            case 3:
                Slot3_WeaponName = Slot3SO.WeaponName;
                Slot3_Automatic = Slot3SO.Automatic;
                Slot3_TotalAmmo = Slot3SO.TotalAmmo;
                Slot3_Ammo = Slot3SO.Ammo;
                Slot3_MagSize = Slot3SO.MagSize;
                Slot3_BulletPrefab = Slot3SO.BulletPrefab;
                Slot3_BulletSpeed = Slot3SO.BulletSpeed;
                Slot3_DelayShoot = Slot3SO.DelayShoot;
                Slot3_RecoilForce = Slot3SO.RecoilForce;
                Slot3_RecoilVelocity = Slot3SO.RecoilVelocity;
                Slot3_Damage = Slot3SO.Damage;
                Slot3_BulletHP = Slot3SO.BulletHP;
                Slot3_GunSprite = Slot3SO.GunSprite;
                Slot3_ReloadingTime = Slot3SO.ReloadingTime;
                Slot3_ShootingSound = Slot3SO.ShootingSound;
                Slot3_ReloadingSound = Slot3SO.ReloadingSound;
                Slot3_EmptyMagSound = Slot3SO.EmptyMagSound;
                Slot3_CaseCartridge = Slot3SO.CaseCartridge;
                Slot3_GunSpriteMirror = Slot3SO.GunSpriteMirror;
                Slot3_ShootLightEffect = Slot3SO.ShootLightEffect;
                break;
            case 4:
                Slot4_WeaponName = Slot4SO.WeaponName;
                Slot4_Automatic = Slot4SO.Automatic;
                Slot4_TotalAmmo = Slot4SO.TotalAmmo;
                Slot4_Ammo = Slot4SO.Ammo;
                Slot4_MagSize = Slot4SO.MagSize;
                Slot4_BulletPrefab = Slot4SO.BulletPrefab;
                Slot4_BulletSpeed = Slot4SO.BulletSpeed;
                Slot4_DelayShoot = Slot4SO.DelayShoot;
                Slot4_RecoilForce = Slot4SO.RecoilForce;
                Slot4_RecoilVelocity = Slot4SO.RecoilVelocity;
                Slot4_Damage = Slot4SO.Damage;
                Slot4_BulletHP = Slot4SO.BulletHP;
                Slot4_GunSprite = Slot4SO.GunSprite;
                Slot4_ReloadingTime = Slot4SO.ReloadingTime;
                Slot4_ShootingSound = Slot4SO.ShootingSound;
                Slot4_ReloadingSound = Slot4SO.ReloadingSound;
                Slot4_EmptyMagSound = Slot4SO.EmptyMagSound;
                Slot4_CaseCartridge = Slot4SO.CaseCartridge;
                Slot4_GunSpriteMirror = Slot4SO.GunSpriteMirror;
                Slot4_ShootLightEffect = Slot4SO.ShootLightEffect;
                break;
        }
    }


    public void UseWeapon1()
    {
        usingWeapon = UsingWeapon.Slot1;
        WeaponName = Slot1_WeaponName;
        Automatic = Slot1_Automatic;
        TotalAmmo = Slot1_TotalAmmo;
        Ammo = Slot1_Ammo;
        MagSize = Slot1_MagSize;
        Bullet = Slot1_BulletPrefab;
        BulletSpeed = Slot1_BulletSpeed;
        DelayShoot = Slot1_DelayShoot;
        Damage = Slot1_Damage;
        BulletHP = Slot1_BulletHP;
        GunSprite = Slot1_GunSprite;
        GunSpriteRenderer.sprite = GunSprite;
        ReloadingTime = Slot1_ReloadingTime;
        ShootSound = Slot1_ShootingSound;
        ReloadingSound = Slot1_ReloadingSound;
        OutOfAmmoSound = Slot1_EmptyMagSound;
        WeaponPanel = Slot1_Panel;
        GunBulletUIimageImage.sprite = Slot1_GunSprite;
        AmmoText.text = Slot1_Ammo.ToString() + "/" + Slot1_TotalAmmo.ToString();
        CaseCartridge = Slot1_CaseCartridge;
        GunSpriteMirror = Slot1_GunSpriteMirror;
        ShootLightEffect = Slot1_ShootLightEffect;
        Slot1_PanelImage.color = SelectedWeaponColor;

        Slot2_PanelImage.color = UnselectedWeaponColor;
        Slot3_PanelImage.color = UnselectedWeaponColor;
        Slot4_PanelImage.color = UnselectedWeaponColor;
    }
    public void UseWeapon2()
    {
        usingWeapon = UsingWeapon.Slot2;
        WeaponName = Slot2_WeaponName;
        Automatic = Slot2_Automatic;
        TotalAmmo = Slot2_TotalAmmo;
        Ammo = Slot2_Ammo;
        MagSize = Slot2_MagSize;
        Bullet = Slot2_BulletPrefab;
        BulletSpeed = Slot2_BulletSpeed;
        DelayShoot = Slot2_DelayShoot;
        Damage = Slot2_Damage;
        BulletHP = Slot2_BulletHP;
        GunSprite = Slot2_GunSprite;
        GunSpriteRenderer.sprite = GunSprite;
        ReloadingTime = Slot2_ReloadingTime;
        ShootSound = Slot2_ShootingSound;
        ReloadingSound = Slot2_ReloadingSound;
        OutOfAmmoSound = Slot2_EmptyMagSound;
        WeaponPanel = Slot2_Panel;
        GunBulletUIimageImage.sprite = Slot2_GunSprite;
        AmmoText.text = Slot2_Ammo.ToString() + "/" + Slot2_TotalAmmo.ToString();
        CaseCartridge = Slot2_CaseCartridge;
        GunSpriteMirror = Slot2_GunSpriteMirror;
        ShootLightEffect = Slot2_ShootLightEffect;
        Slot2_PanelImage.color = SelectedWeaponColor;

        Slot1_PanelImage.color = UnselectedWeaponColor;
        Slot3_PanelImage.color = UnselectedWeaponColor;
        Slot4_PanelImage.color = UnselectedWeaponColor;
    }
    public void UseWeapon3()
    {
        usingWeapon = UsingWeapon.Slot3;
        WeaponName = Slot3_WeaponName;
        Automatic = Slot3_Automatic;
        TotalAmmo = Slot3_TotalAmmo;
        Ammo = Slot3_Ammo;
        MagSize = Slot3_MagSize;
        Bullet = Slot3_BulletPrefab;
        BulletSpeed = Slot3_BulletSpeed;
        DelayShoot = Slot3_DelayShoot;
        Damage = Slot3_Damage;
        BulletHP = Slot3_BulletHP;
        GunSprite = Slot3_GunSprite;
        GunSpriteRenderer.sprite = GunSprite;
        ReloadingTime = Slot3_ReloadingTime;
        ShootSound = Slot3_ShootingSound;
        ReloadingSound = Slot3_ReloadingSound;
        OutOfAmmoSound = Slot3_EmptyMagSound;
        WeaponPanel = Slot3_Panel;
        GunBulletUIimageImage.sprite = Slot3_GunSprite;
        AmmoText.text = Slot3_Ammo.ToString() + "/" + Slot3_TotalAmmo.ToString();
        CaseCartridge = Slot3_CaseCartridge;
        GunSpriteMirror = Slot3_GunSpriteMirror;
        ShootLightEffect = Slot3_ShootLightEffect;
        Slot3_PanelImage.color = SelectedWeaponColor;

        Slot1_PanelImage.color = UnselectedWeaponColor;
        Slot2_PanelImage.color = UnselectedWeaponColor;
        Slot4_PanelImage.color = UnselectedWeaponColor;
    }
    public void UseWeapon4()
    {
        usingWeapon = UsingWeapon.Slot4;
        WeaponName = Slot4_WeaponName;
        Automatic = Slot4_Automatic;
        TotalAmmo = Slot4_TotalAmmo;
        Ammo = Slot4_Ammo;
        MagSize = Slot4_MagSize;
        Bullet = Slot4_BulletPrefab;
        BulletSpeed = Slot4_BulletSpeed;
        DelayShoot = Slot4_DelayShoot;
        Damage = Slot4_Damage;
        BulletHP = Slot4_BulletHP;
        GunSprite = Slot4_GunSprite;
        GunSpriteRenderer.sprite = GunSprite;
        ReloadingTime = Slot4_ReloadingTime;
        ShootSound = Slot4_ShootingSound;
        ReloadingSound = Slot4_ReloadingSound;
        OutOfAmmoSound = Slot4_EmptyMagSound;
        WeaponPanel = Slot4_Panel;
        GunBulletUIimageImage.sprite = Slot4_GunSprite;
        AmmoText.text = Slot4_Ammo.ToString() + "/" + Slot4_TotalAmmo.ToString();
        CaseCartridge = Slot4_CaseCartridge;
        GunSpriteMirror = Slot4_GunSpriteMirror;
        ShootLightEffect = Slot4_ShootLightEffect;
        Slot4_PanelImage.color = SelectedWeaponColor;

        Slot1_PanelImage.color = UnselectedWeaponColor;
        Slot2_PanelImage.color = UnselectedWeaponColor;
        Slot3_PanelImage.color = UnselectedWeaponColor;
    }


    public void ScrollWeapons(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {

        }
    }
    public void SelectWeapon1(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            UseWeapon1();
        }
    }
    public void SelectWeapon2(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            UseWeapon2();
        }
    }
    public void SelectWeapon3(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            UseWeapon3();
        }
    }
    public void SelectWeapon4(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            UseWeapon4();
        }
    }

    public void ShootingBullet()
    {
        switch (usingWeapon)
        {
            case UsingWeapon.Slot1:
                if (Slot1_Ammo > 0 && OnReloading == false)
                {
                    ShootingStarted = true;
                    gameObject.GetComponent<AudioSource>().clip = Slot1_ShootingSound;
                    gameObject.GetComponent<AudioSource>().Play();
                    Vector3 BulletSpawmPosition = GunEffectSpawmPosition.transform.position;
                    GameObject BulletAtual = Instantiate(Slot1_BulletPrefab, BulletSpawmPosition, Quaternion.identity);
                    BulletAtual.GetComponent<Rigidbody2D>().velocity = PlayerMovment.GetMouseDirection() * Slot1_BulletSpeed;
                    ShootLightEfectFunction();
                    Slot1_Ammo--;
                    AmmoText.text = Slot1_Ammo.ToString() + "/" + Slot1_TotalAmmo.ToString();
                    Recoil(Slot1_RecoilForce, Slot1_RecoilVelocity);
                    ShakeCamera();
                    EjectCasesCartridges();
                }
                else
                {
                    gameObject.GetComponent<AudioSource>().clip = Slot1_EmptyMagSound;
                    gameObject.GetComponent<AudioSource>().Play();
                }
                break;
            case UsingWeapon.Slot2:
                if (Slot2_Ammo > 0 && OnReloading == false)
                {
                    ShootingStarted = true;
                    gameObject.GetComponent<AudioSource>().clip = Slot2_ShootingSound;
                    gameObject.GetComponent<AudioSource>().Play();
                    Vector3 BulletSpawmPosition = gameObject.transform.position;
                    BulletSpawmPosition.y += 0.5f;
                    GameObject BulletAtual = Instantiate(Slot2_BulletPrefab, BulletSpawmPosition, Quaternion.identity);
                    BulletAtual.GetComponent<Rigidbody2D>().velocity = PlayerMovment.GetMouseDirection() * Slot2_BulletSpeed;
                    ShootLightEfectFunction();
                    Slot2_Ammo--;
                    AmmoText.text = Slot2_Ammo.ToString() + "/" + Slot2_TotalAmmo.ToString();
                    Recoil(Slot2_RecoilForce, Slot2_RecoilVelocity);
                    ShakeCamera();
                    EjectCasesCartridges();
                }
                else
                {
                    gameObject.GetComponent<AudioSource>().clip = Slot2_EmptyMagSound;
                    gameObject.GetComponent<AudioSource>().Play();
                }
                break;
            case UsingWeapon.Slot3:
                if (Slot3_Ammo > 0 && OnReloading == false)
                {
                    ShootingStarted = true;
                    gameObject.GetComponent<AudioSource>().clip = Slot3_ShootingSound;
                    gameObject.GetComponent<AudioSource>().Play();
                    Vector3 BulletSpawmPosition = gameObject.transform.position;
                    BulletSpawmPosition.y += 0.5f;
                    GameObject BulletAtual = Instantiate(Slot3_BulletPrefab, BulletSpawmPosition, Quaternion.identity);
                    BulletAtual.GetComponent<Rigidbody2D>().velocity = PlayerMovment.GetMouseDirection() * Slot3_BulletSpeed;
                    ShootLightEfectFunction();
                    Slot3_Ammo--;
                    AmmoText.text = Slot3_Ammo.ToString() + "/" + Slot3_TotalAmmo.ToString();
                    Recoil(Slot3_RecoilForce, Slot3_RecoilVelocity);
                    ShakeCamera();
                    EjectCasesCartridges();
                }
                else
                {
                    gameObject.GetComponent<AudioSource>().clip = Slot3_EmptyMagSound;
                    gameObject.GetComponent<AudioSource>().Play();
                }
                break;
            case UsingWeapon.Slot4:
                if (Slot4_Ammo > 0 && OnReloading == false)
                {
                    ShootingStarted = true;
                    gameObject.GetComponent<AudioSource>().clip = Slot4_ShootingSound;
                    gameObject.GetComponent<AudioSource>().Play();
                    Vector3 BulletSpawmPosition = gameObject.transform.position;
                    BulletSpawmPosition.y += 0.5f;
                    GameObject BulletAtual = Instantiate(Slot4_BulletPrefab, BulletSpawmPosition, Quaternion.identity);
                    BulletAtual.GetComponent<Rigidbody2D>().velocity = PlayerMovment.GetMouseDirection() * Slot4_BulletSpeed;
                    ShootLightEfectFunction();
                    Slot4_Ammo--;
                    AmmoText.text = Slot4_Ammo.ToString() + "/" + Slot4_TotalAmmo.ToString();
                    Recoil(Slot4_RecoilForce, Slot4_RecoilVelocity);
                    ShakeCamera();
                    EjectCasesCartridges();
                }
                else
                {
                    gameObject.GetComponent<AudioSource>().clip = Slot4_EmptyMagSound;
                    gameObject.GetComponent<AudioSource>().Play();
                }
                break;
        }
    }

    public void Shoot(InputAction.CallbackContext ctx)
    {
        
        if (ctx.started)
        {
            if (PlayerMovment.InAim == true)
            {
                switch (usingWeapon)
                {
                    case UsingWeapon.Slot1:
                        if (Slot1_Automatic == true)
                        {
                            InvokeRepeating("ShootingBullet", 0f, Slot1_DelayShoot);
                        }
                        else
                        {
                            Invoke("ShootingBullet", 0f);
                        }
                        break;
                    case UsingWeapon.Slot2:
                        if (Slot2_Automatic == true)
                        {
                            InvokeRepeating("ShootingBullet", 0f, Slot2_DelayShoot);
                        }
                        else
                        {
                            Invoke("ShootingBullet", 0f);
                        }
                        break;
                    case UsingWeapon.Slot3:
                        if (Slot3_Automatic == true)
                        {
                            InvokeRepeating("ShootingBullet", 0f, Slot3_DelayShoot);
                        }
                        else
                        {
                            Invoke("ShootingBullet", 0f);
                        }
                        break;
                    case UsingWeapon.Slot4:
                        if (Slot4_Automatic == true)
                        {
                            InvokeRepeating("ShootingBullet", 0f, Slot4_DelayShoot);
                        }
                        else
                        {
                            Invoke("ShootingBullet", 0f);
                        }
                        break;
                }   
            }
        }
        if (ctx.canceled)
        {
            CancelInvoke("ShootingBullet");
        }
    }
    public void ShakeCamera()
    {
        Transformcamera.DOShakePosition(0.1f, 0.2f, 10, 90f, false, false, ShakeRandomnessMode.Full);
    }
    public void CancelRigidbody2DForces()
    {
        rb.velocity = new Vector3(0f, 0f, 0f);
        rb.totalForce = new Vector3(0f, 0f, 0f);
    }
    public void Recoil(float GunRecoilForce, float GunRecoilVelocity)
    {
        Vector3 recoilDirection = PlayerMovment.MouseDirection * -1;

        if (recoilMode == RecoilMode.Force)
        {
            rb.AddForce(recoilDirection * GunRecoilForce);
        }
        else
        {
            rb.velocity = recoilDirection * GunRecoilVelocity;
        }
        Invoke("CancelRigidbody2DForces", RecoilCutOffTime);
    }

    public void EjectCasesCartridges()
    {
        Vector3 ForceEjection3D = PlayerMovment.MouseDirection * -50;
        Vector2 ForceEjection = new Vector2(ForceEjection3D.x, ForceEjection3D.y);
        Vector2 RotatedForceEjection;
        if (ForceEjection.x > 0)
        {
            RotatedForceEjection = new Vector2(-ForceEjection.y, ForceEjection.x);
        }
        else
        {
            RotatedForceEjection = new Vector2(ForceEjection.y, -ForceEjection.x);
        }
        float RandonXforce = UnityEngine.Random.Range(0f, 1f);
        float RandonYforce = UnityEngine.Random.Range(0f, 1f);
        Vector2 RandonForceEjection = new Vector2(RotatedForceEjection.x + RandonXforce, RotatedForceEjection.y - RandonYforce);
        Vector2 InstanceLocation = new Vector2(SpawmCartridgesLocation.transform.position.x , SpawmCartridgesLocation.transform.position.y);
        GameObject InstantiatedCaseCartridge = Instantiate(CaseCartridge, InstanceLocation, Quaternion.identity);
        InstantiatedCaseCartridge.GetComponent<Rigidbody2D>().velocity = RandonForceEjection;
        InstantiatedCaseCartridge.GetComponent<Rigidbody2D>().DORotate(-720f, 4f);
        
    }
    public void ShootLightEfectFunction()
    {
        Vector3 SpawmPosition = GunEffectSpawmPosition.transform.position;
        Quaternion additionalRotation = Quaternion.Euler(0, 0, -100);
        Quaternion newRotation = Gun.transform.rotation * additionalRotation;
        GameObject InstantiatedShootLightEffect = Instantiate(ShootLightEffect, SpawmPosition, newRotation);
        InstantiatedShootLightEffect.transform.SetParent(GunEffectSpawmPosition);
    }

    public void Reload()
    {

        switch (usingWeapon)
        {
            case UsingWeapon.Slot1:
                int AmmoToReload1 = Slot1_MagSize - Slot1_Ammo;
                if (AmmoToReload1 > Slot1_TotalAmmo)
                {
                    AmmoToReload1 = Slot1_TotalAmmo;
                }
                Slot1_Ammo += AmmoToReload1;
                Slot1_TotalAmmo -= AmmoToReload1;
                AmmoText.text = Slot1_Ammo.ToString() + "/" + Slot1_TotalAmmo.ToString();
                OnReloading = false;
                break;
            case UsingWeapon.Slot2:
                int AmmoToReload2 = Slot2_MagSize - Slot2_Ammo;
                if (AmmoToReload2 > Slot2_TotalAmmo)
                {
                    AmmoToReload2 = Slot2_TotalAmmo;
                }
                Slot2_Ammo += AmmoToReload2;
                Slot2_TotalAmmo -= AmmoToReload2;
                AmmoText.text = Slot2_Ammo.ToString() + "/" + Slot2_TotalAmmo.ToString();
                OnReloading = false;
                break;
            case UsingWeapon.Slot3:
                int AmmoToReload3 = Slot3_MagSize - Slot3_Ammo;
                if (AmmoToReload3 > Slot3_TotalAmmo)
                {
                    AmmoToReload3 = Slot3_TotalAmmo;
                }
                Slot3_Ammo += AmmoToReload3;
                Slot3_TotalAmmo -= AmmoToReload3;
                AmmoText.text = Slot3_Ammo.ToString() + "/" + Slot3_TotalAmmo.ToString();
                OnReloading = false;
                break;
            case UsingWeapon.Slot4:
                int AmmoToReload4 = Slot4_MagSize - Slot4_Ammo;
                if (AmmoToReload4 > Slot4_TotalAmmo)
                {
                    AmmoToReload4 = Slot4_TotalAmmo;
                }
                Slot4_Ammo += AmmoToReload4;
                Slot4_TotalAmmo -= AmmoToReload4;
                AmmoText.text = Slot4_Ammo.ToString() + "/" + Slot4_TotalAmmo.ToString();
                OnReloading = false;
                break;
        }
    }
 
    public void ReloadAction(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            switch (usingWeapon)
            {
                case UsingWeapon.Slot1:
                    if (Slot1_Ammo != Slot1_MagSize && Slot1_TotalAmmo != 0)
                    {
                        OnReloading = true;
                        gameObject.GetComponent<AudioSource>().clip = Slot1_ReloadingSound;
                        gameObject.GetComponent<AudioSource>().Play();
                        Invoke("Reload", Slot1_ReloadingTime);
                    }
                    break;
                case UsingWeapon.Slot2:
                    if (Slot2_Ammo != Slot2_MagSize && Slot2_TotalAmmo != 0)
                    {
                        OnReloading = true;
                        gameObject.GetComponent<AudioSource>().clip = Slot2_ReloadingSound;
                        gameObject.GetComponent<AudioSource>().Play();
                        Invoke("Reload", Slot2_ReloadingTime);
                    }
                    break;
                case UsingWeapon.Slot3:
                    if (Slot3_Ammo != Slot3_MagSize && Slot3_TotalAmmo != 0)
                    {
                        OnReloading = true;
                        gameObject.GetComponent<AudioSource>().clip = Slot3_ReloadingSound;
                        gameObject.GetComponent<AudioSource>().Play();
                        Invoke("Reload", Slot3_ReloadingTime);
                    }
                    break;
                case UsingWeapon.Slot4:
                    if (Slot4_Ammo != Slot4_MagSize && Slot4_TotalAmmo != 0)
                    {
                        OnReloading = true;
                        gameObject.GetComponent<AudioSource>().clip = Slot4_ReloadingSound;
                        gameObject.GetComponent<AudioSource>().Play();
                        Invoke("Reload", Slot4_ReloadingTime);
                    }
                    break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ActualizeSlot(1);
        ActualizeSlot(2);
        ActualizeSlot(3);
        ActualizeSlot(4);
        GunSpriteRenderer = Gun.GetComponent<SpriteRenderer>();
        GunBulletUIimageImage = GunBulletUIimage.GetComponent<Image>();
        Slot1_PanelImage = Slot1_Panel.GetComponent<Image>();
        Slot2_PanelImage = Slot2_Panel.GetComponent<Image>();
        Slot3_PanelImage = Slot3_Panel.GetComponent<Image>();
        Slot4_PanelImage = Slot4_Panel.GetComponent<Image>();
        usingWeapon = UsingWeapon.Slot1;
        UseWeapon1();
        AmmoText.text = Ammo.ToString() + "/" + TotalAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
