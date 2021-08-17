using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public Animator pointerAnimator;
    public Animator playerAnimator;

    public GameObject partOne;
    public GameObject partTwo;

    public void OnNext()
    {
        partOne.SetActive(false);
        partTwo.SetActive(true);

        pointerAnimator.SetTrigger("Next");
        playerAnimator.SetTrigger("Next");
    }

    public void OnBack()
    {
        partOne.SetActive(true);
        partTwo.SetActive(false);

        pointerAnimator.SetTrigger("Back");
        playerAnimator.SetTrigger("Back");
    }
}
