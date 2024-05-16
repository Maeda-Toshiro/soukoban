using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    int[,] map; // �}�b�v�̌��f�[�^�i�����j
    GameObject[,] field;    // map �����ɂ����I�u�W�F�N�g�̊i�[��

    /// <summary>
    /// number �𓮂���
    /// </summary>
    /// <param name="number">����������</param>
    /// <param name="moveFrom">�ړ����C���f�b�N�X</param>
    /// <param name="moveTo">�ړ���C���f�b�N�X</param>
    /// <returns></returns>
    bool MoveNumber(Vector2Int moveFrom, Vector2Int moveTo)
    {
        // �����Ȃ��ꍇ�� false ��Ԃ�
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0))
            return false;
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1))
            return false;

        //if (map[moveTo] == 2)
        //{
        //    // �ړ������i���Ȃ灨�A���Ȃ灩���v�Z����j
        //    int velocity = moveTo - moveFrom;
        //    bool success = MoveNumber(2, moveTo, moveTo + velocity);

        //    if (!success)
        //    {
        //        return false;
        //    }
        //}   // �v���C���[�̈ړ���ɔ��������ꍇ�̏���

        // �v���C���[�E���̋��ʏ���
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;
        // �I�u�W�F�N�g�̃V�[����̍��W�𓮂���
        field[moveTo.y, moveTo.x].transform.position =
            new Vector3(moveTo.x, -1 * moveTo.y, 0);

        return true;
    }

    /// <summary>
    /// �v���C���[�̍��W�𒲂ׂĎ擾����
    /// ���jGetPlayerPosition 
    /// </summary>
    /// <returns>�v���C���[�̍��W</returns>
    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] != null
                    && field[y, x].tag == "Player")
                {
                    // �v���C���[��������
                    return new Vector2Int(x, y);
                }
            }
        }

        return new Vector2Int(-1, -1);  // ������Ȃ�����
    }

    void PrintArray()
    {
        string debugText = "";

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                debugText += map[y, x].ToString() + ",";
            }

            debugText += "\n";
        }

        Debug.Log(debugText);
    }

    void Start()
    {
        map = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 2, 0, 0, 1, 0, 2, 0, 0, 0 },
        };  // 0: �����Ȃ�, 1: �v���C���[, 2: ��

        field = new GameObject
        [
            map.GetLength(0),
            map.GetLength(1)
        ];  // map �̍s��Ɠ������ڂ̔z��������ЂƂ����

        PrintArray();

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    // �����Ƀv���C���[���o��
                    GameObject instance =
                        Instantiate(playerPrefab,
                        new Vector3(x, -1 * y, 0),
                        Quaternion.identity);
                    // �v���C���[�͂P�����Ȃ̂Ŕ�����
                    field[y, x] = instance; // �v���C���[��ۑ����Ă���
                    break;
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //int playerIndex = GetPlayerIndex();
            var playerPosition = GetPlayerIndex();
            MoveNumber(playerPosition, new Vector2Int(playerPosition.x + 1, playerPosition.y));    // ���Ɉړ�
            //PrintArray();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var playerPosition = GetPlayerIndex();
            MoveNumber(playerPosition, new Vector2Int(playerPosition.x - 1, playerPosition.y));    // ���Ɉړ�
            PrintArray();
        }
    }
}