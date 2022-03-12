using WebSocketSharp;
using UnityEngine;
using System;


  public class WS_Client : MonoBehaviour { 
      WebSocket ws;
        public Player user;
    
    void Start() {
         ws = new WebSocket("ws://localhost:8000");
         ws.OnMessage += (sender, e) => {
            Debug.Log("Message received from " + ((WebSocket)sender).Url + ", Data : " + e.Data);
        };
        ws.Connect();
    }
    void Update(){
        if(ws == null){
            return;
        }
        if(Input.GetKeyDown(KeyCode.Space)){

            Debug.Log("sending message");
            user.id = "00000000000";
            user.username = "johndoe";
            string json = JsonUtility.ToJson(user);
            ws.Send(json);
        }
        
    }
  }
