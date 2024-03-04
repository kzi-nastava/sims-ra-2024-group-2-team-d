using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Model
{
    public class Tour
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Location {  get; set; }
        public string Language { get; set; }

        public int MaxTourists { get; set; }

        public string ControlPoin {  get; set; }

        public List<DateOnly> Date {  get; set; }

        public int Duration { get; set; }

        public List<String> Pictures { get; set; }
    }
}
