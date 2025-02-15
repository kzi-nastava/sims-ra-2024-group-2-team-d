﻿using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class TourReservationRepository : ITourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourReservation.csv";
        private readonly Serializer<TourReservation> _serializer;
        private List<TourReservation> _tourReservation;
        public TouristRepository _touristRepo;

        public TourReservationRepository()
        {
            _serializer = new Serializer<TourReservation>();
            _tourReservation = _serializer.FromCSV(FilePath);
            _touristRepo = new TouristRepository();
            BindTourists();
        }

        public int NextId()
        {
            _tourReservation = _serializer.FromCSV(FilePath);
            if (_tourReservation.Count < 1)
            {
                return 1;
            }
            return _tourReservation.Max(c => c.Id) + 1;
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            tourReservation.Id = NextId();
            _tourReservation = _serializer.FromCSV(FilePath);
            _tourReservation.Add(tourReservation);
            _serializer.ToCSV(FilePath, _tourReservation);
            return tourReservation;
        }

        public List<TourReservation> GetAll()
        {
            return _tourReservation;
        }

        public void BindTourists()
        {
            foreach (var item in _tourReservation)
            {
                item.Tourists = _touristRepo.GetAllByTourReservationId(item.Id);
            }
        }

        public List<Tourist> GetAllTouristByTourId(int id)
        {
            List<Tourist> tourists = new List<Tourist>();
            BindTourists();
            List<TourReservation> list = _tourReservation.Where(r => r.TourInstanceId == id).ToList();
            foreach (var item in list)
            {
                foreach (var item1 in item.Tourists)
                {
                    if (!tourists.Contains(item1))
                    {
                        tourists.Add(item1);
                    }
                }
            }
            return tourists;
        }

        public int GetTourInstanceById(int id)
        {
            TourReservation tr = _tourReservation.Find(r => r.Id == id);
            if(tr!=null)
            {
                return tr.TourInstanceId;
            }
            else  return -1;
        }
        //useri koji su napravili rezervacije za jednu istancu ture
        public List<int> GetAllUsersByTourInstanceId(int id)
        {
            List<int> users = new List<int>();
            List<TourReservation> list = _tourReservation.Where(r => r.TourInstanceId == id).ToList();
            foreach (var item in list)
            {
                if (!users.Contains(item.UserId))
                {
                    users.Add(item.UserId);
                }
            }
            return users;
        }
        
        public TourReservation? GetByUserAndTourInstanceId(int tourInstanceId, int userId)
        {
            return _tourReservation.Find(r => r.TourInstanceId == tourInstanceId && r.UserId == userId);
        }

        public List<TourReservation> GetByUserId(int  userId)
        {
            return _tourReservation.Where(r => r.UserId == userId).ToList();
        }
    }
}