using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace FileIOTester.Events {
    public class BackgroundSubscriber {
        CoreDispatcher dispatcher;

        public BackgroundSubscriber(CoreDispatcher dispatcher) {
            this.dispatcher = dispatcher;
        }

        public void HandleTitleChangedEvent(string title) {
            var threadId = Environment.CurrentManagedThreadId;

            // Assign into local variable because it is meant to be fire and forget and calling would require an await/async
            var dialogAction = dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                MessageDialog dialog = new MessageDialog(
                    string.Format(CultureInfo.InvariantCulture,
                        "Title: {0} - in background subscriber on thread {1}", title, threadId));
                var showAsync = dialog.ShowAsync();
            });
        }
    }
}
