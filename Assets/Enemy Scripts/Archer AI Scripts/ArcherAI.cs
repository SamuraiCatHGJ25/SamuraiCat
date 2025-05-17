using UnityEngine;

public class ArcherAI : MonoBehaviour
{
    [SerializeField] private Animator archerAnimator;
    [SerializeField] private float resetTime;

    public void makeArcherShoot(float delay)
    {
        if(archerAnimator.GetBool("Shooting") == false)
        {
            archerAnimator.SetBool("Shooting", true);
            CancelInvoke(nameof(SetToIdle));
            Invoke(nameof(SetToIdle), resetTime);
        }
    }

    private void SetToIdle()
    {
        archerAnimator.SetBool("Shooting", false);
    }
}
