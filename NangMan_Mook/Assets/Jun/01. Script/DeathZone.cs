using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [Header("UI 오브젝트 넣기")] [SerializeField] private UIManager ui;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("죽음");
            ui.GameOver();
        }
    }
}
