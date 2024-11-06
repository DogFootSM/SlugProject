using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;




public class Store : MonoBehaviour
{
    [Header("GR Ray")]
    private GraphicRaycaster ray;
    [SerializeField] private Canvas canvas;
    [SerializeField] private PointerEventData ped = new PointerEventData(EventSystem.current);
    private List<RaycastResult> results = new List<RaycastResult>();

    [Header("��ȭ ����")]
    [SerializeField] private TextMeshProUGUI enhanceNum;
    [SerializeField] private Image itemIconImageInfo;
    [SerializeField] private TextMeshProUGUI curAttackTextInfo;
    [SerializeField] private Button priceBtn;

    [Header("Buy Popup")]
    [SerializeField] private GameObject buyPopup;
    [SerializeField] private Button BuyBtn;
    [SerializeField] private TextMeshProUGUI curAttackTextBuy;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private Image itemIconImageBuy;
    [SerializeField] private List<ShopData> shopData = new List<ShopData>();
    private TextMeshProUGUI buyText;

    //WeaponInfo �ڸ�
    private WeaponInfoData infoData;
    private Test weaponInfoData;
    private PlayerDataModel playerDataModel;
    private GameManager gameManager;

    [Header("Store Button List")]
    [SerializeField] private List<Button> buttonList = new List<Button>();

    private string itemName = "";
    private int itemPrice = 0;

    private Color tureColor = new Color(1f, 1f, 1f, 1f);
    private ShopData curShopData;
    private int shopIndex = 0;
    private bool isSelect;



