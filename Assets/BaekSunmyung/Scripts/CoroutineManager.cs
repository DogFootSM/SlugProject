using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public static CoroutineManager Instance;

    private Dictionary<string, IEnumerator> coList = new Dictionary<string, IEnumerator>();

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
    /// �ڷ�ƾ ����
    /// </summary>
    /// <param name="co">������ �ڷ�ƾ</param>
    /// <param name="coName">�ڷ�ƾ �̸�</param>
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
