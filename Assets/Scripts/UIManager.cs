using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private UIType uIType;
    public GameObject Top;
    public GameObject UIBG;
    public GameObject Canvas;
    public GameObject PanelList;
    public GameObject hit;

    public delegate void SetTypeDelegate(int type);
    private SetTypeDelegate setTypeDelegate = null;

    public UIType GetUIType()
    {
        return uIType;
    }

    void Awake()
    {
        GameManager.Instance.uIManager = this;
    }

    public void ButtonClick()
    {
        var buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        Debug.Log(buttonSelf.transform.GetChild(0).GetComponent<Text>().text.Split('：')[2]);
        if(GameManager.Instance.Coin>= int.Parse(buttonSelf.transform.GetChild(0).GetComponent<Text>().text.Split('：')[2]))
        {
            GameManager.Instance.Coin -= int.Parse(buttonSelf.transform.GetChild(0).GetComponent<Text>().text.Split('：')[2]);
            setTypeDelegate?.Invoke(int.Parse(buttonSelf.transform.parent.name));
            CloseUI();
        }
    }

    public void AddLevel()
    {
        
        var buttonSelf = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
        if (UIType.advertisement == uIType) 
        {
            if (GameManager.Instance.Coin >= (20 * hit.GetComponent<BaseClickObject>().Level)+80)
            {
                Debug.Log("升级花费：" + ((20 * hit.GetComponent<BaseClickObject>().Level) + 80));
                GameManager.Instance.Coin -= ((20 * hit.GetComponent<BaseClickObject>().Level) + 80);
                hit.GetComponent<BaseClickObject>().AddLevel();
                CloseUI();
            }
        }
        if (UIType.store == uIType)
        {
            if (GameManager.Instance.Coin >= (30 * hit.GetComponent<BaseClickObject>().Level) + 270)
            {
                Debug.Log("升级花费：" + ((30 * hit.GetComponent<BaseClickObject>().Level) + 270));
                GameManager.Instance.Coin -= ((30 * hit.GetComponent<BaseClickObject>().Level) + 270);
                hit.GetComponent<BaseClickObject>().AddLevel();
                CloseUI();
            }
        }

    }

    //关闭选择框
    public void CloseUI()
    {
        foreach(Transform game in PanelList.transform)
        {
            Destroy(game.gameObject);
        }
        Canvas.SetActive(false);
    }

    //显示选择框
    public void ShowUI(UIType uIType,GameObject hit,SetTypeDelegate setType)
    {
        this.hit = hit;
        this.uIType = uIType;
        setTypeDelegate = setType ;
        Top.GetComponent<Image>().sprite = Resources.Load("Image/" + uIType + "/top", typeof(Sprite)) as Sprite;
        UIBG.GetComponent<Image>().sprite = Resources.Load("Image/" + uIType + "/UIBG", typeof(Sprite)) as Sprite;

        TextAsset t = Resources.Load("Conf/UIConf", typeof(TextAsset)) as TextAsset;

        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(t.text);
        XmlNode node = xmlDocument.SelectSingleNode("items");
        XmlNodeList xnl = node.SelectNodes(uIType.ToString());

        foreach (XmlNode xmlNode in xnl)
        {
            GameObject perfabs = Instantiate(Resources.Load("Prefabs/item", typeof(GameObject))) as GameObject;
            perfabs.name = xmlNode.ChildNodes.Item(1).InnerText;
            perfabs.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(ButtonClick);
            perfabs.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = Resources.Load("Image/" + uIType + "/" + xmlNode.ChildNodes.Item(1).InnerText, typeof(Sprite)) as Sprite;
            

            perfabs.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "名字：" + xmlNode.ChildNodes.Item(0).InnerText + "\n 价格：" + xmlNode.ChildNodes.Item(2).InnerText + "\n";


            if (UIType.advertisement == uIType)
            {
                if (perfabs.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite.Equals(hit.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite))
                {
                    perfabs.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                    perfabs.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(AddLevel);
                    perfabs.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "名字：" + xmlNode.ChildNodes.Item(0).InnerText + "\n 价格：" + ((20 * hit.GetComponent<BaseClickObject>().Level) + 80) + "\n";
                }
            }
            else
            {
                if (perfabs.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite.Equals(hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite))
                {
                    perfabs.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                    perfabs.transform.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(AddLevel);
                    perfabs.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "名字：" + xmlNode.ChildNodes.Item(0).InnerText + "\n 价格：" + ((30 * hit.GetComponent<BaseClickObject>().Level) + 270) + "\n";
                }
            }

            perfabs.transform.SetParent(PanelList.transform,false);

        }
        Canvas.SetActive(true);

    }
}
