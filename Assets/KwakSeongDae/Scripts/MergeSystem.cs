using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;


public class MergeSystem : MonoBehaviour
{
    [Header("���� �ý��� �⺻ ����")]
    [SerializeField] private GameObject mergePrefab;
    [SerializeField] private int topMergeLevel;
    public int TopMergeLevel
    {
        get { return topMergeLevel; }
        set
        {
            if (topMergeLevel < value)
            {
                topMergeLevel = value;
                if (PlayerDataModel.Instance != null)
                    PlayerDataModel.Instance.BulletLevel = topMergeLevel;
            }
        }
    }
    [Tooltip("�ּ� ���� ����(�ִ� �������� �ش� ����ŭ �� ������ �ּ� ������ å��)")]
    [SerializeField] private int difLevel;

    [Header("�ڵ� ���� ���� ����")]
    [Tooltip("���� �ڵ� ���� ����")]
    [SerializeField] private float mergeManufactureTime;
    [SerializeField] private bool IsAutoMerge;

    public Dictionary<int, List<MergeItem>> MergeItemDictionary;

    // ���� ���Ե�
    private List<Transform> slots;
    // �ڵ� ���� ��ƾ
    private Coroutine autoMergeRoutine;
    // ���� �ּ� ���� ����
    private int curMinLevel;
    private void Start()
    {
        // �ʱ�ȭ
        MergeItemDictionary = new Dictionary<int, List<MergeItem>>();
        slots = new List<Transform>();
        curMinLevel = 1;

        //���� ���� �������� �ִ��� Ȯ���ؼ� ��ųʸ��� �ֱ�
        foreach (var item in GetComponentsInChildren<MergeItem>())
        {
            AddItemMergeDictionary(item.MergeLevel, item);
        }

        //���� ���� ���Ե� �߰�
        foreach (var slot in GetComponentsInChildren<MergeSlot>())
        {
            slots.Add(slot.transform);
        }

        //�ڵ� ���� ���� ��ƾ ����
        autoMergeRoutine = StartCoroutine(AutoMergeCoroutine());
    }

    /// <summary>
    /// ���ο� ���� �߰��ϴ� �Լ�
    /// </summary>
    public void AddMerge()
    {
        if (mergePrefab == null) return;

        int emptySlotIdx = CheckEmptySlot();

        if (emptySlotIdx > -1)
        {
            var obj = Instantiate(mergePrefab, slots[emptySlotIdx]);

            // ���� ���� ����
            if (obj.TryGetComponent<MergeItem>(out var item))
            {
                TopMergeLevel = item.MergeLevel;

                // ���� �ּ� ���� ������Ʈ
                UpdateCurrentMinLevel();

                item.MergeLevel = curMinLevel;

                AddItemMergeDictionary(item.MergeLevel, item);
                item.system = this;
            }
            else
            {
                //������ ���������� �Ǵ��Ͽ� ����
                Destroy(obj);
            }
        }
    }

    /// <summary>
    /// ���� ��ųʸ��� ������ �߰��ϴ� �Լ�
    /// </summary>
    /// <param name="key"> ���� �������� ����</param>
    /// <param name="item"> ���� ������ </param>
    void AddItemMergeDictionary(int key, MergeItem item)
    {
        if (MergeItemDictionary.ContainsKey(key) == false)
        {
            // �ش� ������ ��ųʸ��� ���� �߰��Ǵ� ���,���ο� ����Ʈ �߰�
            MergeItemDictionary[key] = new List<MergeItem>();
        }

        MergeItemDictionary[key].Add(item);
    }

    /// <summary>
    /// �� ������ �ռ��ϴ� �Լ�, origin�� ������ ����
    /// origin�� level�� 1 ���
    /// </summary>
    public void Merge(MergeItem origin, MergeItem draging)
    {
        if (origin == null || draging == null) return;

        // draging ������ ����
        Destroy(draging.gameObject);

        // origin�� ���� ����
        var originLevel = origin.MergeLevel;
        origin.MergeLevel++;
        // ���� ���� �ְ� ���� üũ, ���� �������� ���� ���� ������ ��� �ڵ� ����
        TopMergeLevel = origin.MergeLevel;

        // ���� �ּ� ���� ������Ʈ
        UpdateCurrentMinLevel();
    }

