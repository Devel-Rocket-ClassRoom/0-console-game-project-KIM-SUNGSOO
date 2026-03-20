using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class SokobanGame : GameApp
{
    private readonly SceneManager<Scene>  _scene = new SceneManager<Scene>();

    private int currentStage = 1;

    private const int MAX_STAGE = 4;


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

    private void ChangeToClear(int moveCount)
    {
        if (currentStage >= MAX_STAGE)
        {
            ChangeToFinalClear(moveCount);
            return;
        }

        currentStage++;

        var clear = new ClearScene(moveCount);
        clear.NextRequested += ChangeToPlay;

        _scene.ChangeScene(clear);
    }
    private void ChangeToFinalClear(int moveCount)
    {
        var final = new FinalClearScene(moveCount);

        final.RestartRequested += () =>
        {
            currentStage = 1; // 다시 시작
            ChangeToPlay();
        };

        _scene.ChangeScene(final);
    }




}
