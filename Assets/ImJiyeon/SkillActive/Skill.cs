using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    private GameObject player;
    protected PlayerDataModel playerDataModel;
    protected GameManager gameManager;

    public Image LookCoolTime;
    public bool isActived;

    [Header("Stat")]
    public float SkillAttack;
    public float CoolTime;


    private void Start()
    {
        // �ӽ� �÷��̾� ������ �� ���� �ڵ�, �ʿ� ���� �� ���� ����
        player = GameObject.FindGameObjectWithTag("Player");
        playerDataModel = player.GetComponent<PlayerDataModel>();

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
                //if (GameManager.Instance.IsOpenInventory)
                //{
                //    yield return new WaitUntil(() => GameManager.Instance.IsOpenInventory == true);
                //}
                //else if (gameManager.StageInstance.CoolTimeReset)
                //{
                //    yield break;
                //}
                //else

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
