using UnityEngine;

public class CharacterSelectData : MonoBehaviour
{
    public static CharacterSelectData Instance;

    public GameObject p1Prefab;
    public GameObject p2Prefab;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 넘어가도 유지
        }
        else
        {
            Destroy(gameObject); // 중복 방지
        }
    }


}
