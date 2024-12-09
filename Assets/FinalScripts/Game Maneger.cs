using UnityEngine;

public class GameManeger : MonoBehaviour
{
    private static GameManeger instance;

    public static GameManeger Instance
    {
        get
        {
            if (instance == null)
            {
                // Attempt to find an existing GameManeger in the scene
                instance = FindObjectOfType<GameManeger>();

                if (instance == null)
                {
                    // If none exists, log an error and optionally create one
                    Debug.LogError("No GameManeger instance found in the scene!");
                    return null;
                }
            }

            return instance;
        }
    }

    public PlayerController PlayerController { get; set; }
    public Health Health { get; set; }
    public enemy_AI Enemy_AI { get; set; }
    public rangedAI RangedAI { get; set; }
    public EnemyHealth EnemyHealth { get; set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        PlayerController = FindObjectOfType<PlayerController>();
        if (PlayerController == null)
        {
            Debug.LogError("PlayerController not found in the scene!");
        }
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown("f5"))
        {
            Debug.Log("f5");
            SaveSystem.Save();
        }

        if (Input.GetKeyDown("f9"))
        {
            Debug.Log("f9");
            SaveSystem.Load();
        }
#endif
    }
}
