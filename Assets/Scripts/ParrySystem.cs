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
        // var twoPlayerCam = FindObjectOfType<TwoPlayerCamera>();
        // Vector3 shiftedCamPos = originalCamPos;
        // if (twoPlayerCam != null)
        // {
        //     shiftedCamPos.y = twoPlayerCam.transform.position.y;
        // }
        // if (mainCam != null)
        // {
        //     mainCam.orthographicSize = zoomedSize;
        //     mainCam.transform.position = shiftedCamPos;
        // }

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

        // 5. 원상복구
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        if (mainCam != null)
        {
            mainCam.orthographicSize = originalCamSize;
            mainCam.transform.position = originalCamPos;
        }

        if (vignette != null)
        {
            vignette.intensity.Override(0f);
        }

        if (twoPlayerCam != null)
            twoPlayerCam.enabled = true;
            
    }
}
