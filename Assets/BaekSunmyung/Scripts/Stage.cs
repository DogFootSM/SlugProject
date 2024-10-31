using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
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
    [SerializeField] private GameObject bossObject;
    [SerializeField] private Button bossChallengeBtn;

    public int FieldWaveMonsterCount { get { return fieldWaveMonsterCount; } }

    //��� �̹��� ����
    public Action bgAction;

    //Data Table CSV ����
    private StageCSV csvParser;

    //Data Table Index ����
    private int parserIndex = 0;

    //���� ������
    private float killRate;

    //Stage ���� �ӽ� ������
    private MiddleMap curSecondClass = MiddleMap.First;
    private int curThirdClass;
    private int curWaveMonsterCount;    //Data Table���� �޾ƿ;� ��

    //Wave ���� ����
    private bool isWave;

    //���� �������� ���� ����
    private bool isBoss;

    private bool isStageClear;

    //���� �������� Ŭ���� ����
    private bool isBossClear;

    private bool isLoop;

    //���� �ߺз� ���̵� üũ (�ӽ� ������)
    private Difficutly curDifficult = Difficutly.Easy;

    //���� ���� �ڷ�ƾ ī��Ʈ ����
    private int createLimitCount = 0;

    //���� ���� �ڷ�ƾ
    private Coroutine createCo;

    //������ ���� ���� �迭
    [SerializeField] MonsterModel[] monsters;

    //Player ����
    PlayerDataModel player;
    [SerializeField] private bool isPlayerLife = true;
    [SerializeField] Bullet playerBullet;


    public bool IsWave { get { return isWave; } }



    private void Awake()
    {
        bossChallengeBtn.onClick.AddListener(BossChallenge);
    }

    private void Start()
    {
        csvParser = StageCSV.Instance;

        //MapController > ���, �ϴ� �̹��� ����
        //mapController.BackGroundSpriteChange(mapData[(int)curMiddleMap].BackGroundSprite);
        //mapController.SkySpriteChange(mapData[(int)curMiddleMap].SkySprite);

        monsters = new MonsterModel[5];

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDataModel>();
        bossObject.gameObject.SetActive(false);
    }


    private void Update()
    {



        //Data�� �޾ƿ��� ���� ���¸� Return
        if (csvParser.State.Count == 0)
        {
            return;
        }



        //������ Wave ���Ͱ� ���� ���
        if (!isWave && fieldWaveMonsterCount < 1)
        {
            //Wave ȣ��
            Wave(); 
        }

        Debug.Log($"���� �������� :{curThirdClass} - {csvParser.State[parserIndex].Stage_wave}");
        Debug.Log($"���� �ε��� :{parserIndex}");
        if (player.Health < 1)
        {
            isPlayerLife = false;

        }


        StopedCoroutine();
        MonsterRemover();
        StageClear();
        MonsterSafeZone();
        PlayerDeath();
        BossStage();

        
 
        //�������� ����� UI ���� 
        foreGround.fillAmount = killRate * 0.01f;


    }


    /// <summary>
    /// ���� ���� Ȯ��
    /// </summary>
    private void MonsterRemover()
    {
        if (!isPlayerLife)
        {
            //�÷��̾� ��� �� ��ȯ�� ��� ���� ����
            //foreach (MonsterModel model in monsters)
            //{
            //    if (model != null)
            //    {
            //        Destroy(model.gameObject);
            //    }
            //}

            //���� ���� �迭 Ŭ����
            Array.Clear(monsters, 0, monsters.Length);
        }
        else
        {
            foreach (MonsterModel model in monsters)
            {
                if (model != null && model.MonsterHP < 1)
                {
                    for (int i = 0; i < monsters.Length; i++)
                    {
                        if (model == monsters[i])
                        {
                            monsters[i] = null;
                            killMonsterCount++;
                            fieldWaveMonsterCount--;
                        }
                    }
                }
            }

        }
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
            isStageClear = true;
            NextStage();
        }
    }

    /// <summary>
    /// Data Table ThirdClass Column > ��ȯ�Ǵ� �� ����
    /// </summary>
    public void CalculateMonsterSpawn()
    {
        bossObject.gameObject.SetActive(false);
        isBoss = false;

        //�������� ��ȯ ���� �� �ʱ�ȭ
        ThirdClassMonsterCount = 0;

        for (int i = parserIndex; i < parserIndex + waveCount; i++)
        {
            ThirdClassMonsterCount += csvParser.State[i].Stage_monsterNum;
        }
    }

    /// <summary>
    /// Stage Wave ���� 
    /// </summary>
    public void Wave()
    {
        killRate = ((float)killMonsterCount / ThirdClassMonsterCount) * 100f;

        //Index ���� ���� ����
        isWave = true;
        isStageClear = false;

        //�Һз� �������� 
        curThirdClass = csvParser.State[parserIndex].Stage_thirdClass;

        //�������� ���� ���� 
        if (parserIndex % waveCount == 0)
        {
            CalculateMonsterSpawn();
        }

        Debug.Log("���̺� ����");
        //���� ���̺� ���� ��
        curWaveMonsterCount = csvParser.State[parserIndex].Stage_monsterNum;

        createLimitCount = 0;
        CreateMonster();


        //isBoss && !isBosoClear && ��ư�� �����ִ� ����
        //return;

        //������ �׽�Ʈ �ε���
        //parserIndex = 4;


        if (parserIndex % waveCount < 4)
        {
            //���Ⱑ �����ΰ� ������ isPlayerLife && isStageClear?
            if (isPlayerLife)
            {
                parserIndex++;
            }

            //parserIndex++;
        }

    }

    public void BossStage()
    {
        if (parserIndex % waveCount >= 4)
        {
            Debug.Log("���� �������� ����");

            //���� �������� ����
            isBoss = true;

            if (isBoss)
            {   
                //������ ���� ����
                if(isStageClear)
                {
                    Debug.Log("���� Ŭ����");
                    parserIndex++;
                }
                else
                {
                    Debug.Log("���� Ŭ���� ����");
                    bossObject.gameObject.SetActive(true); 
                }
            }
        }
    }
     
    public void PlayerDeath()
    {
         
        if (!isPlayerLife)
        {
            Debug.Log("�÷��̾� ���");
            //Bullet�� ��� ���;� �ϴ°�?

            if (TryGetComponent<Bullet>(out playerBullet))
            {
                Destroy(playerBullet.gameObject);
            }
             
            //���� ���� ����
            if (createCo != null)
            {
                StopCoroutine(createCo);
                createCo = null;

                //Wave False�� ����
                isWave = false;
            }
 
            //���� �������� ���� ����
            if (isBoss)
            {
                //���� �ʵ� ���� �� ������ -1 ���� 
                if (!isLoop)
                {
                    parserIndex += -1;
                    isLoop = true;
                }
            }
            else
            {
                //���� 2�������� �̻� ���� ����
                if (parserIndex >= 5)
                { 
                    //���� Wave �ܰ�� �̵� 
                    //ex) Index 13 (3Stage 4Wave ����) > Index 5 (2Stage 1Wave) �̵�
                    parserIndex += -((parserIndex % waveCount) + waveCount); 
                }
                //���� 1�������� ����
                else
                {
                    parserIndex += -parserIndex;
                }
            }

            //��� ����
            bgAction?.Invoke();
            //�׽�Ʈ �ڵ�
            player.Health = 3000;

            isPlayerLife = true;
        } 
    }

 
    /// <summary>
    /// ���� �������� �絵�� ���
    /// </summary>
    public void BossChallenge()
    {
        Debug.Log("���� �絵��!!");
        parserIndex += 1;
        isLoop = false;
        bossObject.gameObject.SetActive(false);
        bgAction?.Invoke();
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

    public void StopedCoroutine()
    {
        //Wave ���� �Ϸ������
        if (curWaveMonsterCount <= createLimitCount && createCo != null)
        {
            //�ڷ�ƾ ����
            StopCoroutine(createCo);
            createCo = null;
            //Wave False�� ����
            isWave = false;
        }
    }

    int a = 0;
    private IEnumerator CreateMonsterCo()
    {

        WaitForSeconds createWait = new WaitForSeconds(createTimer);
        WaitForSeconds cycleWait = new WaitForSeconds(cycleTimer);
        monsters = new MonsterModel[curWaveMonsterCount];

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

            monsterInstance.gameObject.name = a.ToString() + "����";
            a++;
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
        switch (csvParser.State[parserIndex].Stage_firstClass)
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
