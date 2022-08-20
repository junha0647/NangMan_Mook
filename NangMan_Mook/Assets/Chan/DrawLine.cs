using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    public GameObject LinePrefab;

    LineRenderer lr;
    EdgeCollider2D col;
    List<Vector2> points = new List<Vector2>();

    private bool isPaused = false;

    


    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Tab))
        //{
        //    if(isPaused)
        //    {
        //        Time.timeScale = 1;
        //        isPaused = false;
        //    }
        //    else
        //    {
        //        Time.timeScale = 0;
        //        isPaused = true;
        //    }
        //}

        if (isPaused)
        {
            DrawLines();
        }
    }

    private void DrawLines()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(LinePrefab);
            lr = go.GetComponent<LineRenderer>();
            col = go.GetComponent<EdgeCollider2D>();
            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition)); // 마우스에 현재 위치를 리스트에 추가
            lr.positionCount = 1;
            lr.SetPosition(0, points[0]);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(points[points.Count - 1], pos) > 0.1f)
            {
                points.Add(pos);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, pos);
                col.points = points.ToArray();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            points.Clear();
        }
    }

    

    // 떨어지고 나서 5~7초 후 충돌하지 않으면 오브젝트 삭제

}
