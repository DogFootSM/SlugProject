using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �κ��丮 ������ (����, ��ų ��)�� ���� �����۰� ������
/// ���⳪ ��ų ��ũ��Ʈ�� ������ 
/// </summary>
/// <param name="ItemObject"> ���� �����ۿ� ���� ���� (������ Ÿ�Կ� ����, ������ Ȱ��ȭ ���� ����) </param>
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // ������ ����(���� ���� ����, ������ ����)�� ���� �ش� ���� ������Ʈ Ȱ��ȭ ���� üũ
    public GameObject ItemObject;
    [HideInInspector] public Transform parentAfterDrag; // �巡�� �Ŀ� �ٽ� ���� ��ġ�� ���ư� �θ� Ʈ������

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
        SlotCheck();
    }

    /// <summary>
    /// ���� �ڽ��� ���� ������ Ÿ���� üũ�Ͽ� ���� �������� ��Ʈ���ϴ� �Լ�
    /// </summary>
    public void SlotCheck()
    {
        if (transform.parent.TryGetComponent<InventorySlot>(out var slot))
        {
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
