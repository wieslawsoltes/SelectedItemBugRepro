using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
    
    public class MainWindowViewModel : ViewModelBase
    {
        private ItemViewModel? _selectedItem;
        private ObservableCollection<ItemViewModel> _items;
        private ObservableCollection<ItemViewModel> _selectedItems;
            
        public ItemViewModel? SelectedItem
        {
            get => _selectedItem;
            set => SetSelectedItem(value);
        }
        
        public ObservableCollection<ItemViewModel> Items
        {
            get => _items;
            set => this.RaiseAndSetIfChanged(ref _items, value);
        }

        public ObservableCollection<ItemViewModel> SelectedItems
        {
            get => _selectedItems;
            set => this.RaiseAndSetIfChanged(ref _selectedItems, value);
        }

        public ICommand DemoCommand { get; }

        public MainWindowViewModel()
        {
            _items = new ObservableCollection<ItemViewModel>();
            _selectedItems = new ObservableCollection<ItemViewModel>();

            var test1 = new ItemViewModel() {Title = "test1"};
            var test2 = new ItemViewModel() {Title = "test2"};
            var test3 = new ItemViewModel() {Title = "test3"};

            _items.Add(test1);
            _items.Add(test2);
            _items.Add(test3);

            _selectedItem = test1;

            DemoCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                Items.Remove(test1);

                await Task.Delay(100);

                RaiseAndChangeSelectedItem(null);
                RaiseAndChangeSelectedItem(null);

                await Task.Delay(100);

                SelectedItems.Add(test1);

                RaiseAndChangeSelectedItem(null);
                RaiseAndChangeSelectedItem(test1);

                await Task.Yield();
            });
        }
        
        private void RaiseAndChangeSelectedItem(ItemViewModel? value)
        {
            _selectedItem = value;
            this.RaisePropertyChanged(nameof(SelectedItem));
        }

        private void Select(ItemViewModel? value)
        {
            if (_selectedItem == value)
            {
                return;
            }
            
            RaiseAndChangeSelectedItem(null);
            RaiseAndChangeSelectedItem(value);
        }

        public void SetSelectedItem(ItemViewModel? value)
        {
            Select(value);
        }
    }
}