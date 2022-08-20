using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable] // 직렬화
public class GamaData
{
    public string name;
    public int Hp;
    public string Pos;

    // 각 지역의 잠금여부
    public bool isClear2;
    public bool isClear3;
    public bool isClear4;
    public bool isClear5;

}
