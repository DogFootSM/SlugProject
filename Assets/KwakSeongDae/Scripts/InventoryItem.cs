using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �κ��丮 ������ (����, ��ų ��)�� ���� �����۰� ������
/// ���⳪ ��ų ��ũ��Ʈ�� ������ 
/// </summary>
public class InventoryItem : MonoBehaviour/*, IBeginDragHandler, IDragHandler, IEndDragHandler */
{
    [Header("�κ��丮 ������ �⺻ ����")]
    [Tooltip("���� ������ ������Ʈ")]
    [SerializeField] private GameObject ItemObject;
    [Tooltip("�κ��丮 �������� ��ư")]
    [SerializeField] private Button button;
    [Tooltip("Ȱ��ȭ �κ��丮 ����")]
    [SerializeField] private InventorySlot activeSlot;
    [Tooltip("������ �ر� ����")]
    public bool canUse;
    /* [HideInInspector] public Transform parentAfterDrag; // �巡�� �Ŀ� �ٽ� ���� ��ġ�� ���ư� �θ� Ʈ������ */

    private Image image;                                // �̹���UI�� ���� ��쿡 ���������� �κ��丮 �������̶� �����ϰ� ��Ȱ��ȭ

    void OnEnable()
    {
        if (TryGetComponent<Image>(out image) == false || button == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            button.onClick.AddListener(MoveToActiveSlot);
            // ���� �ڽ��� ���� üũ
            SlotCheck();
        }
    }

    void MoveToActiveSlot()
    {
        // �ر� ���� Ȯ��
        if (canUse == false) return;

        // ���� Ȱ��ȭ ���Կ� �ִ� �������� ���� ���Կ� �ű��
        if (activeSlot.transform.childCount > 0)
        {
            var activeItem = activeSlot.transform.GetChild(0);
            activeItem.SetParent(transform.parent);
            if (activeItem.TryGetComponent<InventoryItem>(out var item))
            {
                item.SlotCheck();
            }
        }

        // ���� ������ �������� Ȱ��ȭ �������� �ű��
        transform.SetParent(activeSlot.transform);
        SlotCheck();
    }

    /* ������ �巡�� ���
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
    */

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
