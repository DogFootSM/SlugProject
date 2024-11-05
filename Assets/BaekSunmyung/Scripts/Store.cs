using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;




public class Store : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [Header("Weapon")]
    [SerializeField] private Button heavyBtn;
    [SerializeField] private Button flameBtn;
    [SerializeField] private Button roketBtn;
    [SerializeField] private Button shotgunBtn;


    [Header("Buy Popup")]
    [SerializeField] private GameObject buyPopup;
    [SerializeField] private Button BuyBtn;
    [SerializeField] private TextMeshProUGUI curAttackText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private Image iconImage;


    //WeaponInfo �ڸ�
    private Test weaponInfoData;
    private PlayerDataModel playerDataModel;

    private bool[] weaponBools = new bool[4];

    private string itemName = "";
    private int itemPrice = 0;


    private void Awake()
    {
        heavyBtn.onClick.AddListener(HeavyUpgrade);
        flameBtn.onClick.AddListener(FlameUpgrade);
        roketBtn.onClick.AddListener(RoketUpgrade);
        shotgunBtn.onClick.AddListener(ShotGutUpgrade);
    }

    private void Start()
    {
        weaponInfoData = GetComponent<Test>();

    }


    private void HeavyUpgrade()
    {
        weaponBools[0] = weaponInfoData.a;

        if (weaponBools[0])
        {
            weaponInfoData.Heavy_Level++;
        }
        else
        {
            //���� ���� �˾� Ȱ��ȭ
            //���� ���� True
            buyPopup.SetActive(true);
            itemName = "���";
            curAttackText.text = playerDataModel.Attack.ToString();
            descText.text = "���� ���";
            itemPrice = 100;
        }
    }

    private void FlameUpgrade()
    {
        weaponBools[1] = weaponInfoData.b;

        if (weaponBools[1])
        {
            weaponInfoData.Flame_Level++;
        }
        else
        {
            buyPopup.SetActive(true);
            itemName = "�÷���";
            curAttackText.text = playerDataModel.Attack.ToString();
            descText.text = "���� �÷���";
            itemPrice = 150;

        }
    }

    private void RoketUpgrade()
    {
        weaponBools[2] = weaponInfoData.c;
        if (weaponBools[2])
        {
            weaponInfoData.Roket_Level++;
        }
        else
        {
            buyPopup.SetActive(true);
            itemName = "����";
            curAttackText.text = playerDataModel.Attack.ToString();
            descText.text = "���� ����";
            itemPrice = 200;
        }
    }

    private void ShotGutUpgrade()
    {
        weaponBools[3] = weaponInfoData.d;
        if (weaponBools[3])
        {
            weaponInfoData.Shotgun_Level++;
        }
        else
        {
            buyPopup.SetActive(true);
            itemName = "����";
            curAttackText.text = playerDataModel.Attack.ToString();
            descText.text = "���� ����";
            itemPrice = 250;
        }
    }


    private void ItemBuy()
    {
        playerDataModel.Money -= itemPrice;
        

        switch (itemName)
        {
            case "���":
                weaponInfoData.a = true;
                Color heavyColor = heavyBtn.image.GetComponent<Color>();
                heavyColor = new Color(1f, 1f, 1f, 1f);
                break;

            case "�÷���":
                weaponInfoData.b = true;
                Color flameColor = flameBtn.image.GetComponent<Color>();
                flameColor = new Color(1f, 1f, 1f, 1f);
                break;

            case "����":
                weaponInfoData.c = true;
                Color roketColor = roketBtn.image.GetComponent<Color>();
                roketColor = new Color(1f, 1f, 1f, 1f);
                break;

            case "����":
                weaponInfoData.d = true;
                Color shotgunColor = shotgunBtn.image.GetComponent<Color>();
                shotgunColor = new Color(1f, 1f, 1f, 1f);
                break; 
        }

        buyPopup.SetActive(false);
         
    }


}
