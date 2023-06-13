using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;
using Unity.Netcode.Components;
using System.Collections;
using System.Collections.Generic;

//EXAMEN

public class Player : NetworkBehaviour {
    
    public NetworkVariable<Color> ColorPlayer = new NetworkVariable<Color>();
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

    public float velocidadMovimiento = 4f;
    public float fuerzaSalto = 10f;

    private Rigidbody rb;
    private Renderer r;


   /* public Color Blanco;
    public Color Rojo;
    public Color Verde;
    public Color Azul;
    public Color Amarillo; */

    public List<Color> coloresDisponibles = new List<Color>();

    private void Start() {
        // Obtiene el componente Rigidbody adjunto al objeto
        rb = GetComponent<Rigidbody>();
        r = GetComponent<Renderer>();

        coloresDisponibles.Add(Color.white);
        coloresDisponibles.Add(Color.red);
        coloresDisponibles.Add(Color.green);
        coloresDisponibles.Add(Color.blue);
        coloresDisponibles.Add(Color.yellow);
    }

    public void Colorear(){
        if (IsServer)
        {
            EnviarColorServerRpc();
        }
    } 

   [ServerRpc]
    void EnviarColorServerRpc(ServerRpcParams rpcParams = default){
        ColorPlayer.Value = ObtenerColorBlanco();
        ActualizarColorJugadorServerRpc();
    } 

    
     Color ObtenerColorBlanco(List<Color> coloresEquipo){
        Debug.Log("Me Cambio de Color a Blanco");
       return Color.white;
    } 

    [ServerRpc]
    void SolicitarCambioPosicionServerRpc(Vector3 direccion){
        Position.Value += direccion;
        ActualizarPosicionClientRpc(Position.Value);
    }
    

    private void Update() {
        
       if (IsOwner)
        {
            Vector3 direccion = Vector3.zero;

            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                direccion = Vector3.left;
                return Color.red;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                direccion = Vector3.right;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                direccion = Vector3.back;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                direccion = Vector3.forward;
            }

            if (direccion != Vector3.zero)
            {
                SolicitarCambioPosicionServerRpc(direccion);
            }
        }
    }


 }


