using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game
{
    //enum CommandType
    //{
    //    WAITING_FOR_PLAYERS,  
    //    GAME_STARTED, 
    //    LOCATION, 
    //    POINTS
    //}
    #region enumerators action from server
    public enum MoveWho
    {
        THIS, OBJECTIVE, OPPONENT
    }
    public enum PointsWho
    {
        THIS, OPPONENT
    }
    public enum WhoPlayer
    {
        THIS, OPPONENT
    }
    #endregion
    #region messages from server
    public class MessageFromServerText
    {
        public string text;
        public MessageFromServerText(string text)
        {
            this.text = text;
        }
    }
    public class MessageFromServerLocation
    {
        //   "LOCATION:OBJECTIVE:" + x + ";" + y
        public MoveWho moveWho;
        public ushort x, y;
        public MessageFromServerLocation(MoveWho moveWho, ushort x, ushort y)
        {
            this.moveWho = moveWho;
            this.x = x;
            this.y = y;
        }

    }
    public class MessageFromServerPoints
    {
        public PointsWho pointsWho;
        public int points;
        public MessageFromServerPoints(PointsWho w, int p) { pointsWho = w; points = p; }
    }
    public class MessageFromServerWins
    {
        public WhoPlayer whoWins;
        public MessageFromServerWins(WhoPlayer w) { whoWins = w; }
    }
    #endregion
    public enum MoveDirection
    {
        UP,DOWN,LEFT,RIGHT
    }
    public class MessageToServerMove
    {
        public MoveDirection moveDirection;
        public MessageToServerMove(MoveDirection moveDirection)
        {
            this.moveDirection = moveDirection;
        }
    }
}
