using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/Gun")]
public class GUNSO : ScriptableObject
{
    [Header("Data")]
    [SerializeField] public string WeaponName;
    [SerializeField] public bool Automatic;
    [SerializeField] public int TotalAmmo;
    [SerializeField] public int Ammo;
    [SerializeField] public int MagSize;
    [SerializeField] public GameObject BulletPrefab;
    [SerializeField] public float BulletSpeed;
    [SerializeField] public float DelayShoot;
    [SerializeField] public float RecoilForce;
    [SerializeField] public float RecoilVelocity;
    [SerializeField] public int Damage;
    [SerializeField] public int BulletHP;
    [SerializeField] public Sprite GunSprite;
    [SerializeField] public float ReloadingTime;
    [SerializeField] public AudioClip ShootingSound;
    [SerializeField] public AudioClip ReloadingSound;
    [SerializeField] public AudioClip EmptyMagSound;
    [SerializeField] public Sprite Image_Panel;
    [SerializeField] public GameObject CaseCartridge;
    [SerializeField] public Sprite GunSpriteMirror;
    [SerializeField] public GameObject ShootLightEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
