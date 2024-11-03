using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StatButtonController : MonoBehaviour
{
    enum CoolTimeType
    {
        FixedTime,DynamicTime
    }
    [Header("��ư ���� ����")]
    [Tooltip("��ư Ŭ�� ��, ���� �ð� �Ŀ� �ݺ� Ʈ���� ���� ����")]
    [SerializeField] private float delayAfterButtonDown;
    [Tooltip("�ݺ� Ʈ���� �ð� ���� ����")]
    [SerializeField] private float triggerCoolTime;
    [Tooltip("���� �ð� ����: �ð��� ������ ���� �ð� �������� ���� ����\n ���� �ð� ����: �ð��� ������ �ּ� �ð� �������� ���� ����, �ð� ������ Lerp")]
    [SerializeField] private CoolTimeType coolTimeType;
    [Tooltip("�ּ� �ð� ���ݱ��� �����ϴ� �ð�")]
    [SerializeField] private float minCoolTimeDelay;
    [Tooltip("�ּ� �ð� ���� ����")]
    [SerializeField] private float triggerMinCoolTime;


    [Header("Ŭ�� �� ������ �̺�Ʈ")]
    [SerializeField] UnityEvent onClick;

    private string refeatButtonClickRoutine = "refeatButtonClick";
    public void ButtonDown()
    {
        // CoroutinManager�� �����ϴ� ��쿡 �۵�, ���� �ڷ�ƾ �۵� ���δ� �ش� �Լ����� üũ
        CoroutineManager.Instance.ManagerCoroutineStart(RefeatTriggerRoutine(), refeatButtonClickRoutine);
    }

    public void ButtonUp()
    {
        CoroutineManager.Instance.ManagerCoroutineStop(refeatButtonClickRoutine);
    }

    IEnumerator RefeatTriggerRoutine()
    {
        // ��ȸ�� ��ư Ŭ�� ó�� �� ������ ���� �ݺ� Ʈ���� �۾� �ǽ�
        onClick?.Invoke();

        yield return new WaitForSeconds(delayAfterButtonDown);
        var delay = new WaitForSeconds(triggerCoolTime);
        var minCoolDelay = minCoolTimeDelay;
        while (true)
        {
            if (coolTimeType == CoolTimeType.DynamicTime)
            {
                // ���ҵ� �ð� ������ ���� �ð� ���� ���� ( �ּ� �ð� + (���� �ð� ���� * �ִ� �ּ� �ð� ����) )
                delay = new WaitForSeconds( triggerMinCoolTime + (minCoolDelay / minCoolTimeDelay * (triggerCoolTime - triggerMinCoolTime)));
            }
            
            onClick?.Invoke();
            
            yield return delay;
            minCoolDelay -= triggerCoolTime; 
        }
    }
}
