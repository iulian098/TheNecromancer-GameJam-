using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/Game Data")]
public class GameData : ScriptableObject
{
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] LayerMask mousePositionMask;
    [SerializeField] AudioClip[] footstepClips;
    [SerializeField] GameObject enemyHealthBar;
    [SerializeField] MovementType movementType;

    public LayerMask PlayerMask => playerMask;
    public LayerMask EnemyMask => enemyMask;
    public LayerMask MousePositionMask => mousePositionMask;
    public AudioClip[] FootstepClips => footstepClips;
    public GameObject EnemyHealthBar => enemyHealthBar;
    public MovementType MovementType { get => movementType; set => movementType = value; }
}

public enum MovementType {
    Keyboard,
    Mouse
}
