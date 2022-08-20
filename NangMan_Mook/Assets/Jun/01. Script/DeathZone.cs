using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [Header("UI ������Ʈ �ֱ�")] [SerializeField] private UIManager ui;
    [Header("Player ������Ʈ �ֱ�")][SerializeField] private PlayerController Player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            ui.MapOut();
            Player.GetComponent<Animator>().SetTrigger("isGameOver");
        }
    }
}
