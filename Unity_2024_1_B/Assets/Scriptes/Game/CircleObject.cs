using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public bool isDrag;             //드레그 중인지 판단하는 (bool)
    public bool isUsed;             //사용 완료 판단하는 (bool)
    Rigidbody2D rigidbody2D;        //2D 강체를 불러온다.

    void Start()
    {
        isUsed = false;                               //사용 완료가 되지 않음(처음 사용)
        rigidbody2D = GetComponent<Rigidbody2D>();  //강체를 가져온다
    }

    void Update()
    {
        if (isUsed) return;

        if (isDrag)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float leftBorder = -4.0f + transform.localScale.x / 2.0f;
            float rightBorder = 4.0f - transform.localScale.x / 2.0f;

            if (mousePos.x < leftBorder) mousePos.x = leftBorder;
            if (mousePos.x > rightBorder) mousePos.x = rightBorder;

            mousePos.y = 8;
            mousePos.z = 0;

            transform.position = Vector3.Lerp(transform.position, mousePos, 0.2f);

        }

        if (Input.GetMouseButtonDown(0)) Drag();
        if (Input.GetMouseButtonUp(0)) Drop();
    }

    void Drag()
    {
        isDrag = true;                      //드래그 시작(true)
        rigidbody2D.simulated = false;      //드래그 중에는 물리 현상이 일어나는 것을 막기 위해서 (false)
    }

    void Drop()
    {
        isDrag = false;                     //드래그 종료
        isUsed = true;                      //사용이 완료
        rigidbody2D.simulated = true;       //물리 현상 시작

        GameObject Temp = GameObject.FindWithTag("GameManager");
        if(Temp != null)
        {
            Temp.gameObject.GetComponent<GameManger>().GenObject();
        }
    }
}
