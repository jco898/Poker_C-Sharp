using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGamePoker
{
    public enum FaceValue { Two = 2, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };

    public enum Suit { Clubs, Diamonds, Hearts, Spades };

    public class Card
    {
        private FaceValue _faceValue;
        private Suit _suit;

        public Card()
        {
            _faceValue = FaceValue.Ace;
            _suit = Suit.Clubs;
        }

        public Card(FaceValue faceValue_, Suit suit_)
        {
            _faceValue = faceValue_;
            _suit = suit_;
        }

        /* 
         * GetFaceValue()
         * 
         * Input:   none
         * 
         * Output:  none
         * 
         * Description: This method is used for returning the face value of a card.
         */

        /// <summary>
        /// This method is used for returning the face value of a card.
        /// </summary>
        /// <returns></returns>

        public FaceValue GetFaceValue()
        {
            return _faceValue;
        }

        /*
         * GetSuit()
         * 
         * Input:   none
         * 
         * Output:  none
         * 
         * Description: Method used for returning the suit of the card.
         */

        /// <summary>
        /// Method used for returning the suit of the card.
        /// </summary>
        /// <returns></returns>

        public Suit GetSuit()
        {
            return _suit;
        }

        /*
         * ToString()
         * 
         * Input:   none
         * 
         * Output:  none
         * 
         * Description: Used for printing the given card.
         *              ex: Ace of Spades
         */

        /// <summary>
        /// Used for printing the given card.
        /// ex: Ace of Spades
        /// </summary>
        /// <returns></returns>

        public override string ToString()
        {
            return string.Format("{0} of {1}", _faceValue, _suit);
        }
    }
}
