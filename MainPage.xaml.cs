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
	public ImageCarousel? CurrentItem {get;set;}

	public MainPage()
	{
		InitializeComponent();

		Images = [];
		BindingContext = this;
	}

	/// <summary>
	/// Event handler for adding an experience to the image carousel
	/// </summary>
	/// <param name="sender"> Request Control of the event</param>
	/// <param name="e"> optional arguments passed in </param>
	private void AddImage(object sender, EventArgs e) {
		Images.Add(new ImageCarousel());
		CurrentItem = Images[TotalCount - 1];
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
		CurrentItem = TotalCount > 0 ? Images[TotalCount - 1] : null;
		OnPropertyChanged(propertyName: nameof(TotalCount));
		OnPropertyChanged(propertyName:nameof(CurrentItem));

	}

}

public class ImageCarousel
{
	/// <summary>
	/// Name or Title of the image to be placed in the card
	/// </summary>
    public string Name { get; set; }
	/// <summary>
	/// Caption, Description, and Additional Details of the image
	/// </summary>
    public string Details { get; set; }
	/// <summary>
	/// Path by which to find and load the image from
	/// </summary>
    public string ImageUrl { get; set; }

	public ImageCarousel() {
		Name="Harmony Together";
		Details="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
		ImageUrl="man_playing_instrument_with_child.jpg";
	}
}

