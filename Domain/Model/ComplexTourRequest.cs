using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class ComplexTourRequest: ISerializable
    {
        public int Id {  get; set; }
        public Status CurrentStatus { get; set; }

        public List<int> TourRequestIds { get; set; }

        public List<TourRequest> TourRequests { get; set; }

        public int UserId {  get; set; }

        public ComplexTourRequest(int userId) { 
            TourRequests = new List<TourRequest>();
            TourRequestIds = new List<int>();
            CurrentStatus = Status.OnHold;
            UserId = userId;
        }

        public ComplexTourRequest()
        {
            TourRequests = new List<TourRequest>();
            TourRequestIds = new List<int>();
            CurrentStatus = Status.OnHold;
        }

        public List<string> TourRequestsForCSV()
        {
            List<string> csvV = new List<string>();
            if (TourRequestIds == null)
            {
                return csvV;
            }
            foreach (int t in TourRequestIds)
            {
                csvV.Add(t.ToString());
            }
            return csvV;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                CurrentStatus.ToString(),
                string.Join(",", TourRequestsForCSV()),
                UserId.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            CurrentStatus = (Status)Enum.Parse(typeof(Status), values[1]);
            if (string.IsNullOrWhiteSpace(values[2]))
            {
                TourRequestIds = new List<int>();
            }
            else
            {
                string[] slices = values[2].Split(',').Select(s => s.Trim()).Where(s => !string.IsNullOrEmpty(s)).ToArray();
                TourRequestIds = new List<int>();

                foreach (string slice in slices)
                {
                    TourRequestIds.Add(int.Parse(slice));
                }
            }
            UserId = int.Parse(values[3]);
        }




    }
}
