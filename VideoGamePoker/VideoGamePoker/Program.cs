using System;

namespace VideoGamePoker
{
    class Program
    {
        /*
         * Video Game Poker
         * 
         * This program uses the Card and Deck classes to create a poker game.
         * 
         * The user is given the options of playing or testing a game.
         * 
         * If they choose to play the game, they will be asked given a bankroll and asked 
         * to wager an amount. A deck of cards will then be dealt and shuffled, starting 
         * a new game.
         * They will be shown their hand and prompted to change the card of their choice.
         * Once their decision is made, the hand will be evaluated for potential winnings.
         * If they still have money in their bankroll, the user will be prompted to play 
         * again, until they choose to exit the game.
         * 
         * If they test the game, they will be shown a menu of all possible 
         * winning hands, and asked to select the winning combination of their 
         * choice. They will then be asked to wager an amount and see their 
         * potential winnings with the hand of their choice. They will be prompted 
         * to play again, until they choose to quit the program.
         * 
         * Tester: Yaoyu Wang
         */

        /// <summary>
        /// This program uses the Card and Deck classes to create a poker game.
        /// 
        /// The user is given the options of playing or testing a game.
        /// 
        /// If they choose to play the game, they will be asked given a bankroll and asked 
        /// to wager an amount. A deck of cards will then be dealt and shuffled, starting 
        /// a new game.
        /// They will be shown their hand and prompted to change the card of their choice.
        /// Once their decision is made, the hand will be evaluated for potential winnings.
        /// If they still have money in their bankroll, the user will be prompted to play 
        /// again, until they choose to exit the game.
        /// 
        /// If they test the game, they will be shown a menu of all possible 
        /// winning hands, and asked to select the winning combination of their 
        /// choice. They will then be asked to wager an amount and see their 
        /// potential winnings with the hand of their choice. They will be prompted 
        /// to play again, until they choose to quit the program.
        /// </summary>

        const double MIN_BET = 1;
        const double MAX_BET = 1000;
        const int ROYAL_FLUSH_PAYOUT = 250;
        const int STRAIGHT_FLUSH_PAYOUT = 50;
        const int FOUR_OF_A_KIND_PAYOUT = 25;
        const int FULL_HOUSE_PAYOUT = 9;
        const int FLUSH_PAYOUT = 6;
        const int STRAIGHT_PAYOUT = 4;
        const int THREE_OF_A_KIND_PAYOUT = 3;
        const int TWO_PAIR_PAYOUT = 2;
        const int PAIR_PAYOUT = 1;
        const int CARDS_IN_HAND = 5;

        static void Main(string[] args)
        {
            double bankroll = 1000;
            bool success;
            Deck deck = new Deck();

            Console.WriteLine("~~~~~~~~~~~~Poker Game~~~~~~~~~~~~ \n");

            int yourChoice = 0;

            Console.WriteLine("1. Play");
            Console.WriteLine("2. Test \n");
            Console.Write("Please enter your desired option: ");
            success = int.TryParse(Console.ReadLine(), out yourChoice);

            while (!success || yourChoice < 1 || yourChoice > 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Error, please enter a valid menu choice: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                success = int.TryParse(Console.ReadLine(), out yourChoice);
            }

            if (yourChoice == 1)
            {
                PlayGame(deck, bankroll);
            }
            else
            {
                TestGame();
            }

            Console.ReadKey();
        }

        /*
         * PlayGame()
         * 
         * Input:   Deck theDeck
         *          double theBankroll
         *          
         * Output:  none
         * 
         * Description: This method starts a new game of poker.
         *              The user is dealt a hand from a shuffled deck.
         *              They will be prompted to replace up to 4 cards 
         *              of their choice.
         *              Once the hand is evaluated, their bankroll will 
         *              be updated and, if not bankrupt, will be prompted 
         *              to play again.
         */

        /// <summary>
        /// This method starts a new game of poker.
        /// The user is dealt a hand from a shuffled deck.
        /// They will be prompted to replace up to 4 cards 
        /// of their choice.
        /// Once the hand is evaluated, their bankroll will 
        /// be updated and, if not bankrupt, will be prompted 
        /// to play again.
        /// </summary>
        /// <param name="theDeck"></param>
        /// <param name="theBankroll"></param>

