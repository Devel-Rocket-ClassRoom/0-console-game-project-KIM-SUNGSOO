using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class TileMap : GameObject
{
    private string[] map;

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
    }

    public override void Update(float deltaTime)
    {
        
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

                    default:
                        buffer.SetCell(x, y, ' ');
                        break;
                }
            }
        }
    }
}