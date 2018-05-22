using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
	public class MainPageViewModel : BaseViewModel
    {
	    public Command DownloadCommand { get; private set; }
		private string _downloadStatus;
		public string DownloadStatus { get { return _downloadStatus; } 
			private set {
				_downloadStatus = value;
				OnPropertyChanged();
			} }
	    
	    public ObservableCollection<ItemViewModel> Items { get; private set; }
        public MainPageViewModel()
        {
	        DownloadCommand = new Command(async () => {
		        await ExecuteDownloadCommand();});
	        
	        Items = new ObservableCollection<ItemViewModel>();
	        for (int i = 0; i < 10000; i++) {
		        Items.Add(new ItemViewModel{Id = i, Name = "Item " +(i+1), Remark = "This is Item Nr " + (i+1)});
	        }
        }

	    private async Task ExecuteDownloadCommand() {
		    DownloadStatus = "Requesting";
		    HttpClient client = new HttpClient();
		    var watch = Stopwatch.StartNew();
		    var resp = await client.GetAsync("http://tpcproxy.tunnelsoft.com/pcmd?cmd=SegmentUpdated&tbmId=60001&count=20&target=SERVER:demo");
		    watch.Stop();
		    var length = resp.Content.Headers.ContentLength;
		    if (length.HasValue) {
			    DownloadStatus = "Completed "+ length.Value + " byte after " + watch.ElapsedMilliseconds+" millis.";
		    }
		    else {
			    DownloadStatus = "Complete after " + watch.ElapsedMilliseconds+" millis.";
		    }
		    
	    }
    }

	public class ItemViewModel : BaseViewModel {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Remark { get; set; }
	}
}
