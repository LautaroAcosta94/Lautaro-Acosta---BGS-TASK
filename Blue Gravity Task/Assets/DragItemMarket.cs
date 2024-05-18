using UnityEngine;
using UnityEngine.EventSystems;

public class DragItemMarket : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public enum SpecificItemType
    {
        CrimsonArmor,
        CrimsonHood,
        IronBoots,
        IronGloves,
        LeatherArmor,
        LeatherHood,
        LeatherBoots,
        LeatherGloves
    }

    public SpecificItemType specificItemType;

    public GameObject CrimsonArmorPrefab;
    public GameObject CrimsonHoodPrefab;
    public GameObject IronBootsPrefab;
    public GameObject IronGlovesPrefab;
    public GameObject LeatherArmorPrefab;
    public GameObject LeatherHoodPrefab;
    public GameObject LeatherBootsPrefab;
    public GameObject LeatherGlovesPrefab;

    public GameObject monedero;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private bool isDragging = false;
    private Transform originalParent;
    private Vector2 originalSize;
    private Vector3 originalScale;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalSize = rectTransform.sizeDelta; // Guardar el tamaño original del objeto
        originalScale = rectTransform.localScale; // Guardar la escala original del objeto
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.CompareTag("ItemMarket"))
        {
            isDragging = true;
            originalParent = transform.parent;
            transform.SetParent(canvas.transform, true); // Hacer el objeto hijo directo del Canvas.
            canvasGroup.blocksRaycasts = false; // Desactiva los raycasts para permitir el arrastre.
            canvasGroup.alpha = 0.6f; // Opcional: cambiar la opacidad para dar feedback visual.
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;
        canvasGroup.blocksRaycasts = true; // Reactiva los raycasts cuando se suelta el objeto.
        canvasGroup.alpha = 1.0f; // Restaurar la opacidad.

        // Encontrar el slot más cercano basado en la posición del objeto arrastrado
        GameObject nearestSlot = FindNearestSlot();
        if (nearestSlot != null)
        {
            // Instanciar el prefab predefinido basado en el tipo específico del objeto
            GameObject prefabToInstantiate = GetPrefabToInstantiate(specificItemType);
            if (prefabToInstantiate != null)
            {
                Instantiate(prefabToInstantiate, nearestSlot.transform.position, Quaternion.identity, nearestSlot.transform);
            }

            // Restar el valor específico del monedero
            DeductValue(specificItemType);

            // Volver el objeto original a su posición original
            transform.SetParent(originalParent, false);
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = originalSize; // Mantener el tamaño original del objeto
            rectTransform.localScale = originalScale; // Mantener la escala original del objeto
        }
        else
        {
            transform.SetParent(originalParent, false); // Si no hay slot cercano, volver al padre original.
            rectTransform.anchoredPosition = Vector2.zero;
            rectTransform.sizeDelta = originalSize; // Mantener el tamaño original del objeto
            rectTransform.localScale = originalScale; // Mantener la escala original del objeto
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragging)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out localPoint);
            rectTransform.localPosition = localPoint;
        }
    }

    private GameObject FindNearestSlot()
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
        GameObject nearestSlot = null;
        float minDistance = float.MaxValue;

        foreach (GameObject slot in slots)
        {
            float distance = Vector2.Distance(transform.position, slot.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestSlot = slot;
            }
        }

        // Puedes ajustar este umbral según sea necesario
        float thresholdDistance = 100.0f;
        if (minDistance <= thresholdDistance)
        {
            return nearestSlot;
        }
        else
        {
            return null;
        }
    }

    private GameObject GetPrefabToInstantiate(SpecificItemType itemType)
    {
        switch (itemType)
        {
            case SpecificItemType.CrimsonArmor:
                return CrimsonArmorPrefab;
            case SpecificItemType.CrimsonHood:
                return CrimsonHoodPrefab;
            case SpecificItemType.IronBoots:
                return IronBootsPrefab;
            case SpecificItemType.IronGloves:
                return IronGlovesPrefab;
            case SpecificItemType.LeatherArmor:
                return LeatherArmorPrefab;
            case SpecificItemType.LeatherHood:
                return LeatherHoodPrefab;
            case SpecificItemType.LeatherBoots:
                return LeatherBootsPrefab;
            case SpecificItemType.LeatherGloves:
                return LeatherGlovesPrefab;
            default:
                return null;
        }
    }

    private void DeductValue(SpecificItemType itemType)
    {
        var intToTextMeshPro = monedero.GetComponent<IntToTextMeshPro>();

        if (intToTextMeshPro != null)
        {
            switch (itemType)
            {
                case SpecificItemType.CrimsonArmor:
                    intToTextMeshPro.value -= 60;
                    break;
                case SpecificItemType.CrimsonHood:
                    intToTextMeshPro.value -= 30;
                    break;
                case SpecificItemType.IronBoots:
                    intToTextMeshPro.value -= 20;
                    break;
                case SpecificItemType.IronGloves:
                    intToTextMeshPro.value -= 10;
                    break;
                case SpecificItemType.LeatherArmor:
                    intToTextMeshPro.value -= 25;
                    break;
                case SpecificItemType.LeatherHood:
                    intToTextMeshPro.value -= 15;
                    break;
                case SpecificItemType.LeatherBoots:
                    intToTextMeshPro.value -= 10;
                    break;
                case SpecificItemType.LeatherGloves:
                    intToTextMeshPro.value -= 5;
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.LogWarning("IntToTextMeshPro component not found on monedero.");
        }
    }
}
