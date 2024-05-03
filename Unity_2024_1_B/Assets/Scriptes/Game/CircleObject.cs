using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public bool isDrag;             //�巹�� ������ �Ǵ��ϴ� (bool)
    public bool isUsed;             //��� �Ϸ� �Ǵ��ϴ� (bool)
    Rigidbody2D rigidbody2D;        //2D ��ü�� �ҷ��´�.

    void Start()
    {
        isUsed = false;                               //��� �Ϸᰡ ���� ����(ó�� ���)
        rigidbody2D = GetComponent<Rigidbody2D>();  //��ü�� �����´�
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
        isDrag = true;                      //�巡�� ����(true)
        rigidbody2D.simulated = false;      //�巡�� �߿��� ���� ������ �Ͼ�� ���� ���� ���ؼ� (false)
    }

    void Drop()
    {
        isDrag = false;                     //�巡�� ����
        isUsed = true;                      //����� �Ϸ�
        rigidbody2D.simulated = true;       //���� ���� ����

        GameObject Temp = GameObject.FindWithTag("GameManager");
        if(Temp != null)
        {
            Temp.gameObject.GetComponent<GameManger>().GenObject();
        }
    }
}