    private void Awake()
    {


        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].onClick.AddListener(Shop);
        }
        BuyBtn.onClick.AddListener(ItemBuy);
        priceBtn.onClick.AddListener(UpGrade);
    }

    private void OnEnable()
    {
        gameManager.IsOpenInventory = true;
    }

    private void OnDisable()
    {
        gameManager.IsOpenInventory = false;
        isSelect = false;
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        infoData = WeaponInfoData.Instance;
        weaponInfoData = Test.Instance;

        ray = canvas.GetComponent<GraphicRaycaster>();
        for (int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].image.sprite = shopData[i].IconImage;

            //���� ������ �������� Ȱ��ȭ ǥ��
            if (shopData[i].IsBuy)
            {
                buttonList[i].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
            }
        }

        buyText = BuyBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        //WeaponInfoData
        weaponInfoData = GetComponent<Test>();

        priceBtn.interactable = false;
    }

    private void Update()
    {
        ped.position = Input.mousePosition;
        results.Clear();
        ray.Raycast(ped, results);

    }

    private void Shop()
    {
        if (results.Count > 0)
        {
            string name = results[0].gameObject.name;
            string[] strIndex = Regex.Replace(name, @"[^0-9]", " ").Split(' ');
            shopIndex = Convert.ToInt32((strIndex[strIndex.Length - 1]));
        }

        curShopData = shopData[shopIndex];

        if (shopData[shopIndex].IsBuy)
        {
            ShowInfoEnhance();
        }
        else
        {
            ItemBuyPopup();
        }
    }

    /// <summary>
    /// Ÿ�� �� ������ ���׷��̵�
    /// </summary>
    private void UpGrade()
    {

        //��ȹ ����
        switch (curShopData.CurStoreType)
        {
            case StoreType.Weapon:
                Debug.Log("���� ��ȭ ����");
                curShopData.CurEnhance++;
                curShopData.IncAttack++;
                curShopData.EnhancePrice += 20;
                //PlayerDataModel.Attack = curShopData.Inattack;
                if(shopIndex == 0)
                {
                    infoData.Heavy_Level = curShopData.IncAttack;
                }
                else if(shopIndex == 1)
                {
                    infoData.Flame_Level = curShopData.IncAttack;
                }
                else if(shopIndex == 2)
                {
                    infoData.Roket_Level = curShopData.IncAttack;
                }
                else if(shopIndex == 3)
                {
                    infoData.Roket_Level = curShopData.IncAttack;
                }

                break;

            case StoreType.Partner:
                Debug.Log("��Ʈ�� ��ȭ ����");
                curShopData.CurEnhance++;
                curShopData.IncAttack++;
                curShopData.EnhancePrice += 20;
                //PlayerDataModel.Attack = curShopData.Inattack;
                break;

            case StoreType.Slug:
                Debug.Log("������ ��ȭ ����");
                curShopData.CurEnhance++;
                curShopData.IncAttack++;
                curShopData.EnhancePrice += 20;
                //PlayerDataModel.Attack = curShopData.Inattack;
                break;

            case StoreType.Skill:
                Debug.Log("��ų ��ȭ ����");
                curShopData.CurEnhance++;
                curShopData.IncAttack++;
                curShopData.EnhancePrice += 20;
                //PlayerDataModel.Attack = curShopData.Inattack;
                break;
        }

        InfoUpdate();
    }

    /// <summary>
    /// ������ ��ȭ �� ���� ������Ʈ
    /// </summary>
    private void InfoUpdate()
    {

        enhanceNum.text = "+" + curShopData.CurEnhance.ToString() + " " + curShopData.ItenName;
        //curAttackTextInfo.text = curShopData.IncAttack.ToString();
        curAttackTextInfo.text = infoData.weaponDamage.ToString();
        TextMeshProUGUI priceText = priceBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        priceText.text = curShopData.EnhancePrice.ToString();

        if (10 < curShopData.EnhancePrice)
        {
            priceBtn.interactable = false;
        }
        else
        {
            priceBtn.interactable = true;
        }


    }

    /// <summary>
    /// ������ ������ ��ȭ ���� 
    /// </summary>
    private void ShowInfoEnhance()
    {
        isSelect = !isSelect ? true : false; 
        if (isSelect)
        {
            enhanceNum.text = "+" + curShopData.CurEnhance.ToString() + " " + curShopData.ItenName;
            itemIconImageInfo.sprite = curShopData.IconImage;

            //PlayerModel.Attack Change
            //curAttackTextInfo.text = curShopData.IncAttack.ToString();
            curAttackTextInfo.text = infoData.weaponDamage.ToString();
            TextMeshProUGUI priceText = priceBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            priceText.text = curShopData.EnhancePrice.ToString();

            if (10 > curShopData.EnhancePrice)
            {
                priceBtn.interactable = true;
            }
        }
        else
        {
            enhanceNum.text = "";
            itemIconImageInfo.sprite = null;
            curAttackTextInfo.text = "";
            TextMeshProUGUI priceText = priceBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            priceText.text = "Price";
            
        }

        //�������� ���� ���� or ���� �ӴϺ��� �������� ��θ� ��ȣ�ۿ� �Ұ�
        if (!isSelect || 10 < curShopData.EnhancePrice)
        {
            priceBtn.interactable = false;
        }
        
    }


    /// <summary>
    /// ������ ���� �˾� ����
    /// </summary>
    private void ItemBuyPopup()
    {

        buyPopup.SetActive(true);
        curAttackTextBuy.text = curShopData.IncAttack.ToString();
        descText.text = curShopData.Desc;
        itemIconImageBuy.sprite = curShopData.IconImage;
        itemPrice = curShopData.Pirce;
        buyText.text = itemPrice.ToString() + " Buy";

        //PlayerDataModel.Money < itemPrice
        //Item Price > ShopData.ItemPrice
        if (3000 < itemPrice)
        {
            BuyBtn.interactable = false;
        }
        else
        {
            BuyBtn.interactable = true;
        }

    }

    /// <summary>
    /// ������ �̼��� ���¿��� ������ ���� ó��
    /// </summary>
    private void ItemBuy()
    {
        curShopData.IsBuy = true;
        buttonList[shopIndex].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        //playerDataModel.Money -= itemPrice;
        buyPopup.SetActive(false);

    }


}
