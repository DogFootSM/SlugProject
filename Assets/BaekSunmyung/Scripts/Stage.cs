using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
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

    [Header("Monster Wave Setting")]
    [Tooltip("Monster Wave Cycle")]
    [SerializeField] private float cycleTimer;

    [Tooltip("Monster Create Delay")]
    [SerializeField] private float createTimer;

    [Header("Monster Safe Zone")]
    [SerializeField] private Transform safeZone;

    [Header("Stage Data Variable")]
    [Tooltip("��з� �������� ����")]
    [SerializeField] private int stageSecondClass;
    [Tooltip("�Һз� Wave ����")]
    [SerializeField] private int waveCount;

    [SerializeField] private TextMeshProUGUI stageInfoText;

    [Header("Test Mode On")]
    [SerializeField] private Button testModeButton;
    [SerializeField] private bool isTestMode;
    [Tooltip("Input Data Table Index")]
    [SerializeField] private int testStageIndex;

    //�ӽ� ����
    [SerializeField] private GameObject bossObject;
    [SerializeField] private Button bossChallengeBtn;

    public int FieldWaveMonsterCount { get { return fieldWaveMonsterCount; } }

    //��� �̹��� ����
    public Action bgAction;

    //��� �̹��� �ε���
    private int bgSecondClsIndex = 0;

    //Data Table CSV ����
    private StageCSV csvParser;

    //Data Table Index ����
    private int parserIndex = 0;

    //���� ������
    private float killRate;

    //Stage ���� ����
    private MiddleMap curSecondClass = MiddleMap.First;     //�������� ���̵� ����
    private int curThirdClass;          //���� ���������� ��ġ
    private int curWaveMonsterCount;    //���� Wave���� ������ ���� ��
    private int curWaveKillCount;       //���� �������� �̵� �� ��������� ���� ��
    private int fieldWaveMonsterCount;  //���� �ʵ����� ���� ���� ��
    private int killMonsterCount;       //������� ���� ���� ��
    private int ThirdClassMonsterCount; //���� ���������� ��ȯ�� �� ���� ��


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
    private PlayerDataModel player;
    [SerializeField] private bool isPlayerLife = true;


    public bool IsWave { get { return isWave; } }


    private void Awake()
    {
        bossChallengeBtn.onClick.AddListener(BossChallenge);
        testModeButton.onClick.AddListener(TestMode);
    }

    private void Start()
    {
        csvParser = StageCSV.Instance;
        bgSecondClsIndex = parserIndex / stageSecondClass;

        //MapController > ���, �ϴ� �̹��� ����
        //mapController.BackGroundSpriteChange(mapData[bgSecondClsIndex].BackGroundSprite);
        //mapController.SkySpriteChange(mapData[bgSecondClsIndex].SkySprite);

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
        Debug.Log($"�ļ� �?:{csvParser.State.Count}");
        //������ Wave ���Ͱ� ���� ���
        if (!isWave && fieldWaveMonsterCount < 1)
        {
            //Wave ȣ��
            Wave();
        }

        if (player.Health < 1)
        {
            isPlayerLife = false;
        }




        MonsterSafeZone();
        PlayerDeath();
        StageClear();
        StopedCoroutine();
        MonsterRemover();

        //BG �ޱ��������� Update ���� ���
        bgSecondClsIndex = parserIndex / stageSecondClass;
        SetStageText();

        Debug.Log($"���� �ε��� :{parserIndex}");
        Debug.Log($"���� isBoss :{isBoss}");
        Debug.Log($"���� �������� :{curThirdClass} - {csvParser.State[parserIndex].Stage_wave}");

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
            foreach (MonsterModel model in monsters)
            {
                if (model != null)
                {
                    Destroy(model.gameObject);
                }
            }

            //���� ���� �迭 Ŭ����
            Array.Clear(monsters, 0, monsters.Length);
            isPlayerLife = true;
            player.Health = 3000;
        }
        else
        {
            //�÷��̾ ������� �� ���� ���� �۾�
            foreach (MonsterModel model in monsters)
            {
                if (model != null && model.MonsterHP < 1)
                {
                    for (int i = 0; i < monsters.Length; i++)
                    {
                        if (model == monsters[i])
                        {
                            curWaveKillCount++;
                            monsters[i] = null;
                            killMonsterCount++;
                            fieldWaveMonsterCount--;
                        }
                    }
                }
            }
            //�������� Ŭ���� Ʈ����
            isStageClear = true;

        }
    }



    /// <summary>
    /// ���� �������� �̵�
    /// </summary>
    private void NextStage()
    {
        SetDifficult();

        //�Ȱ��� ��� �̹��� ���������� ���� ����
        //���� BG Sprite ���� ���� ��� ��� ����
        //if(bgSecondClsIndex != (parserIndex / stageSecondClass))
        //{
        //    bgSecondClsIndex = parserIndex / stageSecondClass;
        //    //���, �ϴ� �̹��� ����
        //    mapController.BackGroundSpriteChange(mapData[bgSecondClsIndex].BackGroundSprite);
        //    mapController.SkySpriteChange(mapData[bgSecondClsIndex].SkySprite);
        //}

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
            isBoss = false;
            isBossClear = true;
            NextStage();
        }
    }

    /// <summary>
    /// Data Table ThirdClass Column > ��ȯ�Ǵ� �� ����
    /// </summary>
    public void CalculateMonsterSpawn()
    {
        //�� �������� 1Wave ���� �� Boss Ŭ���� �� ���·� ���� 
        isBossClear = false;

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

        //�������� ���� ���� 
        if (parserIndex % waveCount == 0)
        {
            CalculateMonsterSpawn();
        }

        //�������� Ŭ���� �� ���¿����� Index ����
        if (isStageClear)
        {
            parserIndex++;
            curWaveKillCount = 0;
        }

        //���� �������� ���� �ܰ�
        if (parserIndex % waveCount >= 4)
        {
            isBoss = true;
            bossObject.gameObject.SetActive(false);
        }

        BossStage();

        //�������� �����
        killRate = ((float)killMonsterCount / ThirdClassMonsterCount) * 100f;

        //Index ���� ���� ����
        isWave = true;
        isStageClear = false;

        //�Һз� �������� 
        curThirdClass = csvParser.State[parserIndex].Stage_thirdClass;

        //���� ���̺� ���� ��
        curWaveMonsterCount = csvParser.State[parserIndex].Stage_monsterNum;

        //���� ���� ���� ��
        createLimitCount = 0;
        CreateMonster();

    }

    public void BossStage()
    {
        //���� �ܰ迡 ������ ���¿��� �÷��̾� ��� ����
        if (parserIndex % waveCount == 3 && isBoss && !isBossClear)
        {
            bossObject.gameObject.SetActive(true);
        }

        //���� Ŭ���� Ȯ��
        if (isBossClear)
        {
            Debug.Log("���� Ŭ����");
        }

    }

    public void PlayerDeath()
    {
        int prevIndex = 0;
        int prevWaveCount = 0;
        if (!isPlayerLife)
        {
            //���� Wave �ε���
            prevIndex = parserIndex > 0 ? parserIndex - 1 : 0;

            //���� Wave ���� ��
            prevWaveCount = csvParser.State[prevIndex].Stage_monsterNum;

            //���� ���� ���� �� - (���� ������������ ����� ���� �� + ���� ������������ ���� ���ͼ�)
            killMonsterCount = (killMonsterCount - (prevWaveCount + curWaveKillCount));

            //�������� ����� ����
            killRate = ((float)killMonsterCount / ThirdClassMonsterCount) * 100f;
            fieldWaveMonsterCount = 0;
            isStageClear = false;

            //���� �������� ���� ����
            if (isBoss)
            {
                //���� �ʵ� ���� �� ������ -1 ���� 
                if (!isLoop)
                {
                    parserIndex--;
                    isLoop = true;
                }

            }
            else
            {
                parserIndex += parserIndex > 0 ? -1 : 0;
            }

            //��� ����
            bgAction?.Invoke();

            curWaveKillCount = 0;
        }

    }

    /// <summary>
    /// ���� �������� �絵�� ���
    /// </summary>
    public void BossChallenge()
    {
        parserIndex++;
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
        if ((curWaveMonsterCount <= createLimitCount && createCo != null))
        {
            //�ڷ�ƾ ����
            StopCoroutine(createCo);
            createCo = null;
            //Wave False�� ����
            isWave = false;

            isPlayerLife = true;
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
     

    public void SetStageText()
    {
        int curWave = csvParser.State[parserIndex].Stage_wave;

        stageInfoText.text = curDifficult.ToString() + " Stage " + curThirdClass.ToString() + " - " + curWave.ToString(); 
    }

    /// <summary>
    /// Stage Test Mode
    /// </summary>
    public void TestMode()
    { 
        if (isTestMode)
        {
            foreach (MonsterModel model in monsters)
            {
                if (model != null)
                {
                    Destroy(model.gameObject);
                }
            }

            //���� ���� �迭 Ŭ����
            Array.Clear(monsters, 0, monsters.Length);

            isWave = false;
            fieldWaveMonsterCount = 0;
            parserIndex = testStageIndex;
            int startIndex = parserIndex - (parserIndex % waveCount);

            for (int i = startIndex; i < parserIndex; i++)
            {
                killMonsterCount += csvParser.State[i].Stage_monsterNum;
            }

            int endIndex = waveCount - (parserIndex % waveCount);
            ThirdClassMonsterCount = 0;
            for (int i = startIndex; i < parserIndex + endIndex; i++)
            {
                ThirdClassMonsterCount += csvParser.State[i].Stage_monsterNum;
            }

            parserIndex--;

        }
        else
        {
            parserIndex = 0;
        }

    }

}
