using TMPro;
using TomGustin.GameDesignPattern;
using UnityEngine;
using UnityEngine.UI;

public class CameraGadgetUI : MonoBehaviour
{
    [SerializeField] private GameObject signPanel;
    [SerializeField] private TextMeshProUGUI sign;
    [SerializeField] private GameObject cameraUI;
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject cameraButtons;
    [SerializeField] private GameObject mainButtons;
    [SerializeField] private TextMeshProUGUI debugText;

    [Header("Gallery Panel")]
    [SerializeField] private RawImage photoPlacement;
    [SerializeField] private GameObject resultPhoto;
    [SerializeField] private GameObject savePanelConfirmation;
    [SerializeField] private GameObject deletePanelConfirmation;
    [SerializeField] private GameObject infoConfirmation;
    [SerializeField] private TextMeshProUGUI infoConfirmationText;
    [SerializeField] private GameObject tapText;
    [SerializeField] private GameObject confirmationPanel;

    private CameraGadget _gadget;
    private CameraGadget gadget
    {
        get
        {
            if (!_gadget) _gadget = ServiceLocator.Resolve<CameraGadget>();
            return _gadget;
        }
    }

    private bool last_state;

    private void Start()
    {
        if (AccountManager.Instance.IsAccountNotLoggedIn()) return;
    }

    public void OpenCameraUI(bool open)
    {
        OpenCamera(open);
    }

    public void ChangeSign(bool inside_camera)
    {
        if (inside_camera && last_state) return;

        if (inside_camera)
        {
            sign.color = new Color(0.3f, 1f, 0f);
            sign.text = "Objek berada di dalam area kamera.";
        }
        else 
        {
            sign.color = new Color(1f, 0.3f, 0f);
            sign.text = "Objek berada di luar area kamera.";
        }

        last_state = inside_camera;
    }

    public void EnableSign(bool enable)
    {
        signPanel.SetActive(enable);
    }

    public void EnableDebug(bool enable)
    {
        debugText.enabled = enable;
    }

    public void UpdateDebugCoord(Vector3 viewport, float time)
    {
        debugText.text = $"X: {viewport.x} | Y: {viewport.y} | Z: {viewport.z} |||| Time: {time}";
    }

    #region Camera Panel Confirmation
    public void OpenResult(bool open)
    {
        cameraUI.SetActive(!open);
        cameraButtons.SetActive(!open);
        resultPhoto.SetActive(open);
    }

    public void RenderResult(Texture2D texture2D)
    {
        photoPlacement.texture = texture2D;
    }

    public void SavePanelConfirmationOpen(bool open)
    {
        confirmationPanel.SetActive(open);
        savePanelConfirmation.SetActive(open);
    }

    public void DeletePanelConfirmationOpen(bool open)
    {
        confirmationPanel.SetActive(open);
        deletePanelConfirmation.SetActive(open);
    }

    public void Save()
    {
        StartCoroutine(gadget.SaveCapture(() =>
        {
            tapText.SetActive(true);
            infoConfirmationText.text = "Berhasil disimpan dalam galery!";
        }));
        savePanelConfirmation.SetActive(false);
        infoConfirmation.SetActive(true);
        tapText.SetActive(false);
        infoConfirmationText.text = "Menyimpan...";
    }

    public void Delete()
    {
        deletePanelConfirmation.SetActive(false);
        infoConfirmation.SetActive(true);
        infoConfirmationText.text = "Berhasil dihapus!";
    }

    public void TapEverywhereInfoConfirmation()
    {
        if (gadget.IsSaving()) return;
        infoConfirmation.SetActive(false);
        SavePanelConfirmationOpen(false);
        DeletePanelConfirmationOpen(false);
        OpenResult(false);
    }
    #endregion

    private void OpenCamera(bool open)
    {
        cameraUI.SetActive(open);
        mainUI.SetActive(!open);
        cameraButtons.SetActive(open);
        mainButtons.SetActive(!open);
    }
}
