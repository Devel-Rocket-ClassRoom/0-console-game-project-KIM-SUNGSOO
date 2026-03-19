using Framework.Engine;
using System;


public class ClearScene : Scene
{
    public event GameAction NextRequested;
    

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
        buffer.WriteTextCentered(12, "Press ENTER to Continue", ConsoleColor.White);
    }
}
