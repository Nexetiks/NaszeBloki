using System.Collections.Generic;
using UnityEngine;
using Events;

namespace Events
{
    public struct PawnSpawned
    {
        public GameManager.PlayerSideEnum Side;
        public GameObject pawn;
    }
}