    public void UpdateMergeStatus()
    {
        // ��� �Ѿ��� ��ȸ�ϸ�, �� �� �ִ� ��ŭ ���� ��, �������� ������
        for (int level = 1; level < TopMergeLevel; level++)
        {
            if (MergeItemDictionary.ContainsKey(level))
            {
                // �ٸ� ������ �����Կ� ����, ���� ������ �ǰ� �ִ� �ش� ��Ҹ� ����Ʈ���� ����
                MergeItemDictionary[level].RemoveAll(item => item == null);

                // if ������ �ּ� �������� ���� ���: �� �� ������ŭ �����ϰ� �������� ����
                if (level < curMinLevel)
                {
                    // 1. ���� ��� ������Ʈ ����
                    int originMergeidx = -1;
                    // 3. 1 ~ 2 �ݺ�
                    for (int i = 0; i < MergeItemDictionary[level].Count; i++)
                    {
                        if (originMergeidx == -1)
                        {
                            originMergeidx = i;
                        }
                        else
                        {
                            // 2. ���� ����
                            Merge(MergeItemDictionary[level][originMergeidx], MergeItemDictionary[level][i]);
                            // �������� ��ųʸ��� �߰�
                            AddItemMergeDictionary(level + 1, MergeItemDictionary[level][originMergeidx]);
                            originMergeidx = -1;
                        }
                    }
                    // 4. ������ �ϳ��� ����, originMergeidx�� ���� �ϳ�
                    if (originMergeidx > 0) DestroyImmediate(MergeItemDictionary[level][originMergeidx].gameObject);
                    // ����Ʈ ����
                    MergeItemDictionary[level].Clear();
                }
                // else ���� ����Ʈ�� ������ ���� �ʴ� ������Ʈ�� ����
                else
                {
                    // ���� ���� �Ǵ� �����۵� ��, ������ ���� ���� �������� ����
                    for (int i = 0; i < MergeItemDictionary[level].Count; i++)
                    {
                        // ���� ���°� ���� ����Ʈ�� ���� ���� ���, ����
                        if (MergeItemDictionary[level][i].MergeLevel != level)
                        {
                            int updateLevel = MergeItemDictionary[level][i].MergeLevel;
                            // �������� ��ųʸ��� �߰��ϰ�, ���� ����Ʈ���� ����
                            AddItemMergeDictionary(updateLevel, MergeItemDictionary[level][i]);
                            MergeItemDictionary[level].RemoveAt(i);
                            //�ε��� ����
                            i--;
                        }
                    }
                }
            }
        }
    }

    void UpdateCurrentMinLevel()
    {
        // �ּ� ���� ����
        if (TopMergeLevel - difLevel > curMinLevel)
        {
            curMinLevel = TopMergeLevel - difLevel;
        }
    }

    IEnumerator AutoMergeCoroutine()
    {
        var delay = new WaitForSeconds(mergeManufactureTime);
        while (true)
        {
            if (IsAutoMerge)
            {
                AddMerge();
                MergeItem m1 = null;
                MergeItem m2 = null;
                bool isSkip = false;
                // ��� ������ �ְ������� ��ȸ�ϸ� ���� ���� ������ ��� ����
                for (int i = TopMergeLevel; i > 0; i--)
                {
                    if (isSkip) break;
                    if (MergeItemDictionary.ContainsKey(i) == false) continue;

                    MergeItem originItem = null;

                    // ���� ������ ������ ���� �����ϸ� ���� ����
                    // �� �Ͽ� �ѹ� ���� �����, But, �ּ� �������� ���� ���� ������ �ѹ��� ����
                    foreach (var item in MergeItemDictionary[i])
                    {
                        if (isSkip) break;

                        if (originItem == null)
                        {
                            originItem = item;
                        }
                        else
                        {
                            m1 = originItem;
                            m2 = item;
                            isSkip = true;
                        }
                    }
                }
                // ���� ����
                Merge(m1, m2);
                UpdateMergeStatus();
                yield return delay;
            }
            else yield return null;
        }
    }

    public void AutoMergeToggleButton()
    {
        IsAutoMerge ^= true;
    }

    /// <summary>
    /// �� ���� ������ üũ�ϴ� �Լ�
    /// </summary>
    /// <returns> �� ���� ������ �ε���, -1�� �� ������ ���� ��� </returns>
    int CheckEmptySlot()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].childCount < 1) return i;
        }
        return -1;
    }
}
