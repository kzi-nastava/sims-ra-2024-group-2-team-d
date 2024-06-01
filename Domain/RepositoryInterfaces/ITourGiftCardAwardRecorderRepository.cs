using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourGiftCardAwardRecorderRepository
    {
        public TourGiftCardAwardRecorder Save(TourGiftCardAwardRecorder tourGiftCardAwardRecorder);
        public TourGiftCardAwardRecorder GetByUserId(int userId);

        public TourGiftCardAwardRecorder Update(TourGiftCardAwardRecorder recorder);

    }
}
