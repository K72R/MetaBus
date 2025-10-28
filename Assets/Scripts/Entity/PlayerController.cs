using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : BaseController
{
    private Camera camera;
    private GameManager gameManager;


    protected override void Start()
    {
        base.Start();
        if (camera == null)
            camera = Camera.main;
    }
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        camera = Camera.main;
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < .9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }

        isAttacking = Input.GetMouseButton(0);
    }

    public override void Death()
    {
        base.Death();
        gameManager.GameOver();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // DreamHole과 부딪혔는지 확인
        if (collision.CompareTag("DreamHole"))
        {
            Debug.Log("몬스터 잡으러 가보자");
            SceneManager.LoadScene("SampleScene");
        }
    }
}
