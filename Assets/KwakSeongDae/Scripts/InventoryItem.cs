using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �κ��丮 ������ (����, ��ų ��)�� �ش� ��ũ��Ʈ ���� �ʿ�,
/// ���⳪ ��ų ��ũ��Ʈ�� ������ 
/// </summary>
public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // ������ ����(���� ���� ����, ������ ����)�� ���� �ش� ���� ������Ʈ Ȱ��ȭ ���� üũ
    public GameObject ItemObject;
    [HideInInspector] public Transform parentAfterDrag; // �巡�� �Ŀ� �ٽ� ���� ��ġ�� ���ư� �θ� Ʈ������

    private Image image;
    void OnEnable()
    {
        if (TryGetComponent<Image>(out image) == false)
        {
            // UI���� ���� �̹���
            image = gameObject.AddComponent<Image>();
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
    }


}
