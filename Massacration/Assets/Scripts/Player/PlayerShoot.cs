using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] float BulletSpeed;
    [SerializeField] int BulletLife;
    [SerializeField] int TotalAmmo;
    [SerializeField] int Ammo;
    [SerializeField] int MagSize;
    [SerializeField] float ReloadingTime;
    [SerializeField] TMP_Text AmmoText;
    [SerializeField] AudioClip ShootSound;
    [SerializeField] AudioClip OutOfAmmoSound;
    [SerializeField] AudioClip ReloadingSound;
    private bool OnReloading = false;
    public void Shoot(InputAction.CallbackContext ctx)
    {
        
        if (ctx.started)
        {
            if (PlayerMovment.InAim == true)
            {
                if(Ammo > 0 && OnReloading ==false){
                    gameObject.GetComponent<AudioSource>().clip = ShootSound;
                    gameObject.GetComponent<AudioSource>().Play();
                    GameObject BulletAtual = Instantiate(Bullet, transform.position, Quaternion.identity);
                    BulletAtual.GetComponent<Rigidbody2D>().velocity = PlayerMovment.GetMouseDirection() * BulletSpeed;
                    Ammo--;
                    AmmoText.text = Ammo.ToString() + "/" + TotalAmmo.ToString();
                }
                else if(OnReloading == false)
                {
                    gameObject.GetComponent<AudioSource>().clip = OutOfAmmoSound;
                    gameObject.GetComponent<AudioSource>().Play();
                }
            }
        }
        
    }
    public void Reload()
    {
        int AmmoToReload = MagSize - Ammo;
        if(AmmoToReload > TotalAmmo)
        {
            AmmoToReload = TotalAmmo;
        }
        Ammo += AmmoToReload;
        TotalAmmo -= AmmoToReload;
        AmmoText.text = Ammo.ToString() + "/" + TotalAmmo.ToString();
        OnReloading = false;
    }
    public void ReloadAction(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (Ammo != MagSize && TotalAmmo != 0)
            {
                //GetComponent<Animator>().Play("Reloading");
                OnReloading = true;
                gameObject.GetComponent<AudioSource>().clip = ReloadingSound;
                gameObject.GetComponent<AudioSource>().Play();
                Invoke("Reload", ReloadingTime);
            }
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        AmmoText.text = Ammo.ToString() + "/" + TotalAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
