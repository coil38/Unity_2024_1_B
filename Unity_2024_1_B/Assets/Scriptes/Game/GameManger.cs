using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public GameObject CircleObject;         //���� ������ ������Ʈ
    public Transform GenTransform;          //������ ������ ��ġ ������Ʈ
    public float TimeCheck;                 //�ð��� üũ�ϱ� ���� (float) ��
    public bool isGen;                      //���� �Ϸ� üũ (bool) ��

    void Start()
    {
        GenObject();                        ///������ ���۵Ǿ����� �Լ��� ȣ���ؼ� �ʱ�ȭ ��Ų��.
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
