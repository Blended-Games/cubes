using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationMTO : MonoBehaviour
{
    public static VibrationMTO Instante;

    private void Awake()
    {
        Instante = this;
    }

    public void VibrateStandart()
    {
#if UNITY_IOS
        Vibration.VibratePop();
#elif UNITY_ANDROID
        Vibration.Vibrate(15);
#endif
    }

    public void VibrationSuccess()
    {
#if UNITY_IOS
        Vibration.VibratePop();
#elif UNITY_ANDROID
        Vibration.Vibrate(15);
#endif
    }

    public void VibrationFail()
    {
#if UNITY_IOS
        Vibration.VibratePop();
#elif UNITY_ANDROID
        Vibration.Vibrate(15);
#endif
    }
}
