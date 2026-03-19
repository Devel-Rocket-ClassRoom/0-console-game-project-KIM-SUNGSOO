using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class SokobanGameScene : Scene
{
    private TileMap map;
    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);
    }

    public override void Load()
    {
        map = new TileMap(this);
        AddGameObject(map);
    }

    public override void Unload()
    {
        ClearGameObjects();
    }

    public override void Update(float deltaTime)
    {
        UpdateGameObjects(deltaTime);
    }
}