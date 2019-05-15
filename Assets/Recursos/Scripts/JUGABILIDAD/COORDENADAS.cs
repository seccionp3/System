using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;

public class COORDENADAS : MonoBehaviour
{
    LeapProvider provider;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void iniciarBusqueda() {
        provider = FindObjectOfType<LeapProvider>() as LeapProvider;
    }

    public void procesoBusqueda() {
        Frame frame = provider.CurrentFrame;
        Hand hand = frame.Hands[0]; // cannot apply indexing
        Vector position = hand.PalmPosition;
        Vector direction = hand.Direction;

        Debug.Log("La posicion de la mano es:" + position + "La direccion de la mano es:" + direction);
    }
}
