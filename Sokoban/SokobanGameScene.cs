using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

public class SokobanGameScene : Scene
{
    private TileMap map;
    private int stageIndex;

    public event GameAction StageCleared;
    public SokobanGameScene(int stageIndex)
    {
        this.stageIndex = stageIndex;
    }
    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);
    }

    public override void Load()
    {
        map = new TileMap(this, stageIndex);
        AddGameObject(map);
    }

    public override void Unload()
    {
        ClearGameObjects();
    }

    public override void Update(float deltaTime)
    {
        UpdateGameObjects(deltaTime);
        if (map.IsCleared())
        {
            StageCleared?.Invoke();
        }
    }
}