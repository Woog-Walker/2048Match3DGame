using Zenject;
using UnityEngine;
using DiceThrower.Mechanics.Thrower;
using DiceGame.Mechanics.MagneteForDices;
using DiceGame.GameFlow;
using DiceGame.InGameCanvasManager;
using DiceGame.Mechanics.InGameScoreManager;
using DiceGame.Mechanics.DiceMerdger;
using DiceGame.Mechanics.InGameSoundsController;
using DiceGame.Mechanics.EndGameCase;

namespace DiceGame.Mechanics.ContainerBinder
{
    public class ZenContainerBinder : MonoInstaller
    {
        [SerializeField] DiceThrowerController diceThrowerController;
        [SerializeField] DicesMagnetController magneteForDices;
        [SerializeField] GameFlowController gameFlowController;
        [SerializeField] CanvasManager canvasManager;
        [SerializeField] ScoreManager scoreManager;
        [SerializeField] MerdgeDicesController merdgeDicesController;
        [SerializeField] InGameSounds inGameSounds;
        [SerializeField] EndGameTriggeringSystem endGameTriggeringSystem;
        [SerializeField] PoolOfParticlesToMerdge poolOfParticlesToMerdge;

        public override void InstallBindings()
        {
            Container.Bind<DiceThrowerController>().FromInstance(diceThrowerController).AsSingle();
            Container.Bind<DicesMagnetController>().FromInstance(magneteForDices).AsSingle();
            Container.Bind<GameFlowController>().FromInstance(gameFlowController).AsSingle();
            Container.Bind<CanvasManager>().FromInstance(canvasManager).AsSingle();
            Container.Bind<ScoreManager>().FromInstance(scoreManager).AsSingle();
            Container.Bind<MerdgeDicesController>().FromInstance(merdgeDicesController).AsSingle();
            Container.Bind<InGameSounds>().FromInstance(inGameSounds).AsSingle();
            Container.Bind<EndGameTriggeringSystem>().FromInstance(endGameTriggeringSystem).AsSingle();
            Container.Bind<PoolOfParticlesToMerdge>().FromInstance(poolOfParticlesToMerdge).AsSingle();
        }
    }
}