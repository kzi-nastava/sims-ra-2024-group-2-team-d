using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
    public class TourGiftCardAwardRecorderService
    {
        public ITourGiftCardAwardRecorderRepository TourGiftCardAwardRecorderRepository { get; set; }
        public IGiftCardRepository GiftCardRepository { get; set; }
        public ITouristRepository TouristRepository { get; set; }
        public ITourInstanceRepository TourInstanceRepository { get; set; }

        public TourGiftCardAwardRecorderService(ITourGiftCardAwardRecorderRepository giftCardAwardRecoredRepo, IGiftCardRepository giftCardRepository, ITourInstanceRepository tourInstanceRepository, ITouristRepository touristRepository)
        {
            TourGiftCardAwardRecorderRepository = giftCardAwardRecoredRepo;
            GiftCardRepository = giftCardRepository;
            TouristRepository = touristRepository;
            TourInstanceRepository = tourInstanceRepository;
        }

        public void GiveGiftCardToEligibleTourist(User user)
        {

            TourGiftCardAwardRecorder recorder = TourGiftCardAwardRecorderRepository.GetByUserId(user.Id);
            DateOnly yearAgo = DateOnly.FromDateTime(DateTime.Now.AddYears(-1));
            if(recorder == null || recorder.ReceivedDate <= yearAgo)
            {
                List<int> reservations = TouristRepository.GetUserReservations(user.Id);
                List<TourInstance> tours = TourInstanceRepository.GetAllByIds(reservations);
                if (TourInstanceRepository.HasAtLeastFiveToursInLastYear(tours))
                {
                    GiftCard newGiftCard = new GiftCard(user.Id);
                    //newGiftCard.ExpirationDate.AddMonths(-6);
                    newGiftCard.ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(6));
                    newGiftCard.ExpirationDate.AddMonths(6);
                    GiftCardRepository.Save(newGiftCard);
                    if(recorder == null)
                    {
                        recorder = new TourGiftCardAwardRecorder(user.Id);
                        TourGiftCardAwardRecorderRepository.Save(recorder);
                    }
                    else
                    {
                        recorder.ReceivedDate = DateOnly.FromDateTime(DateTime.Now);
                        TourGiftCardAwardRecorderRepository.Update(recorder);
                    }
                }
            }
        }
    }
}
