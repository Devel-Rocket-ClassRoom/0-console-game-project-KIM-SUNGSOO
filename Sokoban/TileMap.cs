using Framework.Engine;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

public class TileMap : GameObject
{
    private string[] map;

    private int playerX;
    private int playerY;
    public int Width => map[0].Length;
    public int Height => map.Length;

    public TileMap(Scene scene) : base(scene)
    {
        map = new string[]
        {
            "########",
            "#      #",
            "#  P   #",
            "#  B   #",
            "#  X   #",
            "#      #",
            "########"
        };

        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == 'P')
                {
                    playerX = x;
                    playerY = y;
                }
            }
        }
    }

    public override void Update(float deltaTime)
    {
        HandleInput();
    }

    public override void Draw(ScreenBuffer buffer)
    {
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                char tile = map[y][x];

                switch (tile)
                {
                    case '#':
                        buffer.SetCell(x, y, '#', ConsoleColor.White);
                        break;

                    case 'P':
                        buffer.SetCell(x, y, 'P', ConsoleColor.Green);
                        break;

                    case 'B':
                        buffer.SetCell(x, y, 'B', ConsoleColor.Yellow);
                        break;

                    case 'X':
                        buffer.SetCell(x, y, 'X', ConsoleColor.Red);
                        break;
                    case '+':
                        buffer.SetCell(x, y, '+', ConsoleColor.Cyan);
                        break;

                    case '*':
                        buffer.SetCell(x, y, '*', ConsoleColor.DarkBlue);
                        break;
                    default:
                        buffer.SetCell(x, y, ' ');
                        break;
                }
            }
        }
    }

    void SetTile(int x, int y, char value)
    {
        char[] row = map[y].ToCharArray();
        row[x] = value;
        map[y] = new string(row);
    }

    void MovePlayer(int newX, int newY)
    {
        // 이전 위치 복구
        if (map[playerY][playerX] == '+')
            SetTile(playerX, playerY, 'X');
        else
            SetTile(playerX, playerY, ' ');

        // 이동 위치 처리
        if (map[newY][newX] == 'X')
            SetTile(newX, newY, '+');
        else
            SetTile(newX, newY, 'P');

        playerX = newX;
        playerY = newY;
    }
    void MoveBox(int boxX, int boxY, int newX, int newY)
    {
        // 목적지
        if (map[newY][newX] == 'X')
            SetTile(newX, newY, '*');
        else
            SetTile(newX, newY, 'B');
        


        // 기존 위치 복구
        if (map[boxY][boxX] == '*')
            SetTile(boxX, boxY, 'X');
        else
            SetTile(boxX, boxY, ' ');
    }

    void TryMove(int dx, int dy)
    {
        int nextX = playerX + dx;
        int nextY = playerY + dy;

        int nextNextX = playerX + dx * 2;
        int nextNextY = playerY + dy * 2;

        char next = map[nextY][nextX];

        // 벽이면 이동 불가
        if (next == '#') return;

        // 빈칸 or 목표
        if (next == ' ' || next == 'X')
        {
            MovePlayer(nextX, nextY);
        }
        // 박스
        else if (next == 'B')
        {
            char nextNext = map[nextNextY][nextNextX];
            //공 뒤에 공간이 없을 시 움직일수 없음
            if (nextNext != ' ' && nextNext != 'X')
                return;
            //공 뒤에 비어있으면 이동 가능
            if (nextNext == ' ' || nextNext == 'X')
            {
                MoveBox(nextX, nextY, nextNextX, nextNextY);
                MovePlayer(nextX, nextY);
            }
        }
        
    }
    void HandleInput()
    {
        int dx = 0, dy = 0;

        if (Input.IsKeyDown(ConsoleKey.LeftArrow)) dx = -1;
        if (Input.IsKeyDown(ConsoleKey.RightArrow)) dx = 1;
        if (Input.IsKeyDown(ConsoleKey.UpArrow)) dy = -1;
        if (Input.IsKeyDown(ConsoleKey.DownArrow)) dy = 1;

        if (dx != 0 || dy != 0)
        {
            TryMove(dx, dy);
        }
    }

    
    
    
    
}