using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Tictactoe_contract
{
    
    [ServiceContract (CallbackContract = typeof(ITictactoeCallback))]
    public interface ITictactoe
    {
        [OperationContract]
        void MakeMove(string username, int buttonId, string XO);

        [OperationContract]
        string GetName(string username);

        [OperationContract]
        void AddPlayerToList(string playername);

        [OperationContract(IsOneWay = true)]
        void SubscribeToGame();

        

        [OperationContract]
        void Chat(string username, string message);

    }

    [DataContract]
    public class Player
    {
        [DataMember]
        public string PlayerName { get; set; }
        

        [DataMember]
        public ITictactoeCallback callback { get; set; }

        public Player(string PlayerName)
        {
            this.PlayerName = PlayerName;
        }
    }

    public interface ITictactoeCallback
    {
        
        [OperationContract(IsOneWay = true)]
        void OnChatCallback(string username, string message);
        [OperationContract(IsOneWay = true)]
        void OnMoveCallback(int buttonId, string XO);
    }
}
