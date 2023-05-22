namespace MainComponents.Gifts.Models
{
    public class Gift
    {
        private BoxModel _box; 
        public BoxModel Box => _box;

        private BowModel _bow;
        public BowModel Bow => _bow;

        private OrnamentModel _ornament;

        public Gift(BoxModel box, BowModel bow, OrnamentModel ornament)
        {
            _box = box;
            _bow = bow;
            _ornament = ornament;
        }
        
        public Gift()
        {
            
        }

        public OrnamentModel Ornament => _ornament;

        // public Gift Create()
        // {
        //     return new Gift(box, bow, ornament);
        // }

        public void Copy(Gift from)
        {
            _box = from.Box;
            _bow = from.Bow;
            _ornament = from.Ornament;
        }
        
        public void ApplyGiftPart(GiftPartSO part)
        {
            switch (part)
            {
                case BoxModel box:
                    _box = box;
                    break;
                case BowModel bow:
                    _bow = bow;
                    break;
                case OrnamentModel ornament:
                    _ornament = ornament;
                    break;
            }
        }

        public bool Compare(Gift gift)
        {
            return gift.Box == Box && gift.Bow == Bow && gift.Ornament == Ornament;
        }
    }
}