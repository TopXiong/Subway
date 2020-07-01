using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public UIManager uIManager;
    private static GameManager _instance;
    public List<GameObject> lists = new List<GameObject>();
    public float timer = 5f;
    public GameObject[] nums;

    private Text coinNum;
    private Text electricNum;
    private Text cashNum;

    [SerializeField] 
    private int coin;
    [SerializeField] 
    private int electric;
    [SerializeField] 
    private int cash;

    public int Coin
    {
        get { return coin; }
        set
        {
            StartCoroutine(NumChange(coinNum,value - coin));
            coin = value;
        }
    }

    public int Electric
    {
        get { return electric; }
        set
        {
            StartCoroutine(NumChange(electricNum, value - electric));
            electric = value;
        }
    }

    public int Cash
    {
        get { return cash; }
        set
        {
            StartCoroutine(NumChange(cashNum, value - cash));
            cash = value;
        }
    }

    private IEnumerator NumChange(Text text,int change)
    {
        int changeCount = 100;
        if (change < changeCount)
        {
            for (int i = 0; i < change; i++)
            {
                text.text = (int.Parse(text.text) + 1).ToString();
                yield return null;
            }
            yield return null;
        }
        for (int i = 0;i<changeCount;i++)
        {
            if(i!=changeCount-1)
                text.text = (int.Parse(text.text) + change/changeCount).ToString();
            else
                text.text = (int.Parse(text.text) + change - i*(change / changeCount)).ToString();
            yield return null;
        }
    }

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                //则创建一个
                _instance = GameObject.Find("bg").GetComponent<GameManager>();
            //返回这个实例
            return _instance;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        coinNum = nums[0].GetComponent<Text>();
        electricNum = nums[1].GetComponent<Text>();
        cashNum = nums[2].GetComponent<Text>();
        StartCoroutine(MoneyUpdate());
        Coin = 1000;
        Electric = 10;
        GetMyProfitCash();
    }

    public void GetMyProfitCash()
    {
        int value = 3;
        foreach (GameObject g in lists)
        {
            BaseClickObject baseClick = g.GetComponent<BaseClickObject>();
            if (baseClick.Type != 0)
            {
                value += baseClick.Profit;
            }
        }
        Cash += value;
    }

    public IEnumerator MoneyUpdate()
    {
        while (true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 5.0f;
                Debug.Log("5s过去了");
                for (int i = 0; i < lists.Count; i++)
                {
                    BaseClickObject baseClick = lists[i].GetComponent<BaseClickObject>();
                    if (baseClick.Type != 0)
                    {
                        baseClick.AddAvailable(baseClick.Profit);
                        Debug.Log(baseClick.name + " 增加 " + baseClick.Profit);
                    }
                }
            }            
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
