using UnityEngine;
using UnityEngine.EventSystems;

public class DragItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public enum ItemType
    {
        Armor,
        Hood,
        Boots,
        Gloves
    }

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

    public ItemType itemType;
    public SpecificItemType specificItemType;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private bool isDragging = false;
    private Transform originalParent;
    private Vector2 originalSize;
    private Vector3 originalScale;

    //This is to change the armor

    //Here are the Sprites Renderer wich i want to change the sprites

    //Armor
    public SpriteRenderer Torso;
    public SpriteRenderer Pelvis;
    public SpriteRenderer ShoulderRight;
    public SpriteRenderer ShoulderLeft;
    public SpriteRenderer ElbowRight;
    public SpriteRenderer ElbowLeft;

    //Hood
    public SpriteRenderer Hood;

    //Boots
    public SpriteRenderer RightBoot;
    public SpriteRenderer LeftBoot;

    //Gloves
    public SpriteRenderer RightGlove;
    public SpriteRenderer LeftGlove;

    //This is to change the InventoryArmor

    //Here are the Sprites Renderer wich i want to change the sprites

    //Armor
    public SpriteRenderer InventoryTorso;
    public SpriteRenderer InventoryPelvis;
    public SpriteRenderer InventoryShoulderRight;
    public SpriteRenderer InventoryShoulderLeft;
    public SpriteRenderer InventoryElbowRight;
    public SpriteRenderer InventoryElbowLeft;

    //Hood
    public SpriteRenderer InventoryHood;

    //Boots
    public SpriteRenderer InventoryRightBoot;
    public SpriteRenderer InventoryLeftBoot;

    //Gloves
    public SpriteRenderer InventoryRightGlove;
    public SpriteRenderer InventoryLeftGlove;

    //This is all the Sprites i want to use to change the armor

    //Base Amor

    public Sprite NormalTorso;
    public Sprite NormalPelvis;
    public Sprite NormalShoulderRight;
    public Sprite NormalShoulderLeft;
    public Sprite NormalElbowRight;
    public Sprite NormalElbowLeft;

    //Base Hood
    public Sprite NormalHood;

    //BaseBoots
    public Sprite NormalRightBoot;
    public Sprite NormalLeftBoot;

    //BaseGloves
    public Sprite NormalRightGlove;
    public Sprite NormalLeftGlove;

    //Crimson Armor Sprites

    public Sprite CrimsonTorso;
    public Sprite CrimsonPelvis;
    public Sprite CrimsonShoulderRight;
    public Sprite CrimsonShoulderLeft;
    public Sprite CrimsonElbowRight;
    public Sprite CrimsonElbowLeft;

    //Crimson Hood
    public Sprite CrimsonHood;

    //Crimson Boots
    public Sprite CrimsonRightBoot;
    public Sprite CrimsonLeftBoot;

    //Crimson Gloves
    public Sprite CrimsonRightGlove;
    public Sprite CrimsonLeftGlove;

    //Leather Armor Sprites

    public Sprite LeatherTorso;
    public Sprite LeatherPelvis;
    public Sprite LeatherShoulderRight;
    public Sprite LeatherShoulderLeft;
    public Sprite LeatherElbowRight;
    public Sprite LeatherElbowLeft;

    //Leather Hood
    public Sprite LeatherHood;

    //Leather Boots
    public Sprite LeatherRightBoot;
    public Sprite LeatherLeftBoot;

    //Leather Gloves
    public Sprite LeatherRightGlove;
    public Sprite LeatherLeftGlove;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalSize = rectTransform.sizeDelta; // Guardar el tamaño original del objeto
        originalScale = rectTransform.localScale; // Guardar la escala original del objeto

        // Asign Automatic SpriteRenderers for armor
        Torso = GameObject.FindGameObjectWithTag("Torso").GetComponent<SpriteRenderer>();
        Pelvis = GameObject.FindGameObjectWithTag("Pelvis").GetComponent<SpriteRenderer>();
        ShoulderRight = GameObject.FindGameObjectWithTag("ShoulderRight").GetComponent<SpriteRenderer>();
        ShoulderLeft = GameObject.FindGameObjectWithTag("ShoulderLeft").GetComponent<SpriteRenderer>();
        ElbowRight = GameObject.FindGameObjectWithTag("ElbowRight").GetComponent<SpriteRenderer>();
        ElbowLeft = GameObject.FindGameObjectWithTag("ElbowLeft").GetComponent<SpriteRenderer>();
        Hood = GameObject.FindGameObjectWithTag("Hood").GetComponent<SpriteRenderer>();
        RightBoot = GameObject.FindGameObjectWithTag("RightBoot").GetComponent<SpriteRenderer>();
        LeftBoot = GameObject.FindGameObjectWithTag("LeftBoot").GetComponent<SpriteRenderer>();
        RightGlove = GameObject.FindGameObjectWithTag("RightGlove").GetComponent<SpriteRenderer>();
        LeftGlove = GameObject.FindGameObjectWithTag("LeftGlove").GetComponent<SpriteRenderer>();

        // Asign Automatic SpriteRenderers for InventoryArmor
        InventoryTorso = GameObject.FindGameObjectWithTag("InventoryTorso").GetComponent<SpriteRenderer>();
        InventoryPelvis = GameObject.FindGameObjectWithTag("InventoryPelvis").GetComponent<SpriteRenderer>();
        InventoryShoulderRight = GameObject.FindGameObjectWithTag("InventoryShoulderRight").GetComponent<SpriteRenderer>();
        InventoryShoulderLeft = GameObject.FindGameObjectWithTag("InventoryShoulderLeft").GetComponent<SpriteRenderer>();
        InventoryElbowRight = GameObject.FindGameObjectWithTag("InventoryElbowRight").GetComponent<SpriteRenderer>();
        InventoryElbowLeft = GameObject.FindGameObjectWithTag("InventoryElbowLeft").GetComponent<SpriteRenderer>();
        InventoryHood = GameObject.FindGameObjectWithTag("InventoryHood").GetComponent<SpriteRenderer>();
        InventoryRightBoot = GameObject.FindGameObjectWithTag("InventoryRightBoot").GetComponent<SpriteRenderer>();
        InventoryLeftBoot = GameObject.FindGameObjectWithTag("InventoryLeftBoot").GetComponent<SpriteRenderer>();
        InventoryRightGlove = GameObject.FindGameObjectWithTag("InventoryRightGlove").GetComponent<SpriteRenderer>();
        InventoryLeftGlove = GameObject.FindGameObjectWithTag("InventoryLeftGlove").GetComponent<SpriteRenderer>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.CompareTag("Item"))
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

        // Encontrar el slot vacío más cercano basado en el tipo de objeto
        GameObject nearestSlot = FindNearestSlot();
        if (nearestSlot != null)
        {
            // Si el slot más cercano está ocupado, encontrar el siguiente slot vacío más cercano
            while (nearestSlot.transform.childCount > 0)
            {
                nearestSlot = FindNextNearestSlot(nearestSlot);
                if (nearestSlot == null)
                {
                    break; // Si no se encuentran más slots vacíos, salir del bucle
                }
            }

            if (nearestSlot != null)
            {
                transform.SetParent(nearestSlot.transform, false); // Hacer el objeto hijo del slot más cercano.
                rectTransform.anchoredPosition = Vector2.zero; // Ajustar la posición para centrarlo en el slot.
                rectTransform.sizeDelta = originalSize; // Mantener el tamaño original del objeto
                rectTransform.localScale = originalScale; // Mantener la escala original del objeto

                // Debug.Log solo si el slot es específico
                if (nearestSlot.CompareTag("SlotArmor") || nearestSlot.CompareTag("SlotHood") || nearestSlot.CompareTag("SlotBoots") || nearestSlot.CompareTag("SlotGloves"))
                {
                    Debug.Log("El objeto es: " + specificItemType.ToString());

                    // Aquí defino la lógica para cambiar el sprite según el tipo específico del objeto
                    switch (specificItemType)
                    {
                        case SpecificItemType.CrimsonArmor:

                            //Player
                            Torso.sprite = CrimsonTorso;
                            Pelvis.sprite = CrimsonPelvis;
                            ShoulderRight.sprite = CrimsonShoulderRight;
                            ShoulderLeft.sprite = CrimsonShoulderLeft;
                            ElbowRight.sprite = CrimsonElbowRight;
                            ElbowLeft.sprite = CrimsonElbowLeft;

                            //InventoryView
                            InventoryTorso.sprite = CrimsonTorso;
                            InventoryPelvis.sprite = CrimsonPelvis;
                            InventoryShoulderRight.sprite = CrimsonShoulderRight;
                            InventoryShoulderLeft.sprite = CrimsonShoulderLeft;
                            InventoryElbowRight.sprite = CrimsonElbowRight;
                            InventoryElbowLeft.sprite = CrimsonElbowLeft;

                            break;
                        case SpecificItemType.CrimsonHood:

                            //Player
                            Hood.sprite = CrimsonHood;

                            //Inventory View
                            InventoryHood.sprite = CrimsonHood;

                            break;
                        case SpecificItemType.IronBoots:

                            //Player
                            RightBoot.sprite = CrimsonRightBoot;
                            LeftBoot.sprite = CrimsonLeftBoot;

                            //Inventory View
                            InventoryRightBoot.sprite = CrimsonRightBoot;
                            InventoryLeftBoot.sprite = CrimsonLeftBoot;

                            break;
                        case SpecificItemType.IronGloves:

                            //Player
                            RightGlove.sprite = CrimsonRightGlove;
                            LeftGlove.sprite = CrimsonLeftGlove;

                            //Inventory View
                            InventoryRightGlove.sprite = CrimsonRightGlove;
                            InventoryLeftGlove.sprite = CrimsonLeftGlove;

                            break;
                        case SpecificItemType.LeatherArmor:

                            //Player
                            Torso.sprite = LeatherTorso;
                            Pelvis.sprite = LeatherPelvis;
                            ShoulderRight.sprite = LeatherShoulderRight;
                            ShoulderLeft.sprite = LeatherShoulderLeft;
                            ElbowRight.sprite = LeatherElbowRight;
                            ElbowLeft.sprite = LeatherElbowLeft;

                            //Inventory View

                            InventoryTorso.sprite = LeatherTorso;
                            InventoryPelvis.sprite = LeatherPelvis;
                            InventoryShoulderRight.sprite = LeatherShoulderRight;
                            InventoryShoulderLeft.sprite = LeatherShoulderLeft;
                            InventoryElbowRight.sprite = LeatherElbowRight;
                            InventoryElbowLeft.sprite = LeatherElbowLeft;

                            break;
                        case SpecificItemType.LeatherHood:

                            //Player
                            Hood.sprite = LeatherHood;

                            //Inventory View
                            InventoryHood.sprite = LeatherHood;

                            break;
                        case SpecificItemType.LeatherBoots:

                            //Player
                            RightBoot.sprite = LeatherRightBoot;
                            LeftBoot.sprite = LeatherLeftBoot;

                            //Inventory View
                            InventoryRightBoot.sprite = LeatherRightBoot;
                            InventoryLeftBoot.sprite = LeatherLeftBoot;

                            break;
                        case SpecificItemType.LeatherGloves:

                            //Player
                            RightGlove.sprite = LeatherRightGlove;
                            LeftGlove.sprite = LeatherLeftGlove;

                            //Inventory View
                            InventoryRightGlove.sprite = LeatherRightGlove;
                            InventoryLeftGlove.sprite = LeatherLeftGlove;

                            break;
                    }
                }
                else
                {
                    switch (specificItemType)
                    {
                        case SpecificItemType.CrimsonArmor:

                            //Player
                            Torso.sprite = NormalTorso;
                            Pelvis.sprite = NormalPelvis;
                            ShoulderRight.sprite = NormalShoulderRight;
                            ShoulderLeft.sprite = NormalShoulderLeft;
                            ElbowRight.sprite = NormalElbowRight;
                            ElbowLeft.sprite = NormalElbowLeft;

                            //Inventory View
                            InventoryTorso.sprite = NormalTorso;
                            InventoryPelvis.sprite = NormalPelvis;
                            InventoryShoulderRight.sprite = NormalShoulderRight;
                            InventoryShoulderLeft.sprite = NormalShoulderLeft;
                            InventoryElbowRight.sprite = NormalElbowRight;
                            InventoryElbowLeft.sprite = NormalElbowLeft;

                            break;
                        case SpecificItemType.CrimsonHood:

                            //Player
                            Hood.sprite = NormalHood;

                            //Inventory View
                            InventoryHood.sprite = NormalHood;
                            break;
                        case SpecificItemType.IronBoots:

                            //Player
                            RightBoot.sprite = NormalRightBoot;
                            LeftBoot.sprite = NormalLeftBoot;

                            //Inventory View
                            InventoryRightBoot.sprite = NormalRightBoot;
                            InventoryLeftBoot.sprite = NormalLeftBoot;

                            break;
                        case SpecificItemType.IronGloves:

                            //Player
                            RightGlove.sprite = NormalRightGlove;
                            LeftGlove.sprite = NormalLeftGlove;

                            //Inventory View
                            InventoryRightGlove.sprite = NormalRightGlove;
                            InventoryLeftGlove.sprite = NormalLeftGlove;

                            break;
                        case SpecificItemType.LeatherArmor:

                            //Player
                            Torso.sprite = NormalTorso;
                            Pelvis.sprite = NormalPelvis;
                            ShoulderRight.sprite = NormalShoulderRight;
                            ShoulderLeft.sprite = NormalShoulderLeft;
                            ElbowRight.sprite = NormalElbowRight;
                            ElbowLeft.sprite = NormalElbowLeft;

                            //Inventory View
                            InventoryTorso.sprite = NormalTorso;
                            InventoryPelvis.sprite = NormalPelvis;
                            InventoryShoulderRight.sprite = NormalShoulderRight;
                            InventoryShoulderLeft.sprite = NormalShoulderLeft;
                            InventoryElbowRight.sprite = NormalElbowRight;
                            InventoryElbowLeft.sprite = NormalElbowLeft;

                            break;
                        case SpecificItemType.LeatherHood:

                            //Player
                            Hood.sprite = NormalHood;

                            //Inventory View
                            InventoryHood.sprite = NormalHood;

                            break;
                        case SpecificItemType.LeatherBoots:

                            //Player
                            RightBoot.sprite = NormalRightBoot;
                            LeftBoot.sprite = NormalLeftBoot;

                            //Inventory View
                            InventoryRightBoot.sprite = NormalRightBoot;
                            InventoryLeftBoot.sprite = NormalLeftBoot;

                            break;
                        case SpecificItemType.LeatherGloves:

                            //Player
                            RightGlove.sprite = NormalRightGlove;
                            LeftGlove.sprite = NormalLeftGlove;

                            //Inventory View
                            InventoryRightGlove.sprite = NormalRightGlove;
                            InventoryLeftGlove.sprite = NormalLeftGlove;

                            break;
                    }
                }
            }
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
        GameObject[] specificSlots = new GameObject[0];

        switch (itemType)
        {
            case ItemType.Armor:
                specificSlots = GameObject.FindGameObjectsWithTag("SlotArmor");
                break;
            case ItemType.Hood:
                specificSlots = GameObject.FindGameObjectsWithTag("SlotHood");
                break;
            case ItemType.Boots:
                specificSlots = GameObject.FindGameObjectsWithTag("SlotBoots");
                break;
            case ItemType.Gloves:
                specificSlots = GameObject.FindGameObjectsWithTag("SlotGloves");
                break;
        }

        GameObject nearestSlot = null;
        float minDistance = float.MaxValue;

        foreach (GameObject slot in slots)
        {
            float distance = Vector2.Distance(transform.position, slot.transform.position);
            if (distance < minDistance && slot.transform.childCount == 0) // Verifica si el slot está vacío
            {
                minDistance = distance;
                nearestSlot = slot;
            }
        }

        foreach (GameObject specificSlot in specificSlots)
        {
            float distance = Vector2.Distance(transform.position, specificSlot.transform.position);
            if (distance < minDistance && specificSlot.transform.childCount == 0) // Verifica si el slot específico está vacío
            {
                minDistance = distance;
                nearestSlot = specificSlot;
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

    private GameObject FindNextNearestSlot(GameObject currentSlot)
    {
        GameObject[] slots = GameObject.FindGameObjectsWithTag("Slot");
        GameObject[] specificSlots = new GameObject[0];

        switch (itemType)
        {
            case ItemType.Armor:
                specificSlots = GameObject.FindGameObjectsWithTag("SlotArmor");
                break;
            case ItemType.Hood:
                specificSlots = GameObject.FindGameObjectsWithTag("SlotHood");
                break;
            case ItemType.Boots:
                specificSlots = GameObject.FindGameObjectsWithTag("SlotBoots");
                break;
            case ItemType.Gloves:
                specificSlots = GameObject.FindGameObjectsWithTag("SlotGloves");
                break;
        }

        GameObject nextNearestSlot = null;
        float minDistance = float.MaxValue;

        foreach (GameObject slot in slots)
        {
            if (slot == currentSlot) continue;

            float distance = Vector2.Distance(currentSlot.transform.position, slot.transform.position);
            if (distance < minDistance && slot.transform.childCount == 0) // Verifica si el slot está vacío
            {
                minDistance = distance;
                nextNearestSlot = slot;
            }
        }

        foreach (GameObject specificSlot in specificSlots)
        {
            if (specificSlot == currentSlot) continue;

            float distance = Vector2.Distance(currentSlot.transform.position, specificSlot.transform.position);
            if (distance < minDistance && specificSlot.transform.childCount == 0) // Verifica si el slot específico está vacío
            {
                minDistance = distance;
                nextNearestSlot = specificSlot;
            }
        }

        return nextNearestSlot;
    }
}


