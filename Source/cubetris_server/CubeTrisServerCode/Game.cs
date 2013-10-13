using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using PlayerIO.GameLibrary;
using System.Drawing;
using CubeTrisServerCode;
using System.Threading;

namespace CubeTrisServerCode
{
    public enum STATE
    {
        MATCHMAKING,        // Trying to match partner
        GAME                // During game
    }

    //Player class. each player that join the game will have these attributes.
    public class Player : BasePlayer
    {
        public enum PlayerRole
        {
            BUILDER = 0,
            DEFENDER
        }

        public int X;
        public int Y;
        public PlayerRole playerRole;

        public Player()
        {
            X = 0; //Player topleft x
            Y = 0; //Player topleft y
        }
    }


    [RoomType("CubeTris")]
    public class GameCode : Game<Player>
    {
        private STATE   m_state;              // Current state
        private bool    m_roomReady = false;   // Room ready flag
        private bool[] m_playersState = { false, false };


        // This method is called when an instance of your the game is created
        public override void GameStarted()
        {
            // anything you write to the Console will show up in the 
            // output window of the development server
            Console.WriteLine("Game is started");

            // Set default state
            m_state = STATE.MATCHMAKING;

            // add a timer that sends out an update every 100th millisecond
            AddTimer(delegate
            {
                // Response each X ms
                switch (m_state)
                {
                    case STATE.MATCHMAKING:
                        break;

                    case STATE.GAME:
                        break;

                }

                //Create update message
                Message update = Message.Create("update");

                //Add mouse cordinates for each player to the message
                foreach (Player p in Players)
                {
                    update.Add(p.Id, p.X, p.Y);
                }

                //Broadcast message to all players
                Broadcast(update);
            }, 25);

        }


        // This method is called when the last player leaves the room, and it's closed down.
        public override void GameClosed()
        {
            Console.WriteLine("RoomId: " + RoomId);
        }


        // This method is called whenever a player joins the game
        public override void UserJoined(Player player)
        {
            if (!m_roomReady)
            {
                // Create init message for the joining player
                Message message = Message.Create(Constants.PLAYER_CREATED);

                // Tell player their own id
                message.Add(player.Id);

                // Send init message to player
                player.Send(message);

                if (PlayerCount >= 2)
                {
                    InitGame();
                }
            }
        }


        // This method is called when a player leaves the game
        public override void UserLeft(Player player)
        {
            Console.WriteLine("Player " + player.Id + " left the room");

            //Inform all other players that user left.
            Broadcast("left", player.Id);
        }


        // This method is called when a player sends a message into the server code
        public override void GotMessage(Player player, Message message)
        {
            // Check which command is received and respond 
            switch (message.Type)
            {
                case Constants.PING_MSG:
                    foreach (Player p in Players)
                    {
                        if (p.Id == player.Id)
                        {
                            // Found the player who asked for ping 
                            p.Send(Message.Create(Constants.PING_MSG));
                        }
                    }
                    break;

                case Constants.INIT_PLAYER:
                    if (!m_playersState[0])
                    {
                        m_playersState[0] = true;
                    }
                    else
                    {
                        m_playersState[1] = true;
                        InitGame();
                    }

                    break;

                case Constants.DESTROYED_MISSILE:
                    foreach( Player p in Players )
                    {
                        // Notify the other player
                        if (p != player)
                        {
                            p.Send(Message.Create(Constants.DESTROYED_MISSILE, message.GetInt(0)));
                        }
                    }
                    break;

                case Constants.MOVED_BLOCK:
                    foreach (Player p in Players)
                    {
                        // Notify the other player
                        if (p != player)
                        {
                            p.Send(Message.Create(Constants.MOVED_BLOCK, message.GetInt(0), message.GetString(1)));
                        }
                    }
                    break;
            }
        }


        // Game start logic, notifies players that game should start
        private void InitGame()
        {
            m_roomReady = true;

            // Player 1 is always builder - player 2 is defender
            // TODO: add randomization
            int i = 0;
            foreach ( Player p in Players )
            {
                if (i == 0)
                {
                    p.playerRole = Player.PlayerRole.BUILDER;
                    i++;
                }
                else
                {
                    p.playerRole = Player.PlayerRole.DEFENDER;
                }
            }
            

            ScheduleCallback(delegate
                        {
                            Message readyMsg = Message.Create(Constants.RECEIVE_XML);
                            // TODO: add XML selection logic
                            readyMsg.Add("level1");

                            foreach (Player p in Players)
                            {
                                readyMsg.Add(p.Id + "," + (( int )( p.playerRole ) ).ToString());
                            }

                            Broadcast(readyMsg);
                        }, 1000);

        }
    }
}

