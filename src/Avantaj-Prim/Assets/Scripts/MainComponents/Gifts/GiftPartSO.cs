using UnityEngine;

namespace MainComponents.Gifts
{
    public class GiftPartSO : ScriptableObject, IGiftPartSO
    {
        [SerializeField] private Sprite _sprite;
        public Sprite Sprite => _sprite;
    }
}