using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    int[,] map;

    /// <summary>
    /// number を動かす
    /// </summary>
    /// <param name="number">動かす数字</param>
    /// <param name="moveFrom">移動元インデックス</param>
    /// <param name="moveTo">移動先インデックス</param>
    /// <returns></returns>
    bool MoveNumber(int number, int moveFrom, int moveTo)
    {
        if (moveTo < 0 || moveTo >= map.Length)
        {
            return false;
        }   // 動けない場合は false を返す

        //if (map[moveTo] == 2)
        {
            // 移動方向（正なら→、負なら←を計算する）
            int velocity = moveTo - moveFrom;
            bool success = MoveNumber(2, moveTo, moveTo + velocity);

            if (!success)
            {
                return false;
            }
        }   // プレイヤーの移動先に箱がいた場合の処理

        // プレイヤー・箱の共通処理
        //map[moveTo] = number;
        //map[moveFrom] = 0;
        return true;
    }

    int GetPlayerIndex()
    {
        //for (int i = 0; i < map.Length; i++)
        //{
        //    if (map[i] == 1)
        //    {
        //        return i;
        //    }   // 見つけた
        //}   // 線形探索する

        return -1;  // 見つからなかった
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
            { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0 },
            { 0, 0, 1, 0, 1, 0, 0, 2, 0, 0, 1, 0, 0, 0 },
            { 0, 0, 0, 1, 0, 2, 0, 0, 1, 0, 2, 0, 0, 0 },
        };

        PrintArray();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            int playerIndex = GetPlayerIndex();
            MoveNumber(1, playerIndex, playerIndex + 1);    // →に移動
            PrintArray();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            int playerIndex = GetPlayerIndex();
            MoveNumber(1, playerIndex, playerIndex - 1);    // ←に移動
            PrintArray();
        }
    }
}