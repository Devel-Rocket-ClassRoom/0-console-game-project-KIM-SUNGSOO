using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class SokobanGame : GameApp
{
    private readonly SceneManager<Scene>  _scene = new SceneManager<Scene>();

    private int currentStage = 1;

    //public SokobanGame() : base(80, 30) //콘솔 기본 사이즈를 넘어선 사이즈가 나올 수 있으므로 추후에 스테이지
    //                                    //구성을 위한 기본 사이즈 지정
    //{
    //    Console.SetWindowSize(80, 30);
    //    Console.SetBufferSize(80, 30);
    //}
    public SokobanGame() : base(40, 20)
     {

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
        var game = new SokobanGameScene(currentStage);
        game.StageCleared += ChangeToClear;

        _scene.ChangeScene(game);
    }

    private void ChangeToClear()
    {
        currentStage++;
        var clear = new ClearScene();
        clear.NextRequested += ChangeToPlay;

        _scene.ChangeScene(clear);
    }




}
