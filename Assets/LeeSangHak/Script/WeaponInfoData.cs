using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfoData : MonoBehaviour
{
    public static WeaponInfoData Instance;


    [Header("���� ������Ʈ")]
    [SerializeField] Weapon Heavy;
    [SerializeField] Weapon Flame;
    [SerializeField] Weapon Roket;
    [SerializeField] Weapon Shotgun;

    [Space(15f)]
    [SerializeField] SlugController Metal;
    [SerializeField] SlugController Drill;
    [SerializeField] SlugController Heli;
    [SerializeField] SlugController Jet;
    
    [Space(15f)]
    [SerializeField] SlugController Fio;
    [SerializeField] SlugController Eri;
    [SerializeField] SlugController Marco;
    [SerializeField] SlugController Tarma;

    [Header("������Ʈ ��� ���� ����")]
    public bool useHeavy, useFlame, useRoket, useShotgun;
    [Header("������Ʈ ��� ���� ����")]
    public bool useMetal, useDrill, useHeli, useJet;
    [Header("������Ʈ ��� ���� ����")]
    public bool useFio, useEri, useMarco, useTarma;

    [Header("����")]
    public int Heavy_Level = 0;
    public int Shotgun_Level = 500;
    public int Flame_Level = 1000;
    public int Roket_Level = 1500;
    

    [Space(15f)]
    public int Metal_Level = 2000;
    public int Drill_Level = 2500;
    public int Heli_Level = 3000;
    public int Jet_Level = 3500;

    [Space(15f)]
    public int Marco_Level = 0;
    public int Eri_Level = 0500;
    public int Tarma_Level = 1000;
    public int Fio_Level = 1500;

    private float Heavy_Num = 0;
    private float Flame_Num = 0;
    private float Roket_Num = 0;
    private float Shotgun_Num = 0;

    [Header("���� ������ �ջ�")]
    public float weaponDamage;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Heavy.damage = WeaponCSV.Instance.Weapon[Heavy_Level].Weapon_per;
        Flame.damage = WeaponCSV.Instance.Weapon[Flame_Level].Weapon_per;
        Roket.damage = WeaponCSV.Instance.Weapon[Roket_Level].Weapon_per;
        Shotgun.damage = WeaponCSV.Instance.Weapon[Shotgun_Level].Weapon_per;
    }

    private void Update()
    {
        WeaponDamage();
    }

    private void WeaponDamage()
    {
        if (useHeavy == true)
        {
            Heavy_Num = WeaponCSV.Instance.Weapon[Heavy_Level].Weapon_per;
        }

        if (useFlame == true)
        {
            Flame_Num = WeaponCSV.Instance.Weapon[Flame_Level].Weapon_per;
        }

        if (useRoket == true)
        {
            Roket_Num = WeaponCSV.Instance.Weapon[Roket_Level].Weapon_per;
        }

        if (useShotgun == true)
        {
            Shotgun_Num = WeaponCSV.Instance.Weapon[Shotgun_Level].Weapon_per;
        }

        weaponDamage = Heavy_Num + Flame_Num + Roket_Num + Shotgun_Num;
    }

    private void SlugDamage()
    {

    }
}