using Framework.Engine;
using System;

public class TileMap : GameObject
{
    private string[] tileMap;

    private int playerPosX;
    private int playerPosY;
    private int fillCount = 0; //채워진 박스 갯수
    private int moveCount = 0; //캐릭터 총 이동 횟
    public int Width => tileMap[0].Length;
    public int Height => tileMap.Length;

    // 타일 정의 (중요)
    const char WALL = '#'; // 벽
    const char FLOOR = ' '; //빈 공간 (이동 가능 범위)
    const char PLAYER = 'P'; // 플레이어 캐릭터 위치
    const char BOX = 'B'; //박스 위치
    const char GOAL = 'X'; //골인 지점 위치
    const char BOX_ON_GOAL = '*'; //박스가 골인지점에 들어갔을때 
    const char PLAYER_ON_GOAL = '+'; //플레이어가 골인지점위에 있을때

    public TileMap(Scene scene) : base(scene)
    {
        tileMap = new string[]
        {
            "########",
            "#      #",
            "#      #",
            "#  P   #",
            "#  B   #",
            "#  X   #",
            "#      #",
            "#      #",
            "########"
        };

        //  플레이어 위치 찾기
        for (int y = 0; y < tileMap.Length; y++)
        {
            for (int x = 0; x < tileMap[y].Length; x++)
            {
                if (tileMap[y][x] == PLAYER)
                {
                    playerPosX = x;
                    playerPosY = y;
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
        for (int y = 0; y < tileMap.Length; y++)
        {
            for (int x = 0; x < tileMap[y].Length; x++)
            {
                char tile = tileMap[y][x];

                switch (tile)
                {
                    case WALL:
                        buffer.SetCell(x, y, '#', ConsoleColor.White);
                        break;
                    case PLAYER:
                        buffer.SetCell(x, y, 'P', ConsoleColor.Green);
                        break;
                    case BOX:
                        buffer.SetCell(x, y, 'B', ConsoleColor.Yellow);
                        break;
                    case GOAL:
                        buffer.SetCell(x, y, 'X', ConsoleColor.Red);
                        break;
                    case PLAYER_ON_GOAL:
                        buffer.SetCell(x, y, 'P', ConsoleColor.Red);
                        break;
                    case BOX_ON_GOAL:
                        buffer.SetCell(x, y, 'B', ConsoleColor.Red);
                        break;
                    default:
                        buffer.SetCell(x, y, ' ');
                        break;
                }
            }
        }
    }

    //  타일 수정
    void SetTile(int x, int y, char value)
    {
        char[] row = tileMap[y].ToCharArray();
        row[x] = value;
        tileMap[y] = new string(row);
    }

    // 플레이어 이동
    void MovePlayer(int newX, int newY)
    {
        // 이전 위치 복구
        if (tileMap[playerPosY][playerPosX] == PLAYER_ON_GOAL)
            SetTile(playerPosX, playerPosY, GOAL);
        else
            SetTile(playerPosX, playerPosY, FLOOR);

        // 이동 위치 처리
        if (tileMap[newY][newX] == GOAL)
            SetTile(newX, newY, PLAYER_ON_GOAL);
        else
            SetTile(newX, newY, PLAYER);

        playerPosX = newX;
        playerPosY = newY;
    }

    // 박스 이동
    void MoveBox(int boxPosX, int boxPosY, int newX, int newY)
    {
        // 목적지 처리
        if (tileMap[newY][newX] == GOAL)
            SetTile(newX, newY, BOX_ON_GOAL);
        else
            SetTile(newX, newY, BOX);

        // 기존 위치 복구
        if (tileMap[boxPosY][boxPosX] == BOX_ON_GOAL)
            SetTile(boxPosX, boxPosY, GOAL);
        else
            SetTile(boxPosX, boxPosY, FLOOR);
    }

    //  이동 처리 핵심
    void TryMove(int dx, int dy)
    {
        int nextX = playerPosX + dx;
        int nextY = playerPosY + dy;

        int nextNextX = playerPosX + dx * 2;
        int nextNextY = playerPosY + dy * 2;

        char nextTile = tileMap[nextY][nextX];

        // 벽
        if (nextTile == WALL) return;

        // 이동 가능
        if (nextTile == FLOOR || nextTile == GOAL)
        {
            MovePlayer(nextX, nextY);
        }
        // 박스
        else if (nextTile == BOX || nextTile == BOX_ON_GOAL)
        {
            //  범위 체크 (안정성)
            if (nextNextX < 0 || nextNextX >= Width ||
                nextNextY < 0 || nextNextY >= Height)
                return;

            char nextNextTile = tileMap[nextNextY][nextNextX];

            // 뒤가 막혀있으면 이동 불가
            if (nextNextTile != FLOOR && nextNextTile != GOAL)
                return;

            MoveBox(nextX, nextY, nextNextX, nextNextY);
            MovePlayer(nextX, nextY);
        }
    }

    //  입력 처리
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

    //  클리어 조건
    public bool IsCleared()
    {
        for (int y = 0; y < tileMap.Length; y++)
        {
            for (int x = 0; x < tileMap[y].Length; x++)
            {
                if (tileMap[y][x] == BOX)
                    return false;
            }
        }
        return true;
    }
}