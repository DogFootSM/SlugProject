using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class StageTester : MonoBehaviour
{
    [Header("�� �������� ������")]
    [SerializeField] private List<MapData> mapData = new List<MapData>();
    [SerializeField] private MapController mapController;
    [SerializeField] private Image foreGround;

    [Header("Monster Spawn")]
    [SerializeField] private Transform monsterSpawnPoint;
    [SerializeField] private GameObject monsterPrefab;

    [Header("Monster Safe Zone")]
    [SerializeField] private Transform safeZone;

    [SerializeField] private int viewMonsterCount;
    [SerializeField] private int killMonsterCount;
    [SerializeField] private int totalMonsterCount;

    public int ViewMonsterCount { get { return viewMonsterCount; } }

    public Action bgAction;

    private float killRate;

    //Stage ���� �ӽ� ������
    private MiddleMap curMiddleMap = MiddleMap.One;
    private int curSmallStage = 1;
    private int curWave = 1;
    private int maxWave = 4; // ���� Data Table ���� �޾ƿ� �ʿ� ����
    private int curWaveMonsterCount = 5;

    //���� �ߺз� ���̵� üũ (�ӽ� ������)
    private Difficutly curDifficult = Difficutly.Easy;
    private bool[,] difficultCheck = new bool[(int)MiddleMap.SIZE, (int)Difficutly.SIZE];


    //�׽�Ʈ �ڵ�
    [SerializeField] MonsterModel[] monsters;
    int testIndex = 0;
    //

    private void Start()
    {
        //�׽�Ʈ �ڵ�
        monsters = new MonsterModel[curWaveMonsterCount];


        //��� �ߺз��� Easy ���̵��� True�� ����
        for (int i = 0; i < (int)MiddleMap.SIZE; i++)
        {
            difficultCheck[i, 0] = true;
        }


        //MapController > ���, �ϴ� �̹��� ����
        //mapController.BackGroundSpriteChange(mapData[(int)curMiddleMap].BackGroundSprite);
        //mapController.SkySpriteChange(mapData[(int)curMiddleMap].SkySprite);

        //���� Table���� �޾ƿͼ� ���� �ʿ�
        //totalMonsterCount = curWaveMonsterCount * maxWave;
        totalMonsterCount = curWaveMonsterCount;
        Wave();
    }


    private void Update()
    {
        Debug.Log($"{curMiddleMap} / ���̵� :{curDifficult}");
        Debug.Log($"���� ��������:{curMiddleMap} - {curSmallStage} -{curWave}");
        Debug.Log($"���� ���ǵ� :{monsters[0].MonsterMoveSpeed}");
        //�׽�Ʈ �ڵ�
        if (Input.GetKeyDown(KeyCode.Space))
        {

            monsters[testIndex].MonsterHP = 0;
            Destroy(monsters[testIndex]);
            testIndex++;

            viewMonsterCount--;
            killMonsterCount++;

            //�������� �����
            killRate = ((float)killMonsterCount / totalMonsterCount) * 100f;

        }


        //�������� ����� UI ����
        foreGround.fillAmount = killRate * 0.01f;
        StageClear();
        MonsterSafeZone();



        if (viewMonsterCount < 1)
        {
            curWave++;
            Wave();
        }

    }

    /// <summary>
    /// ���� �������� �̵�
    /// </summary>
    private void NextStage()
    {
        //�� �������� ���� > ex) 1-1 > 1-2
        if (curSmallStage < mapData[(int)curMiddleMap].MaxSmallStage)
        {
            curSmallStage++;
        }
        else
        {

            ChangeDifficult();

            if ((int)curMiddleMap < mapData.Count - 1)
            {
                curMiddleMap++;
            }
            else
            {
                curMiddleMap = MiddleMap.One;
            }

            curSmallStage = 1;

            SetDifficult();

            //���, �ϴ� �̹��� ����
            //mapController.BackGroundSpriteChange(mapData[(int)curMiddleMap].BackGroundSprite);
            //mapController.SkySpriteChange(mapData[(int)curMiddleMap].SkySprite);
        }

        //���� Table���� �޾ƿͼ� ���� �ʿ�
        //totalMonsterCount = curWaveMonsterCount * maxWave;
        totalMonsterCount = curWaveMonsterCount;
        curWave = 0;

        bgAction?.Invoke();
    }

    /// <summary>
    /// �������� Ŭ����
    /// </summary>
    private void StageClear()
    {
        if (killRate >= 100f)
        {
            killMonsterCount = 0;
            killRate = 0f;
            NextStage();
        }
    }


    /// <summary>
    /// ���� �ߺз� �ʿ��� ���̵��� True �� ���� ã�Ƽ� ���� ���̵��� �Ҵ�
    /// </summary>
    public void SetDifficult()
    {
        //���� �ߺз��� Ŭ������ ���̵� Ȯ��
        //true �� �� ���̵��� ������ �ش� ���̵��� ����

        for (int i = 0; i < (int)Difficutly.SIZE; i++)
        {
            if (difficultCheck[(int)curMiddleMap, i])
            {
                curDifficult = (Difficutly)i;
            }
        }
    }

    /// <summary>
    /// �ش� �ߺз� ���� ���̵� �ر�
    /// </summary>
    public void ChangeDifficult()
    {
        //���� �ߺз��� ���̵� Ȯ��
        int curDifIndex = (int)curDifficult + 1;
        difficultCheck[(int)curMiddleMap, curDifIndex] = true;

    }

    /// <summary>
    /// Stage Wave ���� 
    /// </summary>
    public void Wave()
    {

        if (curWave <= maxWave)
        {
            viewMonsterCount = curWaveMonsterCount;
            CreateMonster();
        }
    }

    /// <summary>
    /// ���� ���� ���
    /// </summary>
    public void CreateMonster()
    {
        testIndex = 0;

        //���� Ŭ���� �޾ƿͼ� Instantiate ����
        for (int i = 0; i < curWaveMonsterCount; i++)
        {
            //Offset ���� ��ȹ �ǵ��� ���� ����
            float xPos = UnityEngine.Random.Range(11f, 13f);
            float yPos = UnityEngine.Random.Range(2.5f, -3f);
            Vector3 offset = new Vector3(xPos, yPos, 0);

            GameObject monsterInstance = Instantiate(monsterPrefab, monsterSpawnPoint.position + offset, monsterSpawnPoint.rotation);

            //���� ���� �ÿ��� Collider�� ��
            Collider2D monsterCollider = monsterInstance.GetComponent<Collider2D>();
            monsterCollider.enabled = false;

            monsters[i] = monsterInstance.GetComponent<MonsterModel>();
            AdjustmentStats(i);
        }

    }

    /// <summary>
    /// ���� Collider On
    /// </summary>
    public void MonsterSafeZone()
    {

        for (int i = 0; i < monsters.Length; i++)
        {
            //���Ͱ� Null�� �ƴ� ���¿��� SafeZone�� ������ ��� Collider Ȱ��ȭ
            if (monsters[i] != null)
            {
                if (monsters[i].transform.position.x < safeZone.transform.position.x)
                {
                    Collider2D collider = monsters[i].GetComponent<Collider2D>();
                    collider.enabled = true;
                }
            }
        }
    }

    /// <summary>
    /// ���̵� �� ���� ����
    /// </summary>
    /// <param name="index">���� ���� �� �޾ƿ� �ε���</param>
    public void AdjustmentStats(int index)
    {
        switch (curDifficult)
        {
            case Difficutly.Easy:
                monsters[index].MonsterMoveSpeed = 3.5f;
                break;

            case Difficutly.Normal:
                monsters[index].MonsterMoveSpeed = 5.5f;
                break;

            case Difficutly.Hard:
                monsters[index].MonsterMoveSpeed = 7.5f;
                break;
        }

    }


    /// <summary>
    /// ��׶��� �̹��� ��ġ ���� �׼�
    /// </summary>
    /// <param name="action"></param>
    public void BackGroundResetAction(Action action)
    {
        bgAction = action;
    }



}
