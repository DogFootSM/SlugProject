using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public struct BulletData
{
    public int Bullet_iD, Bullet_level, Bullet_unit;
    public float Bullet_num;
    public string eName;
}

public class BulletCSV : MonoBehaviour
{
    const string bulletPath = "https://docs.google.com/spreadsheets/d/1yrhRkrB5UQH2JDYT2_vz9RW6yRzVaYv2/export?gid=1745937171&format=csv";
    public List<BulletData> Bullet;
    public static BulletCSV Instance;
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
        UnityWebRequest request = UnityWebRequest.Get(bulletPath); // ��ũ�� ���ؼ� ������Ʈ�� �ٿ�ε� ��û
        yield return request.SendWebRequest();                  // ��ũ�� �����ϰ� �Ϸ�� ������ ���

        // �Ϸ�� ��Ȳ
        string receiveText = request.downloadHandler.text;      // �ٿ�ε� �Ϸ��� ������ �ؽ�Ʈ�� �б�

        Debug.Log(receiveText);

        string[] lines = receiveText.Split('\n');
        for (int y = 5; y < lines.Length; y++)
        {
            BulletData bulletData = new BulletData();

            string[] values = lines[y].Split(',', '\t');

            bulletData.eName = values[0];
            bulletData.Bullet_iD = int.Parse(values[1]);
            bulletData.Bullet_level = int.Parse(values[2]);
            bulletData.Bullet_num = float.Parse(values[3]);
            bulletData.Bullet_unit = int.Parse(values[4]);

            Bullet.Add(bulletData);
        }

        downloadCheck = true;
    }
}
