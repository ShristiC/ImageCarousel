using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

namespace Interviews;

// functional code 
public partial class MainPage : ContentPage
{	
	/// <summary>
	/// Collection of ImageCarousel objects containing images and descriptions for each images
	/// </summary>
	public ObservableCollection<ImageCarousel> Images {get; set;}

	/// <summary>
	/// Number of Items in the ImageCarousel
	/// </summary>
	public int TotalCount {
		get {
			return Images.Count;
		}
	}

	/// <summary>
	/// Pointer 
	/// </summary>
	public ImageCarousel? CurrentItem {
		get {
			if(Images.Count > 0) {
				return Images[CurrItemIdx] ;
			} 

			return null;
		}
	}

	public int CurrItemIdx {get; set;}

	public MainPage()
	{
		InitializeComponent();

		Images = [];
		PopulateImages();
		BindingContext = this;
	}

	/// <summary>
	/// Populating images from a csv data file
	/// </summary>
	private async void PopulateImages() {
		using var stream = await Microsoft.Maui.Storage.FileSystem.OpenAppPackageFileAsync("image_data.csv");
		using var reader = new StreamReader(stream);

		// discard first line - header
		var content = reader.ReadLine();
		// get the first content line
		content = reader.ReadLine();
		while(content != null) {
			var split_content = content.Split(",");
			var imageItem = new ImageCarousel(split_content);
			Images.Add(imageItem);
			content = reader.ReadLine();
		}
	}

	/// <summary>
	/// Event handler for adding an experience to the image carousel
	/// </summary>
	/// <param name="sender"> Request Control of the event</param>
	/// <param name="e"> optional arguments passed in </param>
	private void AddImage(object sender, EventArgs e) {
		Images.Add(new ImageCarousel());
		CurrItemIdx = TotalCount - 1;
		SemanticScreenReader.Announce("A new experience has been added!");
		OnPropertyChanged(propertyName:nameof(TotalCount));
		OnPropertyChanged(propertyName: nameof(CurrentItem));
	}

	/// <summary>
	/// Event handler for removing the last experience rom the image carousel
	/// </summary>
	/// <param name="sender"> Request Control of the event</param>
	/// <param name="e"> optional arguments passed in </param>
	private void RemoveLastImage(object sender, EventArgs e) {
		if(TotalCount == 0) {
			SemanticScreenReader.Announce("An Image cannot be removed. There are no Images in the Carousel.");
			return;
		}
		Images.RemoveAt(TotalCount - 1);
		CurrItemIdx = TotalCount;
		OnPropertyChanged(propertyName: nameof(TotalCount));
		OnPropertyChanged(propertyName:nameof(CurrentItem));

	}

	/// <summary>
	/// Handler to go to a previous image
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void OnGoToPreviousImage(object sender, EventArgs e) {
		if(CurrItemIdx == 0) {
			SemanticScreenReader.Announce("There is no previous image to go to.");
			return;
		}
		CurrItemIdx -= 1;
		OnPropertyChanged(propertyName:nameof(CurrentItem));
	}

	/// <summary>
	/// Handler to go to the next image in the carousel
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	private void OnGoToNextImage(object sender, EventArgs e) {
		if(CurrItemIdx == TotalCount - 1) {
			SemanticScreenReader.Announce("There is no next image to go to.");
			return;
		}
		CurrItemIdx += 1;
		OnPropertyChanged(propertyName:nameof(CurrentItem));
	}

}

public class ImageCarousel
{
	/// <summary>
	/// Path by which to find and load the image from
	/// </summary>
    public string ImageUrl { get; set; }
	/// <summary>
	/// Name or Title of the image to be placed in the card
	/// </summary>
    public string Title { get; set; }
	/// <summary>
	/// Caption, Description, and Additional Details of the image
	/// </summary>
    public string Description { get; set; }
	/// <summary>
	/// Alt text of the image
	/// </summary>
    public string AltText { get; set; }

	public ImageCarousel() {
		Title="Harmony Together";
		Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
		ImageUrl="man_playing_instrument_with_child.jpg";
		AltText="An old man is playing an instrument with a child who uses a wheelchair";
	}

	/// <summary>
	/// Creating a set Image Carousel given an array of image data
	/// </summary>
	/// <param name="arr"></param>
	public ImageCarousel(String[] arr) {
		if(arr.Length != 4) {
			throw new Exception("array passed in must be of length 4");
		}
		
		ImageUrl = arr[0];
		Title = arr[1];
		Description = arr[2];
		AltText = arr[3];
		
	}
}

