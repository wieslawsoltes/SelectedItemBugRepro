using ReactiveUI;

namespace SelectedItemBugRepro.ViewModels
{
    public class ItemViewModel : ViewModelBase
    {
        private string _title;

        public string Title
        {
            get => _title;
            set => this.RaiseAndSetIfChanged(ref _title, value);
        }
    }
}