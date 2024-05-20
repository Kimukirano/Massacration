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
using static PlayerShoot;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] float RecoilCutOffTime;
    [SerializeField] Transform Transformcamera;
    [SerializeField] GameObject ShootLightPrefab;
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
    private bool OnReloading = false;
    private SpriteRenderer GunSpriteRenderer;
    [SerializeField] GameObject WeaponPanel;
    [SerializeField] GameObject GunBulletUIimage;
    private Image GunBulletUIimageImage;
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
        AK_47,
        MP5,
        DesertEagle,
        M4,
        Nothing,
    }
    public UsingWeapon usingWeapon;

    [Header("AK-47")]
    [SerializeField] string Ak47_WeaponName;
    [SerializeField] bool Ak47_Automatic;
    [SerializeField] int Ak47_TotalAmmo;
    [SerializeField] int Ak47_Ammo;
    [SerializeField] int Ak47_MagSize;
    [SerializeField] GameObject Ak47_BulletPrefab;
    [SerializeField] float Ak47_BulletSpeed;
    [SerializeField] float Ak47_DelayShoot;
    [SerializeField] float Ak47_RecoilForce;
    [SerializeField] float Ak47_RecoilVelocity;
    [SerializeField] int Ak47_Damage;
    [SerializeField] int Ak47_BulletHP;
    [SerializeField] Sprite AK47_Sprite;
    [SerializeField] float Ak47_ReloadingTime;
    [SerializeField] AudioClip Ak47_ShootingSound;
    [SerializeField] AudioClip Ak47_ReloadingSound;
    [SerializeField] AudioClip Ak47_EmptyMagSound;
    [SerializeField] GameObject Ak47_Panel;
    private Image Ak47_PanelImage;

    [Header("MP5")]
    [SerializeField] string MP5_WeaponName;
    [SerializeField] bool MP5_Automatic;
    [SerializeField] int MP5_TotalAmmo;
    [SerializeField] int MP5_Ammo;
    [SerializeField] int MP5_MagSize;
    [SerializeField] GameObject MP5_BulletPrefab;
    [SerializeField] float MP5_BulletSpeed;
    [SerializeField] float MP5_DelayShoot;
    [SerializeField] float MP5_RecoilForce;
    [SerializeField] float MP5_RecoilVelocity;
    [SerializeField] int MP5_Damage;
    [SerializeField] int MP5_BulletHP;
    [SerializeField] Sprite MP5_Sprite;
    [SerializeField] float MP5_ReloadingTime;
    [SerializeField] AudioClip MP5_ShootingSound;
    [SerializeField] AudioClip MP5_ReloadingSound;
    [SerializeField] AudioClip MP5_EmptyMagSound;
    [SerializeField] GameObject MP5_Panel;
    private Image MP5_PanelImage;

    [Header("Desert Eagle")]
    [SerializeField] string DesertEagle_WeaponName;
    [SerializeField] bool DesertEagle_Automatic;
    [SerializeField] int DesertEagle_TotalAmmo;
    [SerializeField] int DesertEagle_Ammo;
    [SerializeField] int DesertEagle_MagSize;
    [SerializeField] GameObject DesertEagle_BulletPrefab;
    [SerializeField] float DesertEagle_BulletSpeed;
    [SerializeField] float DesertEagle_DelayShoot;
    [SerializeField] float DesertEagle_RecoilForce;
    [SerializeField] float DesertEagle_RecoilVelocity;
    [SerializeField] int DesertEagle_Damage;
    [SerializeField] int DesertEagle_BulletHP;
    [SerializeField] Sprite DesertEagle_Sprite;
    [SerializeField] float DesertEagle_ReloadingTime;
    [SerializeField] AudioClip DesertEagle_ShootingSound;
    [SerializeField] AudioClip DesertEagle_ReloadingSound;
    [SerializeField] AudioClip DesertEagle_EmptyMagSound;
    [SerializeField] GameObject DesertEagle_Panel;
    private Image DesertEagle_PanelImage;

    [Header("M4")]
    [SerializeField] string M4_WeaponName;
    [SerializeField] bool M4_Automatic;
    [SerializeField] int M4_TotalAmmo;
    [SerializeField] int M4_Ammo;
    [SerializeField] int M4_MagSize;
    [SerializeField] GameObject M4_BulletPrefab;
    [SerializeField] float M4_BulletSpeed;
    [SerializeField] float M4_DelayShoot;
    [SerializeField] float M4_RecoilForce;
    [SerializeField] float M4_RecoilVelocity;
    [SerializeField] int M4_Damage;
    [SerializeField] int M4_BulletHP;
    [SerializeField] Sprite M4_Sprite;
    [SerializeField] float M4_ReloadingTime;
    [SerializeField] AudioClip M4_ShootingSound;
    [SerializeField] AudioClip M4_ReloadingSound;
    [SerializeField] AudioClip M4_EmptyMagSound;
    [SerializeField] GameObject M4_Panel;
    private Image M4_PanelImage;

    public static bool ShootingStarted = false;
    public void UseWeapon1()
    {
        usingWeapon = UsingWeapon.AK_47;
        WeaponName = Ak47_WeaponName;
        Automatic = Ak47_Automatic;
        TotalAmmo = Ak47_TotalAmmo;
        Ammo = Ak47_Ammo;
        MagSize = Ak47_MagSize;
        Bullet = Ak47_BulletPrefab;
        BulletSpeed = Ak47_BulletSpeed;
        DelayShoot = Ak47_DelayShoot;
        Damage = Ak47_Damage;
        BulletHP = Ak47_BulletHP;
        GunSpriteRenderer.sprite = AK47_Sprite;
        ReloadingTime = Ak47_ReloadingTime;
        ShootSound = Ak47_ShootingSound;
        ReloadingSound = Ak47_ReloadingSound;
        OutOfAmmoSound = Ak47_EmptyMagSound;
        WeaponPanel = Ak47_Panel;
        GunBulletUIimageImage.sprite = AK47_Sprite;
        AmmoText.text = Ak47_Ammo.ToString() + "/" + Ak47_TotalAmmo.ToString();
        Ak47_PanelImage.color = SelectedWeaponColor;

        MP5_PanelImage.color = UnselectedWeaponColor;
        DesertEagle_PanelImage.color = UnselectedWeaponColor;
        M4_PanelImage.color = UnselectedWeaponColor;
    }
    public void UseWeapon2()
    {
        usingWeapon = UsingWeapon.MP5;
        WeaponName = MP5_WeaponName;
        Automatic = MP5_Automatic;
        TotalAmmo = MP5_TotalAmmo;
        Ammo = MP5_Ammo;
        MagSize = MP5_MagSize;
        Bullet = MP5_BulletPrefab;
        BulletSpeed = MP5_BulletSpeed;
        DelayShoot = MP5_DelayShoot;
        Damage = MP5_Damage;
        BulletHP = MP5_BulletHP;
        GunSpriteRenderer.sprite = MP5_Sprite;
        ReloadingTime = MP5_ReloadingTime;
        ShootSound = MP5_ShootingSound;
        ReloadingSound = MP5_ReloadingSound;
        OutOfAmmoSound = MP5_EmptyMagSound;
        WeaponPanel = MP5_Panel;
        GunBulletUIimageImage.sprite = MP5_Sprite;
        AmmoText.text = MP5_Ammo.ToString() + "/" + MP5_TotalAmmo.ToString();
        MP5_PanelImage.color = SelectedWeaponColor;

        Ak47_PanelImage.color = UnselectedWeaponColor;
        DesertEagle_PanelImage.color = UnselectedWeaponColor;
        M4_PanelImage.color = UnselectedWeaponColor;
    }
    public void UseWeapon3()
    {
        usingWeapon = UsingWeapon.DesertEagle;
        WeaponName = DesertEagle_WeaponName;
        Automatic = DesertEagle_Automatic;
        TotalAmmo = DesertEagle_TotalAmmo;
        Ammo = DesertEagle_Ammo;
        MagSize = DesertEagle_MagSize;
        Bullet = DesertEagle_BulletPrefab;
        BulletSpeed = DesertEagle_BulletSpeed;
        DelayShoot = DesertEagle_DelayShoot;
        Damage = DesertEagle_Damage;
        BulletHP = DesertEagle_BulletHP;
        GunSpriteRenderer.sprite = DesertEagle_Sprite;
        ReloadingTime = DesertEagle_ReloadingTime;
        ShootSound = DesertEagle_ShootingSound;
        ReloadingSound = DesertEagle_ReloadingSound;
        OutOfAmmoSound = DesertEagle_EmptyMagSound;
        WeaponPanel = DesertEagle_Panel;
        GunBulletUIimageImage.sprite = DesertEagle_Sprite;
        AmmoText.text = DesertEagle_Ammo.ToString() + "/" + DesertEagle_TotalAmmo.ToString();
        DesertEagle_PanelImage.color = SelectedWeaponColor;

        Ak47_PanelImage.color = UnselectedWeaponColor;
        MP5_PanelImage.color = UnselectedWeaponColor;
        M4_PanelImage.color = UnselectedWeaponColor;
    }
    public void UseWeapon4()
    {
        usingWeapon = UsingWeapon.M4;
        WeaponName = M4_WeaponName;
        Automatic = M4_Automatic;
        TotalAmmo = M4_TotalAmmo;
        Ammo = M4_Ammo;
        MagSize = M4_MagSize;
        Bullet = M4_BulletPrefab;
        BulletSpeed = M4_BulletSpeed;
        DelayShoot = M4_DelayShoot;
        Damage = M4_Damage;
        BulletHP = M4_BulletHP;
        GunSpriteRenderer.sprite = M4_Sprite;
        ReloadingTime = M4_ReloadingTime;
        ShootSound = M4_ShootingSound;
        ReloadingSound = M4_ReloadingSound;
        OutOfAmmoSound = M4_EmptyMagSound;
        WeaponPanel = M4_Panel;
        GunBulletUIimageImage.sprite = M4_Sprite;
        AmmoText.text = M4_Ammo.ToString() + "/" + M4_TotalAmmo.ToString();
        M4_PanelImage.color = SelectedWeaponColor;

        Ak47_PanelImage.color = UnselectedWeaponColor;
        MP5_PanelImage.color = UnselectedWeaponColor;
        DesertEagle_PanelImage.color = UnselectedWeaponColor;
    }


    public void ScrollWeapons(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {

        }
    }
    public void SelectWeapon1(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
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
    public void ShootingAK47Bullet()
    {
        if(Ak47_Ammo > 0 && OnReloading == false)
        {
            ShootingStarted = true;
            gameObject.GetComponent<AudioSource>().clip = Ak47_ShootingSound;
            gameObject.GetComponent<AudioSource>().Play();
            Vector3 BulletSpawmPosition = gameObject.transform.position;
            BulletSpawmPosition.y += 0.5f;
            GameObject BulletAtual = Instantiate(Ak47_BulletPrefab, BulletSpawmPosition, Quaternion.identity);
            BulletAtual.GetComponent<Rigidbody2D>().velocity = PlayerMovment.GetMouseDirection() * Ak47_BulletSpeed;
            Instantiate(ShootLightPrefab, BulletSpawmPosition, Quaternion.identity); 
            Ak47_Ammo--;
            AmmoText.text = Ak47_Ammo.ToString() + "/" + Ak47_TotalAmmo.ToString();
            Recoil(Ak47_RecoilForce, Ak47_RecoilVelocity);
            ShakeCamera();
        }
        else
        {
            gameObject.GetComponent<AudioSource>().clip = Ak47_EmptyMagSound;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void ShootingMP5Bullet()
    {
        if (MP5_Ammo > 0 && OnReloading == false)
        {
            ShootingStarted = true;
            gameObject.GetComponent<AudioSource>().clip = MP5_ShootingSound;
            gameObject.GetComponent<AudioSource>().Play();
            Vector3 BulletSpawmPosition = gameObject.transform.position;
            BulletSpawmPosition.y += 0.5f;
            GameObject BulletAtual = Instantiate(MP5_BulletPrefab, BulletSpawmPosition, Quaternion.identity);
            BulletAtual.GetComponent<Rigidbody2D>().velocity = PlayerMovment.GetMouseDirection() * MP5_BulletSpeed;
            Instantiate(ShootLightPrefab, BulletSpawmPosition, Quaternion.identity);
            MP5_Ammo--;
            AmmoText.text = MP5_Ammo.ToString() + "/" + MP5_TotalAmmo.ToString();
            Recoil(MP5_RecoilForce, MP5_RecoilVelocity);
            ShakeCamera();
        }
        else
        {
            gameObject.GetComponent<AudioSource>().clip = MP5_EmptyMagSound;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void ShootingDesertEagleBullet()
    {
        if (DesertEagle_Ammo > 0 && OnReloading == false)
        {
            ShootingStarted = true;
            gameObject.GetComponent<AudioSource>().clip = DesertEagle_ShootingSound;
            gameObject.GetComponent<AudioSource>().Play();
            Vector3 BulletSpawmPosition = gameObject.transform.position;
            BulletSpawmPosition.y += 0.5f;
            GameObject BulletAtual = Instantiate(DesertEagle_BulletPrefab, BulletSpawmPosition, Quaternion.identity);
            BulletAtual.GetComponent<Rigidbody2D>().velocity = PlayerMovment.GetMouseDirection() * DesertEagle_BulletSpeed;
            Instantiate(ShootLightPrefab, BulletSpawmPosition, Quaternion.identity);
            DesertEagle_Ammo--;
            AmmoText.text = DesertEagle_Ammo.ToString() + "/" + DesertEagle_TotalAmmo.ToString();
            Recoil(DesertEagle_RecoilForce, DesertEagle_RecoilVelocity);
            ShakeCamera();
        }
        else
        {
            gameObject.GetComponent<AudioSource>().clip = DesertEagle_EmptyMagSound;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }
    public void ShootingM4Bullet()
    {
        if (M4_Ammo > 0 && OnReloading == false)
        {
            ShootingStarted = true;
            gameObject.GetComponent<AudioSource>().clip = M4_ShootingSound;
            gameObject.GetComponent<AudioSource>().Play();
            Vector3 BulletSpawmPosition = gameObject.transform.position;
            BulletSpawmPosition.y += 0.5f;
            GameObject BulletAtual = Instantiate(M4_BulletPrefab, BulletSpawmPosition, Quaternion.identity);
            BulletAtual.GetComponent<Rigidbody2D>().velocity = PlayerMovment.GetMouseDirection() * M4_BulletSpeed;
            Instantiate(ShootLightPrefab, BulletSpawmPosition, Quaternion.identity);
            M4_Ammo--;
            AmmoText.text = M4_Ammo.ToString() + "/" + M4_TotalAmmo.ToString();
            Recoil(M4_RecoilForce, M4_RecoilVelocity);
            ShakeCamera();
        }
        else
        {
            gameObject.GetComponent<AudioSource>().clip = M4_EmptyMagSound;
            gameObject.GetComponent<AudioSource>().Play();
        }
    }


    public void Shoot(InputAction.CallbackContext ctx)
    {
        
        if (ctx.started)
        {
            if (PlayerMovment.InAim == true)
            {
                if (usingWeapon == UsingWeapon.AK_47)
                {
                    if (Ak47_Automatic == true)
                    {
                        InvokeRepeating("ShootingAK47Bullet", 0f, Ak47_DelayShoot);
                    }
                    else
                    {
                        Invoke("ShootingAK7Bullet", 0f);
                    }
                }
                else if (usingWeapon == UsingWeapon.MP5)
                {
                    if (MP5_Automatic == true)
                    {
                        InvokeRepeating("ShootingMP5Bullet", 0f, MP5_DelayShoot);
                    }
                    else
                    {
                        Invoke("ShootingMP5Bullet", 0f);
                    }
                }
                else if (usingWeapon == UsingWeapon.DesertEagle)
                {
                    if (DesertEagle_Automatic == true)
                    {
                        InvokeRepeating("ShootingDesertEagleBullet", 0f, DesertEagle_DelayShoot);
                    }
                    else
                    {
                        Invoke("ShootingDesertEagleBullet", 0f);
                    }
                }
                else if (usingWeapon == UsingWeapon.M4)
                {
                    if (M4_Automatic == true)
                    {
                        InvokeRepeating("ShootingM4Bullet", 0f, M4_DelayShoot);
                    }
                    else
                    {
                        Invoke("ShootingM4Bullet", 0f);
                    }
                }

            }
        }
       
        if(ctx.canceled)
        {
            CancelInvoke("ShootingAK47Bullet");
            CancelInvoke("ShootingMP5Bullet");
            CancelInvoke("ShootingDesertEagleBullet");
            CancelInvoke("ShootingM4Bullet");
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

    public void Ak47Reload()
    {
        int AmmoToReload = Ak47_MagSize - Ak47_Ammo;
        if(AmmoToReload > Ak47_TotalAmmo)
        {
            AmmoToReload = Ak47_TotalAmmo;
        }
        Ak47_Ammo += AmmoToReload;
        Ak47_TotalAmmo -= AmmoToReload;
        AmmoText.text = Ak47_Ammo.ToString() + "/" + Ak47_TotalAmmo.ToString();
        OnReloading = false;
    }
    public void MP5Reload()
    {
        int AmmoToReload = MP5_MagSize - MP5_Ammo;
        if (AmmoToReload > MP5_TotalAmmo)
        {
            AmmoToReload = MP5_TotalAmmo;
        }
        MP5_Ammo += AmmoToReload;
        MP5_TotalAmmo -= AmmoToReload;
        AmmoText.text = MP5_Ammo.ToString() + "/" + MP5_TotalAmmo.ToString();
        OnReloading = false;
    }
    public void DesertEagleReload()
    {
        int AmmoToReload = DesertEagle_MagSize - DesertEagle_Ammo;
        if (AmmoToReload > DesertEagle_TotalAmmo)
        {
            AmmoToReload = DesertEagle_TotalAmmo;
        }
        DesertEagle_Ammo += AmmoToReload;
        DesertEagle_TotalAmmo -= AmmoToReload;
        AmmoText.text = DesertEagle_Ammo.ToString() + "/" + DesertEagle_TotalAmmo.ToString();
        OnReloading = false;
    }
    public void M4Reload()
    {
        int AmmoToReload = M4_MagSize - M4_Ammo;
        if (AmmoToReload > M4_TotalAmmo)
        {
            AmmoToReload = M4_TotalAmmo;
        }
        M4_Ammo += AmmoToReload;
        M4_TotalAmmo -= AmmoToReload;
        AmmoText.text = M4_Ammo.ToString() + "/" + M4_TotalAmmo.ToString();
        OnReloading = false;
    }
    public void ReloadAction(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (usingWeapon == UsingWeapon.AK_47)
            {
                if (Ak47_Ammo != Ak47_MagSize && Ak47_TotalAmmo != 0)
                {
                    OnReloading = true;
                    gameObject.GetComponent<AudioSource>().clip = Ak47_ReloadingSound;
                    gameObject.GetComponent<AudioSource>().Play();
                    Invoke("Ak47Reload", Ak47_ReloadingTime);
                }
            }
            else if (usingWeapon == UsingWeapon.MP5)
            {
                if (MP5_Ammo != MP5_MagSize && MP5_TotalAmmo != 0)
                {
                    OnReloading = true;
                    gameObject.GetComponent<AudioSource>().clip = MP5_ReloadingSound;
                    gameObject.GetComponent<AudioSource>().Play();
                    Invoke("MP5Reload", MP5_ReloadingTime);
                }
            }
            else if (usingWeapon == UsingWeapon.DesertEagle)
            {
                if (DesertEagle_Ammo != DesertEagle_MagSize && DesertEagle_TotalAmmo != 0)
                {
                    OnReloading = true;
                    gameObject.GetComponent<AudioSource>().clip = DesertEagle_ReloadingSound;
                    gameObject.GetComponent<AudioSource>().Play();
                    Invoke("DesertEagleReload", DesertEagle_ReloadingTime);
                }
            }
            else if (usingWeapon == UsingWeapon.M4)
            {
                if (M4_Ammo != M4_MagSize && M4_TotalAmmo != 0)
                {
                    OnReloading = true;
                    gameObject.GetComponent<AudioSource>().clip = M4_ReloadingSound;
                    gameObject.GetComponent<AudioSource>().Play();
                    Invoke("M4Reload", M4_ReloadingTime);
                }
            }
                
            
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        GunSpriteRenderer = Gun.GetComponent<SpriteRenderer>();

        GunBulletUIimageImage = GunBulletUIimage.GetComponent<Image>();

        Ak47_PanelImage = Ak47_Panel.GetComponent<Image>();
        MP5_PanelImage = MP5_Panel.GetComponent<Image>();
        DesertEagle_PanelImage = DesertEagle_Panel.GetComponent<Image>();
        M4_PanelImage = M4_Panel.GetComponent<Image>();

        usingWeapon = UsingWeapon.AK_47;
        UseWeapon1();
        AmmoText.text = Ammo.ToString() + "/" + TotalAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
