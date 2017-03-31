using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewQuestion
{
    public class SeatPicker : ISeatPicker
    {
        private const bool TAKEN = true;
        private const bool EMPTY = false;
        private const int SEAT_NUMBERING_OFFSET = 1;
        private const string NO_OPEN_SEATS_EXCEPTION_MESSAGE = "There are no open seats in the provided seat list.";
        private const int NO_OPEN_SEAT = -1;

        public string DeveloperName
        {
            get
            {
                return "Nathan Plotts";
            }
        }

        public int GetSeat(List<bool> currentSeats)
        {
            if (currentSeats == null)
            {
                throw new ArgumentNullException(nameof(currentSeats));
            }

            if (currentSeats.Count == 0)
            {
                throw new ArgumentException(NO_OPEN_SEATS_EXCEPTION_MESSAGE, nameof(currentSeats));
            }

            // split the seats where groups of taken or empty seats end for readability
            List<List<bool>> splitSeats = SplitSeats(currentSeats);

            // split the search into the ends and the middle
            int bestEndGroup = FindBestEndGroup(splitSeats);
            int bestMiddleGroup = FindBestMiddleGroup(splitSeats);

            // compare the results from the best middle and best end options
            int bestSeat = ChooseSeat(splitSeats, bestEndGroup, bestMiddleGroup);

            if (bestSeat == NO_OPEN_SEAT)
            {
                throw new ArgumentException(NO_OPEN_SEATS_EXCEPTION_MESSAGE, nameof(currentSeats));
            }

            return bestSeat + SEAT_NUMBERING_OFFSET;
        }

        private int ChooseSeat(List<List<bool>> splitSeats, int bestEndGroup, int bestMiddleGroup)
        {
            int bestSeat = NO_OPEN_SEAT;

            if (bestEndGroup == NO_OPEN_SEAT && bestMiddleGroup != NO_OPEN_SEAT)
            {
                bestSeat = FindBestSeatFromSeatGroup(splitSeats, bestMiddleGroup);
            }
            else if (bestEndGroup != NO_OPEN_SEAT && bestMiddleGroup == NO_OPEN_SEAT)
            {
                bestSeat = FindBestSeatFromSeatGroup(splitSeats, bestEndGroup);
            }
            else if (bestEndGroup != NO_OPEN_SEAT && bestMiddleGroup != NO_OPEN_SEAT)
            {
                if (splitSeats[bestEndGroup].Count > splitSeats[bestMiddleGroup].Count / 2)
                {
                    bestSeat = FindBestSeatFromSeatGroup(splitSeats, bestEndGroup);
                }
                else
                {
                    bestSeat = FindBestSeatFromSeatGroup(splitSeats, bestMiddleGroup);
                }
            }

            return bestSeat;
        }

        private int FindBestSeatFromSeatGroup(List<List<bool>> splitSeats, int bestGroup)
        {
            int bestSeat = 0; // default if the first seat is best

            if (bestGroup == splitSeats.Count - 1 && splitSeats.Count > 1) // if the last seat is best
            {
                foreach (List<bool> seatGroup in splitSeats)
                {
                    bestSeat += seatGroup.Count;
                }

                bestSeat--;
            }
            else if (bestGroup != 0) // if the best seat is in the middle of the row
            {
                for (int i = 0; i < bestGroup; i++)
                {
                    bestSeat += splitSeats[i].Count;
                }

                bestSeat += splitSeats[bestGroup].Count / 2; 
            }

            return bestSeat;
        }

        private int FindBestMiddleGroup(List<List<bool>> splitSeats)
        {
            List<bool> bestGroup = new List<bool>();
            int bestGroupIndex = NO_OPEN_SEAT;

            if (splitSeats.Count > 2) // if there's any non-end groups
            {
                for (int i = 1; i < splitSeats.Count - 1; i++) // ignore the end groups
                {
                    if (bestGroup.Count < splitSeats[i].Count && !splitSeats[i][0])
                    {
                        bestGroup = splitSeats[i];
                        bestGroupIndex = i;
                    }
                }
            }

            return bestGroupIndex;
        }

        private int FindBestEndGroup(List<List<bool>> splitSeats)
        {
            int bestEndGroup = NO_OPEN_SEAT;

            if (!splitSeats[0][0] && !splitSeats[splitSeats.Count - 1][0])
            {
                bestEndGroup = splitSeats[0].Count >= splitSeats[splitSeats.Count - 1].Count ? 0 : splitSeats.Count - 1;
            }
            else
            {
                if (!splitSeats[0][0])
                {
                    bestEndGroup = 0;
                }
                else if (!splitSeats[splitSeats.Count - 1][0])
                {
                    bestEndGroup = splitSeats.Count - 1;
                }
            }

            return bestEndGroup;
        }

        private List<List<bool>> SplitSeats(List<bool> currentSeats)
        {
            List<List<bool>> splitSeats = new List<List<bool>>();
            bool currentSeatType = currentSeats[0];
            int startIndex = 0;

            for (int i = 0; i < currentSeats.Count; i++)
            {
                if (currentSeats[i] != currentSeatType) // split the row of seats when it switches from open to taken or taken to open
                {
                    splitSeats.Add(currentSeats.GetRange(startIndex, i - startIndex));
                    startIndex = i;
                    currentSeatType = currentSeats[i];
                }

                if (i == currentSeats.Count - 1)
                {
                    splitSeats.Add(currentSeats.GetRange(startIndex, i - startIndex + 1));
                }
            }

            return splitSeats;
        }
    }
}