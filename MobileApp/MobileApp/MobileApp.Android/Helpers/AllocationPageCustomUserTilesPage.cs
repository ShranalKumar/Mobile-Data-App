using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileApp.Constants;
using MobileApp.Droid.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


namespace MobileApp.Droid.Helpers
{
	public partial class AllocationPageCustomUserTilesPage : ContentView
	{
		public static List<LinearLayout> UserTiles;
		public static List<SeekBar> _seekbars;
		public static double totalAllocated = 0;
		public static double progressChanged;
		public static double unallocated;
		private static User _currentUser;

		public static void getTiles(LinearLayout parent)
		{
			UserTiles = new List<LinearLayout>();
			int pixelDensity = (int)Android.Content.Res.Resources.System.DisplayMetrics.Density;
			_seekbars = new List<SeekBar>();

			foreach (User user in Controller._users)
			{
				_currentUser = user;
				ContextThemeWrapper allocationUserTileContext = new ContextThemeWrapper(parent.Context, Resource.Style.AllocationUserTileLayoutStyle);
				LinearLayout User = new LinearLayout(allocationUserTileContext);
				User.Orientation = Orientation.Vertical;
				User.Id = Int32.Parse(user.UID);

				ContextThemeWrapper TextLayoutContext = new ContextThemeWrapper(parent.Context, Resource.Style.UserTextLayoutStyle);
				FrameLayout UserDetailsTextLayout = new FrameLayout(TextLayoutContext);

				ContextThemeWrapper userNameContext = new ContextThemeWrapper(UserDetailsTextLayout.Context, Resource.Style.UserTextLeftAlignText);
				TextView UserName = new TextView(userNameContext);
				UserName.Text = user.Name.FirstName;

				ContextThemeWrapper userAllocatedContext = new ContextThemeWrapper(UserDetailsTextLayout.Context, Resource.Style.UserTextRightAlignText);
				TextView Allocated = new TextView(userAllocatedContext);
				Allocated.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(user.Allocated, 2));

				ContextThemeWrapper userAllocatedSliderContext = new ContextThemeWrapper(UserDetailsTextLayout.Context, Resource.Style.UserAllocationSliderStyle);
				SeekBar userAllocationSlider = new SeekBar(userAllocatedSliderContext, null, Resource.Style.UserAllocationSliderStyle);
				userAllocationSlider.Max = ((int)Controller._planDataPool + (int)Controller._addOns) * 10;
				userAllocationSlider.Id = Int32.Parse(user.UID);
                userAllocationSlider.SecondaryProgress = (int)((user.Used / (Controller._planDataPool + Controller._addOns)) * userAllocationSlider.Max);

                //userAllocationSlider.Max = (int)((user.Allocated + unallocated) / Controller._planDataPool) * 100;
                _seekbars.Add(userAllocationSlider);
				_seekbars.ForEach(x => totalAllocated += (((double)x.Progress / userAllocationSlider.Max) * Controller._planDataPool));
				unallocated = Controller._planDataPool - totalAllocated + Controller._addOns;
				double progress = (user.Allocated / (Controller._planDataPool + Controller._addOns)) * userAllocationSlider.Max;
				userAllocationSlider.Progress = (int)(progress);

				userAllocationSlider.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
				{
					if (e.FromUser)
					{
						userAllocationSlider.Max = ((int)Controller._planDataPool + (int)Controller._addOns) * 10;
                        userAllocationSlider.SecondaryProgress = (int)((user.Used / (Controller._planDataPool + Controller._addOns)) * userAllocationSlider.Max);
                        totalAllocated = unallocated = 0;
						_seekbars.ForEach(x => totalAllocated += (((double)x.Progress / userAllocationSlider.Max) * (Controller._planDataPool + Controller._addOns)));
						unallocated = Controller._planDataPool - totalAllocated + Controller._addOns;
						double progressChanged = ((double)e.Progress / userAllocationSlider.Max) * (Controller._planDataPool + Controller._addOns);

						List<double> allocatedInProportion = new List<double>();
						_seekbars.ForEach(x => allocatedInProportion.Add(x.Progress));
						List<double> allocatedInGB = new List<double>();
						allocatedInProportion.ForEach(x => allocatedInGB.Add((x / userAllocationSlider.Max) * (Controller._planDataPool + Controller._addOns)));
						double sumAllocatedInGB = allocatedInGB.Sum();
						double reservedData = (Controller._planDataPool - sumAllocatedInGB) + Controller._addOns;
						reservedData = Math.Round(reservedData, 2);
						double reservedToProgress = ((reservedData / (Controller._planDataPool + Controller._addOns)) * userAllocationSlider.Max);
						double max = e.Progress + reservedToProgress;

						//double tempGigaByte = unallocated + (((double)xyz[0] / 100) * (Controller._planDataPool / (progressChanged + unallocated)));
						//int maxBound = (int)((tempGigaByte / Controller._planDataPool) * 100);


						//var thisBar = _seekbars[_seekbars.IndexOf(userAllocationSlider)];
						//thisBar.Progress = e.Progress;                      

						if (progressChanged <= user.Used)
						{
							progressChanged = user.Used;
							Allocated.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(progressChanged, 2));
							_seekbars[_seekbars.IndexOf(userAllocationSlider)].Progress = (int)(Math.Round((user.Used / (Controller._planDataPool + Controller._addOns)) * userAllocationSlider.Max,2));   //This is where it's causing the problem

						}
						else if (e.Progress >= max)
						{
							progressChanged = ((double)(e.Progress + reservedToProgress) / userAllocationSlider.Max) * (Controller._planDataPool + Controller._addOns);
							Allocated.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(progressChanged, 2));
							_seekbars[_seekbars.IndexOf(userAllocationSlider)].Progress = (int)(Math.Round((progressChanged / (Controller._planDataPool + Controller._addOns)) * userAllocationSlider.Max, 2));
						}
						else
						{
							Allocated.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(progressChanged, 2));
							_seekbars[_seekbars.IndexOf(userAllocationSlider)].Progress = e.Progress;
						}

						totalAllocated = unallocated = 0;
						_seekbars.ForEach(x => totalAllocated += (((double)x.Progress / userAllocationSlider.Max) * (Controller._planDataPool + Controller._addOns)));
						unallocated = Controller._planDataPool - totalAllocated + Controller._addOns;

						allocatedInProportion.Clear();
						_seekbars.ForEach(x => allocatedInProportion.Add(x.Progress));
						allocatedInGB.Clear();
						allocatedInProportion.ForEach(x => allocatedInGB.Add((x / userAllocationSlider.Max) * (Controller._planDataPool + Controller._addOns)));
						sumAllocatedInGB = allocatedInGB.Sum();
						reservedData = Controller._planDataPool - sumAllocatedInGB + Controller._addOns;
						AllocationPageView._remainingDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Math.Round(reservedData, 2));
						unallocated = (int)Controller._planDataPool - (int)sumAllocatedInGB + Controller._addOns;
					}

				};
				parent.AddView(User);
				User.AddView(UserDetailsTextLayout);
				UserDetailsTextLayout.AddView(UserName);
				UserDetailsTextLayout.AddView(Allocated);
				User.AddView(userAllocationSlider);
				UserTiles.Add(User);
			}
		}
	}
}
