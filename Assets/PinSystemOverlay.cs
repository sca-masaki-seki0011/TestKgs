using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSystemOverlay : MonoBehaviour
{
    private Camera targetCamera; // âfÇ¡ÇƒÇ¢ÇÈÇ©îªíËÇ∑ÇÈÉJÉÅÉâÇ÷ÇÃéQè∆
    private Transform canvas;
    [SerializeField]
    private GameObject pinPrefab;
    private GameObject pinUI;

    void Start() {
        targetCamera = Camera.main;
        canvas = GameObject.Find("MissionObjectUI").transform;
        pinUI = Instantiate(pinPrefab, canvas);
        pinUI.transform.SetParent(canvas);
        Vector2 screenPosition = GetScreenPosition(transform.position);
        Vector2 localPosition = GetCanvasLocalPosition(screenPosition);
        pinUI.transform.localPosition = localPosition;
    }

    void LateUpdate() {
        Vector3 offset = targetCamera.transform.position - transform.position;
        Vector3 normal = offset.normalized;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, normal, out hit, 20)) {
            Vector3 cameraNormal = Vector3.Normalize(transform.position - targetCamera.transform.position);
            float dot = Vector3.Dot(cameraNormal, targetCamera.transform.forward);
            if(hit.collider.gameObject.name == targetCamera.gameObject.name && dot > 0.6f) {
                pinUI.SetActive(true);
                Vector2 screenPosition = GetScreenPosition(transform.position);
                Vector2 localPosition = GetCanvasLocalPosition(screenPosition);
                pinUI.transform.localPosition = localPosition;
            } else {
                pinUI.SetActive(false);
            }
        }
    }
    /* Ç‡ÇµîªíËÇ™Ç®Ç©ÇµÇØÇÍÇŒÇ±ÇøÇÁÇ‡çáÇÌÇπÇƒÇ¬ÇØÇƒÇ›ÇƒÇ≠ÇæÇ≥Ç¢ÅB
        private void OnBecameVisible()
        {
            pinUI.SetActive(true);
        }
        private void OnBecameInvisible()
        {
            pinUI.SetActive(false);
        }
    */
    private Vector2 GetCanvasLocalPosition(Vector2 screenPosition) {
        return canvas.transform.InverseTransformPoint(screenPosition);
    }

    private Vector2 GetScreenPosition(Vector3 worldPosition) {
        return RectTransformUtility.WorldToScreenPoint(targetCamera, worldPosition);
    }
}
