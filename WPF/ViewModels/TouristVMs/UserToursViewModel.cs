using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Services;
using BookingApp.Services.IServices;
using BookingApp.WPF.Views;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using iText.IO.Image;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using BookingApp.WPF.Views.TouristV;
using iText.Commons.Bouncycastle.Asn1.X509;
using iText.Layout.Borders;

namespace BookingApp.WPF.ViewModels.TouristVMs
{
    public class UserToursViewModel
    {
        public static ObservableCollection<TourInstance> ReservedTours { get; set; }
        public static ObservableCollection<TourInstance> FinishedTours { get; set; }
        public User LoggedInUser { get; set; }
        public static TourInstance SelectedTour { get; set; }
        public RelayCommand OpenMorePicturesCommand {  get; set; }
        public ICommand OpenTourReviewCommand { get; set; }
        public IDialogService _dialogService {  get; set; }
        public RelayCommand GoBackCommand {  get; set; }
        public RelayCommand MoreInfoCommand {  get; set; }

        private readonly MainViewModel MainViewModel;
        public static ObservableCollection<TourInstance> TourInstances { get; set; }
        public ICommand GenerateReservationReportCommand {  get; set; }

        private string _logoPath = "../../../Resources/Images/tourist-logo.jpg";
        private string _appName = "Milan's app for tourism";
        public ITourReservationService TourReservationService { get; set; }

        public UserToursViewModel(User loggedInUser, ObservableCollection<TourInstance> tourInstances, IDialogService dialogService, MainViewModel mainViewModel)
        {
            ReservedTours = new ObservableCollection<TourInstance>();
            FinishedTours = new ObservableCollection<TourInstance>();
            LoggedInUser = loggedInUser;
            TourInstances = tourInstances;
            OpenTourReviewCommand = new RelayCommand(tourInstance => OpenTourReview((TourInstance)tourInstance));
            FilterTours(tourInstances);
            OpenMorePicturesCommand = new RelayCommand(tourInstance =>OpenMorePictures((TourInstance)tourInstance));
            _dialogService = dialogService;
            MainViewModel = mainViewModel;
            MoreInfoCommand = new RelayCommand(tourInstance => ShowMoreInfo((TourInstance)tourInstance));
            GoBackCommand = new RelayCommand(GoBack);
            GenerateReservationReportCommand = new RelayCommand(tourInstance => GenerateReservationReport((TourInstance)tourInstance));
            TourReservationService = Injector.Injector.CreateInstance<ITourReservationService>();
        }

