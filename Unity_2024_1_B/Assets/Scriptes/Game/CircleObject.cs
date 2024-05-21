using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleObject : MonoBehaviour
{
    public bool isDrag;             //드레그 중인지 판단하는 (bool)
    public bool isUsed;             //사용 완료 판단하는 (bool)
    Rigidbody2D rigidbody2D;        //2D 강체를 불러온다.

    public int index;               //과일 번호를 만든다.

    public float EndTime = 0.0f;                //종료 선 시간 체크 변수(float)
    public SpriteRenderer spriteRenderer;       //종료시 스프라이트 색을 변환 시키기 위해 접근 선언

    public GameManger gameManger;               //GameManager 접근 선언

     void Awake()                                           //시작하기전 소스 단계에서부터 셋팅
     {
        isUsed = false;                                     //사용 완료가 되지 않음(처음 사용)
        rigidbody2D = GetComponent<Rigidbody2D>();          //강체를 가져온다.
        rigidbody2D.simulated = false;                      //생성될때는 시뮬레이팅 되지 않는다.
        spriteRenderer = GetComponent<SpriteRenderer>();    //해당 오브젝트의 스프라이트 랜더러 접근 
     }
    
    void Start()
    {
        gameManger = GameObject.FindWithTag("GameManager").GetComponent<GameManger>();      //게임 메니저 얻어온다.
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
    public void Used()
    {
        isDrag = false;                     //드래그 종료
        isUsed = true;                      //사용이 완료
        rigidbody2D.simulated = true;       //물리 현상 시작
    }

    public void OnCollisionEnter2D(Collision2D collision)           //2D 충돌이 일어날 경우
    {
        if (index >= 7)                         //준비된 과일이 최대 7개
            return;
        if (collision.gameObject.tag == "Fruit")        //충돌 물체의 TAG 가 Fruit 일 경우
        {
            CircleObject temp = collision.gameObject.GetComponent<CircleObject>();  //임시로 Class temp를 선언하고 충돌체의 Class(CircleObject)를 받아온다.

            if(temp.index == index)     //과일 번호가 같은 경우
            {
                if(gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())   //유니티에서 지원하는 고유의 ID를 받아와서 ID가 큰쪽에서 다음 과일 생성
                {
                    if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())      //유니티에서 지원하는 고유의 ID를 받아와서 ID가 큰쪽에서 다음 과일 생성
                    {
                        //GameManger 에서 생성함수 호출
                        GameObject Temp = GameObject.FindWithTag("GameManager");
                        if (Temp != null)
                        {
                            Temp.gameObject.GetComponent<GameManger>().MergeObject(index, gameObject.transform.position);       //생성된 MerageObject 함수에 인수와 함께 전당
                        }
                        Destroy(temp.gameObject);                                               //충돌 물체 파괴
                        Destroy(gameObject);                                                    //자기 자신 파괴
                    }
                }
            }
        }
    }
}
