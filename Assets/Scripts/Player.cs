using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 점프 관련 변수
    [SerializeField]
    private float jumpForce = 300f;

    private bool isJump = false;

    // 총알 발사 관련 변수
    [SerializeField]
    private GameObject bulletPos = null;

    // 총알 프리팹
    [SerializeField]
    private GameObject bulletObj = null;

    [SerializeField] //private로 선언해도 컴포넌트 창에서 보이게 함
    private float moveSpeed = 3f;

    void Update()
    {
        PlayerMove();

        if(Input.GetButtonDown("Jump"))
        {
            PlayerJump();
        }

        if(Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    private void PlayerMove()
    {
        float h = Input.GetAxis("Horizontal");
        float playerSpeed = h * moveSpeed * Time.deltaTime;

        Vector3 vector3 = new Vector3();
        vector3.x = playerSpeed;

        transform.Translate(vector3);

        if (h < 0)
        {
            GetComponent<Animator>().SetBool("Walk", true);
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (h == 0)
        {
            GetComponent<Animator>().SetBool("Walk", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walk", true);
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void PlayerJump()
    {
        // 점프 상태가 되어 있지 않을 때만 점프하도록 함
        if(isJump == false)
        {
            // 애니메이션 처리부
            GetComponent<Animator>().SetBool("Walk", false);
            GetComponent<Animator>().SetBool("Jump", true);

            // 점프량만큼 Add Force
            Vector2 vector2 = new Vector2(0, jumpForce);
            GetComponent<Rigidbody2D>().AddForce(vector2);
            isJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌체의 콜라이더가 플랫폼 태그라면
        if (collision.collider.tag == "Platform")
        {
            GetComponent<Animator>().SetBool("Jump", false);
            isJump = false;
        }
    }

    //총알 발사
    private void Fire()
    {
        float direction = transform.localScale.x;
        Quaternion quaternion = new Quaternion(0, 0, 0, 0);
        //생성하고, 총알이 발사되도록 Bullet 컴포넌트(스크립트)에서 초기화 함수 가져옴
        Instantiate(bulletObj, bulletPos.transform.position, quaternion).GetComponent<Bullet>().InstantiateBullet(direction);
    }
}