        public void GenerateReservationReport(TourInstance tourInstance)
        {
            TourReservation reservation = TourReservationService.GetByUserAndTourInstanceId(tourInstance.Id, LoggedInUser.Id);
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), $"{tourInstance.BaseTour.Title}_Report.pdf");
            using (PdfWriter writer = new PdfWriter(filePath))
            {
                using (PdfDocument pdf = new PdfDocument(writer))
                {
                    Document document = new Document(pdf);
                    AddHeader(document, _logoPath, _appName);

                    Table table = new Table(2);
                    table.SetWidth(UnitValue.CreatePercentValue(100));

                    // Tekstualna ćelija
                    Cell textCell = new Cell().SetBorder(Border.NO_BORDER);
                    textCell.Add(new Paragraph("Tour Reservation Report").SetFontSize(22).SetBold());
                    textCell.Add(new Paragraph($"Tour Name: {tourInstance.BaseTour.Title}").SetFontSize(14));
                    textCell.Add(new Paragraph($"Location: {tourInstance.BaseTour.Location}").SetFontSize(14));
                    textCell.Add(new Paragraph($"Date: {tourInstance.Date.ToString("d")}").SetFontSize(14));
                    textCell.Add(new Paragraph($"Duration: {tourInstance.BaseTour.Duration.ToString()} hours").SetFontSize(14));
                    textCell.Add(new Paragraph($"Language: {tourInstance.BaseTour.Language}").SetFontSize(14));

                    // Dodavanje liste ključnih tačaka
                    iText.Layout.Element.List pdfList = new iText.Layout.Element.List()
                        .SetSymbolIndent(12)
                        .SetListSymbol("\u2022")
                        .SetFontSize(12);

                    foreach (var keyPoint in tourInstance.BaseTour.KeyPoints)
                    {
                        ListItem listItem = new ListItem(keyPoint.Name);
                        listItem.SetFontSize(14);
                        pdfList.Add(listItem);
                    }
                    textCell.Add(new Paragraph("Key points:").SetFontSize(16).SetBold());
                    textCell.Add(pdfList);

                    // Dodavanje liste turista
                    textCell.Add(new Paragraph("Tourists:").SetFontSize(16).SetBold());
                    textCell.Add(new Paragraph("---------------------------").SetFontSize(12));
                    foreach (var tourist in reservation.Tourists)
                    {
                        textCell.Add(new Paragraph($"Name: {tourist.Name}").SetFontSize(14));
                        textCell.Add(new Paragraph($"Surname: {tourist.LastName}").SetFontSize(14));
                        textCell.Add(new Paragraph($"Age: {tourist.Age}").SetFontSize(14));
                        textCell.Add(new Paragraph("---------------------------").SetFontSize(12)); // Razdelnik za čitljivost
                    }

                    // Dodavanje tekstualne ćelije u tabelu
                    table.AddCell(textCell);

                    // Slika ćelija
                    Cell imageCell = new Cell().SetBorder(Border.NO_BORDER);
                    Image tourPicture = new Image(ImageDataFactory.Create(tourInstance.BaseTour.Pictures[0]));
                    tourPicture.SetAutoScale(true);
                    imageCell.Add(tourPicture);
                    imageCell.Add(new Paragraph(tourInstance.BaseTour.Description).SetFontSize(12)); // Opis slike

                    // Dodavanje ćelija u tabelu
                    table.AddCell(imageCell);

                    // Dodavanje tabele u dokument
                    document.Add(table);

                    // Zatvaranje dokumenta
                    document.Close();
                }
            }
            OpenPdfViewer(filePath);
        }

        private void AddHeader(Document document, string logoPath, string appName)
        {
            Image logo = new Image(ImageDataFactory.Create(logoPath));
            logo.SetWidth(50);

            Paragraph header = new Paragraph()
                .Add(logo)
                .Add(new Paragraph(appName).SetFontSize(22).SetBold().SetMarginLeft(10))
                .SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER)
                .SetMarginBottom(20);

            document.Add(header);
        }

        private void OpenPdfViewer(string filePath)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var pdfViewerWindow = new PdfViewerWindow(filePath);
                pdfViewerWindow.Show();
            });
        }


        public void ShowMoreInfo(TourInstance tourInstance)
        {
            var viewModel = new MoreInfoAboutTourViewModel(tourInstance, _dialogService);
            bool? result = _dialogService.ShowDialog(viewModel);
        }

        public void GoBack()
        {
            MainViewModel.SwitchView(new TouristHomeViewModel(MainViewModel, LoggedInUser, new DialogService()));
        }

        public void OpenMorePictures(TourInstance tourInstance)
        {
            var viewModel = new ShowMorePicturesViewModel(tourInstance);
            bool? result = _dialogService.ShowDialog(viewModel);
        }

        public void FilterTours(ObservableCollection<TourInstance> tourInstances)
        {
            var _tourReservationService = Injector.Injector.CreateInstance<ITourReservationService>();
            List<TourReservation> tourReservations = _tourReservationService.GetAll();
            List<TourInstance> tourInstanceList = tourInstances.ToList();
            var _touristsService = Injector.Injector.CreateInstance<ITouristService>();
            List<Tourist> tourists = _touristsService.GetAll();
            foreach (TourReservation tourReservation in tourReservations)
            {
                AddToCorrespondingTour(tourReservation, tourists, tourInstanceList);
            }
        }

        public void AddToCorrespondingTour(TourReservation tourReservation, List<Tourist> tourists, List<TourInstance> tourInstanceList)
        {
            if (tourReservation.UserId != LoggedInUser.Id) return;
            var matchingTourInstance = tourInstanceList.Find(tourInstance => tourInstance.Id == tourReservation.TourInstanceId && (tourInstance.End || !tourInstance.Start));
            var matchingTourist = tourists.Find(tourist => tourist.ReservationId == tourReservation.Id && tourist.UserId == LoggedInUser.Id);
            if (matchingTourist == null || matchingTourInstance == null) return;
            if (matchingTourInstance.End && matchingTourist.ShowedUp)
            {
                FinishedTours.Add(matchingTourInstance);
            }
            else if (!matchingTourInstance.Start)
            {
                ReservedTours.Add(matchingTourInstance);
            }
        }

        private void OpenTourReview(TourInstance tourInstance)
        {
            //UserTourReviewView userTourReviewView = new UserTourReviewView(LoggedInUser, tourInstance);
            //userTourReviewView.Show();
            MainViewModel.SwitchView(new UserTourReviewViewModel(LoggedInUser, tourInstance, MainViewModel, _dialogService, TourInstances));
        }
    }
}
