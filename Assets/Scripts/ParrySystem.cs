using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class ParrySystem : MonoBehaviour
{
    private bool isParryActive = false;
    private float parryDuration = 1f;
    private float parryCooldown = 5f;
    private float lastParryTime = -999f;

    private Volume postProcessVolume;
    private Vignette vignette;
    private Camera mainCam;
    private float originalCamSize;
    private Vector3 originalCamPos;
    private TwoPlayerCamera twoPlayerCam;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        twoPlayerCam = FindObjectOfType<TwoPlayerCamera>();
        postProcessVolume = FindObjectOfType<Volume>();
        if (postProcessVolume != null)
            postProcessVolume.profile.TryGet(out vignette);
        // 게임 시작 시 비네트 효과 끄기
        if (vignette != null)
            vignette.intensity.Override(0f);

        mainCam = Camera.main;
        if (mainCam != null)
        {
            originalCamPos = mainCam.transform.position;
            originalCamSize = mainCam.orthographicSize;
        }
    }

    public void ActivateParry()
    {
        if (Time.time - lastParryTime > parryCooldown)
        {
            isParryActive = true;
            lastParryTime = Time.time;
            Invoke(nameof(DeactivateParry), parryDuration);
        }
    }

    public bool IsParryActive() => isParryActive;

    private void DeactivateParry()
    {
        isParryActive = false;
    }

    public void OnParrySuccess()
    {
        StartCoroutine(PlayParryEffects());
        
    }

    

    private IEnumerator PlayParryEffects()
    {
        if (twoPlayerCam != null)
            twoPlayerCam.enabled = false;
        // 1. 슬로우 모션
        Time.timeScale = 0.2f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        // 2. 카메라 줌 + Y축 이동
        float zoomedSize = originalCamSize * 0.9f;
        Vector3 shiftedCamPos = mainCam != null ? mainCam.transform.position : originalCamPos;
        shiftedCamPos.y -= 5f; // y 위치를 조금 아래로 이동

        if (mainCam != null)
        {
            mainCam.orthographicSize = zoomedSize;
            mainCam.transform.position = shiftedCamPos;
        }

        // 3. 비네트 효과
        if (vignette != null)
        {
            vignette.intensity.Override(0.5f);
        }

        // 4. 연출 시간 유지
        yield return new WaitForSecondsRealtime(0.6f);

        // 5. 원상복구(플레이어 기준으로 부드럽게)
        float restoreTime = 0.3f;
        float t = 0f;
        float startSize = mainCam.orthographicSize;
        Vector3 startPos = mainCam.transform.position;
        float targetSize = originalCamSize;
        Vector3 targetPos = originalCamPos;
        // 플레이어 기준으로 복귀
        if (twoPlayerCam != null && twoPlayerCam.player1 != null && twoPlayerCam.player2 != null)
        {
            Vector3 middle = (twoPlayerCam.player1.position + twoPlayerCam.player2.position) * 0.5f;
            float distance = Vector2.Distance(twoPlayerCam.player1.position, twoPlayerCam.player2.position);
            float zoomFactor = twoPlayerCam.zoomFactor;
            float minSize = twoPlayerCam.minSize;
            float maxSize = twoPlayerCam.maxSize;
            targetSize = Mathf.Clamp(distance * zoomFactor, minSize, maxSize);
            targetPos = new Vector3(middle.x, middle.y + twoPlayerCam.yOffset, startPos.z);
        }
        while (t < restoreTime)
        {
            t += Time.unscaledDeltaTime;
            float lerp = Mathf.Clamp01(t / restoreTime);
            if (mainCam != null)
            {
                mainCam.orthographicSize = Mathf.Lerp(startSize, targetSize, lerp);
                mainCam.transform.position = Vector3.Lerp(startPos, targetPos, lerp);
            }
            yield return null;
        }
        if (mainCam != null)
        {
            mainCam.orthographicSize = targetSize;
            mainCam.transform.position = targetPos;
        }

        // 슬로우모션 복구
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        if (vignette != null)
        {
            vignette.intensity.Override(0f);
        }

        if (twoPlayerCam != null)
            twoPlayerCam.enabled = true;
    }
}
