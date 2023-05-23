namespace Systems.CommandSystem.Payloads
{
    public class EndLevelStatePayload : ICommandPayload
    {
        public bool LevelState;
        public bool LevelEnded;

        public EndLevelStatePayload(bool levelState, bool levelEnded)
        {
            LevelState = levelState;
            LevelEnded = levelEnded;
        }
    }
}