using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    private float direction = 1f;

    void Start()
    {
        
    }

    void Update()
    {
        float speed = moveSpeed * Time.deltaTime * direction;
        Vector3 vector3 = new Vector3(speed, 0, 0);

        transform.Translate(vector3);
    }

    //�ʱ�ȭ�Լ�
    public void InstantiateBullet(float _direction)
    {
        direction = _direction;
        Vector3 vector3 = new Vector3(direction, 1, 1); //���� �ٲٱ�
        transform.localScale = vector3;
        Destroy(gameObject, 2f); //�ڱ� �ڽ� ����... this.gameObject (this ����)
    }
}
