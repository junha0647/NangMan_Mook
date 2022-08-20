using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveJon : MonoBehaviour
{

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DataController.Instance.gameData.name = "Player";
            DataController.Instance.gameData.Hp = 3;
            DataController.Instance.gameData.Pos = this.transform.position.x + "/" + this.transform.position.y;
            DataController.Instance.gameData.isClear2 = true;
            DataController.Instance.SaveGameData();
            Destroy(this.gameObject);
        }
    }
}
