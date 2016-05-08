﻿namespace Grouping
{
    using System;
    using System.Collections.ObjectModel;
    using System.Reactive.Linq;
    using System.Windows.Threading;
    using DynamicData;
    using DynamicData.Binding;

    public class AgePersonPair : AbstractNotifyPropertyChanged, IDisposable
    {
        private readonly ReadOnlyObservableCollection<Person> people;
        private readonly IDisposable cleanup;

        public AgePersonPair(IGroup<Person, int> group, Dispatcher dispatcher)
        {
            cleanup = group.List.Connect()
                .ObserveOn(dispatcher)
                .Bind(out people)
                .Subscribe();

            Age = group.GroupKey;
        }

        public ReadOnlyObservableCollection<Person> People => people;

        public int Age { get; set; }

        public void Dispose()
        {
            cleanup.Dispose();
        }
    }    
}