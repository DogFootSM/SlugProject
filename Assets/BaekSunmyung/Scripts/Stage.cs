using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
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

    [SerializeField] private int fieldWaveMonsterCount;
    [SerializeField] private int killMonsterCount;
    [SerializeField] private int ThirdClassMonsterCount;

    [SerializeField] private int waveCount;

    //�ӽ� ����
    [SerializeField] private TextMeshProUGUI bossText;

    public int FieldWaveMonsterCount { get { return fieldWaveMonsterCount; } }

    private StageCSVTest csvPaser;
    private int paserIndex = -1;

    public Action bgAction;

    private float killRate;

    //Stage ���� �ӽ� ������
    private MiddleMap curSecondClass = MiddleMap.First;
    private int curThirdClass;
    private int curWave;
    private int maxWave; // ���� Data Table ���� �޾ƿ� �ʿ� ����
    private int curWaveMonsterCount;    //Data Table���� �޾ƿ;� ��

    private bool isBoss;

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

    private bool isWave;

    private void Start()
    {
        csvPaser = GetComponent<StageCSVTest>();
  
        //��� �ߺз��� Easy ���̵��� True�� ����
        for (int i = 0; i < (int)MiddleMap.SIZE; i++)
        {
            difficultCheck[i, 0] = true;
        }


        //MapController > ���, �ϴ� �̹��� ����
        //mapController.BackGroundSpriteChange(mapData[(int)curMiddleMap].BackGroundSprite);
        //mapController.SkySpriteChange(mapData[(int)curMiddleMap].SkySprite);

        monsters = new MonsterModel[5];
 
    }


    private void Update()
    {
        if (!csvPaser.isComplet)
        {
            return;
        }



        //�׽�Ʈ �ڵ�
        //���� �Ŀ� ���Ͱ� Destroy ���� ��� �Ǵ��ϴ� ������� �ۼ�
        if (Input.GetKeyDown(KeyCode.Space))
        {
            monsters[testIndex].MonsterHP = 0;
        }

        

        if (monsters[testIndex] != null)
        {
            Debug.Log($"���� �ı� ���� :{monsters[testIndex].IsDestroyed()}");
            if (monsters[testIndex].IsDestroyed())
            {
                Debug.Log("�ı��� ����!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                testIndex++;
                fieldWaveMonsterCount--;
            }

            //    if (monsters[testIndex].IsDestroyed())
            //    {
            //        Destroy(monsters[testIndex]);
            //        fieldWaveMonsterCount--;
            //        killMonsterCount++;
            //        testIndex++;
            //        //�������� �����

            //    }
        }

        if (curWaveMonsterCount <= createLimitCount && createCo != null)
        {
            StopCoroutine(createCo);
            createCo = null;
        }

        
        if (!isWave &&fieldWaveMonsterCount < 1)
        {
            //wave ���͸� �� ������� paserIndex ���� 
            Wave();
        }


        //�������� ����� UI ����
        killRate = ((float)killMonsterCount / ThirdClassMonsterCount) * 100f;
        foreGround.fillAmount = killRate * 0.01f;
        StageClear();
        MonsterSafeZone();

    }

     
    /// <summary>
    /// ���� �������� �̵�
    /// </summary>
    private void NextStage()
    { 
        SetDifficult();

        //���, �ϴ� �̹��� ����
        //mapController.BackGroundSpriteChange(mapData[(int)curMiddleMap].BackGroundSprite);
        //mapController.SkySpriteChange(mapData[(int)curMiddleMap].SkySprite);
         
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
        
        isWave = true;

        paserIndex++;

        curThirdClass = csvPaser.State[paserIndex].Stage_thirdClass; 
        Debug.Log($"{csvPaser.State[paserIndex].Stage_secondClass}-{curThirdClass}-{csvPaser.State[paserIndex].Stage_wave}");

        //ThirdClass���� ��ȯ�Ǵ� �� ����
        if (paserIndex % waveCount == 0)
        {
            for (int i = paserIndex; i < paserIndex + waveCount; i++)
            {
                ThirdClassMonsterCount += csvPaser.State[i].Stage_monsterNum;
            }
        }

        //���� ���̺� ���� ��
        curWaveMonsterCount = csvPaser.State[paserIndex].Stage_monsterNum;
        CreateMonster();




        //if (curWave <= maxWave)
        //������ Wave�� �� ���� ���Ͱ� ������
        //if (curWave < maxWave)
        //{
        //    bossText.gameObject.SetActive(false);
        //    //curWaveMonsterCount �� Data Table ���� �޾ƿ� ���� ���� 
        //    createLimitCount = 0;
        //    CreateMonster();
        //}
        ////������ ���� �ۼ�
        //else
        //{
        //    //curWaveMonsterCount �� Data Table ���� �޾ƿ� ���� ����
        //    bossText.gameObject.SetActive(true); 
        //    createLimitCount = 0;
        //    CreateMonster();
        //}


        //IF curWave < maxWave
        //viewMonsterCount = curWaveMonsterCount;
        //ELSE IF curWave >= maxWave ���� ���̺갡 ������ ���̺��� ����
        //�������� ����
        //���� ���Ͱ� ���������� isMeenBoss


    }

    public void CreateBoss()
    {
        //IF ���� ���� ���¿��� �׾��°�
        //isMeenBoss && isDeath
        //���� ������ ����
        //ELSE

        curWaveMonsterCount = 1;

    }



    /// <summary>
    /// ���� ���� ���
    /// </summary>
    public void CreateMonster()
    { 
        if (createCo == null)
        {
            createCo = StartCoroutine(CreateMonsterCo());
        }

    }

    private IEnumerator CreateMonsterCo()
    {

        WaitForSeconds createWait = new WaitForSeconds(createTimer);
        WaitForSeconds cycleWait = new WaitForSeconds(cycleTimer);
        monsters = new MonsterModel[curWaveMonsterCount];
        testIndex = 0;

        //�ð������� ����� �� �غ� ���·� ���� ���� �ʿ�
        yield return cycleWait;
        createLimitCount = 0;
        fieldWaveMonsterCount = curWaveMonsterCount;

        while (curWaveMonsterCount > createLimitCount)
        {
            float xPos = UnityEngine.Random.Range(11f, 13f);
            float yPos = UnityEngine.Random.Range(2.5f, -3f);
            Vector3 offset = new Vector3(xPos, yPos, 0);

            GameObject monsterInstance = Instantiate(monsterPrefab, monsterSpawnPoint.position + offset, monsterSpawnPoint.rotation);
            Collider2D monsterCollider = monsterInstance.GetComponent<Collider2D>();
            monsterCollider.enabled = false;

            monsters[createLimitCount] = monsterInstance.GetComponent<MonsterModel>();
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
        switch (csvPaser.State[paserIndex].Stage_firstClass)
        {
            case "����":
                curDifficult = Difficutly.Easy;
                break;

            case "����":
                curDifficult = Difficutly.Normal;
                break;

            case "�����":
                curDifficult = Difficutly.Hard;
                break;

            case "����1":
                curDifficult = Difficutly.Hell1;
                break;
            case "����2":
                curDifficult = Difficutly.Hell2;
                break;
            case "����3":
                curDifficult = Difficutly.Hell3;
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
