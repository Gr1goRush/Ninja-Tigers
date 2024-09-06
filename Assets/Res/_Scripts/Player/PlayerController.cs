using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float jumpStartTime;
    public Transform _targetGround;
    public LayerMask groundLayer;
    [Range(0f, 5f)]
    public float _sizeOverlapCircle;
    [Space]
    public float _speed;
    [Space]
    public Animator _animator;
    [Space]
    public BackgroundHelper _backgroundHelper;
    [Space]
    public GameObject canvas2;
    [Space]
    public bool isDeath = false;

    private bool isGrounded;
    private float jumpTime;
    private bool isJumping;
    public bool isCollision;
    public bool isTrening;
    bool isBoster = false;

    void Update()
    {
        // ���������, ��������� �� �������� �� �����
        isGrounded = Physics2D.OverlapCircle(_targetGround.position, _sizeOverlapCircle, groundLayer);

        // ���� ���� ������� �� ������ ��� ���, � �������� ��������� �� �����, �� ������ ������
        Jump();
        Move(_speed);
    }
    void Move(float speed)
    {
        // ���������� ��������� ������ � �������������� Translate
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    void Jump()
    {
        if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && isGrounded)
        {
            isJumping = true;
            jumpTime = jumpStartTime;
            // ��������� ���� ������ �����
            GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
            _animator.SetTrigger("jump");
            _animator.SetBool("run", false);
            isCollision = true;
            // ������������� ���������� isGrounded � false, ����� ������������� ������� ������
            isGrounded = false;
        }
        if ((Input.GetMouseButton(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && isJumping)
        {
            if (jumpTime > 0)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpForce;
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            isJumping = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���������, ���������� �� ����� � �������� �� ���� groundLayer
        if (collision.gameObject.CompareTag("Ground") && isCollision)
        {
            // ��������� �������� "run"
            _animator.SetBool("run", true);
            isCollision = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap") && !isTrening)
        {
            if(isGrounded)
            {
                isDeath = true;
                _animator.SetBool("death", true);
                Destroy(_backgroundHelper.GetComponent<BackgroundHelper>());
                _speed = 0;
                canvas2.SetActive(true);
            }
            else
            {
                isDeath = true;
                GetComponent<Rigidbody2D>().velocity = Vector2.up * 10f;
                Destroy(GetComponent<BoxCollider2D>());
                Destroy(_animator.GetComponent<Animator>());
                Destroy(_backgroundHelper.GetComponent<BackgroundHelper>());
                _speed = 0;
                canvas2.SetActive(true);
            }
        }
        else if(collision.gameObject.CompareTag("Trap") && isTrening)
        {
            SceneManager.LoadScene(2);
        }

        if (collision.gameObject.CompareTag("speed"))
        {
            if(!isBoster)
            {
                _speed = 12f;
                isBoster = true;
            }            
            Destroy(collision.gameObject);
            StartCoroutine(bonusTime());
        }
    }
    IEnumerator bonusTime()
    {
        yield return new WaitForSeconds(3f);
        if(!isDeath)
            _speed = 8f;
        isBoster = false;
    }
    void OnDrawGizmos()
    {
        // ���������� ���������� � �������� 0.2 ������ ������� ���������
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_targetGround.position, _sizeOverlapCircle);
    }
}
