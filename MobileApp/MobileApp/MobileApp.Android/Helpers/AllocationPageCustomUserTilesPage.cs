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
                userAllocationSlider.Id = Int32.Parse(user.UID);
                
                //userAllocationSlider.Max = (int)((user.Allocated + unallocated) / Controller._planDataPool) * 100;
                _seekbars.Add(userAllocationSlider);
                _seekbars.ForEach(x => totalAllocated += (((double)x.Progress / 100) * Controller._planDataPool));
                unallocated = Controller._planDataPool - totalAllocated;
                double progress = (user.Allocated / Controller._planDataPool) * 100;
                userAllocationSlider.Progress = (int)(progress);
                
                userAllocationSlider.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) =>
                {
                    if (e.FromUser)
                    {
                        totalAllocated = unallocated = 0;
                        _seekbars.ForEach(x => totalAllocated += (((double)x.Progress / 100) * Controller._planDataPool));
                        unallocated = Controller._planDataPool - totalAllocated;
                        double progressChanged = ((double)e.Progress / 100) * (Controller._planDataPool);

                        List<int> xyz = new List<int>();
                        _seekbars.ForEach(x => xyz.Add(x.Progress));
                        int reservedToProgress = (int)((unallocated / Controller._planDataPool) * 100);
                        int max = e.Progress + reservedToProgress;

                        //double tempGigaByte = unallocated + (((double)xyz[0] / 100) * (Controller._planDataPool / (progressChanged + unallocated)));
                        //int maxBound = (int)((tempGigaByte / Controller._planDataPool) * 100);


                        //var thisBar = _seekbars[_seekbars.IndexOf(userAllocationSlider)];
                        //thisBar.Progress = e.Progress;                      

                        if (progressChanged <= user.Used)
                        {
                            progressChanged = user.Used;
                            Allocated.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(progressChanged, 2));
                            _seekbars[_seekbars.IndexOf(userAllocationSlider)].Progress = (int)((user.Used / Controller._planDataPool)  * 100);
                            //thisBar = _seekbars[_seekbars.IndexOf(userAllocationSlider)];
                            //thisBar.Progress = (int)((user.Used / Controller._planDataPool) * 100);
                            //_seekbars[_seekbars.IndexOf(userAllocationSlider)].Progress = thisBar.Progress;
                        }
                        else if (e.Progress >= max)
                        {
                            progressChanged = ((double)(e.Progress + reservedToProgress) / 100) * Controller._planDataPool;
                            Allocated.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(progressChanged, 2));
                            _seekbars[_seekbars.IndexOf(userAllocationSlider)].Progress = (int)((progressChanged / Controller._planDataPool) * 100);
                        }
                        //else if (xyz[0] > maxBound)
                        //{
                        //    Allocated.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(((double)maxRange1 / 100) * Controller._planDataPool), 2);
                        //    _seekbars[_seekbars.IndexOf(userAllocationSlider)].Progress = maxBound; ///*(int)((*/_seekbars[_seekbars.IndexOf(userAllocationSlider)].Progress/* / Controller._planDataPool) * 100)*/;
                        //}
                        else
                        {
                            Allocated.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(progressChanged, 2));
                            _seekbars[_seekbars.IndexOf(userAllocationSlider)].Progress = e.Progress;
                        }


                        //else if (totalAllocated >= Controller._planDataPool)
                        //{
                        //    progressChanged = user.Allocated;
                        //    Allocated.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round(progressChanged, 1));
                        //    _seekbars[_seekbars.IndexOf(userAllocationSlider)].Progress = (int)((user.Allocated / Controller._planDataPool) * 100);
                        //    thisBar = _seekbars[_seekbars.IndexOf(userAllocationSlider)];
                        //    thisBar.Progress = (int)((user.Allocated / Controller._planDataPool) * 100);
                        //    _seekbars[_seekbars.IndexOf(userAllocationSlider)].Progress = thisBar.Progress;
                        //}

                        totalAllocated = unallocated = 0;
                        //_seekbars.ForEach(x => x.Max = (int)((Controller._users.Where(y => Int32.Parse(y.UID) == x.Id).FirstOrDefault().Allocated + unallocated) / Controller._planDataPool) * 100);
                        _seekbars.ForEach(x => totalAllocated += (((double)x.Progress / 100) * Controller._planDataPool));
                        unallocated = Controller._planDataPool - totalAllocated;
                        //if (unallocated == 0)
                        //{
                        //    Allocated.Text = string.Format(StringConstants.Localizable.DataAmount, Math.Round((((double)_seekbars[_seekbars.IndexOf(userAllocationSlider)].Progress / 100) * Controller._planDataPool), 1));
                        //    _seekbars[_seekbars.IndexOf(userAllocationSlider)].Progress = /*(int)((*/_seekbars[_seekbars.IndexOf(userAllocationSlider)].Progress/* / Controller._planDataPool) * 100)*/;
                        //}
                        AllocationPageView._remainingDataAmount.Text = String.Format(StringConstants.Localizable.DataAmount, Math.Round(unallocated, 2));
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
