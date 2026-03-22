using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

public class SokobanGameScene : Scene
{
    private TileMap map;
    private int stageIndex;

    public event GameAction<int> StageCleared;

    public event GameAction StageFailed;
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
        map.TrapTriggered += OnStageFailed;
        AddGameObject(map);

    }

    public override void Unload()
    {
        ClearGameObjects();
    }
    private void OnStageFailed()
    {
        StageFailed?.Invoke();
    }

    public override void Update(float deltaTime)
    {
        UpdateGameObjects(deltaTime);
        if (map.IsCleared())
        {
            StageCleared?.Invoke(map.moveCount);
        }
    }
}