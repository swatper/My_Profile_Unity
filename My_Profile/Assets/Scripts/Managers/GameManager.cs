using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Managers
    public static GameManager GM_Instance;
    public static GameManager Instance { get { Init(); return GM_Instance; } }

    InputManager M_Input = new InputManager();
    public static InputManager Input { get { return Instance.M_Input; } }
    #endregion

    public TimeUpdater TimeUpdater = new TimeUpdater();
    public static TimeUpdater Clock { get { return Instance.TimeUpdater; } }

    public PlayerController pController;
    public static PlayerController Player { get { return Instance.pController; } }

    static void Init()
    {
        if (GM_Instance == null)
        {
            GameObject gmObject = GameObject.Find("GameManager");
            if (gmObject == null)
            {
                gmObject = new GameObject { name = "GameManager" };
                gmObject.AddComponent<GameManager>();
            }
            GM_Instance = gmObject.GetComponent<GameManager>();
            DontDestroyOnLoad(gmObject);
        }
    }

    private void Awake()
    {
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            pController = playerObject.GetComponent<PlayerController>();
        }
        StartCoroutine(UpdateTimePerSec());
    }

    private void Update() {
        Input.KeyEvent();
    }

    IEnumerator UpdateTimePerSec() {
        while (true) {
            TimeUpdater.UpdateTime();
            yield return new WaitForSeconds(1.0f);
        }
    }
}
