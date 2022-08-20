using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [Header("UI ������Ʈ �ֱ�")] [SerializeField] private UIManager ui;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("����");
            ui.GameOver();
        }
    }
}
