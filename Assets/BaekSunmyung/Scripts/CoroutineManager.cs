using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public static CoroutineManager Instance { get; private set; }


    private Dictionary<MonoBehaviour, Coroutine> newCoList = new Dictionary<MonoBehaviour, Coroutine>();

    private Dictionary<string, IEnumerator> coList = new Dictionary<string, IEnumerator>();

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

    public WaitForSeconds GetWaitForSeconds(float time)
    {
        if (!_waitForSeconds.ContainsKey(time))
        {
            _waitForSeconds[time] = new WaitForSeconds(time);
        }

        return _waitForSeconds[time];
    }


    public void ManagerCoroutineStart(IEnumerator co, string coName)
    {
        //������ �̸��� �����ϴ��� Dictionary �˻�
        if (coList.ContainsKey(coName))
        {
            //������ �̸��� �ִٸ� Value�� ������ �� �ִ��� Ȯ��
            if (coList.TryGetValue(coName, out IEnumerator copyCo))
            {
                //Value�� �����Դٸ� �ڷ�ƾ�� �������� �����̴� �����ϰ� ����
                StopCoroutine(copyCo);
                coList.Remove(coName);
            }
        }

        coList[coName] = co;
        StartCoroutine(StartMyCoroutine(co, coName));
    }


    /// <summary>
    /// �ڷ�ƾ ����
    /// </summary>
    /// <param name="co">������ �ڷ�ƾ</param>
    /// <param name="key">�ڷ�ƾ �̸�</param>
    //public void ManagerCoroutineStart(Coroutine co, MonoBehaviour key)
    //{
    //    //������ �̸��� �����ϴ��� Dictionary �˻�
    //    if (newCoList.ContainsKey(key))
    //    {
    //        if(newCoList.TryGetValue(key, out Coroutine copy))
    //        {
    //            StopCoroutine(copy);
    //            newCoList.Remove(key);
    //        } 
    //    }

        
    //    newCoList[key] = co;
    //    co = StartCoroutine(StartMyCoroutine());
    //}
     
    /// <summary>
    /// �ڷ�ƾ ���� ����
    /// </summary>
    /// <param name="coName">�ڷ�ƾ �̸�</param>
    public void ManagerCoroutineStop(string coName)
    {
        if (coList.ContainsKey(coName))
        {
            StopCoroutine(coList[coName]);
            coList.Remove(coName);
        }
    }

    //public IEnumerator StartMyCoroutine(MonoBehaviour key)
    //{
    //    //�� �ڷ�ƾ ���Ǻ��� ������ �ǰ� �ִ� �������� Ȯ��
    //    while (newCoList[key] != null)
    //    {
    //        //�κ��丮�� ���� ���¸� �Ͻ� ����
    //        if (GameManager.Instance.IsOpenInventory)
    //        {
    //            yield return new WaitUntil(() => !GameManager.Instance.IsOpenInventory);
    //        }
    //        else
    //        {
    //            //���� �ڷ�ƾ���� ������� wait������ ���
    //            yield return co.Current;
    //        }
    //    }

    //    //�ڷ�ƾ�� ��������� ���� 
    //    if (coList.ContainsKey(key))
    //    {
    //        coList.Remove(key);
    //    }
    //}


    public IEnumerator StartMyCoroutine(IEnumerator co, string coName)
    {
        //�� �ڷ�ƾ ���Ǻ��� ������ �ǰ� �ִ� �������� Ȯ��
        while (co.MoveNext())
        {
            //�κ��丮�� ���� ���¸� �Ͻ� ����
            if (GameManager.Instance.IsOpenInventory)
            {
                yield return new WaitUntil(() => !GameManager.Instance.IsOpenInventory);
            }
            else
            {
                //���� �ڷ�ƾ���� ������� wait������ ���
                yield return co.Current;
            }
        }

        //�ڷ�ƾ�� ��������� ���� 
        if (coList.ContainsKey(coName))
        {
            coList.Remove(coName);
        }
    }


}
