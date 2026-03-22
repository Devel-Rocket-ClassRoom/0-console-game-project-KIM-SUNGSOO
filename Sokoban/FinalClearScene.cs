using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class FinalClearScene : Scene
{
    private int totalMoves;

    public event GameAction RestartRequested;

    public FinalClearScene(int totalMoves)
    {
        this.totalMoves = totalMoves;
    }

    public override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.Enter))
        {
            RestartRequested?.Invoke();
        }
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.WriteTextCentered(8, "ALL STAGES CLEAR!", ConsoleColor.Green);
        buffer.WriteTextCentered(10, $"Total Moves: {totalMoves}", ConsoleColor.Yellow);
        buffer.WriteTextCentered(12, "Press ENTER to Restart or", ConsoleColor.White);
        buffer.WriteTextCentered(13, "Press ESC to quit", ConsoleColor.White);
    }

    public override void Load()
    {
        
    }

    public override void Unload()
    {
        
    }
}
