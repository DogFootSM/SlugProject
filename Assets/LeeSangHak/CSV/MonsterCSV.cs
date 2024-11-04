using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public struct MonsterData
{
    public enum Enemy_type 
    {
        �𵧱�, �Ϲݸ���1, �Ϲݸ���2, �Ϲݸ���3, �Ϲݸ���4, �Ϲݸ���5, �Ϲݸ���6,
        �Ϲݸ���7, �Ϲݸ���8, �Ϲݸ���9, �Ϲݸ���10, ��������1, ��������2, ��������3, ��������4,
        ��������5, ��������6, ��������7, ��������8, ��������9, ��������10
    }

    public int Enemy_iD, Enemy_atk, Enemy_hp;
    public float Enemy_atkSpd;
    public string eName, Enemy_name;

    public Enemy_type enemy_Type;

}

public class MonsterCSV : MonoBehaviour
{
    const string monsterPath = "https://docs.google.com/spreadsheets/d/1yrhRkrB5UQH2JDYT2_vz9RW6yRzVaYv2/export?gid=206694631&format=csv";
    [SerializeField] List<MonsterData> Monster;
    public static MonsterCSV Instance;
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
        UnityWebRequest request = UnityWebRequest.Get(monsterPath); // ��ũ�� ���ؼ� ������Ʈ�� �ٿ�ε� ��û
        yield return request.SendWebRequest();                  // ��ũ�� �����ϰ� �Ϸ�� ������ ���

        // �Ϸ�� ��Ȳ
        string receiveText = request.downloadHandler.text;      // �ٿ�ε� �Ϸ��� ������ �ؽ�Ʈ�� �б�

        Debug.Log(receiveText);

        string[] lines = receiveText.Split('\n');
        for (int y = 5; y < lines.Length; y++)
        {
            MonsterData monsterData = new MonsterData();

            string[] values = lines[y].Split(',', '\t');

            monsterData.eName = values[0];
            monsterData.Enemy_iD = int.Parse(values[1]);
            monsterData.Enemy_name = values[2];
            Enum.TryParse(values[3], out monsterData.enemy_Type);
            monsterData.Enemy_hp = int.Parse(values[4]);
            monsterData.Enemy_atk = int.Parse(values[5]);
            monsterData.Enemy_atkSpd = float.Parse(values[6]);

            Monster.Add(monsterData);
        }

        downloadCheck = true;
    }
}