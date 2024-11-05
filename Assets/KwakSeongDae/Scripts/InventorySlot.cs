using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public enum SlotType
    {
        ActiveSlot, StorageSlot, Size
    }

    [Header("���� ���� ����")]
    [Tooltip("ActiveSlot: �ش� ���Կ� ���� �������� Ȱ��ȭ\n" +
             "StorageSlot: �ش� ���Կ� ���� �������� ��Ȱ��ȭ")]
    public SlotType slotType;
}
