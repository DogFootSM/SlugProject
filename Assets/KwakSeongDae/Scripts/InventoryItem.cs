using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �κ��丮 ������ (����, ��ų ��)�� ���� �����۰� ������
/// ���⳪ ��ų ��ũ��Ʈ�� ������ 
/// </summary>
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("�κ��丮 ������ �⺻ ����")]
    [SerializeField] private GameObject ItemObject;
    [HideInInspector] public Transform parentAfterDrag; // �巡�� �Ŀ� �ٽ� ���� ��ġ�� ���ư� �θ� Ʈ������
    public bool canUse;

    private Image image;
    // �̹���UI�� ���� ��쿡 ���������� �κ��丮 �������̶� �����ϰ� ��Ȱ��ȭ
    void OnEnable()
    {
        if (TryGetComponent<Image>(out image) == false)
        {
            gameObject.SetActive(false);
        }
        else
        {
            // ���� �ڽ��� ���� üũ
            SlotCheck();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canUse)
        {
            // �巡���� �������� Raycast ���� ���� ���� (�ٸ� Ray�� ��ȣ�ۿ���� �ʵ���)
            image.raycastTarget = false;
            // �ϴ� �巡�� �����ϸ�, ���� �θ��� Ʈ�������� ���� ��, �θ� ���� ����
            parentAfterDrag = transform.parent;
            transform.SetParent(transform.root);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canUse)
        {
            transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canUse)
        {
            image.raycastTarget = true;
            transform.SetParent(parentAfterDrag);
            SlotCheck();
        }
    }

    /// <summary>
    /// ���� �ڽ��� ���� ������ Ÿ���� üũ�Ͽ� ���� �������� ��Ʈ���ϴ� �Լ�
    /// ��� �������� OnEnable()���� �ʱ�ȭ �۾� �ʿ�
    /// </summary>
    public void SlotCheck()
    {
        if (transform.parent != null && transform.parent.TryGetComponent<InventorySlot>(out var slot))
        {
            if (ItemObject == null)
            {
                Debug.Log("���� �κ��丮 �����ۿ� ���� ������ ������Ʈ�� �Ҵ���� �ʾҽ��ϴ�.");
                return;
            }
            switch (slot.slotType)
            {
                case InventorySlot.SlotType.ActiveSlot:
                    ItemObject.SetActive(true);
                    break;
                case InventorySlot.SlotType.StorageSlot:
                    ItemObject.SetActive(false);
                    break;
            }
        }
    }
}
