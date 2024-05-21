using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    public GameObject[] CircleObject;         //���� ������ ������Ʈ
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
            TimeCheck -= Time.deltaTime;            //�� �����Ӹ��� ������ �ð��� ���ش�.
            if(TimeCheck <= 0)                      //�ش� �� �ð��� ������ ��� (1�� -> 0�ʰ� �Ǿ��� ���)
            {
                int RandNumber = Random.Range(0, 3);                    //0 ~ 2 ���� �� ���� ���ڸ� ����
                GameObject Temp = Instantiate(CircleObject[RandNumber]);         //���������� ������Ʈ�� ���� Ű���ش�. (InsTantiate)
                Temp.transform.position = GenTransform.position;        //������ ��ġ�� �̵� ��Ų��.
                isGen = true;                                           //Gen�� �Ǿ��ٰ� true�� bool ���� �����Ѵ�.
            }
        }
    }

    public void GenObject()
    {
        isGen = false;                          //�ʱ�ȭ : isGen �� False (���� ���� �ʾҴ�)
        TimeCheck = 1.0f;                       //1���� ���� �������� ���� ��Ű�� ���� �ʱ�ȭ
    }

    public void MergeObject(int index, Vector3 position)        //Merge �Լ��� ���Ϲ�ȣ(int) �� ���� ��ġ��(Vector3)�� ���� �޴´�.
    {
        GameObject Temp = Instantiate(CircleObject[index]);     //index�� �״�� ����. (0 ���� �迭�� ���۵����� index ���� 1�� �־
        Temp.transform.position = position;                     //��ġ�� ���� ���� ������ ���
        Temp.GetComponent<CircleObject>().Used();               //������ Used �Լ� ���
    }
}
