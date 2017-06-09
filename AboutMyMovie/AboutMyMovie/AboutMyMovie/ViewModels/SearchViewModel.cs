using System;
using AboutMyMovie.Interfaces;
using Xamarin.Forms;

namespace AboutMyMovie.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        private readonly IMovieApi _movieApi;

        public Command SearchMovieCommand { get; }

        #region Getters and Setters
        private string _nameToSearch;
        public string NameToSearch
        {
            get { return _nameToSearch; }
            set
            {
                SetProperty(ref _nameToSearch, value);
                SearchMovieCommand.ChangeCanExecute();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _year;
        public string Year
        {
            get { return _year; }
            set { SetProperty(ref _year, value); }
        }

        private string _released;
        public string Released
        {
            get { return _released; }
            set { SetProperty(ref _released, value); }
        }

        private string _genre;
        public string Genre
        {
            get { return _genre; }
            set { SetProperty(ref _genre, value); }
        }

        private string _director;
        public string Director
        {
            get { return _director; }
            set { SetProperty(ref _director, value); }
        }


        private bool _isGridVisible;

        public bool IsGridVisible
        {
            get { return _isGridVisible; }
            set { SetProperty(ref _isGridVisible, value); }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage;}
            set { SetProperty(ref _errorMessage, value); }
        }

        private bool _isErrorMessageVisible;

        public bool IsErrorMessageVisible
        {
            get { return _isErrorMessageVisible; }
            set { SetProperty(ref _isErrorMessageVisible, value); }
        }
        #endregion

        public SearchViewModel()
        {
            IsGridVisible = false;
            IsErrorMessageVisible = false;
            _movieApi = DependencyService.Get<IMovieApi>();
            SearchMovieCommand = new Command(ExecuteSearchMovieCommand, CanExecuteSearchMovieCommand);
        }

        private bool CanExecuteSearchMovieCommand()
        {
            return !string.IsNullOrEmpty(NameToSearch);
        }

        private async void ExecuteSearchMovieCommand()
        {
            try
            {
                var movie = await _movieApi.GetMovieInfoAsync(NameToSearch);
                if (movie == null)
                {
                    ErrorMessage = "Movie not found";
                    IsErrorMessageVisible = true;
                    return;
                }

                IsErrorMessageVisible = false;

                Name = movie.Name;
                Year = movie.Year;
                Released = movie.Released;
                Genre = movie.Genre;
                Director = movie.Director;

                IsGridVisible = true;
            }
            catch (Exception ex)
            {                
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

    }
}
