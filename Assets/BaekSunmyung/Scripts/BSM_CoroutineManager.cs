using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using UnityEngine.UI;

public class BSM_CoroutineManager : MonoBehaviour
{
    public static BSM_CoroutineManager Instance { get; private set; }


    private Dictionary<MonoBehaviour, Coroutine> newCoList = new Dictionary<MonoBehaviour, Coroutine>();
     
    public Dictionary<float, WaitForSeconds> _waitForSeconds = new Dictionary<float, WaitForSeconds>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
     
    /// <summary>
    /// WaitForSeconds ��ü ��ȯ
    /// </summary>
    /// <param name="time">���� �ð� Key</param>
    /// <returns></returns>
    public WaitForSeconds GetWaitForSeconds(float time)
    {
        if (!_waitForSeconds.ContainsKey(time))
        {
            _waitForSeconds[time] = new WaitForSeconds(time);
        }

        return _waitForSeconds[time];
    }
     

    /// <summary>
    /// �ڷ�ƾ ����
    /// </summary>
    /// <param name="value">������ �ڷ�ƾ</param>
    /// <param name="key">�ڷ�ƾ �̸�</param>
    public IEnumerator ManagerCoroutineStart(Coroutine value, MonoBehaviour key)
    {
        //������ �̸��� �����ϴ��� Dictionary �˻�
        if (newCoList.ContainsKey(key))
        {
            if (newCoList.TryGetValue(key, out Coroutine copy))
            {
                Debug.Log("�ڷ�ƾ ����");
                StopCoroutine(copy);
                newCoList.Remove(key);
            }
        }

        Debug.Log("����Ʈ�� �ڷ�ƾ �Ҵ�");
        newCoList[key] = value; 
        IEnumerator enumerator = newCoList.GetEnumerator();

        while (enumerator.MoveNext())
        {
            //�κ��丮�� ���� ���¸� �Ͻ� ����
            if (GameManager.Instance.IsOpenInventory)
            {
                yield return new WaitUntil(() => !GameManager.Instance.IsOpenInventory);
            }
            else
            {
                Debug.Log("�ڷ�ƾ �ݺ�");
                //���� �ڷ�ƾ���� ������� wait������ ���
                yield return enumerator.Current;
            } 
        }
    }

    /// <summary>
    /// �ڷ�ƾ ���� ����
    /// </summary>
    /// <param name="coName">�ڷ�ƾ �̸�</param>
    public void ManagerCoroutineStop(MonoBehaviour key)
    { 
        if (newCoList.ContainsKey(key))
        {
            Debug.Log("�ڷ�ƾ ���� ����");
            StopCoroutine(newCoList[key]);
            newCoList.Remove(key); 
        }
    } 
}
