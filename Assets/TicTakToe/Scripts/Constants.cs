using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TicTacToe
{
    public static class Constants 
    {
        /// <summary>
        /// Define here which mark the player uses at his turn
        /// and which sign uses computer for his turn
        /// </summary>
        public const CellContent PlayerMark = CellContent.PlayerTurnMark;
        public const CellContent ComputerMark = CellContent.ComputerTurnMark;
    }
}
