using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject rayPos = null;
    [SerializeField]
    private float moveSpeed = 3f;
    [SerializeField]
    private int HP = 3;

    private bool moveRight = true;

    private bool isDead = false;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        CheckRay();
    }

    private void CheckRay()
    {
        // 죽지 않았다면 체크한다.
        if (isDead == false)
        {
            // 레이어 마스크
            LayerMask layerMask = new LayerMask();
            layerMask = LayerMask.GetMask("Platform");

            // 레이캐스트
            RaycastHit2D ray = Physics2D.Raycast(rayPos.transform.position, new Vector2(0, -1), 1.1f, layerMask.value);

            Debug.DrawRay(rayPos.transform.position, new Vector3(0, -1, 0), Color.red);

            // 히트가 되지 않으면
            if(ray == false)
            {
                Debug.Log("반대방향으로");
                if (moveRight == true)
                {
                    moveRight = false;
                }
                else
                {
                    moveRight = true;
                }
            }

            Move();
        }
    }

    private void Move()
    {
        float direction = 0;
        
        if (moveRight)
        {
            direction = 1f;
        }
        else
        {
            direction = -1f;
        }

        // 플립 (좌우전환)
        Vector3 vector3 = new Vector3(direction, 1, 1);
        transform.localScale = vector3;

        // 이동
        float speed = moveSpeed * Time.deltaTime * direction;
        vector3 = new Vector3(speed, 0, 0);
        transform.Translate(vector3);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool check = collision.CompareTag("Bullet");
        if (check)
        {
            HP = HP - 1;

            if (HP < 1)
            {
                GetComponent<Animator>().SetBool("Death", true);
                isDead = true;
                Destroy(this.gameObject, 1);
            }
        }
    }
}
