namespace MainComponents.Gifts
{
    public class Gift
    {
        private BoxModel Box;
        private BowModel Bow;
        private OrnamentModel Ornament;

        public Gift (BoxModel box, BowModel bow, OrnamentModel ornament)
        {
            Box = box;
            Bow = bow;
            Ornament = ornament;
        }
        
        public Gift ()
        {
            Box = null;
            Bow = null;
            Ornament = null;
        }

        public Gift Create()
        {
            return new Gift(Box, Bow, Ornament);
        }

        public void Copy(Gift from)
        {
            Box = from.Box;
            Bow = from.Bow;
            Ornament = from.Ornament;
        }
        
        public void ApplyGiftPart(GiftPartSO part)
        {
            switch (part)
            {
                case BoxModel box:
                    Box = box;
                    break;
                case BowModel bow:
                    Bow = bow;
                    break;
                case OrnamentModel ornament:
                    Ornament = ornament;
                    break;
            }
        }

        public bool Compare(Gift gift)
        {
            return gift.Box == Box && gift.Bow == Bow && gift.Ornament == Ornament;
        }
    }
}