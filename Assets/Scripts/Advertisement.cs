using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Advertisement : BaseClickObject
{
    public override int Profit { get { return ((Level-1) * 2 + 5) * GameManager.Instance.Electric; } }

    public override void SetType(int type)
    {
        base.SetType(type);
        TextAsset t = Resources.Load("Conf/UIConf", typeof(TextAsset)) as TextAsset;
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.LoadXml(t.text);
        XmlNode node = xmlDocument.SelectSingleNode("items");
        XmlNodeList xnl = node.SelectNodes(UIType.advertisement.ToString());
        XmlNode xmlNode = xnl.Item(type - 1);
        gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load("Image/" + UIType.advertisement.ToString() + "/" + xmlNode.ChildNodes.Item(1).InnerText, typeof(Sprite)) as Sprite;
    }

    public override void OnClick()
    {
        if (money.activeSelf)
        {
            GameManager.Instance.Coin += Available;
            Available = 0;
            money.SetActive(false);
            return;
        }
        GameManager.Instance.uIManager.ShowUI(UIType.advertisement, gameObject,SetType);
    }

    // Start is called before the first frame update
    void Start()
    {
        money = transform.GetChild(1).gameObject;
        GameManager.Instance.lists.Add(gameObject);
        Level = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
