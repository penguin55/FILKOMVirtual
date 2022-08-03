using Sirenix.OdinInspector;
using System.Collections;
using UnityTaskManager;
using UnityEngine;
using TomGustin.GameDesignPattern;
using UnityEngine.Events;
using System.IO;

public class CameraGadget : MonoBehaviour
{
    public bool debug;
    [SerializeField] private Camera cameraMain;
    [SerializeField] private Camera screenshotCamera;
    [SerializeField] private UnityEvent onEquip;
    [SerializeField] private UnityEvent onCapture;
    
    private Transform modelTargetDetection;
    private CameraTaskField activeCameraTaskField;

    [SerializeField, ReadOnly] private bool active;
    [SerializeField, ReadOnly] private bool inCameraField;
    Task gadgetTask;

    private CameraGadgetUI cgui;
    private PlayerController pc;

    Texture2D renderResult;
    bool screenshot;
    bool saving;
    bool equipped;

    private void Awake()
    {
        //cameraMain = Camera.main;
        cgui = ServiceLocator.Resolve<CameraGadgetUI>();
        pc = ServiceLocator.Resolve<PlayerController>();
    }

    public void SetCameraTargetDetection(Transform modelTargetDetection, CameraTaskField cameraTaskField)
    {
        activeCameraTaskField = cameraTaskField;
        this.modelTargetDetection = modelTargetDetection;
        if (active) UnequipGadget();
    }

    public void Capture()
    {
        if (screenshot) return;
        screenshot = true;

        StartCoroutine(TakeCapture());
    }

    public void EquipGadget()
    {
        equipped = true;
        cgui.EnableSign(modelTargetDetection);
        onEquip?.Invoke();
        if (modelTargetDetection) gadgetTask = new Task(StartDetection());
        else
        {
            screenshotCamera.gameObject.SetActive(true);
            cgui.OpenCameraUI(true);
            pc.Pause(true);
            active = true;
        }
    }

    public void UnequipGadget()
    {
        active = false;
        cgui.OpenCameraUI(false);
        pc.Pause(false);
        screenshotCamera.gameObject.SetActive(false);
        gadgetTask?.Stop();
        equipped = false;
    }

    public IEnumerator SaveCapture(UnityAction onComplete)
    {
        saving = true;
        string filename = string.Format("{0}_Capture_{1}.png", Application.productName, System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));

        NativeGallery.SaveImageToGallery(renderResult, "FILKOM Virtual", filename);
        /*byte[] itemBGBytes = renderResult.EncodeToPNG();
        File.WriteAllBytes($"{Application.persistentDataPath}/{filename}", itemBGBytes);
        print($"{Application.persistentDataPath}/{filename}");*/

        yield return null;
        onComplete?.Invoke();
        
        activeCameraTaskField?.ClearCapture();
        saving = false;
    }

    public bool IsSaving()
    {
        return saving;
    }

    public bool IsEquipped()
    {
        return equipped;
    }

    IEnumerator TakeCapture()
    {
        RenderTexture renderTexture = screenshotCamera.activeTexture;

        yield return new WaitForEndOfFrame();

        var old_rt = RenderTexture.active;
        RenderTexture.active = (RenderTexture) renderTexture;

        renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        renderResult.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        renderResult.Apply();

        cgui.OpenResult(true);
        cgui.RenderResult(renderResult);
        RenderTexture.active = old_rt;
        screenshot = false;
        onCapture?.Invoke();
    }

    IEnumerator StartDetection()
    {
        if (!modelTargetDetection || !cameraMain) yield break;

        screenshotCamera.gameObject.SetActive(true);
        cgui.OpenCameraUI(true);
        pc.Pause(true);
        active = true;

        cgui.EnableDebug(debug);

        while (true)
        {
            if (!modelTargetDetection) break;
            Vector3 viewport = cameraMain.WorldToViewportPoint(modelTargetDetection.position);

            if (debug) cgui.UpdateDebugCoord(viewport, Time.time);

            bool inCameraFrustum = Is01(viewport.x) && Is01(viewport.y);
            bool inFrontOfCamera = viewport.z > 0;

            inCameraField = inFrontOfCamera && inCameraFrustum;
            cgui.ChangeSign(inCameraField);

            yield return null;
        }
    }

    private bool Is01(float a)
    {
        return a > 0 && a < 1;
    }
}
