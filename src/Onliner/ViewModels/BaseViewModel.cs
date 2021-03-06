﻿using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace Onliner
{
    public class BaseViewModel : INotifyPropertyChanged
	{
		public BaseViewModel ()
		{
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string propertyName){

			if (PropertyChanged == null) {
				return;
			}

			PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
		}

        private bool isBusy;
        /// <summary>
        /// Gets or sets if the view is busy.
        /// </summary>
        public const string IsBusyPropertyName = "IsBusy";
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value, IsBusyPropertyName); }
        }

        private bool canLoadMore = true;
        /// <summary>
        /// Gets or sets if we can load more.
        /// </summary>
        public const string CanLoadMorePropertyName = "CanLoadMore";
        public bool CanLoadMore
        {
            get { return canLoadMore; }
            set { SetProperty(ref canLoadMore, value, CanLoadMorePropertyName); }
        }

        protected void SetProperty<T>(
            ref T backingStore, T value,
            string propertyName,
            Action onChanged = null,
            Action<T> onChanging = null)
        {


            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            if (onChanging != null)
                onChanging(value);

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
        }


	}
}

