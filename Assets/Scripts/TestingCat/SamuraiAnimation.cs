using UnityEngine;

public class SamuraiAnimation : MonoBehaviour
{
    private static readonly int IsRunning = Animator.StringToHash("IsRunning");

    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;

    public void UpdateAnimation(bool isRunning)
    {
        _animator.SetBool(IsRunning, isRunning);
    }
}