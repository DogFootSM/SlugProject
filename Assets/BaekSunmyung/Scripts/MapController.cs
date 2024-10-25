using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MapController : MonoBehaviour
{

    [Header("��� �̹��� ����Ʈ")]
    [SerializeField] private List<GameObject> backgroundMaps = new List<GameObject>();
 

    [Header("��� �̹��� �̵� �ӵ�")]
    [SerializeField] private float mapTranslateSpeed;     //Player MoveSpeed ���

    [Header("�� �������� ������")]
    [SerializeField] private List<MapData> mapData = new List<MapData>();

    //�׽�Ʈ �ڵ� > ���� ������ ���� �ʿ�
    [SerializeField] private int viewMonsterCount = 0;
    [SerializeField] private int totalMonsterCount = 0;
    [SerializeField] private int killMonsterCount = 0;

    [SerializeField] private Fade fade;

    //������ ���� �ʿ�
    private float killRate = 0f;

    //��� �̹��� �̵� �� ��ġ xPos 
    private float endPosX = 0;

    //��� �̹��� ����
    private int backGroundCount = 0;

    //��� �̹��� �ʱ�ȭ ��ġ
    private Vector3 startPos;

    //�ӽ� ������
    private MiddleMap curMiddleMap = MiddleMap.One;
    private int curSmallStage = 1;


    private bool isDeath;


    private void Awake()
    {
        backGroundCount = backgroundMaps.Count;

        endPosX = backgroundMaps[0].transform.localScale.x;

        //��� �̹��� ������ ���� �ʱ�ȭ x��ġ ����
        startPos = new Vector3(endPosX * (backGroundCount - 1), 0f);
    }

    private void Start()
    { 
        //BackGroundSpriteChange(mapData[(int)curMiddleMap].BackGroundSprite);
        //SkySpriteChange(mapData[(int)curMiddleMap].SkySprite);
    }


    private void Update()
    {
        Debug.Log($"���� ��������:{curMiddleMap} - {curSmallStage}");

        //�׽�Ʈ �ڵ�
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //viewMonsterCount--;
            killMonsterCount++;

            //�������� �����
            killRate = ((float)killMonsterCount / totalMonsterCount) * 100;

        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            viewMonsterCount++;
        }


        TranslateBackGround();
        RePositionBackGround();
        StageClear();


        //isDeath ���� ���� Player Life �� �޾ƿ� �ʿ䰡 ����
        if (isDeath)
        {
            ResetBackGround();
        }


    }


    /// <summary>
    /// ��� �̹��� ���� ��ġ�� ����
    /// </summary>
    public void ResetBackGround()
    {

        for (int i = 0; i < backGroundCount; i++)
        {
            backgroundMaps[i].transform.position = new Vector3(endPosX * i, 0f, 0f);
         
        }

    }

    /// <summary>
    /// Stage Ŭ���� �� BackGround ���� ����
    /// </summary>
    public void StageClear()
    {

        if (killRate >= 100f)
        {
            fade.FadeOut();
            ResetBackGround(); 
            NextStage();

            killMonsterCount = 0;
            killRate = 0f;
            viewMonsterCount = 1;
        }
        else
        {
            //�� ���� �Ϸ������ Fade In
            if (fade.IsFade)
            {
                fade.FadeIn();
            }
        }

    }

    public void NextStage()
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

            //Map Data�� Sprite Image�� ����
            //BackGroundSpriteChange(mapData[(int)curMiddleMap].BackGroundSprite); 
            //SkySpriteChange(mapData[(int)curMiddleMap].SkySprite);
        }
    }

    /// <summary>
    /// ��� �̹��� ��ġ �̵�
    /// </summary>
    public void TranslateBackGround()
    {
        //���� �� �󿡼� ���̴� ���Ͱ� ���� ��쿡�� �� �̵� ����
        //���� Player or Monster���� ������ Count�� �޾� �� �ʿ� ����
        if (viewMonsterCount == 0)
        {
            for (int i = 0; i < backGroundCount; i++)
            {
                backgroundMaps[i].transform.Translate(Vector3.left * mapTranslateSpeed * Time.deltaTime);
            }
        }

    }

    /// <summary>
    /// ��� �̹��� ��ġ ����
    /// </summary>
    public void RePositionBackGround()
    {
        for (int i = 0; i < backGroundCount; i++)
        {
            if (backgroundMaps[i].transform.localPosition.x <= -endPosX)
            {
                backgroundMaps[i].transform.localPosition = startPos;
            }
        }
    }

    /// <summary>
    /// Map Data�� �� ��� Sprite ����
    /// </summary>
    /// <param name="sprite">�� �ܰ� �� �޾ƿ� ��׶��� �̹���</param>
    public void BackGroundSpriteChange(Sprite sprite)
    { 
        for (int i = 0; i < backGroundCount; i++)
        {
            SpriteRenderer render = backgroundMaps[i].GetComponent<SpriteRenderer>();
            render.sprite = sprite;
        }
    }

    /// <summary>
    /// Map data�� �ϴ� ��� Sprite ����
    /// </summary>
    /// <param name="sprite">�� �ܰ� �� �ϴ� �̹���</param>
    public void SkySpriteChange(Sprite sprite)
    {
        for(int i = 0; i < backGroundCount; i++)
        { 
            SpriteRenderer skyRen = backgroundMaps[i].transform.GetChild(0).GetComponent<SpriteRenderer>();
            skyRen.sprite = sprite; 
        } 
    }



}
