using Unity.Netcode;
using UnityEngine;

//EXAMEN
public class GameManager : NetworkBehaviour
{
    void OnGUI()
    {
        // Establece el área para mostrar los elementos en la interfaz gráfica de usuario (GUI)
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        
        // Comprueba si no es cliente ni servidor para mostrar los botones de inicio
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer){
            StartButtons();
        } else {
            StatusLabels();

            SubmitNewPosition();
        }
        // Finaliza el área de la GUI
        GUILayout.EndArea();
    }

    // Método para mostrar los botones de inicio
    static void StartButtons(){
        // Comprueba si se hace clic en el botón "Host" y llama a la función StartHost() del NetworkManager
        if (GUILayout.Button("Host")) NetworkManager.Singleton.StartHost();
        // Comprueba si se hace clic en el botón "Client" y llama a la función StartClient() del NetworkManager
        if (GUILayout.Button("Client")) NetworkManager.Singleton.StartClient();
        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartHost();
    }

    // Método para mostrar las etiquetas de estado
    static void StatusLabels(){
        // Determina el modo actual (Host o Cliente) y lo muestra en una etiqueta
        var mode = NetworkManager.Singleton.IsHost ?
            "Host" : NetworkManager.Singleton.IsClient ? "Client" : "Client";
        // Muestra el tipo de transporte de red utilizado en una etiqueta
        GUILayout.Label("Transport: " +
            NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
        // Muestra el modo actual (Host o Cliente) en una etiqueta
        GUILayout.Label("Mode: " + mode);
    }    

    static void SubmitNewPosition(){
        if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Mover a Inicio" : "Request Position Change"))
        {
            if (NetworkManager.Singleton.IsServer && !NetworkManager.Singleton.IsClient)
            {
                var players = NetworkManager.Singleton.ConnectedClientsList;
                foreach (var player in players)
                {
                   // player.PlayerObject.GetComponent<Player>().Mover();
                }
            }
            else
            {
                var playerObject = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
                var player = playerObject.GetComponent<Player>();
               // player.Mover();
            }
        }
    }
}
