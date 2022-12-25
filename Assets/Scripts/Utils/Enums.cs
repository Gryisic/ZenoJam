namespace ZenoJam.Utils
{
    public static class Enums 
    {
        public enum SceneType
        {
            Prologue = 1,
            Platformer = 2,
            RPG = 3,
            JRPG = 4,
            Epilogue = 5,
            MainMenu = 6
        }

        public enum GameModeType 
        {
            GameInitMode,
            GameplayMode,
            DialogueMode,
            SceneSwitchMode,
            CutsceneMode,
            PauseMode,
            MainMenuMode
        }

        public enum UnitType 
        {
            Player,
            Slime,
            Bat
        }

        public enum PlayerRole 
        {
            Neutral,
            Platformer,
            RPG,
            JRPG
        }

        public enum AnimationType 
        {
            Movement,
            Dash,
            Jump,
            TakeDamage,
            Death
        }

        public enum UIAnimationType 
        {
            FadeIn,
            FadeOut,
            SlideIn,
            SlideOut
        }

        public enum MonologueType 
        {
            Main,
            Additional
        }

        public enum MoveType 
        {
            None,
            Linear
        }
    }
}