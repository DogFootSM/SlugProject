using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillActive_Thr : MonoBehaviour
{
    [SerializeField] Image LookCoolTime;
    [SerializeField] float CoolTime;


    public void SkillThr()
    {
        // ��ų �ڵ� �ۼ� ����


        if (LookCoolTime.gameObject.activeSelf == false)
        {
            Debug.Log("����° ��ų ����");

            StartCoroutine(SkillThrCoolTime(CoolTime));
        }
    }


    IEnumerator SkillThrCoolTime(float Cool)
    {
        LookCoolTime.gameObject.SetActive(true);
        gameObject.GetComponent<Button>().interactable = false;

        Debug.Log("��Ÿ�� ����");
        float MaxCool = Cool;

        while (Cool > 0.1f)
        {
            Cool -= Time.deltaTime;
            LookCoolTime.fillAmount = (Cool / MaxCool);

            yield return new WaitForFixedUpdate();
        }

        LookCoolTime.gameObject.SetActive(false);
        gameObject.GetComponent<Button>().interactable = true;

        Debug.Log("��Ÿ�� ����");
    }
}
