using Framework.Engine;
using System;


public class ClearScene : Scene
{
    public event GameAction NextRequested;


    private int moveCount;

    public ClearScene(int moveCount)
    {
        this.moveCount = moveCount;
    }
    public override void Load()
    {
        
    }

    public override void Unload()
    {
        
    }

    public override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.Enter))
        {
            NextRequested?.Invoke();
        }
    }
    public override void Draw(ScreenBuffer buffer)
    {
        buffer.WriteTextCentered(10, "STAGE CLEAR!", ConsoleColor.Green);
        buffer.WriteTextCentered(12, $"Total Moves: {moveCount}", ConsoleColor.Yellow);
        buffer.WriteTextCentered(14, "Press ENTER to Continue", ConsoleColor.White);
        
    }
}