        public static void PlayGame(Deck theDeck, double theBankroll)
        {
            double yourBet = 0;

            yourBet = AskAnte(theBankroll);
            theBankroll -= yourBet;

            Console.WriteLine("Money in bank: ${0}", theBankroll);

            theDeck.Shuffle();

            Card[] hand = new Card[CARDS_IN_HAND];

            for (int i = 0; i < hand.Length; i++)
            {
                if (!theDeck.IsDeckEmpty())
                {
                    hand[i] = theDeck.DealACard();
                }
            }

            PrintHand(hand);

            ReplaceCards(hand, theDeck);

            PrintHand(hand);

            EvaluateHand(hand, ref theBankroll, yourBet);

            CheckIfBroke(theBankroll);

            AskToPlayAgain(theDeck, theBankroll);
        }

        /*
         * TestGame()
         * 
         * Input:   none
         * 
         * Output:  none
         * 
         * Description: This method is used for testing the game.
         *              The user is shown a menu, asked to select the winning 
         *              hand they would like to test.
         *              Once their choice is validated, a switch case will take 
         *              the user to the appropriate testing method.
         */

        /// <summary>
        /// This method is used for testing the game.
        /// The user is shown a menu, asked to select the winning 
        /// hand they would like to test.
        /// Once their choice is validated, a switch case will take 
        /// the user to the appropriate testing method.
        /// </summary>

        public static void TestGame()
        {
            bool success;
            int yourChoice = 0;
            double testBankroll = 1000;

            Console.WriteLine(); // For spacing
            Console.WriteLine("1. Test Royal Flush");
            Console.WriteLine("2. Test Straight Flush");
            Console.WriteLine("3. Test Four-of-a-Kind");
            Console.WriteLine("4. Test Full House");
            Console.WriteLine("5. Test Flush");
            Console.WriteLine("6. Test Straight");
            Console.WriteLine("7. Test Three-of-a-Kind");
            Console.WriteLine("8. Test 2 Pair");
            Console.WriteLine("9. Test Pair");
            Console.WriteLine("0. QUIT \n");

            Console.Write("Please enter your desired option: ");
            success = int.TryParse(Console.ReadLine(), out yourChoice);

            while (!success || yourChoice < 0 || yourChoice > 9)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Error, please enter a valid menu choice: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                success = int.TryParse(Console.ReadLine(), out yourChoice);
            }

            switch (yourChoice)
            {
                case 1:
                    TestRoyalFlush(testBankroll);
                    break;
                case 2:
                    TestStraightFlush(testBankroll);
                    break;
                case 3:
                    TestFourOfAKind(testBankroll);
                    break;
                case 4:
                    TestFullHouse(testBankroll);
                    break;
                case 5:
                    TestFlush(testBankroll);
                    break;
                case 6:
                    TestStraight(testBankroll);
                    break;
                case 7:
                    TestThreeOfAKind(testBankroll);
                    break;
                case 8:
                    TestTwoPair(testBankroll);
                    break;
                case 9:
                    TestPair(testBankroll);
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
            }
        }

        /*
         * AskAnte()
         * 
         * Input:   double theBankroll
         * 
         * Output:  none
         * 
         * Description: Method used for prompting the user to 
         *              enter the amount of their bet.
         *              If their input is invalid, a while loop 
         *              repeatedly queries the user until their 
         *              input becomes valid.
         */

        /// <summary>
        /// Method used for prompting the user to 
        /// enter the amount of their bet.
        /// If their input is invalid, a while loop 
        /// repeatedly queries the user until their 
        /// input becomes valid.
        /// </summary>
        /// <param name="theBankroll"></param>
        /// <returns></returns>

        public static double AskAnte(double theBankroll)
        {
            double theBet = 0;
            bool success;

            Console.WriteLine(); // For spacing
            Console.Write("Enter the amount you would like to bet (1..{0}): ", theBankroll);
            success = double.TryParse(Console.ReadLine(), out theBet);

            while (!success || theBet > theBankroll || theBet < MIN_BET || theBet > MAX_BET)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Input! The amount must be between 1 and {0}", theBankroll);
                Console.Write("Enter the amount you would like to bet (1..{0}): ", theBankroll);
                Console.ForegroundColor = ConsoleColor.Gray;
                success = double.TryParse(Console.ReadLine(), out theBet);
            }

