using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PaintsActivation : MonoBehaviour, IInteractable
{
    public AudioSource sonidoBotonCuadros;
    public Animator boton;
    public float tiempoApagado = 0.9f;

    [SerializeField]
    private Color CorrectColorRGB;
    private string CorrectColor;

    [SerializeField]
    private bool isColorCorrect;
    private ColorCycle colorCyclePaints;

    private VintageBaulController VintageBaulController;

    public void Interact(RaycastHit hit)
    {
        //traer boton animator del objeto raycasteado hit
        //una vez obtenido el boton busca el componente padre
        Transform parent = (hit.transform.parent).transform.parent;
        Transform pic = parent.GetChild(1);
        Transform bordLed = pic.GetChild(1);
        Transform placaLed = pic.GetChild(3);
        if(Input.GetKeyDown(KeyCode.E))
        {

            StartCoroutine(EncenderYApagarAnimacionBoton(this.gameObject.GetComponent<Animator>()));
            Color switchingColorRGB = colorCyclePaints.NextColor(bordLed.GetComponent<Renderer>().material.color);
            bordLed.GetComponent<Renderer>().material.color = switchingColorRGB;
            bordLed.GetComponent<Renderer>().material.SetColor("_EmissionColor", switchingColorRGB);

            placaLed.GetComponent<Renderer>().material.color = switchingColorRGB;
            placaLed.GetComponent<Renderer>().material.SetColor("_EmissionColor", switchingColorRGB);
            string switchingColorHex = switchingColorRGB.ToHexString();
            isColorCorrect = (this.CorrectColor == switchingColorHex) ? true : false;
            Debug.Log($"the color {switchingColorHex} is {isColorCorrect} the correct color is {this.CorrectColor}");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CorrectColor = CorrectColorRGB.ToHexString();
        Color[] colorSecuence = { Color.red, Color.blue, Color.green, Color.magenta, Color.yellow };
        colorCyclePaints = new ColorCycle(colorSecuence);
        VintageBaulController = GameObject.Find("TapaBaul").GetComponent<VintageBaulController>();
    }

    // Update is called once per frame
    void Update()
    {
        PuzzleCuadros();
    }
    void PuzzleCuadros()
    {
        var paints = Object.FindObjectsOfType<PaintsActivation>();
        if(paints.All(x => x.isColorCorrect))
        {
            Debug.Log("All paint colors are correct!");
            VintageBaulController.Open();
        }
    }
    private IEnumerator EncenderYApagarAnimacionBoton(Animator buttonHit)
    {
        Debug.Log("Enciende Courrutine");
        buttonHit.SetBool("Press", true); // PRIMERO ACTIVA LA ANIMACION
        while(buttonHit.GetBool("Press") == true)
        {
            Debug.Log("Ingresa al while");
            yield return new WaitForSeconds(tiempoApagado); //ESPERA UNOS SEGUNDOS Y LUEGO EJECUTA LA LINEA DE ABAJO
            buttonHit.SetBool("Press", false); // LUEGO LA DESACTIVA
        }
        yield break; //FINALIZA LA COURRUTINE
    }
}
