using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public GameObject playerPrefab;
    int[,] map; // マップの元データ（数字）
    GameObject[,] field;    // map を元にしたオブジェクトの格納庫

    /// <summary>
    /// number を動かす
    /// </summary>
    /// <param name="number">動かす数字</param>
    /// <param name="moveFrom">移動元インデックス</param>
    /// <param name="moveTo">移動先インデックス</param>
    /// <returns></returns>
    bool MoveNumber(Vector2Int moveFrom, Vector2Int moveTo)
    {
        // 動けない場合は false を返す
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0))
            return false;
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1))
            return false;

        //if (map[moveTo] == 2)
        //{
        //    // 移動方向（正なら→、負なら←を計算する）
        //    int velocity = moveTo - moveFrom;
        //    bool success = MoveNumber(2, moveTo, moveTo + velocity);

        //    if (!success)
        //    {
        //        return false;
        //    }
        //}   // プレイヤーの移動先に箱がいた場合の処理

        // プレイヤー・箱の共通処理
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x] = null;
        // オブジェクトのシーン上の座標を動かす
        field[moveTo.y, moveTo.x].transform.position =
            new Vector3(moveTo.x, -1 * moveTo.y, 0);

        return true;
    }

    /// <summary>
    /// プレイヤーの座標を調べて取得する
    /// ※）GetPlayerPosition 
    /// </summary>
    /// <returns>プレイヤーの座標</returns>
    Vector2Int GetPlayerIndex()
    {
        for (int y = 0; y < field.GetLength(0); y++)
        {
            for (int x = 0; x < field.GetLength(1); x++)
            {
                if (field[y, x] != null
                    && field[y, x].tag == "Player")
                {
                    // プレイヤーを見つけた
                    return new Vector2Int(x, y);
                }
            }
        }

        return new Vector2Int(-1, -1);  // 見つからなかった
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
        };  // 0: 何もない, 1: プレイヤー, 2: 箱

        field = new GameObject
        [
            map.GetLength(0),
            map.GetLength(1)
        ];  // map の行列と同じ升目の配列をもうひとつ作った

        PrintArray();

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    // ここにプレイヤーを出す
                    GameObject instance =
                        Instantiate(playerPrefab,
                        new Vector3(x, -1 * y, 0),
                        Quaternion.identity);
                    // プレイヤーは１つだけなので抜ける
                    field[y, x] = instance; // プレイヤーを保存しておく
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
            MoveNumber(playerPosition, new Vector2Int(playerPosition.x + 1, playerPosition.y));    // →に移動
            //PrintArray();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var playerPosition = GetPlayerIndex();
            MoveNumber(playerPosition, new Vector2Int(playerPosition.x - 1, playerPosition.y));    // →に移動
            PrintArray();
        }
    }
}