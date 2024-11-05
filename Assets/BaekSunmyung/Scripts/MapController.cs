using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;

public class MapController : MonoBehaviour
{

    [SerializeField] private Stage stage;
    [Header("�� �������� ������")]
    [SerializeField] private List<MapData> mapData = new List<MapData>();

    [Header("��� �̹��� ����Ʈ")]
    [SerializeField] private List<GameObject> backgroundMaps = new List<GameObject>();

    [Header("��� �̹��� �̵� �ӵ�")]
    [SerializeField] private float mapTranslateSpeed;     //Player MoveSpeed ���

    [SerializeField] private Fade fade;

    [Header("Background Map Reset")]
    [SerializeField] private float backGroundResetSpeed;

    private int thirdIndex = 0;
    public int ThirdIndex { get { return thirdIndex; } set { thirdIndex = value; } }

    //��� �̹��� �̵� �� ��ġ xPos 
    private float endPosX = 0;

    //��� �̹��� ����
    private int backGroundCount = 0;

    private Action bgAction;

    //��� �̹��� �ʱ�ȭ ��ġ
    private Vector3 startPos;

    private int viewMonsterCount;
    private bool isDeath;

    private CoroutineManager crManager;
    private Coroutine resetRoutine;
    private bool isChange;
    private string coroutineName = "ResetCoroutine";

    private void Awake()
    {
        backGroundCount = backgroundMaps.Count;

        endPosX = backgroundMaps[0].transform.localScale.x * 17.82f;

        //��� �̹��� ������ ���� �ʱ�ȭ x��ġ ����
        startPos = new Vector3(endPosX * (backGroundCount - 1), -2.29f);
    }

    private void Start()
    {
        crManager = CoroutineManager.Instance;
        bgAction = ResetBackGround;
        stage.BackGroundResetAction(bgAction);
    }


    private void Update()
    {
        TranslateBackGround();
        RePositionBackGround();

        if (fade.IsFade)
        {
            fade.FadeIn();

            if (isChange)
            {
                crManager.ManagerCoroutineStop(this);
            } 
        }
    }


    /// <summary>
    /// ��� �̹��� ���� ��ġ�� ����
    /// </summary>
    public void ResetBackGround()
    {
        fade.FadeOut();
        Coroutine resetRoutine = StartCoroutine(MapResetCoroutine());
        crManager.ManagerCoroutineStart(resetRoutine, this);
    }


    /// <summary>
    /// ��� �̹��� ��ġ �̵�
    /// </summary>
    public void TranslateBackGround()
    {
        //���� �� �󿡼� ���̴� ���Ͱ� ���� ��쿡�� �� �̵� ����
        if (stage.FieldWaveMonsterCount == 0)
        {
            //��з� ���� 4��° ���� �������� ��ũ�Ѹ� x
            if (stage.SecondClass == 4 && stage.IsBoss)
            {
                RePositionBackGround();
                return;
            }
                 
            for (int i = 0; i < backGroundCount; i++)
            { 
                backgroundMaps[i].transform.Translate(Vector3.left * mapTranslateSpeed * Time.deltaTime);
            }
        } 
    }

    /// <summary>
    /// ��� �̹��� ��ġ ���� �̵�
    /// </summary>
    public void RePositionBackGround()
    {
        for (int i = 0; i < backGroundCount; i++)
        {
            if (backgroundMaps[i].transform.localPosition.x <= -endPosX)
            {
                backgroundMaps[i].transform.localPosition = new Vector3(33.56f, -2.29f);
            }
        }
    }

    /// <summary>
    /// Map Data�� �� ��� Sprite ���� 
    /// </summary>
    /// <param name="index">��з� �� �� �ε���</param>
    public void BackGroundSpriteChange(int index)
    {
        for (int i = 0; i < backGroundCount; i++)
        {
            SpriteRenderer render = backgroundMaps[i].GetComponent<SpriteRenderer>();
 
            if (index == 4)
            { 
                if (thirdIndex <= 3)
                {
                    render.sprite = mapData[index - 1].BackGroundSprite[0];
                }
                else 
                { 
                    //
                    if(i == 1)
                        render.flipX = true;
                    render.sprite = mapData[index - 1].BackGroundSprite[1];
                } 
            }
            else
            {
                render.sprite = mapData[index - 1].BackGroundSprite[0];

            }
            
        }
    }

    /// <summary>
    /// Map data�� �ϴ� ��� Sprite ����
    /// </summary>
    /// <param name="index">�� �ܰ躰 �޾ƿ� �ε���</param>
    public void SkySpriteChange(int index)
    {
        for (int i = 0; i < backGroundCount; i++)
        {
            SpriteRenderer skyRen = backgroundMaps[i].transform.GetChild(0).GetComponent<SpriteRenderer>();
            if (mapData[index - 1].SkySprite != null)
            {
                skyRen.sprite = mapData[index - 1].SkySprite[0];
            }
            else
            { 
                skyRen.sprite = null;
            } 
        }
    }
     

    private IEnumerator MapResetCoroutine()
    {
        yield return CoroutineManager.Instance.GetWaitForSeconds(backGroundResetSpeed);

        if (!isChange)
        {
            //if (GameManager.Instance.IsOpenInventory)
            //{
            //    yield return new WaitUntil(() => !GameManager.Instance.IsOpenInventory);
            //}

            for (int i = 0; i < backGroundCount; i++)
            {
                backgroundMaps[i].transform.position = new Vector3(endPosX * i, -2.29f, 0f);
            }
        }

        isChange = true;

        yield break;

    }


}
