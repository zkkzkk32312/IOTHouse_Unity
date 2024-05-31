using UnityEngine;

public class MessageReceiver : MonoBehaviour
{
    public void ReceiveMessage(string message)
    {
        Debug.Log("Received message from webpage: " + message);
    }
}
