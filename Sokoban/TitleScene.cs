using System;
using System.Collections.Generic;
using System.Text;
using Framework.Engine;

public class socobanTitleScene : Scene
{
    public event GameAction StartRequested;

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
            StartRequested?.Invoke();
        }
    }
    public override void Draw(ScreenBuffer buffer)
    {
        buffer.WriteTextCentered(6, "S O K O B A N", ConsoleColor.Yellow);
        buffer.WriteTextCentered(10, "Arrow Keys: Character Move");
        buffer.WriteTextCentered(12, "ESC: Quit");
        buffer.WriteTextCentered(15, "Press ENTER to Start", ConsoleColor.Green);
    }
}