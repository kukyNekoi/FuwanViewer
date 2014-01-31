﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FuwanViewer.Model.VisualNovels;
using FuwanViewer.Presentation.ViewModels.Abstract;

namespace FuwanViewer.Presentation.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private HistoryList<VisualNovel> _recentlyViewedNovels;

        public ICommand OpenVisualNovelCommand { get; set; }
        public ICommand OpenAllNovelsCommand { get; set; }
        public ICommand OpenFeaturedNovelsCommand { get; set; }
        public List<VisualNovelWidgetViewModel> RecentlyViewedNovels 
        {
            get
            {
                return _recentlyViewedNovels.ToList().ConvertAll(
                    delegate (VisualNovel vn)
                    {
                        return new VisualNovelWidgetViewModel(vn, (param) => OpenVisualNovelCommand.Execute(param));
                    });
            }
        }

        public HomeViewModel(Action<VisualNovel> openVisualNovel, Action openAllNovels, Action openFeaturedNovels, HistoryList<VisualNovel> recentlyViewedNovelsCollection)
        {
            base.DisplayName = "Home";

            this.OpenVisualNovelCommand = new RelayCommand((o) => openVisualNovel(o as VisualNovel));
            this.OpenAllNovelsCommand = new RelayCommand(openAllNovels);
            this.OpenFeaturedNovelsCommand = new RelayCommand(openFeaturedNovels);
            
            this._recentlyViewedNovels = recentlyViewedNovelsCollection;
            // RecentlyViewedNovels = new ObservableCollection<VisualNovelWidgetViewModel>();
            _recentlyViewedNovels.CollectionChanged += UpdateWidgetCollection;
        }

        void UpdateWidgetCollection(object sender, NotifyCollectionChangedEventArgs e)
        {
            //if (e.Action == NotifyCollectionChangedAction.Move)
            //{
            //    RecentlyViewedNovels.Move(e.OldStartingIndex, e.NewStartingIndex);
            //    return;
            //}

            //if (e.NewItems.Count > 0)
            //{
            //    foreach (var item in e.NewItems)
            //    {
            //        RecentlyViewedNovels.Add(
            //            new VisualNovelWidgetViewModel(
            //                item as VisualNovel,
            //                (vn) => OpenVisualNovelCommand.Execute(vn)));
            //    }
            //}
            //if (e.OldItems != null && e.OldItems.Count > 0)
            //{
            //    var oldNovels = e.OldItems.Cast<VisualNovel>();
            //    var itemsToRemove = RecentlyViewedNovels.Where((vnw) => oldNovels.Contains(vnw._novel));
            //    foreach (var item in itemsToRemove)
            //    {
            //        RecentlyViewedNovels.Remove(item);
            //    }
            //}
            OnPropertyChanged("RecentlyViewedNovels");
        }
    }
}
