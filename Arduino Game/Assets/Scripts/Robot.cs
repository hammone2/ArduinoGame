using UnityEngine;

public class Robot : MonoBehaviour
{
    public void Die()
    {
        CarSceneManager.Instance.UpdateKillsText();
        Destroy(gameObject);
    }
}
