using Zenject;
using UnityEngine;
using DiceGame.Mechanics.Thrower;
using DiceGame.Mechanics.MagneteForDices;
using DiceGame.Mechanics.GameFlow;
using DiceGame.Mechanics.InGameCanvasManager;
using DiceGame.Mechanics.InGameScoreManager;
using DiceGame.Mechanics.DiceMerdger;
using DiceGame.Mechanics.InGameSoundsController;
using DiceGame.Mechanics.EndGameCase;
using DiceGame.Mechanics.PoolOfVFX;
using DiceGame.Mechanics.TouchInput;

namespace DiceGame.Mechanics.ContainerBinder
{
    public class ZenContainerBinder : MonoInstaller
    {
        [SerializeField] private DiceThrowerController diceThrowerController;
        [SerializeField] private DicesMagnetController magneteForDices;
        [SerializeField] private GameFlowController gameFlowController;
        [SerializeField] private CanvasManager canvasManager;
        [SerializeField] private ScoreManager scoreManager;
        [SerializeField] private MerdgeDicesController merdgeDicesController;
        [SerializeField] private InGameSounds inGameSounds;
        [SerializeField] private EndGameTriggeringSystem endGameTriggeringSystem;
        [SerializeField] private PoolOfParticlesToMerdge poolOfParticlesToMerdge;
        [SerializeField] private TouchInputController touchInputController;

        public override void InstallBindings()
        {
            // Bindings for all necessary components
            Container.Bind<DiceThrowerController>().FromInstance(diceThrowerController).AsSingle();
            Container.Bind<DicesMagnetController>().FromInstance(magneteForDices).AsSingle();
            Container.Bind<GameFlowController>().FromInstance(gameFlowController).AsSingle();
            Container.Bind<CanvasManager>().FromInstance(canvasManager).AsSingle();
            Container.Bind<ScoreManager>().FromInstance(scoreManager).AsSingle();
            Container.Bind<MerdgeDicesController>().FromInstance(merdgeDicesController).AsSingle();
            Container.Bind<InGameSounds>().FromInstance(inGameSounds).AsSingle();
            Container.Bind<EndGameTriggeringSystem>().FromInstance(endGameTriggeringSystem).AsSingle();
            Container.Bind<PoolOfParticlesToMerdge>().FromInstance(poolOfParticlesToMerdge).AsSingle();
            Container.Bind<TouchInputController>().FromInstance(touchInputController).AsSingle();
        }
    }
}

// 🍒