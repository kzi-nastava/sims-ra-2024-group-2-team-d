using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class PictureRepository
    {
        private const string FilePath = "../../../Resources/Data/pictures.csv";

        private readonly Serializer<Picture> _serializer;

        private List<Picture> _pictures;

        public PictureRepository()
        {
            _serializer = new Serializer<Picture>();
            _pictures = _serializer.FromCSV(FilePath);
        }

        public Picture Save(Picture picture)
        {
            picture.Id = NextId();
            _pictures = _serializer.FromCSV(FilePath);
            _pictures.Add(picture);
            _serializer.ToCSV(FilePath, _pictures);
            return picture;
        }

        public int NextId()
        {
            _pictures = _serializer.FromCSV(FilePath);
            if (_pictures.Count < 1)
            {
                return 1;
            }
            return _pictures.Max(a => a.Id) + 1;
        }

        public List<Picture> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
    }
}
