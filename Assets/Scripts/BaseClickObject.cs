using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public abstract class BaseClickObject : MonoBehaviour
{
    /// <summary>
    /// 可用收益
    /// </summary>
    [SerializeField]
    protected int available;

    protected GameObject money;

    public int Available
    {
        get { return available; }
        protected set { available = value; }
    }

    public void AddAvailable(int value)
    {
        available += value;
        if (available > 500)
            money.SetActive(true);
    }

    [SerializeField]
    protected int level = 1;
    /// <summary>
    /// 每5s的收益
    /// </summary>
    public virtual int Profit { get { return 0; } }
    /// <summary>
    /// 店铺或广告的类型
    /// </summary>
    [SerializeField]
    protected int type;

    public int Type
    {
        get { return type; }
    }

    public int Level { get { return level; } protected set { level = value; } }

    public virtual void AddLevel()
    {
        level++;
        GameManager.Instance.GetMyProfitCash();
    }

    public virtual void SetType(int type)
    {
        this.type = type;
        GameManager.Instance.GetMyProfitCash();
    }
    public abstract void OnClick();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override String ToString()
    {
        return gameObject.name;
    }
}
