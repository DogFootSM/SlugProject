using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button heavyBtn;
    [SerializeField] private Button flameBtn;
    [SerializeField] private Button roketBtn;
    [SerializeField] private Button shotgunBtn;
    [SerializeField] private GameObject buyPopup;

    [SerializeField] private Button BuyBtn;

    private Test weaponInfoData;
    private GraphicRaycaster ray;
    private PointerEventData ped = new PointerEventData(EventSystem.current);
    private List<RaycastResult> results = new List<RaycastResult>();
    bool isHeavyBuy;
    bool isFlameBuy;
    bool isRoket;
    bool isShotgun;

    string itemName = "";
    string itemDesc = "";
    int itemPrice = 0;


    private void Awake()
    {
        heavyBtn.onClick.AddListener(HeavyUpgrade);
        flameBtn.onClick.AddListener(FlameUpgrade);
        roketBtn.onClick.AddListener(RoketUpgrade);
        shotgunBtn.onClick.AddListener(ShotGutUpgrade); 
    }

    private void Start()
    {
        ray = canvas.GetComponent<GraphicRaycaster>();
        weaponInfoData = GetComponent<Test>();


        isHeavyBuy = weaponInfoData.a;
        isFlameBuy = weaponInfoData.b;
        isRoket = weaponInfoData.c;
        isShotgun = weaponInfoData.d;
    }


    private void HeavyUpgrade()
    {
        if (isHeavyBuy)
        {
            weaponInfoData.Heavy_Level++;
        }
        else
        {
            //���� ���� �˾� Ȱ��ȭ
            //���� ���� True
            buyPopup.SetActive(true);
            itemName = "���";
            itemDesc = "���� ���";
            itemPrice = 100;
        }
    }

    private void FlameUpgrade()
    {
        if (isFlameBuy)
        {
            weaponInfoData.Flame_Level++;
        }
        else
        {
            buyPopup.SetActive(true);
            itemName = "���";
            itemDesc = "���� ���";
            itemPrice = 100;
        }
    }

    private void RoketUpgrade()
    {
        if (isRoket)
        {
            weaponInfoData.Roket_Level++;
        }
        else
        {
            buyPopup.SetActive(true);
            BuyBtn.onClick?.Invoke();
        }
    }

    private void ShotGutUpgrade()
    {
        if (isShotgun)
        {
            weaponInfoData.Shotgun_Level++;
        }
        else
        {
            buyPopup.SetActive(true);
        }
    }

    private void ItemBuy()
    {
         

    }


}
