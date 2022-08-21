using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("�̵��ӵ�")][SerializeField] private float Speed;
    [Header("������")][SerializeField] private float JumpForce;
    [Header("ü��")] public int HealthPoint;

    [Header("HP �̹��� ������Ʈ �ֱ�")][SerializeField] private GameObject[] UIhealth;

    [Header("UI ������Ʈ �ֱ�")][SerializeField] private SoundManager soundManager;

    private Rigidbody2D rigid;
    private Animator anim;
    private Transform Tr;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Tr = GetComponent<Transform>();
        Spawn();
    }

    public bool isGameOver = false;
    private void Update()
    {
        if(!UIManager.isDraw && !isGameOver)
        {
            Move();
            Jump();
        }
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // === �̵� �ִϸ��̼� === //
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }

        // === �̵� �� �¿� ���� === //
        if (Input.GetAxis("Horizontal") < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (Input.GetAxis("Horizontal") != 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // === �̵��ӵ� ���� === //
        if (rigid.velocity.x > Speed)
        {
            rigid.velocity = new Vector2(Speed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < Speed * (-1))
        {
            rigid.velocity = new Vector2(Speed * (-1), rigid.velocity.y);
        }

        // === �̵� �� �̲��� ���� === //
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.01f, rigid.velocity.y);
        }

        // === ���� �̲��� ���� === //
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
            soundManager.PlaySound("JUMP");
            rigid.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }

        if (rigid.velocity.y <= 0)
        {
            Vector2 frontVec = new Vector2(rigid.position.x + rigid.velocity.x * 0.025f, rigid.position.y);
            Debug.DrawRay(frontVec, Vector3.down, new Color(1, 0, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2.5f, LayerMask.GetMask("Platform", "Line"));
            
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 1.5f)
                {
                    anim.SetBool("isJump", false);
                }
            }
            // ���⼭ ������ ���� rayHit�� 2f�� distance�� ����(1.2f)�� �� �����ؾ� ��.
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spike")
        {
            soundManager.PlaySound("DAMAGED");
            UIhealth[HealthPoint - 1].SetActive(false);
            if (gameObject.layer == 8)
            {
                OnDamaged();
                int dirc = transform.position.x - collision.transform.position.x > 0 ? 1 : -1;
                rigid.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse);
            }
        }

        if(collision.gameObject.tag == "DeathZone")
        {
            UIhealth[0].SetActive(false);
            UIhealth[1].SetActive(false);
            UIhealth[2].SetActive(false);
            gameObject.layer = 9;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Mook")
        {
            Destroy(collision.gameObject);
            SceneManager.LoadScene("04. ClearScene");
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
        isGameOver = true;
        anim.SetTrigger("isGameOver");
    }

    private void Spawn()
    {
        DataController.Instance.LoadGameData();

        string[] tmpPosArray = DataController.Instance.gameData.Pos.Split('/');
        Vector2 TmpPos = new Vector2(float.Parse(tmpPosArray[0]), float.Parse(tmpPosArray[1]));

        if (DataController.Instance.gameData.isClear5)
        {
            Tr.position = TmpPos;
            DataController.Instance.gameData.isClear4 = false;
            DataController.Instance.gameData.isClear3 = false;
            DataController.Instance.gameData.isClear2 = false;
        }
        else if (DataController.Instance.gameData.isClear4)
        {
            Tr.position = TmpPos;
            DataController.Instance.gameData.isClear3 = false;
            DataController.Instance.gameData.isClear2 = false;
        }
        else if (DataController.Instance.gameData.isClear3)
        {
            Tr.position = TmpPos;
            DataController.Instance.gameData.isClear2 = false;
        }
        else if (DataController.Instance.gameData.isClear2)
        {
            Tr.position = TmpPos;
        }
    }
}