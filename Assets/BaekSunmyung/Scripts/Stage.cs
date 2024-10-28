using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stage : MonoBehaviour
{
    [Header("�� �������� ������")]
    [SerializeField] private List<MapData> mapData = new List<MapData>();

    [SerializeField] private MapController mapController;

    [SerializeField] private int viewMonsterCount;
    [SerializeField] private int killMonsterCount;
    [SerializeField] private int totalMonsterCount;


    public Action bgAction;

    private float killRate;

    //�ӽ� ������
    private MiddleMap curMiddleMap = MiddleMap.One;
    private int curSmallStage = 1;

    private int curWave = 1;
    private int maxWave = 4; // ���� Data Table ���� �޾ƿ� �ʿ� ����

    //���� �ߺз� ���̵� üũ
    private Difficutly curDifficult = Difficutly.Easy;
    private bool[,] difficultCheck = new bool[(int)MiddleMap.SIZE, (int)Difficutly.SIZE];

    private void Start()
    {
        //MapController > ���, �ϴ� �̹��� ����
        //mapController.BackGroundSpriteChange(mapData[(int)curMiddleMap].BackGroundSprite);
        //mapController.SkySpriteChange(mapData[(int)curMiddleMap].SkySprite);
    }


    private void Update()
    {
        Debug.Log($"���� ��������:{curMiddleMap} - {curSmallStage}");

        //�׽�Ʈ �ڵ�
        if (Input.GetKeyDown(KeyCode.Space))
        {

            viewMonsterCount--;
            killMonsterCount++;

            //�������� �����
            killRate = ((float)killMonsterCount / totalMonsterCount) * 100;

        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            viewMonsterCount++;
        }

        StageClear();
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
            curMiddleMap++;
            curSmallStage = 1;

            //���, �ϴ� �̹��� ����
            //mapController.BackGroundSpriteChange(mapData[(int)curMiddleMap].BackGroundSprite);
            //mapController.SkySpriteChange(mapData[(int)curMiddleMap].SkySprite);
        }

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
            viewMonsterCount = 1;

            NextStage();
        }
    }
    
    public void SetDifficult()
    {
        //���� �ߺз��� Ŭ������ ���̵� Ȯ��
        //true �� �� ���̵��� ������ �ش� ���̵��� ����
        
        //for(int i = 0; i < (int)Difficutly.SIZE; i++)
        //{
        //    if (difficultCheck[(int)curMiddleMap, i])
        //    {
        //        curDifficult = (Difficutly)i;
        //    }
        //}
         
    }

    public void ChangeDifficult()
    {
        //���� �ߺз��� ���̵� Ȯ��
        //IF �ߺз��� smallStage�� �ִ� stage���� Ŀ���°�?
        //True : difficultCheck[(int)curMiddleMap, (int)curDifficult++] = true;
        //False : difficultCheck[(int)curMiddleMap, (int)curDifficult++] = false;
    }

    public void StartWave()
    {
        //IF ���� ���̺갡 maxWave ���ΰ�
        //���� ���̺� Ȯ��
        //���� ���̺꿡 �´� ���� ���� 
        //CreateMonster()

        //IF killRate >= 100f
        //Stage Clear()
        //curWave ++;

        //IF ������ wave�� maxWave ���� ū��?
        //curSmallStage ����
        //IF curSmallStage�� �ߺз��� maxStage�� �����ߴ°�?
        //middleStage ����
    }

    public void CreateMonster()
    {
        //���� Ŭ���� �޾ƿͼ� Instantiate ����
    }


    public void BackGroundResetAction(Action action)
    {
        bgAction = action;
    }

}
