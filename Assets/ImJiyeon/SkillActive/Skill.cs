using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    public bool isActived;
    public string SetCoolTimeCoroutineName = "SetCoolTime";


    private void Start()
    {
        isActived = true;
    }

    // ��ų�� �ߵ� �Լ�
    public abstract void Activate();


    // ��ų�� ��Ÿ���� �»��ϴ� �Լ� (�ش� �ڵ�� �ӽ�)
    public IEnumerator SetCurrentCooltime(float Cool, Image LookCoolTime, Button skillButton)
    {
        if (isActived == false)
        {
            LookCoolTime.gameObject.SetActive(true);
            skillButton.interactable = false;

            Debug.Log("��Ÿ�� ����");
            float MaxCool = Cool;

            while (Cool > 0.1f)
            {
                Cool -= Time.deltaTime;
                LookCoolTime.fillAmount = (Cool / MaxCool);

                yield return new WaitForFixedUpdate();
            }

            LookCoolTime.gameObject.SetActive(false);
            skillButton.interactable = true;

            Debug.Log("��Ÿ�� ����");
            isActived = true;
        }
    }
}
