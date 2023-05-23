namespace Systems.CommandSystem.Payloads
{
    public class EndLevelStatePayload : ICommandPayload
    {
        public bool LevelEnd;

        public EndLevelStatePayload(bool levelEnd)
        {
            LevelEnd = levelEnd;
        }
    }
}