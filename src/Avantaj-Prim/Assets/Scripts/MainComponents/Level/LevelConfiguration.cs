using MainComponents.Gifts;
using Services.ResourceProvider;
using UnityEngine;

namespace MainComponents.Level
{
    [CreateAssetMenu(fileName = "Level", menuName = "Constants/Level")]
    public class LevelConfiguration : ScriptableObject, IResource
    {
        public int LevelNumber = 1;
        public int CustomersCount = 1;
        public int Difficulty = 1;
        public float OrderWaitingTime = 1f;
        public BoxModel[] AvailableBoxes;
        public BowModel[] AvailableBows;
        public OrnamentModel[] AvailableOrnaments;
    }
}