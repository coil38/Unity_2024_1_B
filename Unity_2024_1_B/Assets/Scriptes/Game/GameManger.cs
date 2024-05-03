using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public GameObject CircleObject;         //과일 프리맵 오브젝트
    public Transform GenTransform;          //과일이 생성될 위치 오브젝트
    public float TimeCheck;                 //시간을 체크하기 위한 (float) 값
    public bool isGen;                      //생성 완료 체크 (bool) 값

    void Start()
    {
        GenObject();                        ///게임이 시작되었을때 함수를 호출해서 초기화 시킨다.
    }

    void Update()
    {
        if(!isGen)      //if(isGen == false)
        {
            TimeCheck -= Time.deltaTime;
            if(TimeCheck <= 0)
            {
                GameObject Temp = Instantiate(CircleObject);
                Temp.transform.position = GenTransform.position;
                isGen = true;
            }
        }
    }

    public void GenObject()
    {
        isGen = false;
        TimeCheck = 1.0f;
    }
}
