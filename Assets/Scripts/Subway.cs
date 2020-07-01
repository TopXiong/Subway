using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Subway : BaseClickObject
{
    public Vector3 InitPostion;
    public Vector3 Direction = new Vector3(+25f, -13.6f, 0);
    public float Speed = 0f;
    public bool IsLeave = true;
    [SerializeField]
    private bool IsMove = false;
    [SerializeField]
    private float interval = 3f;
    [SerializeField]
    private float timer;

    public void Leave()
    {
        Speed = 0f;
        StartCoroutine(Run(0.8f));
        StartCoroutine(Wait());
        
    }

    public void Entry()
    {
        Speed = 1.7f;
        transform.position = InitPostion - Direction*100;
        StartCoroutine(Run(0f));
    }

    private IEnumerator Run(float EndSpeed)
    {
        Debug.Log(transform.position.x);
        IsMove = true;
        while (transform.position.x <= 15)
        {
            Speed = Mathf.Lerp(Speed, EndSpeed, Time.deltaTime);
            transform.position = transform.position + Speed * Direction;
            if (!IsLeave && InitPostion.x - transform.position.x<0.4 || Speed < 0.01f)
            {
                Speed = 0f;
                IsLeave = true;
                money.SetActive(true);
                IsMove = false;
                yield break;
            }
            yield return null;
        }
        IsMove = false;
        IsLeave = false;
    }

    public override void OnClick()
    {
        if (IsMove)
            return;
        GameManager.Instance.Coin += (30 * (2 * Level - 1)) /10;
        GameManager.Instance.Electric += 3 * Level;
        money.SetActive(false);
        Leave();
    }

    private IEnumerator Wait()
    {
        while (true)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = interval;
                Entry();
                yield break;
            }
            yield return null;
        }
        
    }

    void Start()
    {
        InitPostion = transform.position;
        Direction = Direction * 0.01f;
        money = transform.GetChild(0).gameObject;
        timer = interval;
        IsLeave = true;
        IsMove = false;
        Level = 1;
    }

}