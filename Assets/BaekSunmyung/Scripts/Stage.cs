using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class Stage : MonoBehaviour
{
    [Header("�� �������� ������")]
    [SerializeField] private List<MapData> mapData = new List<MapData>();
    [SerializeField] private MapController mapController;
    [SerializeField] private Image foreGround;

    [Header("Monster Spawn")]
    [SerializeField] private Transform monsterSpawnPoint;
    [SerializeField] private GameObject monsterPrefab;

    [SerializeField] private int viewMonsterCount;
    [SerializeField] private int killMonsterCount;
    [SerializeField] private int totalMonsterCount;

    public int ViewMonsterCount { get { return viewMonsterCount; } }

    public Action bgAction;

    private float killRate;

    //�ӽ� ������
    private MiddleMap curMiddleMap = MiddleMap.One;
    private int curSmallStage = 1;
    private int curWave = 1;
    private int maxWave = 4; // ���� Data Table ���� �޾ƿ� �ʿ� ����
    private int curWaveMonsterCount = 5;

    //���� �ߺз� ���̵� üũ
    private Difficutly curDifficult = Difficutly.Easy;
    private bool[,] difficultCheck = new bool[(int)MiddleMap.SIZE, (int)Difficutly.SIZE];

    private void Start()
    {
        for (int i = 0; i < (int)MiddleMap.SIZE; i++)
        {
            //��
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

        //�׽�Ʈ �ڵ�
        if (Input.GetKeyDown(KeyCode.Space))
        {

            viewMonsterCount--;
            killMonsterCount++;

            //�������� �����
            killRate = ((float)killMonsterCount / totalMonsterCount) * 100f;

        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            viewMonsterCount++;
        }

        //�������� ����� UI ����
        foreGround.fillAmount = killRate * 0.01f;
        StageClear();

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
            
            if((int)curMiddleMap < mapData.Count-1)
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

    public void Wave()
    {

        if (curWave <= maxWave)
        {
            viewMonsterCount = curWaveMonsterCount;
            CreateMonster();
        }
    }


    public void CreateMonster()
    {
        //���� Ŭ���� �޾ƿͼ� Instantiate ����
        for (int i = 0; i < curWaveMonsterCount; i++)
        {

            //Offset ���� ��ȹ �ǵ��� ���� ����
            float xPos = UnityEngine.Random.Range(9.5f, 11f);
            float yPos = UnityEngine.Random.Range(2.5f, -3f);
            Vector3 offset = new Vector3(xPos, yPos, 0);

            GameObject monsterInstance = Instantiate(monsterPrefab, monsterSpawnPoint.position + offset, monsterSpawnPoint.rotation);
        }

    }


    public void BackGroundResetAction(Action action)
    {
        bgAction = action;
    }

}
