using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;


public class FaileScene : Scene
{
    
    public event GameAction RestartRequested;

    public override void Load()
    {
        
    }

    public override void Unload()
    {
        
    }
    public override void Draw(ScreenBuffer buffer)
    {
        buffer.WriteTextCentered(10, "STAGE Failed!", ConsoleColor.Green);
        buffer.WriteTextCentered(12, $"Want To Retry?", ConsoleColor.Yellow);
        buffer.WriteTextCentered(14, "Press ENTER to Continue or", ConsoleColor.White);
        buffer.WriteTextCentered(16, "Press ESC to quit", ConsoleColor.White);
    }

    

    public override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.Enter))
        {
            RestartRequested?.Invoke();
        }
    }
}