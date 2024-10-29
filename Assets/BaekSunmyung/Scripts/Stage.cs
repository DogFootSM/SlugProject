using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
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

    [Tooltip("Monster Wave Cycle")]
    [SerializeField] private float cycleTimer;

    [Tooltip("Monster Create Delay")]
    [SerializeField] private float createTimer;

    [Header("Monster Safe Zone")]
    [SerializeField] private Transform safeZone;

    [SerializeField] private int viewMonsterCount;
    [SerializeField] private int killMonsterCount;
    [SerializeField] private int totalMonsterCount;

    //�ӽ� ����
    [SerializeField] private TextMeshProUGUI bossText;

    public int ViewMonsterCount { get { return viewMonsterCount; } }

    public Action bgAction;

    private float killRate;

    //Stage ���� �ӽ� ������
    private MiddleMap curMiddleMap = MiddleMap.First;
    private int curSmallStage = 1;
    private int curWave = 0;
    private int maxWave = 3; // ���� Data Table ���� �޾ƿ� �ʿ� ����
    private int curWaveMonsterCount = 5;    //Data Table���� �޾ƿ;� ��

    //���� �ߺз� ���̵� üũ (�ӽ� ������)
    private Difficutly curDifficult = Difficutly.Easy;
    private bool[,] difficultCheck = new bool[(int)MiddleMap.SIZE, (int)Difficutly.SIZE];


    //���� ���� �ڷ�ƾ ī��Ʈ ����
    private int createLimitCount = 0;

    //���� ���� �ڷ�ƾ
    private Coroutine createCo;


    //�׽�Ʈ �ڵ�
    [SerializeField] MonsterModel[] monsters;
    private int testIndex = 0;


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
        //ù ��° Wave ����
        totalMonsterCount = curWaveMonsterCount * maxWave;

    }


    private void Update()
    {
        Debug.Log($"{curMiddleMap} / ���̵� :{curDifficult}");
        Debug.Log($"���� ��������:{curMiddleMap} - {curSmallStage} -{curWave}");
         
        //�׽�Ʈ �ڵ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            monsters[testIndex].MonsterHP = 0;
        }

        if (monsters[testIndex] != null)
        {
            if (monsters[testIndex].MonsterHP < 1)
            {
                Destroy(monsters[testIndex]);
                viewMonsterCount--;
                killMonsterCount++;
                testIndex++;
                //�������� �����
                killRate = ((float)killMonsterCount / totalMonsterCount) * 100f;
            }
        }
 

        //�������� ����� UI ����
        foreGround.fillAmount = killRate * 0.01f;
        StageClear();
        MonsterSafeZone();


        if (curWaveMonsterCount <= createLimitCount && createCo != null)
        {
            Debug.Log("���� �ڷ�ƾ ����");
            StopCoroutine(createCo);
            createCo = null;
        }

        if (viewMonsterCount < 1)
        {
            testIndex = 0; 
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

            //�ӽ� ���� (int)curMiddleMap < (int)MiddleMap.SIZE;
            if ((int)curMiddleMap < mapData.Count - 1)
            {
                curMiddleMap++;
            }
            else
            {
                //��� �ߺз� Ŭ���� �� ù �ߺз��� ����
                curMiddleMap = MiddleMap.First;
            }

            curSmallStage = 1;

            SetDifficult();

            //���, �ϴ� �̹��� ����
            //mapController.BackGroundSpriteChange(mapData[(int)curMiddleMap].BackGroundSprite);
            //mapController.SkySpriteChange(mapData[(int)curMiddleMap].SkySprite);
        }

        //���� Table���� �޾ƿͼ� ���� �ʿ�
        totalMonsterCount = curWaveMonsterCount * maxWave;
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
    /// Stage Wave ���� 
    /// </summary>
    public void Wave()
    {
        //if (curWave <= maxWave)
        //������ Wave�� �� ���� ���Ͱ� ������
        if (curWave < maxWave)
        {
            bossText.gameObject.SetActive(false);
            //curWaveMonsterCount �� Data Table ���� �޾ƿ� ���� ���� 
            createLimitCount = 0;
            CreateMonster();
        }
        //������ ���� �ۼ�
        else
        {
            //curWaveMonsterCount �� Data Table ���� �޾ƿ� ���� ����
            bossText.gameObject.SetActive(true); 
            createLimitCount = 0;
            CreateMonster();
        }


        //IF curWave < maxWave
        //viewMonsterCount = curWaveMonsterCount;
        //ELSE IF curWave >= maxWave ���� ���̺갡 ������ ���̺��� ����
        //�������� ����
        //���� ���Ͱ� ���������� isMeenBoss


    }

    public void BossAndDeath()
    {

        //IF ���� ���� ���¿��� �׾��°�
        //isMeenBoss && isDeath
        //���� ������ ����
        //ELSE
         
    }


    /// <summary>
    /// ���� ���� ���
    /// </summary>
    public void CreateMonster()
    {
        if(createCo == null)
        {
            createCo = StartCoroutine(CreateMonsterCo());
        } 
        
    }

    private IEnumerator CreateMonsterCo()
    {
         
        WaitForSeconds createWait = new WaitForSeconds(createTimer);
        WaitForSeconds cycleWait = new WaitForSeconds(cycleTimer);

        //�ð������� ����� �� �غ� ���·� ���� ���� �ʿ�
        yield return cycleWait;
        curWave++;
        viewMonsterCount = curWaveMonsterCount;

        while (curWaveMonsterCount > createLimitCount)
        {
            Debug.Log("���� ����");
            float xPos = UnityEngine.Random.Range(11f, 13f);
            float yPos = UnityEngine.Random.Range(2.5f, -3f);
            Vector3 offset = new Vector3(xPos, yPos, 0);

            GameObject monsterInstance = Instantiate(monsterPrefab, monsterSpawnPoint.position + offset, monsterSpawnPoint.rotation);
            Collider2D monsterCollider = monsterInstance.GetComponent<Collider2D>();
            monsterCollider.enabled = false;

            monsters[createLimitCount] = monsterInstance.GetComponent<MonsterModel>();
            AdjustmentStats(createLimitCount);
            createLimitCount++;
            yield return createWait;
        }


        yield break;
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
    /// ��׶��� �̹��� ��ġ ���� �׼�
    /// </summary>
    /// <param name="action"></param>
    public void BackGroundResetAction(Action action)
    {
        bgAction = action;
    }



}
