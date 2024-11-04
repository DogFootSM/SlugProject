using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Networking;


[System.Serializable]
public struct WeaponData
{
    public enum Weapon_type { HeavyMachinegun, Shotgun, FlameShot, RocketRuncher }

    public int Weapon_iD, Weapon_level, Weapon_priceGoldUnit, Weapon_priceDiaNum, Weapon_diaNum;
    public float Weapon_per, Weapon_priceGoldNum, Weapon_diaPer;
    public string eName;

    public Weapon_type weapon_Type;
}

public class WeaponCSV : MonoBehaviour
{
    const string weaponPath = "https://docs.google.com/spreadsheets/d/1yrhRkrB5UQH2JDYT2_vz9RW6yRzVaYv2/export?gid=2042254668&format=csv";
    public List<WeaponData> Weapon;
    public static WeaponCSV Instance;
    public bool downloadCheck;

    private void Start()
    {
        downloadCheck = false;
    }

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

        StartCoroutine(DownloadRoutine());
    }

    IEnumerator DownloadRoutine()
    {
        UnityWebRequest request = UnityWebRequest.Get(weaponPath); // ��ũ�� ���ؼ� ������Ʈ�� �ٿ�ε� ��û
        yield return request.SendWebRequest();                  // ��ũ�� �����ϰ� �Ϸ�� ������ ���

        // �Ϸ�� ��Ȳ
        string receiveText = request.downloadHandler.text;      // �ٿ�ε� �Ϸ��� ������ �ؽ�Ʈ�� �б�

        Debug.Log(receiveText);

        string[] lines = receiveText.Split('\n');
        for (int y = 5; y < lines.Length; y++)
        {
            WeaponData weaponData = new WeaponData();

            string[] values = lines[y].Split(',', '\t');

            weaponData.eName = values[0];
            weaponData.Weapon_iD = int.Parse(values[1]);
            Enum.TryParse(values[2], out weaponData.weapon_Type);
            weaponData.Weapon_level = int.Parse(values[3]);
            weaponData.Weapon_per = float.Parse(values[4]);
            weaponData.Weapon_priceGoldNum = float.Parse(values[5]);
            weaponData.Weapon_priceGoldUnit = int.Parse(values[6]);
            weaponData.Weapon_priceDiaNum = int.Parse(values[7]);
            weaponData.Weapon_diaPer = float.Parse(values[8]);
            weaponData.Weapon_diaNum = int.Parse(values[9]);

            Weapon.Add(weaponData);
        }

        downloadCheck = true;
    }
}