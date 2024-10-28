using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{

    [SerializeField] private Stage stage;

    [Header("��� �̹��� ����Ʈ")]
    [SerializeField] private List<GameObject> backgroundMaps = new List<GameObject>();
  
    [Header("��� �̹��� �̵� �ӵ�")]
    [SerializeField] private float mapTranslateSpeed;     //Player MoveSpeed ���
 
    [SerializeField] private Fade fade;

    //��� �̹��� �̵� �� ��ġ xPos 
    private float endPosX = 0;

    //��� �̹��� ����
    private int backGroundCount = 0;

    private Action bgAction;

    //��� �̹��� �ʱ�ȭ ��ġ
    private Vector3 startPos;

    private int viewMonsterCount;
    private bool isDeath;

    private Coroutine resetRoutine;
    private bool isChange;

    private void Awake()
    {
        backGroundCount = backgroundMaps.Count;

        endPosX = backgroundMaps[0].transform.localScale.x;

        //��� �̹��� ������ ���� �ʱ�ȭ x��ġ ����
        startPos = new Vector3(endPosX * (backGroundCount - 1), 0f);
    }

    private void Start()
    {
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

            if(isChange && resetRoutine != null)
            {
                Debug.Log("�ڷ�ƾ ����");
                StopCoroutine(resetRoutine);
                resetRoutine = null;
                isChange = false;
            }

        }
    }


    /// <summary>
    /// ��� �̹��� ���� ��ġ�� ����
    /// </summary>
    public void ResetBackGround()
    {
        fade.FadeOut();

        resetRoutine = StartCoroutine(ResetCo());
    }
  

    /// <summary>
    /// ��� �̹��� ��ġ �̵�
    /// </summary>
    public void TranslateBackGround()
    {
        //���� �� �󿡼� ���̴� ���Ͱ� ���� ��쿡�� �� �̵� ����
        //���� Player or Monster���� ������ Count�� �޾� �� �ʿ� ����
        // or Player�� �̵� ������ ��� �̵��ϴ� ������� ����
        if (viewMonsterCount == 0)
        {
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
    
    private IEnumerator ResetCo()
    {
        //���� ���ġ�Ǳ� �� ��� �ð�
        //���� player �� �غ� ���¸� �޾� �� �� ������ ��� �ð��� �ʿ� ����
        yield return new WaitForSeconds(1.5f);
 
        if (!isChange)
        {
            for (int i = 0; i < backGroundCount; i++)
            {
                backgroundMaps[i].transform.position = new Vector3(endPosX * i, 0f, 0f);
            } 
        }

        isChange = true;

        yield break;

    }


}
