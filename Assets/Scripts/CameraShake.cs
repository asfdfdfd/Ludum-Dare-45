using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shake(float height)
    {
        float shakeAmt = height * 0.2f; // the degrees to shake the camera
        float shakePeriodTime = 0.42f; // The period of each shake
        float dropOffTime = 1.6f; // How long it takes the shaking to settle down to nothing
        LTDescr shakeTween = LeanTween.rotateAroundLocal(gameObject, Vector3.right, shakeAmt, shakePeriodTime)
        .setEase(LeanTweenType.easeShake) // this is a special ease that is good for shaking
        .setLoopClamp()
        .setRepeat(-1);

        // Slow the camera shake down to zero
        LeanTween.value(gameObject, shakeAmt, 0f, dropOffTime).setOnUpdate(
            (float val) => {
                shakeTween.setTo(Vector3.right * val);
            }
        ).setEase(LeanTweenType.easeOutQuad);
    }

    public void ShakeABit()
    {
        //LeanTween.moveY(gameObject, gameObject.transform.position.y + 1f, 0.2f).setEase(LeanTweenType.easeShake).setLoopClamp().setRepeat(-1);
        //LeanTween.moveX(gameObject, gameObject.transform.position.x + 0.6f, 0.25f).setEase(LeanTweenType.easeShake).setLoopClamp().setRepeat(-1);
    }
}
