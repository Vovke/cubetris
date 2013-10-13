using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CubeTrisServerCode
{
    class Constants
    {
        /* Receive message */
        public const String PING_MSG = "PING";                      // Ping probing  
        public const String GAME_READY = "GAME_READY";              // Player is ready to start game ( loading completed )
        public const String GAME_ENDED = "GAME_ENDED";              // Message from player that game is ended, Score should be sent at this point
        public const String INIT_PLAYER = "INIT_PLAYER";            // New player initialisation
        public const String GAME_MOVE_BUILDER = "GAME_MOVE_BUILDER";

        /* Response message */
        public const String RECEIVE_XML = "RECEIVE_XML";            // Sending game XML to players
        public const String START_GAME = "START_GAME";              // Sended after both players sent GAME_READY command
        public const String FINISH_GAME = "FINISH_GAME";            // Sent upon finishing game ( both players sent GAME_ENDED command )
        public const String PLAYER_CREATED = "PLAYER_CREATED";      // Responce upon new player initialisation

        /* Defender messages */
        public const String DESTROYED_MISSILE = "DESTROYED_MISSILE";// Received by builder, means the defender have destroyed a missile

        /* Builder messages */
        public const String MOVED_BLOCK = "MOVED_BLOCK";			// Received by defender, means the builder moved blocks

    }
}
