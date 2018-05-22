using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using TestApp.Models;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
	public class MainPageViewModel : BaseViewModel {
		private long _listCreationTime;
	    public long ListCreationTime {
		    get { return _listCreationTime; } 
		    set {
			    if (value != _listCreationTime) {
				    _listCreationTime = value;
				    OnPropertyChanged();
			    }
			    
		    }
	    }
	    public Command FillListCommand { get; private set; }
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
	        
	        FillListCommand = new Command(ExecuteFillListCommand);
	        
	        Items = new ObservableCollection<ItemViewModel>();
	        
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

	    private void ExecuteFillListCommand() {
		    Items.Clear();
		    var watch = Stopwatch.StartNew();
		    for (int i = 0; i < 10000; i++) {
			    Items.Add(new ItemViewModel{Id = i, Name = "Item " +(i+1), Remark = "This is Item Nr " + (i+1)});
		    }

		    watch.Stop();
		    ListCreationTime = watch.ElapsedMilliseconds;
	    }
    }

	public class ItemViewModel : BaseViewModel {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Remark { get; set; }
	}
}
