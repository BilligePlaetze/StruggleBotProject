﻿using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using StruggleBud.Resources;
using StruggleBud.Dialogs.Habits;

namespace StruggleBud.Dialogs.DataCollection
{
    [Serializable]
    public class RootDataCollectionDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {

            context.Call(new StruggleBud.Dialogs.DataCollection.Calendar.CalendarDataCollectorDialog(), this.CalenderDataCollectionCompletedAsync);

            return Task.CompletedTask;
        }

        private async Task CalenderDataCollectionCompletedAsync(IDialogContext context, IAwaitable<object> result)
        {
            await StartHabitDataCollectionAsync(context, result);
        }

        private async Task StartHabitDataCollectionAsync(IDialogContext context, IAwaitable<object> result)
        {
            await context.PostAsync(StringResources.TimeBlockerMessage1);

            context.Call(new RootHabitDialog(), this.ShowHabitOverView);
        }

        private Task ShowHabitOverView(IDialogContext context, IAwaitable<object> result)
        {
            context.Call(new FinishDataCollectionDialog(), this.EndOfHabitDataCollection);
            return Task.CompletedTask;
        }

        private Task EndOfHabitDataCollection(IDialogContext context, IAwaitable<object> result)
        {
            context.Done(true);
            return Task.CompletedTask;
        }
    }
}