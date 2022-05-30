using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/Game Data")]
public class GameData : ScriptableObject
{
    [SerializeField] LayerMask playerMask;
    [SerializeField] LayerMask enemyMask;
    [SerializeField] AudioClip[] footstepClips;
    [SerializeField] GameObject enemyHealthBar;

    public LayerMask PlayerMask => playerMask;
    public LayerMask EnemyMask => enemyMask;
    public AudioClip[] FootstepClips => footstepClips;
    public GameObject EnemyHealthBar => enemyHealthBar;

}
