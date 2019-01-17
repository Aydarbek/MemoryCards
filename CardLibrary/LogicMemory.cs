using System;

namespace CardLibrary
{
    public class LogicMemory
    {

        int[] cards = new int[16];
        bool[] opens = new bool[16];
        int done, status;
        int cardA, cardB;
        IPlayable play;
        static Random rand = new Random();
        public int Counter { get; set; }
        public int BestScore { get; set; } = int.MaxValue;


        public LogicMemory (IPlayable play)
        {
            this.play = play;

        }
        public void CreateNewGame()
        {
            for (int j = 0; j < cards.Length; j++)
                cards[j] = j % (cards.Length / 2) + 1;
            for (int j = 0; j < 100; j++)
                ShuffleCards();
            for (int j = 0; j < cards.Length; j++)
                play.HideCard(j);
            for (int j = 0; j < cards.Length; j++)
                opens[j] = false;
            done = 0;
            status = 0;
            Counter = 0;

        }

        public void ClickPicture(int nr)
        {
            if (opens[nr] == true)
                return;
            switch (status)
            {
                case 0: Status0(nr); break;
                case 1: Status1(nr); break;
                case 2: Status2(nr); break;
                case 3: Status3(nr); break;
            }
           
        }

        private void ShuffleCards()
        {
            int a = rand.Next(0, cards.Length);
            int b = rand.Next(0, cards.Length);
            if (a == b) return;

            int x = cards[a];
            cards[a] = cards[b];
            cards[b] = x;

        }

        private void Open(int picture)
        {
            opens[picture] = true;
            play.ShowCard(picture, cards[picture]);
        }
        


        private void Status0(int nr)
        {
            cardA = nr;
            play.ShowCard(cardA, cards[cardA]);
            status = 1;
            Counter++;
        }

        private void Status1(int nr)
        {
            cardB = nr;
            if (cardA == cardB)
                return;
            play.ShowCard(cardB, cards[cardB]);
            status = 2;
            Counter++;
            if (cards[cardA] == cards[cardB])
            {
                Open(cardA);
                Open(cardB);
                done += 2;
                if (done == 16)
                    play.ShowWinner();
                else
                    status = 0;
            }
            else
                status = 3;

        }
        private void Status2(int nr)
        {

        }
        private void Status3(int nr)
        {
            play.HideCard(cardA);
            play.HideCard(cardB);
            cardA = nr;
            Status0(nr);
            Counter++;
        }
    }
}
