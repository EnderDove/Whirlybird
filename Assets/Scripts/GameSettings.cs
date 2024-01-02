using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Game Settings")]
public class GameSettings : ScriptableObject
{
    [Header("Player Parameters")]
    [Min(0f)] public float MovementSpeed = 5f;
    [Min(0f)] public float JumpHeight = 8f;
    [Min(0f)] public float HightJumpHeight = 15f;
    [Min(0f)] public float FlightDuration = 7f;
    [Min(0f)] public float FlightSpeed = 7f;

    [Header("Platform Parameters")]
    [Min(0f)] public float PropellerSpawnChance = 0.15f;
    [Min(0f)] public float PlatformSpawnInterval = 2f;
    [Min(1)] public int ObstaclesCountLimit = 3;
    [Min(0f), Tooltip("Screens per second")] public float MovingPlatformsSpeed = 0.3f;
}
