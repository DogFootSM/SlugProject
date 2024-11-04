using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    private GameManager gameManager;

    public bool isActived;
    public string SetCoolTimeCoroutineName = "SetCoolTime";


    private void Start()
    {
        gameManager = GameManager.Instance.GetComponent<GameManager>();
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
            //while (gameManager.StageInstance.CoolTimeReset)
            //{
            //    yield return null;
            //}

            LookCoolTime.gameObject.SetActive(false);
            skillButton.interactable = true;

            Debug.Log("��Ÿ�� ����");
            isActived = true;
        }
    }
}
