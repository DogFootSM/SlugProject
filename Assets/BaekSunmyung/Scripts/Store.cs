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
    private Test weaponInfoData;
    private PlayerDataModel playerDataModel;
    
    [Header("Store Button List")]
    [SerializeField] private List<Button> buttonList = new List<Button>();
    private bool[] weaponBools;

    private string itemName = "";
    private int itemPrice = 0;

    private Color tureColor = new Color(1f, 1f, 1f, 1f);
    private ShopData curShopData;
    private int shopIndex = 0;

    private void Awake()
    {

        //weaponInfodATA = weaponinfoData.Instace
        weaponInfoData = Test.Instance;

        heavyBtn.onClick.AddListener(Heavy);
        flameBtn.onClick.AddListener(Flame);
        roketBtn.onClick.AddListener(Roket);
        shotgunBtn.onClick.AddListener(ShotGun);
        BuyBtn.onClick.AddListener(ItemBuy);
        priceBtn.onClick.AddListener(UpGrade);
    }

    private void Start()
    { 
  
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
        weaponInfoData = GetComponent<Test>();
        playerDataModel = FindObjectOfType<PlayerDataModel>();
    }

    private void Update()
    {
        curShopData = shopData[shopIndex];
    }

    private void Heavy()
    {
        shopIndex = 0;

        if (shopData[shopIndex].IsBuy)
        {
            ShowInfoEnhance(shopData[shopIndex]);
        }
        else
        {
            ItemBuyPopup(shopData[shopIndex]);
        }
    }
     
    private void Flame()
    {
        shopIndex = 1;

        if (shopData[shopIndex].IsBuy)
        {
            ShowInfoEnhance(shopData[shopIndex]);
        }
        else
        {
            ItemBuyPopup(shopData[shopIndex]);
        }
    }

    private void Roket()
    {
        shopIndex = 2;

        if (shopData[shopIndex].IsBuy)
        {
            ShowInfoEnhance(shopData[shopIndex]);
        }
        else
        {
            ItemBuyPopup(shopData[shopIndex]);
        }
    }

    private void ShotGun()
    {
        shopIndex = 3;


        if (shopData[shopIndex].IsBuy)
        {
            ShowInfoEnhance(shopData[shopIndex]);
        }
        else
        {
            ItemBuyPopup(shopData[shopIndex]);
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

                break;

            case StoreType.Partner:

                break;

            case StoreType.Slug:

                break;

            case StoreType.Skill:

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
        curAttackTextInfo.text = curShopData.IncAttack.ToString();
        TextMeshProUGUI priceText = priceBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        priceText.text = curShopData.EnhancePrice.ToString();
    }

    /// <summary>
    /// ������ ������ ��ȭ ���� 
    /// </summary>
    /// <param name="shopData"></param>
    private void ShowInfoEnhance(ShopData shopData)
    {
        curShopData = shopData; 
        enhanceNum.text = "+" + shopData.CurEnhance.ToString() + " " + shopData.ItenName;
        itemIconImageInfo.sprite = shopData.IconImage;

        //PlayerModel.Attack Change
        curAttackTextInfo.text = shopData.IncAttack.ToString();
        TextMeshProUGUI priceText = priceBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        priceText.text = shopData.EnhancePrice.ToString();
    }


    /// <summary>
    /// ������ ���� �˾� ����
    /// </summary>
    /// <param name="shopData"></param>
    private void ItemBuyPopup(ShopData shopData)
    {
         
        buyPopup.SetActive(true);
        itemName = shopData.ItenName;
        curAttackTextBuy.text = shopData.IncAttack.ToString();
        descText.text = shopData.Desc;
        itemIconImageBuy.sprite = shopData.IconImage;
        itemPrice = shopData.Pirce;
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
        //playerDataModel.Money -= itemPrice;
 
        //������ ������ Ȯ�� �� ������ Ȱ��ȭ ǥ��
        //������ ���� ���� ��ȯ
        switch (itemName)
        {
            case "Heavy":
                curShopData.IsBuy = true;
                buttonList[0].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                break;

            case "Flame":
                curShopData.IsBuy = true;
                buttonList[1].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                break;

            case "Roket":
                curShopData.IsBuy = true;
                buttonList[2].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                break;

            case "ShotGun":
                curShopData.IsBuy = true;
                buttonList[3].GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                break; 
        }

        buyPopup.SetActive(false);
         
    }


}
