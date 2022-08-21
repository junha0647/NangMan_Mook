using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// �����ϴ� ���
// 1. ������ �����Ͱ� ����
// 2. �����͸� ���̽����� ��ȯ
// 3. ���̽��� �ܺο� ����

// �ҷ����� ���
// 1. �ܺο� ����� ���̽��� ������
// 2. ���̽��� ���������·� ��ȯ
// 3. �ҷ��� �����͸� ���


public class DataController : MonoBehaviour
{

    // �̱������� ����
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    static DataController _instance;

    public static DataController Instance
    {
        get
        {
            if(!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    // ���� ������ �����̸� ����
    public string GameDataFileName = "StarfishData.json";

    // "���ϴ� �̸�(����).json"
    public GamaData _gameData;
    public GamaData gameData
    {
        get
        {
            // ������ ���۵Ǹ� �ڵ����� ����ǵ���
            if(_gameData == null)
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    private void Start()
    {
        LoadGameData();
        SaveGameData();
    }

    // ����� ���� �ҷ�����
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName;

        // ����� ������ �ִٸ�
        if(File.Exists(filePath))
        {
            print("�ҷ����� ����");
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GamaData>(FromJsonData);
        }

        // ����� ������ ���ٸ�
        else
        {
            print("���ο� ���� ����");
            _gameData = new GamaData();
        }
    }

    // ���� �����ϱ�
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData);
        string filePath = Application.persistentDataPath + GameDataFileName;

        // �̹� ����� ������ �ִٸ� �����
        File.WriteAllText(filePath, ToJsonData);

        // �ùٸ� �������� Ȯ�� (�����Ӱ� ����)
        print("����Ϸ�");
        print("2�� " + gameData.isClear2);
        print("3�� " + gameData.isClear3);
        print("4�� " + gameData.isClear4);
        print("5�� " + gameData.isClear5);
    }

    // ������ �����ϸ� �ڵ�����ǵ���
    private void OnApplicationQuit()
    {
        SaveGameData();
    }

    public void SaveUnlock(int chapterNumber)
    {
        switch (chapterNumber)
        {
            case 2:
                _gameData.isClear2 = true;
                _gameData.isClear2Count++;
                SaveGameData();
                break;
            case 3:
                _gameData.isClear3 = true;
                _gameData.isClear3Count++;
                SaveGameData();
                break;
            case 4:
                _gameData.isClear4 = true;
                _gameData.isClear4Count++;
                SaveGameData();
                break;
            case 5:
                _gameData.isClear5 = true;
                _gameData.isClear5Count++;
                SaveGameData();
                break;
        }
    }

    public void SaveReset()
    {
        _gameData.Pos = "0/0";
        _gameData.isClear2 = false;
        _gameData.isClear3 = false;
        _gameData.isClear4 = false;
        _gameData.isClear2Count = 0;
        _gameData.isClear3Count = 0;
        _gameData.isClear4Count = 0;
        SaveGameData();
    }
}
