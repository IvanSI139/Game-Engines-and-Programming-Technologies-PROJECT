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
    public enemy_AI enemy_AI { get; set; }
    public rangedAI rangedAI { get; set; }
    public projectile projectile { get; set; }

    public EnemyHealth EnemyHealth { get; set; }

    public EnemyProjectile EnemyProjectile { get; set; }

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

        Health = FindObjectOfType<Health>();
        if (Health == null)
        {
            Debug.LogError("Health not found in the scene!");
        }

        enemy_AI = FindObjectOfType<enemy_AI>();
        if (enemy_AI == null)
        {
            Debug.LogError("enemy_AI not found in the scene!");
        }

        rangedAI = FindObjectOfType<rangedAI>();
        if (rangedAI == null)
        {
            Debug.LogError("rangedAI not found in the scene!");
        }

        EnemyHealth = FindObjectOfType<EnemyHealth>();
        if (EnemyHealth == null)
        {
            Debug.LogError("EnemyHealth not found in the scene!");
        }

        projectile = FindObjectOfType<projectile>();
        if (projectile == null)
        {
            Debug.LogError("projectile not found in the scene!");
        }

        EnemyProjectile = FindObjectOfType<EnemyProjectile>();
        if (EnemyProjectile == null)
        {
            Debug.LogError("EnemyProjectile not found in the scene!");
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