            return theBet;
        }

        /*
         * PrintHand()
         * 
         * Input:   Card[] theHand
         * 
         * Output:  Card[] theHand
         * 
         * Description: Method is used for printing the current 
         *              hand of the user.
         */

        /// <summary>
        /// Method is used for printing the current 
        /// hand of the user.
        /// </summary>
        /// <param name="theHand"></param>

        public static void PrintHand(Card[] theHand)
        {
            Console.WriteLine(); // For spacing

            for (int i = 0; i < theHand.Length; i++)
            {
                Console.WriteLine("[{0}] {1}", i + 1, theHand[i].ToString());
            }
        }

        /*
         * ReplaceCards()
         * 
         * Input:   Card[] theHand
         *          Deck theDeck
         *          
         * Output:  none
         * 
         * Description: This method is used for prompting the user 
         *              to replace a card.
         *              If their input is invalid, an error message 
         *              will be shown, asking them again.
         *              A bool array assures the user cannot change 
         *              a card they've already replaced.
         *              If the deck is not empty, a new card will 
         *              be dealt, replacing the card index of their 
         *              choice.
         */

        /// <summary>
        /// This method is used for prompting the user 
        /// to replace a card.
        /// If their input is invalid, an error message 
        /// will be shown, asking them again.
        /// A bool array assures the user cannot change 
        /// a card they've already replaced.
        /// If the deck is not empty, a new card will 
        /// be dealt, replacing the card index of their 
        /// choice.
        /// </summary>
        /// <param name="theHand"></param>
        /// <param name="theDeck"></param>

        public static void ReplaceCards(Card[] theHand, Deck theDeck)
        {
            bool success;
            bool OK;
            int yourChoice = 0;
            bool[] cardTaken = new bool[] { false, false, false, false, false, false };

            for (int i = 0; i < theHand.Length - 1; i++)
            {
                OK = false;

                while (OK == false)
                {
                    Console.WriteLine(); // For spacing

                    Console.Write("Choose the card you'd like to replace, or enter 0 to continue: ");
                    success = int.TryParse(Console.ReadLine(), out yourChoice);

                    while (!success || yourChoice < 0 || yourChoice > 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("Error, enter a valid card or 0 to quit: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        success = int.TryParse(Console.ReadLine(), out yourChoice);
                    }

                    if (cardTaken[yourChoice] == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Error, card already chosen...");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        OK = true;
                    }
                }

                if (yourChoice == 0)
                {
                    break;
                }

                if (!theDeck.IsDeckEmpty())
                {
                    theHand[yourChoice - 1] = theDeck.DealACard();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Card successfully replaced...");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                cardTaken[yourChoice] = true;
            }
        }

        /*
         * EvaluateHand()
         * 
         * Input:   Card[] theHand
         *          ref double theBankroll
         *          double yourBet
         *          
         * Output:  ref double theBankroll
         *          double yourBet
         *          
         * Description: This method is used for checking whether 
         *              the user's hand contains a winning combination.
         *              The hand is sent to separate validating methods, 
         *              from highest winning combination (Royal Flush) 
         *              to lowest (Pair).
         *              Once their hand is evaluated, their winnings 
         *              are shown and their bankroll will be updated.
         */

        /// <summary>
        /// This method is used for checking whether 
        /// the user's hand contains a winning combination.
        /// The hand is sent to separate validating methods, 
        /// from highest winning combination (Royal Flush) 
        /// to lowest (Pair).
        /// Once their hand is evaluated, their winnings 
        /// are shown and their bankroll will be updated.
        /// </summary>
        /// <param name="theHand"></param>
        /// <param name="theBankroll"></param>
        /// <param name="yourBet"></param>

        public static void EvaluateHand(Card[] theHand, ref double theBankroll, double yourBet)
        {
            Console.WriteLine(); // For spacing

            if (CheckRoyalFlush(theHand))
            {
                yourBet *= ROYAL_FLUSH_PAYOUT;
                theBankroll += yourBet;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Winning Hand: Royal Flush");
                Console.WriteLine("You won: {0}", yourBet);
                Console.WriteLine("Money in Bank: {0}", theBankroll);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (CheckStraightFlush(theHand))
            {
                yourBet *= STRAIGHT_FLUSH_PAYOUT;
                theBankroll += yourBet;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Winning Hand: Straight Flush");
                Console.WriteLine("You won: {0}", yourBet);
                Console.WriteLine("Money in Bank: {0}", theBankroll);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (CheckFourOfAKind(theHand))
            {
                yourBet *= FOUR_OF_A_KIND_PAYOUT;
                theBankroll += yourBet;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Winning Hand: Four of a Kind");
                Console.WriteLine("You won: {0}", yourBet);
                Console.WriteLine("Money in Bank: {0}", theBankroll);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (CheckFullHouse(theHand))
            {
                yourBet *= FULL_HOUSE_PAYOUT;
                theBankroll += yourBet;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Winning Hand: Full House");
                Console.WriteLine("You won: {0}", yourBet);
                Console.WriteLine("Money in Bank: {0}", theBankroll);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (CheckFlush(theHand))
            {
                yourBet *= FLUSH_PAYOUT;
                theBankroll += yourBet;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Winning Hand: Flush");
                Console.WriteLine("You won: {0}", yourBet);
                Console.WriteLine("Money in Bank: {0}", theBankroll);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (CheckStraight(theHand))
            {
                yourBet *= STRAIGHT_PAYOUT;
                theBankroll += yourBet;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Winning Hand: Straight");
                Console.WriteLine("You won: {0}", yourBet);
                Console.WriteLine("Money in Bank: {0}", theBankroll);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (CheckThreeOfAKind(theHand))
            {
                yourBet *= THREE_OF_A_KIND_PAYOUT;
                theBankroll += yourBet;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Winning Hand: Three of a Kind");
                Console.WriteLine("You won: {0}", yourBet);
                Console.WriteLine("Money in Bank: {0}", theBankroll);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (CheckTwoPair(theHand))
            {
                yourBet *= TWO_PAIR_PAYOUT;
                theBankroll += yourBet;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Winning Hand: Two Pair");
                Console.WriteLine("You won: {0}", yourBet);
                Console.WriteLine("Money in Bank: {0}", theBankroll);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else if (CheckPair(theHand))
            {
                yourBet *= PAIR_PAYOUT;
                theBankroll += yourBet;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Winning Hand: Pair");
                Console.WriteLine("You won: {0}", yourBet);
                Console.WriteLine("Money in Bank: {0}", theBankroll);
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have no winning hand");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Money won: $0.00");
                Console.WriteLine("Money in the bank: {0}", theBankroll);
            }
        }

        /*
         * CheckIfBroke()
         * 
         * Input:   double theBankroll
         * 
         * Output:  none
         * 
         * Description: Used to determine if a user is out of money.
         *              If their bankroll is empty, they will be notified 
         *              that they lost the game, closing the console.
         */

        /// <summary>
        /// Used to determine if a user is out of money.
        /// If their bankroll is empty, they will be notified 
        /// that they lost the game, closing the console.
        /// </summary>
        /// <param name="theBankroll"></param>

        public static void CheckIfBroke(double theBankroll)
        {
            if (theBankroll == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No more money left in the bank, you lose!");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        /*
         * AskToPlayAgain()
         * 
         * Input:   Deck theDeck
         *          double theBankroll
         *          
         * Output:  none
         * 
         * Description: Method used for asking the user to play again.
         *              If they press "N", the console will close, ending 
         *              the game.
         *              Otherwise, they are returned to the PlayGame() method, 
         *              starting a new game.
         */

        /// <summary>
        /// Method used for asking the user to play again.
        /// If they press "N", the console will close, ending 
        /// the game.
        /// Otherwise, they are returned to the PlayGame() method, 
        /// starting a new game.
        /// </summary>
        /// <param name="theDeck"></param>
        /// <param name="theBankroll"></param>

        public static void AskToPlayAgain(Deck theDeck, double theBankroll)
        {
            string yourChoice;

            Console.Write("Do you wish to play again? Press any key to continue or N to quit: \n");
            yourChoice = Console.ReadLine();

            if (yourChoice.ToUpper() == "N")
            {
                Environment.Exit(0);
            }
            else
            {
                PlayGame(theDeck, theBankroll);
            }
        }

        /*
         * TestRoyalFlush()
         * 
         * Input:   double theTestBankroll
         * 
         * Output:  none
         * 
         * Description: Method used for testing a Royal Flush winning hand.
         * 
         *              The used is dealt a hand containing 
         *              the winning combination.
         *              After the hand is evaluated, they are 
         *              returned to the testing method.
         */

        /// <summary>
        /// Method used for testing a Royal Flush winning hand.
        /// 
        /// The used is dealt a hand containing 
        /// the winning combination.
        /// After the hand is evaluated, they are 
        /// returned to the testing method.
        /// </summary>
        /// <param name="theTestBankroll"></param>

        public static void TestRoyalFlush(double theTestBankroll)
        {
            double myBankroll = theTestBankroll;
            double yourBet = 0;
            yourBet = AskAnte(myBankroll);
            myBankroll -= yourBet;

            Card[] hand = new Card[CARDS_IN_HAND];
            hand[0] = new Card(FaceValue.Ten, Suit.Spades);
            hand[1] = new Card(FaceValue.Jack, Suit.Spades);
            hand[2] = new Card(FaceValue.Queen, Suit.Spades);
            hand[3] = new Card(FaceValue.King, Suit.Spades);
            hand[4] = new Card(FaceValue.Ace, Suit.Spades);

            PrintHand(hand);

            EvaluateHand(hand, ref myBankroll, yourBet);

            TestGame();
        }

        /*
         * TestStraightFlush()
         * 
         * Input:   double theTestBankroll
         * 
         * Output:  none
         * 
         * Description: Method used for testing a Straight Flush winning hand.
         */

        /// <summary>
        /// Method used for testing a Straight Flush winning hand.
        /// </summary>
        /// <param name="theTestBankroll"></param>

        public static void TestStraightFlush(double theTestBankroll)
        {
            double myBankroll = theTestBankroll;
            double yourBet = 0;
            yourBet = AskAnte(myBankroll);
            myBankroll -= yourBet;

            Card[] hand = new Card[CARDS_IN_HAND];
            hand[0] = new Card(FaceValue.Two, Suit.Spades);
            hand[1] = new Card(FaceValue.Three, Suit.Spades);
            hand[2] = new Card(FaceValue.Four, Suit.Spades);
            hand[3] = new Card(FaceValue.Five, Suit.Spades);
            hand[4] = new Card(FaceValue.Six, Suit.Spades);

            PrintHand(hand);

            EvaluateHand(hand, ref myBankroll, yourBet);

            TestGame();
        }

        /*
         * TestFourOfAKind()
         * 
         * Input:   double theTestBankroll
         * 
         * Output:  none
         * 
         * Description: Method used for testing a Four of a Kind winning hand.
         */

        /// <summary>
        /// Method used for testing a Four of a Kind winning hand.
        /// </summary>
        /// <param name="theTestBankroll"></param>

        public static void TestFourOfAKind(double theTestBankroll)
        {
            double yourBet = AskAnte(theTestBankroll);
            theTestBankroll -= yourBet;

            Card[] hand = new Card[CARDS_IN_HAND];
            hand[0] = new Card(FaceValue.Ten, Suit.Spades);
            hand[1] = new Card(FaceValue.Ten, Suit.Hearts);
            hand[2] = new Card(FaceValue.Ten, Suit.Clubs);
            hand[3] = new Card(FaceValue.Ten, Suit.Diamonds);
            hand[4] = new Card(FaceValue.Ace, Suit.Spades);

            PrintHand(hand);

            EvaluateHand(hand, ref theTestBankroll, yourBet);

            TestGame();
        }

        /*
         * TestFullHouse()
         * 
         * Input:   double theTestBankroll
         * 
         * Output:  none
         * 
         * Description: Method used for testing a Full House winning hand.
         */

        /// <summary>
        /// Method used for testing a Full House winning hand.
        /// </summary>
        /// <param name="theTestBankroll"></param>

        public static void TestFullHouse(double theTestBankroll)
        {
            double yourBet = AskAnte(theTestBankroll);
            theTestBankroll -= yourBet;

            Card[] hand = new Card[CARDS_IN_HAND];
            hand[0] = new Card(FaceValue.Ten, Suit.Spades);
            hand[1] = new Card(FaceValue.Ten, Suit.Diamonds);
            hand[2] = new Card(FaceValue.Ten, Suit.Hearts);
            hand[3] = new Card(FaceValue.Queen, Suit.Spades);
            hand[4] = new Card(FaceValue.Queen, Suit.Clubs);

            PrintHand(hand);

            EvaluateHand(hand, ref theTestBankroll, yourBet);

            TestGame();
        }

        /*
         * TestFlush()
         * 
         * Input:   double theTestBankroll
         * 
         * Output:  none
         * 
         * Description: Method used for testing a Flush winning hand.
         */

        /// <summary>
        /// Method used for testing a Flush winning hand.
        /// </summary>
        /// <param name="theTestBankroll"></param>

        public static void TestFlush(double theTestBankroll)
        {
            double yourBet = AskAnte(theTestBankroll);
            theTestBankroll -= yourBet;

            Card[] hand = new Card[CARDS_IN_HAND];
            hand[0] = new Card(FaceValue.Two, Suit.Spades);
            hand[1] = new Card(FaceValue.Four, Suit.Spades);
            hand[2] = new Card(FaceValue.Five, Suit.Spades);
            hand[3] = new Card(FaceValue.Seven, Suit.Spades);
            hand[4] = new Card(FaceValue.Ten, Suit.Spades);

            PrintHand(hand);

            EvaluateHand(hand, ref theTestBankroll, yourBet);

            TestGame();
        }

        /*
         * TestStraight()
         * 
         * Input:   double theTestBankroll
         * 
         * Output:  none
         * 
         * Description: Method used for testing a Straight winning hand.
         */

        /// <summary>
        /// Method used for testing a Straight winning hand.
        /// </summary>
        /// <param name="theTestBankroll"></param>

        public static void TestStraight(double theTestBankroll)
        {
            double yourBet = AskAnte(theTestBankroll);
            theTestBankroll -= yourBet;

            Card[] hand = new Card[CARDS_IN_HAND];
            hand[0] = new Card(FaceValue.Six, Suit.Spades);
            hand[1] = new Card(FaceValue.Seven, Suit.Spades);
            hand[2] = new Card(FaceValue.Eight, Suit.Diamonds);
            hand[3] = new Card(FaceValue.Nine, Suit.Spades);
            hand[4] = new Card(FaceValue.Ten, Suit.Hearts);

            PrintHand(hand);

            EvaluateHand(hand, ref theTestBankroll, yourBet);

            TestGame();
        }

        /*
         * TestThreeOfAKind()
         * 
         * Input:   double theTestBankroll
         * 
         * Output:  none
         * 
         * Description: Method used for testing a Three of a Kind winning hand.
         */

        /// <summary>
        /// Method used for testing a Three of a Kind winning hand.
        /// </summary>
        /// <param name="theTestBankroll"></param>

        public static void TestThreeOfAKind(double theTestBankroll)
        {
            double yourBet = AskAnte(theTestBankroll);
            theTestBankroll -= yourBet;

            Card[] hand = new Card[CARDS_IN_HAND];
            hand[0] = new Card(FaceValue.Four, Suit.Diamonds);
            hand[1] = new Card(FaceValue.Four, Suit.Clubs);
            hand[2] = new Card(FaceValue.Four, Suit.Hearts);
            hand[3] = new Card(FaceValue.Seven, Suit.Spades);
            hand[4] = new Card(FaceValue.Nine, Suit.Spades);

            PrintHand(hand);

            EvaluateHand(hand, ref theTestBankroll, yourBet);

            TestGame();
        }

        /*
         * TestTwoPair()
         * 
         * Input:   double theTestBankroll
         * 
         * Output:  none
         * 
         * Description: Method used for testing a Two Pair winning hand.
         */

        /// <summary>
        /// Method used for testing a Two Pair winning hand.
        /// </summary>
        /// <param name="theTestBankroll"></param>

        public static void TestTwoPair(double theTestBankroll)
        {
            double yourBet = AskAnte(theTestBankroll);
            theTestBankroll -= yourBet;

            Card[] hand = new Card[CARDS_IN_HAND];
            hand[0] = new Card(FaceValue.Six, Suit.Hearts);
            hand[1] = new Card(FaceValue.Six, Suit.Spades);
            hand[2] = new Card(FaceValue.Eight, Suit.Diamonds);
            hand[3] = new Card(FaceValue.Eight, Suit.Spades);
            hand[4] = new Card(FaceValue.Ten, Suit.Spades);

            PrintHand(hand);

            EvaluateHand(hand, ref theTestBankroll, yourBet);

            TestGame();
        }

        /*
         * TestPair()
         * 
         * Input:   double theTestBankroll
         * 
         * Output:  none
         * 
         * Description: Method used for testing a Pair winning hand.
         */

        /// <summary>
        /// Method used for testing a Pair winning hand.
        /// </summary>
        /// <param name="theTestBankroll"></param>

        public static void TestPair(double theTestBankroll)
        {
            double yourBet = AskAnte(theTestBankroll);
            theTestBankroll -= yourBet;

            Card[] hand = new Card[CARDS_IN_HAND];
            hand[0] = new Card(FaceValue.Three, Suit.Spades);
            hand[1] = new Card(FaceValue.Four, Suit.Spades);
            hand[2] = new Card(FaceValue.Ten, Suit.Spades);
            hand[3] = new Card(FaceValue.Jack, Suit.Spades);
            hand[4] = new Card(FaceValue.Jack, Suit.Hearts);

            PrintHand(hand);

            EvaluateHand(hand, ref theTestBankroll, yourBet);

            TestGame();
        }

        /*
         * CheckRoyalFlush()
         * 
         * Input:   Card[] theHand
         * 
         * Output:  none
         * 
         * Description: Bool method used for checking whether a hand 
         *              contains a Royal Flush.
         *              
         *              If the winning combination is found, true is returned.
         *              Otherwise, the method returns false.
         */

        /// <summary>
        /// Bool method used for checking whether a hand 
        /// contains a Royal Flush.
        /// 
        /// If the winning combination is found, true is returned.
        /// Otherwise, the method returns false.
        /// </summary>
        /// <param name="theHand"></param>
        /// <returns></returns>

        public static bool CheckRoyalFlush(Card[] theHand)
        {
            SortCardsBySuit(theHand);

            if (theHand[0].GetSuit() < theHand[4].GetSuit())
            {
                return false;
            }

            SortCardsByFaceValue(theHand);

            if (theHand[0].GetFaceValue() == FaceValue.Ten)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * CheckStraightFlush()
         * 
         * Input:   Card[] theHand
         * 
         * Output:  none
         * 
         * Description: Bool method used for checking whether a hand 
         *              contains a Straight Flush.
         */

        /// <summary>
        /// Bool method used for checking whether a hand 
        /// contains a Straight Flush.
        /// </summary>
        /// <param name="theHand"></param>
        /// <returns></returns>

        public static bool CheckStraightFlush(Card[] theHand)
        {
            SortCardsBySuit(theHand);

            if (theHand[0].GetSuit() != theHand[4].GetSuit())
            {
                return false;
            }

            SortCardsByFaceValue(theHand);

            if ((int)theHand[0].GetFaceValue() == (int)theHand[4].GetFaceValue() - 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * CheckFourOfAKind()
         * 
         * Input:   Card[] theHand
         * 
         * Output:  none
         * 
         * Description: Bool method used for checking whether a hand 
         *              contains a Four of a Kind.
         */

        /// <summary>
        /// Bool method used for checking whether a hand 
        /// contains a Four of a Kind.
        /// </summary>
        /// <param name="theHand"></param>
        /// <returns></returns>

        public static bool CheckFourOfAKind(Card[] theHand)
        {
            SortCardsBySuit(theHand);

            int sameSuitCount = 0;

            for (int i = 0; i < theHand.Length - 1; i++)
            {
                if (theHand[i].GetSuit() == theHand[i + 1].GetSuit())
                {
                    sameSuitCount++;
                }
            }

            if (sameSuitCount > 1)
            {
                return false;
            }

            SortCardsByFaceValue(theHand);

            if (theHand[0].GetFaceValue() == theHand[3].GetFaceValue())
            {
                return true;
            }
            else if (theHand[1].GetFaceValue() == theHand[4].GetFaceValue())
            {
                return true;
            }

            return false;
        }

        /*
         * CheckFullHouse()
         * 
         * Input:   Card[] theHand
         * 
         * Output:  none
         * 
         * Description: Bool method used for checking whether a hand 
         *              contains a Full House.
         */

        /// <summary>
        /// Bool method used for checking whether a hand 
        /// contains a Full House.
        /// </summary>
        /// <param name="theHand"></param>
        /// <returns></returns>

        public static bool CheckFullHouse(Card[] theHand)
        {
            SortCardsByFaceValue(theHand);

            if (theHand[0].GetFaceValue() == theHand[2].GetFaceValue())
            {
                if (theHand[3].GetFaceValue() == theHand[4].GetFaceValue())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (theHand[2].GetFaceValue() == theHand[4].GetFaceValue())
            {
                if (theHand[0].GetFaceValue() == theHand[1].GetFaceValue())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /*
         * CheckFlush()
         * 
         * Input:   Card[] theHand
         * 
         * Output:  none
         * 
         * Description: Bool method used for checking whether a hand 
         *              contains a Flush.
         */

        /// <summary>
        /// Bool method used for checking whether a hand 
        /// contains a Flush.
        /// </summary>
        /// <param name="theHand"></param>
        /// <returns></returns>

        public static bool CheckFlush(Card[] theHand)
        {
            SortCardsBySuit(theHand);

            if (theHand[0].GetSuit() == theHand[4].GetSuit())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /*
         * CheckStraight()
         * 
         * Input:   Card[] theHand
         * 
         * Output:  none
         * 
         * Description: Bool method used for checking whether a hand 
         *              contains a Straight.
         */

        /// <summary>
        /// Bool method used for checking whether a hand 
        /// contains a Straight.
        /// </summary>
        /// <param name="theHand"></param>
        /// <returns></returns>

        public static bool CheckStraight(Card[] theHand)
        {
            SortCardsBySuit(theHand);

            if (theHand[0].GetSuit() == theHand[4].GetSuit())
            {
                return false;
            }
            
            SortCardsByFaceValue(theHand);

            if (theHand[0].GetFaceValue() == theHand[1].GetFaceValue())
            {
                return false;
            }

            if ((int)theHand[0].GetFaceValue() == (int)theHand[4].GetFaceValue() - 4)
            {
                return true;
            }
            else if (theHand[0].GetFaceValue() == FaceValue.Ace && theHand[4].GetFaceValue() == FaceValue.Five)
            {
                return true;
            }

            return false;
        }

        /*
         * CheckThreeOfAKind()
         * 
         * Input:   Card[] theHand
         * 
         * Output:  none
         * 
         * Description: Bool method used for checking whether a hand 
         *              contains a Three of a Kind.
         */

        /// <summary>
        /// Bool method used for checking whether a hand 
        /// contains a Three of a Kind.
        /// </summary>
        /// <param name="theHand"></param>
        /// <returns></returns>

        public static bool CheckThreeOfAKind(Card[] theHand)
        {
            SortCardsByFaceValue(theHand);

            if (theHand[0].GetFaceValue() == theHand[2].GetFaceValue())
            {
                return true;
            }
            else if (theHand[1].GetFaceValue() == theHand[3].GetFaceValue())
            {
                return true;
            }
            else if (theHand[2].GetFaceValue() == theHand[4].GetFaceValue())
            {
                return true;
            }

            return false;
        }

        /*
         * CheckTwoPair()
         * 
         * Input:   Card[] theHand
         * 
         * Output:  none
         * 
         * Description: Bool method used for checking whether a hand 
         *              contains a Two Pair.
         */

        /// <summary>
        /// Bool method used for checking whether a hand 
        /// contains a Two Pair.
        /// </summary>
        /// <param name="theHand"></param>
        /// <returns></returns>

        public static bool CheckTwoPair(Card[] theHand)
        {
            SortCardsByFaceValue(theHand);
            
            if (theHand[0].GetFaceValue() == theHand[1].GetFaceValue())
            {
                if (theHand[2].GetFaceValue() == theHand[3].GetFaceValue())
                {
                    return true;
                }
                else if (theHand[3].GetFaceValue() == theHand[4].GetFaceValue())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (theHand[1].GetFaceValue() == theHand[2].GetFaceValue())
            {
                if (theHand[3].GetFaceValue() == theHand[4].GetFaceValue())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        /*
         * CheckPair()
         * 
         * Input:   Card[] theHand
         * 
         * Output:  none
         * 
         * Description: Bool method used for checking whether a hand 
         *              contains a Pair.
         */

        /// <summary>
        /// Bool method used for checking whether a hand 
        /// contains a Pair.
        /// </summary>
        /// <param name="theHand"></param>
        /// <returns></returns>

        public static bool CheckPair(Card[] theHand)
        {
            SortCardsByFaceValue(theHand);

            if (theHand[0].GetFaceValue() == theHand[1].GetFaceValue())
            {
                return true;
            }
            else if (theHand[1].GetFaceValue() == theHand[2].GetFaceValue())
            {
                return true;
            }
            else if (theHand[2].GetFaceValue() == theHand[3].GetFaceValue())
            {
                return true;
            }
            else if (theHand[3].GetFaceValue() == theHand[4].GetFaceValue())
            {
                return true;
            }

            return false;
        }

        /*
         * SortCardsBySuit()
         * 
         * Input:   Card[] theHand
         * 
         * Output:  none
         * 
         * Description: This method is used for sorting the user's hand 
         *              by suit.
         *              It uses the insertion sorting method.
         */

        /// <summary>
        /// This method is used for sorting the user's hand 
        /// by suit.
        /// It uses the insertion sorting method.
        /// </summary>
        /// <param name="theHand"></param>

        public static void SortCardsBySuit(Card[] theHand)
        {
            int j;

            for (int i = 1; i < theHand.Length; i++)
            {
                j = i;

                while (j > 0 && theHand[j - 1].GetSuit() > theHand[j].GetSuit())
                {
                    Card temp = theHand[j];
                    theHand[j] = theHand[j - 1];
                    theHand[j - 1] = temp;
                    j--;
                }
            }
        }

        /*
         * SortCardsByFaceValue()
         * 
         * Input:   Card[] thehand
         * 
         * Output:  none
         * 
         * Description: Method uses the insertion sorting algorithm 
         *              to sort the user's hand by face value.
         */

        /// <summary>
        /// Method uses the insertion sorting algorithm 
        /// to sort the user's hand by face value.
        /// </summary>
        /// <param name="theHand"></param>

        public static void SortCardsByFaceValue(Card[] theHand)
        {
            int j;

            for (int i = 1; i < theHand.Length; i++)
            {
                j = i;

                while (j > 0 && theHand[j - 1].GetFaceValue() > theHand[j].GetFaceValue())
                {
                    Card temp = theHand[j];
                    theHand[j] = theHand[j - 1];
                    theHand[j - 1] = temp;
                    j--;
                }
            }
        }
    }
}
