namespace Events
{
    public struct FactionChange
    {
        public GameManager.FractionEnum faction;
        public GameManager.PlayerSideEnum side;
    }
}