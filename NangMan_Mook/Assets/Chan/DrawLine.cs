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
            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition)); // ���콺�� ���� ��ġ�� ����Ʈ�� �߰�
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

    

    // �������� ���� 5~7�� �� �浹���� ������ ������Ʈ ����

}
