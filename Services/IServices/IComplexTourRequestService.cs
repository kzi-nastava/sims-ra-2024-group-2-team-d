using BookingApp.Domain.Model;

namespace BookingApp.Services.IServices
{
    public interface IComplexTourRequestService
    {
        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest);
    }
}
