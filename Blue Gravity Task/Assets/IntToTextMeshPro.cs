using UnityEngine;
using TMPro;  // Necesario para usar TextMeshPro

public class IntToTextMeshPro : MonoBehaviour
{
    // Variable para el valor entero
    public int value;

    // Referencia al componente TextMeshPro
    public TextMeshProUGUI textMeshPro;

    void Update()
    {
        // Asignar el valor de la variable entera al texto del TextMeshPro
        textMeshPro.text = value.ToString();
    }
}
