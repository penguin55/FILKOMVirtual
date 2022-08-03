using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    [Header("Bound")]
    [SerializeField] private CameraBound horizontalBound;
    [SerializeField] private CameraBound verticalBound;

    [Header("Properties")]
    [SerializeField] private CameraProperties cameraPropsMinimap;
    [SerializeField] private CameraProperties cameraPropsMap;

    [Header("References")]
    [SerializeField] private Transform targetPlayer;
    [SerializeField] private RenderTexture textureMap;
    [SerializeField] private Camera cameraMap;
    [SerializeField] private GameObject mapPanel;

    [SerializeField] private RawImage minimapImage;
    [SerializeField] private RawImage mapImage;

    Texture2D minimapResult;
    Texture2D mapResult;

    private bool rendering;
    private bool openingMap;

    public void OpenMap(bool open)
    {
        if (rendering) return;
        mapPanel.SetActive(open);
        openingMap = open;
        if (open)
        {
            cameraPropsMinimap.position = transform.position;
            StartCoroutine(RenderedTexture());
        }
    }

    IEnumerator RenderedTexture()
    {
        rendering = true;
        yield return new WaitForEndOfFrame();

        var old_rt = RenderTexture.active;
        RenderTexture.active = (RenderTexture) textureMap;

        minimapResult = new Texture2D(textureMap.width, textureMap.height, TextureFormat.ARGB32, false);
        minimapResult.ReadPixels(new Rect(0, 0, textureMap.width, textureMap.height), 0, 0);
        minimapResult.Apply();

        minimapImage.texture = minimapResult;

        //Move Cam
        transform.position = cameraPropsMap.position;
        cameraMap.orthographicSize = cameraPropsMap.distance;

        RenderTexture.active.Release();
        RenderTexture.active = old_rt;

        yield return new WaitForEndOfFrame();

        old_rt = RenderTexture.active;
        RenderTexture.active = (RenderTexture)textureMap;

        mapResult = new Texture2D(textureMap.width, textureMap.height, TextureFormat.ARGB32, false);
        mapResult.ReadPixels(new Rect(0, 0, textureMap.width, textureMap.height), 0, 0);
        mapResult.Apply();

        RenderTexture.active.Release();
        RenderTexture.active = old_rt;

        yield return new WaitForEndOfFrame();
        //Move back cam
        transform.position = cameraPropsMinimap.position;
        cameraMap.orthographicSize = cameraPropsMinimap.distance;

        mapImage.texture = mapResult;

        minimapImage.texture = textureMap;

        rendering = false;
    }

    private void Update()
    {
        if (!targetPlayer || openingMap) return;

        transform.position = new Vector3(ClampMap(targetPlayer.position.x, verticalBound), transform.position.y, ClampMap(targetPlayer.position.z, horizontalBound));
    }

    private float ClampMap(float value, CameraBound bound)
    {
        return Mathf.Clamp(value, bound.min, bound.max);
    }

    [System.Serializable]
    public class CameraProperties
    {
        public Vector3 position;
        public float distance;
    }

    [System.Serializable]
    public class CameraBound
    {
        public float min;
        public float max;
    }
}
