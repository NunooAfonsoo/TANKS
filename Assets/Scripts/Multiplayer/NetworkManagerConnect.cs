using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using TMPro;

public class NetworkManagerConnect : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private UnityTransport unityTransport;
    [SerializeField] private ushort port;

    public void StartHost()
    {
        string ip = GetLocalIPAddress();
        unityTransport.SetConnectionData(ip, port);
        NetworkManager.Singleton.Shutdown();
        NetworkManager.Singleton.StartHost();
        Hide();
    }

    public string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }

        throw new System.Exception("No network adapters with an IPv4 address in the system!");
    }

    public void StartClient()
    {
        if (!string.IsNullOrEmpty(inputField.text))
        {
            unityTransport.SetConnectionData(inputField.text, port);

            NetworkManager.Singleton.StartClient();
            Hide();
        }
    }

    private void Hide()
    {
        GameManager.Instance.NetworkStatusChosen();
        gameObject.SetActive(false);
    }
}
