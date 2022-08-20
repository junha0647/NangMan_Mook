using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("이동속도")][SerializeField] private float Speed;
    [Header("점프력")][SerializeField] private float JumpForce;
    [Header("체력")] public int HealthPoint;

    [Header("HP 이미지 오브젝트 넣기")][SerializeField] private GameObject[] UIhealth;

    private Rigidbody2D rigid;
    private Animator anim;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!UIManager.isDraw)
        {
            Move();
            Jump();
        }
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // === 이동 애니메이션 === //
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }

        // === 이동 시 좌우 반전 === //
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // === 이동속도 제한 === //
        if (rigid.velocity.x > Speed)
        {
            rigid.velocity = new Vector2(Speed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < Speed * (-1))
        {
            rigid.velocity = new Vector2(Speed * (-1), rigid.velocity.y);
        }

        // === 이동 시 미끄럼 방지 === //
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.01f, rigid.velocity.y);
        }

        // === 경사면 미끄럼 방지 === //
        if (h == 0)
        {
            rigid.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
            
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("isJump"))
        {
            rigid.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }

        if (rigid.velocity.y <= 0)
        {
            Vector2 frontVec = new Vector2(rigid.position.x + rigid.velocity.x * 0.03f, rigid.position.y);
            Debug.DrawRay(frontVec, Vector3.down, new Color(1, 0, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2.5f, LayerMask.GetMask("Platform", "Line"));
            
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.5f)
                {
                    anim.SetBool("isJump", false);
                }
            }
            // 여기서 주의할 점은 rayHit의 2f와 distance의 범위(1.2f)를 잘 조절해야 함.
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            UIhealth[HealthPoint - 1].SetActive(false);
            if (gameObject.layer == 8)
            {
                OnDamaged();
                int dirc = transform.position.x - collision.transform.position.x > 0 ? 1 : -1;
                rigid.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse);
            }
        }
    }

    private void OnDamaged()
    {
        --HealthPoint;
        gameObject.layer = 9;
        anim.SetBool("isDamaged", true);

        if (HealthPoint > 0)
        {
            Invoke("OffDamaged", 0.5f);
        }
        else if(HealthPoint <= 0)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            GameOver();
        }
    }

    private void OffDamaged()
    {
        gameObject.layer = 8;
        anim.SetBool("isDamaged", false);
    }

    private void GameOver()
    {
        anim.SetTrigger("isGameOver");
    }
}