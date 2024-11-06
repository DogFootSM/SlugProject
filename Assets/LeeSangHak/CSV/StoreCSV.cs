using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static PlayerDataModel;
using static StoreData;

[System.Serializable]
public struct StoreData
{
    public enum StatusStore_category { Atk, TouchAtk, AskSpd, Hp, HpGen }

    public int StatusStore_iD, StatusStore_level, StatusStore_satatusUnit, StatusStore_priceGoldUnit;
    public float StatusStore_satatusNum, StatusStore_priceGoldNum;
    public string eName;
    public StatusStore_category statusStore_Category;
}

public class StoreCSV : MonoBehaviour
{
    const string storepath = "https://docs.google.com/spreadsheets/d/1yrhRkrB5UQH2JDYT2_vz9RW6yRzVaYv2/export?gid=654683464&format=csv";
    public List<StoreData> Store;
    public static StoreCSV Instance;
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
        UnityWebRequest request = UnityWebRequest.Get(storepath); // ��ũ�� ���ؼ� ������Ʈ�� �ٿ�ε� ��û
        yield return request.SendWebRequest();                  // ��ũ�� �����ϰ� �Ϸ�� ������ ���

        // �Ϸ�� ��Ȳ
        string receiveText = request.downloadHandler.text;      // �ٿ�ε� �Ϸ��� ������ �ؽ�Ʈ�� �б�

        Debug.Log(receiveText);

        string[] lines = receiveText.Split('\n');
        for (int y = 5; y < lines.Length; y++)
        {
            StoreData storeData = new StoreData();

            string[] values = lines[y].Split(',', '\t');

            storeData.eName = values[0];
            storeData.StatusStore_iD = int.Parse(values[1]);
            Enum.TryParse(values[2], out storeData.statusStore_Category);
            storeData.StatusStore_level = int.Parse(values[3]);
            storeData.StatusStore_satatusNum = float.Parse(values[4]);
            storeData.StatusStore_satatusUnit = int.Parse(values[5]);
            storeData.StatusStore_priceGoldNum = float.Parse(values[6]);
            storeData.StatusStore_priceGoldUnit = int.Parse(values[7]);

            Store.Add(storeData);
        }

        downloadCheck = true;
    }
}
