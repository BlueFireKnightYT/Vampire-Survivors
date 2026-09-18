using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Timer : MonoBehaviour
{
    TextMeshProUGUI timer;

    public int startTime = 0;
    public float survivingTime;

    int minutes;
    int seconds;

    private void Start()
    {
        timer = GetComponentInChildren<TextMeshProUGUI>();
        survivingTime = startTime;
    }

    private void Update()
    {
        survivingTime += Time.deltaTime;
        minutes = Mathf.FloorToInt(survivingTime / 60);
        seconds = Mathf.FloorToInt(survivingTime % 60);

        timer.text = minutes.ToString("D2") + ":" + seconds.ToString("D2");
    }
}
