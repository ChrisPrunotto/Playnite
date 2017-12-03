﻿using NLog;
using Playnite;
using Playnite.MetaProviders;
using PlayniteUI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayniteUI.ViewModels
{
    public class MetadataDownloadViewModel : ObservableObject
    {
        public enum ViewMode
        {
            Wizard,
            Manual
        }

        private static Logger logger = LogManager.GetCurrentClassLogger();
        private IWindowFactory window;


        private int viewTabIndex = 0;
        public int ViewTabIndex
        {
            get => viewTabIndex;
            set
            {
                viewTabIndex = value;
                OnPropertyChanged("ViewTabIndex");
                OnPropertyChanged("ShowDownloadButton");
                OnPropertyChanged("ShowNextButton");
                OnPropertyChanged("ShowBackButton");
            }
        }

        public bool ShowDownloadButton
        {
            get => ViewTabIndex == 1;
        }

        public bool ShowNextButton
        {
            get => Mode == ViewMode.Wizard && ViewTabIndex == 0;
        }

        public bool ShowBackButton
        {
            get => Mode == ViewMode.Wizard && ViewTabIndex == 1;
        }

        private ViewMode mode;
        public ViewMode Mode
        {
            get
            {
                return mode;
            }

            set
            {
                mode = value;
                ViewTabIndex = mode == ViewMode.Wizard ? 0 : 1;
                OnPropertyChanged("Mode");
                OnPropertyChanged("ShowDownloadButton");
                OnPropertyChanged("ShowNextButton");
                OnPropertyChanged("ShowBackButton");
            }
        }

        private MetadataDownloaderSettings settings = new MetadataDownloaderSettings();
        public MetadataDownloaderSettings Settings
        {
            get
            {
                return settings;
            }

            set
            {
                settings = value;
                OnPropertyChanged("Settings");
            }
        }

        public RelayCommand<object> DownloadCommand
        {
            get => new RelayCommand<object>((a) =>
            {
                CloseDialog(true);
            });
        }

        public RelayCommand<object> CancelCommand
        {
            get => new RelayCommand<object>((a) =>
            {
                CloseDialog(false);
            });
        }

        public RelayCommand<object> NextCommand
        {
            get => new RelayCommand<object>((a) =>
            {
                Next();
            });
        }

        public RelayCommand<object> BackCommand
        {
            get => new RelayCommand<object>((a) =>
            {
                Back();
            });
        }

        public MetadataDownloadViewModel(IWindowFactory window)
        {
            this.window = window;            
        }

        public bool? OpenView(ViewMode mode)
        {
            Mode = mode;
            return window.CreateAndOpenDialog(this);
        }

        public void CloseDialog(bool success)
        {
            window.Close(success);
        }

        public void Next()
        {
            ViewTabIndex = 1;
        }

        public void Back()
        {
            ViewTabIndex = 0;
        }
    }
}
