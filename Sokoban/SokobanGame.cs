using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class SokobanGame : GameApp
{
    private readonly SceneManager<Scene>  _scene = new SceneManager<Scene>();
    public SokobanGame() : base(80, 30)
    {
        Console.SetWindowSize(80, 30);
        Console.SetBufferSize(80, 30);
    }
    public SokobanGame(int width, int height) : base(width, height)
    {
    }

    protected override void Draw() //Tilte Scene 구성
    {
        _scene.CurrentScene?.Draw(Buffer);
    }

    protected override void Initialize()
    {
        ChangeToTitle();
    }

    protected override void Update(float deltaTime) //Title 부분에서 esc누르면 게임 종료 
    {
        if (Input.IsKeyDown(ConsoleKey.Escape))
        {
            Quit();
            return;
        }
        _scene.CurrentScene?.Update(deltaTime);
    }

   private void ChangeToTitle()
   {
        var title = new socobanTitleScene();
        title.StartRequested += ChangeToPlay;
        _scene.ChangeScene(title);
   }
   private void ChangeToPlay()
   {

   }
}
