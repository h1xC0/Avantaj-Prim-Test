using Systems.CommandSystem.Payloads;

namespace Signals
{
    public class EndLevelPayload : ICommandPayload
    {
        public bool LevelCompletionState;
        
        public EndLevelPayload(bool state)
        {
            LevelCompletionState = state;
        }
    }
}