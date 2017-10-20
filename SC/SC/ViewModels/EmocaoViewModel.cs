using Plugin.Media;
using Plugin.Media.Abstractions;
using SC.Models;
using SC.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SC.ViewModels
{
    public class EmocaoViewModel : BaseViewModel
    {
        private const string EmotionApiKey = "f21c2dfc44df4dc79ce2f5785d89181d";
        private readonly EmotionService _emotionService = new EmotionService(EmotionApiKey);
        private Stream _imagemStream;


        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value; OnPropertyChanged(); TirarFotoCommand.ChangeCanExecute(); AbrirFotoCommand.ChangeCanExecute();
                AnalisarImagemUrlCommand.ChangeCanExecute();  AnalisarStreamImagemCommand.ChangeCanExecute();
            }
        }



        private List<ResultadoEmocao> _resultadoImagem;

        public List<ResultadoEmocao> ResultadoImagem
        {
            get { return _resultadoImagem; }
            set { _resultadoImagem = value; OnPropertyChanged(); }
        }

        private string _imageUrl;

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; OnPropertyChanged(); }
        }

        private string _messagemErro;

        public string MessagemErro
        {
            get { return _messagemErro; }
            set { _messagemErro = value; OnPropertyChanged(); }
        }
        
        public Command TirarFotoCommand { get; }
        public Command AbrirFotoCommand { get; }
        public Command AnalisarImagemUrlCommand { get; } //continuar daqui
        public Command AnalisarStreamImagemCommand { get; }

        public EmocaoViewModel()
        {
            ResultadoImagem = new List<ResultadoEmocao>();
            TirarFotoCommand = new Command(async () => await ExecuteTirarFotoCommand(),()=> !IsBusy);
            AbrirFotoCommand = new Command(async () => await ExecuteAbrirFotoCommand(), ()=>!IsBusy);
            AnalisarImagemUrlCommand = new Command(async () => await ExecuteAnalisarImagemUrlCommand(), () => !IsBusy);
            AnalisarStreamImagemCommand = new Command(async () => await ExecuteAnalisarStreamImagemCommand(), () => !IsBusy);

        }

        async Task ExecuteAnalisarStreamImagemCommand()
        {
            IsBusy = true;
            try
            {
                ResultadoImagem = null;
                MessagemErro = string.Empty;

              
                ResultadoImagem = await _emotionService.ReconhecimentoEmocaoPorStreamAsync(_imagemStream);
            }
            catch (Exception ex)
            {

                MessagemErro = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteAnalisarImagemUrlCommand()
        {
            IsBusy = true;
            try
            {
                ResultadoImagem = null;
                MessagemErro = string.Empty;

                ResultadoImagem = await _emotionService.ReconhecimentoEmocaoPorImagemUrlAsync(ImageUrl);
            }
            catch (Exception ex) 
            {

                MessagemErro = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteTirarFotoCommand()
        {
            await CrossMedia.Current.Initialize();

            var arquivoFoto = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());

            _imagemStream = arquivoFoto?.GetStream();
            ImageUrl = arquivoFoto?.Path;
        }

        async Task ExecuteAbrirFotoCommand()
        {
            await CrossMedia.Current.Initialize();
            var arquivoFoto = await CrossMedia.Current.PickPhotoAsync();

            _imagemStream = arquivoFoto?.GetStream();
            ImageUrl = arquivoFoto?.Path;
        }
    }
}
