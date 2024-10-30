using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour/*, IDropHandler*/
{
    public enum SlotType
    {
        ActiveSlot, StorageSlot, Size
    }

    [Header("���� ���� ����")]
    [Tooltip("ActiveSlot: �ش� ���Կ� ���� �������� Ȱ��ȭ\n" +
             "StorageSlot: �ش� ���Կ� ���� �������� ��Ȱ��ȭ")]
    public SlotType slotType;
    /* ��� ��� ��Ȱ��ȭ
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            if (eventData.pointerDrag.TryGetComponent<InventoryItem>(out var Item) && Item.canUse)
            {
                Item.parentAfterDrag = transform;
            }
        }
        else if (transform.childCount == 1) // �̹� 1���� �������� ���� ��쿡 ���� ����
        {
            if (eventData.pointerDrag.TryGetComponent<InventoryItem>(out var dragItem) && dragItem.canUse)
            {
                var swapObject = transform.GetChild(0);
                // �ٲ� ��, ���ҵ� �����ۿ� ���� ���� üũ ����
                if (swapObject.TryGetComponent<InventoryItem>(out var swapItem) && swapItem.canUse)
                {
                    //���� ���ư����� �ߴ� Ʈ�������� ���� �ڽ��� ���� �������� �ֱ�
                    swapObject.SetParent(dragItem.parentAfterDrag);
                    swapItem.SlotCheck();
                    //�������� ������ parent�� ����
                    dragItem.parentAfterDrag = transform;
                }
            }
        }
    }
    */
}
