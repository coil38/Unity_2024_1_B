using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public bool isDrag;             //�巹�� ������ �Ǵ��ϴ� (bool)
    public bool isUsed;             //��� �Ϸ� �Ǵ��ϴ� (bool)
    Rigidbody2D rigidbody2D;        //2D ��ü�� �ҷ��´�.

    public int index;               //���� ��ȣ�� �����.

    public float EndTime = 0.0f;                //���� �� �ð� üũ ����(float)
    public SpriteRenderer spriteRenderer;       //����� ��������Ʈ ���� ��ȯ ��Ű�� ���� ���� ����

    public GameManger gameManger;               //GameManager ���� ����

     void Awake()                                           //�����ϱ��� �ҽ� �ܰ迡������ ����
     {
        isUsed = false;                                     //��� �Ϸᰡ ���� ����(ó�� ���)
        rigidbody2D = GetComponent<Rigidbody2D>();          //��ü�� �����´�.
        rigidbody2D.simulated = false;                      //�����ɶ��� �ùķ����� ���� �ʴ´�.
        spriteRenderer = GetComponent<SpriteRenderer>();    //�ش� ������Ʈ�� ��������Ʈ ������ ���� 
     }
    
    void Start()
    {
        gameManger = GameObject.FindWithTag("GameManager").GetComponent<GameManger>();      //���� �޴��� ���´�.
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
    public void Used()
    {
        isDrag = false;                     //�巡�� ����
        isUsed = true;                      //����� �Ϸ�
        rigidbody2D.simulated = true;       //���� ���� ����
    }

    public void OnCollisionEnter2D(Collision2D collision)           //2D �浹�� �Ͼ ���
    {
        if (index >= 7)                         //�غ�� ������ �ִ� 7��
            return;
        if (collision.gameObject.tag == "Fruit")        //�浹 ��ü�� TAG �� Fruit �� ���
        {
            CircleObject temp = collision.gameObject.GetComponent<CircleObject>();  //�ӽ÷� Class temp�� �����ϰ� �浹ü�� Class(CircleObject)�� �޾ƿ´�.

            if(temp.index == index)     //���� ��ȣ�� ���� ���
            {
                if(gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())   //����Ƽ���� �����ϴ� ������ ID�� �޾ƿͼ� ID�� ū�ʿ��� ���� ���� ����
                {
                    if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())      //����Ƽ���� �����ϴ� ������ ID�� �޾ƿͼ� ID�� ū�ʿ��� ���� ���� ����
                    {
                        //GameManger ���� �����Լ� ȣ��
                        GameObject Temp = GameObject.FindWithTag("GameManager");
                        if (Temp != null)
                        {
                            Temp.gameObject.GetComponent<GameManger>().MergeObject(index, gameObject.transform.position);       //������ MerageObject �Լ��� �μ��� �Բ� ����
                        }
                        Destroy(temp.gameObject);                                               //�浹 ��ü �ı�
                        Destroy(gameObject);                                                    //�ڱ� �ڽ� �ı�
                    }
                }
            }
        }
    }
}
