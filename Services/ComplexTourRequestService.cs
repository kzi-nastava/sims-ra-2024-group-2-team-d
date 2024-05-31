using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services.IServices;

namespace BookingApp.Services
{
    public class ComplexTourRequestService : IComplexTourRequestService
    {
        public IComplexTourRequestRepository ComplexTourRequestReposotry { get; set; }
        public ComplexTourRequestService()
        {
            ComplexTourRequestReposotry = Injector.Injector.CreateInstance<IComplexTourRequestRepository>();
        }

        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest)
        {
            return ComplexTourRequestReposotry.Save(complexTourRequest);
        }
    }
}
