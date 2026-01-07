using UnityEngine;

public class SetFullscreen : MonoBehaviour
{
    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
