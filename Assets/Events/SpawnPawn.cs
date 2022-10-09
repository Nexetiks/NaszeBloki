namespace Events
{
    public struct SpawnPawn
    {
        public GameManager.PlayerSideEnum Side;
        public UnitsData unit;
    }
}