using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public enum SlotType
    {
        ActiveSlot, StorageSlot, Size
    }

    [Header("���� ���� ����")]
    [Tooltip("ActiveSlot: �ش� ���Կ� ���� �������� Ȱ��ȭ\n" +
             "StorageSlot: �ش� ���Կ� ���� �������� ��Ȱ��ȭ")]
    public SlotType slotType;
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            if (eventData.pointerDrag.TryGetComponent<InventoryItem>(out var Item))
            {
                Item.parentAfterDrag = transform;
            }
        }
        else if (transform.childCount == 1) // �̹� 1���� �������� ���� ��쿡 ���� ����
        {
            if (eventData.pointerDrag.TryGetComponent<InventoryItem>(out var Item))
            {
                //���� ���ư����� �ߴ� Ʈ�������� ���� �ڽ��� ���� �������� �ֱ�
                var swapObject = transform.GetChild(0);
                swapObject.SetParent(Item.parentAfterDrag);
                // �ٲ� ��, �巡�� �ȵǴ� �����ۿ� ���ؼ� ���� ���� üũ
                if (swapObject.TryGetComponent<InventoryItem>(out var Item2))
                {
                    Item2.SlotCheck();
                }

                //�������� ������ parent�� ����
                Item.parentAfterDrag = transform;
            }
        }
    }
}
