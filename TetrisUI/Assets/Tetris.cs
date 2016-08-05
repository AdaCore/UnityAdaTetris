/**
 *
 *                             GNAT EXAMPLE
 *
 *                      Copyright (C) 2016, AdaCore
 *
 * GNAT is free software;  you can  redistribute it  and/or modify it under
 * terms of the  GNU General Public License as published  by the Free Soft-
 * ware  Foundation;  either version 3,  or (at your option) any later ver-
 * sion.  GNAT is distributed in the hope that it will be useful, but WITH-
 * OUT ANY WARRANTY;  without even the  implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE.
 *
 * As a special exception under Section 7 of GPL version 3, you are granted
 * additional permissions described in the GCC Runtime Library Exception,
 * version 3.1, as published by the Free Software Foundation.
 *
 * You should have received a copy of the GNU General Public License and
 * a copy of the GCC Runtime Library Exception along with this program;
 * see the files COPYING3 and COPYING.RUNTIME respectively.  If not, see
 * <http://www.gnu.org/licenses/>.
 *
 * GNAT was originally developed  by the GNAT team at  New York University.
 * Extensive contributions were provided by Ada Core Technologies Inc.
 *
 */

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class Tetris : MonoBehaviour
{

    [DllImport("tetris")]
    private static extern byte tetris_get_state();

    [DllImport("tetris")]
    private static extern void tetris_cycle();

    [DllImport("tetris")]
    private static extern byte tetris_get_kind(int x, int y);

    private struct Piece
    {
        public byte shape;
        public byte direction;
        public int x;
        public int y;
    }

    [DllImport("tetris")]
    private static extern Piece tetris_get_cur_piece();

    [DllImport("tetris")]
    private static extern bool tetris_check_possible_i_shapes(byte d, int x, int y);

    [DllImport("tetris")]
    private static extern bool tetris_check_possible_three_shapes(byte s, byte d, int x, int y);

    [DllImport("tetris")]
    private static extern bool tetris_do_action(byte a);

    [DllImport("tetris")]
    private static extern bool tetris_is_line_destroyed(int y);

    int xSize = 10;
    int ySize = 38;

    bool fallThrough = false;

    GameObject prefabBlock;
    Cell[,] cells;

    // Use this for initialization
    void Awake()
    {
        prefabBlock = Resources.Load("Cell") as GameObject;

        cells = new Cell[xSize, ySize];

        for (int x = 0; x < xSize; ++x)
        {
            for (int y = 0; y < ySize; ++y)
            {
                cells[x, y] = Instantiate(prefabBlock).GetComponent<Cell>();
                cells[x, y].transform.position = new Vector3(x, ySize - y, 0);
                cells[x, y].gameObject.SetActive(false);
            }
        }
    }

    float lastTime = 0;

    private void Refresh ()
    {
        for (int x = 0; x < xSize; ++x)
        {
            for (int y = 0; y < ySize; ++y)
            {
                int k = tetris_get_kind(x + 1, y + 1);

                if (k == 0)
                {
                    cells[x, y].gameObject.SetActive(false);
                }
                else
                {
                    cells[x, y].gameObject.SetActive(true);
                    cells[x, y].SetKind(k);
                }
            }
        }

        Piece curPiece = tetris_get_cur_piece();

        switch (curPiece.shape)
        {
            case 0:
                break;
            case 2: // O
                cells[curPiece.x - 1, curPiece.y - 1].gameObject.SetActive(true);
                cells[curPiece.x - 1, curPiece.y + 1 - 1].gameObject.SetActive(true);
                cells[curPiece.x + 1 - 1, curPiece.y - 1].gameObject.SetActive(true);
                cells[curPiece.x + 1 - 1, curPiece.y + 1 - 1].gameObject.SetActive(true);

                cells[curPiece.x - 1, curPiece.y - 1].SetKind(2);
                cells[curPiece.x - 1, curPiece.y + 1 - 1].SetKind(2);
                cells[curPiece.x + 1 - 1, curPiece.y - 1].SetKind(2);
                cells[curPiece.x + 1 - 1, curPiece.y + 1 - 1].SetKind(2);

                break;
            case 1: // I
                for (int y = 0; y <= 3; ++y)
                {
                    for (int x = 0; x <= 3; ++x)
                    {
                        if (tetris_check_possible_i_shapes(curPiece.direction, x, y))
                        {
                            cells[curPiece.x + x - 1, curPiece.y + y - 1].gameObject.SetActive(true);
                            cells[curPiece.x + x - 1, curPiece.y + y - 1].SetKind(1);
                        }
                    }
                }
                break;
            default: // all others
                for (int y = 0; y <= 2; ++y)
                {
                    for (int x = 0; x <= 2; ++x)
                    {
                        if (tetris_check_possible_three_shapes(curPiece.shape, curPiece.direction, x, y))
                        {
                            cells[curPiece.x + x - 1, curPiece.y + y - 1].gameObject.SetActive(true);
                            cells[curPiece.x + x - 1, curPiece.y + y - 1].SetKind(curPiece.shape);
                        }
                    }
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float delay = 1.0f;

        if (fallThrough && tetris_get_state() == 2)
        {
            delay = 0.1f;
        } else
        {
            fallThrough = false;
            delay = 1.0f;
        }

        if (Time.time - lastTime > delay)
        {
            tetris_cycle();
            lastTime = Time.time;

            for (int y = 0; y < ySize; ++y)
            {
                if (tetris_is_line_destroyed(y + 1))
                {
                    for (int x = 0; x < xSize; ++x)
                    {
                        cells[x, y].Explode();
                    }
                }
            }

            Refresh();
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            tetris_do_action(0);
            Refresh();
        } else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            tetris_do_action(1);
            Refresh();
        } else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            tetris_do_action(3);
            Refresh();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            tetris_do_action(4);
            Refresh();
        } else if (Input.GetKeyUp(KeyCode.Space))
        {
            fallThrough = true;
        }
    }
}
