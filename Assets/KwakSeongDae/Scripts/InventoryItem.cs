using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// �κ��丮 ������ (����, ��ų ��)�� ���� �����۰� ������
/// ���⳪ ��ų ��ũ��Ʈ�� ������ 
/// </summary>
public class InventoryItem : MonoBehaviour
{
    [Header("�κ��丮 ������ �⺻ ����")]
    [Tooltip("���� ������ ������Ʈ")]
    public GameObject ItemObject;
    [Tooltip("�κ��丮 �������� ��ư")]
    [SerializeField] private Button button;
    [Tooltip("Ȱ��ȭ ����")]
    [SerializeField] private InventorySlot activeSlot;
    [Tooltip("������ �����ϴ� �κ��丮 Ʈ������")]
    [SerializeField] private Transform inventory;
    [Tooltip("������ �ر� ����")]
    public bool canUse;

    private Image image;                                // �̹���UI�� ���� ��쿡 ���������� �κ��丮 �������̶� �����ϰ� ��Ȱ��ȭ
    [HideInInspector] public Transform originParent;
    void OnEnable()
    {
        if (TryGetComponent<Image>(out image) == false || button == null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            button.onClick.AddListener(MoveToActiveSlot);
            originParent = transform.parent;
            // ���� �ڽ��� ���� üũ
            SlotCheck();
        }
    }
    private void OnDisable()
    {
        if (button != null)
        {
            button.onClick.RemoveAllListeners();
        }
    }

    private void Update()
    {
        // �ر� ���� Ȯ��
        button.interactable = canUse;
    }

    void MoveToActiveSlot()
    {
        // �ر� ���� �ٽ� Ȯ��
        if (canUse == false) return;

        // ���� �������� ���Կ� �ִ��� üũ
        if (transform.parent.TryGetComponent<InventorySlot>(out var slot) == false) return;

        // if Ȱ��ȭ ���Կ� �������� �ִ� ���� ���� ��������
        if(slot.slotType == InventorySlot.SlotType.ActiveSlot)
        {
            transform.SetParent(originParent);
        }
        // else ���丮�� ���Կ� �������� �ִ� ���� Ȱ��ȭ ��������
        else
        {
            // ���� Ȱ��ȭ ���Կ� �ִ� �������� ���� ���Կ� �ű��
            if (activeSlot.transform.childCount > 0)
            {
                var activeItem = activeSlot.transform.GetChild(0);
                if (activeItem.TryGetComponent<InventoryItem>(out var item))
                {
                    activeItem.SetParent(item.originParent);
                    item.SlotCheck();
                }
            }
            // ���� ������ �������� Ȱ��ȭ �������� �ű��
            transform.SetParent(activeSlot.transform);
        }

        // � �۾��� ����ǵ��� ���� ���� üũ �ʿ�
        SlotCheck();
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
