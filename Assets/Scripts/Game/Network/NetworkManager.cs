using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using SocketIO;

public class NetworkManager : MonoBehaviour
{

    #region Properties
    private string clientID;
    private SocketIOComponent socket;
    public int TeamIndex;
    #endregion

    #region From server
    private void OnConnected(SocketIOEvent e)
    {
        Debug.Log("connected");
    }

    private void StartGame(SocketIOEvent e)
    {
        TeamIndex = JSONConverter.JSONToInt(e.data, "team");
        SendRoomName(JSONConverter.JSONToStr(e.data, "roomName"));
        SceneManager.LoadScene(1);
    }

    private void GetCode(SocketIOEvent e)
    {
        clientID = JSONConverter.JSONToStr(e.data, "code");
        Debug.Log(clientID);
    }

    private void GetAction(SocketIOEvent e)
    {
        Action.ToAction(e.data).Apply();
    }

    private void Leave(SocketIOEvent e)
    {
        string reason = JSONConverter.JSONToStr(e.data, "reason");
        Debug.Log(reason);
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    #endregion

    #region To server
    public void SendAction(Action a)
    {
        socket.Emit("fc:action", a.ToJSON());
    }

    public void SendAttack(Creature cr, List<Cage> cages)
    {
        if (Generall.GameMode == GameMode.Multiplayer && cr.Team == Team.M)
        {
            Action a = new Action(cr.Cage, Action.ActionType.Attack, cages);
            SendAction(a);
        }
    }

    public void SendSkill(Creature cr, ActiveSkill skill, List<Cage> cages)
    {
        if (Generall.GameMode == GameMode.Multiplayer && cr.Team == Team.M)
        {
            int idx = cr.Skills.ActiveSkills.FindIndex(e =>  e == skill);
            Action a = new Action(cr.Cage, idx, cages);
            SendAction(a);
        }
    }

    public void SendRoomName(string roomName)
    {
        socket.Emit("fc:getRoomName", JSONConverter.CreateJSON().Add("roomName", roomName).Get());
    }
    #endregion

    #region Button actions
    public void Connect()
    {
        DontDestroyOnLoad(gameObject);
        socket = gameObject.GetComponent<SocketIOComponent>();
        socket.On("fs:connected", OnConnected);
        socket.On("fs:start", StartGame);
        socket.On("fs:code", GetCode);
        socket.On("fs:action", GetAction);
        socket.On("fs:leave", Leave);
        socket.Connect();
    }
    public void Back ()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
    #endregion

    #region Singleton
    private void Start()
    {
        Get = this;
    }
    public static NetworkManager Get;
    #endregion

}