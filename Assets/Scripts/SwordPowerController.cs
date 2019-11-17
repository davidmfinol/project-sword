using UnityEngine;

public class SwordPowerController : MonoBehaviour
{
    public GameObject leftHand;
    public GameObject leftSword;
    public GameObject rightHand;
    public GameObject rightSword;

    public bool LeftPower => Input.GetButton("Left Power");
    public bool LeftSwordGrip => Input.GetButton("Left SwordGrip");
    public bool RightPower => Input.GetButton("Right Power");
    public bool RightSwordGrip => Input.GetButton("Right SwordGrip");

    void Update()
    {
        leftSword.SetActive(LeftSwordGrip);
        rightSword.SetActive(RightSwordGrip);
    }
}
