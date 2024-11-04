using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class MergeSlot : MonoBehaviour, IDropHandler
{
    [Header("���� ���� �⺻ ����")]
    [SerializeField] MergeSystem system;

    public void OnDrop(PointerEventData eventData)
    {
        if (system == null) return;

        if (transform.childCount == 0)
        {
            if (eventData.pointerDrag.TryGetComponent<MergeItem>(out var Item))
            {
                Item.parentAfterDrag = transform;
            }
        }
        else if (transform.childCount == 1) // �̹� 1���� �������� ���� ��쿡 ���� üũ
        {
            if (eventData.pointerDrag.TryGetComponent<MergeItem>(out var dragItem))
            {
                var swapObject = transform.GetChild(0);
                if (swapObject.TryGetComponent<MergeItem>(out var swapItem))
                {
                    // ���� ������ ���� ��� ����
                    if (dragItem.MergeLevel == swapItem.MergeLevel)
                    {
                        // ���� ���� ��� �� �巡�� �������� ����
                        system.Merge(swapItem,dragItem);
                        system.UpdateMergeStatus();
                    }
                    // �׷��� ���� ��� ���� ����
                    else
                    {
                        //���� ���ư����� �ߴ� Ʈ�������� ���� �ڽ��� ���� �������� �ֱ�
                        swapObject.SetParent(dragItem.parentAfterDrag);
                        //�������� ������ parent�� ����
                        dragItem.parentAfterDrag = transform;
                    }
                }
            }
        }
    }

}
