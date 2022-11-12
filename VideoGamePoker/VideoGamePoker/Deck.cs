using System;
using System.Collections.Generic;
using System.Text;

namespace VideoGamePoker
{
    class Deck
    {
        private const int CARDS_IN_DECK = 52;

        private int currentCard = 0;
        private Card[] deck = new Card[CARDS_IN_DECK];
        private Random random;

        public Deck()
        {
            random = new Random();
            int i = 0;

            foreach (Suit s in Enum.GetValues(typeof(Suit)))
            {
                foreach (FaceValue fV in Enum.GetValues(typeof(FaceValue)))
                {
                    deck[i++] = new Card(fV, s);
                }
            }
        }

        /*
         * Shuffle()
         * 
         * Input:   none
         * 
         * Output:  none
         * 
         * Description: This method is used for shuffling a deck of cards.
         *              For each card in the deck, a random card is generated 
         *              and swapped with the respective card.
         */

        /// <summary>
        /// This method is used for shuffling a deck of cards.
        /// For each card in the deck, a random card is generated 
        /// and swapped with the respective card.
        /// </summary>

        public void Shuffle()
        {
            currentCard = 0;
            
            Random r = new Random();

            for (int i = 0; i < CARDS_IN_DECK; i++)
            {
                Card temp = new Card();
                int randomCard = r.Next(CARDS_IN_DECK);

                temp = deck[i];
                deck[i] = deck[randomCard];
                deck[randomCard] = temp;
            }
        }

        /*
         * IsDeckEmpty()
         * 
         * Input:   none
         * 
         * Output:  none
         * 
         * Description: Bool method used for checking whether there 
         *              are still cards left to deal.
         *              It is called before each card is dealt.
         */

        /// <summary>
        /// Bool method used for checking whether there 
        /// are still cards left to deal.
        /// It is called before each card is dealt.
        /// </summary>
        /// <returns></returns>

        public bool IsDeckEmpty()
        {
            return currentCard > CARDS_IN_DECK - 1;
        }

        /*
         * DealACard()
         * 
         * Input:   none
         * 
         * Output:  none
         * 
         * Description: This method is used for dealing a new card.
         *              If there are no cards left to deal, an exception 
         *              is thrown.
         *              Otherwise, a new card is returned.
         */

        /// <summary>
        /// This method is used for dealing a new card.
        /// If there are no cards left to deal, an exception 
        /// is thrown.
        /// Otherwise, a new card is returned.
        /// </summary>
        /// <returns></returns>

        public Card DealACard()
        {
            if (currentCard > CARDS_IN_DECK - 1)
            {
                throw new ArgumentException("There are no more cards to deal.", "currentCard");
            }
            
            return deck[currentCard++];
        }

        /*
         * ToString()
         * 
         * Input:   none
         * 
         * Output:  none
         * 
         * Description: Used for printing the cards in the deck.
         */

        /// <summary>
        /// Used for printing the cards in the deck.
        /// </summary>
        /// <returns></returns>

        public override string ToString()
        {
            StringBuilder sr = new StringBuilder();
            
            for (int i = 0; i < CARDS_IN_DECK; i++)
            {
                sr.AppendFormat("{0} \n", deck[i]);
            }

            return string.Format("{0}", sr.ToString());
        }
    }
}
