using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MergeItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("���� �⺻ ����")]
    [SerializeField] private int mergeLevel;
    [HideInInspector] public MergeSystem system;
    public int MergeLevel
    {
        get { return mergeLevel; }
        set
        {
            if (mergeLevel < value)
            {
                // ���� ���ŵǸ鼭 ���� �ؽ�Ʈ�� ����
                mergeLevel = value;
                mergeLevelText?.SetText(mergeLevel.ToString());
            }
        }
    }
    [Tooltip("���� ���� ǥ���� �ؽ�Ʈ")]
    [SerializeField] TextMeshProUGUI mergeLevelText;
    [Tooltip("���� Ŭ�� �ð� ���� ����")]
    [SerializeField] private float doubleClickTIme;
    

    [HideInInspector] public Transform parentAfterDrag; // �巡�� �Ŀ� �ٽ� ���� ��ġ�� ���ư� �θ� Ʈ������
    private Image image;                                // �̹���UI�� ���� ��쿡 ���������� �κ��丮 �������̶� �����ϰ� ��Ȱ��ȭ

    void OnEnable()
    {
        if (TryGetComponent<Image>(out image) == false)
        {
            gameObject.SetActive(false);
        }
        else
        {
            mergeLevelText?.SetText(MergeLevel.ToString());
        }
    }

    #region ������ �巡�� ���
    private Vector3 originPos;
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡���� �������� Raycast ���� ���� ���� (�ٸ� Ray�� ��ȣ�ۿ���� �ʵ���)
        image.raycastTarget = false;
        // �ϴ� �巡�� �����ϸ�, ���� �θ��� Ʈ�������� ���� ��, �θ� ���� ����
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;
        transform.SetParent(parentAfterDrag);
    }
    #endregion

    #region ������ ����Ŭ�� ���
    private float lastClickTime;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (system == null) return;

        if (Time.time - lastClickTime < doubleClickTIme)
        {
            if (system.MergeItemDictionary.ContainsKey(MergeLevel))
            {
                // Ư�� ������ �����ϸ鼭 ���� ���� ���� ��
                for (int i = 0; i < system.MergeItemDictionary[MergeLevel].Count; i++)
                {
                    // �ڱ� �ڽų��� �����Ǵ� ��� ����
                    if (system.MergeItemDictionary[MergeLevel][i] == this) continue;
                    // �ռ��� �� �ִ� ������ ���� ���� �ռ� ����
                    if (system.MergeItemDictionary[MergeLevel][i] == null) continue;
                    // �������� ������ �´� ���� ��ųʸ��� �߰�
                    system.Merge(this,system.MergeItemDictionary[MergeLevel][i]);
                    system.UpdateMergeStatus();
                    // ���� �ռ� ����
                    break;
                }
            }
        }
        lastClickTime = Time.time;
    }
    #endregion
}
