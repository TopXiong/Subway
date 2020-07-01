using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    bool delay = true;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game Start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) && delay)
        {     
            Vector3 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(v.x, v.y), new Vector2(v.x, v.y), 0.1f); 
            if (Physics2D.Raycast(new Vector2(v.x, v.y), new Vector2(v.x, v.y), 0.1f) && !GameManager.Instance.uIManager.Canvas.activeInHierarchy)
            {
                try
                {
                    Debug.Log(hit.collider.gameObject);
                    hit.collider.gameObject.GetComponent<BaseClickObject>().OnClick();
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
        }
        delay = !GameManager.Instance.uIManager.Canvas.activeInHierarchy;
    }
}
