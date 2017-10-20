using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SC.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmocaoTeste : ContentPage
    {
        public EmocaoTeste()
        {
            Title = "Emotion";

            BindingContext = new ViewModels.EmocaoViewModel();
       
            var takePhotoButton = new Button
            {
                Text = "Tirar Foto"
               
            };
            takePhotoButton.SetBinding(Button.CommandProperty, "TirarFotoCommand");

            var pickPhotoButton = new Button
            {
                Text = "Abrir Foto"
                
            };
            pickPhotoButton.SetBinding(Button.CommandProperty, "AbrirFotoCommand");

            var imageUrlEntry = new Entry();
            imageUrlEntry.SetBinding(Entry.TextProperty, "ImageUrl");

            var image = new Image
            {
                HeightRequest = 300
            };
            image.SetBinding(Image.SourceProperty, "ImageUrl");

            var testeBinding = new Label
            {
                FontSize = 22,
            };
            testeBinding.SetBinding(Label.TextProperty, "ImageUrl");

            var extractTextFromImageUrlButton = new Button
            {
                Text = "Reconhecer Emoção (Url)",             
                FontSize = 22
            };
            extractTextFromImageUrlButton.SetBinding(Button.CommandProperty, "AnalisarImagemUrlCommand");

            var extractTextFromImageStreamButton = new Button
            {
                Text = "Reconhecer Emoção (Stream)",
                FontSize = 21
            };
            extractTextFromImageStreamButton.SetBinding(Button.CommandProperty, "AnalisarStreamImagemCommand");

            var isBusyActivityIndicator = new ActivityIndicator();
            isBusyActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            isBusyActivityIndicator.SetBinding(ActivityIndicator.IsEnabledProperty, "IsBusy");
            isBusyActivityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");

            var errorMessageLabel = new Label
            {
                TextColor = Color.Red,
                FontSize = 20
            };
            errorMessageLabel.SetBinding(Label.TextProperty, "MessagemErro");

            var emotionsDataTemplate = new DataTemplate(() =>
            {
                var angerLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                angerLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Anger",
                    BindingMode.Default,
                    null,
                    null,
                    "Raiva : {0:F0}"));

                var contemptLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                contemptLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Contempt",
                    BindingMode.Default,
                    null,
                    null,
                    "Desprezo: {0:F0}"));

                var disgustLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                disgustLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Disgust",
                    BindingMode.Default,
                    null,
                    null,
                    "Desgosto: {0:F0}"));

                var fearLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                fearLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Fear",
                    BindingMode.Default,
                    null,
                    null,
                    "Medo: {0:F0}"));

                var happinessLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                happinessLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Happiness",
                    BindingMode.Default,
                    null,
                    null,
                    "Alegria: {0:F0}"));

                var neutralLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                neutralLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Neutral",
                    BindingMode.Default,
                    null,
                    null,
                    "Neutro: {0:F0}"));

                var sadnessLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                sadnessLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Sadness",
                    BindingMode.Default,
                    null,
                    null,
                    "Tristeza: {0:F0}"));

                var surpriseLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                surpriseLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Surprise",
                    BindingMode.Default,
                    null,
                    null,
                    "Surpreso: {0:F0}"));

                var faceStackLayout = new StackLayout
                {
                    Padding = 5,
                    Children =
                    {
                       angerLabel,
                       contemptLabel,
                       disgustLabel,
                       fearLabel,
                       happinessLabel,
                       neutralLabel,
                       sadnessLabel,
                       surpriseLabel
                    }
                };

                return new ViewCell
                {
                    View = faceStackLayout
                };
            });

            var regionsListView = new ListView
            {
                HasUnevenRows = true,
                ItemTemplate = emotionsDataTemplate
            };
            regionsListView.SetBinding(ListView.ItemsSourceProperty, "ResultadoImagem");

            var stackLayout = new StackLayout
            {
                Padding = new Thickness(10, 20),
                Children =
                {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Children =
                        {
                            takePhotoButton,
                            pickPhotoButton
                        }
                    },
                    imageUrlEntry,
                    //testeBinding,
                    image,
                    extractTextFromImageUrlButton,
                    extractTextFromImageStreamButton,
                    isBusyActivityIndicator,
                    errorMessageLabel,
                    regionsListView
                }
            };

            Content = new ScrollView
            {
                Content = stackLayout
            };
        }
        //InitializeComponent();
    }
}
